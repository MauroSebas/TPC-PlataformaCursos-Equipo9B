using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Cursada; // <--- IMPORTANTE: Usamos la del Dominio

namespace Datos.Cursada
{
    public class EntregaDatos
    {
        // 1. ALUMNO: REGISTRAR ENTREGA
        public void Registrar(Entrega entrega)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Entrega_Crear");
                datos.setearParametro("@InscripcionID", entrega.InscripcionId);
                datos.setearParametro("@ExamenID", entrega.ExamenId);
                datos.setearParametro("@UrlResolucion", entrega.UrlResolucion);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // 2. ALUMNO: OBTENER MI ENTREGA
        public Entrega ObtenerPorInscripcion(int idInscripcion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Entrega_ObtenerPorInscripcion");
                datos.setearParametro("@InscripcionID", idInscripcion);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Entrega aux = new Entrega();
                    aux.Id = (int)datos.Lector["EntregaID"];
                    aux.ExamenId = (int)datos.Lector["ExamenID"];
                    aux.InscripcionId = (int)datos.Lector["InscripcionID"];
                    aux.UrlResolucion = (string)datos.Lector["UrlResolucion"];
                    aux.FechaEntrega = (DateTime)datos.Lector["FechaEntrega"];
                    aux.Estado = (string)datos.Lector["Estado"];

                    if (!(datos.Lector["DevolucionProfesor"] is DBNull))
                    {
                        aux.DevolucionProfesor = (string)datos.Lector["DevolucionProfesor"];
                    }

                    return aux;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // 3. ADMIN: LISTAR PENDIENTES
        public List<Entrega> ListarPendientes()
        {
            List<Entrega> lista = new List<Entrega>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Entrega_ListarPendientes");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Entrega aux = new Entrega();
                    aux.Id = (int)datos.Lector["EntregaID"];
                    aux.FechaEntrega = (DateTime)datos.Lector["FechaEntrega"];
                    aux.UrlResolucion = (string)datos.Lector["UrlResolucion"];
                    aux.Estado = (string)datos.Lector["Estado"];
                    aux.InscripcionId = (int)datos.Lector["InscripcionID"];
                    aux.EmailAlumno = (string)datos.Lector["AlumnoEmail"];

                    if (!(datos.Lector["AlumnoNombre"] is DBNull))
                        aux.NombreAlumno = (string)datos.Lector["AlumnoNombre"];

                    aux.TituloCurso = (string)datos.Lector["CursoTitulo"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // 4. ADMIN: CORREGIR
        public void Corregir(int idEntrega, string estado, string devolucion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Entrega_Corregir");
                datos.setearParametro("@EntregaID", idEntrega);
                datos.setearParametro("@Estado", estado);
                datos.setearParametro("@Devolucion", devolucion);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


    }
}
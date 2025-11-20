using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class InscripcionDatos
    {
        public AccesoDatos datos { get; set; }

        public int AltaInscripcion(Inscripcion nueva)
        {

            try
            {
                datos.setearConSP("sp_Inscripcion_Alta");
                datos.setearParametro("@IdUsuario", nueva.UsuarioID);
                datos.setearParametro("@IdCurso", nueva.CursoID);
                datos.setearParametro("@FechaInscripcion", nueva.FechaInscripcion);
                datos.setearParametro("FechaExpiracion", nueva.FechaExpiracion);
                datos.setearParametro("Estado", nueva.Estado);

                int idNuevo = datos.ejecutarAccionScalar();
                return idNuevo;
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

        public Inscripcion BuscarInscripcion(int idUsuario, int idCurso)
        {
            Inscripcion aux = new Inscripcion();

            try
            {
                datos.setearConSP("sp_Inscripcion_Obtener");
                datos.setearParametro("@IdUsuario", idUsuario);
                datos.setearParametro("@IdCurso", idCurso);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {

                    aux.Id = (int)datos.Lector["InscripcionID"];
                    aux.UsuarioID = (int)datos.Lector["UsuarioID"];
                    aux.CursoID = (int)datos.Lector["CursoID"];
                    aux.FechaInscripcion = (DateTime)datos.Lector["FechaInscripcion"];
                    aux.FechaExpiracion = (DateTime)datos.Lector["FechaExpiracion"];
                    aux.Estado = (string)datos.Lector["Estado"];

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


        public List<Inscripcion> ListarUsuariosInscripcion(int idUsuario)
        {
            List<Inscripcion> lista = new List<Inscripcion>();

            try
            {
                    datos.setearConSP("sp_Inscripcion_ListarPorUsuario");
                    datos.setearParametro("@IdUsuario", idUsuario);
                    datos.ejecutarLectura();

                    while (datos.Lector.Read())
                    {
                        Inscripcion aux = new Inscripcion();
                        aux.Id = (int)datos.Lector["InscripcionID"];
                        aux.UsuarioID = (int)datos.Lector["UsuarioID"];
                        aux.CursoID = (int)datos.Lector["CursoID"];
                        aux.FechaInscripcion = (DateTime)datos.Lector["FechaInscripcion"];
                        aux.FechaExpiracion = (DateTime)datos.Lector["FechaExpiracion"];
                        aux.Estado = (string)datos.Lector["Estado"];
                        aux.Curso = new Curso();
                        aux.Curso.Id = (int)datos.Lector["CursoID"];
                        aux.Curso.Titulo = (string)datos.Lector["Titulo"];
                        aux.Curso.UrlImagenPortada = (string)datos.Lector["UrlImagenPortada"];

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
    }
}

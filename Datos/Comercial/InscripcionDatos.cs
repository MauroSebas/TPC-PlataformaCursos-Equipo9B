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
        public int AltaInscripcion(Inscripcion nueva)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Inscripcion_Alta");

                datos.setearParametro("@IdUsuario", nueva.Usuario.UsuarioID);
                datos.setearParametro("@IdCurso", nueva.Curso.Id);

                datos.setearParametro("@FechaInscripcion", nueva.FechaInscripcion);

                if (nueva.FechaExpiracion.HasValue)
                    datos.setearParametro("@FechaExpiracion", nueva.FechaExpiracion.Value);
                else
                    datos.setearParametro("@FechaExpiracion", DBNull.Value);

                datos.setearParametro("@Estado", nueva.Estado);

                return datos.ejecutarAccionScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la inscripción.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }        
        public Inscripcion ObtenerActiva(int idUsuario, int idCurso)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConSP("sp_Inscripcion_ObtenerActiva");
                datos.setearParametro("@IdUsuario", idUsuario);
                datos.setearParametro("@IdCurso", idCurso);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {

                    Inscripcion aux = new Inscripcion();

                    aux.Id = (int)datos.Lector["InscripcionID"];
                    aux.Estado = (string)datos.Lector["Estado"];

                    aux.Usuario = new Usuario { UsuarioID = (int)datos.Lector["UsuarioID"] };
                    aux.Curso = new Curso { Id = (int)datos.Lector["CursoID"] };

                    if (!(datos.Lector["FechaInscripcion"] is DBNull))
                        aux.FechaInscripcion = (DateTime)datos.Lector["FechaInscripcion"];


                    if (!(datos.Lector["FechaExpiracion"] is DBNull))
                        aux.FechaExpiracion = (DateTime)datos.Lector["FechaExpiracion"];

                    return aux;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar inscripción activa.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<Inscripcion> ListarPorUsuario(int idUsuario)
        {
            List<Inscripcion> lista = new List<Inscripcion>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConSP("sp_Inscripcion_ListarPorUsuario");
                datos.setearParametro("@IdUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {

                    Inscripcion aux = new Inscripcion();

                    aux.Id = (int)datos.Lector["InscripcionID"];
                    aux.Estado = (string)datos.Lector["Estado"];

                    aux.Usuario = new Usuario { UsuarioID = (int)datos.Lector["UsuarioID"] };
                    aux.Curso = new Curso();


                    if (!(datos.Lector["CursoID"] is DBNull))
                        aux.Curso.Id = (int)datos.Lector["CursoID"];
                    if (!(datos.Lector["Titulo"] is DBNull))
                        aux.Curso.Titulo = (string)datos.Lector["Titulo"];
                    if (!(datos.Lector["UrlImagenPortada"] is DBNull))
                        aux.Curso.UrlImagenPortada = (string)datos.Lector["UrlImagenPortada"];

                    if (!(datos.Lector["FechaInscripcion"] is DBNull))
                        aux.FechaInscripcion = (DateTime)datos.Lector["FechaInscripcion"];

                    if (!(datos.Lector["FechaExpiracion"] is DBNull))
                        aux.FechaExpiracion = (DateTime)datos.Lector["FechaExpiracion"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar inscripciones del usuario.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void ActualizarEstado(int idInscripcion, string nuevoEstado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                
                datos.setearConSP("sp_Inscripcion_ActualizarEstado");
                datos.setearParametro("@InscripcionID", idInscripcion);
                datos.setearParametro("@Estado", nuevoEstado);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar estado de inscripción.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Pago> ListarAdmin(string estado = null)
        {
            List<Pago> lista = new List<Pago>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Pago_ListarAdmin");

                // Si viene null o vacío, mandamos DBNull para que el SP traiga todo
                if (string.IsNullOrEmpty(estado))
                    datos.setearParametro("@Estado", DBNull.Value);
                else
                    datos.setearParametro("@Estado", estado);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    // Usamos el método privado que ya tenés para no repetir código de mapeo
                    // Si no lo tenés privado, copialo de ListarPagosPendientes
                    lista.Add(MapearPago(datos));
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar pagos para admin.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}


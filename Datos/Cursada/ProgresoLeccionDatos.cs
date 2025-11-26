using Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ProgresoLeccionDatos
    {
       
        public void MarcarProgreso(int idInscripcion, int idLeccion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Progreso_Marcar");
                datos.setearParametro("@InscripcionID", idInscripcion);
                datos.setearParametro("@LeccionID", idLeccion);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al marcar el progreso.", ex);
            }
            finally { datos.cerrarConexion(); }
        }
        public void EliminarProgreso(int idInscripcion, int idLeccion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Progreso_Eliminar");
                datos.setearParametro("@InscripcionID", idInscripcion);
                datos.setearParametro("@LeccionID", idLeccion);
                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw new Exception("Error al desmarcar.", ex); }
            finally { datos.cerrarConexion(); }
        }

        public List<ProgresoLeccion> ListarProgreso(int idInscripcion)
        {
            List<ProgresoLeccion> lista = new List<ProgresoLeccion>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Progreso_Listar");
                datos.setearParametro("@InscripcionID", idInscripcion);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ProgresoLeccion aux = new ProgresoLeccion();
                    aux.Id = (int)datos.Lector["ProgresoID"];
                    aux.IdInscripcion = (int)datos.Lector["InscripcionID"];
                    aux.IdLeccion = (int)datos.Lector["LeccionID"];

                    if (!(datos.Lector["FechaCompletado"] is DBNull))
                        aux.FechaCompletado = (DateTime)datos.Lector["FechaCompletado"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar el progreso.", ex);
            }
            finally { datos.cerrarConexion(); }
        }
    }
}

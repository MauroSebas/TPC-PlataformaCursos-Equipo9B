using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Datos
{
    public class ArchivoLeccionDatos
    {
        
        public List<ArchivoLeccion> ListarArchivosPorLeccion( int IdLeccion)
        {
            AccesoDatos datos = new AccesoDatos();
            List<ArchivoLeccion> listaArchivoLeccion = new List<ArchivoLeccion>();

            try
            {

                datos.setearConSP("sp_ArchivoLeccion_ListarPorLeccion");
                datos.setearParametro("@IdLeccion", IdLeccion);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ArchivoLeccion aux = new ArchivoLeccion();
                    aux.Id = (int)datos.Lector["ArchivoLeccionID"];
                    aux.Nombre = (string)datos.Lector["NombreArchivo"];
                    aux.UrlArchivo = (string)datos.Lector["UrlArchivo"];
                    aux.TipoArchivo = (string)datos.Lector["TipoArchivo"];
                    aux.Leccion = new Leccion();
                    aux.Leccion.Id = IdLeccion;


                    listaArchivoLeccion.Add(aux);
                }

                return listaArchivoLeccion;

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
        
        public int AltaArchivoLeccion(ArchivoLeccion nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConSP("sp_AltaArchivoLeccion");
                datos.setearParametro("@LeccionID", nuevo.Leccion.Id);
                datos.setearParametro("@NombreArchivo", nuevo.Nombre);
                datos.setearParametro("@UrlArchivo", nuevo.UrlArchivo);
                datos.setearParametro("TipoArchivo", nuevo.TipoArchivo);

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
        



    }
}

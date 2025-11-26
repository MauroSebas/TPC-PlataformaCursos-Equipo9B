using Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Contenido
{
    public class LeccionDatos
    {
        public List<Leccion> Listar(int idModulo)
        {
            List<Leccion> lista = new List<Leccion>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Leccion_ListarPorModulo");
                datos.setearParametro("@ModuloID", idModulo);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(MapearLeccion(datos));
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public Leccion Obtener(int idLeccion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Leccion_Obtener");
                datos.setearParametro("@LeccionID", idLeccion);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return MapearLeccion(datos);
                }
                return null;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public void Agregar(Leccion nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Leccion_Alta");
                CargarParametros(datos, nuevo);
                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public void Modificar(Leccion leccion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Leccion_Modificar");
                datos.setearParametro("@LeccionID", leccion.Id);
                CargarParametros(datos, leccion);
                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public void Eliminar(int idLeccion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Leccion_Eliminar");
                datos.setearParametro("@LeccionID", idLeccion);
                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public void Reordenar(int idModulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Leccion_Reordenar");
                datos.setearParametro("@ModuloID", idModulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public void ActualizarOrden(int idLeccion, int nuevoOrden)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Leccion_ActualizarOrden");
                datos.setearParametro("@LeccionID", idLeccion);
                datos.setearParametro("@Orden", nuevoOrden);
                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        // --- MÉTODOS PRIVADOS PARA NO REPETIR CÓDIGO ---

        private Leccion MapearLeccion(AccesoDatos datos)
        {
            Leccion aux = new Leccion();
            aux.Id = (int)datos.Lector["LeccionID"];
            aux.IdModulo = (int)datos.Lector["ModuloID"];
            aux.Titulo = (string)datos.Lector["Titulo"];
            aux.Orden = (int)datos.Lector["Orden"];
            aux.TipoMaterial = (string)datos.Lector["TipoMaterial"];

           
            if (!(datos.Lector["URLRecurso"] is DBNull))
                aux.UrlRecurso = (string)datos.Lector["URLRecurso"];

            if (!(datos.Lector["UrlDocumento"] is DBNull))
                aux.UrlDocumento = (string)datos.Lector["UrlDocumento"];

           
            if (!(datos.Lector["Descripcion"] is DBNull))
                aux.Descripcion = (string)datos.Lector["Descripcion"];

            aux.DuracionMinutos = (int)datos.Lector["DuracionMinutos"];
            aux.Estado = (bool)datos.Lector["EstaActivo"];

            return aux;
        }

        private void CargarParametros(AccesoDatos datos, Leccion leccion)
        {
            datos.setearParametro("@ModuloID", leccion.IdModulo); 
            datos.setearParametro("@Titulo", leccion.Titulo);
            datos.setearParametro("@Orden", leccion.Orden);
            datos.setearParametro("@TipoMaterial", leccion.TipoMaterial);

            datos.setearParametro("@URLRecurso", string.IsNullOrEmpty(leccion.UrlRecurso) ? (object)DBNull.Value : leccion.UrlRecurso);
            datos.setearParametro("@UrlDocumento", string.IsNullOrEmpty(leccion.UrlDocumento) ? (object)DBNull.Value : leccion.UrlDocumento);
            datos.setearParametro("@Descripcion", string.IsNullOrEmpty(leccion.Descripcion) ? (object)DBNull.Value : leccion.Descripcion);

            datos.setearParametro("@DuracionMinutos", leccion.DuracionMinutos);
        }
    }
}

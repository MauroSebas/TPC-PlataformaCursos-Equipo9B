using Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Contenido
{
    public class ModuloDatos
    {
        public List<Modulo> Listar(int idCurso)
        {
            List<Modulo> lista = new List<Modulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Modulo_ListarPorCurso");
                datos.setearParametro("@CursoID", idCurso);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Modulo aux = new Modulo();
                    aux.Id = (int)datos.Lector["ModuloID"];
                    aux.IdCurso = (int)datos.Lector["CursoID"];
                    aux.Nombre = (string)datos.Lector["NombreModulo"];
                    aux.Orden = (int)datos.Lector["Orden"];
                    aux.EstaActivo = (bool)datos.Lector["EstaActivo"];
                    aux.CantidadLecciones = (int)datos.Lector["CantidadLecciones"];

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
        public Modulo Obtener(int idModulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Modulo_Obtener");
                datos.setearParametro("@ModuloID", idModulo);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Modulo aux = new Modulo();
                    aux.Id = (int)datos.Lector["ModuloID"];
                    aux.IdCurso = (int)datos.Lector["CursoID"];
                    aux.Nombre = (string)datos.Lector["NombreModulo"];
                    aux.Orden = (int)datos.Lector["Orden"];
                    aux.EstaActivo = (bool)datos.Lector["EstaActivo"];
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
        public void Agregar(Modulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Modulo_Alta");
                datos.setearParametro("@CursoID", nuevo.IdCurso);
                datos.setearParametro("@NombreModulo", nuevo.Nombre);
                datos.setearParametro("@Orden", nuevo.Orden);
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
        public void Modificar(Modulo modulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Modulo_Modificar");
                datos.setearParametro("@ModuloID", modulo.Id);
                datos.setearParametro("@NombreModulo", modulo.Nombre);
                datos.setearParametro("@Orden", modulo.Orden);
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
        public void ActualizarOrden(int idModulo, int nuevoOrden)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Modulo_ActualizarOrden");
                datos.setearParametro("@ModuloID", idModulo);
                datos.setearParametro("@Orden", nuevoOrden);
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
        public void Eliminar(int idModulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Modulo_Eliminar");
                datos.setearParametro("@ModuloID", idModulo);
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

        public void Reordenar(int idCurso)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Modulo_Reordenar");
                datos.setearParametro("@CursoID", idCurso);
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


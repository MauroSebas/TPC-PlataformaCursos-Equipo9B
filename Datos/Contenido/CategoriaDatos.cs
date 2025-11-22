using Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class CategoriaDatos
    {       
        public List<Categoria> listarCategoriaConSP()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Categoria_ListarActivas");
                datos.ejecutarLectura(); 

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)datos.Lector["CategoriaID"];
                    aux.Nombre = (string)datos.Lector["NombreCategoria"];
                    aux.EstaActivo = (bool)datos.Lector["EstaActivo"];
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
        public int agregarCategoriaConSP(Categoria nueva)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_AltaCategoria");
                datos.setearParametro("@nombre", nueva.Nombre);
                int idNuevo = datos.ejecutarAccionScalar();
                return idNuevo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }      
        public Categoria BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_Categoria_BuscarPorID"); 
                datos.setearParametro("@ID", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)datos.Lector["CategoriaID"];
                    aux.Nombre = (string)datos.Lector["NombreCategoria"];
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
        public void modificarConSP(Categoria nueva) 
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_ModificarCategoria");
                datos.setearParametro("@ID", nueva.Id);
                datos.setearParametro("@nombre", nueva.Nombre);

               
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }  
        public void eliminarLogicoConSP(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConSP("sp_EliminacionLogicaCategoria");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }     
    }
}


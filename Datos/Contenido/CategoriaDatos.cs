using Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class CategoriaDatos
    {
        public List<Categoria> Listar()
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
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public Categoria Obtener(int id)
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
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public int Agregar(Categoria nueva)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_AltaCategoria");
                datos.setearParametro("@nombre", nueva.Nombre);
                return datos.ejecutarAccionScalar();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public void Modificar(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                
                datos.setearConSP("sp_Categoria_Modificar");
                datos.setearParametro("@CategoriaID", categoria.Id);
                datos.setearParametro("@NombreCategoria", categoria.Nombre);
                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                
                datos.setearConSP("sp_Categoria_Eliminar");
                datos.setearParametro("@CategoriaID", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public int ContarCursosPorCategoria(int categoriaId)
        {
            AccesoDatos datos = new AccesoDatos();
            try           {
              
                datos.setearConsulta("SELECT COUNT(*) FROM Curso WHERE CategoriaID = @CatID AND EstaActivo = 1");
                datos.setearParametro("@CatID", categoriaId);

                return datos.ejecutarAccionScalar();
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


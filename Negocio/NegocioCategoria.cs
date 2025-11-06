using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Datos;

namespace Negocio
{
    public class NegocioCategoria
    {
        public List<Categoria> listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("SELECT CategoriaID,NombreCategoria, EstaActivo FROM Categoria;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {

                    Categoria aux = new Categoria();

                    aux.Id= (int)datos.Lector["CategoriaID"];
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

        public List<Categoria> listarConSP()
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

        public void agregarConSP(Categoria nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConSP("sp_AltaCategoria");
                datos.setearParametro("@nombre", nuevo.Nombre);
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
        public Categoria BuscarPorId(int id)
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            Categoria seleccionada = new Categoria();

            try
            {
                lista = listarConSP();

                foreach (Categoria cat in lista)
                {
                    if ( cat.Id == id)
                    {
                        seleccionada = cat;
                    }
                }

                return seleccionada;
            }
            catch (Exception ex)
            {

                throw ex;
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
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminarLogico(int id)
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


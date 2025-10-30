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

                    aux.CategoriaID = (int)datos.Lector["CategoriaID"];
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
                datos.setearConSP("sp_Listar");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {

                    Categoria aux = new Categoria();

                    aux.CategoriaID = (int)datos.Lector["CategoriaID"];
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

    }
}


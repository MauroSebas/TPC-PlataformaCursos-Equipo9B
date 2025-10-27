using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class CategoriaCursoNegocio
    {

        public List<CategoriaCurso> listar()
        {
            List<CategoriaCurso> lista = new List<CategoriaCurso>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //seteo la consulta
                datos.setearConsulta("SELECT IDCategoria,NombreCategoria FROM CategoriaCurso;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    CategoriaCurso aux = new CategoriaCurso();
                    aux.Id = (int)datos.Lector["IDCategoria"];
                    aux.Nombre = (string)datos.Lector["NombreCategoria"];

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

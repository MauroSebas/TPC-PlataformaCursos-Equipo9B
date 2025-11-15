using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Datos;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listarCategoria()
        {
            CategoriaDatos datos = new CategoriaDatos();
            List<Categoria> lista = new List<Categoria>();
            lista = datos.listarCategoriaConSP();
            
            //Validaciones
            return lista;
        }

        public int agregarCategoria(Categoria nueva)
        {
            CategoriaDatos datos = new CategoriaDatos();


            int idNuevo = datos.agregarCategoriaConSP(nueva);
            
            //Validar
            return idNuevo;

        }
        public Categoria BuscarPorId(int id)
        {
            CategoriaDatos datos = new CategoriaDatos();
            Categoria seleccionada = new Categoria();
            seleccionada = datos.BuscarPorId(id);

            //Validaciones
            return seleccionada;

        }

        public int modificarCategoria(Categoria categoria)
        {
            CategoriaDatos datos = new CategoriaDatos();
            int idModificada = datos.modificarConSP(categoria);

            // Mejorar validacion
            if ( idModificada == categoria.Id)
            {
                return idModificada;
            }
            else
            {
                return -1;
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


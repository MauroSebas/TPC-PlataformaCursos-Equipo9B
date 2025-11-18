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
        
        private readonly CategoriaDatos datos = new CategoriaDatos();
        public List<Categoria> listarCategoria()
        {
            try
            {                
                return datos.listarCategoriaConSP();
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error al listar las categorías.", ex);
            }
        }
        public int agregarCategoria(Categoria nueva)
        {
            try
            {                
                if (nueva == null)
                    throw new Exception("La categoría es nula.");
                if (string.IsNullOrWhiteSpace(nueva.Nombre))
                    throw new Exception("El nombre de la categoría es obligatorio.");
                if (nueva.Nombre.Length > 40) 
                    throw new Exception("El nombre no puede superar los 40 caracteres.");
                
                int idNuevo = datos.agregarCategoriaConSP(nueva);
                return idNuevo;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error al agregar la categoría: " + ex.Message, ex);
            }
        }
        public Categoria BuscarPorId(int id)
        {
            try
            {                
                return datos.BuscarPorId(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar la categoría.", ex);
            }
        }     
        public void modificarCategoria(Categoria categoria)
        {
            try
            {
               
                if (categoria == null)
                    throw new Exception("La categoría es nula.");
                if (categoria.Id <= 0)
                    throw new Exception("El ID de la categoría no es válido.");
                if (string.IsNullOrWhiteSpace(categoria.Nombre))
                    throw new Exception("El nombre de la categoría es obligatorio.");
                if (categoria.Nombre.Length > 40) 
                    throw new Exception("El nombre no puede superar los 40 caracteres.");
                 
                datos.modificarConSP(categoria);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la categoría: " + ex.Message, ex);
            }
        }       
        public void eliminarLogico(int id)
        {
            try
            {
                
                CursoNegocio cursoNegocio = new CursoNegocio();
                if (cursoNegocio.ContarCursosPorCategoria(id) > 0)
                {
                    throw new Exception("No se puede eliminar una categoría con cursos asociados.");
                }
                
                datos.eliminarLogicoConSP(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la categoría.", ex);
            }
        }
    }
}


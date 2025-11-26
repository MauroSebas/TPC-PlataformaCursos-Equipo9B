using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Datos;

namespace Negocio
{
    public class CategoriaNegocio
    {

        private readonly CategoriaDatos datos = new CategoriaDatos();

        public List<Categoria> Listar()
        {
            try { return datos.Listar(); }
            catch (Exception ex) { throw new Exception("Error al listar categorías.", ex); }
        }

        public Categoria Obtener(int id)
        {
            try { return datos.Obtener(id); }
            catch (Exception ex) { throw new Exception("Error al buscar categoría.", ex); }
        }

        public int Guardar(Categoria categoria)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(categoria.Nombre))
                    throw new Exception("El nombre es obligatorio.");
                if (categoria.Nombre.Length < 3)
                    throw new Exception("Nombre demasiado corto(Minimo 3 Caracteres).");
                if (categoria.Nombre.Length > 50)
                    throw new Exception("Nombre demasiado largo.");

                if (categoria.Id > 0)
                {
                    datos.Modificar(categoria);
                    return categoria.Id;
                }
                else
                {
                    return datos.Agregar(categoria);
                }
            }
            catch (Exception ex) { throw new Exception("Error al guardar categoría: " + ex.Message, ex); }
        }

        public void Eliminar(int id)
        {
            try
            {
               
                CursoNegocio cn = new CursoNegocio();

               
                if (cn.ContarCursosPorCategoria(id) > 0)
                    throw new Exception("No se puede eliminar: Esta categoría tiene cursos activos.");

              
                datos.Eliminar(id);
            }
            catch (Exception ex) { throw ex; }
        }

        public int ContarCursosPorCategoria(int categoriaId)
        {
            try
            {
                return datos.ContarCursosPorCategoria(categoriaId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al contar cursos por categoría.", ex);
            }
        }
    }
}


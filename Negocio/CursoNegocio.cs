using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CursoNegocio
    {

        private readonly CursoDatos datos = new CursoDatos();
        public List<Curso> listarCursos()
        {
            try
            {

                return datos.listarCursoConSP();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al listar los cursos desde la capa de negocio.", ex);
            }
        }
        public Curso BuscarCurso(int id)
        {
            try
            {

                return datos.BuscarCursoPorId(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar el curso.", ex);
            }
        }
        public int GuardarCurso(Curso curso)
        {
            try
            {

                if (curso == null)
                    throw new Exception("El objeto 'curso' es nulo.");

                // Título
                if (string.IsNullOrWhiteSpace(curso.Titulo))
                    throw new Exception("El título del curso es obligatorio.");
                if (curso.Titulo.Length > 255)
                    throw new Exception("El título no puede superar los 255 caracteres.");

                // Descripción (Opcional, pero con límite si existe)
                if (!string.IsNullOrEmpty(curso.Descripcion) && curso.Descripcion.Length > 4000)
                    // Usamos 4000 como un límite lógico para nvarchar(max)
                    throw new Exception("La descripción es demasiado larga (máx 4000 caracteres).");

                // Categoría
                if (curso.Categoria == null || curso.Categoria.Id <= 0)
                    throw new Exception("Debe seleccionar una categoría válida.");

                // Precio
                if (curso.Precio < 0)
                    throw new Exception("El precio no puede ser negativo.");

                // URL (Opcional, pero con límite si existe)
                if (!string.IsNullOrEmpty(curso.UrlImagenPortada) && curso.UrlImagenPortada.Length > 2000)
                    // Límite generoso para URLs
                    throw new Exception("La URL de la imagen es demasiado larga (máx 2000 caracteres).");

                // Modalidad de Pago (¡NUEVO!)
                if (string.IsNullOrWhiteSpace(curso.ModalidadPago))
                    throw new Exception("La modalidad de pago es obligatoria.");
                if (curso.ModalidadPago.Length > 50)
                    throw new Exception("La modalidad de pago no puede superar los 50 caracteres.");

                // Duración (¡NUEVO!)
                if (curso.DuracionAccesoDias <= 0)
                    throw new Exception("La duración de acceso debe ser de al menos 1 día.");



                if (curso.Id > 0)
                {

                    datos.modificarCursoConSP(curso);
                    return curso.Id;
                }
                else
                {

                    return datos.agregarCursoConSP(curso);
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al guardar el curso: " + ex.Message, ex);
            }
        }
        public void eliminarCursoLogico(int id)
        {
            try
            {
                // --- 4. ¡¡LA REGLA DE NEGOCIO MÁS IMPORTANTE!! ---
                // (Por ahora la dejamos comentada, porque falta InscripcionNegocio)

                // InscripcionNegocio inscripcionNegocio = new InscripcionNegocio();
                // if (inscripcionNegocio.CursoTieneInscripciones(id))
                // {
                //    throw new Exception("No se puede eliminar un curso que ya tiene alumnos inscriptos. Desactívelo en su lugar.");
                // }

                // Si pasa la validación, le da la orden al mecánico
                datos.eliminarCursoSP(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el curso.", ex);
            }
        }
        public int ContarCursosPorCategoria(int categoriaId)
        {
            try
            {
                return datos.ContarCursosPorCategoria(categoriaId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al contar cursos en negocio.", ex);
            }
        }
        public List<Curso> filtrarCursos(string titulo, int categoriaId)
        {
            try
            {                
                return datos.filtrarCursosConSP(titulo, categoriaId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al filtrar los cursos.", ex);
            }
        }

        public void CambiarEstadoPublicado(int cursoId, bool publico)
        {
            try
            {                
                if (cursoId <= 0)
                    throw new Exception("ID de curso no válido");

                datos.CambiarPublicadoSP(cursoId, publico);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar estado de publicación en Negocio.", ex);
            }
        }
    }


}


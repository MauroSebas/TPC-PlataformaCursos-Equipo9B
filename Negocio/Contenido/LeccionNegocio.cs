using Datos.Contenido;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Contenido
{
    public class LeccionNegocio
    {
        private LeccionDatos datos = new LeccionDatos();

        public List<Leccion> Listar(int idModulo)
        {
            try
            {
                if (idModulo <= 0) return new List<Leccion>();
                return datos.Listar(idModulo);
            }
            catch (Exception ex) { throw new Exception("Error al listar lecciones.", ex); }
        }

        public Leccion Obtener(int idLeccion)
        {
            try
            {
                if (idLeccion <= 0) throw new Exception("ID de lección inválido.");
                return datos.Obtener(idLeccion);
            }
            catch (Exception ex) { throw new Exception("Error al obtener la lección.", ex); }
        }

        public void Guardar(Leccion leccion)
        {
            try
            {
               
                if (leccion.Titulo != null) leccion.Titulo = leccion.Titulo.Trim();
                if (leccion.UrlRecurso != null) leccion.UrlRecurso = leccion.UrlRecurso.Trim();
                if (leccion.Descripcion != null) leccion.Descripcion = leccion.Descripcion.Trim();

                
                if (string.IsNullOrWhiteSpace(leccion.Titulo))
                    throw new Exception("El título es obligatorio.");
                if (leccion.Titulo.Length < 5)
                    throw new Exception("El título es muy corto (mínimo 5 letras).");
                if (leccion.Titulo.Length > 50)
                    throw new Exception("El título es muy largo (máximo 50 letras).");

               
                if (!string.IsNullOrWhiteSpace(leccion.Descripcion))
                {
                    if (leccion.Descripcion.Length < 10)
                        throw new Exception("La descripción es muy corta (mínimo 10 letras).");
                    if (leccion.Descripcion.Length > 200)
                        throw new Exception("La descripción es muy larga (máximo 200 letras).");
                }

                if (leccion.IdModulo <= 0) throw new Exception("Error crítico: Sin módulo.");
                if (leccion.DuracionMinutos < 0) throw new Exception("Duración negativa no permitida.");

                if (leccion.Orden <= 0) leccion.Orden = 99; 

                
                if (string.IsNullOrWhiteSpace(leccion.TipoMaterial))
                    throw new Exception("Debe seleccionar un tipo de material.");

                switch (leccion.TipoMaterial)
                {
                    case "Video":
                        if (string.IsNullOrWhiteSpace(leccion.UrlRecurso))
                            throw new Exception("Para una lección de Video, debés ingresar la URL (YouTube/Vimeo).");
                        break;

                    case "Archivo":
                       
                        if (string.IsNullOrWhiteSpace(leccion.UrlDocumento) && string.IsNullOrWhiteSpace(leccion.UrlRecurso))
                            throw new Exception("Para una lección de Archivo, debés subir un documento o poner un link de descarga.");
                        break;

                    case "Enlace":
                        if (string.IsNullOrWhiteSpace(leccion.UrlRecurso))
                            throw new Exception("Debés ingresar la URL del enlace externo.");
                        break;
                }

               
                if (leccion.Id > 0)
                {
                    datos.Modificar(leccion);
                }
                else
                {
                    datos.Agregar(leccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la lección: " + ex.Message, ex);
            }
        }

        public void Eliminar(int idLeccion)
        {
            try
            {
                if (idLeccion <= 0) throw new Exception("ID inválido.");
                datos.Eliminar(idLeccion);
            }
            catch (Exception ex) { throw new Exception("Error al eliminar lección.", ex); }
        }

        public void Reordenar(int idModulo)
        {
            try
            {
                if (idModulo <= 0) return;
                datos.Reordenar(idModulo);
            }
            catch (Exception ex) { throw new Exception("Error al reordenar lecciones.", ex); }
        }

        public void ActualizarOrden(int idLeccion, int nuevoOrden)
        {
            try
            {
                if (idLeccion <= 0 || nuevoOrden <= 0) return;
                datos.ActualizarOrden(idLeccion, nuevoOrden);
            }
            catch (Exception ex) { throw new Exception("Error al cambiar orden.", ex); }
        }
    }
}

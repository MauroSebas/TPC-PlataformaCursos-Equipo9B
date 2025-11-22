using Datos.Contenido;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Contenido
{
    public class ModuloNegocio
    {
        private ModuloDatos datos = new ModuloDatos();
        public List<Modulo> Listar(int idCurso)
        {
            try
            {
                // Validación básica: No intentar ir a la DB si el ID es inválido
                if (idCurso <= 0) return new List<Modulo>();

                return datos.Listar(idCurso);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al recuperar la lista de módulos.", ex);
            }
        }
        public Modulo Obtener(int idModulo)
        {
            try
            {
                if (idModulo <= 0) throw new Exception("El ID del módulo no es válido.");

                return datos.Obtener(idModulo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los detalles del módulo.", ex);
            }
        }
        public void Guardar(Modulo modulo)
        {
            try
            {               
                if (modulo.Nombre != null) modulo.Nombre = modulo.Nombre.Trim();
               
                if (string.IsNullOrWhiteSpace(modulo.Nombre))
                    throw new Exception("El nombre del módulo es obligatorio.");

                if (modulo.Nombre.Length > 50) 
                    throw new Exception("El nombre es demasiado largo (máximo 50 caracteres).");

                if (modulo.Nombre.Length < 3)
                    throw new Exception("El nombre es muy corto, debe tener al menos 3 letras.");

                if (modulo.IdCurso <= 0)
                    throw new Exception("Error crítico: El módulo no está vinculado a un curso válido.");

                if (modulo.Orden <= 0)
                    modulo.Orden = 99;

               
                if (modulo.Id > 0)
                {
                    datos.Modificar(modulo);
                }
                else
                {
                    datos.Agregar(modulo);
                }
            }
            catch (Exception ex)
            {                
                throw new Exception("Ocurrió un error al intentar guardar el módulo: " + ex.Message, ex);
            }
        }
        public void ActualizarOrden(int idModulo, int nuevoOrden)
        {
            try
            {
                if (idModulo <= 0) throw new Exception("ID de módulo inválido.");
                if (nuevoOrden <= 0) throw new Exception("El orden no puede ser negativo o cero.");

                datos.ActualizarOrden(idModulo, nuevoOrden);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al reordenar los módulos.", ex);
            }
        }
        public void Eliminar(int idModulo)
        {
            try
            {
                if (idModulo <= 0) throw new Exception("No se seleccionó un módulo válido para eliminar.");

                datos.Eliminar(idModulo);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo eliminar el módulo. Intente nuevamente.", ex);
            }
        }


        public void Reordenar(int idCurso)
        {
            try
            {
                datos.Reordenar(idCurso);
            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrió un error al intentar eliminar el Modulo: " + ex.Message, ex);
            }
        }
    }
}


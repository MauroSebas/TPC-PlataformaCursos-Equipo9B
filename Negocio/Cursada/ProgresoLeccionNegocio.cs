using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Cursada
{
    public class ProgresoLeccionNegocio
    {
        private ProgresoLeccionDatos datos = new ProgresoLeccionDatos();

       
        public void MarcarCompleta(int idInscripcion, int idLeccion)
        {
            try
            {
                if (idInscripcion <= 0) throw new Exception("ID de inscripción inválido.");
                if (idLeccion <= 0) throw new Exception("ID de lección inválido.");

                // Podríamos validar acá si la inscripción está activa, 
                // pero asumimos que si llegó al Aula es porque ya validó el acceso.

                datos.MarcarProgreso(idInscripcion, idLeccion);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al marcar la lección como completada.", ex);
            }
        }

        // 2. LISTAR PROGRESO (Para pintar el sidebar)
        public List<ProgresoLeccion> ListarProgreso(int idInscripcion)
        {
            try
            {
                if (idInscripcion <= 0) return new List<ProgresoLeccion>();

                return datos.ListarProgreso(idInscripcion);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el progreso del curso.", ex);
            }
        }
        public void EliminarProgreso(int idInscripcion, int idLeccion)
        {
            datos.EliminarProgreso(idInscripcion, idLeccion);
        }
        // 3. HELPER: VERIFICAR SI UNA LECCIÓN ESPECÍFICA ESTÁ COMPLETA
        // (Útil para deshabilitar botones o pintar cosas puntuales sin traer toda la lista)
        public bool EstaCompleta(int idInscripcion, int idLeccion)
        {
            // Traemos la lista y buscamos en memoria (más rápido que ir a la DB por cada lección si ya tenemos la lista en sesión)
            // Pero acá hacemos la versión directa a la lista para ser seguros.
            List<ProgresoLeccion> lista = ListarProgreso(idInscripcion);

            // Buscamos si existe el registro para esa lección
            return lista.Exists(x => x.IdLeccion == idLeccion);
        }
    }
}

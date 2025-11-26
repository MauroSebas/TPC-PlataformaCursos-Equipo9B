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

               

                datos.MarcarProgreso(idInscripcion, idLeccion);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al marcar la lección como completada.", ex);
            }
        }

       
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
        
        public bool EstaCompleta(int idInscripcion, int idLeccion)
        {
            
            List<ProgresoLeccion> lista = ListarProgreso(idInscripcion);

            
            return lista.Exists(x => x.IdLeccion == idLeccion);
        }
    }
}

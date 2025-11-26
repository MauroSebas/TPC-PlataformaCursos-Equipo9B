using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ProgresoLeccion
    {
        public int Id { get; set; }

       
        public int IdInscripcion { get; set; }
        public int IdLeccion { get; set; }

       
        public Inscripcion Inscripcion { get; set; }
        public Leccion Leccion { get; set; }

        
        public DateTime? FechaCompletado { get; set; }

        // Propiedad  para el Frontend (devuelve true si tiene fecha)
        public bool EstaCompletada
        {
            get { return FechaCompletado.HasValue; }
        }

        public ProgresoLeccion() { }
    }
}

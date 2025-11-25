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

        // IDs sueltos para facilitar el guardado
        public int IdInscripcion { get; set; }
        public int IdLeccion { get; set; }

        // Objetos para navegación (si los necesitamos traer completos)
        // public Inscripcion Inscripcion { get; set; }
        // public Leccion Leccion { get; set; }

        // El dato real de la DB
        public DateTime? FechaCompletado { get; set; }

        // Propiedad "Helper" para el Frontend (devuelve true si tiene fecha)
        public bool EstaCompletada
        {
            get { return FechaCompletado.HasValue; }
        }

        public ProgresoLeccion() { }
    }
}

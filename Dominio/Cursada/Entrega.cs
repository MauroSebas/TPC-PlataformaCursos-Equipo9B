using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Cursada
{
    public class Entrega
    {
        public int Id { get; set; }

        // Relaciones
        public Inscripcion Inscripcion { get; set; }
        public Examen Examen { get; set; }

        // Datos de la entrega
        public string UrlResolucion { get; set; } // Link al Drive/Github
        public DateTime FechaEntrega { get; set; }
        public string Estado { get; set; } // "Pendiente", "Aprobado", "Rechazado"
        public string DevolucionProfesor { get; set; } // Feedback de texto
    }
}

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
        public int InscripcionId { get; set; }
        public int ExamenId { get; set; }
        public string UrlResolucion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Estado { get; set; }
        public string DevolucionProfesor { get; set; }

       
        public string NombreAlumno { get; set; }
        public string EmailAlumno { get; set; }
        public string TituloCurso { get; set; }
    }
}
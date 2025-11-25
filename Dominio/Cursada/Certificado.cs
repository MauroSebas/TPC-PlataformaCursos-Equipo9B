using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{


    public class Certificado
    {
        public int Id { get; set; }
        public int InscripcionId { get; set; } 
        public string UrlArchivo { get; set; } 
        public DateTime FechaEmision { get; set; }

      
        public string NombreCurso { get; set; }
        public string UrlImagenCurso { get; set; }
    }


}

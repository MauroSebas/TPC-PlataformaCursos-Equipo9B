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
        public Inscripcion Inscripcion { get; set; }
        public DateTime FechaEmision { get; set; }
        public string UrlArchivoCertificado { get; set; }

    }
}

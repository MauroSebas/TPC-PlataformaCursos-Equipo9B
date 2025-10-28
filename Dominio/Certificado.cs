using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Certificado
    {
        public int CertificadoID { get; set; }
        public int InscripcionID { get; set; }
        public DateTime FechaEmision { get; set; }
        public string CodigoValidacion { get; set; }

    }
}

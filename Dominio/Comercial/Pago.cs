using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{

    public class Pago
    {
        public int Id { get; set; }

        public Inscripcion Inscripcion;
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; }
        public string Estado { get; set; }
        public string UrlComprobante { get; set; } 
        public DateTime? FechaPago { get; set; } 
        public string Observaciones { get; set; } 

        public string MontoFormateado => Monto.ToString("C", new CultureInfo("es-AR"));
    }
}

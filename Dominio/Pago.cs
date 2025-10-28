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
        public int PagoID { get; set; }
        public int InscripcionID { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; }
        public string Estado { get; set; }
        
        public string MontoFormateado
        {
            get
            {
                return Monto.ToString("C", new CultureInfo("es-AR"));
            }
        }
    }
}

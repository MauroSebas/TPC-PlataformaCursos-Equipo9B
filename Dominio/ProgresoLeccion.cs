using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ProgresoLeccion
    {
        public int ProgresoID { get; set; }
        public int InscripcionID { get; set; }
        public int LeccionID { get; set; }
        public bool Completada { get; set; }
    }
}

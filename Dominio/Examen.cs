using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{ 

    public class Examen
    {
        public int ExamenID { get; set; }
        public int LeccionID { get; set; }
        public int LimiteIntentos { get; set; }
        public int LimiteTiempoMinutos { get; set; }
        public bool HabilitacionCondicional { get; set; }
        public bool EstaActivo { get; set; }

    }

}

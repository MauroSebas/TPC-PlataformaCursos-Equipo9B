using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Dominio
{
    public class OpcionRespuesta
    {
        public int OpcionID { get; set; }
        public int PreguntaID { get; set; }
        public string TextoOpcion { get; set; }
        public bool EsCorrecta { get; set; }
        public bool EstaActivo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Dominio
{

    public class Pregunta
    {
        public int PreguntaID { get; set; }
        public int ExamenID { get; set; }
        public string Enunciado { get; set; }
        public string UrlImagen { get; set; }
        public bool EstaActivo { get; set; }

    }
}

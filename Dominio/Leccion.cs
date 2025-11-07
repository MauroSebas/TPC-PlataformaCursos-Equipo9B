using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Leccion
    {
        public int Id { get; set; }
        public Modulo Modulo { get; set; }
        public string Titulo { get; set; }
        public string TipoMaterial { get; set; }
        public string UrlRecurso { get; set; }
        public bool EstaActivo { get; set; }

    }
}

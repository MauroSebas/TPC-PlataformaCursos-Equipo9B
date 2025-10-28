using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Modulo
    {
        public int ModuloID { get; set; }
        public int CursoID { get; set; }
        public string NombreModulo { get; set; }
        public int Orden { get; set; }
        public bool EstaActivo { get; set; }
    }
}

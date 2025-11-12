using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ArchivoLeccion
    {
        public int Id { get; set;}
        public Leccion Leccion { get; set;}
        public string Nombre { get; set; }
        public string UrlArchivo { get; set; }
        public string TipoArchivo { get; set; }

    }
}

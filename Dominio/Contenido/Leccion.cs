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
        public int IdModulo { get; set; }       
        public Modulo Modulo { get; set; }
        public string Titulo { get; set; }
        public int Orden { get; set; }        
        public string TipoMaterial { get; set; }
        public string UrlRecurso { get; set; }   
        public string UrlDocumento { get; set; }
        public string Descripcion { get; set; }
        public int DuracionMinutos { get; set; }
        public bool Estado { get; set; }        
        public Leccion() { }
    }

}


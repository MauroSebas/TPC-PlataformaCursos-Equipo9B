using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{ 

    public class Examen
    {
        public int Id { get; set; }
        
        public int CursoId { get; set; }

        
        public Curso Curso { get; set; }
        public string UrlConsigna { get; set; } 
        public bool EstaActivo { get; set; }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class CursoObjetivo
    {
        public int Id { get; set; }
        public Curso Curso {get;set;}
        public string Descripcion { get; set; }

    }
}

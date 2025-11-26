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
        public int Id { get; set; }        
        public int IdCurso { get; set; }        
        public Curso Curso { get; set; }

        public string Nombre { get; set; }
        public int Orden { get; set; }
        public bool EstaActivo { get; set; }
        public int CantidadLecciones { get; set; }
        public List<Leccion> Lecciones { get; set; }        
        public Modulo()
        {
            Lecciones = new List<Leccion>();
        }
    }
}


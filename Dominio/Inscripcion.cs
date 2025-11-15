using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Inscripcion
    {
        public int InscripcionID { get; set; }
        public int UsuarioID { get; set; }
        public int CursoID { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public DateTime FechaExpiracion { get; set; }
        

    }
}

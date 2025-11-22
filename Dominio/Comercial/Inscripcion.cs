using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Inscripcion
    {
        public int Id { get; set; }
        public int UsuarioID { get; set; }// Mas facil para guardar
        public Usuario Usuario { get; set; }//Mas facil para la lectura de atributos
        public int CursoID { get; set; }
        public Curso Curso { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public DateTime? FechaExpiracion { get; set; } 
        public string Estado { get; set; } // "Activo", "Pendiente"


    }
}

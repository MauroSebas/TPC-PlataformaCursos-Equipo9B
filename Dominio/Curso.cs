using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Curso
    {
        public int CursoID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public float Precio { get; set; }
        public string UrlImagenPortada { get; set; }
        public string ModalidadPago { get; set; }
        public int DuracionAccesoDias { get; set; }
        public bool Publicado { get; set; }
        public int CategoriaID { get; set; }
        public string NivelDificultad { get; set; }
        public string Idioma { get; set; }
        public bool EstaActivo { get; set; }
        public string PrecioFormateado
        {
            get
            {
                return Precio.ToString("C", new CultureInfo("es-AR"));
            }
        }



    }
}

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
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string UrlImagenPortada { get; set; }
        public  List<Modulo> Modulos { get; set; }
        public string ModalidadPago { get; set; }
        public int DuracionAccesoDias { get; set; }
        public bool Publicado { get; set; }
        public Categoria Categoria { get; set; }
        public decimal Precio { get; set; }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Seguridad
{
    public class UsuarioToken
    {

        public int TokenID { get; set; }
        public int UsuarioID { get; set; }
        public string Token { get; set; }
        public int TipoToken { get; set; }
        public DateTime FechaVencimiento { get; set; }

    }
}

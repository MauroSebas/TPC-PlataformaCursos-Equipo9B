using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string Email { get; set; }
        public bool EstaActivo { get; set; }
        private string PaswordHash
        {
            get { return PaswordHash; }

            set
            {
                if (EstaActivo == false)
                {
                    PaswordHash = null;
                }
                else
                {
                    PaswordHash = value;
                }
            }
        }
        
        public string Rol { get; set; }
        public string EstadoCuenta { get; set; }

    }
}

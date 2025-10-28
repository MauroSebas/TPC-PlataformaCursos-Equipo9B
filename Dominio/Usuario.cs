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
        public string PasswordHash { get; set; } 
        public string Rol { get; set; } 
        public string EstadoCuenta { get; set; } 
        public bool EstaActivo { get; set; } 
        public string UrlFotoPerfil { get; set; } 

        public Usuario() { }
        public bool EsAdmin()
        {
            return this.Rol == "Admin";
        }
    }
}

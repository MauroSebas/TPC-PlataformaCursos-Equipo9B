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
        public bool EstaActivo { get; set; } // Baja lógica
        public string UrlFotoPerfil { get; set; } 

        
        public int RolID { get; set; }
        public int EstadoCuentaID { get; set; }

        // --- Propiedades de Navegación (Opcionales) ---
        public Rol Rol { get; set; } //ADMIN - PARTICIPANTE
        public EstadoCuenta EstadoCuenta { get; set; } //Pendiente - Activo

        public Usuario() { }

    }
}

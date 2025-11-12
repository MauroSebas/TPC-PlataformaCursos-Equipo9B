using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PerfilNegocio
    {
        private readonly PerfilDatos perfilDatos = new PerfilDatos();

        public Perfil ObtenerPerfilPorUsuarioID(int usuarioID)
        {
            if (usuarioID <= 0)
                throw new ArgumentException("El ID de usuario no es válido.");

            return perfilDatos.ObtenerPerfilPorUsuarioID(usuarioID);
        }

        public void ActualizarPerfil(Perfil perfil)
        {
            if (perfil == null)
                throw new ArgumentNullException(nameof(perfil), "El perfil no puede ser nulo.");

            if (perfil.UsuarioID <= 0)
                throw new ArgumentException("El UsuarioID no es válido.");

            if (perfil.Nombre?.Length > 100)
                throw new ArgumentException("El nombre no puede superar los 100 caracteres.");

            if (perfil.Apellido?.Length > 100)
                throw new ArgumentException("El apellido no puede superar los 100 caracteres.");

            perfilDatos.ActualizarPerfil(perfil);
        }
    }
}

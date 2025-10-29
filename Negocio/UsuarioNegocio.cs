using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class UsuarioNegocio
    {
        
        public bool RegistrarUsuario(Usuario nuevoUsuario) 
        {
            UsuarioDatos datos = new UsuarioDatos();
            try
            {
                // 1. Validar si el email ya existe
                if (datos.BuscarPorEmail(nuevoUsuario.Email) != null)
                {
                    // El email ya está registrado, no se puede insertar
                    return false; 
                }

                // 2. Hashear la contraseña ( BCrypt.Net-Next)
                nuevoUsuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(nuevoUsuario.PasswordHash); // Sobrescribe la pass plana con el hash

                // 3. Establecer valores por defecto.
                //RolID 2 = Participante y EstadoCuentaID 2 = PendienteActivación para que confirme el correo.
                nuevoUsuario.RolID = 2; // ID de "Participante"
                nuevoUsuario.EstadoCuentaID = 2; // ID de  "PendienteActivacion"
                nuevoUsuario.EstaActivo = true; // Por defecto, activo

                // 4. Llamar a la capa de Datos para insertar
                int idGenerado = datos.InsertarNuevo(nuevoUsuario);

                // 5. Verificar si la inserción fue exitosa
                return idGenerado > 0; // Si devolvió un ID > 0, fue exitoso

            }
            catch (Exception ex)
            {
                // Loggear el error ex
                // Acá manejar mejor casos de expcepción.
                return false; 
            }
           
        }
    }
}

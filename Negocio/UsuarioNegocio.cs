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
        // Método para registrar un nuevo usuario
        public bool RegistrarUsuario(Usuario nuevoUsuario) // Devuelve true si fue exitoso, false si no
        {
            UsuarioDatos datos = new UsuarioDatos();
            try
            {
                // 1. Validar si el email ya existe
                if (datos.BuscarPorEmail(nuevoUsuario.Email) != null)
                {
                    // El email ya está registrado, no se puede insertar
                    return false; // Indicamos fallo por email duplicado
                }

                // 2. Hashear la contraseña (¡Necesitas instalar BCrypt.Net-Next!)
                // Asegúrate de tener: using BCrypt.Net;
                // Si no has instalado el paquete NuGet:
                // Click derecho en el proyecto Negocio > Administrar paquetes NuGet > Buscar "BCrypt.Net-Next" > Instalar
                nuevoUsuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(nuevoUsuario.PasswordHash); // Sobrescribimos la pass plana con el hash

                // 3. Establecer valores por defecto (IMPORTANTE: Verifica IDs)
                // Asumimos RolID 2 = Participante y EstadoCuentaID 1 = Activo (o Pendiente si requiere verificación)
                nuevoUsuario.RolID = 2; // ID de "Participante"
                nuevoUsuario.EstadoCuentaID = 1; // ID de "Activo" (o 2 si es "PendienteActivacion")
                nuevoUsuario.EstaActivo = true; // Por defecto, activo

                // 4. Llamar a la capa de Datos para insertar
                int idGenerado = datos.InsertarNuevo(nuevoUsuario);

                // 5. Verificar si la inserción fue exitosa
                return idGenerado > 0; // Si devolvió un ID > 0, fue exitoso

            }
            catch (Exception ex)
            {
                // Loggear el error ex
                // Considerar lanzar una excepción personalizada si es necesario
                return false; // Indicamos fallo general
            }
            // No necesitamos finally aquí, Datos ya cierra su conexión
        }
    }
}

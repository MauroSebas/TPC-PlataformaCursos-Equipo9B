using Datos;
using Dominio;
using Dominio.Enums;
using Microsoft.AspNet.Identity;
using Negocio.Seguridad;
using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Web;

namespace Negocio
{
    public class UsuarioNegocio
    {
        private readonly UsuarioDatos usuarioDatos = new UsuarioDatos();
        private readonly PerfilDatos perfilDatos = new PerfilDatos();
        private readonly UsuarioTokenNegocio tokenNegocio = new UsuarioTokenNegocio();
        private readonly EmailServicio emailServicio = new EmailServicio();

        private readonly PasswordHasher _hasher = new PasswordHasher(); 
        public void RegistrarUsuario(Usuario nuevoUsuario, string passwordPlano)
        {
            try
            {
                // 1. Validar que el email no exista
                if (usuarioDatos.BuscarPorEmail(nuevoUsuario.Email) != null)
                {
                    throw new Exception("El email ingresado ya se encuentra registrado.");
                }

                // 2. Validar reglas de negocio
                if (string.IsNullOrEmpty(passwordPlano) || passwordPlano.Length < 8)
                {
                    throw new Exception("La contraseña debe tener al menos 8 caracteres.");
                }

                // 3. Hashear la contraseña
                nuevoUsuario.PasswordHash = _hasher.HashPassword(passwordPlano); 

                // 4. Asignar valores por defecto
                nuevoUsuario.RolID = (int)RolEnum.Participante;
                nuevoUsuario.EstadoCuentaID = (int)EstadoCuentaEnum.PendienteActivacion;
                nuevoUsuario.EstaActivo = true;
                

                // 5. Insertar en BD
                int idGenerado = usuarioDatos.InsertarNuevo(nuevoUsuario);

                if (idGenerado <= 0)
                {
                    throw new Exception("Error general: No se pudo crear el usuario en la base de datos.");
                }

                // 6. Crear perfil vacío automáticamente
                this.CrearPerfilVacio(idGenerado);


                // 7. Generar el Token de Activación
                string token = tokenNegocio.GenerarToken(idGenerado, TipoTokenEnum.ActivacionCuenta);

                
                string host = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority;
                string applicationPath = HttpContext.Current.Request.ApplicationPath;
                //  path no termine con '/' si no es la raíz
                if (!applicationPath.Equals("/"))
                    applicationPath += "/";

                string linkActivacion = $"{host}{applicationPath}Auth/ActivarCuenta.aspx?token={token}";

                // 9. Estos son los reemplazos para el template HTML
                var reemplazos = new Dictionary<string, string>();
                reemplazos.Add("{{NOMBRE_USUARIO}}", nuevoUsuario.Email); 
                reemplazos.Add("{{LINK_ACTIVACION}}", linkActivacion);

                // 10. Enviar el Email
                emailServicio.EnviarTemplateEmail(
                    nuevoUsuario.Email,
                    "¡Activa tu cuenta en nuestra Plataforma!",
                    "ActivacionCuenta.html",
                    reemplazos
                );
            }
            catch (Exception ex)
            {
                
                if (ex.Message.Contains("El email ingresado") || ex.Message.Contains("La contraseña debe"))
                    throw ex; 

                
                throw new Exception("Error en la capa de negocio al registrar usuario.", ex);
            }
        }
        public Usuario ValidarLogin(string email, string passwordPlano)
        {
            try
            {
               
                var usuario = usuarioDatos.BuscarPorEmail(email);

           
                if (usuario == null)
                    return null; 

                if (!usuario.EstaActivo)
                    return null; 

               
                if (string.IsNullOrEmpty(usuario.PasswordHash))
                    return null;

                if (_hasher.VerifyHashedPassword(usuario.PasswordHash, passwordPlano) == PasswordVerificationResult.Failed)
                    return null;


                usuario.FechaUltimoLogin = DateTime.Now;
                usuarioDatos.Actualizar(usuario);        

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fatal en la capa de negocio al validar login.", ex);
            }
        }
        public void ReenviarTokenActivacion(string email)
        {
            try
            {
                
                var usuario = usuarioDatos.BuscarPorEmail(email);

                
                if (usuario == null)
                {
                   
                    throw new Exception("Si el email está registrado, te hemos enviado el correo.");
                }

                
                if (usuario.EstadoCuentaID == (int)EstadoCuentaEnum.Activo)
                {
                    throw new Exception("Esta cuenta ya se encuentra activa. Podés iniciar sesión directamente.");
                }

                // Esto es si el usuario esta trabado con el estado (PendienteActivacion)
                // Genera un NUEVO token 
                string token = tokenNegocio.GenerarToken(usuario.UsuarioID, TipoTokenEnum.ActivacionCuenta);

                //  Todo el proce de mandar el mail es lo mismo que para registrarte.
                //Tengo que meterlo en una función aparte, pero ahora no tengo tiempo.
                string host = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority;
                string applicationPath = HttpContext.Current.Request.ApplicationPath;
                if (!applicationPath.Equals("/"))
                    applicationPath += "/";

                string linkActivacion = $"{host}{applicationPath}Auth/ActivarCuenta.aspx?token={token}";

                
                var reemplazos = new Dictionary<string, string>();
                reemplazos.Add("{{NOMBRE_USUARIO}}", usuario.Email);
                reemplazos.Add("{{LINK_ACTIVACION}}", linkActivacion);

                
                emailServicio.EnviarTemplateEmail(
                    usuario.Email,
                    "Reenvío de Activación de Cuenta",
                    "ActivacionCuenta.html", 
                    reemplazos
                );
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("activa") ||          
                    ex.Message.Contains("registrado") ||     
                    ex.Message.Contains("Por favor, esperá")) 
                {
                    throw ex; 
                }
                
                throw new Exception("Error al procesar la solicitud de reenvío.", ex);
            }
        }
        public List<Usuario> ListarUsuarios(
            string email = null, string nombre = null, string apellido = null,
            int? rolId = null, int? estadoCuentaId = null, bool? estaActivo = null,
            DateTime? fechaCreacionDesde = null, DateTime? fechaCreacionHasta = null,
            DateTime? fechaUltimoLoginDesde = null, DateTime? fechaUltimoLoginHasta = null,
            DateTime? fechaBajaDesde = null, DateTime? fechaBajaHasta = null
        )
        {
            try
            {            
                var lista = usuarioDatos.Listar(
                    email, nombre, apellido, rolId, estadoCuentaId, estaActivo,
                    fechaCreacionDesde, fechaCreacionHasta, fechaUltimoLoginDesde, fechaUltimoLoginHasta,
                    fechaBajaDesde, fechaBajaHasta
                );               

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar usuarios en la capa de negocio.", ex);
            }
        }
      
        public Usuario ObtenerUsuarioPorID(int id)
        {
            try
            {
                var usuario = usuarioDatos.BuscarPorID(id);
                if (usuario == null)
                    throw new Exception("No se encontró el usuario con el ID especificado.");
               
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario por ID.", ex);
            }
        }
       
        public void ActualizarPassword(int usuarioID, string passwordActual, string passwordNueva)
        {
            var usuario = usuarioDatos.BuscarPorID(usuarioID);
            if (usuario == null)
                throw new Exception("No se encontró el usuario.");

            if (string.IsNullOrEmpty(usuario.PasswordHash) || _hasher.VerifyHashedPassword(usuario.PasswordHash, passwordActual) == PasswordVerificationResult.Failed)
                throw new Exception("La contraseña actual ingresada es incorrecta.");

            if (string.IsNullOrEmpty(passwordNueva) || passwordNueva.Length < 8)
                throw new Exception("La nueva contraseña debe tener al menos 8 caracteres.");

            usuario.PasswordHash = _hasher.HashPassword(passwordNueva);
            if (!usuarioDatos.Actualizar(usuario))
                throw new Exception("No se pudo actualizar la contraseña en la base de datos.");
        }
      
        public void CambiarEmail(int usuarioID, string nuevoEmail)
        {
            var usuario = usuarioDatos.BuscarPorID(usuarioID);
            if (usuario == null)
                throw new Exception("No se encontró el usuario.");
            
            var otroUsuario = usuarioDatos.BuscarPorEmail(nuevoEmail);
            if (otroUsuario != null && otroUsuario.UsuarioID != usuarioID)
                throw new Exception("El email ingresado ya está en uso por otra cuenta.");

            usuario.Email = nuevoEmail;
            if (!usuarioDatos.Actualizar(usuario))
                throw new Exception("No se pudo actualizar el email en la base de datos.");
        }
        public Usuario BuscarPorEmail(string email)
        {
            try
            {              
                return usuarioDatos.BuscarPorEmail(email);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de negocio al buscar por email.", ex);
            }
        }
        public void CambiarEstadoCuenta(int usuarioID, int nuevoEstadoCuentaID)
        {
            var usuario = usuarioDatos.BuscarPorID(usuarioID);
            if (usuario == null)
                throw new Exception("No se encontró el usuario.");

            usuario.EstadoCuentaID = nuevoEstadoCuentaID;
            if (!usuarioDatos.Actualizar(usuario))
                throw new Exception("No se pudo actualizar el estado de la cuenta.");
        }
        
        public void DarDeBajaUsuario(int usuarioID)
        {
            var usuario = usuarioDatos.BuscarPorID(usuarioID);
            if (usuario == null)
                throw new Exception("No se encontró el usuario.");

            usuario.EstaActivo = false;
            usuario.FechaBaja = DateTime.Now;
            if (!usuarioDatos.Actualizar(usuario))
                throw new Exception("No se pudo dar de baja al usuario.");
        }
        public void ReactivarUsuario(int usuarioID)
        {
            var usuario = usuarioDatos.BuscarPorID(usuarioID);
            if (usuario == null)
                throw new Exception("No se encontró el usuario.");

            usuario.EstaActivo = true;
            usuario.FechaBaja = null; 
            if (!usuarioDatos.Actualizar(usuario))
                throw new Exception("No se pudo reactivar al usuario.");
        }

        private void CrearPerfilVacio(int usuarioID)
        {
            if (usuarioID <= 0)
                throw new ArgumentException("ID de usuario inválido.");
            perfilDatos.InsertarPerfilVacio(usuarioID);
        }

        public void ActualizarPassword(int usuarioID, string nuevaPassword)
        {
            try
            {
                var usuario = usuarioDatos.BuscarPorID(usuarioID);
                if (usuario == null)
                    throw new Exception("No se encontró el usuario para actualizar la contraseña.");

                if (string.IsNullOrEmpty(nuevaPassword) || nuevaPassword.Length < 8)
                    throw new Exception("La nueva contraseña debe tener al menos 8 caracteres.");

                usuario.PasswordHash = _hasher.HashPassword(nuevaPassword);

                if (!usuarioDatos.Actualizar(usuario))
                    throw new Exception("No se pudo actualizar la contraseña en la base de datos.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la contraseña.", ex);
            }
        }
    }
}
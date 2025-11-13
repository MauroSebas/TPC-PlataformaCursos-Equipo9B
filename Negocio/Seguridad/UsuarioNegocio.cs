using Datos;
using Dominio;
using Dominio.Enums;
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
                nuevoUsuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordPlano);

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

                // 9. Armar los reemplazos para el template HTML
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

                if (!BCrypt.Net.BCrypt.Verify(passwordPlano, usuario.PasswordHash))
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
                // 1. Buscamos al usuario por email (la DAL nos trae todo)
                var usuario = usuarioDatos.BuscarPorEmail(email);

                // 2. REGLA DE NEGOCIO: ¿Existe el usuario?
                if (usuario == null)
                {
                    // ¡OJO! No le decimos "email no existe" por seguridad (para que
                    // no adivinen emails). Le damos un mensaje genérico.
                    throw new Exception("Si el email está registrado, te hemos enviado el correo.");
                }

                // 3. REGLA DE NEGOCIO: ¿Ya está activo?
                if (usuario.EstadoCuentaID == (int)EstadoCuentaEnum.Activo)
                {
                    throw new Exception("Esta cuenta ya se encuentra activa. Podés iniciar sesión directamente.");
                }

                // 4. ¡OK, es un usuario "trabado" (PendienteActivacion)!
                // Generamos un NUEVO token (la BLL de Token se encarga de esto)
                string token = tokenNegocio.GenerarToken(usuario.UsuarioID, TipoTokenEnum.ActivacionCuenta);

                // 5. Armamos el link (reutilizamos la lógica de RegistrarUsuario)
                string host = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority;
                string applicationPath = HttpContext.Current.Request.ApplicationPath;
                if (!applicationPath.Equals("/"))
                    applicationPath += "/";

                string linkActivacion = $"{host}{applicationPath}Auth/ActivarCuenta.aspx?token={token}";

                // 6. Armamos los reemplazos
                var reemplazos = new Dictionary<string, string>();
                reemplazos.Add("{{NOMBRE_USUARIO}}", usuario.Email);
                reemplazos.Add("{{LINK_ACTIVACION}}", linkActivacion);

                // 7. Enviar el Email (reutilizamos el servicio y el template)
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

            if (string.IsNullOrEmpty(usuario.PasswordHash) || !BCrypt.Net.BCrypt.Verify(passwordActual, usuario.PasswordHash))
                throw new Exception("La contraseña actual ingresada es incorrecta.");

            if (string.IsNullOrEmpty(passwordNueva) || passwordNueva.Length < 8)
                throw new Exception("La nueva contraseña debe tener al menos 8 caracteres.");

            usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordNueva);
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
                // Este es un simple "pass-through".
                // Llama a la DAL (que ya trae el objeto completo con JOINs)
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
    }
}
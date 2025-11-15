using Dominio;
using Dominio.Enums;
using Negocio;
using Negocio.Seguridad;
using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI; // Necesario para ScriptManager
using System.Web.UI.WebControls;

namespace Vistas.Auth
{
    public partial class RecuperarContraseña : System.Web.UI.Page
    {
        // Controles declarados en el ASPX (deben ser accesibles)
        protected System.Web.UI.WebControls.Panel pnlEmailSolicitud;
        protected System.Web.UI.WebControls.Panel pnlError;
        protected System.Web.UI.WebControls.Literal litErrorMessage;
        protected System.Web.UI.WebControls.TextBox txtEmailRecuperacion;
        protected System.Web.UI.WebControls.Button btnRestablecer;

        // Capas de Negocio
        private readonly UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        private readonly UsuarioTokenNegocio tokenNegocio = new UsuarioTokenNegocio();
        private readonly EmailServicio emailServicio = new EmailServicio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlError.Visible = false;
            }
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            // 1. Verificamos validaciones de ASP.NET (RequiredField, Regex)
            Page.Validate("RecuperarGroup");
            if (!Page.IsValid)
                return;

            string email = txtEmailRecuperacion.Text.Trim();
            pnlError.Visible = false;

            try
            {
                var usuario = usuarioNegocio.BuscarPorEmail(email);

                // *** INICIO DE VALIDACIÓN DE NEGOCIO ***
                // 2. Validar: El usuario debe existir para poder enviarle el link.
                if (usuario == null)
                {
                    // Lanza una excepción genérica para evitar que adivinen emails
                    throw new Exception("Si el email está registrado, te hemos enviado el correo.");
                }

                // 3. Generar Token (Tipo: Restablecer Contraseña)
                // Usaremos tu TipoTokenEnum.ResetPassword
                string token = tokenNegocio.GenerarToken(usuario.UsuarioID, Dominio.Enums.TipoTokenEnum.ResetPassword);

                // 4. Armar el Link (apunta a RestablecerContraseña.aspx)
                string host = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority;
                string applicationPath = HttpContext.Current.Request.ApplicationPath;
                if (!applicationPath.Equals("/"))
                    applicationPath += "/";

                string linkRestablecer = $"{host}{applicationPath}Auth/RestablecerContraseña.aspx?token={token}";

                // 5. Preparar y Enviar Email
                var reemplazos = new Dictionary<string, string>
                {
                    { "{{LINK_RESTABLECER}}", linkRestablecer }
                };

                emailServicio.EnviarTemplateEmail(
                    usuario.Email,
                    "Solicitud para Restablecer Contraseña",
                    "RestablecerContraseña.html",
                    reemplazos
                );

                // 6. ÉXITO TOTAL: Mostrar Modal de Confirmación
                MostrarMensajeExito(email);
            }
            catch (Exception ex)
            {
                // 7. FALLÓ: Mostramos la Excepción de Negocio al usuario
                litErrorMessage.Text = ex.Message;
                pnlError.Visible = true;
            }
        }

        private void MostrarMensajeExito(string email)
        {
            // Oculta el formulario y muestra el mensaje de éxito (que debe disparar el modal)
            pnlEmailSolicitud.Visible = false;

            // Inyectamos el script de Bootstrap 5 para MOSTRAR el modal
            string script = $@"
                document.addEventListener('DOMContentLoaded', function() {{
                    var modalEl = document.getElementById('exitoRecuperacionModal'); // Usar ID del Modal real
                    if (modalEl) {{
                        var modal = new bootstrap.Modal(modalEl);
                        modal.show();
                    }}
                }});
            ";
            // Usamos el literal para pasar el email al modal (si lo necesitamos)
            litSuccessMessage.Text = $"Hemos enviado las instrucciones a {email}.";

            // Registramos el script para que se ejecute al cargar la página
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowSuccessModalScript", script, true);
        }
    }
}
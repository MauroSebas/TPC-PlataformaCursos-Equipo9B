using Dominio;
using Dominio.Enums;
using Negocio;
using Negocio.Seguridad;
using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI; 
using System.Web.UI.WebControls;

namespace Vistas.Auth
{
    public partial class RecuperarContraseña : System.Web.UI.Page
    {       
       
       
        private readonly UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        private readonly UsuarioTokenNegocio tokenNegocio = new UsuarioTokenNegocio();
        private readonly EmailServicio emailServicio = new EmailServicio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlError.Visible = false;

                if (Session["Usuario"] != null)
                {
                    Usuario usuario = (Usuario)Session["Usuario"];
                    if (usuario.Rol.NombreRol == "Administrador")
                    {
                        Response.Redirect("~/Administrador/AdminPanel.aspx");
                        return;
                    }
                    else if (usuario.Rol.NombreRol == "Participante")
                    {
                        Response.Redirect("~/Alumno/MisCursos.aspx");
                    }
                }

            }
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            
            Page.Validate("RecuperarGroup");
            if (!Page.IsValid)
                return;

            string email = txtEmailRecuperacion.Text.Trim();
            pnlError.Visible = false;

            try
            {
                var usuario = usuarioNegocio.BuscarPorEmail(email);

                
                if (usuario == null)
                {
                    
                    throw new Exception("Si el email está registrado, te hemos enviado el correo.");
                }

              
                string token = tokenNegocio.GenerarToken(usuario.UsuarioID, Dominio.Enums.TipoTokenEnum.ResetPassword);

               
                string host = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority;
                string applicationPath = HttpContext.Current.Request.ApplicationPath;
                if (!applicationPath.Equals("/"))
                    applicationPath += "/";

                string linkRestablecer = $"{host}{applicationPath}Auth/RestablecerContraseña.aspx?token={token}";

               
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

                
                MostrarMensajeExito(email);
            }
            catch (Exception ex)
            {
               
                litErrorMessage.Text = ex.Message;
                pnlError.Visible = true;
            }
        }

        private void MostrarMensajeExito(string email)
        {
           
            pnlEmailSolicitud.Visible = false;

            
            string script = $@"
                document.addEventListener('DOMContentLoaded', function() {{
                    var modalEl = document.getElementById('exitoRecuperacionModal'); 
                    if (modalEl) {{
                        var modal = new bootstrap.Modal(modalEl);
                        modal.show();
                    }}
                }});
            ";
            
            litSuccessMessage.Text = $"Hemos enviado las instrucciones a {email}.";

            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowSuccessModalScript", script, true);
        }
    }
}
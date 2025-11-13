using Dominio;
using Dominio.Enums;
using Negocio;
using Negocio.Servicios;
using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace Vistas
{
    public partial class Loguin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlError.Visible = false;
            }
        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            Page.Validate("LoginGroup");
            if (!Page.IsValid)
                return;

            pnlError.Visible = false;
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuarioLogueado;

            try
            {
                usuarioLogueado = negocio.ValidarLogin(
                    txtEmail.Text.Trim(),
                    txtPassword.Text
                );

                if (usuarioLogueado == null)
                {
                    MostrarError("Email o contraseña incorrectos.");
                    return;
                }

                if (usuarioLogueado.EstadoCuentaID == (int)EstadoCuentaEnum.PendienteActivacion)
                {
                    ViewState["EmailParaReenvio"] = usuarioLogueado.Email;
                    MostrarError("Tu cuenta está pendiente de activación. Por favor, revisa tu correo.", true);
                    return;
                }

                Session["Usuario"] = usuarioLogueado;
                // Armar el nombre del rol EXACTO tal como está en DB
                string rolNombre = usuarioLogueado.RolID == (int)RolEnum.Administrador ? "Administrador" : "Participante";

                // Crear ticket + cookie
                var ticket = new FormsAuthenticationTicket(
                    1,
                    usuarioLogueado.Email,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(60),
                    false,
                    rolNombre,
                    FormsAuthentication.FormsCookiePath
                );

                string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                {
                    HttpOnly = true,
                    Secure = Request.IsSecureConnection 
                };
                Response.Cookies.Add(authCookie);

              
                if (usuarioLogueado.RolID == (int)RolEnum.Administrador)
                    Response.Redirect("~/Administrador/Default.aspx", true);
                else
                    Response.Redirect("~/Alumno/MisCursos.aspx", true);
            }
            catch (Exception ex)
            {
                MostrarError("Error fatal: " + ex.Message);
            }
        }

        protected void lnkReenviar_Click(object sender, EventArgs e)
        {
            try
            {
                string email = ViewState["EmailParaReenvio"]?.ToString();
                if (string.IsNullOrEmpty(email))
                {
                    MostrarError("No se pudo encontrar el email para el reenvío. Vuelve a intentarlo.");
                    return;
                }

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.ReenviarTokenActivacion(email);
               
                pnlError.Visible = false;
                
                string script = $@"
                    document.addEventListener('DOMContentLoaded', function() {{
                        var modalEl = document.getElementById('{reenvioExitosoModal.ClientID}');
                        if (modalEl) {{
                            var modal = new bootstrap.Modal(modalEl);
                            modal.show();
                        }}
                    }});
                ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowReenvioModalScript", script, true);
            }
            catch (Exception ex)
            {
                MostrarError("Error al reenviar el correo: " + ex.Message);
            }
        }

        private void MostrarError(string mensaje, bool mostrarLinkReenvio = false)
        {
            pnlError.Visible = true;
            pnlError.CssClass = "alert alert-danger";
            litError.Text = mensaje;
            lnkReenviar.Visible = mostrarLinkReenvio;
        }
    }
}
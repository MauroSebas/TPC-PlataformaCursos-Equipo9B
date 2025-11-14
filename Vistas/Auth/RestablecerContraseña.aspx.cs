using Dominio.Enums;
using Negocio;
using Negocio.Seguridad;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Auth
{
    public partial class RestablecerContraseña : System.Web.UI.Page
    {
        private readonly UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        private readonly UsuarioTokenNegocio tokenNegocio = new UsuarioTokenNegocio();

        // Propiedad para guardar el ID del usuario VÁLIDO (es el ID obtenido del token)
        public int UsuarioIDToken
        {
            get { return (int)(ViewState["UsuarioIDToken"] ?? 0); }
            set { ViewState["UsuarioIDToken"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarTokenDeLaURL();
            }
        }

        private void ValidarTokenDeLaURL()
        {
            pnlFormulario.Visible = false; 

            string token = Request.QueryString["token"];

            if (string.IsNullOrEmpty(token))
            {
                MostrarMensaje("Error", "No se proporcionó un token de restablecimiento.", "alert-danger", false);
                return;
            }

            try
            {
               
                int usuarioID = tokenNegocio.ValidarToken(token, TipoTokenEnum.ResetPassword); 

                
                UsuarioIDToken = usuarioID;

                
                pnlFormulario.Visible = true;
                pnlMensaje.Visible = false;
            }
            catch (Exception ex)
            {
                
                MostrarMensaje("Enlace Inválido", ex.Message, "alert-warning", true);
            }
        }

        protected void btnCambiarPassword_Click(object sender, EventArgs e)
        {
            
            Page.Validate("RestablecerGroup");
            if (!Page.IsValid)
                return;

            pnlError.Visible = false;

            try
            {
                int usuarioID = UsuarioIDToken;

               
                if (usuarioID == 0)
                {
                    
                    throw new Exception("Error de sesión. El token ya no es válido.");
                }

                string nuevaPassword = txtNuevaPassword.Text;

                
                usuarioNegocio.ActualizarPassword(usuarioID, nuevaPassword);

                
                pnlFormulario.Visible = false;
                MostrarMensaje("¡Contraseña Actualizada!", "Tu contraseña ha sido restablecida correctamente. Ya puedes iniciar sesión.", "alert-success", true);

                
            }
            catch (Exception ex)
            {
                
                litErrorMessage.Text = ex.Message;
                pnlError.Visible = true;
            }
        }

        // Método Helper para mostrar mensajes de éxito/error en el panel de alerta
        private void MostrarMensaje(string titulo, string mensaje, string cssClass, bool mostrarLogin)
        {
            pnlMensaje.Visible = true;
            pnlMensaje.CssClass = cssClass;

            string icon = cssClass.Contains("danger") || cssClass.Contains("warning") ? "❌" : "✅";

            litTitulo.Text = icon + " " + titulo;
            litMensaje.Text = mensaje;
            hlLogin.Visible = mostrarLogin;

            pnlFormulario.Visible = false;
        }
    }
}
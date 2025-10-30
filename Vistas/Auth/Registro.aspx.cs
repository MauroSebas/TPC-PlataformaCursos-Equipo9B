using System;
using System.Web.UI;
using Dominio;
using Negocio;

namespace Vistas
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlError.Visible = false;
            }
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            Page.Validate("RegistroGroup");
            if (!Page.IsValid)
                return;

            pnlError.Visible = false;

            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario nuevoUsuario = new Usuario
                {
                    Email = txtEmailRegistro.Text.Trim(),
                    PasswordHash = txtPasswordRegistro.Text
                };

                bool registroExitoso = negocio.RegistrarUsuario(nuevoUsuario);

                if (registroExitoso)
                {
                    txtEmailRegistro.Text = "";
                    txtPasswordRegistro.Text = "";
                    txtConfirmPassword.Text = "";

                    // Modal se abre después del postback
                    litScriptModal.Text = $@"
                        <script>
                        document.addEventListener('DOMContentLoaded', function() {{
                            document.getElementById('{emailUsuarioModal.ClientID}').innerText = '{nuevoUsuario.Email}';
                            var modal = new bootstrap.Modal(document.getElementById('{registroExitosoModal.ClientID}'));
                            modal.show();
                        }});
                        </script>";
                }
                else
                {
                    litErrorMessage.Text = "Error al registrar: El email ingresado ya existe.";
                    pnlError.Visible = true;
                }
            }
            catch (Exception)
            {
                litErrorMessage.Text = "Ocurrió un error inesperado. Por favor, intenta de nuevo más tarde.";
                pnlError.Visible = true;
            }
        }
    }
}

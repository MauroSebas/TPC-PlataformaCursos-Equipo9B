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
            // Ocultar panel de error al cargar la página por primera vez
            if (!IsPostBack)
            {
                pnlError.Visible = false;
            }
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            // Validar en servidor usando el ValidationGroup 
            Page.Validate("RegistroGroup");
            if (!Page.IsValid)
            {
                return; 
            }

            // Ocultar mensajes de error previos al intentar de nuevo
            pnlError.Visible = false;

            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario nuevoUsuario = new Usuario();

                nuevoUsuario.Email = txtEmailRegistro.Text.Trim();
                nuevoUsuario.PasswordHash = txtPasswordRegistro.Text; // Pasa la contraseña plana, Negocio la hashea

                // Llamar a negocio (setea EstadoCuentaID = 2 por defecto)
                bool registroExitoso = negocio.RegistrarUsuario(nuevoUsuario);

                if (registroExitoso)
                {
                    
                    txtEmailRegistro.Text = "";
                    txtPasswordRegistro.Text = "";
                    txtConfirmPassword.Text = "";

                    
                    //   modal por su ID y usa el método 'show' de Bootstrap
                    string script = $@"
                        document.getElementById('{emailUsuarioModal.ClientID}').innerText = '{nuevoUsuario.Email}';
                        var modal = new bootstrap.Modal(document.getElementById('{registroExitosoModal.ClientID}'));
                        modal.show();
                    ";

                    //  ScriptManager para que funcione dentro del UpdatePanel
                    ScriptManager.RegisterStartupScript(upRegistro, upRegistro.GetType(), "MostrarRegistroExitosoModal", script, true);
                }
                else
                {
                    // Registro Falló -> Mostrar error
                    litErrorMessage.Text = "Error al registrar: El email ingresado ya existe.";
                    pnlError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Error inesperado -> Mostrar error genérico
                // Meter log para depuración si se rompe
                litErrorMessage.Text = "Ocurrió un error inesperado. Por favor, intenta de nuevo más tarde.";
                pnlError.Visible = true;
            }
        }
    }
}

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
            // 1. Verificamos los validadores del FRONT-END
            // (RequiredField, Compare, Regex de Email y Password)
            // ¡OJO! Tu botón tiene ValidationGroup="RegistroGroup", 
            // así que el Page.IsValid solo funciona si lo llamás explícitamente.
            Page.Validate("RegistroGroup");
            if (!Page.IsValid)
            {
                return; // Los validadores ASP.NET ya mostraron el error.
            }

            // Si los validadores de front pasaron, ocultamos errores viejos
            pnlError.Visible = false;

            // 2. ¡Llamamos a la Capa de Negocio (BLL) con TRY-CATCH!
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();

                // 3. Creamos el objeto Usuario (solo con el email)
                Usuario nuevo = new Usuario
                {
                    Email = txtEmailRegistro.Text.Trim()
                    // ¡OJO! Ya no pasamos el hash.
                };

                // 4. Obtenemos el password en texto plano
                string passwordPlano = txtPasswordRegistro.Text;

                // 5. ¡¡EJECUTAMOS EL PROCESO!!
                // Este método AHORA es VOID (no devuelve bool).
                // Si no lanza una excepción, es que fue un ÉXITO.
                negocio.RegistrarUsuario(nuevo, passwordPlano);

                // 6. ¡¡ÉXITO TOTAL!!
                // Si llegamos a esta línea, es porque NINGUNA excepción saltó.

                // Limpiamos los campos
                txtEmailRegistro.Text = "";
                txtPasswordRegistro.Text = "";
                txtConfirmPassword.Text = "";

                // Ponemos el email del usuario en el modal
                emailUsuarioModal.InnerText = nuevo.Email;

                // Inyectamos el script de Bootstrap 5 para MOSTRAR el modal
                // (Usamos ScriptManager para que funcione bien en PostBacks)
                string script = $@"
            document.addEventListener('DOMContentLoaded', function() {{
                var modalEl = document.getElementById('{registroExitosoModal.ClientID}');
                if (modalEl) {{
                    var modal = new bootstrap.Modal(modalEl);
                    modal.show();
                }}
            }});
        ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModalScript", script, true);
            }
            catch (Exception ex)
            {
                // 7. ¡¡FALLÓ!!
                // La BLL (negocio.RegistrarUsuario) nos "gritó" una Excepción.
                // (ej. "El email ya existe", "Error al enviar el email", etc.)

                // Mostramos el Panel de Error y le ponemos el MENSAJE EXACTO
                // que nos dio la BLL. ¡Ya no adivinamos!
                pnlError.Visible = true;
                litErrorMessage.Text = ex.Message; // <-- ¡ESTA ES LA MAGIA!
            }
        }
    }
}

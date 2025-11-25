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

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            
            Page.Validate("RegistroGroup");
            if (!Page.IsValid)
            {
                return; 
            }

            
            pnlError.Visible = false;

            
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();

               
                Usuario nuevo = new Usuario
                {
                    Email = txtEmailRegistro.Text.Trim()
                    
                };

               
                string passwordPlano = txtPasswordRegistro.Text;

                
                negocio.RegistrarUsuario(nuevo, passwordPlano);

                
                txtEmailRegistro.Text = "";
                txtPasswordRegistro.Text = "";
                txtConfirmPassword.Text = "";

                
                emailUsuarioModal.InnerText = nuevo.Email;

                
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
                
                pnlError.Visible = true;
                litErrorMessage.Text = ex.Message; 
            }
        }
    }
}

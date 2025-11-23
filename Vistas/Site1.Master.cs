using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConfigurarMenu();
            }
        }
            private void ConfigurarMenu()
             {
           
            if (Session["Usuario"] != null)
            {
                Usuario usuario = (Usuario)Session["Usuario"];

               
                phAnonimo.Visible = false;

                
                phLogueado.Visible = true;
                if(usuario.Perfil.NombreCompleto != "")
                {
                    litNombreUser.Text = usuario.Perfil.NombreCompleto;
                }
                else
                {
                    litNombreUser.Text = usuario.Email.Split('@')[0];
                }


              
                if (usuario.Perfil != null && !string.IsNullOrEmpty(usuario.Perfil.UrlFotoPerfil))
                    imgAvatar.ImageUrl = ResolveUrl(usuario.Perfil.UrlFotoPerfil);
                else
                    imgAvatar.ImageUrl = "https://ui-avatars.com/api/?name=" + litNombreUser.Text + "&background=0D8ABC&color=fff";

                // Lógica según ROL
                if (usuario.Rol.NombreRol == "Administrador")
                {                    
                    phBotonAdmin.Visible = true;
                    phBotonAlumno.Visible = false;
                    phCart.Visible = false;
                }
                else 
                {
                    phBotonAdmin.Visible = false;
                    phBotonAlumno.Visible = true; 
                }
            }
            
            else
            {
               
                phAnonimo.Visible = true;
                phLogueado.Visible = false;
                phBotonAlumno.Visible = false;
            }
        }
        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Home.aspx");
        }

    }
}

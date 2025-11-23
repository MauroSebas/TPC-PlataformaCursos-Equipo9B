using Dominio; // <-- ¡Necesitamos esto para "ver" la clase Usuario!
using System;
using System.Web.UI;

namespace Vistas
{
    public partial class AlumnoMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Session["Usuario"] == null)
            {
                
                Response.Redirect("~/Auth/Loguin.aspx?error=DebeLoguearse", false);
                return; 
            }

          
            if (!IsPostBack) 
            {
                
                Usuario userLogueado = (Usuario)Session["Usuario"];

                


                
                if (userLogueado.Perfil != null)
                {
                    string nombreParaMostrar;
                    string inicialParaAvatar;

                   
                    if (!string.IsNullOrWhiteSpace(userLogueado.Perfil.Nombre) &&
                        !string.IsNullOrWhiteSpace(userLogueado.Perfil.Apellido))
                    {
                        
                        nombreParaMostrar = userLogueado.Perfil.NombreCompleto;
                      
                        inicialParaAvatar = userLogueado.Perfil.Nombre[0].ToString();
                    }
                    else
                    {
                       
                        nombreParaMostrar = userLogueado.Email.Split('@')[0];
                        
                        inicialParaAvatar = userLogueado.Email[0].ToString();
                    }

                   
                    litNombreUsuario.Text = nombreParaMostrar;

                    
                    if (string.IsNullOrEmpty(userLogueado.Perfil.UrlFotoPerfil))
                    {
                       
                        imgAvatar.ImageUrl = $"https://placehold.co/32x32/0d6efd/FFFFFF?text={inicialParaAvatar.ToUpper()}";
                    }
                    else
                    {
                       
                        imgAvatar.ImageUrl = userLogueado.Perfil.UrlFotoPerfil;
                    }
                }
                else
                {
                   
                    litNombreUsuario.Text = userLogueado.Email.Split('@')[0];
                    imgAvatar.ImageUrl = $"https://placehold.co/32x32/888/FFFFFF?text={userLogueado.Email[0].ToString().ToUpper()}";
                }
            }
        }

       
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            
            Session.Abandon();

           
            Response.Redirect("~/Home.aspx");
        }
    }
}
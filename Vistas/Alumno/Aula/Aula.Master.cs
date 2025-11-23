using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class Aula1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 1. SEGURIDAD: Si se cayó la sesión, lo sacamos del aula
            if (Session["Usuario"] == null)
            {
                // Redirigimos al login y guardamos la URL para que vuelva directo a esta clase
                string urlActual = Request.Url.PathAndQuery;
                Response.Redirect($"~/Auth/Loguin.aspx?error=SesionExpirada&ReturnUrl={Server.UrlEncode(urlActual)}", true);
                return;
            }

            if (!IsPostBack)
            {
                CargarDatosUsuarioHeader();
            }
        }

        private void CargarDatosUsuarioHeader()
        {
            Usuario user = (Usuario)Session["Usuario"];

            // Lógica para mostrar Nombre o Email
            string nombreMostrar = user.Email.Split('@')[0]; // Default
            string inicial = user.Email.Substring(0, 1).ToUpper();
            string urlAvatar = "";

            if (user.Perfil != null)
            {
                if (!string.IsNullOrWhiteSpace(user.Perfil.Nombre))
                {
                    nombreMostrar = user.Perfil.Nombre + " " + user.Perfil.Apellido;
                    inicial = user.Perfil.Nombre.Substring(0, 1).ToUpper();
                }
                urlAvatar = user.Perfil.UrlFotoPerfil;
            }

            // Asignamos a los controles del Header (si existen en tu HTML de Aula.Master)
            // Como en tu HTML anterior no vi los controles runat="server" para el nombre en el Aula.Master,
            // verificá que existan o agregalos:

            if (litNombreUsuario != null)
                litNombreUsuario.Text = nombreMostrar;

            if (imgAvatar != null)
            {
                if (!string.IsNullOrEmpty(urlAvatar))
                    imgAvatar.ImageUrl = ResolveUrl(urlAvatar);
                else
                    imgAvatar.ImageUrl = $"https://placehold.co/32x32/0d6efd/FFFFFF?text={inicial}";
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Home.aspx");
        }
    }
}


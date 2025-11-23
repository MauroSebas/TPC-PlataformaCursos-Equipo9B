using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class MisCursos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Auth/Loguin.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarMisCursos();
            }
        }

        private void CargarMisCursos()
        {
            try
            {
               
                Usuario usuario = (Usuario)Session["Usuario"];

               
                InscripcionNegocio negocio = new InscripcionNegocio();
                List<Inscripcion> listaInscripciones = negocio.ListarPorUsuario(usuario.UsuarioID);

               
                if (listaInscripciones.Count > 0)
                {
                    repMisCursos.DataSource = listaInscripciones;
                    repMisCursos.DataBind();
                }
                else
                {
                    pnlSinCursos.Visible = true; 
                }
            }
            catch (Exception ex)
            {
               
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx");
            }
        }

        public string ObtenerImagen(object urlObj)
        {
            string url = urlObj as string;
            if (string.IsNullOrEmpty(url))
                return ResolveUrl("~/Assets/img/placeholder-curso.jpg");

            return ResolveUrl(url);
        }
    }
}

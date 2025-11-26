using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Alumno
{
    public partial class Certificados : System.Web.UI.Page
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
                CargarCertificados();
            }
        }

        private void CargarCertificados()
        {
            try
            {
                Usuario u = (Usuario)Session["Usuario"];
                CertificadoNegocio negocio = new CertificadoNegocio();

               
                List<Certificado> lista = negocio.ListarMisCertificados(u.UsuarioID);

                if (lista.Count > 0)
                {
                    repCertificados.DataSource = lista;
                    repCertificados.DataBind();
                    pnlVacio.Visible = false;
                }
                else
                {
                    pnlVacio.Visible = true; 
                }
            }
            catch (Exception)
            {
                
                pnlVacio.Visible = true;
            }
        }
    }
}

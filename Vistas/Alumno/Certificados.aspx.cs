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
            // 1. Validar Sesión
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

                // Llamamos al método que ya creamos en Negocio
                List<Certificado> lista = negocio.ListarMisCertificados(u.UsuarioID);

                if (lista.Count > 0)
                {
                    repCertificados.DataSource = lista;
                    repCertificados.DataBind();
                    pnlVacio.Visible = false;
                }
                else
                {
                    pnlVacio.Visible = true; // Mostrar cartel de vacío
                }
            }
            catch (Exception)
            {
                // Si falla la base de datos, mostramos vacío para no romper la UI
                pnlVacio.Visible = true;
            }
        }
    }
}

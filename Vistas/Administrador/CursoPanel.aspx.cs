using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Aministrador
{
    public partial class CursoPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CursoNegocio negocio = new CursoNegocio();
                    dgvCurso.DataSource = negocio.listarCursos();
                    dgvCurso.DataBind();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        protected void btnAgregarCurso_Click1(object sender, EventArgs e)
        {
            Response.Redirect("CursoForm.aspx");
        }

        protected void dgvCurso_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
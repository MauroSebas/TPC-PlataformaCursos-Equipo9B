using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Aministrador
{
    public partial class LeccionForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("CursoPanel.aspx");
        }

        protected void btnGuardaryContinuar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ModuloGestion.aspx");
        }
    }
}
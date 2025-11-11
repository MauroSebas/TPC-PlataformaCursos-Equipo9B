using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Aministrador
{
    public partial class ModuloGestion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAgregarLeccion_Click(object sender, EventArgs e)
        {
            Response.Redirect("LeccionForm.aspx");
        }

        protected void btnGuardarySalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("CursoPanel.aspx");
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("CursoPanel.aspx");
        }
    }
}
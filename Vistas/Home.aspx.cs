using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Vistas
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NegocioCategoria negocio = new NegocioCategoria();
            dgvCategorias.DataSource = negocio.listar();
            dgvCategorias.DataBind();
            
        }
    }
}
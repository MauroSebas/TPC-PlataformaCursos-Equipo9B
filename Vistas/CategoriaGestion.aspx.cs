using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace Vistas
{
    public partial class CategoriaGestion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NegocioCategoria negocio = new NegocioCategoria();
            dgvCategorias.DataSource = negocio.listarConSP();
            dgvCategorias.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("CategoriaGestion.aspx");
        }

        protected void dgvCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = dgvCategorias.SelectedDataKey.Value.ToString();

            Response.Redirect("CategoriaEliminarEditar.aspx?id=" + id);
            
        }

        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            NegocioCategoria negocio = new NegocioCategoria();
            Categoria nueva = new Categoria();
            nueva.Nombre = txtNombre.Text;
            negocio.agregarConSP(nueva);

        }
    }
}
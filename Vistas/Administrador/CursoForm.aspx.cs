using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Aministrador
{
    public partial class CursoForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {   
            NegocioCategoria negocioCat = new NegocioCategoria();
            try
            {
                if (!IsPostBack)
                {
                    ddlCategoria.DataSource = negocioCat.listarConSP();
                    ddlCategoria.DataBind();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnGuardarCurso_Click(object sender, EventArgs e)
        {
            CursoNegocio negocio = new CursoNegocio();
            Curso nuevo = new Curso();
            nuevo.Titulo = txtNombreCurso.Text;
            nuevo.Descripcion = txtDescripcion.Text;
            nuevo.Precio = decimal.Parse(txtPrecio.Text);

            nuevo.Categoria = new Categoria();
            nuevo.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);
            nuevo.UrlImagenPortada = txtImagenPortada.Text;
            nuevo.ModalidadPago = "Transferencia";

            if (rbAccesoPermanente.Checked)
            {
                nuevo.DuracionAccesoDias = 0;
            }
            else if (rbTiempoLimitado.Checked)
            {
                nuevo.DuracionAccesoDias = int.Parse(txtDuracionDias.Text);
            }

            negocio.agregarConSP(nuevo);
            Response.Redirect("ModuloGestion.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("CursoPanel.aspx");
        }
    }
}
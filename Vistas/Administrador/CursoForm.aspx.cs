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
            CategoriaNegocio negocioCat = new CategoriaNegocio();
            try
            {
                if (!IsPostBack)
                {
                    ddlCategoria.DataSource = negocioCat.listarCategoria();
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
            
            try
            {
                nuevo.Titulo = txtNombreCurso.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.Precio = decimal.Parse(txtPrecio.Text);

                nuevo.Categoria = new Categoria();
                nuevo.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);
                nuevo.UrlImagenPortada = txtImagenPortada.Text;
                nuevo.ModalidadPago = "Transferencia";

                if (rdbAccesoPermanente.Checked)
                {
                    nuevo.DuracionAccesoDias = 0;
                }
                else if (rdbTiempoLimitado.Checked)
                {
                    nuevo.DuracionAccesoDias = int.Parse(txtDuracionDias.Text);
                }

                int IdGenerado = negocio.agregarCurso(nuevo);

                if (  IdGenerado > 0)
                {
                    //Modal informando ingreso exitoso
                    
                    Response.Redirect("ModuloGestion.aspx?ID=" + IdGenerado, false);
                }
                else
                {
                    //Modal informando ingreso defectuoso
                }



            }
            catch(Exception ex)
            {
                throw new Exception("No se pudo generar ek nuevo curso", ex);
            }
                                                                                                                    

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("CursoPanel.aspx");
        }
    }
}
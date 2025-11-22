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
    public partial class CursoPanel : System.Web.UI.Page
    {

        private readonly CursoNegocio _cursoNegocio = new CursoNegocio();
        private readonly CategoriaNegocio _catNegocio = new CategoriaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                pnlMensajeGlobal.Visible = false;
               
                if (Session["CursoPanelMensaje"] != null)
                {
                    MostrarMensajeGlobal(Session["CursoPanelMensaje"].ToString());
                    Session["CursoPanelMensaje"] = null;
                }

                if (!IsPostBack)
                {
                    CargarGrilla();
                    CargarFiltros();
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error al cargar la página: {ex.Message}", true);
            }
        }
        private void CargarGrilla()
        {           
            List<Curso> lista = _cursoNegocio.listarCursos();
            gvCursos.DataSource = lista;
            gvCursos.DataBind();
        }
        private void CargarFiltros()
        {
            try
            {
                
                ddlCategoriaFiltro.DataSource = _catNegocio.listarCategoria();
                ddlCategoriaFiltro.DataBind();                
                ddlCategoriaFiltro.Items.Insert(0, new ListItem("-- Todas las Categorías --", "0"));
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error al cargar categorías: {ex.Message}", true);
            }
        }    
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
               
                string titulo = txtBuscar.Text;
                int catId = Convert.ToInt32(ddlCategoriaFiltro.SelectedValue);

               
                gvCursos.DataSource = _cursoNegocio.filtrarCursos(titulo, catId);
                gvCursos.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error al filtrar: {ex.Message}", true);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            ddlCategoriaFiltro.SelectedValue = "0";
            CargarGrilla(); 
        }       
        protected void gvCursos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCursos.PageIndex = e.NewPageIndex;
            CargarGrilla(); 
        }    
        protected void chkPublicado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)sender;
                GridViewRow row = (GridViewRow)chk.NamingContainer;
                int cursoId = Convert.ToInt32(gvCursos.DataKeys[row.RowIndex].Value);

                
                _cursoNegocio.CambiarEstadoPublicado(cursoId, chk.Checked);

                
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error al cambiar estado: {ex.Message}", true);
            }
        }
       
        protected void gvCursos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                // Buscamos la tabla que ASP.NET crea
                if (e.Row.Cells.Count > 0 && e.Row.Cells[0].Controls.Count > 0 && e.Row.Cells[0].Controls[0] is Table)
                {
                    Table pagerTable = (Table)e.Row.Cells[0].Controls[0];
                    pagerTable.Attributes.Add("class", "pagination justify-content-center mb-0");

                    foreach (TableRow row in pagerTable.Rows)
                    {
                        foreach (TableCell cell in row.Cells)
                        {
                            cell.Attributes.Add("class", "page-item");
                            if (cell.Controls.Count > 0 && cell.Controls[0] is LinkButton)
                            {
                                ((LinkButton)cell.Controls[0]).CssClass = "page-link";
                            }
                            else if (cell.Controls.Count > 0 && cell.Controls[0] is Label)
                            {
                                // Página actual
                                ((Label)cell.Controls[0]).CssClass = "page-link";
                                cell.CssClass = "page-item active";
                            }
                        }
                    }
                }
            }
        }
        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Recuperamos el ID del HiddenField
                int idCurso = int.Parse(hfIdCursoEliminar.Value);

                // 2. Llamamos a Negocio para eliminar (que ahora llama al SP corregido)
                _cursoNegocio.eliminarCursoLogico(idCurso);

                // 3. Actualizamos la grilla
                CargarGrilla();

                // 4. Feedback visual (Opcional: Cerrar modal por si quedó abierto y mostrar mensaje)
                // Como estamos dentro de un UpdatePanel, el modal se cierra solo al renderizar de nuevo, 
                // pero mostramos el mensaje de éxito.
                MostrarMensajeGlobal("El curso se archivó correctamente.", false);
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error al eliminar: {ex.Message}", true);
            }
        }
        private void MostrarMensajeGlobal(string mensaje, bool esError = false)
        {
            pnlMensajeGlobal.Visible = true;
            litMensajeGlobal.Text = mensaje;
            pnlMensajeGlobal.CssClass = esError ? "alert alert-danger" : "alert alert-success";
           
            updMensajeGlobal.Update();
        }

    }
}

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

        // Declaramos los negocios acá para reusarlos
        private readonly CursoNegocio _cursoNegocio = new CursoNegocio();
        private readonly CategoriaNegocio _catNegocio = new CategoriaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Ocultamos el panel en cada carga
                pnlMensajeGlobal.Visible = false;

                // Chequeamos si venimos de un guardado exitoso
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

        // --- MÉTODOS DE CARGA ---

        private void CargarGrilla()
        {
            // ¡¡Usa el ID correcto gvCursos!!
            gvCursos.DataSource = _cursoNegocio.listarCursos();
            gvCursos.DataBind();
        }

        private void CargarFiltros()
        {
            // (Asumiendo que tu CategoriaNegocio tiene "listarCategoria")
            ddlCategoriaFiltro.DataSource = _catNegocio.listarCategoria();
            ddlCategoriaFiltro.DataBind();
            // Le agregamos el "Todas" al principio
            ddlCategoriaFiltro.Items.Insert(0, new ListItem("-- Todas las Categorías --", "0"));
        }

        // --- EVENTOS DE LOS BOTONES DE FILTRO ---

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            // (Acá iría la lógica de filtrar)
            CargarGrilla();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            ddlCategoriaFiltro.SelectedValue = "0";
            CargarGrilla();
        }

        // --- EVENTOS DE LA GRILLA (gvCursos) ---

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

                // (Falta este método en CursoNegocio, pero lo dejamos listo)
                // _cursoNegocio.CambiarEstadoPublicado(cursoId, chk.Checked);

                MostrarMensajeGlobal($"Estado del curso {cursoId} cambiado.", false);
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error al cambiar estado: {ex.Message}", true);
            }
        }

        protected void gvCursos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Archivar")
            {
                try
                {
                    int cursoId = Convert.ToInt32(e.CommandArgument);
                    // ¡Llamamos a la capa de negocio blindada!
                    _cursoNegocio.eliminarCursoLogico(cursoId);

                    CargarGrilla();
                    MostrarMensajeGlobal("Curso archivado con éxito.", false);
                }
                catch (Exception ex)
                {
                    // (Acá caería el error de "no podés borrar si tiene alumnos")
                    MostrarMensajeGlobal($"Error al archivar: {ex.Message}", true);
                }
            }
        }

        // (El btnAgregarCurso_Click1 se borra porque el botón es un HyperLink)

        // --- ¡¡HELPERS COPIADOS Y PEGADOS (como pediste)!! ---

        /// <summary>
        /// Muestra un mensaje en un panel de error "LOCAL" (adentro de otro panel).
        /// (Lo dejamos acá por si lo necesitamos después)
        /// </summary>
        private void MostrarErrorEnPanel(Panel pnlError, Literal litError, string mensaje)
        {
            pnlError.Visible = true;
            litError.Text = mensaje;
            pnlMensajeGlobal.Visible = false;
            updMensajeGlobal.Update();
        }

        /// <summary>
        /// Muestra un mensaje en el panel "GLOBAL" (el de arriba de todo).
        /// </summary>
        private void MostrarMensajeGlobal(string mensaje, bool esError = false)
        {
            pnlMensajeGlobal.Visible = true;
            litMensajeGlobal.Text = mensaje;
            pnlMensajeGlobal.CssClass = esError ? "alert alert-danger" : "alert alert-success";

            // Forzamos la actualización del panel global
            updMensajeGlobal.Update();
        }
        protected void gvCursos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // ¡¡ESTA ES LA MAGIA PARA LA PAGINACIÓN DE BOOTSTRAP!!
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                if (e.Row.FindControl("gvCursos_pager") != null)
                {
                    // Si encontramos el pager, lo tuneamos
                    LinkButton btnFirst = (LinkButton)e.Row.FindControl("gvCursos_First");
                    LinkButton btnPrev = (LinkButton)e.Row.FindControl("gvCursos_Prev");
                    LinkButton btnNext = (LinkButton)e.Row.FindControl("gvCursos_Next");
                    LinkButton btnLast = (LinkButton)e.Row.FindControl("gvCursos_Last");

                    if (btnFirst != null) btnFirst.CssClass = "page-link";
                    if (btnPrev != null) btnPrev.CssClass = "page-link";
                    if (btnNext != null) btnNext.CssClass = "page-link";
                    if (btnLast != null) btnLast.CssClass = "page-link";
                }

                // Buscamos los links de los números (1, 2, 3...)
                Table pagerTable = (Table)e.Row.Cells[0].Controls[0];
                if (pagerTable != null)
                {
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
                                // Este es el número de la página actual
                                ((Label)cell.Controls[0]).CssClass = "page-link";
                                cell.CssClass = "page-item active"; // Lo marcamos como activo
                            }
                        }
                    }
                }
            }
        }

    }
}

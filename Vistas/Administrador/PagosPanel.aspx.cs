using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Vistas.Aministrador
{
    public partial class PagosPanel : System.Web.UI.Page
    {
        // MÉTODO CENTRALIZADO: Se usa en Load, Filtrar, Limpiar y Paginación
        private void CargarPagos()
        {
            try
            {
                PagoNegocio negocio = new PagoNegocio();

                // 1. Obtener valores de la UI
                string estadoFiltro = ddlEstadoFiltro.SelectedValue;
                if (estadoFiltro == "") estadoFiltro = null;

                string textoBusqueda = txtBuscar.Text.Trim();
                if (textoBusqueda == "") textoBusqueda = null;

                // 2. Usar el NUEVO método de filtrado
                // Este método maneja tanto el estado como la búsqueda en la BD
                List<Pago> lista = negocio.FiltrarPagos(estadoFiltro, textoBusqueda);

                // 3. Asignar al GridView
                gvPagos.DataSource = lista;
                gvPagos.DataBind();

                // 4. Asignar al GridView
                gvPagos.DataSource = lista;
                gvPagos.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error al cargar listado: {ex.Message}", true);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //Carga inicial 
                CargarPagos();
            }

        }
        
        //Evento de paginacion en gvPagos
        protected void gvPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvPagos.PageIndex = e.NewPageIndex;//Numero de pagina a la cual el usuario quiere ir
                CargarPagos(); // Mantiene los filtros activos al cambiar de página
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error al cambiar página: {ex.Message}", true);
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            // Al hacer clic, simplemente recargamos usando los valores de los inputs
            CargarPagos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                // Reseteamos los controles visuales
                txtBuscar.Text = "";
                ddlEstadoFiltro.SelectedIndex = 0; // Vuelve a "-- Todos --"

                // Recargamos la lista limpia
                CargarPagos();
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error al limpiar filtros: {ex.Message}", true);
            }    
        }
        protected void gvPagos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Preguntamos si la fila que se está creando es la del Paginador (Footer)
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                if (e.Row.Cells.Count > 0 && e.Row.Cells[0].Controls.Count > 0 && e.Row.Cells[0].Controls[0] is Table)
                {
                    // 1. Obtenemos la tabla que contiene los números de página
                    Table pagerTable = (Table)e.Row.Cells[0].Controls[0];

                    // 2. Le aplicamos las clases de Bootstrap para centrar y estilizar
                    // 'pagination': Estilo base
                    // 'justify-content-center': Centrado horizontal
                    // 'mb-0': Sin margen inferior
                    pagerTable.Attributes.Add("class", "pagination justify-content-center mb-0");

                    // 3. Recorremos cada celda (que contiene los números 1, 2, etc.)
                    foreach (TableRow row in pagerTable.Rows)
                    {
                        foreach (TableCell cell in row.Cells)
                        {
                            // A cada celda le ponemos 'page-item'
                            cell.Attributes.Add("class", "page-item");

                            // 4. Estilizamos los enlaces (números clicables) y el texto (número actual)
                            if (cell.Controls.Count > 0)
                            {
                                if (cell.Controls[0] is LinkButton)
                                {
                                    // Es un botón clicable (Página 2, Siguiente, etc.)
                                    ((LinkButton)cell.Controls[0]).CssClass = "page-link";
                                }
                                else if (cell.Controls[0] is Label)
                                {
                                    // Es la página actual (ej: 1), la marcamos como activa
                                    ((Label)cell.Controls[0]).CssClass = "page-link";

                                    // Agregamos la clase 'active' al contenedor padre (el <li>)
                                    cell.CssClass += " active";
                                }
                            }
                        }
                    }
                }
            }
        }


        //Identifico que boton se presiono y sobre que fila
        protected void gvPagos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Evitamos conflictos con eventos de paginación
            if (e.CommandName == "Page") return;

            //Capturo el ID del registro seleccionado con el boton(Aprobar o rechazar pago)
            PagoNegocio negocio = new PagoNegocio();
            int idPago = Convert.ToInt32(e.CommandArgument);

            try
            {
                if (e.CommandName == "Aprobar")
                {
                    negocio.AprobarPago(idPago, "Aprobado por administrador");

                    MostrarMensajeGlobal($"Pago Aprobado", false);

                }
                else if (e.CommandName == "Rechazar")
                {
                    negocio.RechazarPago(idPago, "Comprobante inválido o ilegible");

                    MostrarMensajeGlobal($"Pago Rechazado", true);
                }

                //Recargo la grilla
                CargarPagos();
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error al procesar el pago: {ex.Message}", true);
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
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
                }
                else if (e.CommandName == "Rechazar")
                {
                    negocio.RechazarPago(idPago, "Comprobante inválido o ilegible");
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

            // Si usas UpdatePanel en el mensaje, asegúrate de llamarlo. 
            // Si no tienes el panel 'updMensajeGlobal', esta línea no es necesaria.
            // updMensajeGlobal.Update(); 
        }

    }
}
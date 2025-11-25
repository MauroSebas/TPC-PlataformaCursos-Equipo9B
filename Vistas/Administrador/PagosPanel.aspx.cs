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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPagos();
            }
        }

        private void CargarPagos()
        {
            try
            {
                PagoNegocio negocio = new PagoNegocio();
                string estadoFiltro = ddlEstadoFiltro.SelectedValue;
                if (estadoFiltro == "") estadoFiltro = null;

                string textoBusqueda = txtBuscar.Text.Trim();
                if (textoBusqueda == "") textoBusqueda = null;

                List<Pago> lista = negocio.FiltrarPagos(estadoFiltro, textoBusqueda);

                gvPagos.DataSource = lista;
                gvPagos.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error al cargar listado: {ex.Message}", true);
            }
        }

        // Eventos de Grilla
        protected void gvPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPagos.PageIndex = e.NewPageIndex;
            CargarPagos();
        }

        protected void gvPagos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Page") return;

            int idPago = Convert.ToInt32(e.CommandArgument);
            PagoNegocio negocio = new PagoNegocio();

            try
            {
                if (e.CommandName == "Aprobar")
                {
                    // APROBACIÓN DIRECTA (O podés poner modal también si querés)
                    negocio.AprobarPago(idPago, "Aprobado por administrador");
                    MostrarMensajeGlobal("Pago APROBADO exitosamente.", false);
                    CargarPagos();
                }
                else if (e.CommandName == "AbrirRechazo")
                {
                    // 1. Guardamos el ID en el HiddenField
                    hfPagoIdRechazo.Value = idPago.ToString();

                    // 2. Limpiamos el TextBox
                    txtObservacionRechazo.Text = string.Empty;

                    // 3. Abrimos el modal con JS
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "mostrarModalRechazo();", true);
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error: {ex.Message}", true);
            }
        }

        // EVENTO DEL MODAL (Confirmación Final)
        protected void btnConfirmarRechazo_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hfPagoIdRechazo.Value)) return;

                int idPago = int.Parse(hfPagoIdRechazo.Value);
                string motivo = txtObservacionRechazo.Text;

                PagoNegocio negocio = new PagoNegocio();
                negocio.RechazarPago(idPago, motivo);

                // Cerrar modal y recargar
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ClosePop", "cerrarModalRechazo();", true);

                MostrarMensajeGlobal("El pago ha sido RECHAZADO y el alumno notificado.", true); // true = Rojo (warning)
                CargarPagos();
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal("Error al rechazar: " + ex.Message, true);
            }
        }

        // Filtros
        protected void btnFiltrar_Click(object sender, EventArgs e) { CargarPagos(); }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            ddlEstadoFiltro.SelectedIndex = 0;
            CargarPagos();
        }

        // Helpers Visuales
        private void MostrarMensajeGlobal(string mensaje, bool esError = false)
        {
            pnlMensajeGlobal.Visible = true;
            litMensajeGlobal.Text = mensaje;
            pnlMensajeGlobal.CssClass = esError ? "alert alert-warning" : "alert alert-success"; // Warning queda mejor que Danger para rechazo
            updMensajeGlobal.Update();
        }

        public string ObtenerClaseBadge(string estado)
        {
            switch (estado)
            {
                case "Aprobado": return "text-bg-success";
                case "Rechazado": return "text-bg-danger";
                default: return "text-bg-warning";
            }
        }
    }

}

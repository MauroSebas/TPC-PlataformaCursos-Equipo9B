using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class MisPagos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] == null)
                {
                    Response.Redirect("~/Auth/Loguin.aspx");
                    return;
                }
                CargarPagos();
            }

        }

        private void CargarPagos()
        {
            try
            {
                Usuario u = (Usuario)Session["Usuario"];
                PagoNegocio pNeg = new PagoNegocio();
                List<Pago> lista = pNeg.ListarPorUsuario(u.UsuarioID);

                if (lista.Count > 0)
                {
                    repPagos.DataSource = lista;
                    repPagos.DataBind();
                    pnlSinPagos.Visible = false;
                }
                else
                {
                    pnlSinPagos.Visible = true;
                }
            }
            catch (Exception )
            {
               
            }
        }

        public string ObtenerBadgeEstado(string estado)
        {
            switch (estado)
            {
                case "Aprobado": return "badge bg-success bg-opacity-10 text-success px-3 py-2 rounded-pill";
                case "Pendiente": return "badge bg-warning bg-opacity-10 text-warning px-3 py-2 rounded-pill";
                case "Rechazado": return "badge bg-danger bg-opacity-10 text-danger px-3 py-2 rounded-pill";
                default: return "badge bg-secondary";
            }
        }

        // ============================================================
        // LÓGICA DE REINTENTO (MODAL)
        // ============================================================

      
        protected void repPagos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Reintentar")
            {
               
                string[] args = e.CommandArgument.ToString().Split('|');
                string idInscripcion = args[0];
                string observacion = args.Length > 1 ? args[1] : "Sin detalles.";

               
                hfIdInscripcionReintento.Value = idInscripcion;
                lblObservacionAdmin.Text = observacion;
                lblErrorModal.Text = ""; 

             
                pnlModalReintento.Visible = true;
            }
        }

       
        protected void btnGuardarReintento_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fuNuevoComprobante.HasFile)
                {
                    lblErrorModal.Text = "⚠️ Tenés que seleccionar un archivo nuevo.";
                    pnlModalReintento.Visible = true; 
                    return;
                }

               
                string carpeta = Server.MapPath("~/Assets/Comprobantes/");
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

                string extension = Path.GetExtension(fuNuevoComprobante.FileName);
                string nombreArchivo = $"Pago_Correccion_{Guid.NewGuid()}{extension}";
                string rutaCompleta = Path.Combine(carpeta, nombreArchivo);
                fuNuevoComprobante.SaveAs(rutaCompleta);

                string urlVirtual = "~/Assets/Comprobantes/" + nombreArchivo;

               
                int idInscripcion = int.Parse(hfIdInscripcionReintento.Value);
                PagoNegocio pNeg = new PagoNegocio();

               
                pNeg.ActualizarComprobante(idInscripcion, urlVirtual, "Transferencia", "Pendiente");

               
                pnlModalReintento.Visible = false;
                CargarPagos();
            }
            catch (Exception ex)
            {
                lblErrorModal.Text = "Error: " + ex.Message;
                pnlModalReintento.Visible = true;
            }
        }

        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            pnlModalReintento.Visible = false;
        }
    }
}

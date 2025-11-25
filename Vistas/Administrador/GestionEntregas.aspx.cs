using Negocio;
using Negocio.Cursada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Administrador
{
    public partial class GestionEntregas : System.Web.UI.Page
    {
        // Propiedades temporales en ViewState para saber qué estamos corrigiendo
        private int IdEntregaSeleccionada
        {
            get { return (int)(ViewState["IdEntrega"] ?? 0); }
            set { ViewState["IdEntrega"] = value; }
        }

        private int IdInscripcionSeleccionada
        {
            get { return (int)(ViewState["IdInscripcion"] ?? 0); }
            set { ViewState["IdInscripcion"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEntregas();
            }
        }

        private void CargarEntregas()
        {
            try
            {
                EntregaNegocio negocio = new EntregaNegocio();
                dgvEntregas.DataSource = negocio.ListarPendientes();
                dgvEntregas.DataBind();
            }
            catch (Exception)
            {
                // Manejo de error silencioso o log
            }
        }

        // Seleccionar alumno de la grilla
        protected void dgvEntregas_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Recuperamos los IDs guardados en DataKeyNames
            // Indice 0 = Id (Entrega), Indice 1 = InscripcionId
            int idEntrega = Convert.ToInt32(dgvEntregas.SelectedDataKey.Values[0]);
            int idInscripcion = Convert.ToInt32(dgvEntregas.SelectedDataKey.Values[1]);

            this.IdEntregaSeleccionada = idEntrega;
            this.IdInscripcionSeleccionada = idInscripcion;

            // Preparamos la UI
            pnlCorreccion.Visible = true;
            txtDevolucion.Text = ""; // Limpiar caja

            // Hacemos foco visual (opcional)
            txtDevolucion.Focus();
        }

        protected void btnAprobar_Click(object sender, EventArgs e)
        {
            ProcesarCorreccion(true);
        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            ProcesarCorreccion(false);
        }

        private void ProcesarCorreccion(bool aprobado)
        {
            try
            {
                if (this.IdEntregaSeleccionada == 0) return;

                // 1. Guardar Corrección (Feedback y Estado)
                EntregaNegocio entNeg = new EntregaNegocio();
                entNeg.CorregirEntrega(this.IdEntregaSeleccionada, aprobado, txtDevolucion.Text);

                // 2. Si aprobó -> Generar Certificado Automático
                if (aprobado)
                {
                    CertificadoNegocio certNeg = new CertificadoNegocio();
                    certNeg.GenerarCertificado(this.IdInscripcionSeleccionada);
                }

                // 3. Feedback visual y limpieza
                MostrarMensaje(aprobado ? "Entrega APROBADA y certificado generado." : "Entrega RECHAZADA correctamente.");

                pnlCorreccion.Visible = false;
                this.IdEntregaSeleccionada = 0;

                // Recargar lista
                CargarEntregas();
            }
            catch (Exception ex)
            {
                // Mostrar error si explota
                litMensaje.Text = "Error: " + ex.Message;
                pnlMensaje.CssClass = "alert alert-danger alert-dismissible fade show";
                pnlMensaje.Visible = true;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlCorreccion.Visible = false;
            this.IdEntregaSeleccionada = 0;
            dgvEntregas.SelectedIndex = -1;
        }

        private void MostrarMensaje(string texto)
        {
            litMensaje.Text = texto;
            pnlMensaje.CssClass = "alert alert-success alert-dismissible fade show";
            pnlMensaje.Visible = true;
        }
    }
}

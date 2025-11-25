using Dominio;
using Dominio.Cursada;
using Negocio;
using Negocio.Cursada;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Administrador // Asegurate que coincida con tu namespace (Aministrador o Administrador)
{
    public partial class GestionEntregas : System.Web.UI.Page
    {
        // Propiedades temporales
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

        // --- LISTADO Y FILTROS ---

        private void CargarEntregas()
        {
            try
            {
                EntregaNegocio negocio = new EntregaNegocio();
                string filtro = ddlFiltroEstado.SelectedValue;

                dgvEntregas.DataSource = negocio.ListarEntregas(filtro);
                dgvEntregas.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar: " + ex.Message, true);
            }
        }

        protected void ddlFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEntregas();
            pnlCorreccion.Visible = false;
            this.IdEntregaSeleccionada = 0;
        }

        // --- SELECCIÓN ---

        protected void dgvEntregas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idEntrega = Convert.ToInt32(dgvEntregas.SelectedDataKey.Values[0]);
            int idInscripcion = Convert.ToInt32(dgvEntregas.SelectedDataKey.Values[1]);

            this.IdEntregaSeleccionada = idEntrega;
            this.IdInscripcionSeleccionada = idInscripcion;

            EntregaNegocio negocio = new EntregaNegocio();
            Entrega ent = negocio.ObtenerUltimaEntrega(idInscripcion);

            if (ent != null)
            {
                txtDevolucion.Text = ent.DevolucionProfesor;

                if (ent.Estado == "Pendiente")
                    litTituloAccion.Text = "Nueva Corrección";
                else
                    litTituloAccion.Text = "Editar Corrección (" + ent.Estado + ")";
            }

            pnlCorreccion.Visible = true;
            pnlMensaje.Visible = false;
        }

        // --- ACCIONES ---

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

                string urlCertificado = "";

                // 1. SI APRUEBA -> VALIDAR Y GUARDAR ARCHIVO
                if (aprobado)
                {
                    if (!fuCertificado.HasFile)
                    {
                        MostrarMensaje("⚠️ Para APROBAR es obligatorio subir el certificado (PDF).", true);
                        return;
                    }

                    string ext = Path.GetExtension(fuCertificado.FileName).ToLower();
                    if (ext != ".pdf")
                    {
                        MostrarMensaje("⚠️ El certificado debe ser un archivo PDF.", true);
                        return;
                    }

                    string nombreArchivo = "Cert_" + this.IdInscripcionSeleccionada + "_" + DateTime.Now.Ticks + ".pdf";
                    string rutaVirtual = "~/Assets/Certificados/" + nombreArchivo;
                    string rutaFisica = Server.MapPath(rutaVirtual);

                    string directorio = Path.GetDirectoryName(rutaFisica);
                    if (!Directory.Exists(directorio)) Directory.CreateDirectory(directorio);

                    fuCertificado.SaveAs(rutaFisica);
                    urlCertificado = rutaVirtual;
                }

                // 2. GUARDAR CORRECCIÓN
                EntregaNegocio entNeg = new EntregaNegocio();
                entNeg.CorregirEntrega(this.IdEntregaSeleccionada, aprobado, txtDevolucion.Text);

                // 3. SI APROBÓ -> GENERAR CERTIFICADO
                if (aprobado)
                {
                    CertificadoNegocio certNeg = new CertificadoNegocio();
                    certNeg.GenerarCertificado(this.IdInscripcionSeleccionada, urlCertificado);
                }

                // 4. FIN
                MostrarMensaje(aprobado ? "✅ Entrega APROBADA y Certificado generado." : "❌ Entrega RECHAZADA correctamente.", false);

                pnlCorreccion.Visible = false;
                this.IdEntregaSeleccionada = 0;
                CargarEntregas();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlCorreccion.Visible = false;
            this.IdEntregaSeleccionada = 0;
            dgvEntregas.SelectedIndex = -1;
        }

        // --- HELPERS ---

        private void MostrarMensaje(string texto, bool esError)
        {
            litMensaje.Text = texto;
            pnlMensaje.CssClass = esError ? "alert alert-danger alert-dismissible fade show" : "alert alert-success alert-dismissible fade show";
            pnlMensaje.Visible = true;
        }

        public string ObtenerClaseBadge(string estado)
        {
            switch (estado)
            {
                case "Pendiente": return "text-bg-warning";
                case "Aprobado": return "text-bg-success";
                case "Rechazado": return "text-bg-danger";
                default: return "text-bg-secondary";
            }
        }
    }
}
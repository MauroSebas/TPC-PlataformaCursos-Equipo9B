using Negocio;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Vistas
{
    public partial class ProcesoPago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] == null)
                {
                    Response.Redirect("~/Auth/Loguin.aspx?returnUrl=" + Request.Url.PathAndQuery);
                    return;
                }

                string idStr = Request.QueryString["idCurso"];
                if (string.IsNullOrEmpty(idStr) || !int.TryParse(idStr, out int idCurso))
                {
                    Response.Redirect("~/Home.aspx");
                    return;
                }

                CargarDatosCurso(idCurso);
            }
        }

        private void CargarDatosCurso(int idCurso)
        {
            try
            {
                CursoNegocio cn = new CursoNegocio();
                Curso curso = cn.BuscarCurso(idCurso);

                if (curso != null)
                {
                    lblTituloCurso.Text = curso.Titulo;                   

                    string precio = curso.PrecioFormateado;
                    lblPrecioCurso.Text = precio;
                    lblSubtotal.Text = precio;
                    lblTotal.Text = precio;
                    lblMontoModal.Text = precio;
                }
            }
            catch { Response.Redirect("~/Home.aspx"); }
        }

        // --- EVENTOS DE MODALES ---

        protected void btnIniciarPago_Click(object sender, EventArgs e)
        {
            pnlModalPago.Visible = true; 
        }

        protected void btnCerrarModales_Click(object sender, EventArgs e)
        {
            pnlModalPago.Visible = false; 
            pnlModalExito.Visible = false;
            lblMensaje.Text = "";
        }

        protected void btnEnviarComprobante_Click(object sender, EventArgs e)
        {
           
            if (!fuComprobante.HasFile)
            {
                lblMensaje.Text = "⚠️ Debes seleccionar un archivo.";
                pnlModalPago.Visible = true;
                return;
            }

            try
            {
                int idCurso = int.Parse(Request.QueryString["idCurso"]);
                Usuario usuario = (Usuario)Session["Usuario"];

                
                string carpeta = Server.MapPath("~/Assets/Comprobantes/");
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

                string nombreArchivo = $"Pago_{Guid.NewGuid()}{Path.GetExtension(fuComprobante.FileName)}";
                string rutaCompleta = Path.Combine(carpeta, nombreArchivo);
                fuComprobante.SaveAs(rutaCompleta);

                string urlParaDB = "~/Assets/Comprobantes/" + nombreArchivo;

               
                CursoNegocio cn = new CursoNegocio();
                Curso curso = cn.BuscarCurso(idCurso);

                InscripcionNegocio inscNeg = new InscripcionNegocio();

               
                inscNeg.InscribirPago(usuario.UsuarioID,curso.Id,curso.Precio,urlParaDB);

               
                pnlModalPago.Visible = false;
                pnlModalExito.Visible = true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                pnlModalPago.Visible = true; 
            }
        }

        protected void btnEntendido_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Alumno/MisCursos.aspx");
        }
    }
}




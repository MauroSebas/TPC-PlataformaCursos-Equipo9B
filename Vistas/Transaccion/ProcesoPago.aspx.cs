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
        
        private List<Curso> ListaCursosCompra
        {
            get
            {
                if (Session["ListaCursosCompra"] != null)
                    return (List<Curso>)Session["ListaCursosCompra"];
                return new List<Curso>();
            }
            set { Session["ListaCursosCompra"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 1. Validar sesión
                if (Session["Usuario"] == null)
                {
                    Response.Redirect("~/Auth/Loguin.aspx?returnUrl=" + Request.Url.PathAndQuery);
                    return;
                }

                // 2. Cargar Carrito
                CargarCarrito();
            }
        }

        private void CargarCarrito()
        {
            try
            {
                CursoNegocio cn = new CursoNegocio();
                List<Curso> cursosAComprar = new List<Curso>();

                // Recuperamos los IDs del carrito (o creamos lista vacía)
                List<int> idsEnCarrito = Session["Carrito"] as List<int>;

                // Si venimos por URL directa (?idCurso=5), lo agregamos al carrito temporalmente
                string idUrl = Request.QueryString["idCurso"];
                if (!string.IsNullOrEmpty(idUrl) && int.TryParse(idUrl, out int idDirecto))
                {
                    if (idsEnCarrito == null) idsEnCarrito = new List<int>();
                    if (!idsEnCarrito.Contains(idDirecto)) idsEnCarrito.Add(idDirecto);
                    Session["Carrito"] = idsEnCarrito; // Actualizamos sesión
                }

                // Si sigue vacío, mostramos mensaje
                if (idsEnCarrito == null || idsEnCarrito.Count == 0)
                {
                    pnlCarritoVacio.Visible = true;
                    btnIniciarPago.Enabled = false;
                    return;
                }

                // Buscamos los objetos curso en la DB
                decimal total = 0;
                foreach (int id in idsEnCarrito)
                {
                    Curso c = cn.BuscarCurso(id);
                    if (c != null)
                    {
                        cursosAComprar.Add(c);
                        total += c.Precio;
                    }
                }

                // Guardamos la lista de objetos COMPLETA para usarla al momento de pagar
                this.ListaCursosCompra = cursosAComprar;

                // Renderizamos en el HTML
                repCarrito.DataSource = cursosAComprar;
                repCarrito.DataBind();

                litCantidadCursos.Text = cursosAComprar.Count.ToString();

                // Totales
                lblSubtotal.Text = total.ToString("C");
                lblTotal.Text = total.ToString("C");
                lblMontoModal.Text = total.ToString("C"); 
            }
            catch
            {
               
                Response.Redirect("~/Home.aspx");
            }
        }

        // --- EVENTOS MODALES ---
        protected void btnIniciarPago_Click(object sender, EventArgs e) {
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
                lblMensaje.Text = "⚠️ Debes subir el comprobante de la transferencia.";
                pnlModalPago.Visible = true; 
                return;
            }

            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                InscripcionNegocio inscNeg = new InscripcionNegocio();

                
                string carpeta = Server.MapPath("~/Assets/Comprobantes/");
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

                string extension = Path.GetExtension(fuComprobante.FileName);
                string nombreArchivo = $"Pago_Pack_{Guid.NewGuid()}{extension}";
                string rutaCompleta = Path.Combine(carpeta, nombreArchivo);
                fuComprobante.SaveAs(rutaCompleta);

                string urlParaDB = "~/Assets/Comprobantes/" + nombreArchivo;

                

                foreach (Curso curso in this.ListaCursosCompra)
                {
                    
                    if (inscNeg.ObtenerInscripcionActiva(usuario.UsuarioID, curso.Id) == null)
                    {
                        
                        inscNeg.InscribirPago(usuario.UsuarioID, curso.Id, curso.Precio, urlParaDB);
                    }
                }

                
                Session["Carrito"] = null;
                Session["ListaCursosCompra"] = null;

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
            Response.Redirect("~/Alumno/MisPagos.aspx");
        }
    }
}





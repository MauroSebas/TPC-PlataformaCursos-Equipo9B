using Negocio;
using Negocio.Contenido;
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
               
                if (Session["Usuario"] == null)
                {
                    Response.Redirect("~/Auth/Loguin.aspx?returnUrl=" + Request.Url.PathAndQuery);
                    return;
                }

               
                Usuario usuario = (Usuario)Session["Usuario"];
                if (usuario.Rol.NombreRol == "Administrador")
                {
                    Response.Redirect("~/Administrador/AdminPanel.aspx");
                    return;
                }

               
                CargarCarrito();
            }
        }

        private void CargarCarrito()
        {
            try
            {
                CursoNegocio cn = new CursoNegocio();
                InscripcionNegocio insNeg = new InscripcionNegocio();
                Usuario u = (Usuario)Session["Usuario"];

                List<Curso> cursosAComprar = new List<Curso>();

               
                List<int> idsEnCarrito = Session["Carrito"] as List<int>;

               
                string idUrl = Request.QueryString["idCurso"];
                if (!string.IsNullOrEmpty(idUrl) && int.TryParse(idUrl, out int idDirecto))
                {
                    if (idsEnCarrito == null) idsEnCarrito = new List<int>();
                    if (!idsEnCarrito.Contains(idDirecto)) idsEnCarrito.Add(idDirecto);
                    Session["Carrito"] = idsEnCarrito;
                }

                
                if (idsEnCarrito == null || idsEnCarrito.Count == 0)
                {
                    MostrarCarritoVacio();
                    return;
                }

               

                List<int> idsValidos = new List<int>();
                decimal total = 0;

                foreach (int id in idsEnCarrito)
                {
                    
                    var inscripcion = insNeg.ObtenerInscripcionActiva(u.UsuarioID, id);

                    if (inscripcion == null)
                    {
                       
                        Curso c = cn.BuscarCurso(id);
                        if (c != null && c.EstaActivo)
                        {
                            cursosAComprar.Add(c);
                            idsValidos.Add(id);
                            total += c.Precio;
                        }
                    }
                }

               
                Session["Carrito"] = idsValidos;

                
                if (this.Master is Site1 master) master.ActualizarContadorCarrito();

              
                if (cursosAComprar.Count == 0)
                {
                    MostrarCarritoVacio();
                    return;
                }

               
                this.ListaCursosCompra = cursosAComprar;

                
                repCarrito.DataSource = cursosAComprar;
                repCarrito.DataBind();

                litCantidadCursos.Text = cursosAComprar.Count.ToString();

                lblSubtotal.Text = total.ToString("C");
                lblTotal.Text = total.ToString("C");
                lblMontoModal.Text = total.ToString("C");

                pnlCarritoVacio.Visible = false;
                btnIniciarPago.Enabled = true;
            }
            catch
            {
                Response.Redirect("~/Home.aspx");
            }
        }

        private void MostrarCarritoVacio()
        {
            pnlCarritoVacio.Visible = true;
            btnIniciarPago.Enabled = false;
            repCarrito.DataSource = null;
            repCarrito.DataBind();
            lblTotal.Text = "$0.00";
            lblSubtotal.Text = "$0.00";
            litCantidadCursos.Text = "0";
        }

        // --- ELIMINAR ITEM DEL CARRITO ---
        protected void repCarrito_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int idCurso = Convert.ToInt32(e.CommandArgument);

                List<int> ids = (List<int>)Session["Carrito"];
                if (ids != null)
                {
                    ids.Remove(idCurso);
                    Session["Carrito"] = ids;
                }

                CargarCarrito(); 
            }
        }

        // --- EVENTOS MODALES ---
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
                lblMensaje.Text = "⚠️ Debes subir el comprobante de la transferencia.";
                pnlModalPago.Visible = true;
                return;
            }

            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                InscripcionNegocio inscNeg = new InscripcionNegocio();

                // Guardar Archivo
                string carpeta = Server.MapPath("~/Assets/Comprobantes/");
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

                string extension = Path.GetExtension(fuComprobante.FileName);
                string nombreArchivo = $"Pago_Pack_{Guid.NewGuid()}{extension}";
                string rutaCompleta = Path.Combine(carpeta, nombreArchivo);

                fuComprobante.SaveAs(rutaCompleta);
                string urlParaDB = "~/Assets/Comprobantes/" + nombreArchivo;

                // Recorrer Cursos y Generar Inscripciones
                foreach (Curso curso in this.ListaCursosCompra)
                {
                    // chequeo  seguridad
                    if (inscNeg.ObtenerInscripcionActiva(usuario.UsuarioID, curso.Id) == null)
                    {
                        inscNeg.InscribirPago(usuario.UsuarioID, curso.Id, curso.Precio, urlParaDB);
                    }
                }

                // Limpiar Carrito
                Session["Carrito"] = null;
                Session["ListaCursosCompra"] = null;

                if (this.Master is Site1 master) master.ActualizarContadorCarrito();

               
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
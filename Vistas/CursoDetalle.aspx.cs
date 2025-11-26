using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class CursoDetalle : System.Web.UI.Page
    {

        public int IdCursoSeleccionado
        {
            get { return ViewState["IdCurso"] != null ? (int)ViewState["IdCurso"] : 0; }
            set { ViewState["IdCurso"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idStr = Request.QueryString["id"];
                if (string.IsNullOrEmpty(idStr) || !int.TryParse(idStr, out int id))
                {
                    Response.Redirect("Home.aspx");
                    return;
                }

                this.IdCursoSeleccionado = id;
                CargarDatosDelCurso(id);
              
            }
        }

        private void CargarDatosDelCurso(int id)
        {
            try
            {
                CursoNegocio negocio = new CursoNegocio();
                Curso seleccionado = negocio.BuscarCurso(id);

                if (seleccionado == null) { Response.Redirect("Home.aspx"); return; }

                // Mapeo visual
                lblTitulo.Text = seleccionado.Titulo;
                lblDescripcion.Text = seleccionado.Descripcion;

                // Duración
                lblDuracion.Text = (seleccionado.DuracionAccesoDias == 0)
                    ? "Acceso ilimitado"
                    : $"Acceso por {seleccionado.DuracionAccesoDias} días";

                lblNivel.Text = seleccionado.NivelDificultad;
                lblIdioma.Text = seleccionado.Idioma;
                liCertificado.Visible = seleccionado.ConCertificado;
                imgSidebar.ImageUrl = string.IsNullOrEmpty(seleccionado.UrlImagenPortada) ? ResolveUrl("~/Assets/img/placeholder-curso.jpg") : seleccionado.UrlImagenPortada;

               
                if (seleccionado.Precio > 0)
                {
                    lblPrecio.Text = seleccionado.PrecioFormateado;
                    phCursoPago.Visible = true;
                    phCursoGratis.Visible = false;
                }
                else
                {
                    lblPrecio.Text = "GRATIS";
                    lblPrecio.CssClass += " text-success";
                    phCursoPago.Visible = false;
                    phCursoGratis.Visible = true;
                }

                
                if (seleccionado.Objetivos != null && seleccionado.Objetivos.Count > 0)
                {
                    repObjetivos.DataSource = seleccionado.Objetivos;
                    repObjetivos.DataBind();
                }
                else
                {
                    lblSinObjetivos.Visible = true;
                }
            }
            catch (Exception ) { }
        }

        protected void btnComprar_Click(object sender, EventArgs e)
        {
            if (!ValidarSesion()) return;

           
            AgregarAlCarrito(this.IdCursoSeleccionado);

            
            Response.Redirect("~/Transaccion/ProcesoPago.aspx");
        }

        private void AgregarAlCarrito(int idCurso)
        {
            List<int> carrito;

            if (Session["Carrito"] == null)
            {
                carrito = new List<int>();
            }
            else
            {
                carrito = (List<int>)Session["Carrito"];
            }

           
            if (!carrito.Contains(idCurso))
            {
                carrito.Add(idCurso);
                Session["Carrito"] = carrito;
            }
        }
        private void ConfigurarBotones(int idCurso)
        {
            if (Session["Usuario"] != null)
            {
                Usuario u = (Usuario)Session["Usuario"];
                InscripcionNegocio insNeg = new InscripcionNegocio();

                // Tu SP ObtenerInscripcionActiva ya trae las pendientes y aprobadas
                var inscripcion = insNeg.ObtenerInscripcionActiva(u.UsuarioID, idCurso);

                if (inscripcion != null)
                {
                    // Bloqueamos todo
                    btnAgregarCarrito.Enabled = false;
                    btnComprar.Enabled = false;
                    btnInscribirse.Enabled = false;

                    if (inscripcion.Estado == "Pendiente")
                    {
                        btnAgregarCarrito.Text = "⏳ Pago en Revisión";
                        btnAgregarCarrito.CssClass = "btn btn-warning btn-lg disabled";
                    }
                    else
                    {
                        btnAgregarCarrito.Text = "✅ Ya tienes este curso";
                        btnAgregarCarrito.CssClass = "btn btn-secondary btn-lg disabled";
                    }
                }
            }
        }

        protected void btnInscribirse_Click(object sender, EventArgs e)
        {
            if (!ValidarSesion()) return;
            int idCurso = this.IdCursoSeleccionado;
            try
            {
               
                Usuario usuario = (Usuario)Session["usuario"];
                InscripcionNegocio negocio = new InscripcionNegocio();

                var inscripcion = negocio.ObtenerInscripcionActiva(usuario.UsuarioID, idCurso);

                if (inscripcion != null)
                {
                    pnlAlertaYaComprado.Visible = true;
                    return;
                }
                negocio.InscribirGratuito(usuario.UsuarioID, this.IdCursoSeleccionado);

               
                Response.Redirect("~/Alumno/MisCursos.aspx?msg=exito");
            }
            catch (Exception) { }
        }


        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            if (!ValidarSesion()) return;

           
            AgregarAlCarrito(this.IdCursoSeleccionado);

            if (this.Master is Site1 master)
            {
                master.ActualizarContadorCarrito();
            }

           
            btnAgregarCarrito.Text = "¡Agregado!";
            btnAgregarCarrito.Enabled = false;
            btnAgregarCarrito.CssClass = "btn btn-success btn-lg";
        }

        protected void btnVolverAHome_Click(object sender, EventArgs e) {
            Response.Redirect("Home.aspx");
        }
        protected void btnVolverAMisCursos_Click(object sender, EventArgs e) { 
            Response.Redirect("~/Alumno/MisCursos.aspx"); 
        }

        private bool ValidarSesion()
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Auth/Loguin.aspx?ReturnUrl=" + Server.UrlEncode(Request.Url.PathAndQuery));
                return false;
            }
            return true;
        }
    }
}

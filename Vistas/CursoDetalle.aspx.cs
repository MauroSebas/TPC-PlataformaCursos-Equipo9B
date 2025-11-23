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

                // Lógica Botones
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

                // Objetivos
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
            catch (Exception ex) { /* Log error */ }
        }

        // ================================
        // BOTÓN COMPRAR (El que querés probar)
        // ================================
        protected void btnComprar_Click(object sender, EventArgs e)
        {
            // 1. Validar Sesión
            if (!ValidarSesion()) return;

            int idCurso = this.IdCursoSeleccionado;

            // 2. Validar si ya compró (DESCOMENTADO y CORREGIDO)
            Usuario usuario = (Usuario)Session["usuario"];
            InscripcionNegocio negocio = new InscripcionNegocio();

            // Usamos el método correcto: ObtenerInscripcionActiva
            var inscripcion = negocio.ObtenerInscripcionActiva(usuario.UsuarioID, idCurso);

            if (inscripcion != null)
            {
                pnlAlertaYaComprado.Visible = true;
                return;
            }

            // 3. Redirigir al proceso de pago (Todo OK)
            Response.Redirect($"~/Transaccion/ProcesoPago.aspx?idCurso={idCurso}", false);
        }

        // ================================
        // BOTÓN INSCRIBIRSE (GRATIS)
        // ================================
        protected void btnInscribirse_Click(object sender, EventArgs e)
        {
            if (!ValidarSesion()) return;

            try
            {
                // ACÁ SÍ LLAMAMOS A LA LÓGICA DE INSCRIPCIÓN GRATUITA
                Usuario usuario = (Usuario)Session["usuario"];
                InscripcionNegocio negocio = new InscripcionNegocio();

                negocio.InscribirGratuito(usuario.UsuarioID, this.IdCursoSeleccionado);

                // Éxito -> Mis Cursos
                Response.Redirect("~/Alumno/MisCursos.aspx?msg=exito");
            }
            catch (Exception) { /* Manejar error */ }
        }

        // Otros botones
        protected void btnAgregarCarrito_Click(object sender, EventArgs e) { if (ValidarSesion()) Response.Redirect("Carrito.aspx"); }
        protected void btnVolverAHome_Click(object sender, EventArgs e) { Response.Redirect("Home.aspx"); }
        protected void btnVolverAMisCursos_Click(object sender, EventArgs e) { Response.Redirect("~/Alumno/MisCursos.aspx"); }

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

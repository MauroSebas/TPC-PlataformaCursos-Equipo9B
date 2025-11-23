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
                // 1. Validar ID en URL
                string idStr = Request.QueryString["id"];

                if (string.IsNullOrEmpty(idStr) || !int.TryParse(idStr, out int id))
                {
                    Response.Redirect("Home.aspx");
                    return;
                }

                // Guardamos el ID para usarlo en los botones
                this.IdCursoSeleccionado = id;

                // 2. Cargar datos
                CargarDatosDelCurso(id);
            }
        }

        private void CargarDatosDelCurso(int id)
        {
            CursoNegocio negocio = new CursoNegocio();
            try
            {
                Curso seleccionado = negocio.BuscarCurso(id);

                if (seleccionado == null)
                {
                    Response.Redirect("Error.aspx?mensaje=CursoNoEncontrado");
                    return;
                }

                // --- MAPEO VISUAL ---
                lblTitulo.Text = seleccionado.Titulo;
                lblDescripcion.Text = seleccionado.Descripcion;

                // Info Sidebar
                // --- LÓGICA DE DURACIÓN (NUEVO) ---
                if (seleccionado.DuracionAccesoDias == 0)
                {
                    // Si es 0, mostramos el texto bonito
                    lblDuracion.Text = "Acceso ilimitado";
                    // Opcional: Podés darle un colorcito verde para destacar
                    // lblDuracion.CssClass = "text-success fw-medium"; 
                }
                else
                {
                    // Si tiene días, armamos la frase completa
                    lblDuracion.Text = $"Acceso por {seleccionado.DuracionAccesoDias} días";
                }
                // ------------------------------------

                lblNivel.Text = seleccionado.NivelDificultad;
                lblNivel.Text = seleccionado.NivelDificultad;
                lblIdioma.Text = seleccionado.Idioma;
                liCertificado.Visible = seleccionado.ConCertificado;

                // Imagen
                string urlImagen = string.IsNullOrEmpty(seleccionado.UrlImagenPortada)
                                   ? "https://via.placeholder.com/800x400?text=Sin+Imagen"
                                   : seleccionado.UrlImagenPortada;
                imgSidebar.ImageUrl = urlImagen;
                

                // --- LÓGICA DE PRECIO Y BOTONES (NUEVO) ---
                if (seleccionado.Precio > 0)
                {
                    // CURSO PAGO
                    lblPrecio.Text = seleccionado.PrecioFormateado;
                    phCursoPago.Visible = true;   // Mostramos Carrito y Comprar
                    phCursoGratis.Visible = false; // Ocultamos Inscripción Directa
                }
                else
                {
                    // CURSO GRATUITO
                    lblPrecio.Text = "Gratis";
                    lblPrecio.CssClass += " text-success"; // Color verde
                    phCursoPago.Visible = false;
                    phCursoGratis.Visible = true; // Mostramos Botón "Inscribirse Gratis"
                }

                // --- CARGA DE OBJETIVOS ---
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
            catch (Exception ex)
            {
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx");
            }
        }

        // --- BOTONES DE ACCIÓN ---

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            if (!ValidarSesion()) return;

            // Lógica futura: CarritoNegocio.Agregar(...)
            Response.Redirect("Carrito.aspx");
        }

       // ================================
// BOTÓN COMPRAR
// ================================
protected void btnComprar_Click(object sender, EventArgs e)
{
    // Validar sesión
    if (Session["usuario"] == null)
    {
        Response.Redirect("~/Auth/Loguin.aspx?ReturnUrl=" + Server.UrlEncode(Request.Url.PathAndQuery));
        return;
    }

    // Obtener ID del curso actual
    int idCurso = this.IdCursoSeleccionado;
    if (idCurso <= 0)
    {
        Response.Redirect("Home.aspx");
        return;
    }

    // Validar si el usuario ya está inscripto
    Usuario usuario = (Usuario)Session["usuario"];
    InscripcionNegocio negocio = new InscripcionNegocio();

    var inscripcion = negocio.ObtenerInscripcion(usuario.UsuarioID, idCurso);
    if (inscripcion != null)
    {
        pnlAlertaYaComprado.Visible = true;
        return;
    }

    // Redirigir al proceso de pago
    Response.Redirect("~/Transaccion/ProcesoPago.aspx?idCurso=" + idCurso, false);
}


// ================================
// BOTONES DEL MODAL
// ================================
protected void btnVolverAHome_Click(object sender, EventArgs e)
{
    Response.Redirect("Home.aspx");
}

protected void btnVolverAMisCursos_Click(object sender, EventArgs e)
{
    Response.Redirect("~/Alumno/MisCursos.aspx");
}


// ================================
// BOTÓN INSCRIBIRSE (CURSO GRATIS)
// ================================
protected void btnInscribirse_Click(object sender, EventArgs e)
{
    if (!ValidarSesion()) return;

    try
    {
        // TODO: registrar inscripción del curso gratuito
        Response.Redirect("~/Alumno/MisCursos.aspx?msg=exito");
    }
    catch (Exception ex)
    {
        // Manejar error
    }
}


// ================================
// VALIDAR SESIÓN
// ================================
private bool ValidarSesion()
{
    if (Session["usuario"] == null)
    {
        string urlActual = Request.Url.PathAndQuery;
        Response.Redirect("~/Auth/Loguin.aspx?ReturnUrl=" + Server.UrlEncode(urlActual));
        return false;
    }
    return true;
}

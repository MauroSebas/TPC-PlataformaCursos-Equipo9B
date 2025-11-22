using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class CursoDetalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 1. Validar que venga un ID en la URL
                string idStr = Request.QueryString["id"];

                if (string.IsNullOrEmpty(idStr) || !int.TryParse(idStr, out int id))
                {
                    Response.Redirect("Home.aspx"); // Si el ID está mal, volvemos al inicio
                    return;
                }

                // 2. Cargar el curso
                CargarDatosDelCurso(id);
            }
        }

        private void CargarDatosDelCurso(int id)
        {
            CursoNegocio negocio = new CursoNegocio();
            try
            {
                // Este método ahora trae la info básica + la lista de objetivos
                Curso seleccionado = negocio.BuscarCurso(id);

                if (seleccionado == null)
                {
                    Response.Redirect("Error.aspx?mensaje=CursoNoEncontrado");
                    return;
                }

                // 3. Mapear datos a la Vista
                lblTitulo.Text = seleccionado.Titulo;
                lblDescripcion.Text = seleccionado.Descripcion;
                lblPrecio.Text = seleccionado.PrecioFormateado; // Usamos tu propiedad formateada

                // Sidebar info
                lblDuracion.Text = seleccionado.DuracionAccesoDias.ToString();
                lblNivel.Text = seleccionado.NivelDificultad;
                lblIdioma.Text = seleccionado.Idioma;

                // Lógica de visualización del Certificado
                // Al ser un control HTML con runat="server", usamos la propiedad Visible
                liCertificado.Visible = seleccionado.ConCertificado;

                // Imágenes
                // Si no tiene imagen, podríamos poner una por defecto
                string urlImagen = string.IsNullOrEmpty(seleccionado.UrlImagenPortada)
                                   ? "https://via.placeholder.com/800x400?text=Sin+Imagen"
                                   : seleccionado.UrlImagenPortada;

                // Asignar al panel de fondo (Header)
                //pnlImagenPortada.Style["background-image"] = $"url('{urlImagen}')";
                // Asignar a la imagen chica del sidebar
                imgSidebar.ImageUrl = urlImagen;


                // 4. Cargar la lista "Lo que aprenderás" (Repeater)
                if (seleccionado.Objetivos != null && seleccionado.Objetivos.Count > 0)
                {
                    repObjetivos.DataSource = seleccionado.Objetivos;
                    repObjetivos.DataBind();
                }
                else
                {
                    // Si no tiene objetivos cargados, mostramos mensaje
                    lblSinObjetivos.Visible = true;
                }

            }
            catch (Exception ex)
            {
                // Manejo básico de errores, idealmente loguear el error
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            // Acá iría tu lógica de carrito más adelante
            // Ejemplo: CarritoNegocio.Agregar(idCurso);
            Response.Redirect("Carrito.aspx");
        }
    }
}
using Dominio;
using Dominio.Enums;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Aministrador
{
    public partial class CursoForm : System.Web.UI.Page
    {
        private readonly CursoNegocio _cursoNegocio = new CursoNegocio();
        private readonly CategoriaNegocio _catNegocio = new CategoriaNegocio();
        private Curso cursoActual;

        protected void Page_Load(object sender, EventArgs e)
        {
            pnlMensajeGlobal.Visible = false;

            if (!IsPostBack)
            {
                CargarCategorias();
                CargarModalidadPago();

                // Si hay ID en QueryString, es EDICIÓN
                if (Request.QueryString["id"] != null)
                {
                    int idCurso = int.Parse(Request.QueryString["id"]);
                    CargarCurso(idCurso);
                }
            }
            // Mantenemos el curso en ViewState para que esté disponible en PostBacks (e.g. al guardar)
            cursoActual = ViewState["CursoActual"] as Curso;
        }

        private void CargarCategorias()
        {
            try
            {
                ddlCategoria.DataSource = _catNegocio.listarCategoria();
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("-- Seleccione Categoría --", "0"));
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error al cargar categorías: {ex.Message}", true);
            }
        }

        // Carga el DropDownList usando el Enum y el Helper GetDescription
        private void CargarModalidadPago()
        {
            try
            {
                ddlModalidadPago.DataSource = Enum.GetValues(typeof(ModalidadPagoEnum))
                    .Cast<ModalidadPagoEnum>()
                    .Select(e => new ListItem
                    {
                        Text = e.GetDescription(), 
                        Value = e.GetDescription()  
                    }).ToList();

                ddlModalidadPago.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error al cargar modalidades de pago: {ex.Message}", true);
            }
        }


        private void CargarCurso(int idCurso)
        {
            try
            {
                
                cursoActual = _cursoNegocio.BuscarCurso(idCurso);

                if (cursoActual != null)
                {
                   
                    litTituloPagina.Text = $"Editar Curso: {cursoActual.Titulo}";
                    txtTitulo.Text = cursoActual.Titulo;
                    txtDescripcion.Text = cursoActual.Descripcion;
                    txtPrecio.Text = cursoActual.Precio.ToString("0.00");
                    txtDuracionDias.Text = cursoActual.DuracionAccesoDias.ToString();
                   

                   
                    ddlModalidadPago.SelectedValue = cursoActual.ModalidadPago;

                    
                    ddlCategoria.SelectedValue = cursoActual.Categoria.Id.ToString();

                    
                    if (!string.IsNullOrEmpty(cursoActual.UrlImagenPortada))
                    {
                        imgPortadaActual.ImageUrl = ResolveUrl(cursoActual.UrlImagenPortada);
                        imgPortadaActual.Visible = true;
                    }

                    ViewState["CursoActual"] = cursoActual; 
                }
                else
                {
                    MostrarMensajeGlobal("El curso solicitado no existe.", true);
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error al cargar datos del curso: {ex.Message}", true);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Page.Validate("Curso");
            if (!Page.IsValid) return;

            try
            {
                // Si cursoActual es null, crea uno nuevo. Si no, usa el existente (modo Edición).
                Curso cursoAGuardar = cursoActual ?? new Curso();

                // 1. Mapeo de datos del formulario
                cursoAGuardar.Titulo = txtTitulo.Text.Trim();
                cursoAGuardar.Descripcion = txtDescripcion.Text.Trim();
                cursoAGuardar.Precio = decimal.Parse(txtPrecio.Text);
                cursoAGuardar.DuracionAccesoDias = int.Parse(txtDuracionDias.Text);
               

                // Mapeamos el string del DropDownList (que es el valor de la Description del Enum)
                cursoAGuardar.ModalidadPago = ddlModalidadPago.SelectedValue;

                // Mapeamos la Categoría
                cursoAGuardar.Categoria = new Categoria { Id = int.Parse(ddlCategoria.SelectedValue) };

                // Estos valores deberían manejarse en la lógica, pero los seteamos por si son nuevos
                if (cursoAGuardar.Id == 0)
                    cursoAGuardar.EstaActivo = true;

                // 2. Manejo de la imagen
                string nombreArchivoImagen = null;
                string extension = null;

                if (fileUploadPortada.HasFile)
                {
                    extension = Path.GetExtension(fileUploadPortada.FileName).ToLower();
                    if (extension != ".jpg" && extension != ".png" && extension != ".jpeg")
                    {
                        MostrarMensajeGlobal("Solo se permiten archivos JPG, JPEG o PNG.", true);
                        return;
                    }

                    // Si es nuevo curso, usamos GUID temporal. Si es edición, usamos el ID actual.
                    nombreArchivoImagen = cursoAGuardar.Id != 0
                                          ? $"curso-{cursoAGuardar.Id}{extension}"
                                          : $"temp-{Guid.NewGuid()}{extension}";

                    string rutaVirtual = $"~/Assets/Cursos/{nombreArchivoImagen}";
                    string rutaFisica = Server.MapPath(rutaVirtual);

                    fileUploadPortada.SaveAs(rutaFisica);
                    cursoAGuardar.UrlImagenPortada = rutaVirtual;
                }

                // 3. Persistencia en la capa de Negocio
                string mensaje = "";
                int idCursoGuardado = _cursoNegocio.GuardarCurso(cursoAGuardar);

                if (cursoAGuardar.Id == 0) // Fue un ALTA
                {
                    mensaje = $"¡Curso '{cursoAGuardar.Titulo}' creado con éxito!";

                    // Si usamos nombre temporal (ALTA) y se creó, renombramos el archivo con el nuevo ID
                    if (cursoAGuardar.UrlImagenPortada != null && cursoAGuardar.UrlImagenPortada.Contains("temp-"))
                    {
                        string oldRuta = Server.MapPath(cursoAGuardar.UrlImagenPortada);
                        string newRutaVirtual = $"~/Assets/Cursos/curso-{idCursoGuardado}{extension}";
                        string newRutaFisica = Server.MapPath(newRutaVirtual);

                        // Solo renombramos si el archivo existe (por si falla la carga)
                        if (File.Exists(oldRuta))
                        {
                            File.Move(oldRuta, newRutaFisica);
                            // ASUMIMOS QUE EN NEGOCIO TIENES UN MÉTODO PARA ACTUALIZAR SOLO LA RUTA EN LA DB
                            // _cursoNegocio.ActualizarRutaImagen(idCursoGuardado, newRutaVirtual); 
                        }
                    }
                }
                else // Fue una MODIFICACIÓN
                {
                    mensaje = $"¡Curso '{cursoAGuardar.Titulo}' modificado con éxito!";
                }

                Session["CursoPanelMensaje"] = mensaje;
                // Redirigimos al panel principal después de guardar
                Response.Redirect("~/Administrador/CursoPanel.aspx", false);

            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal($"Error al guardar el curso: {ex.Message}", true);
            }
        }

        // --- HELPER DE MENSAJES ---
        private void MostrarMensajeGlobal(string mensaje, bool esError = false)
        {
            pnlMensajeGlobal.Visible = true;
            litMensajeGlobal.Text = mensaje;
            pnlMensajeGlobal.CssClass = esError ? "alert alert-danger" : "alert alert-success";
            updMensajeGlobal.Update();
        }
    }
}
        

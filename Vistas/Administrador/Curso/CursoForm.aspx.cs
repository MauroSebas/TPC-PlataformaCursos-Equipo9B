using Dominio;
using Dominio.Enums;
using Negocio;
using Negocio.Contenido;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Aministrador
{
    public partial class CursoForm : System.Web.UI.Page
    {
        private readonly CursoNegocio _cursoNegocio = new CursoNegocio();
        private readonly CategoriaNegocio _catNegocio = new CategoriaNegocio();

        // Propiedad para manejar el ID en ViewState
        private int CursoIdEnEdicion
        {
            get
            {
                if (ViewState["CursoId"] != null)
                    return (int)ViewState["CursoId"];
                return 0;
            }
            set { ViewState["CursoId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            pnlMensajeGlobal.Visible = false;

            if (!IsPostBack)
            {
                CargarListasDesplegables();

                // Verificar si es Edición
                string idStr = Request.QueryString["id"];
                if (idStr != null)
                {
                    int idCurso = 0;
                    if (int.TryParse(idStr, out idCurso))
                    {
                        CargarDatosCurso(idCurso);
                    }
                }
                else
                {
                    // Si es Alta, inicializamos la UI por defecto
                    rbTipoImagen_CheckedChanged(null, null);
                    ddlModalidadPago_SelectedIndexChanged(null, null);
                    pnlObjetivos.Visible = false;
                }
            }
        }

        private void CargarListasDesplegables()
        {
            try
            {
                // 1. Categorías
                ddlCategoria.DataSource = _catNegocio.listarCategoria();
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("-- Seleccione Categoría --", "0"));

                // 2. Modalidad Pago (Sin LINQ)
                ddlModalidadPago.Items.Clear();
                Array valoresEnum = Enum.GetValues(typeof(ModalidadPagoEnum));

                foreach (ModalidadPagoEnum modalidad in valoresEnum)
                {
                    ListItem item = new ListItem();
                    item.Text = modalidad.GetDescription();
                    item.Value = modalidad.GetDescription();
                    ddlModalidadPago.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal("Error al cargar listas: " + ex.Message, true);
            }
        }

        private void CargarDatosCurso(int idCurso)
        {
            try
            {
                Curso curso = _cursoNegocio.BuscarCurso(idCurso);
                if (curso == null)
                {
                    MostrarMensajeGlobal("El curso no existe.", true);
                    return;
                }

                this.CursoIdEnEdicion = curso.Id;
                litTituloPagina.Text = "Editar Curso: " + curso.Titulo;

                // Mapeo simple
                txtTitulo.Text = curso.Titulo;
                txtDescripcion.Text = curso.Descripcion;
                txtPrecio.Text = curso.Precio.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                txtDuracionDias.Text = curso.DuracionAccesoDias.ToString();
                ddlNivel.SelectedValue = curso.NivelDificultad;
                ddlIdioma.SelectedValue = curso.Idioma;
                chkCertificadoHTML.Checked = curso.ConCertificado;
                // Habilitar el Panel de Objetivos si estamos editando
                pnlObjetivos.Visible = true;
                CargarGrillaObjetivos(idCurso);
                // DropDowns
                if (ddlCategoria.Items.FindByValue(curso.Categoria.Id.ToString()) != null)
                    ddlCategoria.SelectedValue = curso.Categoria.Id.ToString();

                if (ddlModalidadPago.Items.FindByValue(curso.ModalidadPago) != null)
                    ddlModalidadPago.SelectedValue = curso.ModalidadPago;

                // Imagen
                if (!string.IsNullOrEmpty(curso.UrlImagenPortada))
                {
                    imgPortadaActual.ImageUrl = ResolveUrl(curso.UrlImagenPortada);
                    imgPortadaActual.Visible = true;
                    ViewState["UrlImagenActual"] = curso.UrlImagenPortada;

                    // Si es URL externa (empieza con http)
                    if (curso.UrlImagenPortada.StartsWith("http"))
                    {
                        rbImagenUrl.Checked = true;
                        rbImagenArchivo.Checked = false;
                        txtUrlImagen.Text = curso.UrlImagenPortada;
                    }
                    else // Es archivo local
                    {
                        rbImagenArchivo.Checked = true;
                        rbImagenUrl.Checked = false;
                    }
                }

                // Disparar eventos de UI para acomodar los campos visualmente
                ddlModalidadPago_SelectedIndexChanged(null, null);
                rbTipoImagen_CheckedChanged(null, null);
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal("Error al cargar el curso: " + ex.Message, true);
            }
        }

        // --- EVENTOS DE UI ---

        // 1. Bloquea precio si es Gratuito
        protected void ddlModalidadPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool esGratuito = (ddlModalidadPago.SelectedValue == "Gratuito");
            txtPrecio.Enabled = !esGratuito;

            if (esGratuito)
            {
                txtPrecio.Text = "0.00";
            }
        }

        // 2. Switch entre Archivo y URL
        protected void rbTipoImagen_CheckedChanged(object sender, EventArgs e)
        {
            if (rbImagenArchivo.Checked)
            {
                fileUploadPortada.Enabled = true;
                txtUrlImagen.Enabled = false;
                txtUrlImagen.Text = string.Empty;
            }
            else
            {
                fileUploadPortada.Enabled = false;
                txtUrlImagen.Enabled = true;
            }
        }

        // --- GUARDADO ---

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                Curso curso = new Curso();
                curso.Id = this.CursoIdEnEdicion;

                curso.Titulo = txtTitulo.Text.Trim();
                curso.Descripcion = txtDescripcion.Text.Trim();

                // Lógica Precio Gratuito vs Pago
                if (ddlModalidadPago.SelectedValue == "Gratuito")
                {
                    curso.Precio = 0; // Forzamos 0 en el backend
                }
                else
                {
                    string precioTexto = txtPrecio.Text.Replace(",", ".");
                    decimal precioFinal = 0;
                    if (decimal.TryParse(precioTexto, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out precioFinal))
                        curso.Precio = precioFinal;
                }

                // Duración
                int dias = 0;
                if (int.TryParse(txtDuracionDias.Text, out dias))
                    curso.DuracionAccesoDias = dias;

                // DropDowns
                curso.Categoria = new Categoria();
                curso.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);
                curso.ModalidadPago = ddlModalidadPago.SelectedValue;

                // --- AGREGAR ESTE MAPEO ---
                curso.NivelDificultad = ddlNivel.SelectedValue;
                curso.Idioma = ddlIdioma.SelectedValue;
                curso.ConCertificado = chkCertificadoHTML.Checked;

                // Estados
                curso.EstaActivo = true;

                if (curso.Id == 0)
                {
                    curso.Publicado = false; // Alta nace despublicado
                }
                else
                {
                    // Recuperamos estado original
                    Curso original = _cursoNegocio.BuscarCurso(curso.Id);
                    if (original != null) curso.Publicado = original.Publicado;
                    else curso.Publicado = false;
                }

                // Imagen
                string urlImagen = ManejarImagen(curso.Id);
                curso.UrlImagenPortada = urlImagen;

                // Guardar en BD
                int idGuardado = _cursoNegocio.GuardarCurso(curso);

                // Renombrar si era nuevo y archivo temporal
                if (curso.Id == 0 && !string.IsNullOrEmpty(urlImagen) && urlImagen.Contains("temp"))
                {
                    RenombrarImagenDefinitiva(urlImagen, idGuardado);
                }

                if (this.CursoIdEnEdicion == 0) // Si era nuevo
                {
                    // Redirigimos A LA MISMA PAGINA con el ID para habilitar objetivos
                    Response.Redirect("~/Administrador/Curso/CursoForm.aspx?id=" + idGuardado, false);
                }
                else
                {
                    // Si ya existía, volvemos al panel
                    Session["CursoPanelMensaje"] = "¡Curso guardado con éxito!";
                    Response.Redirect("~/Administrador/Curso/CursoPanel.aspx", false);
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal("Error al guardar: " + ex.Message, true);
            }
        }

        private string ManejarImagen(int cursoId)
        {
            // A. Eligió URL
            if (rbImagenUrl.Checked)
            {
                if (!string.IsNullOrWhiteSpace(txtUrlImagen.Text))
                    return txtUrlImagen.Text.Trim();
            }
            // B. Eligió Archivo
            else if (rbImagenArchivo.Checked && fileUploadPortada.HasFile)
            {
                string extension = Path.GetExtension(fileUploadPortada.FileName).ToLower();
                string nombreArchivo;

                if (cursoId == 0) nombreArchivo = "temp-" + Guid.NewGuid().ToString() + extension;
                else nombreArchivo = "curso-" + cursoId.ToString() + extension;

                string rutaVirtual = "~/Assets/Cursos/" + nombreArchivo;
                string rutaFisica = Server.MapPath(rutaVirtual);

                string directorio = Path.GetDirectoryName(rutaFisica);
                if (!Directory.Exists(directorio)) Directory.CreateDirectory(directorio);

                fileUploadPortada.SaveAs(rutaFisica);
                return rutaVirtual;
            }

            // C. Mantener existente (Edición)
            if (cursoId > 0 && ViewState["UrlImagenActual"] != null)
            {
                return ViewState["UrlImagenActual"].ToString();
            }

            return null;
        }

        private void RenombrarImagenDefinitiva(string urlTemp, int nuevoId)
        {
            try
            {
                string rutaFisicaTemp = Server.MapPath(urlTemp);
                if (File.Exists(rutaFisicaTemp))
                {
                    string extension = Path.GetExtension(rutaFisicaTemp);
                    string nuevaRutaVirtual = "~/Assets/Cursos/curso-" + nuevoId.ToString() + extension;
                    string nuevaRutaFisica = Server.MapPath(nuevaRutaVirtual);

                    File.Move(rutaFisicaTemp, nuevaRutaFisica);

                    // Actualizar solo la ruta en la DB
                    _cursoNegocio.ActualizarImagen(nuevoId, nuevaRutaVirtual);
                }
            }
            catch { }
        }

        private void MostrarMensajeGlobal(string mensaje, bool esError)
        {
            pnlMensajeGlobal.Visible = true;
            litMensajeGlobal.Text = mensaje;
            pnlMensajeGlobal.CssClass = esError ? "alert alert-danger" : "alert alert-success";
            updMensajeGlobal.Update();
        }

        // =========================================================================
        //  LÓGICA DE OBJETIVOS (NUEVO)
        // =========================================================================

        private void CargarGrillaObjetivos(int idCurso)
        {
            CursoObjetivoNegocio negocioObj = new CursoObjetivoNegocio();
            dgvObjetivos.DataSource = negocioObj.Listar(idCurso);
            dgvObjetivos.DataBind();
            updObjetivos.Update(); // Forzamos actualización del panel
        }

        protected void btnAgregarObjetivo_Click(object sender, EventArgs e)
        {
            try
            {
                // Usamos CursoIdEnEdicion que ya tenés definido como propiedad
                if (this.CursoIdEnEdicion == 0) return;

                CursoObjetivoNegocio negocioObj = new CursoObjetivoNegocio();
                CursoObjetivo nuevo = new CursoObjetivo();

                nuevo.Descripcion = txtNuevoObjetivo.Text;
                nuevo.Curso = new Curso { Id = this.CursoIdEnEdicion };

                negocioObj.Agregar(nuevo);

                txtNuevoObjetivo.Text = string.Empty;
                CargarGrillaObjetivos(this.CursoIdEnEdicion);
            }
            catch (Exception ex)
            {
                // Mostrar error sutilmente o en el global
                MostrarMensajeGlobal("Error al agregar objetivo: " + ex.Message, true);
            }
        }

        protected void dgvObjetivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idObjetivo = Convert.ToInt32(dgvObjetivos.SelectedDataKey.Value);

                CursoObjetivoNegocio negocioObj = new CursoObjetivoNegocio();
                negocioObj.Eliminar(idObjetivo);

                CargarGrillaObjetivos(this.CursoIdEnEdicion);
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal("Error al eliminar objetivo: " + ex.Message, true);
            }
        }
    }
}


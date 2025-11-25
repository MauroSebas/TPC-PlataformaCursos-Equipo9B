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
               
                ddlCategoria.DataSource = _catNegocio.Listar();
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("-- Seleccione Categoría --", "0"));

              
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


                ExamenNegocio exNeg = new ExamenNegocio();
                Examen examen = exNeg.ObtenerPorCurso(curso.Id);

                if (examen != null && examen.EstaActivo)
                {
                    chkRequiereExamen.Checked = true;
                    txtUrlExamen.Text = examen.UrlConsigna;
                }
                else
                {
                    chkRequiereExamen.Checked = false;
                    txtUrlExamen.Text = string.Empty;
                }


                // Habilitar el Panel de Objetivos
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

                // Disparar eventos de UI 
                ddlModalidadPago_SelectedIndexChanged(null, null);
                rbTipoImagen_CheckedChanged(null, null);
            }
            catch (Exception ex)
            {
                MostrarMensajeGlobal("Error al cargar el curso: " + ex.Message, true);
            }
        }

        // --- EVENTOS DE UI ---

        //  Bloquea precio si es Gratuito
        protected void ddlModalidadPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool esGratuito = (ddlModalidadPago.SelectedValue == "Gratuito");
            txtPrecio.Enabled = !esGratuito;

            if (esGratuito)
            {
                txtPrecio.Text = "0.00";
            }
        }

        // Switch entre Archivo y URL
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

                
                if (ddlModalidadPago.SelectedValue == "Gratuito")
                {
                    curso.Precio = 0; 
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

               //Mapeo
                curso.NivelDificultad = ddlNivel.SelectedValue;
                curso.Idioma = ddlIdioma.SelectedValue;
                curso.ConCertificado = chkCertificadoHTML.Checked;

               
                curso.EstaActivo = true;

                if (curso.Id == 0)
                {
                    curso.Publicado = false; 
                }
                else
                {
                    
                    Curso original = _cursoNegocio.BuscarCurso(curso.Id);
                    if (original != null) curso.Publicado = original.Publicado;
                    else curso.Publicado = false;
                }

               
                string urlImagen = ManejarImagen(curso.Id);
                curso.UrlImagenPortada = urlImagen;

               
                int idGuardado = _cursoNegocio.GuardarCurso(curso);
                if (chkRequiereExamen.Checked)
                {
                    ExamenNegocio exNeg = new ExamenNegocio();
                    // Si marcó el check pero dejó vacío el link, podemos validar o dejarlo pasar (mejor validar)
                    if (!string.IsNullOrEmpty(txtUrlExamen.Text))
                    {
                        exNeg.Guardar(idGuardado, txtUrlExamen.Text);
                    }
                }

                if (curso.Id == 0 && !string.IsNullOrEmpty(urlImagen) && urlImagen.Contains("temp"))
                {
                    RenombrarImagenDefinitiva(urlImagen, idGuardado);
                }

                if (this.CursoIdEnEdicion == 0) 
                {
                    
                    Response.Redirect("~/Administrador/Curso/CursoForm.aspx?id=" + idGuardado, false);
                }
                else
                {
                    
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
           
            if (rbImagenUrl.Checked)
            {
                if (!string.IsNullOrWhiteSpace(txtUrlImagen.Text))
                    return txtUrlImagen.Text.Trim();
            }
            
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
            updObjetivos.Update(); 
        }

        protected void btnAgregarObjetivo_Click(object sender, EventArgs e)
        {
            try
            {
                
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


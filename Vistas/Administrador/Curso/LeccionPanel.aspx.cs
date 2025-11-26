using Dominio;
using Negocio.Contenido;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Administrador.Curso
{
    public partial class LeccionPanel : System.Web.UI.Page
    {
        
        public int IdModuloActual
        {
            get { return ViewState["IdModulo"] != null ? (int)ViewState["IdModulo"] : 0; }
            set { ViewState["IdModulo"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idStr = Request.QueryString["id"];
                if (string.IsNullOrEmpty(idStr) || !int.TryParse(idStr, out int id))
                {
                    Response.Redirect("CursoPanel.aspx"); 
                    return;
                }

                this.IdModuloActual = id;
                CargarInfoModulo(id);
                CargarGrilla();
            }
        }

        private void CargarInfoModulo(int id)
        {
            try
            {
                ModuloNegocio mNeg = new ModuloNegocio();
                Modulo modulo = mNeg.Obtener(id);
                if (modulo != null)
                {
                    litTituloModulo.Text = "Lecciones: " + modulo.Nombre;

                   
                    hfIdCursoDelModulo.Value = modulo.IdCurso.ToString();
                    btnVolver.NavigateUrl = $"~/Administrador/Curso/ModuloGestion.aspx?id={modulo.IdCurso}";
                }
            }
            catch { }
        }

        private void CargarGrilla()
        {
            try
            {
                LeccionNegocio lNeg = new LeccionNegocio();
                gvLecciones.DataSource = lNeg.Listar(this.IdModuloActual);
                gvLecciones.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar: " + ex.Message, true);
            }
        }

       
        protected void ddlTipoMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipo = ddlTipoMaterial.SelectedValue;

           
            pnlUrl.Visible = false;
            pnlArchivo.Visible = false;

           
            if (tipo == "Video" || tipo == "Enlace")
            {
                pnlUrl.Visible = true;
                txtDuracion.Enabled = true; 
            }
            else if (tipo == "Archivo")
            {
                pnlArchivo.Visible = true;

                txtDuracion.Text = "0"; 
                txtDuracion.Enabled = false; 
            }
        }

       
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                LeccionNegocio lNeg = new LeccionNegocio();
                Leccion leccion = new Leccion(); 
                Leccion original = null;         

                int idEditar = int.Parse(hfIdLeccionEditar.Value);

                
                if (idEditar > 0)
                {
                    leccion.Id = idEditar;
                    original = lNeg.Obtener(idEditar); 
                    leccion.IdModulo = original.IdModulo;
                    leccion.Orden = original.Orden; 

                   
                    leccion.UrlRecurso = original.UrlRecurso;
                    leccion.UrlDocumento = original.UrlDocumento;
                }
                else
                {
                    // ALTA NUEVA
                    List<Leccion> lista = lNeg.Listar(this.IdModuloActual);
                    leccion.IdModulo = this.IdModuloActual;
                    leccion.Orden = lista.Count + 1;
                }

               
                leccion.Titulo = txtTitulo.Text;
                leccion.Descripcion = txtDescripcion.Text;
                leccion.TipoMaterial = ddlTipoMaterial.SelectedValue;

                int duracion = 0;
                if (int.TryParse(txtDuracion.Text, out duracion)) leccion.DuracionMinutos = duracion;

               
                if (leccion.TipoMaterial == "Video" || leccion.TipoMaterial == "Enlace")
                {
                    
                    if (!string.IsNullOrWhiteSpace(txtUrlRecurso.Text))
                        leccion.UrlRecurso = txtUrlRecurso.Text;

                    leccion.UrlDocumento = null; 
                }
                else if (leccion.TipoMaterial == "Archivo")
                {
                    
                    if (fileUploadMaterial.HasFile)
                    {
                       
                        string ext = Path.GetExtension(fileUploadMaterial.FileName);
                        string nombreArchivo = $"Material-{Guid.NewGuid()}{ext}";
                        string carpetaDestino = "~/Assets/Material/";
                        string rutaFisica = Server.MapPath(carpetaDestino);

                        if (!Directory.Exists(rutaFisica)) Directory.CreateDirectory(rutaFisica);

                        fileUploadMaterial.SaveAs(Path.Combine(rutaFisica, nombreArchivo));
                        leccion.UrlDocumento = carpetaDestino + nombreArchivo;
                    }
                    else
                    {
                       
                        if (idEditar > 0 && !string.IsNullOrEmpty(original.UrlDocumento))
                        {
                            
                            leccion.UrlDocumento = original.UrlDocumento;
                        }
                        else
                        {
                            
                            leccion.UrlDocumento = null;
                        }
                    }

                    leccion.UrlRecurso = null; 
                }

                
                lNeg.Guardar(leccion);

                
                LimpiarFormulario();
                CargarGrilla();                
                updFormulario.Update();
                MostrarMensaje("Lección guardada correctamente.", false);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

       
        protected void gvLecciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LeccionNegocio lNeg = new LeccionNegocio();

            if (e.CommandName == "Editar")
            {
                int idLeccion = Convert.ToInt32(e.CommandArgument);
                Leccion leccion = lNeg.Obtener(idLeccion);

                
                txtTitulo.Text = leccion.Titulo;
                txtDescripcion.Text = leccion.Descripcion;
                txtDuracion.Text = leccion.DuracionMinutos.ToString();
                ddlTipoMaterial.SelectedValue = leccion.TipoMaterial;
                hfIdLeccionEditar.Value = leccion.Id.ToString();

                
                ddlTipoMaterial_SelectedIndexChanged(null, null);

                if (leccion.TipoMaterial == "Video" || leccion.TipoMaterial == "Enlace")
                {
                    txtUrlRecurso.Text = leccion.UrlRecurso;
                }
                else if (leccion.TipoMaterial == "Archivo")
                {
                   
                    if (!string.IsNullOrEmpty(leccion.UrlDocumento))
                    {
                        pnlArchivoExistente.Visible = true;
                        lblArchivoActual.Text = Path.GetFileName(leccion.UrlDocumento);
                    }
                }

                btnGuardar.Text = "Guardar Cambios";
                btnGuardar.CssClass = "btn btn-warning fw-bold text-dark";
                btnCancelar.Visible = true;
                updFormulario.Update();
            }
            else if (e.CommandName == "Subir" || e.CommandName == "Bajar")
            {               
                int idTocado = Convert.ToInt32(e.CommandArgument);
                List<Leccion> lista = lNeg.Listar(this.IdModuloActual);

                Leccion mover = null;
                Leccion vecino = null;
                int index = -1;

                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i].Id == idTocado) { mover = lista[i]; index = i; break; }
                }

                if (mover != null)
                {
                    if (e.CommandName == "Subir" && index > 0) vecino = lista[index - 1];
                    else if (e.CommandName == "Bajar" && index < lista.Count - 1) vecino = lista[index + 1];

                    if (vecino != null)
                    {
                        int auxOrden = mover.Orden;
                        lNeg.ActualizarOrden(mover.Id, vecino.Orden);
                        lNeg.ActualizarOrden(vecino.Id, auxOrden);
                        CargarGrilla();
                    }
                }
            }
        }

       
        protected void gvLecciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal litIcono = (Literal)e.Row.FindControl("litIconoTipo");
                string tipo = DataBinder.Eval(e.Row.DataItem, "TipoMaterial").ToString();

                string iconoHtml = "";
                switch (tipo)
                {
                    case "Video": iconoHtml = "<i class='bi bi-play-circle-fill text-danger fs-5'></i>"; break;
                    case "Archivo": iconoHtml = "<i class='bi bi-file-earmark-text-fill text-primary fs-5'></i>"; break;
                    case "Enlace": iconoHtml = "<i class='bi bi-link-45deg text-info fs-5'></i>"; break;
                    default: iconoHtml = "<i class='bi bi-journal-text text-secondary fs-5'></i>"; break;
                }
                litIcono.Text = iconoHtml;
            }
        }

       
        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idLeccion = int.Parse(hfIdLeccionEliminar.Value);
                LeccionNegocio lNeg = new LeccionNegocio();

                lNeg.Eliminar(idLeccion);
                lNeg.Reordenar(this.IdModuloActual); 

                if (hfIdLeccionEditar.Value == idLeccion.ToString()) LimpiarFormulario();

                CargarGrilla();
                MostrarMensaje("Lección eliminada.", false);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        
        // AUXILIARES
       
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtTitulo.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtUrlRecurso.Text = string.Empty;
            txtDuracion.Text = "10";
            ddlTipoMaterial.SelectedIndex = 0;
            hfIdLeccionEditar.Value = "0";

            pnlUrl.Visible = true;
            pnlArchivo.Visible = false;
            pnlArchivoExistente.Visible = false;

            btnGuardar.Text = "Guardar Lección";
            btnGuardar.CssClass = "btn btn-primary";
            btnCancelar.Visible = false;

            txtDuracion.Enabled = true; 
            ddlTipoMaterial.SelectedIndex = 0;
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            litMensaje.Text = mensaje;
            pnlMensaje.CssClass = esError ? "alert alert-danger" : "alert alert-success";
            pnlMensaje.Visible = true;
            updMensaje.Update();
        }
    }
}

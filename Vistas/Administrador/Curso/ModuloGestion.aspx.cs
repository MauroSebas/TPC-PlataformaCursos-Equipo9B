using Dominio;
using Negocio;
using Negocio.Contenido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Aministrador
{
    public partial class ModuloGestion : System.Web.UI.Page
    {        
        public int IdCursoActual
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
                    Response.Redirect("~/Administrador/Curso/CursoPanel.aspx");
                    return;
                }

                this.IdCursoActual = id;
                CargarInfoCurso(id);
                CargarGrilla();
            }
        }

        private void CargarInfoCurso(int id)
        {
            try
            {
                CursoNegocio cNeg = new CursoNegocio();
                Curso curso = cNeg.BuscarCurso(id);
                if (curso != null)
                {
                    litTituloCurso.Text = "Módulos de: " + curso.Titulo;
                }
            }
            catch { }
        }

        private void CargarGrilla()
        {
            try
            {
                ModuloNegocio mNeg = new ModuloNegocio();
                gvModulos.DataSource = mNeg.Listar(this.IdCursoActual);
                gvModulos.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar módulos: " + ex.Message, true);
            }
        }

        // =========================================================
        // LÓGICA  AGREGAR / MODIFICAR
        // =========================================================
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                ModuloNegocio mNeg = new ModuloNegocio();
                Modulo modulo = new Modulo();

                int idEditar = int.Parse(hfIdModuloEditar.Value);

                if (idEditar > 0)
                {
                    // EDICIÓN
                    modulo = mNeg.Obtener(idEditar);
                    modulo.Nombre = txtNombreModulo.Text;

                    mNeg.Guardar(modulo);
                    MostrarMensaje("Módulo modificado correctamente.", false);
                }
                else
                {
                    // ALTA
                    List<Modulo> listaActual = mNeg.Listar(this.IdCursoActual);
                    modulo.IdCurso = this.IdCursoActual;
                    modulo.Nombre = txtNombreModulo.Text;
                    modulo.Orden = listaActual.Count + 1;

                    mNeg.Guardar(modulo);
                    MostrarMensaje("Módulo agregado correctamente.", false);
                }

                LimpiarFormulario();
                CargarGrilla();
                updFormulario.Update();
                updGrilla.Update();
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            updFormulario.Update();
        }

        // =========================================================
        // ACCIONES GRILLA: EDITAR / SUBIR / BAJAR
        // =========================================================
        protected void gvModulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            ModuloNegocio mNeg = new ModuloNegocio();

            if (e.CommandName == "Editar")
            {
                int idModulo = Convert.ToInt32(e.CommandArgument);

                Modulo modulo = mNeg.Obtener(idModulo);

                // Llenar form
                txtNombreModulo.Text = modulo.Nombre;
                hfIdModuloEditar.Value = modulo.Id.ToString();

                // Cambiar estado visual del botón
                btnAgregar.Text = "Guardar Cambios";
                btnAgregar.CssClass = "btn btn-warning fw-bold text-dark";
                btnCancelarEdicion.Visible = true;

                updFormulario.Update();
            }
            else if (e.CommandName == "Subir" || e.CommandName == "Bajar")
            {
                
                int idModuloTocado = Convert.ToInt32(e.CommandArgument);
                List<Modulo> lista = mNeg.Listar(this.IdCursoActual);

                Modulo moduloMover = null;
                Modulo moduloVecino = null;
                int indexMover = -1;

                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i].Id == idModuloTocado)
                    {
                        moduloMover = lista[i];
                        indexMover = i;
                        break;
                    }
                }

                if (moduloMover != null)
                {
                    if (e.CommandName == "Subir" && indexMover > 0)
                    {
                        moduloVecino = lista[indexMover - 1];
                    }
                    else if (e.CommandName == "Bajar" && indexMover < lista.Count - 1)
                    {
                        moduloVecino = lista[indexMover + 1];
                    }

                    if (moduloVecino != null)
                    {
                        int ordenTemporal = moduloMover.Orden;
                        mNeg.ActualizarOrden(moduloMover.Id, moduloVecino.Orden);
                        mNeg.ActualizarOrden(moduloVecino.Id, ordenTemporal);

                        CargarGrilla();
                        updGrilla.Update();
                    }
                }
            }
        }

        // =========================================================
        // ELIMINACIÓN CONFIRMADA  MODAL
        // =========================================================
        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idModulo = int.Parse(hfIdModuloEliminar.Value);
                ModuloNegocio mNeg = new ModuloNegocio();

                mNeg.Eliminar(idModulo);

                
                mNeg.Reordenar(this.IdCursoActual);

                
                if (hfIdModuloEditar.Value == idModulo.ToString())
                    LimpiarFormulario();

                CargarGrilla();
                updGrilla.Update();
                MostrarMensaje("Módulo eliminado correctamente.", false);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al eliminar: " + ex.Message, true);
            }
        }

        // =========================================================
        // AUXILIARES
        // =========================================================
        private void LimpiarFormulario()
        {
            txtNombreModulo.Text = string.Empty;
            hfIdModuloEditar.Value = "0";
            btnAgregar.Text = "Agregar al Final";
            btnAgregar.CssClass = "btn btn-primary";
            btnCancelarEdicion.Visible = false;
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


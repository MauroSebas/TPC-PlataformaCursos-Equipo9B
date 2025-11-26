using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace Vistas
{
    public partial class CategoriaGestion : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            try
            {
                CategoriaNegocio catNeg = new CategoriaNegocio();
                gvCategorias.DataSource = catNeg.Listar();
                gvCategorias.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar: " + ex.Message, true);
            }
        }

        // =========================================================
        // GUARDAR (ALTA / MODIFICACIÓN)
        // =========================================================
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                CategoriaNegocio catNeg = new CategoriaNegocio();
                Categoria cat = new Categoria();

                int idEditar = int.Parse(hfIdCategoriaEditar.Value);

                if (idEditar > 0)
                {
                   
                    cat = catNeg.Obtener(idEditar);
                    cat.Nombre = txtNombre.Text; 

                    catNeg.Guardar(cat); 
                    MostrarMensaje("Categoría modificada.", false);
                }
                else
                {
                    
                    cat.Nombre = txtNombre.Text;
                    cat.EstaActivo = true;

                    catNeg.Guardar(cat);
                    MostrarMensaje("Categoría creada.", false);
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

        // =========================================================
        // ACCIONES DE GRILLA (Solo Editar)
        // =========================================================
        protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                CategoriaNegocio catNeg = new CategoriaNegocio();
                Categoria cat = catNeg.Obtener(id);

                if (cat != null)
                {
                    txtNombre.Text = cat.Nombre;
                    hfIdCategoriaEditar.Value = cat.Id.ToString();

                    // Visual
                    btnGuardar.Text = "Guardar Cambios";
                    btnGuardar.CssClass = "btn btn-warning fw-bold text-dark";
                    btnCancelar.Visible = true;

                    updFormulario.Update();
                }
            }
        }

        // =========================================================
        // ELIMINAR
        // =========================================================
        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(hfIdCategoriaEliminar.Value);
                CategoriaNegocio catNeg = new CategoriaNegocio();
                
                catNeg.Eliminar(id);

                if (hfIdCategoriaEditar.Value == id.ToString()) LimpiarFormulario();

                CargarGrilla();
                updGrilla.Update();
                MostrarMensaje("Categoría eliminada.", false);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true); 
            }
        }

        // =========================================================
        // AUXILIARES
        // =========================================================
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            updFormulario.Update();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            hfIdCategoriaEditar.Value = "0";

            btnGuardar.Text = "Guardar Categoría";
            btnGuardar.CssClass = "btn btn-primary";
            btnCancelar.Visible = false;
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


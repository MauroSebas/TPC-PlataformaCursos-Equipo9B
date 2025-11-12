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
    public partial class CategoriaEliminarEditar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)// Va a precargar si no es un postback de la pantalla
            {

                CategoriaNegocio negocio = new CategoriaNegocio();
                Categoria seleccionada = new Categoria();

                int id = Request.QueryString["Id"] != null ? int.Parse(Request.QueryString["Id"].ToString()) : 0;//Operador ternario

                if (id != 0)
                {
                    //Utlizo la base de datos para traer la lista de categorias
                    seleccionada = negocio.BuscarPorId(id);
                }

                //Precargar el label

                txtModificar.Text = seleccionada.Nombre;


            }
            

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
           
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria seleccionada = new Categoria();

            int id = int.Parse(Request.QueryString["Id"].ToString());
            seleccionada.Id = id;
            seleccionada.Nombre = txtModificar.Text;
            negocio.modificarConSP(seleccionada);
            Response.Redirect("CategoriaGestion.aspx");

        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("CategoriaGestion.aspx");

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria seleccionada = new Categoria();

            int id = int.Parse(Request.QueryString["Id"].ToString());
            seleccionada.Id = id;
            negocio.eliminarLogico(id);
            Response.Redirect("CategoriaGestion.aspx");
        }
    }
}
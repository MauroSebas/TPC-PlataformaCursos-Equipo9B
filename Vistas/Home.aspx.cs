using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Vistas
{
    public partial class Home : System.Web.UI.Page
    {
        private readonly CategoriaNegocio _catNegocio = new CategoriaNegocio();
        private readonly CursoNegocio _cursoNegocio = new CursoNegocio();      
        protected int CategoriaSeleccionadaId
        {
            get
            {
                if (ViewState["CatFiltro"] != null)
                    return (int)ViewState["CatFiltro"];
                return 0; 
            }
            set { ViewState["CatFiltro"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
                CargarCursos();

                btnTodos.CssClass = "cat-pill text-decoration-none active";
            }
        }
        private void CargarCategorias()
        {
            try
            {
                List<Categoria> lista = _catNegocio.Listar();
                repCategorias.DataSource = lista;
                repCategorias.DataBind();                
            }
            catch (Exception) { }
        }
        private void CargarCursos()
        {
            CursoNegocio negocio = new CursoNegocio();
            try
            {
                
                List<Curso> listaCompleta = negocio.listarCursos();

               
                List<Curso> listaFiltrada = listaCompleta.FindAll(x => x.Publicado == true);
               
                if (this.CategoriaSeleccionadaId > 0)
                {
                    listaFiltrada = listaFiltrada.FindAll(x => x.Categoria.Id == this.CategoriaSeleccionadaId);
                }

               
                repCursos.DataSource = listaFiltrada;
                repCursos.DataBind();
            }
            catch (Exception) { }
        }
        protected void btnFiltroCategoria_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int idCategoriaNueva = int.Parse(btn.CommandArgument);

            
            if (this.CategoriaSeleccionadaId == idCategoriaNueva && idCategoriaNueva != 0)
            {
                this.CategoriaSeleccionadaId = 0;
            }
            else
            {
                this.CategoriaSeleccionadaId = idCategoriaNueva;
            }

            
            CargarCursos();
            CargarCategorias(); 

           
            btnTodos.CssClass = "cat-pill text-decoration-none " + (CategoriaSeleccionadaId == 0 ? "active" : "");
        }     
        public string ObtenerImagen(object urlObj)
        {
            string url = urlObj as string;
            if (string.IsNullOrEmpty(url))
            {
                
                return ResolveUrl("~/Assets/Cursos/placeholder-curso.jpg");
            }
            
            return ResolveUrl(url);
        }
    }
}
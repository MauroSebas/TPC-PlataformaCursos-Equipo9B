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
    public partial class MisCursos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCursosSimulados();
            }
        }

        private void CargarCursosSimulados()
        {
            try
            {
                CursoNegocio negocio = new CursoNegocio();

                // TODO: Cuando esté el módulo de Inscripción, cambiar esto por:
                // List<Curso> lista = negocio.ListarPorAlumno(usuarioId);

                // POR AHORA: Traemos todos para probar el flujo
                List<Curso> lista = negocio.listarCursos();

                // Filtramos solo los publicados para que no se vea basura
                // (O podés dejar todo si querés ver cómo queda)
                var listaVisible = lista.FindAll(x => x.Publicado);

                repMisCursos.DataSource = listaVisible;
                repMisCursos.DataBind();
            }
            catch (Exception ex)
            {
                // Manejo error
            }
        }

        public string ObtenerImagen(object urlObj)
        {
            string url = urlObj as string;
            if (string.IsNullOrEmpty(url))
                return ResolveUrl("~/Assets/img/placeholder-curso.jpg");

            return ResolveUrl(url);
        }
    }
}

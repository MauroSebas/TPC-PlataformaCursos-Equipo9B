using Dominio;
using Negocio;
using Negocio.Contenido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Alumno.aula
{
    public partial class Aula : System.Web.UI.Page
    {
        // Propiedades de Estado
        public int IdCursoActual
        {
            get { return ViewState["IdCurso"] != null ? (int)ViewState["IdCurso"] : 0; }
            set { ViewState["IdCurso"] = value; }
        }

        public int IdLeccionActual
        {
            get { return ViewState["IdLeccion"] != null ? (int)ViewState["IdLeccion"] : 0; }
            set { ViewState["IdLeccion"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 1. Validar Curso
                string idCursoStr = Request.QueryString["id"];
                if (string.IsNullOrEmpty(idCursoStr) || !int.TryParse(idCursoStr, out int idCurso))
                {
                    Response.Redirect("~/Alumno/MisCursos.aspx");
                    return;
                }

                // --- VALIDAR INSCRIPCIÓN (Lógica futura) ---
                /* Usuario u = (Usuario)Session["Usuario"];
                InscripcionNegocio insNeg = new InscripcionNegocio();
                if (!insNeg.EstaInscripto(u.Id, idCurso)) {
                    Response.Redirect("~/CursoDetalle.aspx?id=" + idCurso); // Lo mandamos a comprar
                    return;
                }
                */

                this.IdCursoActual = idCurso;

                // 2. Validar Lección
                string idLeccionStr = Request.QueryString["leccion"];
                int idLeccion = 0;

                if (!string.IsNullOrEmpty(idLeccionStr) && int.TryParse(idLeccionStr, out int parsedId))
                {
                    idLeccion = parsedId;
                }
                else
                {
                    // Auto-Redirección a la primera clase
                    idLeccion = ObtenerPrimeraLeccion(idCurso);
                    if (idLeccion > 0)
                    {
                        Response.Redirect($"Aula.aspx?id={idCurso}&leccion={idLeccion}");
                        return;
                    }
                }

                this.IdLeccionActual = idLeccion;

                // 3. Cargar Todo
                CargarHeaderCurso(idCurso);
                CargarMenuLateral(idCurso);

                if (idLeccion > 0)
                    CargarContenidoLeccion(idLeccion);
            }
        }

        // ============================================================
        // A. CARGA DE DATOS
        // ============================================================

        private void CargarHeaderCurso(int idCurso)
        {
            Label lbl = (Label)Master.FindControl("lblNombreCurso");
            if (lbl != null)
            {
                try
                {
                    CursoNegocio cn = new CursoNegocio();
                    var curso = cn.BuscarCurso(idCurso);
                    if (curso != null) lbl.Text = curso.Titulo;
                }
                catch { }
            }
        }

        private void CargarMenuLateral(int idCurso)
        {
            try
            {
                ModuloNegocio mNeg = new ModuloNegocio();
                repModulos.DataSource = mNeg.Listar(idCurso);
                repModulos.DataBind();
            }
            catch { }
        }

        protected void repModulos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Modulo m = (Modulo)e.Item.DataItem;
                Repeater repHijo = (Repeater)e.Item.FindControl("repLecciones");

                // CHEQUEO DE SEGURIDAD: Solo intentamos llenar si encontramos el control
                if (repHijo != null)
                {
                    LeccionNegocio lNeg = new LeccionNegocio();
                    repHijo.DataSource = lNeg.Listar(m.Id);
                    repHijo.DataBind();
                }
            }
        }

        // ============================================================
        // B. CONTENIDO PRINCIPAL
        // ============================================================

        private void CargarContenidoLeccion(int idLeccion)
        {
            LeccionNegocio lNeg = new LeccionNegocio();
            Leccion leccion = lNeg.Obtener(idLeccion);
            if (leccion == null) return;

            lblTituloLeccion.Text = leccion.Titulo;
            litDescripcion.Text = string.IsNullOrEmpty(leccion.Descripcion) ? "Sin descripción disponible." : leccion.Descripcion;

            // Paneles Multimedia
            divVideo.Visible = false;
            pnlRecursoExterno.Visible = false;

            if (leccion.TipoMaterial == "Video")
            {
                divVideo.Visible = true;
                string url = leccion.UrlRecurso;
                if (!string.IsNullOrEmpty(url) && url.Contains("watch?v="))
                    url = url.Replace("watch?v=", "embed/");
                iframeVideo.Attributes["src"] = url;
            }
            else
            {
                pnlRecursoExterno.Visible = true;
                lnkRecurso.NavigateUrl = (leccion.TipoMaterial == "Archivo") ? ResolveUrl(leccion.UrlDocumento) : leccion.UrlRecurso;
                lnkRecurso.Text = (leccion.TipoMaterial == "Archivo")
                    ? "<i class='bi bi-download me-2'></i>Descargar Material"
                    : "<i class='bi bi-box-arrow-up-right me-2'></i>Ir al Sitio Externo";
            }

            // Configurar Botones Navegación
            ConfigurarNavegacion(idLeccion);
        }

        private void ConfigurarNavegacion(int idActual)
        {
            int anterior = BuscarLeccionAdyacente(idActual, false);
            int siguiente = BuscarLeccionAdyacente(idActual, true);

            // Botón Anterior
            btnAnterior.Enabled = (anterior > 0);
            btnAnterior.CommandArgument = anterior.ToString(); // Guardamos ID en el botón

            // Botón Siguiente
            if (siguiente > 0)
            {
                btnSiguiente.Text = "Siguiente →";
                btnSiguiente.CommandArgument = siguiente.ToString();
                // btnSiguiente.CssClass = "btn btn-primary px-4";
            }
            else
            {
                btnSiguiente.Text = "Finalizar Curso 🎉";
                btnSiguiente.CommandArgument = "FIN";
                // btnSiguiente.CssClass = "btn btn-success px-4";
            }
        }

        // ============================================================
        // C. LÓGICA DE NAVEGACIÓN (ANTERIOR / SIGUIENTE)
        // ============================================================

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            string arg = ((Button)sender).CommandArgument;

            // Acá iría la lógica de marcar como visto:
            // ProgresoNegocio.MarcarVisto(Usuario.Id, this.IdLeccionActual);

            if (arg == "FIN")
            {
                // Lógica de fin de curso (Certificado, Congrats, etc)
                Response.Redirect("~/Alumno/MisCursos.aspx?msg=curso_completado");
            }
            else
            {
                int idSig = int.Parse(arg);
                Response.Redirect($"Aula.aspx?id={this.IdCursoActual}&leccion={idSig}");
            }
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            string arg = ((Button)sender).CommandArgument;
            if (int.TryParse(arg, out int idAnt) && idAnt > 0)
            {
                Response.Redirect($"Aula.aspx?id={this.IdCursoActual}&leccion={idAnt}");
            }
        }

        // Método helper potente para buscar ID anterior o siguiente
        private int BuscarLeccionAdyacente(int idActual, bool buscarSiguiente)
        {
            // Aplanamos todas las lecciones del curso en una lista lineal
            ModuloNegocio mNeg = new ModuloNegocio();
            LeccionNegocio lNeg = new LeccionNegocio();
            List<Modulo> modulos = mNeg.Listar(this.IdCursoActual);
            List<Leccion> flatList = new List<Leccion>();

            foreach (var m in modulos)
            {
                flatList.AddRange(lNeg.Listar(m.Id));
            }

            // Buscamos el índice
            for (int i = 0; i < flatList.Count; i++)
            {
                if (flatList[i].Id == idActual)
                {
                    if (buscarSiguiente)
                    {
                        return (i + 1 < flatList.Count) ? flatList[i + 1].Id : 0;
                    }
                    else
                    {
                        return (i - 1 >= 0) ? flatList[i - 1].Id : 0;
                    }
                }
            }
            return 0;
        }

        private int ObtenerPrimeraLeccion(int idCurso)
        {
            // Reutilizamos la lógica de buscar el primero de la lista plana
            ModuloNegocio mNeg = new ModuloNegocio();
            LeccionNegocio lNeg = new LeccionNegocio();
            var modulos = mNeg.Listar(idCurso);

            foreach (var m in modulos)
            {
                var lecciones = lNeg.Listar(m.Id);
                if (lecciones.Count > 0) return lecciones[0].Id;
            }
            return 0;
        }

        // ============================================================
        // D. AUXILIARES
        // ============================================================
        public string ObtenerIcono(object tipoObj)
        {
            string tipo = tipoObj.ToString();
            if (tipo == "Video") return "bi bi-play-circle-fill me-2";
            if (tipo == "Archivo") return "bi bi-file-earmark-text-fill me-2";
            return "bi bi-link-45deg me-2";
        }
    }
}
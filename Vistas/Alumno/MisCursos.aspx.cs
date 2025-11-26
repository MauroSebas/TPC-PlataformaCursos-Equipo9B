using Dominio;
using Negocio;
using Negocio.Contenido;
using Negocio.Cursada;
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
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Auth/Loguin.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarMisCursos();
            }
        }

        private void CargarMisCursos()
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                InscripcionNegocio negocio = new InscripcionNegocio();

                // Traemos todas las inscripciones del usuario
                List<Inscripcion> listaInscripciones = negocio.ListarPorUsuario(usuario.UsuarioID);

                
                List<Inscripcion> listaAprobada = new List<Inscripcion>();

                foreach (Inscripcion i in listaInscripciones)
                {
                    if (i.Estado == "Aprobado")
                    {
                        listaAprobada.Add(i);
                    }
                }

                if (listaAprobada.Count > 0)
                {
                    repMisCursos.DataSource = listaAprobada;
                    repMisCursos.DataBind();
                }
                else
                {
                    pnlSinCursos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx");
            }
        }

        // --- MÉTODOS VISUALES PARA EL FRONT ---

        public string ObtenerImagen(object urlObj)
        {
            string url = urlObj as string;
            if (string.IsNullOrEmpty(url))
                return ResolveUrl("~/Assets/img/placeholder-curso.jpg");

            return ResolveUrl(url);
        }

       
        public int ObtenerPorcentaje(object idCursoObj, object idInscripcionObj)
        {
            try
            {
                int idCurso = Convert.ToInt32(idCursoObj);
                int idInscripcion = Convert.ToInt32(idInscripcionObj);

               
                ModuloNegocio mNeg = new ModuloNegocio();
                LeccionNegocio lNeg = new LeccionNegocio();

                List<Modulo> modulos = mNeg.Listar(idCurso);
                int totalLecciones = 0;

                foreach (Modulo m in modulos)
                {
                    List<Leccion> lecciones = lNeg.Listar(m.Id);
                    totalLecciones += lecciones.Count;
                }

                // Si no tiene lecciones, es 0%
                if (totalLecciones == 0) return 0;

                //  Contar Lecciones Vistas
                ProgresoLeccionNegocio pNeg = new ProgresoLeccionNegocio();
                List<ProgresoLeccion> progreso = pNeg.ListarProgreso(idInscripcion);
                int vistas = progreso.Count;

                // Calcular
                return (vistas * 100) / totalLecciones;
            }
            catch
            {
                return 0;
            }
        }

        // Calcula texto de expiración
        public string ObtenerTextoExpiracion(object fechaExpObj)
        {
            if (fechaExpObj == null) return "Acceso Ilimitado";

            DateTime fechaExp = (DateTime)fechaExpObj;
            TimeSpan diferencia = fechaExp - DateTime.Today;

            if (diferencia.Days < 0)
            {
                return "<span class='text-danger fw-bold'>Vencido</span>";
            }
            else if (diferencia.Days == 0)
            {
                return "<span class='text-warning fw-bold'>Vence Hoy</span>";
            }
            else
            {
                return $"Vence en {diferencia.Days} días";
            }
        }
    }
}


using Dominio;
using Dominio.Cursada; 
using Negocio;
using Negocio.Contenido;
using Negocio.Cursada; 
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Alumno.aula
{
    public partial class Aula : System.Web.UI.Page
    {     

        public int IdCursoActual
        {
            get
            {
                if (ViewState["IdCurso"] != null)
                {
                    return (int)ViewState["IdCurso"];
                }
                return 0;
            }
            set { ViewState["IdCurso"] = value; }
        }

        public int IdLeccionActual
        {
            get
            {
                if (ViewState["IdLeccion"] != null)
                {
                    return (int)ViewState["IdLeccion"];
                }
                return 0;
            }
            set { ViewState["IdLeccion"] = value; }
        }

        public int IdInscripcionActual
        {
            get
            {
                if (ViewState["IdInscripcion"] != null)
                {
                    return (int)ViewState["IdInscripcion"];
                }
                return 0;
            }
            set { ViewState["IdInscripcion"] = value; }
        }

       
        public List<int> LeccionesCompletadas
        {
            get
            {
                if (Session["LeccionesVistas"] != null)
                {
                    return (List<int>)Session["LeccionesVistas"];
                }
                return new List<int>();
            }
            set { Session["LeccionesVistas"] = value; }
        }

       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                if (Session["Usuario"] == null)
                {
                    Response.Redirect("~/Auth/Loguin.aspx");
                    return;
                }

               
                string idCursoStr = Request.QueryString["id"];
                int idCurso = 0;

                if (string.IsNullOrEmpty(idCursoStr) || !int.TryParse(idCursoStr, out idCurso))
                {
                    Response.Redirect("~/Alumno/MisCursos.aspx");
                    return;
                }

               
                Usuario u = (Usuario)Session["Usuario"];
                InscripcionNegocio insNeg = new InscripcionNegocio();

                
                var inscripcion = insNeg.ObtenerInscripcionActiva(u.UsuarioID, idCurso);

                if (inscripcion == null)
                {
                   
                    Response.Redirect($"~/CursoDetalle.aspx?id={idCurso}");
                    return;
                }

                // Guardar datos clave en memoria
                this.IdCursoActual = idCurso;
                this.IdInscripcionActual = inscripcion.Id;

                //  Cargar el Progreso 
                CargarProgreso();

                // Determinar qué lección mostrar  
                string idLeccionStr = Request.QueryString["leccion"];
                int idLeccion = 0;

                if (!string.IsNullOrEmpty(idLeccionStr) && int.TryParse(idLeccionStr, out int parsedId))
                {
                    idLeccion = parsedId;
                }
                else
                {
                    // primera lección del curso
                    idLeccion = ObtenerPrimeraLeccion(idCurso);

                    // redirigi para que la URL quede limpia
                    if (idLeccion > 0)
                    {
                        Response.Redirect($"Aula.aspx?id={idCurso}&leccion={idLeccion}");
                        return;
                    }
                }

                this.IdLeccionActual = idLeccion;

                
                CargarInfoCurso(idCurso);    
                CargarMenuLateral(idCurso);  

               
                if (idLeccion > 0)
                {
                    CargarContenidoLeccion(idLeccion);
                }

                
                ConfigurarSidebarExamen();
            }
        }

       

        private void CargarProgreso()
        {
            try
            {
                ProgresoLeccionNegocio pNeg = new ProgresoLeccionNegocio();

                
                List<ProgresoLeccion> listaProgreso = pNeg.ListarProgreso(this.IdInscripcionActual);

                
                List<int> listaSoloIds = new List<int>();
                foreach (var item in listaProgreso)
                {
                    listaSoloIds.Add(item.IdLeccion);
                }

                this.LeccionesCompletadas = listaSoloIds;

               
                ActualizarBarraProgreso();
            }
            catch { }
        }

        private void ActualizarBarraProgreso()
        {
            try
            {
              
                int porcentaje = CalcularPorcentajeInt();

                litPorcentaje.Text = porcentaje + "%";
                barraProgreso.Attributes["style"] = $"width: {porcentaje}%";
            }
            catch { }
        }

        
        private int CalcularPorcentajeInt()
        {
            try
            {
                ModuloNegocio mNeg = new ModuloNegocio();
                LeccionNegocio lNeg = new LeccionNegocio();

                List<Modulo> modulos = mNeg.Listar(this.IdCursoActual);

                int totalLecciones = 0;

               
                foreach (var m in modulos)
                {
                    int cantidadEnModulo = lNeg.Listar(m.Id).Count;
                    totalLecciones += cantidadEnModulo;
                }

               
                if (totalLecciones == 0) return 0;

                int completadas = this.LeccionesCompletadas.Count;

                // Regla de tres simple
                return (completadas * 100) / totalLecciones;
            }
            catch
            {
                return 0;
            }
        }

        
        //  LÓGICA DEL EXAMEN Y ENTREGA 
        

        private void ConfigurarSidebarExamen()
        {
            try
            {
                ExamenNegocio exNeg = new ExamenNegocio();
                Examen examen = exNeg.ObtenerPorCurso(this.IdCursoActual);

               
                if (examen != null && examen.EstaActivo)
                {
                    pnlSidebarExamen.Visible = true;

                    // link de la consigna por si entra
                    lnkDescargarConsigna.NavigateUrl = examen.UrlConsigna;

                    //  100% de las clases
                    int porcentaje = CalcularPorcentajeInt();

                    if (porcentaje >= 100)
                    {
                        
                        btnIrExamen.Visible = true;
                        divExamenBloqueado.Visible = false;
                    }
                    else
                    {
                       
                        btnIrExamen.Visible = false;
                        divExamenBloqueado.Visible = true;
                    }
                }
                else
                {
                   
                    pnlSidebarExamen.Visible = false;
                }
            }
            catch { }
        }

       
        protected void btnIrExamen_Click(object sender, EventArgs e)
        {
            
            divVideo.Visible = false;
            pnlRecursoExterno.Visible = false;

            
            lblTituloLeccion.Text = "";
            litDescripcion.Text = "";

            
            pnlVistaExamen.Visible = true;

           
            CargarEstadoEntrega();
        }

        private void CargarEstadoEntrega()
        {
            EntregaNegocio entNeg = new EntregaNegocio();

           
            Entrega entrega = entNeg.ObtenerUltimaEntrega(this.IdInscripcionActual);

            if (entrega == null)
            {
                // CNunca entregó nada -> Mostrar Formulario de carga
                pnlFormularioEntrega.Visible = true;
                pnlEstadoEntrega.Visible = false;
            }
            else
            {
                //  Ya entregó -> Mostrar Estado
                pnlFormularioEntrega.Visible = false;
                pnlEstadoEntrega.Visible = true;

                // Reseteamos visibilidad de cosas opcionales
                pnlFeedback.Visible = false;
                btnReintentar.Visible = false;

                if (entrega.Estado == "Pendiente")
                {
                    litIconoEstado.Text = "<i class='bi bi-hourglass-split fs-1 text-warning'></i>";
                    litTituloEstado.Text = "En Corrección";
                    litMensajeEstado.Text = "Tu entrega del " + entrega.FechaEntrega.ToString("dd/MM/yyyy") + " está siendo revisada por el profesor.";
                }
                else if (entrega.Estado == "Aprobado")
                {
                    litIconoEstado.Text = "<i class='bi bi-check-circle-fill fs-1 text-success'></i>";
                    litTituloEstado.Text = "¡Aprobado!";
                    litMensajeEstado.Text = "Felicitaciones, has completado el curso satisfactoriamente.";

                    if (!string.IsNullOrEmpty(entrega.DevolucionProfesor))
                    {
                        pnlFeedback.Visible = true;
                        litFeedback.Text = entrega.DevolucionProfesor;
                    }
                }
                else if (entrega.Estado == "Rechazado")
                {
                    litIconoEstado.Text = "<i class='bi bi-x-circle-fill fs-1 text-danger'></i>";
                    litTituloEstado.Text = "Entrega Rechazada";
                    litMensajeEstado.Text = "Por favor, revisa las correcciones y vuelve a intentarlo.";

                    // Habilitar el botón para que pueda volver a ver el formulario
                    btnReintentar.Visible = true;

                    if (!string.IsNullOrEmpty(entrega.DevolucionProfesor))
                    {
                        pnlFeedback.Visible = true;
                        litFeedback.Text = entrega.DevolucionProfesor;
                    }
                }
            }
        }

        // Botón "Enviar a Corrección"
        protected void btnEntregarExamen_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (string.IsNullOrWhiteSpace(txtLinkEntrega.Text))
                {
                    return;
                }

                // Recuperarr el ID del examen
                ExamenNegocio exNeg = new ExamenNegocio();
                Examen ex = exNeg.ObtenerPorCurso(this.IdCursoActual);

               
                EntregaNegocio entNeg = new EntregaNegocio();
                entNeg.RegistrarEntrega(this.IdInscripcionActual, ex.Id, txtLinkEntrega.Text);

                
                CargarEstadoEntrega();
            }
            catch (Exception)
            {
               
            }
        }

        // Botón "Volver a Entregar" (solo si fue rechazado)
        protected void btnReintentar_Click(object sender, EventArgs e)
        {
            pnlEstadoEntrega.Visible = false;
            pnlFormularioEntrega.Visible = true;
            txtLinkEntrega.Text = ""; 
        }

       
        // 5. CARGA DE CONTENIDO (Lecciones de video/archivo)
       

        private void CargarContenidoLeccion(int idLeccion)
        {
            
            pnlVistaExamen.Visible = false;

            LeccionNegocio lNeg = new LeccionNegocio();
            ModuloNegocio mNeg = new ModuloNegocio();

            Leccion leccion = lNeg.Obtener(idLeccion);
            if (leccion == null) return;

            Modulo modulo = mNeg.Obtener(leccion.IdModulo);

           
            string numeracion = "";
            if (modulo != null)
            {
                numeracion = modulo.Orden + "." + leccion.Orden;
            }

            lblTituloLeccion.Text = numeracion + " - " + leccion.Titulo;

           
            if (string.IsNullOrEmpty(leccion.Descripcion))
            {
                litDescripcion.Text = "Sin descripción.";
            }
            else
            {
                litDescripcion.Text = leccion.Descripcion;
            }

            // --- MANEJO DE VIDEO O ARCHIVO  ---

            if (leccion.TipoMaterial == "Video")
            {
                divVideo.Visible = true;
                pnlRecursoExterno.Visible = false;

                string url = leccion.UrlRecurso;
                // Transformar URL de Youtube  para embeber
                if (!string.IsNullOrEmpty(url) && url.Contains("watch?v="))
                {
                    url = url.Replace("watch?v=", "embed/");
                }
                iframeVideo.Attributes["src"] = url;
            }
            else
            {
                // Es archivo o enlace externo
                divVideo.Visible = false;
                pnlRecursoExterno.Visible = true;

                if (leccion.TipoMaterial == "Archivo")
                {
                    lnkRecurso.NavigateUrl = ResolveUrl(leccion.UrlDocumento);
                    lnkRecurso.Text = "Descargar Archivo";
                }
                else
                {
                    lnkRecurso.NavigateUrl = leccion.UrlRecurso;
                    lnkRecurso.Text = "Ir al Enlace";
                }
            }

            // --- CONFIGURAR BOTÓN DE COMPLETADO ---

            bool yaVisto = this.LeccionesCompletadas.Contains(idLeccion);

            if (yaVisto)
            {
                btnCompletada.Text = "✔ Completada (Desmarcar)";
                btnCompletada.CssClass = "btn btn-success text-white w-100 shadow-sm";
                btnCompletada.CommandArgument = "Desmarcar";

               
                btnSiguiente.Visible = true;
            }
            else
            {
                btnCompletada.Text = "Marcar como Visto";
                btnCompletada.CssClass = "btn btn-outline-primary w-100 shadow-sm";
                btnCompletada.CommandArgument = "Marcar";

               
                btnSiguiente.Visible = false;
            }

            ConfigurarNavegacion(idLeccion);
        }

        // Botón Principal (Marcar/Desmarcar)
        protected void btnCompletada_Click(object sender, EventArgs e)
        {
            try
            {
                ProgresoLeccionNegocio pNeg = new ProgresoLeccionNegocio();
                Button btn = (Button)sender;

                if (btn.CommandArgument == "Marcar")
                {
                    pNeg.MarcarCompleta(this.IdInscripcionActual, this.IdLeccionActual);

                   
                    CargarProgreso();

                   
                    ConfigurarSidebarExamen();

                   
                    int siguiente = BuscarLeccionAdyacente(this.IdLeccionActual, true);

                    if (siguiente > 0)
                    {
                        Response.Redirect($"Aula.aspx?id={this.IdCursoActual}&leccion={siguiente}");
                    }
                    else
                    {
                        
                        Response.Redirect(Request.Url.AbsoluteUri);
                    }
                }
                else
                {
                    // Desmarcar
                    pNeg.EliminarProgreso(this.IdInscripcionActual, this.IdLeccionActual);

                    CargarProgreso();
                    ConfigurarSidebarExamen();

                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            catch { }
        }

        
        //NAVEGACIÓN Y HELPERS
        

        private void ConfigurarNavegacion(int idLeccionActual)
        {
            int idAnterior = BuscarLeccionAdyacente(idLeccionActual, false);
            int idSiguiente = BuscarLeccionAdyacente(idLeccionActual, true);  

            if (idAnterior > 0)
            {
                btnAnterior.Enabled = true;
                btnAnterior.CommandArgument = idAnterior.ToString();
            }
            else
            {
                btnAnterior.Enabled = false;
            }

            if (idSiguiente > 0)
            {
                btnSiguiente.Text = "Sig. →";
                btnSiguiente.CommandArgument = idSiguiente.ToString();
            }
            else
            {
                btnSiguiente.Text = "Finalizar Curso";
                btnSiguiente.CommandArgument = "FIN";
            }
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string arg = btn.CommandArgument;

            if (arg == "FIN")
            {
                Response.Redirect("~/Alumno/MisCursos.aspx");
            }
            else
            {
                Response.Redirect($"Aula.aspx?id={this.IdCursoActual}&leccion={arg}");
            }
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string arg = btn.CommandArgument;

            int idDestino = 0;
            if (int.TryParse(arg, out idDestino))
            {
                if (idDestino > 0)
                {
                    Response.Redirect($"Aula.aspx?id={this.IdCursoActual}&leccion={idDestino}");
                }
            }
        }

        // Método manual para encontrar lecciones adyacentes
        private int BuscarLeccionAdyacente(int idActual, bool buscarSiguiente)
        {
            ModuloNegocio mNeg = new ModuloNegocio();
            LeccionNegocio lNeg = new LeccionNegocio();

            // Aplanar la lista de lecciones en orden
            List<Modulo> modulos = mNeg.Listar(this.IdCursoActual);
            List<Leccion> listaPlana = new List<Leccion>();

            foreach (var m in modulos)
            {
                List<Leccion> leccionesDelModulo = lNeg.Listar(m.Id);
                listaPlana.AddRange(leccionesDelModulo);
            }

            // Buscamos índice
            for (int i = 0; i < listaPlana.Count; i++)
            {
                if (listaPlana[i].Id == idActual)
                {
                    if (buscarSiguiente)
                    {
                        if (i + 1 < listaPlana.Count) return listaPlana[i + 1].Id;
                    }
                    else
                    {
                        if (i - 1 >= 0) return listaPlana[i - 1].Id;
                    }
                }
            }
            return 0;
        }

        // Métodos auxiliares visuales
        private void CargarInfoCurso(int id)
        {
            try
            {
                CursoNegocio cn = new CursoNegocio();
                Curso c = cn.BuscarCurso(id);
                if (c != null)
                {
                    litNombreCursoSidebar.Text = c.Titulo;
                }
            }
            catch { }
        }

        private void CargarMenuLateral(int id)
        {
            ModuloNegocio mNeg = new ModuloNegocio();
            repModulos.DataSource = mNeg.Listar(id);
            repModulos.DataBind();
        }

        protected void repModulos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Modulo m = (Modulo)e.Item.DataItem;
                Repeater repHijo = (Repeater)e.Item.FindControl("repLecciones");

                if (repHijo != null)
                {
                    LeccionNegocio lNeg = new LeccionNegocio();
                    repHijo.DataSource = lNeg.Listar(m.Id);
                    repHijo.DataBind();
                }
            }
        }

        public string ObtenerIcono(object idObj, object tipoObj)
        {
            int id = Convert.ToInt32(idObj);
            if (this.LeccionesCompletadas.Contains(id)) return "bi bi-check-circle-fill text-success me-2";

            string tipo = tipoObj.ToString();
            if (tipo == "Video") return "bi bi-play-circle me-2";
            if (tipo == "Archivo") return "bi bi-file-earmark-text me-2";
            return "bi bi-link-45deg me-2";
        }

        private int ObtenerPrimeraLeccion(int idCurso)
        {
            ModuloNegocio mNeg = new ModuloNegocio();
            LeccionNegocio lNeg = new LeccionNegocio();
            List<Modulo> modulos = mNeg.Listar(idCurso);

            foreach (var m in modulos)
            {
                List<Leccion> lecciones = lNeg.Listar(m.Id);
                if (lecciones.Count > 0) return lecciones[0].Id;
            }
            return 0;
        }

        public string ObtenerNumeroLeccion(Control parent, Control item)
        {
            RepeaterItem itemModulo = (RepeaterItem)parent;
            RepeaterItem itemLeccion = (RepeaterItem)item;
            return (itemModulo.ItemIndex + 1) + "." + (itemLeccion.ItemIndex + 1);
        }

        public bool EsModuloActivo(object id) { return true; }
    }
}
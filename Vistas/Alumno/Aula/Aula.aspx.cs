using Dominio;
using Dominio.Cursada; // Para la clase Entrega y Certificado
using Negocio;
using Negocio.Contenido;
using Negocio.Cursada; // Para EntregaNegocio y ExamenNegocio
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Alumno.aula
{
    public partial class Aula : System.Web.UI.Page
    {
        // ============================================================
        // 1. PROPIEDADES DE ESTADO (Para no perder datos al recargar)
        // ============================================================

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

        // Lista en memoria para pintar rápido los check verdes del menú
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

        // ============================================================
        // 2. CARGA DE LA PÁGINA (Page_Load)
        // ============================================================

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // A. Validar que esté logueado
                if (Session["Usuario"] == null)
                {
                    Response.Redirect("~/Auth/Loguin.aspx");
                    return;
                }

                // B. Validar ID del curso en la URL
                string idCursoStr = Request.QueryString["id"];
                int idCurso = 0;

                if (string.IsNullOrEmpty(idCursoStr) || !int.TryParse(idCursoStr, out idCurso))
                {
                    Response.Redirect("~/Alumno/MisCursos.aspx");
                    return;
                }

                // C. Validar que el alumno tenga inscripción activa (que haya pagado)
                Usuario u = (Usuario)Session["Usuario"];
                InscripcionNegocio insNeg = new InscripcionNegocio();

                // Usamos var para simplificar, pero devuelve una Inscripcion
                var inscripcion = insNeg.ObtenerInscripcionActiva(u.UsuarioID, idCurso);

                if (inscripcion == null)
                {
                    // Si no tiene inscripción válida, lo mandamos al detalle para que compre
                    Response.Redirect($"~/CursoDetalle.aspx?id={idCurso}");
                    return;
                }

                // D. Guardar datos clave en memoria
                this.IdCursoActual = idCurso;
                this.IdInscripcionActual = inscripcion.Id;

                // E. Cargar el Progreso (Fundamental hacerlo antes de pintar el menú)
                CargarProgreso();

                // F. Determinar qué lección mostrar (¿Vino una en la URL o buscamos la primera?)
                string idLeccionStr = Request.QueryString["leccion"];
                int idLeccion = 0;

                if (!string.IsNullOrEmpty(idLeccionStr) && int.TryParse(idLeccionStr, out int parsedId))
                {
                    idLeccion = parsedId;
                }
                else
                {
                    // Buscamos la primera lección del curso
                    idLeccion = ObtenerPrimeraLeccion(idCurso);

                    // Si encontramos una, redirigimos para que la URL quede limpia
                    if (idLeccion > 0)
                    {
                        Response.Redirect($"Aula.aspx?id={idCurso}&leccion={idLeccion}");
                        return;
                    }
                }

                this.IdLeccionActual = idLeccion;

                // G. Pintar la pantalla
                CargarInfoCurso(idCurso);     // Título del curso arriba
                CargarMenuLateral(idCurso);   // Árbol de módulos

                // Si hay lección válida, cargamos su contenido (video/texto)
                if (idLeccion > 0)
                {
                    CargarContenidoLeccion(idLeccion);
                }

                // H. Configurar el botón de Examen Final (Si corresponde)
                ConfigurarSidebarExamen();
            }
        }

        // ============================================================
        // 3. LÓGICA DE PROGRESO (Backend <-> DB)
        // ============================================================

        private void CargarProgreso()
        {
            try
            {
                ProgresoLeccionNegocio pNeg = new ProgresoLeccionNegocio();

                // Traemos de la DB qué lecciones vio este alumno
                List<ProgresoLeccion> listaProgreso = pNeg.ListarProgreso(this.IdInscripcionActual);

                // Filtramos solo los IDs para usarlos fácil después
                List<int> listaSoloIds = new List<int>();
                foreach (var item in listaProgreso)
                {
                    listaSoloIds.Add(item.IdLeccion);
                }

                this.LeccionesCompletadas = listaSoloIds;

                // Pintamos la barra de porcentaje
                ActualizarBarraProgreso();
            }
            catch { }
        }

        private void ActualizarBarraProgreso()
        {
            try
            {
                // Calculamos el porcentaje en un método auxiliar limpio
                int porcentaje = CalcularPorcentajeInt();

                litPorcentaje.Text = porcentaje + "%";
                barraProgreso.Attributes["style"] = $"width: {porcentaje}%";
            }
            catch { }
        }

        // Método auxiliar que devuelve el número entero del porcentaje (0 a 100)
        private int CalcularPorcentajeInt()
        {
            try
            {
                ModuloNegocio mNeg = new ModuloNegocio();
                LeccionNegocio lNeg = new LeccionNegocio();

                List<Modulo> modulos = mNeg.Listar(this.IdCursoActual);

                int totalLecciones = 0;

                // Sumamos manual cuántas lecciones tiene el curso en total
                foreach (var m in modulos)
                {
                    int cantidadEnModulo = lNeg.Listar(m.Id).Count;
                    totalLecciones += cantidadEnModulo;
                }

                // Evitamos división por cero
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

        // ============================================================
        // 4. LÓGICA DEL EXAMEN Y ENTREGA (NUEVO)
        // ============================================================

        private void ConfigurarSidebarExamen()
        {
            try
            {
                ExamenNegocio exNeg = new ExamenNegocio();
                Examen examen = exNeg.ObtenerPorCurso(this.IdCursoActual);

                // Solo mostramos el panel si el curso tiene un examen activo
                if (examen != null && examen.EstaActivo)
                {
                    pnlSidebarExamen.Visible = true;

                    // Preparamos el link de la consigna por si entra
                    lnkDescargarConsigna.NavigateUrl = examen.UrlConsigna;

                    // Verificamos si completó el 100% de las clases
                    int porcentaje = CalcularPorcentajeInt();

                    if (porcentaje >= 100)
                    {
                        // DESBLOQUEADO: Mostramos el link verde
                        btnIrExamen.Visible = true;
                        divExamenBloqueado.Visible = false;
                    }
                    else
                    {
                        // BLOQUEADO: Mostramos el candado gris
                        btnIrExamen.Visible = false;
                        divExamenBloqueado.Visible = true;
                    }
                }
                else
                {
                    // Si no hay examen, ocultamos todo el bloque del sidebar
                    pnlSidebarExamen.Visible = false;
                }
            }
            catch { }
        }

        // Evento al hacer clic en "Examen Final" en el menú lateral
        protected void btnIrExamen_Click(object sender, EventArgs e)
        {
            // 1. Ocultamos todo lo que sea contenido de lecciones
            divVideo.Visible = false;
            pnlRecursoExterno.Visible = false;

            // Limpiamos títulos para que no confunda
            lblTituloLeccion.Text = "";
            litDescripcion.Text = "";

            // 2. Mostramos el panel principal del examen
            pnlVistaExamen.Visible = true;

            // 3. Cargamos el estado de la entrega (si ya entregó o no)
            CargarEstadoEntrega();
        }

        private void CargarEstadoEntrega()
        {
            EntregaNegocio entNeg = new EntregaNegocio();

            // Buscamos si el alumno ya hizo una entrega para esta inscripción
            Entrega entrega = entNeg.ObtenerUltimaEntrega(this.IdInscripcionActual);

            if (entrega == null)
            {
                // CASO A: Nunca entregó nada -> Mostrar Formulario de carga
                pnlFormularioEntrega.Visible = true;
                pnlEstadoEntrega.Visible = false;
            }
            else
            {
                // CASO B: Ya entregó -> Mostrar Estado
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

                    // Habilitamos el botón para que pueda volver a ver el formulario
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
                // Validación simple
                if (string.IsNullOrWhiteSpace(txtLinkEntrega.Text))
                {
                    return;
                }

                // Recuperamos el ID del examen
                ExamenNegocio exNeg = new ExamenNegocio();
                Examen ex = exNeg.ObtenerPorCurso(this.IdCursoActual);

                // Guardamos la entrega
                EntregaNegocio entNeg = new EntregaNegocio();
                entNeg.RegistrarEntrega(this.IdInscripcionActual, ex.Id, txtLinkEntrega.Text);

                // Recargamos la pantalla para que vea el estado "Pendiente"
                CargarEstadoEntrega();
            }
            catch (Exception)
            {
                // Manejar error si hace falta
            }
        }

        // Botón "Volver a Entregar" (solo si fue rechazado)
        protected void btnReintentar_Click(object sender, EventArgs e)
        {
            pnlEstadoEntrega.Visible = false;
            pnlFormularioEntrega.Visible = true;
            txtLinkEntrega.Text = ""; // Limpiamos el campo
        }

        // ============================================================
        // 5. CARGA DE CONTENIDO (Lecciones de video/archivo)
        // ============================================================

        private void CargarContenidoLeccion(int idLeccion)
        {
            // IMPORTANTE: Si estamos cargando una lección, OCULTAMOS el panel de examen
            pnlVistaExamen.Visible = false;

            LeccionNegocio lNeg = new LeccionNegocio();
            ModuloNegocio mNeg = new ModuloNegocio();

            Leccion leccion = lNeg.Obtener(idLeccion);
            if (leccion == null) return;

            Modulo modulo = mNeg.Obtener(leccion.IdModulo);

            // Armar título (Ej: 1.2 - Introducción)
            string numeracion = "";
            if (modulo != null)
            {
                numeracion = modulo.Orden + "." + leccion.Orden;
            }

            lblTituloLeccion.Text = numeracion + " - " + leccion.Titulo;

            // Descripción
            if (string.IsNullOrEmpty(leccion.Descripcion))
            {
                litDescripcion.Text = "Sin descripción.";
            }
            else
            {
                litDescripcion.Text = leccion.Descripcion;
            }

            // --- MANEJO DE VIDEO O ARCHIVO (Logica if/else clara) ---

            if (leccion.TipoMaterial == "Video")
            {
                divVideo.Visible = true;
                pnlRecursoExterno.Visible = false;

                string url = leccion.UrlRecurso;
                // Transformar URL de Youtube si es necesario para embeber
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

                // Si ya la vio, mostramos el botón "Siguiente"
                btnSiguiente.Visible = true;
            }
            else
            {
                btnCompletada.Text = "Marcar como Visto";
                btnCompletada.CssClass = "btn btn-outline-primary w-100 shadow-sm";
                btnCompletada.CommandArgument = "Marcar";

                // Si no la vio, ocultamos "Siguiente" para obligarlo a marcar visto
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

                    // Refrescamos el progreso en memoria y visualmente
                    CargarProgreso();

                    // IMPORTANTE: Al marcar progreso, chequeamos si se desbloquea el examen
                    ConfigurarSidebarExamen();

                    // Avanzamos automático a la siguiente lección
                    int siguiente = BuscarLeccionAdyacente(this.IdLeccionActual, true);

                    if (siguiente > 0)
                    {
                        Response.Redirect($"Aula.aspx?id={this.IdCursoActual}&leccion={siguiente}");
                    }
                    else
                    {
                        // Si era la última, recargamos la página actual
                        Response.Redirect(Request.Url.AbsoluteUri);
                    }
                }
                else
                {
                    // Desmarcar
                    pNeg.EliminarProgreso(this.IdInscripcionActual, this.IdLeccionActual);

                    CargarProgreso();
                    ConfigurarSidebarExamen(); // Puede que se vuelva a bloquear el examen

                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            catch { }
        }

        // ============================================================
        // 6. NAVEGACIÓN Y HELPERS
        // ============================================================

        private void ConfigurarNavegacion(int idLeccionActual)
        {
            int idAnterior = BuscarLeccionAdyacente(idLeccionActual, false); // false = anterior
            int idSiguiente = BuscarLeccionAdyacente(idLeccionActual, true);  // true = siguiente

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

        // Método manual para encontrar lecciones adyacentes (SIN LINQ)
        private int BuscarLeccionAdyacente(int idActual, bool buscarSiguiente)
        {
            ModuloNegocio mNeg = new ModuloNegocio();
            LeccionNegocio lNeg = new LeccionNegocio();

            // Aplanamos la lista de lecciones en orden
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
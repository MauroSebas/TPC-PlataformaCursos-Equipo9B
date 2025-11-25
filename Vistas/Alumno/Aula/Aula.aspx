<%@ Page Title="" Language="C#" MasterPageFile="~/Alumno/aula/Aula.Master" AutoEventWireup="true" CodeBehind="Aula.aspx.cs" Inherits="Vistas.Alumno.aula.Aula" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Layout Principal */
        .aula-container {
            display: flex;
            height: calc(100vh - 56px);
            overflow: hidden;
            position: relative;
            background-color: var(--bs-body-bg); /* Color de fondo automático */
        }

        /* Sidebar con Transición */
        .aula-sidebar {
            width: 350px;
            flex-shrink: 0;
            /* CAMBIO: Usar variable de borde en lugar de #f0f0f0 */
            border-right: 1px solid var(--bs-border-color); 
            /* CAMBIO: Usar variable de fondo en lugar de #fff */
            background-color: var(--bs-body-bg); 
            overflow-y: auto;
            transition: margin-left 0.3s ease;
        }
        
        /* Clase para ocultar sidebar */
        .aula-sidebar.toggled {
            margin-left: -350px;
        }

        .aula-content {
            flex-grow: 1;
            overflow-y: auto;
            padding: 2rem;
            position: relative;
            transition: all 0.3s ease;
            /* Asegura que el texto se adapte al tema */
            color: var(--bs-body-color); 
        }

        /* Items del Menú */
        .sidebar-item {
            display: flex; 
            align-items: center; 
            padding: 12px 16px;
            border-left: 3px solid transparent; 
            transition: all 0.2s;
            text-decoration: none;
            /* CAMBIO: Color de texto automático */
            color: var(--bs-body-color); 
        }

        /* Hover automático según el tema */
        .sidebar-item:hover { 
            background-color: var(--bs-tertiary-bg); 
            color: var(--bs-primary); 
        }

        /* Item Activo */
        .sidebar-item.active { 
            background-color: var(--bs-primary-bg-subtle); /* Azulito suave que cambia en dark */
            color: var(--bs-primary-text-emphasis); 
            font-weight: 600; 
            border-left: 3px solid var(--bs-primary); 
        }

        .sidebar-item i { 
            font-size: 1.1rem; 
            width: 28px; 
            display: flex; 
            justify-content: center; 
        }

        /* Botón Toggle */
        .btn-toggle-sidebar {
            cursor: pointer;
            border: none;
            background: transparent;
            font-size: 1.5rem;
            /* CAMBIO: Color del ícono automático */
            color: var(--bs-body-color); 
            padding: 0;
            margin-right: 1rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="aula-container">

        <aside class="aula-sidebar shadow-sm" id="sidebarAula">
            
            <div class="p-4 border-bottom  sticky-top">
                <h6 class="fw-bold  mb-3 lh-sm">
                    <asp:Literal ID="litNombreCursoSidebar" runat="server"></asp:Literal>
                </h6>
                
                <div class="d-flex justify-content-between align-items-end mb-1">
                    <span class="small text-muted fw-bold">Tu Progreso</span>
                    <span class="small text-primary fw-bold">
                        <asp:Literal ID="litPorcentaje" runat="server" Text="0%"></asp:Literal>
                    </span>
                </div>
                <div class="progress" style="height: 6px; border-radius: 10px;">
                    <div id="barraProgreso" runat="server" class="progress-bar bg-primary" role="progressbar" style="width: 0%"></div>
                </div>
            </div>

            <div class="accordion accordion-flush" id="accordionCurso">
                <asp:Repeater ID="repModulos" runat="server" OnItemDataBound="repModulos_ItemDataBound">
                    <ItemTemplate>
                        <div class="accordion-item">
                            <h2 class="accordion-header">
                                <button class="accordion-button <%# Convert.ToBoolean(Eval("EstaActivo")) ? "" : "collapsed" %>" type="button" data-bs-toggle="collapse" data-bs-target='#collapse<%# Eval("Id") %>'>
                                    Módulo <%# Eval("Orden") %>: <%# Eval("Nombre") %>
                                </button>
                            </h2>
                            <div id='collapse<%# Eval("Id") %>' class='accordion-collapse collapse <%# EsModuloActivo(Eval("Id")) ? "show" : "" %>' data-bs-parent="#accordionCurso">
                                <div class="accordion-body">
                                    <asp:Repeater ID="repLecciones" runat="server">
                                        <ItemTemplate>
                                            <a href='Aula.aspx?id=<%# Request.QueryString["id"] %>&leccion=<%# Eval("Id") %>' 
                                               class='sidebar-item <%# Convert.ToInt32(Eval("Id")) == IdLeccionActual ? "active" : "" %>'>
                                                <i class='<%# ObtenerIcono(Eval("Id"), Eval("TipoMaterial")) %>'></i>
                                                <span class="text-truncate">
                                                    <%# ObtenerNumeroLeccion(Container.Parent.Parent, Container) %> - <%# Eval("Titulo") %>
                                                </span>
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pnlSidebarExamen" runat="server" Visible="false" CssClass="mt-3 pt-3 border-top px-3">
    <small class="text-uppercase text-muted fw-bold" style="font-size: 0.7rem; letter-spacing: 1px;">Evaluación</small>
    
    <asp:LinkButton ID="btnIrExamen" runat="server" OnClick="btnIrExamen_Click" Visible="false"
        CssClass="d-flex align-items-center p-3 mt-2 rounded border border-success bg-success bg-opacity-10 text-decoration-none text-success shadow-sm action-hover">
        <i class="bi bi-trophy-fill fs-4 me-3"></i>
        <div class="lh-1 text-start">
            <span class="fw-bold d-block mb-1">Examen Final</span>
            <small>¡Estás listo! Ingresar</small>
        </div>
    </asp:LinkButton>

    <div id="divExamenBloqueado" runat="server" class="d-flex align-items-center p-3 mt-2 rounded border  text-muted opacity-75">
        <i class="bi bi-lock-fill fs-4 me-3"></i>
        <div class="lh-1">
            <span class="fw-bold d-block mb-1">Examen Final</span>
            <small>Completa el 100% para desbloquear</small>
        </div>
    </div>
</asp:Panel>
            </div>
        </aside>

        <main class="aula-content">
            <div class="container-fluid p-0" style="max-width: 1100px; margin: 0 auto;">
                
                <div class="d-flex align-items-center mb-3">
                    <button type="button" class="btn-toggle-sidebar" onclick="toggleSidebar()">
                        <i class="bi bi-list"></i>
                    </button>
                    <span class="text-muted small">Clase Actual</span>
                </div>

                <asp:UpdatePanel ID="updContenido" runat="server">
                    <ContentTemplate>
                        
                        <div id="divVideo" runat="server" visible="false" class="mb-4">
                            <div class="ratio ratio-16x9 rounded-4 shadow-lg overflow-hidden " style="border: 1px solid var(--bs-border-color);">
                                <iframe id="iframeVideo" runat="server" allowfullscreen style="border:0;"></iframe>
                            </div>
                        </div>
                        <asp:Panel ID="pnlVistaExamen" runat="server" Visible="false" CssClass="fade-in">
    
    <div class="text-center mb-5">
        <div class="d-inline-flex align-items-center justify-content-center bg-primary  rounded-circle mb-3" style="width: 64px; height: 64px;">
            <i class="bi bi-file-earmark-text-fill fs-2"></i>
        </div>
        <h2 class="fw-bold">Examen Final</h2>
        <p class="text-muted">Demostrá lo que aprendiste para obtener tu certificado.</p>
    </div>

    <div class="row justify-content-center">
        <div class="col-lg-8">
            <div class="card border-0 shadow-sm rounded-4 overflow-hidden">
                <div class="card-body p-4 p-lg-5">

                    <div class="mb-4">
                        <h5 class="fw-bold mb-3">1. Descargar Consigna</h5>
                        <div class="d-flex align-items-center p-3 border rounded bg-body-tertiary">
                            <i class="bi bi-file-pdf fs-1 text-danger me-3"></i>
                            <div class="flex-grow-1">
                                <h6 class="mb-0 fw-bold">Trabajo Práctico Final</h6>
                                <small class="text-muted">Lee atentamente las instrucciones antes de empezar.</small>
                            </div>
                            <asp:HyperLink ID="lnkDescargarConsigna" runat="server" Target="_blank" CssClass="btn btn-outline-primary btn-sm fw-bold">
                                <i class="bi bi-download me-2"></i>Descargar
                            </asp:HyperLink>
                        </div>
                    </div>

                    <hr class="my-4 opacity-10" />

                    <h5 class="fw-bold mb-3">2. Tu Entrega</h5>

                    <asp:Panel ID="pnlFormularioEntrega" runat="server">
                        <div class="form-group mb-3">
                            <label class="form-label small fw-bold text-muted">Link de tu resolución (Google Drive / GitHub)</label>
                            <div class="input-group">
                                <span class="input-group-text "><i class="bi bi-link-45deg"></i></span>
                                <asp:TextBox ID="txtLinkEntrega" runat="server" CssClass="form-control" placeholder="https://..." />
                            </div>
                            <div class="form-text x-small">Asegurate de que el link sea público o accesible para el profesor.</div>
                        </div>
                        <div class="d-grid">
                            <asp:Button ID="btnEntregarExamen" runat="server" Text="Enviar a Corrección" 
                                CssClass="btn btn-primary py-2 fw-bold" OnClick="btnEntregarExamen_Click" />
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlEstadoEntrega" runat="server" Visible="false" CssClass="text-center py-3  rounded border">
                        <asp:Literal ID="litIconoEstado" runat="server" />
                        
                        <h4 class="fw-bold mt-2"><asp:Literal ID="litTituloEstado" runat="server" /></h4>
                        <p class="text-muted mb-3"><asp:Literal ID="litMensajeEstado" runat="server" /></p>

                        <asp:Panel ID="pnlFeedback" runat="server" Visible="false" CssClass="alert alert-secondary text-start d-inline-block w-100 mb-0">
                            <strong><i class="bi bi-chat-quote-fill me-2"></i>Devolución del Profesor:</strong>
                            <br />
                            <asp:Literal ID="litFeedback" runat="server" />
                        </asp:Panel>

                        <asp:Button ID="btnReintentar" runat="server" Text="Volver a Entregar" Visible="false" 
                            CssClass="btn btn-outline-danger btn-sm mt-3" OnClick="btnReintentar_Click" />
                    </asp:Panel>

                </div>
            </div>
        </div>
    </div>
</asp:Panel>

                        <asp:Panel ID="pnlRecursoExterno" runat="server" Visible="false" CssClass="card mb-4 border-0 shadow-sm rounded-4 bg-body">
                            <div class="card-body text-center py-5">
                                <h4 class="fw-bold mb-2">Material Complementario</h4>
                                <asp:HyperLink ID="lnkRecurso" runat="server" Target="_blank" CssClass="btn btn-primary px-5 py-2 rounded-pill shadow-sm">
                                    <i class="bi bi-download me-2"></i>Acceder
                                </asp:HyperLink>
                            </div>
                        </asp:Panel>

                        <div class="row">
                            <div class="col-lg-9">
                                <h2 class="fw-bold mb-3 ">
                                    <asp:Label ID="lblTituloLeccion" runat="server"></asp:Label>
                                </h2>
                                
                                <div class="mb-5">
                                    <h5 class="fw-bold border-bottom pb-2 mb-3 text-secondary" style="font-size: 1rem;">Descripción</h5>
                                    <p class="text-secondary" style="line-height: 1.8;">
                                        <asp:Literal ID="litDescripcion" runat="server"></asp:Literal>
                                    </p>
                                </div>
                            </div>

                            <div class="col-lg-3">
                                <div class="d-grid gap-2 sticky-top" style="top: 20px;">
                                    <asp:Button ID="btnCompletada" runat="server" CssClass="btn py-2 fw-bold shadow-sm" OnClick="btnCompletada_Click" />
                                    
                                    <div class="d-flex gap-2 mt-2">
                                        <asp:Button ID="btnAnterior" runat="server" Text="← Ant." CssClass="btn btn-outline-secondary flex-fill" OnClick="btnAnterior_Click" />
                                        <asp:Button ID="btnSiguiente" runat="server" Text="Sig. →" CssClass="btn btn-outline-secondary flex-fill" OnClick="btnSiguiente_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </main>
    </div>

    <script>
        function toggleSidebar() {
            document.getElementById('sidebarAula').classList.toggle('toggled');
        }
    </script>

</asp:Content>
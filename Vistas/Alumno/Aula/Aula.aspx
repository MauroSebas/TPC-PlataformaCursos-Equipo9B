<%@ Page Title="" Language="C#" MasterPageFile="~/Alumno/aula/Aula.Master" AutoEventWireup="true" CodeBehind="Aula.aspx.cs" Inherits="Vistas.Alumno.aula.Aula" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Estilos específicos para el layout del Aula */
        .aula-sidebar {
            width: 320px;
            border-right: 1px solid #dee2e6;
            background-color: #f8f9fa;
            overflow-y: auto;
        }

        .aula-content {
            flex: 1;
            overflow-y: auto;
            padding: 2rem;
            background-color: #fff;
        }

        /* Estilo de los items del menú lateral */
        .sidebar-item {
            display: flex;
            align-items: center;
            padding: 10px 15px;
            text-decoration: none;
            color: #495057;
            border-radius: 6px;
            margin-bottom: 2px;
            transition: all 0.2s;
        }

        .sidebar-item:hover {
            background-color: #e9ecef;
            color: #000;
        }

        .sidebar-item.active {
            background-color: #e7f1ff;
            color: #0d6efd;
            font-weight: 600;
        }

        .sidebar-item i {
            font-size: 1.2rem;
            width: 30px;
            text-align: center;
        }

        /* Ajuste fino para el acordeón */
        .accordion-button {
            background-color: transparent !important;
            box-shadow: none !important;
            padding: 1rem 1.25rem;
            font-weight: 600;
        }
        .accordion-button:not(.collapsed) {
            color: #0d6efd;
            background-color: rgba(13, 110, 253, 0.05) !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex h-100">

        <aside class="aula-sidebar d-none d-md-block">
            
            <div class="p-3 border-bottom">
                <h6 class="fw-bold text-dark mb-2">Contenido del Curso</h6>
                <div class="progress" style="height: 6px;">
                    <div class="progress-bar bg-success" role="progressbar" style="width: 25%"></div>
                </div>
                <small class="text-muted mt-1 d-block" style="font-size: 0.75rem;">25% Completado</small>
            </div>

            <div class="accordion accordion-flush" id="accordionCurso">
                
                <asp:Repeater ID="repModulos" runat="server" OnItemDataBound="repModulos_ItemDataBound">
                    <ItemTemplate>
                        <div class="accordion-item bg-transparent">
                            <h2 class="accordion-header">
                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target='#collapse<%# Eval("Id") %>'>
                                    Módulo <%# Eval("Orden") %>: <%# Eval("Nombre") %>
                                </button>
                            </h2>
                            <div id='collapse<%# Eval("Id") %>' class="accordion-collapse collapse" data-bs-parent="#accordionCurso">
                                <div class="accordion-body p-2">
                                    
                                    <asp:Repeater ID="repLecciones" runat="server">
                                        <ItemTemplate>
                                            <a href='Aula.aspx?id=<%# Request.QueryString["id"] %>&leccion=<%# Eval("Id") %>' 
                                               class='sidebar-item <%# Convert.ToInt32(Eval("Id")) == IdLeccionActual ? "active" : "" %>'>
                                                
                                                <i class='<%# ObtenerIcono(Eval("TipoMaterial")) %>'></i>
                                                
                                                <span class="text-truncate small"><%# Eval("Orden") %>. <%# Eval("Titulo") %></span>
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
        </aside>

        <main class="aula-content">
            <div class="container-fluid" style="max-width: 1000px;">
                
                <asp:UpdatePanel ID="updContenido" runat="server">
                    <ContentTemplate>
                        
                        <h2 class="fw-bold mb-3">
                            <asp:Label ID="lblTituloLeccion" runat="server" Text="Selecciona una lección..."></asp:Label>
                        </h2>

                        <div class="ratio ratio-16x9 bg-dark rounded mb-4 shadow-sm" id="divVideo" runat="server" visible="false">
                            <iframe id="iframeVideo" runat="server" allowfullscreen></iframe>
                        </div>

                        <asp:Panel ID="pnlRecursoExterno" runat="server" Visible="false" CssClass="card mb-4 bg-light border-0">
                            <div class="card-body text-center py-5">
                                <i class="bi bi-file-earmark-text fs-1 text-primary mb-3 d-block"></i>
                                <h5 class="fw-bold">Material de Descarga</h5>
                                <p class="text-muted">Esta clase contiene un archivo adjunto o enlace externo.</p>
                                <asp:HyperLink ID="lnkRecurso" runat="server" Target="_blank" CssClass="btn btn-primary px-4 rounded-pill">
                                    <i class="bi bi-download me-2"></i>Acceder al Recurso
                                </asp:HyperLink>
                            </div>
                        </asp:Panel>

                        <div class="mb-5">
                            <h5 class="fw-bold border-bottom pb-2 mb-3">Sobre esta clase</h5>
                            <p class="text-secondary" style="line-height: 1.7;">
                                <asp:Literal ID="litDescripcion" runat="server"></asp:Literal>
                            </p>
                        </div>

                        <div class="d-flex justify-content-between align-items-center pt-4 border-top">
                            <asp:Button ID="btnAnterior" runat="server" Text="← Anterior" CssClass="btn btn-outline-secondary" OnClick="btnAnterior_Click" Enabled="false" />
                            <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente →" CssClass="btn btn-primary px-4" OnClick="btnSiguiente_Click" />
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </main>

    </div>

</asp:Content>
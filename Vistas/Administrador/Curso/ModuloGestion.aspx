<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="ModuloGestion.aspx.cs" Inherits="Vistas.Aministrador.ModuloGestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:HiddenField ID="hfIdModuloEliminar" runat="server" Value="0" />



    <div class="d-flex flex-wrap justify-content-between align-items-center gap-3 mb-4 border-bottom pb-3">
        <div class="d-flex flex-column gap-1">
            <h1 class="h3 fw-bolder text-body-emphasis mb-0">
                <asp:Literal ID="litTituloCurso" runat="server" Text="Gestionar Módulos" />
            </h1>
            <p class="text-body-secondary fs-6 mb-0">Organiza el contenido del curso en capítulos o unidades.</p>
        </div>
        <div>
            <asp:HyperLink ID="btnVolver" runat="server" NavigateUrl="~/Administrador/Curso/CursoPanel.aspx"
                CssClass="btn btn-outline-secondary d-flex align-items-center gap-2 fw-bold small">
                <i class="bi bi-arrow-left"></i>
                <span>Volver a Cursos</span>
            </asp:HyperLink>
        </div>
    </div>

    <asp:UpdatePanel ID="updMensaje" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlMensaje" runat="server" Visible="false" EnableViewState="false" CssClass="alert">
                <asp:Literal ID="litMensaje" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="row g-4">

        <div class="col-lg-4">
            <div class="card border-0 shadow-sm rounded-lg sticky-top" style="top: 20px;">
                <div class="card-header  border-bottom py-3">
                    <h6 class="mb-0 fw-bold"><i class="bi bi-plus-circle me-2 text-primary"></i>Módulo</h6>
                </div>
                <div class="card-body p-4">

                    <asp:UpdatePanel ID="updFormulario" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:HiddenField ID="hfIdModuloEditar" runat="server" Value="0" />

                            <div class="mb-3">
                                <label class="form-label small fw-bold text-muted">Nombre del Módulo</label>
                                <asp:TextBox ID="txtNombreModulo" runat="server" CssClass="form-control" placeholder="Ej: Introducción..." ValidationGroup="Modulo"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombreModulo"
                                    ErrorMessage="El nombre es obligatorio" CssClass="text-danger small" Display="Dynamic" ValidationGroup="Modulo" />
                            </div>

                            <div class="d-grid gap-2">
                                <asp:Button ID="btnAgregar" runat="server" Text="Agregar al Final" CssClass="btn btn-primary"
                                    OnClick="btnAgregar_Click" ValidationGroup="Modulo" />

                                <asp:Button ID="btnCancelarEdicion" runat="server" Text="Cancelar" CssClass="btn btn-link text-muted btn-sm"
                                    OnClick="btnCancelarEdicion_Click" Visible="false" CausesValidation="false" />
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>

        <div class="col-lg-8">
            <div class="card border-0 shadow-sm rounded-lg">
                <div class="card-header bg-body-tertiary border-bottom-0 py-3">
                    <h6 class="mb-0 fw-bold">Estructura del Curso</h6>
                </div>

                <div class="card-body p-0">
                    <asp:UpdatePanel ID="updGrilla" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <asp:GridView ID="gvModulos" runat="server" CssClass="table table-hover align-middle mb-0"
                                AutoGenerateColumns="false" DataKeyNames="Id" GridLines="None"
                                OnRowCommand="gvModulos_RowCommand">
                                <Columns>

                                    <asp:TemplateField HeaderText="Orden" ItemStyle-Width="120px" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <div class="d-flex align-items-center justify-content-center gap-1">
                                                <asp:LinkButton ID="btnSubir" runat="server" CommandName="Subir" CommandArgument='<%# Eval("Id") %>'
                                                    CssClass="btn btn-sm border-0 p-1"
                                                    Style="color: #4CAF50;" ToolTip="Mover Arriba">
                                                    <i class="bi bi-arrow-up-short fs-5"></i>
                                                </asp:LinkButton>

                                                <span class="badge fw-bold"
                                                    style="min-width: 25px; background-color: #bdbdbd; color: #1a1a1a;">
                                                    <%# Eval("Orden") %>
                                                </span>

                                                <asp:LinkButton ID="btnBajar" runat="server" CommandName="Bajar" CommandArgument='<%# Eval("Id") %>'
                                                    CssClass="btn btn-sm border-0 p-1"
                                                    Style="color: #4CAF50;" ToolTip="Mover Abajo">
                                                    <i class="bi bi-arrow-down-short fs-5"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nombre del Módulo">
                                        <ItemTemplate>
                                            <span class="fw-medium"><%# Eval("Nombre") %></span>
                                            <div class="small text-muted">
                                                <%# Eval("CantidadLecciones") %> Lecciones
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-end pe-4">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("Id") %>'
                                                CssClass="btn btn-sm btn-outline-warning border-0 me-1" ToolTip="Editar Nombre">
                                                <i class="bi bi-pencil-square"></i>
                                            </asp:LinkButton>

                                            <asp:HyperLink ID="hlLecciones" runat="server"
                                                NavigateUrl='<%# Eval("Id", "~/Administrador/Curso/LeccionPanel.aspx?id={0}") %>'
                                                CssClass="btn btn-sm btn-outline-info border-0 me-1" ToolTip="Ver Lecciones">
                                                    <i class="bi bi-collection-play"></i>
                                                </asp:HyperLink>

                                            <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'
                                                CssClass="btn btn-sm btn-outline-danger border-0" ToolTip="Eliminar"
                                                OnClientClick='<%# "mostrarModalEliminar(" + Eval("Id") + "); return false;" %>'>
                                                <i class="bi bi-trash"></i>
                                            </asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="text-center py-5 text-muted">
                                        <i class="bi bi-folder2-open fs-1 opacity-50"></i>
                                        <p class="mt-2">Este curso no tiene módulos todavía.</p>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

    </div>

    <div class="modal fade" id="modalConfirmaEliminar" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title"><i class="bi bi-exclamation-triangle-fill me-2"></i>Eliminar Módulo</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p class="mb-1">¿Estás seguro de que querés eliminar este módulo?</p>
                    <p class="small text-muted">Se ocultarán también todas las lecciones que contenga.</p>
                </div>
                <div class="modal-footer border-0">
                    <button type="button" class="btn btn-light" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Sí, Eliminar"
                        CssClass="btn btn-danger" OnClick="btnConfirmarEliminar_Click" />
                </div>
            </div>
        </div>
    </div>

    <script>
        function mostrarModalEliminar(idModulo) {
            var hiddenField = document.getElementById('<%= hfIdModuloEliminar.ClientID %>');
            hiddenField.value = idModulo;
            var myModal = new bootstrap.Modal(document.getElementById('modalConfirmaEliminar'));
            myModal.show();
        }
    </script>
</asp:Content>

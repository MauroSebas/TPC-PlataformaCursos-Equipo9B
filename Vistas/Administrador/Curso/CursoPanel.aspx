<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CursoPanel.aspx.cs" Inherits="Vistas.Aministrador.CursoPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-hover > tbody > tr:hover > * {
            --bs-table-hover-bg: var(--bs-tertiary-bg);
        }

        .card:hover {
            transform: none;
            box-shadow: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- 1. Encabezado de la Página -->
    <div class="d-flex flex-wrap justify-content-between align-items-center gap-3 mb-4 border-bottom pb-3">
        <div class="d-flex flex-column gap-1">
            <h1 class="h3 fw-bolder text-body-emphasis mb-0">Gestión de Cursos</h1>
            <p class="text-body-secondary fs-6 mb-0">Administra todos los cursos de tu plataforma.</p>
        </div>
        <div>
            <asp:HyperLink ID="btnCrearCurso" runat="server"
                NavigateUrl="~/Administrador/Curso/CursoForm.aspx"
                CssClass="btn btn-primary d-flex align-items-center gap-2 fw-bold small">
                <i class="bi bi-plus-circle fs-6"></i>
                <span>Crear Nuevo Curso</span>
            </asp:HyperLink>
        </div>
    </div>

    <!-- Panel de Mensajes  -->
    <asp:UpdatePanel ID="updMensajeGlobal" runat="server" UpdateMode="Conditional">
        <contenttemplate>
            <asp:Panel ID="pnlMensajeGlobal" runat="server" Visible="false" EnableViewState="false" CssClass="alert">
                <asp:Literal ID="litMensajeGlobal" runat="server" />
            </asp:Panel>
        </contenttemplate>
    </asp:UpdatePanel>

    <!-- 2. Filtros -->
    <asp:UpdatePanel ID="updFiltros" runat="server" UpdateMode="Conditional">
        <contenttemplate>
            <div class="card p-4 mb-4 rounded-3 bg-body">
                <div class="row g-3 align-items-center">
                    <div class="col-md-5">
                        <asp:Label ID="lblBuscar" runat="server" Text="Buscar por título" CssClass="form-label small fw-medium" AssociatedControlID="txtBuscar" />
                        <div class="input-group">
                            <span class="input-group-text"><i class="bi bi-search"></i></span>
                            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar curso..." />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblCategoria" runat="server" Text="Filtrar por Categoría" CssClass="form-label small fw-medium" AssociatedControlID="ddlCategoriaFiltro" />
                        <asp:DropDownList ID="ddlCategoriaFiltro" runat="server" CssClass="form-select"
                            AppendDataBoundItems="true" DataTextField="Nombre" DataValueField="Id">
                            <asp:ListItem Text="-- Todas las Categorías --" Value="0" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 d-flex align-self-end">
                        <div class="d-grid gap-2 d-md-flex w-100">
                            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" OnClick="btnFiltrar_Click" />
                            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary w-100" OnClick="btnLimpiar_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>
            </div>
        </contenttemplate>
    </asp:UpdatePanel>

    <!-- 3. Grilla de Cursos -->
    <asp:UpdatePanel ID="updGrillaCursos" runat="server" UpdateMode="Conditional">
        <contenttemplate>
            <div class="card border-0 rounded-lg">
                <div class="card-header bg-body-tertiary border-bottom-0">
                    <h5 class="mb-0">Cursos Registrados</h5>
                </div>
                <div class="card-body p-0">
                    <asp:GridView ID="gvCursos" runat="server"
                        CssClass="table table-striped ..."
                        AutoGenerateColumns="false"
                        DataKeyNames="Id"
                        AllowPaging="true" PageSize="10"
                        OnPageIndexChanging="gvCursos_PageIndexChanging"
                        OnRowCreated="gvCursos_RowCreated">

                        <columns>
                            <asp:BoundField DataField="Titulo" HeaderText="Título" HeaderStyle-CssClass="px-4" ItemStyle-CssClass="px-4 fw-medium" />
                            <asp:BoundField DataField="Categoria.Nombre" HeaderText="Categoría" />
                            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
                            <asp:TemplateField HeaderText="Publicado" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <itemtemplate>
                                    <div class="form-check form-switch d-flex justify-content-center align-items-center m-0 p-0">

                                        <asp:CheckBox ID="chkPublicado" runat="server"
                                            Checked='<%# Bind("Publicado") %>'
                                            AutoPostBack="true"
                                            OnCheckedChanged="chkPublicado_CheckedChanged" />
                                    </div>
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="text-end" ItemStyle-CssClass="text-end px-4">
                                <itemtemplate>
                                    <div class="d-flex justify-content-end gap-2">
                                        <asp:HyperLink ID="hlEditar" runat="server"
                                            NavigateUrl='<%# Eval("Id", "~/Administrador/Curso/CursoForm.aspx?id={0}") %>'
                                            CssClass="btn btn-sm btn-outline-secondary" ToolTip="Editar Curso">
                                            <i class="bi bi-pencil"></i>
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="hlModulos" runat="server"
                                            NavigateUrl='<%# Eval("Id", "~/Administrador/Curso/ModuloGestion.aspx?id={0}") %>'
                                            CssClass="btn btn-sm btn-outline-info" ToolTip="Gestionar Módulos/Lecciones">
                                            <i class="bi bi-list-task"></i>
                                        </asp:HyperLink>
                                        <asp:LinkButton ID="btnArchivar" runat="server"
                                            CssClass="btn btn-sm btn-outline-danger" ToolTip="Archivar Curso"
                                            OnClientClick='<%# "mostrarModalEliminar(" + Eval("Id") + "); return false;" %>'>
                                            <i class="bi bi-archive"></i>
                                        </asp:LinkButton>
                                    </div>
                                </itemtemplate>
                            </asp:TemplateField>
                        </columns>
                        <emptydatatemplate>
                            <div class="text-center py-5">
                                <i class="bi bi-book-half fs-1 text-body-tertiary"></i>
                                <h4 class="mt-3 fw-bold">No se encontraron cursos</h4>
                                <p class="text-body-secondary">Aún no se han creado cursos. ¡Empezá creando el primero!</p>
                                <asp:HyperLink ID="btnCrearVacio" runat="server"
                                    NavigateUrl="~/Administrador/Curso/CursoForm.aspx"
                                    CssClass="btn btn-primary mt-3">
                                    <i class="bi bi-plus-circle me-2"></i>Crear Nuevo Curso
                                </asp:HyperLink>
                            </div>
                        </emptydatatemplate>
                        <pagerstyle cssclass="p-3" />
                        <pagersettings
                            mode="NumericFirstLast"
                            position="Bottom"
                            pagebuttoncount="5"
                            firstpagetext="&laquo;"
                            lastpagetext="&raquo;"
                            nextpagetext="&rsaquo;"
                            previouspagetext="&lsaquo;" />
                    </asp:GridView>
                </div>
            </div>
        </contenttemplate>
        <triggers>
            <asp:AsyncPostBackTrigger ControlID="btnFiltrar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
        </triggers>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hfIdCursoEliminar" runat="server" />

    <div class="modal fade" id="modalConfirmaEliminar" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title">
                        <i class="bi bi-exclamation-triangle-fill me-2"></i>Confirmar Eliminación
                    </h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p>¿Estás seguro de que querés archivar este curso?</p>
                    <p class="small text-muted">El curso dejará de ser visible, pero no se borrará el historial.</p>
                </div>
                <div class="modal-footer border-0">
                    <button type="button" class="btn btn-light" data-bs-dismiss="modal">Cancelar</button>

                    <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Sí, Archivar"
                        CssClass="btn btn-danger" OnClick="btnConfirmarEliminar_Click" />
                </div>
            </div>
        </div>
    </div>

    <script>
        function mostrarModalEliminar(idCurso) {
            // 1. Guardamos el ID en el HiddenField
            var hiddenField = document.getElementById('<%= hfIdCursoEliminar.ClientID %>');
            hiddenField.value = idCurso;

            // 2. Abrimos el Modal de Bootstrap
            var myModal = new bootstrap.Modal(document.getElementById('modalConfirmaEliminar'));
            myModal.show();
        }
    </script>
</asp:Content>

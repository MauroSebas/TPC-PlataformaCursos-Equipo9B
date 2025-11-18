<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CursoPanel.aspx.cs" Inherits="Vistas.Aministrador.CursoPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        
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
                NavigateUrl="~/Administrador/CursoForm.aspx" 
                CssClass="btn btn-primary d-flex align-items-center gap-2 fw-bold small"> <%-- ¡ARREGLO 1! (sin shadow-sm) --%>
                <i class="bi bi-plus-circle fs-6"></i>
                <span>Crear Nuevo Curso</span>
            </asp:HyperLink>
        </div>
    </div>

    <!-- Panel de Mensajes (Queda igual) -->
    <asp:UpdatePanel ID="updMensajeGlobal" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlMensajeGlobal" runat="server" Visible="false" EnableViewState="false" CssClass="alert">
                <asp:Literal ID="litMensajeGlobal" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- 2. Filtros (¡ARREGLO 1! Sacamos la sombra) -->
    <asp:UpdatePanel ID="updFiltros" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="card p-4 mb-4 rounded-3 bg-body"> <%-- ¡¡SIN shadow-sm!! --%>
                <div class="row g-3 align-items-center">
                    <%-- ... (Tu código de filtros queda igual) ... --%>
                    <div class="col-md-5">
                        <asp:Label ID="lblBuscar" runat="server" Text="Buscar por título" CssClass="form-label small fw-medium" AssociatedControlID="txtBuscar"/>
                        <div class="input-group">
                            <span class="input-group-text"><i class="bi bi-search"></i></span>
                            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar curso..." />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblCategoria" runat="server" Text="Filtrar por Categoría" CssClass="form-label small fw-medium" AssociatedControlID="ddlCategoriaFiltro"/>
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
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- 3. Grilla de Cursos -->
    <asp:UpdatePanel ID="updGrillaCursos" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- ¡¡ARREGLO 1!! Sacamos la sombra -->
            <div class="card border-0 rounded-lg">
                <div class="card-header bg-body-tertiary border-bottom-0">
                    <h5 class="mb-0">Cursos Registrados</h5>
                </div>
                <div class="card-body p-0">
                    <asp:GridView ID="gvCursos" runat="server"
                        CssClass="table table-striped align-middle mb-0" <%-- ¡¡ARREGLO 1: SIN table-hover!! --%>
                        AutoGenerateColumns="false"
                        DataKeyNames="Id"
                        AllowPaging="true" PageSize="10"
                        OnPageIndexChanging="gvCursos_PageIndexChanging"
                        OnRowCommand="gvCursos_RowCommand">
                        
                        <Columns>
                            <asp:BoundField DataField="Titulo" HeaderText="Título" HeaderStyle-CssClass="px-4" ItemStyle-CssClass="px-4 fw-medium" />
                            <asp:BoundField DataField="Categoria.Nombre" HeaderText="Categoría" />
                            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
                            
                            <!-- ¡¡ARREGLO 2: El Switch!! -->
                            <asp:TemplateField HeaderText="Publicado" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <div class="form-check form-switch d-flex justify-content-center align-items-center m-0 p-0">
                                        <%-- ¡¡Le sacamos el CssClass="form-check-input"!! --%>
                                        <asp:CheckBox ID="chkPublicado" runat="server" 
                                            Checked='<%# Bind("Publicado") %>'
                                            AutoPostBack="true" 
                                            OnCheckedChanged="chkPublicado_CheckedChanged" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="text-end" ItemStyle-CssClass="text-end px-4">
                                <ItemTemplate>
                                    <%-- ... (Tus botones de acciones quedan igual) ... --%>
                                    <div class="d-flex justify-content-end gap-2">
                                        <asp:HyperLink ID="hlEditar" runat="server"
                                            NavigateUrl='<%# Eval("Id", "~/Administrador/CursoForm.aspx?id={0}") %>'
                                            CssClass="btn btn-sm btn-outline-secondary" ToolTip="Editar Curso">
                                            <i class="bi bi-pencil"></i>
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="hlModulos" runat="server"
                                            NavigateUrl='<%# Eval("Id", "~/Administrador/ModuloPanel.aspx?id={0}") %>'
                                            CssClass="btn btn-sm btn-outline-info" ToolTip="Gestionar Módulos/Lecciones">
                                            <i class="bi bi-list-task"></i>
                                        </asp:HyperLink>
                                        <asp:LinkButton ID="btnArchivar" runat="server"
                                            CssClass="btn btn-sm btn-outline-danger" ToolTip="Archivar Curso"
                                            CommandName="Archivar" CommandArgument='<%# Bind("Id") %>'
                                            OnClientClick="return confirm('¿Estás seguro de que querés archivar este curso?');">
                                            <i class="bi bi-archive"></i>
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <EmptyDataTemplate>
                            <%-- ... (Tu EmptyDataTemplate queda igual) ... --%>
                            <div class="text-center py-5">
                                <i class="bi bi-book-half fs-1 text-body-tertiary"></i>
                                <h4 class="mt-3 fw-bold">No se encontraron cursos</h4>
                                <p class="text-body-secondary">Aún no se han creado cursos. ¡Empezá creando el primero!</p>
                                <asp:HyperLink ID="btnCrearVacio" runat="server" 
                                    NavigateUrl="~/Administrador/CursoForm.aspx" 
                                    CssClass="btn btn-primary mt-3">
                                    <i class="bi bi-plus-circle me-2"></i>Crear Nuevo Curso
                                </asp:HyperLink>
                            </div>
                        </EmptyDataTemplate>
                        
                       <!-- ¡¡ARREGLO DE PAGINACIÓN!! -->
                        <PagerStyle CssClass="p-3" />
                        <PagerSettings 
                            Mode="NumericFirstLast"
                            Position="Bottom"
                            PageButtonCount="5"
                            FirstPageText="&laquo;"
                            LastPageText="&raquo;"
                            NextPageText="&rsaquo;"
                            PreviousPageText="&lsaquo;" 
                            />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnFiltrar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

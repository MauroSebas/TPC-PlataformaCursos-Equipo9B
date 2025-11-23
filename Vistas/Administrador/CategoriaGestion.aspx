<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CategoriaGestion.aspx.cs" Inherits="Vistas.CategoriaGestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:HiddenField ID="hfIdCategoriaEliminar" runat="server" Value="0" />

    <div class="d-flex flex-wrap justify-content-between align-items-center gap-3 mb-4 border-bottom pb-3">
        <div class="d-flex flex-column gap-1">
            <h1 class="h3 fw-bolder text-body-emphasis mb-0">Gestión de Categorías</h1>
            <p class="text-body-secondary fs-6 mb-0">Administra las categorías para organizar tus cursos.</p>
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
                    <h6 class="mb-0 fw-bold"><i class="bi bi-tags-fill me-2 text-primary"></i>Categoría</h6>
                </div>
                <div class="card-body p-4">

                    <asp:UpdatePanel ID="updFormulario" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:HiddenField ID="hfIdCategoriaEditar" runat="server" Value="0" />

                            <div class="mb-3">
                                <label class="form-label small fw-bold text-muted">Nombre</label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej: Programación Web..." ValidationGroup="Categoria"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" 
                                    ErrorMessage="El nombre es obligatorio" CssClass="text-danger small" Display="Dynamic" ValidationGroup="Categoria" />
                            </div>

                            <div class="d-grid gap-2">
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Categoría" CssClass="btn btn-primary" 
                                    OnClick="btnGuardar_Click" ValidationGroup="Categoria" />
                                
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-link text-muted btn-sm" 
                                    OnClick="btnCancelar_Click" Visible="false" CausesValidation="false" />
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>

        <div class="col-lg-8">
            <div class="card border-0 shadow-sm rounded-lg">
                <div class="card-header bg-body-tertiary border-bottom-0 py-3">
                    <h6 class="mb-0 fw-bold">Listado Actual</h6>
                </div>
                
                <div class="card-body p-0">
                    <asp:UpdatePanel ID="updGrilla" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            
                            <asp:GridView ID="gvCategorias" runat="server" CssClass="table table-hover align-middle mb-0"
                                AutoGenerateColumns="false" DataKeyNames="Id" GridLines="None"
                                OnRowCommand="gvCategorias_RowCommand">
                                <Columns>
                                    
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-CssClass="fw-medium px-4" HeaderStyle-CssClass="px-4" />

                                    <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-end pe-4">
                                        <ItemTemplate>
                                            
                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' 
                                                CssClass="btn btn-sm btn-light text-warning border-0 me-1" ToolTip="Editar">
                                                <i class="bi bi-pencil-square"></i>
                                            </asp:LinkButton>

                                            <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' 
                                                CssClass="btn btn-sm btn-light text-danger border-0" ToolTip="Eliminar"
                                                OnClientClick='<%# "mostrarModalEliminar(" + Eval("Id") + "); return false;" %>'>
                                                <i class="bi bi-trash"></i>
                                            </asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="text-center py-5 text-muted">
                                        <i class="bi bi-tags fs-1 opacity-50"></i>
                                        <p class="mt-2">No hay categorías cargadas.</p>
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
                    <h5 class="modal-title">Eliminar Categoría</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <p class="mb-1">¿Seguro que querés eliminar esta categoría?</p>
                    <p class="small text-danger fw-bold">No podrás eliminarla si tiene cursos activos asociados.</p>
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
        function mostrarModalEliminar(id) {
            document.getElementById('<%= hfIdCategoriaEliminar.ClientID %>').value = id;
            var myModal = new bootstrap.Modal(document.getElementById('modalConfirmaEliminar'));
            myModal.show();
        }
    </script>

</asp:Content>

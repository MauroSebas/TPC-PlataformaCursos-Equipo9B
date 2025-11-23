<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="LeccionPanel.aspx.cs" Inherits="Vistas.Administrador.Curso.LeccionPanel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:HiddenField ID="hfIdLeccionEliminar" runat="server" Value="0" />
    <asp:HiddenField ID="hfIdCursoDelModulo" runat="server" Value="0" /> <div class="d-flex flex-wrap justify-content-between align-items-center gap-3 mb-4 border-bottom pb-3">
        <div class="d-flex flex-column gap-1">
            <h1 class="h3 fw-bolder text-body-emphasis mb-0">
                <asp:Literal ID="litTituloModulo" runat="server" Text="Gestionar Lecciones" />
            </h1>
            <p class="text-body-secondary fs-6 mb-0">Cargá el contenido multimedia para este módulo.</p>
        </div>
        <div>
            <asp:HyperLink ID="btnVolver" runat="server" 
                CssClass="btn btn-outline-secondary d-flex align-items-center gap-2 fw-bold small">
                <i class="bi bi-arrow-left"></i>
                <span>Volver a Módulos</span>
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

        <div class="col-lg-5"> <div class="card border-0 shadow-sm rounded-lg sticky-top" style="top: 20px;">
                <div class="card-header  border-bottom py-3">
                    <h6 class="mb-0 fw-bold"><i class="bi bi-collection-play me-2 text-primary"></i>Contenido</h6>
                </div>
                <div class="card-body p-4">

                    <asp:UpdatePanel ID="updFormulario" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:HiddenField ID="hfIdLeccionEditar" runat="server" Value="0" />
                            <div class="mb-3">
                                <label class="form-label small fw-bold text-muted">Título de la Clase</label>
                                <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" placeholder="Ej: Configurando el entorno..." ValidationGroup="Leccion"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtTitulo" ErrorMessage="Requerido" CssClass="text-danger small" Display="Dynamic" ValidationGroup="Leccion" />
                            </div>

                            <div class="mb-3">
                                <label class="form-label small fw-bold text-muted">Descripción / Notas</label>
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Resumen de lo que se verá..." ValidationGroup="Leccion"></asp:TextBox>
                            </div>

                            <div class="row g-2 mb-3">
                                <div class="col-md-6">
                                    <label class="form-label small fw-bold text-muted">Tipo Material</label>
                                    <asp:DropDownList ID="ddlTipoMaterial" runat="server" CssClass="form-select" 
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlTipoMaterial_SelectedIndexChanged">
                                        <asp:ListItem Text="Video (YouTube/Vimeo)" Value="Video" Selected="True" />
                                        <asp:ListItem Text="Archivo (PDF/Zip)" Value="Archivo" />
                                        <asp:ListItem Text="Enlace Externo" Value="Enlace" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label small fw-bold text-muted">Duración (min)</label>
                                    <asp:TextBox ID="txtDuracion" runat="server" CssClass="form-control" TextMode="Number" Text="10" ValidationGroup="Leccion"></asp:TextBox>
                                </div>
                            </div>

                            <asp:Panel ID="pnlUrl" runat="server" Visible="true" CssClass="mb-3  p-3 rounded border">
                                <label class="form-label small fw-bold text-muted">Pegar Enlace (URL)</label>
                                <div class="input-group">
                                    <span class="input-group-text "><i class="bi bi-link-45deg"></i></span>
                                    <asp:TextBox ID="txtUrlRecurso" runat="server" CssClass="form-control" placeholder="https://..."></asp:TextBox>
                                </div>
                                <div class="form-text small">Ej: https://www.youtube.com/watch?v=...</div>
                            </asp:Panel>

                            <asp:Panel ID="pnlArchivo" runat="server" Visible="false" CssClass="mb-3  p-3 rounded border">
                                <label class="form-label small fw-bold text-muted">Subir Documento</label>
                                <asp:FileUpload ID="fileUploadMaterial" runat="server" CssClass="form-control form-control-sm" />
                                
                                <asp:Panel ID="pnlArchivoExistente" runat="server" Visible="false" CssClass="mt-2">
                                    <span class="badge bg-success"><i class="bi bi-check-circle me-1"></i>Archivo cargado</span>
                                    <asp:Label ID="lblArchivoActual" runat="server" CssClass="small text-muted ms-1"></asp:Label>
                                </asp:Panel>
                            </asp:Panel>

                            <div class="d-grid gap-2 mt-4">
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Lección" CssClass="btn btn-primary" 
                                    OnClick="btnGuardar_Click" ValidationGroup="Leccion" />
                                
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar Edición" CssClass="btn btn-link text-muted btn-sm" 
                                    OnClick="btnCancelar_Click" Visible="false" CausesValidation="false" />
                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnGuardar" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>

        <div class="col-lg-7">
            <div class="card border-0 shadow-sm rounded-lg">
                <div class="card-body p-0">
                    <asp:UpdatePanel ID="updGrilla" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            
                            <asp:GridView ID="gvLecciones" runat="server" CssClass="table table-hover align-middle mb-0"
                                AutoGenerateColumns="false" DataKeyNames="Id" GridLines="None"
                                OnRowCommand="gvLecciones_RowCommand" OnRowDataBound="gvLecciones_RowDataBound">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="#" ItemStyle-Width="90px" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <div class="d-flex align-items-center justify-content-center gap-1">
                                                <asp:LinkButton ID="btnSubir" runat="server" CommandName="Subir" CommandArgument='<%# Eval("Id") %>' CssClass="text-secondary"><i class="bi bi-caret-up-fill"></i></asp:LinkButton>
                                                <span class="fw-bold small"><%# Eval("Orden") %></span>
                                                <asp:LinkButton ID="btnBajar" runat="server" CommandName="Bajar" CommandArgument='<%# Eval("Id") %>' CssClass="text-secondary"><i class="bi bi-caret-down-fill"></i></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-Width="40px" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litIconoTipo" runat="server"></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Titulo" HeaderText="Lección" ItemStyle-CssClass="fw-medium" />

                                    <asp:TemplateField HeaderText="Dur.">
                                        <ItemTemplate>
                                            <span class="badge bg-light text-dark border"><%# Eval("DuracionMinutos") %>m</span>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-CssClass="text-end pe-3">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' 
                                                CssClass="btn btn-sm btn-light text-warning border-0"><i class="bi bi-pencil-square"></i></asp:LinkButton>
                                            
                                            <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' 
                                                CssClass="btn btn-sm btn-light text-danger border-0"
                                                OnClientClick='<%# "mostrarModalEliminar(" + Eval("Id") + "); return false;" %>'>
                                                <i class="bi bi-trash"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="text-center py-5 text-muted">No hay lecciones cargadas.</div>
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
                    <h5 class="modal-title">Eliminar Lección</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <p>¿Seguro que querés eliminar esta lección?</p>
                </div>
                <div class="modal-footer border-0">
                    <button type="button" class="btn btn-light" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Sí, Eliminar" CssClass="btn btn-danger" OnClick="btnConfirmarEliminar_Click" />
                </div>
            </div>
        </div>
    </div>

    <script>
        function mostrarModalEliminar(id) {
            document.getElementById('<%= hfIdLeccionEliminar.ClientID %>').value = id;
            new bootstrap.Modal(document.getElementById('modalConfirmaEliminar')).show();
        }
    </script>
</asp:Content>

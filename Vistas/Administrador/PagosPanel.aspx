<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="PagosPanel.aspx.cs" Inherits="Vistas.Aministrador.PagosPanel" %>

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

  <div class="d-flex flex-wrap justify-content-between align-items-center gap-3 mb-4 border-bottom pb-3">
        <div class="d-flex flex-column gap-1">
            <h1 class="h3 fw-bolder text-body-emphasis mb-0">Aprobación de Pagos</h1>
            <p class="text-body-secondary fs-6 mb-0">Gestiona los pagos realizados por los alumnos.</p>
        </div>
    </div>

    <asp:UpdatePanel ID="updMensajeGlobal" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlMensajeGlobal" runat="server" Visible="false" EnableViewState="false" CssClass="alert mb-4">
                <i class="bi bi-info-circle-fill me-2"></i>
                <asp:Literal ID="litMensajeGlobal" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="updPrincipal" runat="server">
        <ContentTemplate>
            
            <!-- CAMPO OCULTO PARA GUARDAR EL ID DEL PAGO A RECHAZAR -->
            <asp:HiddenField ID="hfPagoIdRechazo" runat="server" />

            <!-- FILTROS -->
            <div class="card p-4 mb-4 rounded-3 bg-body">
                <div class="row g-3 align-items-center">
                    <div class="col-md-5">
                        <asp:Label ID="lblBuscar" runat="server" Text="Buscar por alumno o curso" CssClass="form-label small fw-medium" AssociatedControlID="txtBuscar" />
                        <div class="input-group">
                            <span class="input-group-text"><i class="bi bi-search"></i></span>
                            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Ej: pepe@email.com" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblEstado" runat="server" Text="Filtrar por Estado" CssClass="form-label small fw-medium" AssociatedControlID="ddlEstadoFiltro" />
                        <asp:DropDownList ID="ddlEstadoFiltro" runat="server" CssClass="form-select">
                            <asp:ListItem Text="-- Todos los Estados --" Value="" />
                            <asp:ListItem Text="Pendiente" Value="Pendiente" />
                            <asp:ListItem Text="Aprobado" Value="Aprobado" />
                            <asp:ListItem Text="Rechazado" Value="Rechazado" />
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

            <!-- GRILLA -->
            <div class="card border-0 rounded-lg">
                <div class="card-header bg-body-tertiary border-bottom-0">
                    <h5 class="mb-0">Listado de Pagos</h5>
                </div>
                <div class="card-body p-0 table-responsive">
                    <asp:GridView ID="gvPagos" runat="server" CssClass="table table-hover align-middle mb-0"
                        AutoGenerateColumns="false" DataKeyNames="Id"
                        AllowPaging="true" PageSize="10"
                        OnPageIndexChanging="gvPagos_PageIndexChanging"
                        OnRowCommand="gvPagos_RowCommand" GridLines="None">
                        
                        <Columns>
                            <asp:BoundField DataField="Inscripcion.Usuario.Email" HeaderText="Email Alumno" HeaderStyle-CssClass="px-4" ItemStyle-CssClass="px-4 fw-medium" />
                            <asp:BoundField DataField="Inscripcion.Curso.Titulo" HeaderText="Curso" />
                            <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:C}" />
                            <asp:BoundField DataField="MetodoPago" HeaderText="Método" />
                            <asp:BoundField DataField="FechaPago" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                            
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <span class='badge rounded-pill <%# ObtenerClaseBadge(Eval("Estado").ToString()) %>'>
                                        <%# Eval("Estado") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-end px-4">
    <ItemTemplate>
        <div class="d-flex justify-content-end gap-2">
            
            <%-- 1. VER COMPROBANTE (Siempre visible) --%>
            <asp:HyperLink ID="lnkVerComprobante" runat="server" NavigateUrl='<%# Eval("UrlComprobante") %>' Target="_blank"
                CssClass="btn btn-sm btn-outline-secondary" ToolTip="Ver Comprobante">
                <i class="bi bi-file-earmark-image"></i>
            </asp:HyperLink>

            <%-- 2. BOTÓN APROBAR (Visible si es Pendiente O Rechazado) --%>
            <%-- Oculto si YA está Aprobado --%>
            <asp:LinkButton ID="btnAprobar" runat="server" CommandName="Aprobar" CommandArgument='<%# Eval("Id") %>'
                CssClass="btn btn-sm btn-success fw-bold" ToolTip="Aprobar Pago"
                Visible='<%# Eval("Estado").ToString() != "Aprobado" %>'
                OnClientClick="return confirm('¿Confirmar aprobación del pago?');">
                <i class="bi bi-check-lg"></i>
            </asp:LinkButton>

            <%-- 3. BOTÓN RECHAZAR (Visible si es Pendiente O Aprobado) --%>
            <%-- Oculto si YA está Rechazado --%>
            <asp:LinkButton ID="btnRechazar" runat="server" CommandName="AbrirRechazo" CommandArgument='<%# Eval("Id") %>'
                CssClass="btn btn-sm btn-danger fw-bold" ToolTip="Rechazar / Anular"
                Visible='<%# Eval("Estado").ToString() != "Rechazado" %>'>
                <i class="bi bi-x-lg"></i>
            </asp:LinkButton>

        </div>
    </ItemTemplate>
</asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="text-center py-5">
                                <i class="bi bi-cash-coin fs-1 text-body-tertiary"></i>
                                <h4 class="mt-3 fw-bold">No hay pagos registrados</h4>
                            </div>
                        </EmptyDataTemplate>
                        <PagerStyle CssClass="p-3" />
                    </asp:GridView>
                </div>
            </div>

            <!-- MODAL DE RECHAZO (Bootstrap 5) -->
            <div class="modal fade" id="modalRechazo" tabindex="-1" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-header bg-danger text-white">
                            <h5 class="modal-title fw-bold"><i class="bi bi-exclamation-triangle-fill me-2"></i>Rechazar Pago</h5>
                            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <p>Por favor, indica el motivo del rechazo para notificar al alumno:</p>
                            <div class="form-group">
                                <asp:TextBox ID="txtObservacionRechazo" runat="server" TextMode="MultiLine" Rows="4" 
                                    CssClass="form-control" placeholder="Ej: El comprobante es ilegible, monto incorrecto..."></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvRechazo" runat="server" ControlToValidate="txtObservacionRechazo" 
                                    ValidationGroup="Rechazo" ErrorMessage="Debes escribir un motivo." CssClass="text-danger small d-block mt-1" Display="Dynamic" />
                            </div>
                        </div>
                        <div class="modal-footer border-0">
                            <button type="button" class="btn btn-link text-decoration-none text-secondary" data-bs-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btnConfirmarRechazo" runat="server" Text="Confirmar Rechazo" 
                                CssClass="btn btn-danger fw-bold px-4" OnClick="btnConfirmarRechazo_Click" ValidationGroup="Rechazo" />
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- SCRIPT PARA ABRIR/CERRAR EL MODAL DESDE C# -->
    <script type="text/javascript">
        function mostrarModalRechazo() {
            var myModal = new bootstrap.Modal(document.getElementById('modalRechazo'));
            myModal.show();
        }
        function cerrarModalRechazo() {
            var myModalEl = document.getElementById('modalRechazo');
            var modal = bootstrap.Modal.getInstance(myModalEl);
            if (modal) {
                modal.hide();
            }
            // Limpiar backdrop residual por si acaso
            document.querySelectorAll('.modal-backdrop').forEach(el => el.remove());
        }
    </script>

</asp:Content>

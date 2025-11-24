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
            <h1 class="h3 fw-bolder text-body-emphasis mb-0">Aprobacion de Pagos</h1>
            <p class="text-body-secondary fs-6 mb-0">Gestiona los pagos realizados por los alumnos.</p>
        </div>
    </div>

    <%--Panel de mensajes--%>
    <asp:UpdatePanel ID="updMensajeGlobal" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlMensajeGlobal" runat="server" Visible="false" EnableViewState="false" CssClass="alert mb-4">
                <i class="bi bi-info-circle-fill me-2"></i>
                <asp:Literal ID="litMensajeGlobal" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="updFiltros" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
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
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="updGrillaPagos" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="card border-0 rounded-lg">
                <div class="card-header bg-body-tertiary border-bottom-0">
                    <h5 class="mb-0">Listado de Pagos</h5>
                </div>
                <div class="card-body p-0">
                    <asp:GridView ID="gvPagos" runat="server"
                        CssClass="table table-hover align-middle mb-0"
                        AutoGenerateColumns="false"
                        DataKeyNames="Id"
                        AllowPaging="true" PageSize="10"
                        OnPageIndexChanging="gvPagos_PageIndexChanging"
                        OnRowCommand="gvPagos_RowCommand">
                       


                        <Columns>
                            <%-- Email del Alumno --%>
                            <asp:BoundField DataField="Inscripcion.Usuario.Email" HeaderText="Email Alumno" HeaderStyle-CssClass="px-4" ItemStyle-CssClass="px-4 fw-medium" />

                            <%-- Nombre del Curso --%>
                            <asp:BoundField DataField="Inscripcion.Curso.Titulo" HeaderText="Curso" />

                            <%-- Monto --%>
                            <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:C}" />

                            <%-- Método Pago --%>
                            <asp:BoundField DataField="MetodoPago" HeaderText="Método" />

                            <%-- Fecha --%>
                            <asp:BoundField DataField="FechaPago" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />

                            <%-- Estado (Con Badge condicional en RowDataBound o TemplateField) --%>
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstadoGrid" runat="server" Text='<%# Eval("Estado") %>' CssClass="badge rounded-pill"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%-- Acciones --%>
                            <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-end px-4">
                                <ItemTemplate>
                                    <div class="d-flex justify-content-end gap-2">
                                        <%-- Ver Comprobante --%>
                                        <asp:HyperLink ID="lnkVerComprobante" runat="server"
                                            NavigateUrl='<%# Eval("UrlComprobante") %>' Target="_blank"
                                            CssClass="btn btn-sm btn-outline-secondary" ToolTip="Ver Comprobante">
                                            <i class="bi bi-file-earmark-image"></i>
                                        </asp:HyperLink>

                                        <%-- Aprobar (Solo visible si es Pendiente) --%>
                                        <asp:LinkButton ID="btnAprobar" runat="server"
                                            CommandName="Aprobar" CommandArgument='<%# Eval("Id") %>'
                                            CssClass="btn btn-sm btn-success fw-bold" ToolTip="Aprobar Pago"
                                            OnClientClick="return confirm('¿Confirmar aprobación del pago?');">
                                            APROBAR
                                        </asp:LinkButton>

                                        <%-- Rechazar --%>
                                        <asp:LinkButton ID="btnRechazar" runat="server"
                                            CommandName="Rechazar" CommandArgument='<%# Eval("Id") %>'
                                            CssClass="btn btn-sm btn-danger fw-bold" ToolTip="Rechazar Pago"
                                            OnClientClick="return confirm('¿Rechazar este pago?');">
                                            RECHAZAR
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <EmptyDataTemplate>
                            <div class="text-center py-5">
                                <i class="bi bi-cash-coin fs-1 text-body-tertiary"></i>
                                <h4 class="mt-3 fw-bold">No hay pagos registrados</h4>
                                <p class="text-body-secondary">No se encontraron pagos con los filtros actuales.</p>
                            </div>
                        </EmptyDataTemplate>

                        <PagerStyle CssClass="p-3" />
                        <PagerSettings Mode="NumericFirstLast" Position="Bottom" PageButtonCount="5" FirstPageText="&laquo;" LastPageText="&raquo;" />
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

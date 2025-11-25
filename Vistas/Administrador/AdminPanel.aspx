<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="Vistas.Aministrador.AdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex flex-column gap-4">
        
        

        <div class="row g-4">
            
            <div class="col-md-6 col-xl-3">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-body">
                        <div class="d-flex align-items-center mb-2">
                            <div class="d-flex align-items-center justify-content-center rounded bg-primary bg-opacity-10 text-primary" style="width: 40px; height: 40px;">
                                <i class="bi bi-journal-bookmark-fill fs-5"></i>
                            </div>
                            <span class="ms-3 text-body-secondary small text-uppercase fw-bold">Total Cursos</span>
                        </div>
                        <div class="d-flex align-items-end justify-content-between">
                            <h2 class="fw-bold mb-0 text-body-emphasis">
                                <asp:Literal ID="litTotalCursos" runat="server" Text="0"></asp:Literal>
                            </h2>
                            <%--<span class="badge text-bg-success bg-opacity-10 text-success">+2 este mes</span>--%>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-6 col-xl-3">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-body">
                        <div class="d-flex align-items-center mb-2">
                            <div class="d-flex align-items-center justify-content-center rounded bg-warning bg-opacity-10 text-warning" style="width: 40px; height: 40px;">
                                <i class="bi bi-people-fill fs-5"></i>
                            </div>
                            <span class="ms-3 text-body-secondary small text-uppercase fw-bold">Alumnos</span>
                        </div>
                        <div class="d-flex align-items-end justify-content-between">
                            <h2 class="fw-bold mb-0 text-body-emphasis">
                                <asp:Literal ID="litTotalAlumnos" runat="server" Text="0"></asp:Literal>
                            </h2>
                           <%-- <span class="badge text-bg-success bg-opacity-10 text-success">+5% crec.</span>--%>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-6 col-xl-3">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-body">
                        <div class="d-flex align-items-center mb-2">
                            <div class="d-flex align-items-center justify-content-center rounded bg-success bg-opacity-10 text-success" style="width: 40px; height: 40px;">
                                <i class="bi bi-currency-dollar fs-5"></i>
                            </div>
                            <span class="ms-3 text-body-secondary small text-uppercase fw-bold">Ingresos</span>
                        </div>
                        <div class="d-flex align-items-end justify-content-between">
                            <h2 class="fw-bold mb-0 text-body-emphasis">
                                <asp:Literal ID="litIngresos" runat="server" Text="$0"></asp:Literal>
                            </h2>
                            <span class="badge text-bg-success bg-opacity-10 text-success">Total histórico</span>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-6 col-xl-3">
                <div class="card border-0 shadow-sm h-100 border-start border-4 border-danger">
                    <div class="card-body">
                        <div class="d-flex align-items-center mb-2">
                            <div class="d-flex align-items-center justify-content-center rounded bg-danger bg-opacity-10 text-danger" style="width: 40px; height: 40px;">
                                <i class="bi bi-exclamation-triangle-fill fs-5"></i>
                            </div>
                            <span class="ms-3 text-body-secondary small text-uppercase fw-bold">Pendientes</span>
                        </div>
                        <div class="d-flex align-items-end justify-content-between">
                            <h2 class="fw-bold mb-0 text-danger">
                                <asp:Literal ID="litPendientes" runat="server" Text="0"></asp:Literal>
                            </h2>
                            <a href="PagosPanel.aspx" class="text-decoration-none small fw-bold text-danger">Revisar →</a>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div class="row g-4">

    <!-- CURSOS POPULARES -->
    <div class="col-lg-4">
        <div class="card border-0 shadow-sm h-100">
            <div class="card-header bg-transparent border-0 pt-4 px-4">
                <h5 class="mb-0 fw-bold text-body-emphasis">Cursos Populares</h5>
            </div>
            <div class="card-body px-4">
                <asp:Repeater ID="repCursosPopulares" runat="server">
                   <ItemTemplate>
    <div class="d-flex justify-content-between py-2 border-bottom">

       
        <span class="text-truncate" style="white-space: normal; max-width: 70%;">
            <%# Eval("Titulo") %>
        </span>

       
        <span class="fw-bold text-primary">
            <%# Eval("Inscripciones") %>
        </span>

    </div>
</ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pnlSinCursos" runat="server" CssClass="text-center py-4 text-muted" Visible="false">
                    Sin datos suficientes.
                </asp:Panel>
            </div>
        </div>
    </div>

    <!-- ULTIMOS USUARIOS REGISTRADOS -->
    <div class="col-lg-4">
        <div class="card border-0 shadow-sm h-100">
            <div class="card-header bg-transparent border-0 pt-4 px-4">
                <h5 class="mb-0 fw-bold text-body-emphasis">Últimos Alumnos Registrados</h5>
            </div>
            <div class="card-body px-4">
                <asp:Repeater ID="repUsuariosRecientes" runat="server">
                    <ItemTemplate>
                        <div class="py-2 border-bottom">
                            <div class="fw-semibold"><%# Eval("Nombre") %> <%# Eval("Apellido") %></div>
                            <small class="text-muted"><%# Eval("Email") %></small><br />
                            <small class="text-secondary"><%# Eval("FechaCreacion", "{0:dd/MM/yyyy}") %></small>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pnlSinUsuarios" runat="server" CssClass="text-center py-4 text-muted" Visible="false">
                    No hay usuarios nuevos.
                </asp:Panel>
            </div>
        </div>
    </div>

    <!-- ACCIONES RAPIDAS  -->
    <div class="col-lg-4">
        <div class="card border-0 shadow-sm h-100">
            <div class="card-header bg-transparent border-0 pt-4 px-4">
                <h5 class="mb-0 fw-bold text-body-emphasis">Acciones Rápidas</h5>
            </div>
            <div class="card-body p-4 d-flex flex-column gap-3">

                <a href="Curso/CursoForm.aspx"
                   class="btn btn-primary btn-lg w-100 d-flex align-items-center justify-content-center gap-2 shadow-sm">
                    <i class="bi bi-plus-circle-fill"></i> Crear Nuevo Curso
                </a>

                <a href="PagosPanel.aspx"
                   class="btn btn-outline-secondary btn-lg w-100 d-flex align-items-center justify-content-center gap-2">
                    <i class="bi bi-receipt"></i> Revisar Pagos
                    <span class="badge text-bg-danger rounded-pill ms-auto">
                        <asp:Literal ID="litBadgePendientes" runat="server" Text="0"></asp:Literal>
                    </span>
                </a>


            </div>
        </div>
    </div>

</div>
</div>
</asp:Content>

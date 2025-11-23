<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CursoDetalle.aspx.cs" Inherits="Vistas.CursoDetalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .course-header-image {
            background-image: url('https://lh3.googleusercontent.com/aida-public/AB6AXuCuRQWZfRPVEnP6kdCoXxcdLIErxFUFJoJ0RyzKFbpHE0dZ7S8oLeErDwFN-V_vv8-zIJqCg_JBNX3FwKIb96kaadWEnD2lnWHhbLEpl1-c4aJciFP3EOCSn0HWVCw7MZbtji0_7WG3rIwZpNvWhyU5FDP4xOJJ5PEnPf1MhB45-NkCpmWtbgme2q8jIT3uWXiMWo7XZc_EUH5YyX6ckZ4hMEGOVpNvQETsP6YtSvs03dHmUxxkrpMlj_j-n-8bgKFvS-7MKk931MY'); /* Imagen del prototipo */
            background-size: cover;
            background-position: center;
            height: 350px; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container">
        <div class="row g-5">

            <%-- Columna Izquierda --%>
            <div class="col-lg-8">

                <%-- Título --%>
                <h1 class="display-5 fw-bold mb-3">
                    <asp:Label ID="lblTitulo" runat="server" Text="Título del Curso"></asp:Label>
                </h1>

                <%-- Descripción --%>
                <p class="lead text-muted mb-5">
                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripción del curso..."></asp:Label>
                </p>

                <%-- Sección "Lo que aprenderás" (REPEATER para la lista dinámica) --%>
                <div class="card border-0 shadow-sm mb-4 rounded-lg">
                    <div class="card-body p-4">
                        <h4 class="card-title fw-bold mb-3">Lo que aprenderás</h4>

                        <ul class="list-unstyled mb-0">
                            <asp:Repeater ID="repObjetivos" runat="server">
                                <ItemTemplate>
                                    <li class="d-flex align-items-start mb-2">
                                        <i class="bi bi-check-circle-fill text-primary me-2 mt-1"></i>
                                        <span><%# Eval("Descripcion") %></span>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>

                            <%-- Mensaje por si no hay objetivos cargados --%>
                            <asp:Label ID="lblSinObjetivos" runat="server" Visible="false" Text="No se detallan objetivos específicos." CssClass="text-muted small"></asp:Label>
                        </ul>

                    </div>
                </div>

            </div>

            <%-- Columna Derecha: Sidebar --%>
            <div class="col-lg-4">
                <div class="sticky-top" style="top: 80px;">
                    <div class="card border-0 shadow-sm rounded-lg overflow-hidden">

                        <%-- Imagen Sidebar --%>
                        <asp:Image ID="imgSidebar" runat="server" CssClass="card-img-top" AlternateText="Portada" />

                        <div class="card-body p-4">
                            <%-- Precio --%>
                            <h2 class="card-title display-6 fw-bold mb-3">
                                <asp:Label ID="lblPrecio" runat="server" Text="$0.00"></asp:Label>
                            </h2>

                            <div class="d-grid gap-2">
                                <asp:Button runat="server" ID="btnAgregarCarrito" Text="Añadir al Carrito" CssClass="btn btn-primary btn-lg" OnClick="btnAgregarCarrito_Click" />
                                <asp:Button runat="server" ID="btnComprar" Text="Comprar Curso" CssClass="btn btn-outline-primary btn-lg" OnClick="btnComprar_Click" />


                            </div>

                            <hr class="my-4">

                            <h5 class="fw-semibold mb-3">Este curso incluye:</h5>
                            <ul class="list-unstyled text-muted small">
                                <%-- Duración --%>
                                <li class="mb-2 d-flex align-items-center">
                                    <i class="bi bi-calendar-check me-2 fs-5"></i>
                                    Acceso por
                                    <asp:Label ID="lblDuracion" runat="server" Text="0"></asp:Label>
                                    días
                                </li>

                                <%-- Nivel (NUEVO) --%>
                                <li class="mb-2 d-flex align-items-center">
                                    <i class="bi bi-mortarboard me-2 fs-5"></i>
                                    <asp:Label ID="lblNivel" runat="server" Text="Nivel"></asp:Label>
                                </li>

                                <%-- Idioma (NUEVO) --%>
                                <li class="mb-2 d-flex align-items-center">
                                    <i class="bi bi-translate me-2 fs-5"></i>
                                    Idioma:
                                    <asp:Label ID="lblIdioma" runat="server" Text="Español"></asp:Label>
                                </li>

                                <%-- Certificado (NUEVO - Se oculta si es false) --%>
                                <li id="liCertificado" runat="server" class="d-flex align-items-center">
                                    <i class="bi bi-patch-check me-2 fs-5"></i>Certificado de finalización
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <%-- MODAL DE ALERTA (Si ya tiene el curso seleccionado) --%>
    <%-- Con Visible="false"--%>
    <asp:Panel ID="pnlAlertaYaComprado" runat="server" Visible="false">

        <div class="modal-backdrop fade show"></div>

        <div class="modal fade show d-block" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content rounded-4 shadow-lg border-0">

                    <div class="modal-header border-bottom-0 pb-0">
                        <h5 class="modal-title fw-bold text-primary">
                            <i class="bi bi-info-circle-fill me-2"></i>¡Atención!
                        </h5>
                    </div>

                    <div class="modal-body py-4 text-center">
                        <h4 class="fw-bold mb-3">Ya tienes este curso</h4>
                        <p class="text-secondary mb-0">
                            Detectamos que ya estás inscripto en este curso.
                        </p>
                    </div>

                    <div class="modal-footer border-top-0 justify-content-center pb-4">
                        <asp:Button ID="btnVolverAHome" runat="server" Text="Seguir Viendo Cursos" CssClass="btn btn-primary btn-lg px-5 rounded-pill" OnClick="btnVolverAHome_Click"/>
                        <asp:Button ID="btnVolverAMisCursos" runat="server" Text="Volver a Mis Cursos" CssClass="btn btn-primary btn-lg px-5 rounded-pill" OnClick="btnVolverAMisCursos_Click" />
                    </div>

                </div>
            </div>
        </div>
    </asp:Panel>


</asp:Content>

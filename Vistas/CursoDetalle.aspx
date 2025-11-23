<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CursoDetalle.aspx.cs" Inherits="Vistas.CursoDetalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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

                <%-- Sección "Lo que aprenderás" --%>
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


                            <%-- Mensaje por si no hay objetivos --%>

                            <asp:Label ID="lblSinObjetivos" runat="server" Visible="false" Text="No se detallan objetivos específicos." CssClass="text-muted small"></asp:Label>
                        </ul>
                    </div>
                </div>

            </div>

            <%-- Fin Columna Izquierda --%>


            <%-- Columna Derecha: Sidebar --%>
            <div class="col-lg-4">
                <div class="sticky-top" style="top: 80px;">
                    <div class="card border-0 shadow-sm rounded-lg overflow-hidden">

                        <%-- Imagen Sidebar --%>
                        <asp:Image ID="imgSidebar" runat="server" CssClass="card-img-top" AlternateText="Portada" />

                        <div class="card-body p-4">

                            <%-- PRECIO --%>
                            <h2 class="card-title display-6 fw-bold mb-3">
                                <asp:Label ID="lblPrecio" runat="server" Text="$0.00"></asp:Label>
                            </h2>

                            <%-- BOTONES DE ACCIÓN (Lógica Condicional - Tu versión) --%>
                            <div class="d-grid gap-2">

                                <%-- GRUPO 1: Cursos PAGOS --%>
                                <asp:PlaceHolder ID="phCursoPago" runat="server" Visible="false">
                                    <asp:Button runat="server" ID="btnAgregarCarrito" Text="Añadir al Carrito"
                                        CssClass="btn btn-outline-primary btn-lg" OnClick="btnAgregarCarrito_Click" />

                                    <asp:Button runat="server" ID="btnComprar" Text="Comprar Ahora"
                                        CssClass="btn btn-primary btn-lg" OnClick="btnComprar_Click" />
                                </asp:PlaceHolder>

                                <%-- GRUPO 2: Cursos GRATUITOS --%>
                                <asp:PlaceHolder ID="phCursoGratis" runat="server" Visible="false">
                                    <asp:Button runat="server" ID="btnInscribirse" Text="🚀 Inscribirse Gratis"
                                        CssClass="btn btn-success btn-lg fw-bold py-3" OnClick="btnInscribirse_Click" />
                                    <small class="text-center text-muted mt-1">Acceso inmediato sin costo.</small>
                                </asp:PlaceHolder>

                            </div>

                            <hr class="my-4">

                            <h5 class="fw-semibold mb-3">Este curso incluye:</h5>
                            <ul class="list-unstyled text-muted small">

                                <%-- DURACIÓN (Label completo) --%>
                                <li class="mb-2 d-flex align-items-center">
                                    <i class="bi bi-calendar-check me-2 fs-5"></i>
                                    <asp:Label ID="lblDuracion" runat="server"></asp:Label>
                                </li>

                                <%-- Nivel --%>
                                <li class="mb-2 d-flex align-items-center">
                                    <i class="bi bi-mortarboard me-2 fs-5"></i>
                                    <asp:Label ID="lblNivel" runat="server" Text="Nivel"></asp:Label>
                                </li>

                                <%-- Idioma --%>
                                <li class="mb-2 d-flex align-items-center">
                                    <i class="bi bi-translate me-2 fs-5"></i>
                                    Idioma: <asp:Label ID="lblIdioma" runat="server" Text="Español"></asp:Label>
                                </li>

                                <%-- Certificado --%>
                                <li id="liCertificado" runat="server" class="d-flex align-items-center">
                                    <i class="bi bi-patch-check me-2 fs-5"></i>Certificado de finalización
                                </li>
                            </ul>
                        </div> <%-- Fin Card Body --%>
                    </div> <%-- Fin Card --%>
                </div> <%-- Fin Sticky --%>
            </div> <%-- Fin Columna Derecha --%>

        </div> <%-- Fin Row --%>
    </div> <%-- Fin Container --%>

    <%-- MODAL DE ALERTA  --%>
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
                        <asp:Button ID="btnVolverAHome" runat="server" Text="Seguir Viendo Cursos" CssClass="btn btn-outline-secondary btn-lg px-5 rounded-pill" OnClick="btnVolverAHome_Click"/>
                        <asp:Button ID="btnVolverAMisCursos" runat="server" Text="Volver a Mis Cursos" CssClass="btn btn-primary btn-lg px-5 rounded-pill" OnClick="btnVolverAMisCursos_Click" />
                    </div>

                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>

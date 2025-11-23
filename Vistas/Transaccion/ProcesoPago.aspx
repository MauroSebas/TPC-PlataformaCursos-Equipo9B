<%@ Page Title="Proceso de Pago" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ProcesoPago.aspx.cs" Inherits="Vistas.ProcesoPago" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">
        <div class="row g-5">

            <div class="col-lg-7">

                <h2 class="fw-bold mb-4 text-body-emphasis">Confirma tu compra</h2>

                <%-- Tarjeta del Curso --%>
                <div class="card mb-4 shadow-sm bg-body rounded-3 border-0">
                    <div class="card-header bg-transparent border-bottom fw-semibold text-body-emphasis py-3">
                        Resumen del Pedido
                    </div>
                    <div class="card-body p-4">
                        <div class="d-flex justify-content-between align-items-center">
                            <div class="d-flex align-items-center">
                                <%-- Imagen del curso --%>
                                <img src="<%= ResolveUrl("~/Assets/img/curso_uiux.png") %>" alt="Curso" class="me-4 rounded shadow-sm" width="120" height="80" style="object-fit: cover;" />

                                <div>
                                    <asp:Label runat="server" ID="lblTituloCurso" CssClass="fw-bold d-block fs-5 text-body-emphasis mb-1" Text="Curso de Diseño UI/UX" />
                                    <asp:Label runat="server" ID="lblAutorCurso" CssClass="text-muted small" Text="Por Juan Pérez" />
                                </div>
                            </div>

                            <asp:Label runat="server" ID="lblPrecioCurso" CssClass="fw-bold fs-4 text-primary" Text="$99.99" />
                        </div>
                    </div>
                </div>

            </div>



            <div class="col-lg-5">

                <%-- Tarjeta de Totales --%>
                <div class="card shadow-lg border-primary border-2 mt-lg-2 bg-body rounded-4">
                    <div class="card-body p-4">
                        <h5 class="card-title fw-bold mb-4 text-primary">Detalle de la Compra</h5>

                        <div class="d-flex justify-content-between text-secondary mb-2">
                            <span>Subtotal</span>
                            <asp:Label runat="server" ID="lblSubtotal" CssClass="fw-medium" Text="$99.99" />
                        </div>

                        <div class="d-flex justify-content-between text-success mb-3">
                            <span>Descuentos</span>
                            <asp:Label runat="server" ID="lblDescuento" CssClass="fw-medium" Text="-$0.00" />
                        </div>

                        <hr class="my-4" />

                        <div class="d-flex justify-content-between align-items-center mb-4">
                            <span class="fw-bold fs-5 text-body-emphasis">Total a pagar</span>
                            <asp:Label runat="server" ID="lblTotal" CssClass="fw-bolder fs-3 text-primary" Text="$99.99" />
                        </div>
                        <div class="d-grid gap-2">
                            <button type="button" class="btn btn-primary btn-lg fw-bold shadow-sm w-100" data-bs-toggle="modal" data-bs-target="#pagoModal">Realizar Pago </button>
                        </div>

                        <div class="text-center mt-3 text-muted small">
                            <i class="bi bi-lock-fill me-1"></i>Pago 100% seguro y encriptado
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </div>


    <%--    Modal de Transferencia--%>

    <div class="modal fade" id="pagoModal" tabindex="-1" aria-labelledby="pagoModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content rounded-4 shadow-lg border-0">

                <div class="modal-header p-4 border-bottom">
                    <h5 class="modal-title fw-bold fs-5" id="pagoModalLabel">Completa tu pago por transferencia</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>

                <div class="modal-body p-4 d-flex flex-column gap-4">
                    <p class="text-body-secondary mb-0">Transfiere el monto exacto al alias proporcionado y luego sube el comprobante de pago para finalizar tu inscripción.</p>

                    <div class="card bg-body-tertiary border rounded-3 p-3">
                        <div class="d-flex align-items-center justify-content-between border-bottom pb-2 mb-2">
                            <span class="text-body-secondary small">Alias Bancario</span>
                            <div class="d-flex align-items-center gap-2">
                                <span class="fw-medium text-body-emphasis small">PLATAFORMA.ALIAS</span>
                                <button class="btn btn-link text-primary p-0 text-decoration-none">
                                    <span class="material-symbols-outlined fs-6">content_copy</span>
                                </button>
                            </div>
                        </div>

                        <div class="d-flex justify-content-between border-bottom pb-2 mb-2">
                            <span class="text-body-secondary small">Concepto</span>

                            <asp:Label ID="lblNombreCursoModal" runat="server" CssClass="fw-medium text-body-emphasis small text-end" Text="Nombre del Curso" />
                        </div>

                        <div class="d-flex justify-content-between pt-1">
                            <span class="text-body-secondary small">Monto a Transferir</span>
                            <div class="d-flex align-items-center gap-2">

                                <asp:Label ID="lblMontoModal" runat="server" CssClass="fw-bold text-primary small" Text="$0.00" />

                                <button class="btn btn-link text-primary p-0"><span class="material-symbols-outlined fs-6">content_copy</span></button>
                            </div>
                        </div>
                    </div>

                    <div>
                        <asp:UpdatePanel ID="upModalPago" runat="server">
                            <ContentTemplate>
                                <div>
                                    <h6 class="fw-bold mb-3">Subí tu Comprobante de Pago</h6>

                                    <label class="file-drop-zone d-flex flex-column align-items-center  justify-content-center w-100 rounded-3 text-center py-5 px-3" style="cursor: pointer;">

                                        <asp:FileUpload ID="fuComprobante" runat="server" CssClass="form-control w-75" />

                                        <span class="small text-body-secondary mt-3">JPG, PNG, PDF. Máximo 5MB.</span>
                                    </label>

                                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger mt-2 d-block"></asp:Label>
                                </div>

                                <div class="modal-footer p-4 bg-body-tertiary rounded-bottom-4 border-top">
                                    <button type="button" class="btn btn-light fw-bold" data-bs-dismiss="modal">Cancelar</button>

                                    <asp:Button ID="btnEnviarComprobante" runat="server" Text="Enviar Comprobante" CssClass="btn btn-primary fw-bold" OnClick="btnEnviarComprobante_Click" />
                                </div>
                            </ContentTemplate>

                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnEnviarComprobante" />
                            </Triggers>
                            <%--// Permite que update panel ignore el postback parcial q tiene
                               por defecto por AJAX y haga una recarga comlpeta (fullPostback)--%>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--Modal de Comprobante recibido--%>

    <div class="modal fade" id="modalConfirmacion" tabindex="-1" aria-labelledby="successModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content rounded-4 shadow-lg border-0 p-4 text-center">
                <div class="modal-body d-flex flex-column align-items-center gap-3">
                    <div class="d-flex align-items-center justify-content-center rounded-circle bg-success-subtle" style="width: 4rem; height: 4rem;">
                        <span class="material-symbols-outlined fs-1 text-success">check_circle</span>
                    </div>
                    <div class="d-flex flex-column gap-2">
                        <h4 class="fw-bold text-body-emphasis mb-0">Comprobante recibido con éxito</h4>
                        <p class="text-body-secondary small mb-0">
                            Nuestro equipo administrativo revisará el pago. Recibirás una notificación por email una vez que se apruebe.
                        </p>
                    </div>
                    <button type="button" class="btn btn-primary w-100 fw-bold mt-3" data-bs-dismiss="modal">Entendido</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

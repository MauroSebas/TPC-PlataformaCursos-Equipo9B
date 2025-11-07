<%@ Page Title="Proceso de Pago" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ProcesoPago.aspx.cs" Inherits="Vistas.ProcesoPago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">
        <div class="row g-4">
            
            <div class="col-lg-7">
                <h2 class="fw-bold mb-4">Confirma tu compra</h2>

                <div class="card mb-4 shadow-sm bg-body">
                    <div class="card-header bg-transparent border-bottom fw-semibold text-body-emphasis">Resumen del Pedido</div>
                    <div class="card-body d-flex justify-content-between align-items-center">
                        <div class="d-flex align-items-center">
                            <img src="<%= ResolveUrl("~/Assets/img/curso_uiux.png") %>" alt="Curso" class="me-3 rounded" width="120" height="80" />
                            <div>
                                <asp:Label runat="server" ID="lblTituloCurso" CssClass="fw-semibold d-block fs-6 text-body-emphasis" Text="Curso de Diseño UI/UX" />
                                <asp:Label runat="server" ID="lblAutorCurso" CssClass="text-muted small" Text="Por Juan Pérez" />
                            </div>
                        </div>
                        <asp:Label runat="server" ID="lblPrecioCurso" CssClass="fw-bold fs-5 text-primary" Text="$99.99" />
                    </div>
                </div>

                <div class="card shadow-sm mb-4 bg-body">
                    <div class="card-header bg-transparent border-bottom fw-semibold text-body-emphasis">Método de Pago</div>
                    <div class="card-body">
                        <ul class="nav nav-tabs mb-3" id="pagoTabs" role="tablist">
                            <li class="nav-item" role="presentation">
                                <button class="nav-link active" id="tarjeta-tab" data-bs-toggle="tab" data-bs-target="#tarjeta" type="button" role="tab">Tarjeta de Crédito/Débito</button>
                            </li>
                            <li class="nav-item" role="presentation">
                                <button class="nav-link" id="paypal-tab" data-bs-toggle="tab" data-bs-target="#paypal" type="button" role="tab">PayPal</button>
                            </li>
                        </ul>

                        <div class="tab-content">
                            <div class="tab-pane fade show active" id="tarjeta" role="tabpanel">
                                <div class="row g-3">
                                    <div class="col-12">
                                        <asp:Label runat="server" AssociatedControlID="txtNombreTarjeta" Text="Nombre en la Tarjeta" CssClass="form-label" />
                                        <asp:TextBox runat="server" ID="txtNombreTarjeta" CssClass="form-control" placeholder="Juan Pérez García" />
                                    </div>
                                    <div class="col-12">
                                        <asp:Label runat="server" AssociatedControlID="txtNumeroTarjeta" Text="Número de Tarjeta" CssClass="form-label" />
                                        <asp:TextBox runat="server" ID="txtNumeroTarjeta" CssClass="form-control" placeholder="0000 0000 0000 0000" MaxLength="19" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label runat="server" AssociatedControlID="txtVencimiento" Text="Fecha de Vencimiento" CssClass="form-label" />
                                        <asp:TextBox runat="server" ID="txtVencimiento" CssClass="form-control" placeholder="MM/AA" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label runat="server" AssociatedControlID="txtCVV" Text="CVV" CssClass="form-label" />
                                        <asp:TextBox runat="server" ID="txtCVV" CssClass="form-control" placeholder="123" MaxLength="4" TextMode="Password" />
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane fade" id="paypal" role="tabpanel">
                                <div class="alert alert-info mt-3">
                                    Serás redirigido a PayPal para completar el pago de forma segura.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="d-grid d-lg-none mt-4">
                    <asp:Button runat="server" ID="btnRealizarPagoBottom" CssClass="btn btn-primary btn-lg" Text="Realizar Pago" />
                </div>
            </div>

            <div class="col-lg-5">
                <div class="card shadow-lg border-primary border-3 mt-lg-5 bg-body">
                    <div class="card-body">
                        <h5 class="card-title fw-bold mb-4 text-primary">Detalle de la Compra</h5>
                        <div class="d-flex justify-content-between text-muted mb-2">
                            <span>Subtotal</span>
                            <asp:Label runat="server" ID="lblSubtotal" Text="$99.99" />
                        </div>
                        <div class="d-flex justify-content-between text-muted mb-2">
                            <span>Descuentos</span>
                            <asp:Label runat="server" ID="lblDescuento" Text="$0.00" />
                        </div>
                        <hr />
                        <div class="d-flex justify-content-between fw-bold fs-5">
                            <span class="text-body-emphasis">Total a pagar</span>
                            <asp:Label runat="server" ID="lblTotal" CssClass="text-danger" Text="$99.99" />
                        </div>
                        <div class="text-center mt-3 text-muted small">
                            <i class="bi bi-lock-fill me-1"></i> Pago 100% seguro y encriptado
                        </div>
                    </div>
                </div>
                
                <div class="d-grid mt-4 d-none d-lg-grid">
                    <asp:Button runat="server" ID="btnRealizarPagoTop" CssClass="btn btn-primary btn-lg" Text="Realizar Pago" />
                </div>
            </div>

        </div>
    </div>
</asp:Content>
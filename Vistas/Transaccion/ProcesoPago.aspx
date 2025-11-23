<%@ Page Title="Proceso de Pago" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ProcesoPago.aspx.cs" Inherits="Vistas.ProcesoPago" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="container my-5">
        <div class="row g-5">
            
            <div class="col-lg-7">
                <h2 class="fw-bold mb-4 text-body-emphasis">Confirma tu compra</h2>
                <div class="card mb-4 shadow-sm bg-body rounded-3 border-0">
                    <div class="card-header bg-transparent border-bottom fw-semibold text-body-emphasis py-3">
                        Resumen del Pedido
                    </div>
                    <div class="card-body p-4">
                        <div class="d-flex justify-content-between align-items-center">
                            <div class="d-flex align-items-center">
                                <img src="<%= ResolveUrl("~/Assets/img/curso_uiux.png") %>" alt="Curso" class="me-4 rounded shadow-sm" width="120" height="80" style="object-fit: cover;" />
                                <div>
                                    <asp:Label runat="server" ID="lblTituloCurso" CssClass="fw-bold d-block fs-5 text-body-emphasis mb-1" Text="Curso..." />
                                    <asp:Label runat="server" ID="lblAutorCurso" CssClass="text-muted small" Text="Plataforma LMS" />
                                </div>
                            </div>
                            <asp:Label runat="server" ID="lblPrecioCurso" CssClass="fw-bold fs-4 text-primary" Text="$0.00" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-5">
                <div class="card shadow-lg border-primary border-2 mt-lg-2 bg-body rounded-4">
                    <div class="card-body p-4">
                        <h5 class="card-title fw-bold mb-4 text-primary">Detalle de la Compra</h5>
                        
                        <div class="d-flex justify-content-between text-secondary mb-2">
                            <span>Subtotal</span>
                            <asp:Label runat="server" ID="lblSubtotal" CssClass="fw-medium" Text="$0.00" />
                        </div>
                        <div class="d-flex justify-content-between text-success mb-3">
                            <span>Descuentos</span>
                            <asp:Label runat="server" ID="lblDescuento" CssClass="fw-medium" Text="-$0.00" />
                        </div>
                        <hr class="my-4" />
                        <div class="d-flex justify-content-between align-items-center mb-4">
                            <span class="fw-bold fs-5 text-body-emphasis">Total a pagar</span>
                            <asp:Label runat="server" ID="lblTotal" CssClass="fw-bolder fs-3 text-primary" Text="$0.00" />
                        </div>
                        
                        <div class="d-grid gap-2">
                            <asp:Button ID="btnIniciarPago" runat="server" Text="Realizar Pago" 
                                CssClass="btn btn-primary btn-lg fw-bold shadow-sm w-100" 
                                OnClick="btnIniciarPago_Click" />
                        </div>
                        
                        <div class="text-center mt-3 text-muted small">
                            <i class="bi bi-lock-fill me-1"></i>Pago 100% seguro
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlModalPago" runat="server" Visible="false">
        <div class="modal-backdrop fade show"></div>
        <div class="modal fade show d-block" tabindex="-1">
            <div class="modal-dialog modal-lg modal-dialog-centered">
                <div class="modal-content rounded-4 shadow-lg border-0">
                    
                    <div class="modal-header p-4 border-bottom">
                        <h5 class="modal-title fw-bold fs-5">Completa tu pago</h5>
                        <asp:LinkButton ID="btnCerrarModalPago" runat="server" CssClass="btn-close" OnClick="btnCerrarModales_Click"></asp:LinkButton>
                    </div>

                    <div class="modal-body p-4 d-flex flex-column gap-4">
                        <p class="text-body-secondary mb-0">Transfiere el monto exacto y subí el comprobante.</p>

                        <div class="card bg-body-tertiary border rounded-3 p-3">
                            <div class="d-flex justify-content-between border-bottom pb-2 mb-2">
                                <span class="text-body-secondary small">Alias</span>
                                <span class="fw-medium text-body-emphasis small">PLATAFORMA.ALIAS</span>
                            </div>
                            <div class="d-flex justify-content-between pt-1">
                                <span class="text-body-secondary small">Monto</span>
                                <asp:Label ID="lblMontoModal" runat="server" CssClass="fw-bold text-primary small" Text="$0.00" />
                            </div>
                        </div>

                        <div>
                            <h6 class="fw-bold mb-3">Subí tu Comprobante</h6>
                            <div class="p-3 border rounded bg-light text-center">
                                <asp:FileUpload ID="fuComprobante" runat="server" CssClass="form-control" />
                                <span class="small text-muted d-block mt-2">JPG, PNG, PDF (Max 5MB)</span>
                            </div>
                            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger mt-2 d-block fw-bold"></asp:Label>
                        </div>
                    </div>

                    <div class="modal-footer p-4 bg-body-tertiary rounded-bottom-4 border-top">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-light fw-bold" OnClick="btnCerrarModales_Click" />
                        <asp:Button ID="btnEnviarComprobante" runat="server" Text="Enviar Comprobante" CssClass="btn btn-primary fw-bold" OnClick="btnEnviarComprobante_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlModalExito" runat="server" Visible="false">
        <div class="modal-backdrop fade show"></div>
        <div class="modal fade show d-block" tabindex="-1">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content rounded-4 shadow-lg border-0 p-5 text-center">
                    <div class="mb-3">
                        <div class="d-inline-flex align-items-center justify-content-center rounded-circle bg-success-subtle" style="width: 5rem; height: 5rem;">
                            <i class="bi bi-check-lg fs-1 text-success"></i>
                        </div>
                    </div>
                    <h2 class="fw-bold mb-2">¡Pago Recibido!</h2>
                    <p class="text-muted mb-4">Tu inscripción está pendiente de aprobación. Te avisaremos pronto.</p>
                    
                    <asp:Button ID="btnEntendido" runat="server" Text="Ir a Mis Cursos" CssClass="btn btn-primary w-100 py-2 fw-bold" OnClick="btnEntendido_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>

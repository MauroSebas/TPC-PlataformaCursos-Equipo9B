<%@ Page Title="Mis Pagos" Language="C#" MasterPageFile="~/Alumno/Alumno.Master" AutoEventWireup="true" CodeBehind="MisPagos.aspx.cs" Inherits="Vistas.MisPagos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="d-flex flex-column gap-4">
        <h2 class="fw-bold mb-2 text-body-emphasis">Mis Pagos</h2>

        <div class="card shadow-sm border-0 rounded-4 overflow-hidden">
            <div class="card-body p-0">
                <div class="table-responsive">
                    <table class="table table-hover align-middle mb-0">
                        <thead class="border-bottom">
                            <tr>
                                <th scope="col" class="py-3 ps-4 text-body-secondary small text-uppercase">Curso</th>
                                <th scope="col" class="py-3 text-body-secondary small text-uppercase">Fecha</th>
                                <th scope="col" class="py-3 text-body-secondary small text-uppercase">Método</th>
                                <th scope="col" class="py-3 text-end text-body-secondary small text-uppercase">Monto</th>
                                <th scope="col" class="py-3 text-center text-body-secondary small text-uppercase">Estado</th>
                                <th scope="col" class="py-3 text-end pe-4 text-body-secondary small text-uppercase">Acciones</th>
                            </tr>
                        </thead>
                        
                        <tbody>
                            <asp:Repeater ID="repPagos" runat="server" OnItemCommand="repPagos_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td class="ps-4 fw-bold text-primary">
                                            <%# Eval("Inscripcion.Curso.Titulo") %>
                                        </td>
                                        <td class="text-body-secondary small">
                                            <%# Eval("FechaPago", "{0:dd/MM/yyyy}") %>
                                        </td>
                                        <td class="text-body-secondary small">
                                            <%# Eval("MetodoPago") %>
                                        </td>
                                        <td class="text-end fw-bold text-body-emphasis">
                                            <%# Eval("Monto", "{0:C}") %>
                                        </td>
                                        <td class="text-center">
                                            <span class='<%# ObtenerBadgeEstado(Eval("Estado").ToString()) %>'>
                                                <%# Eval("Estado") %>
                                            </span>
                                        </td>
                                        <td class="text-end pe-4">
                                            
                                            <asp:PlaceHolder ID="phVerComprobante" runat="server" Visible='<%# !string.IsNullOrEmpty(Convert.ToString(Eval("UrlComprobante"))) %>'>
                                                <a href='<%# ResolveUrl(Convert.ToString(Eval("UrlComprobante"))) %>' target="_blank" 
                                                   class="btn btn-sm btn-outline-secondary border-opacity-25" title="Ver Comprobante">
                                                    <i class="bi bi-eye"></i>
                                                </a>
                                            </asp:PlaceHolder>

                                            <asp:LinkButton ID="btnReintentar" runat="server" 
                                                CommandName="Reintentar" 
                                                CommandArgument='<%# Eval("Inscripcion.Id") + "|" + Eval("Observaciones") %>'
                                                CssClass="btn btn-sm btn-outline-danger ms-1" 
                                                Visible='<%# Eval("Estado").ToString() == "Rechazado" %>'
                                                ToolTip="Corregir Comprobante">
                                                <i class="bi bi-arrow-repeat"></i>
                                            </asp:LinkButton>

                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                    
                    <asp:Panel ID="pnlSinPagos" runat="server" Visible="false" CssClass="text-center py-5">
                        <div class="py-4">
                            <i class="bi bi-wallet2 fs-1 text-body-tertiary"></i>
                            <p class="mt-3 text-body-secondary">No tenés pagos registrados.</p>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlModalReintento" runat="server" Visible="false">
        
        <asp:HiddenField ID="hfIdInscripcionReintento" runat="server" />

        <div class="modal-backdrop fade show"></div>
        <div class="modal fade show d-block" tabindex="-1">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content rounded-4 shadow-lg border-0">
                    
                    <div class="modal-header bg-danger ">
                        <h5 class="modal-title fw-bold">Corregir Pago Rechazado</h5>
                        <asp:LinkButton ID="btnCerrarX" runat="server" CssClass="btn-close btn-close-white" OnClick="btnCerrarModal_Click"></asp:LinkButton>
                    </div>

                    <div class="modal-body p-4">
                        
                        <div class="alert alert-warning d-flex align-items-start gap-2 mb-4" role="alert">
                            <i class="bi bi-exclamation-triangle-fill mt-1"></i>
                            <div>
                                <strong>Motivo del rechazo:</strong><br/>
                                <asp:Label ID="lblObservacionAdmin" runat="server" Text="..."></asp:Label>
                            </div>
                        </div>

                        <h6 class="fw-bold mb-3">Subir nuevo comprobante</h6>
                        <div class="file-drop-zone p-3 border rounded  text-center">
                            <asp:FileUpload ID="fuNuevoComprobante" runat="server" CssClass="form-control" />
                            <span class="small text-muted d-block mt-2">Subí una imagen clara o PDF.</span>
                        </div>
                        <asp:Label ID="lblErrorModal" runat="server" CssClass="text-danger small mt-2 d-block fw-bold"></asp:Label>

                    </div>

                    <div class="modal-footer border-0">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn " OnClick="btnCerrarModal_Click" />
                        <asp:Button ID="btnGuardarReintento" runat="server" Text="Enviar Corrección" CssClass="btn btn-primary fw-bold" OnClick="btnGuardarReintento_Click" />
                    </div>

                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
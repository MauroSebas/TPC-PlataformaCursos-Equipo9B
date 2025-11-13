<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Loguin.aspx.cs" Inherits="Vistas.Loguin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<%= ResolveUrl("~/Assets/js/togglePassword.js") %>" defer></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center align-items-center" style="min-height: 60vh;">
        <div class="col-md-6 col-lg-5 col-xl-4">
            <div class="card shadow-sm border-0 rounded-lg">
                <div class="card-body p-4 p-sm-5">

                    <div class="text-center mb-4">
                        <i class="bi bi-person-circle fs-1 text-primary"></i>
                        <h3 class="card-title fw-bold mt-2">Bienvenido de nuevo</h3>
                    </div>
                    
                    <asp:Panel runat="server" ID="pnlError" Visible="false" CssClass="alert alert-danger" EnableViewState="false">
                        <asp:Literal runat="server" ID="litError" />
                        <br />
                        <asp:LinkButton ID="lnkReenviar" runat="server" 
                            OnClick="lnkReenviar_Click" 
                            Visible="false" 
                            CssClass="alert-link">
                            ¿Reenviar email de activación?
                        </asp:LinkButton>
                    </asp:Panel>

                    <!-- Email -->
                    <div class="mb-3">
                        <label for="<%= txtEmail.ClientID %>" class="form-label">Email <span class="text-danger">*</span></label>
                        <asp:TextBox runat="server" ID="txtEmail" type="email" CssClass="form-control form-control-lg" placeholder="Ingresa tu email" />
                        <asp:RequiredFieldValidator runat="server" 
                            ErrorMessage="El email es requerido." 
                            ControlToValidate="txtEmail" 
                            Display="Dynamic" CssClass="text-danger small" 
                            ValidationGroup="LoginGroup"/> 
                    </div>

                    <!-- Contraseña -->
                    <div class="mb-3">
                        <label for="<%= txtPassword.ClientID %>" class="form-label">Contraseña <span class="text-danger">*</span></label>
                        <div class="input-group input-group-lg">
                            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control" placeholder="Ingresa tu contraseña" />
                            <button class="btn btn-outline-secondary" type="button" id="togglePassword">
                                <i class="bi bi-eye"></i>
                            </button>
                        </div>
                        <asp:RequiredFieldValidator runat="server" 
                            ErrorMessage="La contraseña es requerida." 
                            ControlToValidate="txtPassword" 
                            Display="Dynamic" CssClass="text-danger small" 
                            ValidationGroup="LoginGroup"/>
                    </div>

                    <div class="d-flex justify-content-end mb-4">
                        <asp:HyperLink NavigateUrl="RecuperarContraseña.aspx" Text="¿Olvidaste tu contraseña?" CssClass="small text-decoration-none" runat="server" />
                    </div>
                    
                    <div class="d-grid">
                        <asp:Button runat="server" ID="btnIniciarSesion" 
                            Text="Iniciar Sesión" 
                            CssClass="btn btn-primary btn-lg" 
                            OnClick="btnIniciarSesion_Click" 
                            ValidationGroup="LoginGroup"/>
                    </div>

                    <div class="text-center mt-4">
                        <span class="text-muted small">¿No tienes una cuenta?</span>
                        <asp:HyperLink NavigateUrl="Registro.aspx" Text="Regístrate" CssClass="fw-semibold text-decoration-none ms-1" runat="server" />
                    </div>
                
                </div>
            </div>
        </div>
    </div>
    
    <!-- Modal de Boostrap para Confirmar el reenvio del Token -->
    <div class="modal fade" id="reenvioExitosoModal" runat="server" aria-labelledby="reenvioExitosoModalLabel" aria-hidden="true" data-bs-backdrop="static">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-success text-white">
                    <h5 class="modal-title" id="reenvioExitosoModalLabel">✅ ¡Email Reenviado!</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p>¡Perfecto! Te acabamos de reenviar el correo de activación.</p>
                    <p class="fw-bold">Por favor, revisá tu bandeja de entrada (y la de spam).</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>
    
    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            togglePassword('#togglePassword', '#<%= txtPassword.ClientID %>');
        });
    </script>
</asp:Content>
<%@ Page Title="Crear Cuenta" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Vistas.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Estilos específicos si fueran necesarios --%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    
    <asp:UpdatePanel ID="upRegistro" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row justify-content-center align-items-center" style="min-height: 70vh;">
                <div class="col-md-7 col-lg-6 col-xl-5">

                    <div class="card shadow-sm border-0 rounded-lg">
                        <div class="card-body p-4 p-sm-5">

                            <div class="text-center mb-4">
                                <i class="bi bi-person-plus-fill fs-1 text-primary"></i>
                                <h3 class="card-title fw-bold mt-2">Crear una cuenta</h3>
                                <p class="text-muted">Únete a nuestra comunidad y empieza a aprender.</p>
                            </div>

                            <%-- Mensaje de Error (solo errores, éxito va al modal) --%>
                            <asp:Panel runat="server" ID="pnlError" Visible="false" CssClass="alert alert-danger" EnableViewState="false">
                                <asp:Literal runat="server" ID="litErrorMessage" />
                            </asp:Panel>

                            <div class="mb-3">
                                <label for="<%= txtEmailRegistro.ClientID %>" class="form-label">Email <span class="text-danger">*</span></label>
                                <asp:TextBox runat="server" ID="txtEmailRegistro" type="email" CssClass="form-control form-control-lg" placeholder="tu@email.com" />
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="El email es requerido." ControlToValidate="txtEmailRegistro" Display="Dynamic" CssClass="text-danger small" ValidationGroup="RegistroGroup"/>
                                <asp:RegularExpressionValidator runat="server" ErrorMessage="Ingresa un email válido." ControlToValidate="txtEmailRegistro" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" CssClass="text-danger small" ValidationGroup="RegistroGroup"/>
                            </div>

                            <div class="mb-3">
                                <label for="<%= txtPasswordRegistro.ClientID %>" class="form-label">Contraseña <span class="text-danger">*</span></label>
                                <div class="input-group input-group-lg">
                                    <asp:TextBox runat="server" ID="txtPasswordRegistro" TextMode="Password" CssClass="form-control" placeholder="Crea una contraseña segura" />
                                    <button class="btn btn-outline-secondary" type="button" disabled><i class="bi bi-eye"></i></button>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="La contraseña es requerida." ControlToValidate="txtPasswordRegistro" Display="Dynamic" CssClass="text-danger small" ValidationGroup="RegistroGroup"/>
                            </div>

                            <div class="mb-4">
                                <label for="<%= txtConfirmPassword.ClientID %>" class="form-label">Confirmar Contraseña <span class="text-danger">*</span></label>
                                <div class="input-group input-group-lg">
                                    <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" CssClass="form-control" placeholder="Vuelve a escribir la contraseña" />
                                    <button class="btn btn-outline-secondary" type="button" disabled><i class="bi bi-eye-slash"></i></button>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="Confirma la contraseña." ControlToValidate="txtConfirmPassword" Display="Dynamic" CssClass="text-danger small" ValidationGroup="RegistroGroup"/>
                                <asp:CompareValidator runat="server" ErrorMessage="Las contraseñas no coinciden." ControlToValidate="txtConfirmPassword" ControlToCompare="txtPasswordRegistro" Operator="Equal" Display="Dynamic" CssClass="text-danger small" ValidationGroup="RegistroGroup"/>
                            </div>

                            <div class="d-grid">
                                <asp:Button runat="server" ID="btnRegistrarse" Text="Registrarse" CssClass="btn btn-primary btn-lg" OnClick="btnRegistrarse_Click" ValidationGroup="RegistroGroup"/>
                            </div>

                            <div class="text-center mt-4">
                                <span class="text-muted small">¿Ya tienes una cuenta?</span>
                                <asp:HyperLink NavigateUrl="~/Auth/Loguin.aspx" Text="Inicia sesión" CssClass="fw-semibold text-decoration-none ms-1" runat="server" />
                            </div>

                        </div>
                    </div>

                </div> <%-- Fin col --%>
            </div> <%-- Fin row --%>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Modal -->
<div class="modal fade" id="registroExitosoModal" runat="server" aria-labelledby="registroExitosoModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-header bg-success text-white">
                <h5 class="modal-title" id="registroExitosoModalLabel">✅ ¡Registro Casi Completo!</h5>
                <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <p>Te hemos enviado un correo electrónico a <strong id="emailUsuarioModal" runat="server"></strong>.</p>
                <p>Por favor, haz clic en el enlace de verificación para activar tu cuenta y poder iniciar sesión.</p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" onclick="redirectToLogin()">Aceptar e ir a Login</button>
            </div>
        </div>
    </div>
</div>

    <script type="text/javascript">
        function redirectToLogin() {
            window.location.href = '<%= ResolveUrl("~/Auth/Loguin.aspx") %>';
        }
    </script>

</asp:Content>

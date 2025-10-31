<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RestablecerContraseña.aspx.cs" Inherits="Vistas.Auth.RestablecerContraseña" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center align-items-center" style="min-height: 70vh;">
        <div class="col-md-7 col-lg-6 col-xl-5">

            <div class="card shadow-sm border-0 rounded-lg">
                <div class="card-body p-4 p-sm-5">

                    <div class="text-center mb-4">
                        <%-- Icono de candado --%>
                        <i class="bi bi-key-fill fs-1 text-primary"></i>
                        <h3 class="card-title fw-bold mt-2">Crear Nueva Contraseña</h3>
                        <p class="text-muted">Ingresa y confirma tu nueva contraseña para acceder a tu cuenta.</p>
                    </div>

                    <%-- Mensaje de Error (para errores de token o DB) --%>
                    <asp:Panel runat="server" ID="pnlError" Visible="false" CssClass="alert alert-danger" EnableViewState="false">
                        <asp:Literal runat="server" ID="litErrorMessage" />
                    </asp:Panel>

                    <%-- Panel para el Formulario (Se mostrará solo si el token es válido) --%>
                    <asp:Panel ID="pnlFormulario" runat="server" Visible="true"> 
                    
                        <div class="mb-3">
                            <label for="<%= txtNuevaPassword.ClientID %>" class="form-label">Nueva Contraseña <span class="text-danger">*</span></label>
                            <div class="input-group input-group-lg">
                                <asp:TextBox runat="server" ID="txtNuevaPassword" TextMode="Password" CssClass="form-control" placeholder="Ingresa tu nueva contraseña" />
                                <%-- Botón de mostrar/ocultar --%>
                                <button class="btn btn-outline-secondary" type="button" id="btnShowNuevaPassword"><i class="bi bi-eye"></i></button>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="La nueva contraseña es requerida." ControlToValidate="txtNuevaPassword" Display="Dynamic" CssClass="text-danger small" ValidationGroup="ResetGroup"/>
                        </div>

                        <div class="mb-4">
                            <label for="<%= txtConfirmacionPassword.ClientID %>" class="form-label">Confirmar Contraseña <span class="text-danger">*</span></label>
                            <div class="input-group input-group-lg">
                                <asp:TextBox runat="server" ID="txtConfirmacionPassword" TextMode="Password" CssClass="form-control" placeholder="Confirma la nueva contraseña" />
                                <button class="btn btn-outline-secondary" type="button" id="btnShowConfirmacionPassword"><i class="bi bi-eye"></i></button>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Debes confirmar la contraseña." ControlToValidate="txtConfirmacionPassword" Display="Dynamic" CssClass="text-danger small" ValidationGroup="ResetGroup"/>
                            <asp:CompareValidator runat="server" ErrorMessage="Las contraseñas no coinciden." ControlToValidate="txtConfirmacionPassword" ControlToCompare="txtNuevaPassword" Operator="Equal" Display="Dynamic" CssClass="text-danger small" ValidationGroup="ResetGroup"/>
                        </div>

                        <div class="d-grid">
                            <asp:Button runat="server" ID="btnCambiarPassword" Text="Cambiar Contraseña" CssClass="btn btn-primary btn-lg"  ValidationGroup="ResetGroup"/>
                        </div>
                    </asp:Panel>

                    <%-- Panel para Mensajes de Éxito o Token Inválido --%>
                    <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
                        <div class="text-center py-4">
                            <i class="bi bi-exclamation-triangle-fill fs-1 text-danger mb-3"></i>
                            <h4 class="fw-bold">Token Inválido o Expirado</h4>
                            <p class="text-muted">El enlace que has utilizado no es válido o ha caducado. Por favor, solicita un nuevo enlace para restablecer tu contraseña.</p>
                            <asp:HyperLink NavigateUrl="~/Auth/RecuperarContrasena.aspx" Text="Solicitar nuevo enlace" CssClass="btn btn-outline-primary mt-3" runat="server" />
                        </div>
                    </asp:Panel>


                    <div class="text-center mt-4">
                        <asp:HyperLink NavigateUrl="~/Auth/Loguin.aspx" Text="Volver al Inicio de Sesión" CssClass="fw-semibold text-decoration-none small" runat="server" />
                    </div>

                </div>
            </div>

        </div>
    </div>

    <script src="../Assets/js/togglePassword.js"></script>
    <script type="text/javascript">
        // Lógica para los ojitos (asumiendo que tu JS está en Assets/js/togglePassword.js)
        document.addEventListener('DOMContentLoaded', function () {
            // Revisa el archivo togglePassword.js para asegurarte que la función acepte selectores
            togglePassword('#btnShowNuevaPassword', '#<%= txtNuevaPassword.ClientID %>');
            togglePassword('#btnShowConfirmacionPassword', '#<%= txtConfirmacionPassword.ClientID %>');
        });
    </script>
</asp:Content>

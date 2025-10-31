<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RecuperarContraseña.aspx.cs" Inherits="Vistas.Auth.RecuperarContraseña" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center align-items-center" style="min-height: 70vh;">
        <div class="col-md-7 col-lg-6 col-xl-5">

            <div class="card shadow-sm border-0 rounded-lg">
                <div class="card-body p-4 p-sm-5">
                    
                    <div class="text-center mb-4">
                        <%-- Icono de candado para la contraseña --%>
                        <i class="bi bi-person-lock fs-1 text-primary"></i> 
                        <h3 class="card-title fw-bold mt-2">Recuperar Contraseña</h3>
                    </div>

                    <%-- Panel Principal: Solicitud de Email (Paso 1) --%>
                    <asp:Panel ID="pnlEmailSolicitud" runat="server">
                        <p class="text-muted text-center mb-4">
                            Ingresa el email asociado a tu cuenta. Te enviaremos un enlace para que puedas crear una nueva contraseña.
                        </p>

                        <%-- Mensaje de Error (si el email no existe) --%>
                         <asp:Panel runat="server" ID="pnlError" Visible="false" CssClass="alert alert-danger" EnableViewState="false">
                            <asp:Literal runat="server" ID="litErrorMessage" />
                        </asp:Panel>

                        <div class="mb-4">
                            <label for="<%= txtEmailRecuperacion.ClientID %>" class="form-label">Email</label>
                            <asp:TextBox runat="server" ID="txtEmailRecuperacion" type="email" CssClass="form-control form-control-lg" placeholder="tu.email@ejemplo.com" />
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="El email es requerido." ControlToValidate="txtEmailRecuperacion" Display="Dynamic" CssClass="text-danger small" ValidationGroup="RecuperarGroup" />
                            <asp:RegularExpressionValidator runat="server" ErrorMessage="Ingresa un email válido." ControlToValidate="txtEmailRecuperacion" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" CssClass="text-danger small" ValidationGroup="RecuperarGroup" />
                        </div>

                        <div class="d-grid">
                            <asp:Button runat="server" ID="btnRestablecer" Text="Restablecer Contraseña" CssClass="btn btn-primary btn-lg" ValidationGroup="RecuperarGroup" />
                        </div>
                    </asp:Panel>
                    
                    <%-- Panel de Confirmación de Envío (Paso 2) --%>
                    <asp:Panel ID="pnlEnvioConfirmado" runat="server" Visible="false">
                        <div class="text-center py-4">
                            <i class="bi bi-envelope-check-fill fs-1 text-success mb-3"></i>
                            <h4 class="fw-bold">Correo Enviado</h4>
                            <p class="text-muted">Hemos enviado las instrucciones a tu correo. Revisa la bandeja de entrada y, si no lo encuentras, revisa el spam.</p>
                        </div>
                    </asp:Panel>


                    <div class="text-center mt-4">
                        <%-- Volver al login --%>
                        <asp:HyperLink NavigateUrl="~/Auth/Loguin.aspx" Text="Volver al Inicio de Sesión" CssClass="fw-semibold text-decoration-none small" runat="server" />
                    </div>

                </div>
            </div>

        </div>
    </div>
</asp:Content>

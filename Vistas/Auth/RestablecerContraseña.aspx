<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RestablecerContraseña.aspx.cs" Inherits="Vistas.Auth.RestablecerContraseña" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center align-items-center" style="min-height: 70vh;">
        <div class="col-md-7 col-lg-6 col-xl-5">

            <div class="card shadow-sm border-0 rounded-lg">
                <div class="card-body p-4 p-sm-5">
                    
                    <div class="text-center mb-4">
                        <i class="bi bi-shield-lock-fill fs-1 text-danger"></i> 
                        <h3 class="card-title fw-bold mt-2">Crear Nueva Contraseña</h3>
                        <p class="text-muted small">Ingresa tu nueva contraseña segura.</p>
                    </div>

                    <%-- Panel de Mensaje (Éxito/Error de Token) --%>
                   
                    <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert" EnableViewState="false">
                        <h4 class="alert-heading fw-bold"><asp:Literal ID="litTitulo" runat="server" /></h4>
                        <p><asp:Literal ID="litMensaje" runat="server" /></p>
                        <hr />
                       
                        <asp:HyperLink ID="hlLogin" NavigateUrl="~/Auth/Loguin.aspx" Text="Ir al Inicio de Sesión" CssClass="btn btn-sm btn-outline-secondary" runat="server" Visible="false"/>
                    </asp:Panel>

                    <%-- Panel Principal: Formulario de Nueva Contraseña --%>
                    <asp:Panel ID="pnlFormulario" runat="server" Visible="false">
                        
                        <%-- Mensaje de Error (de Negocio, ej. falla al guardar) --%>
                        <asp:Panel runat="server" ID="pnlError" Visible="false" CssClass="alert alert-danger" EnableViewState="false">
                            <asp:Literal runat="server" ID="litErrorMessage" />
                        </asp:Panel>

                        <%-- Campo 1: Nueva Contraseña --%>
                        <div class="mb-4">
                            <label for="<%= txtNuevaPassword.ClientID %>" class="form-label">Nueva Contraseña</label>
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="txtNuevaPassword" type="password" CssClass="form-control form-control-lg" placeholder="Contraseña segura" />
                                <button class="btn btn-outline-secondary" type="button" id="btnShowPassword"><i class="bi bi-eye-slash-fill"></i></button>
                            </div>
                            
                            <%-- 1. VALIDACIÓN: Campo Requerido --%>
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="La contraseña es requerida." ControlToValidate="txtNuevaPassword" Display="Dynamic" CssClass="text-danger small" ValidationGroup="RestablecerGroup" />
                            <%-- 2. VALIDACIÓN: Expresión Regular  --%>
                            <asp:RegularExpressionValidator runat="server" 
                                ErrorMessage="Mínimo 8 caracteres, al menos 1 mayúscula, 1 minúscula y 1 número." 
                                ControlToValidate="txtNuevaPassword" 
                                ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$" 
                                Display="Dynamic" CssClass="text-danger small" ValidationGroup="RestablecerGroup" />
                        </div>

                        <%-- Campo 2: Confirmar Contraseña --%>
                        <div class="mb-4">
                            <label for="<%= txtConfirmarPassword.ClientID %>" class="form-label">Confirmar Contraseña</label>
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="txtConfirmarPassword" type="password" CssClass="form-control form-control-lg" placeholder="Repite la contraseña" />
                                <button class="btn btn-outline-secondary" type="button" id="btnShowConfirmPassword"><i class="bi bi-eye-slash-fill"></i></button>
                            </div>
                            
                            <%-- 3. VALIDACIÓN: Comparar con el campo anterior --%>
                            <asp:CompareValidator runat="server" ErrorMessage="Las contraseñas no coinciden." 
                                ControlToValidate="txtConfirmarPassword" 
                                ControlToCompare="txtNuevaPassword" 
                                Operator="Equal" 
                                Type="String" 
                                Display="Dynamic" CssClass="text-danger small" ValidationGroup="RestablecerGroup" />
                        </div>

                        <div class="d-grid">
                            <asp:Button runat="server" ID="btnCambiarPassword" Text="Guardar Nueva Contraseña" OnClick="btnCambiarPassword_Click" CssClass="btn btn-danger btn-lg" ValidationGroup="RestablecerGroup" />
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
        document.addEventListener('DOMContentLoaded', function () {
            togglePassword('#btnShowPassword', '#<%= txtNuevaPassword.ClientID %>');
        togglePassword('#btnShowConfirmPassword', '#<%= txtConfirmarPassword.ClientID %>');
    });
    </script>
</asp:Content>
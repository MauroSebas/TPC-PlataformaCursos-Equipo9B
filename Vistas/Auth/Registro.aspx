<%@ Page Title="Crear Cuenta" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Vistas.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Podríamos agregar CSS específico si fuera necesario --%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center align-items-center" style="min-height: 70vh;"> <%-- Aumenté un poco min-height --%>
        <div class="col-md-7 col-lg-6 col-xl-5">

            <div class="card shadow-sm border-0 rounded-lg">
                <div class="card-body p-4 p-sm-5">

                    <div class="text-center mb-4">
                        <i class="bi bi-person-plus-fill fs-1 text-primary"></i>
                        <h3 class="card-title fw-bold mt-2">Crear una cuenta</h3>
                        <p class="text-muted">Únete a nuestra comunidad y empieza a aprender.</p>
                    </div>

                    <%-- Mensaje de Error/Éxito --%>
                    <asp:Literal runat="server" ID="litMensaje" EnableViewState="false" />

                    <div class="mb-3">
                        <label for="<%= txtEmailRegistro.ClientID %>" class="form-label">Email <span class="text-danger">*</span></label>
                        <asp:TextBox runat="server" ID="txtEmailRegistro" type="email" CssClass="form-control form-control-lg" placeholder="tu@email.com" />
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="El email es requerido." ControlToValidate="txtEmailRegistro" Display="Dynamic" CssClass="text-danger small" />
                        <asp:RegularExpressionValidator runat="server" ErrorMessage="Ingresa un email válido." ControlToValidate="txtEmailRegistro" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" CssClass="text-danger small" />
                    </div>

                    <div class="mb-3">
                        <label for="<%= txtPasswordRegistro.ClientID %>" class="form-label">Contraseña <span class="text-danger">*</span></label>
                        <div class="input-group input-group-lg">
                            <asp:TextBox runat="server" ID="txtPasswordRegistro" TextMode="Password" CssClass="form-control" placeholder="Crea una contraseña segura" />
                             <%-- Botón para mostrar/ocultar (requiere JS adicional, no implementado aquí) --%>
                            <button class="btn btn-outline-secondary" type="button" disabled>
                                <i class="bi bi-eye"></i>
                            </button>
                        </div>
                         <asp:RequiredFieldValidator runat="server" ErrorMessage="La contraseña es requerida." ControlToValidate="txtPasswordRegistro" Display="Dynamic" CssClass="text-danger small" />
                         <%-- Podrías agregar un validador de complejidad de contraseña aquí --%>
                    </div>

                    <div class="mb-4">
                        <label for="<%= txtConfirmPassword.ClientID %>" class="form-label">Confirmar Contraseña <span class="text-danger">*</span></label>
                        <div class="input-group input-group-lg">
                            <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" CssClass="form-control" placeholder="Vuelve a escribir la contraseña" />
                            <button class="btn btn-outline-secondary" type="button" disabled>
                                 <i class="bi bi-eye-slash"></i>
                            </button>
                        </div>
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Confirma la contraseña." ControlToValidate="txtConfirmPassword" Display="Dynamic" CssClass="text-danger small" />
                        <asp:CompareValidator runat="server" ErrorMessage="Las contraseñas no coinciden." ControlToValidate="txtConfirmPassword" ControlToCompare="txtPasswordRegistro" Operator="Equal" Display="Dynamic" CssClass="text-danger small" />
                    </div>

                    <div class="d-grid">
                        <%-- **** CAMBIO PRINCIPAL AQUÍ **** --%>
                        <asp:Button runat="server" ID="btnRegistrarse" Text="Registrarse" CssClass="btn btn-primary btn-lg" OnClick="btnRegistrarse_Click" />
                    </div>

                    <div class="text-center mt-4">
                        <span class="text-muted small">¿Ya tienes una cuenta?</span>
                        <%-- Asegúrate que el NavigateUrl esté bien si moviste Loguin.aspx a /Auth --%>
                        <asp:HyperLink NavigateUrl="~/Auth/Loguin.aspx" Text="Inicia sesión" CssClass="fw-semibold text-decoration-none ms-1" runat="server" />
                    </div>

                </div>
            </div>

        </div>
    </div>
</asp:Content>
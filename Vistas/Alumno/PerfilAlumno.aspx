<%@ Page Title="" Language="C#" MasterPageFile="~/Alumno.Master" AutoEventWireup="true" CodeBehind="PerfilAlumno.aspx.cs" Inherits="Vistas.Alumno.PerfilAlumno1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="h2 mb-4 fw-bold">Mi Perfil</h1>

<div class="card shadow-sm border-0">
    <div class="card-body p-4 p-md-5">

        <div class="row align-items-center mb-4 pb-4 border-bottom">
            <div class="col-auto">
                <asp:Image ID="imgAvatar" runat="server" alt="Avatar" Width="96" Height="96" CssClass="rounded-circle" />
            </div>
            <div class="col">
                <h4 class="fw-bold mb-1">
                    <asp:Literal ID="litNombreUsuario" runat="server" Text="Nombre de Usuario" />
                </h4>
                <p class="text-muted mb-2">Sube una nueva foto de perfil.</p>
                <asp:FileUpload ID="fileUploadAvatar" runat="server" CssClass="form-control form-control-sm" Style="max-width: 300px;" />
            </div>
        </div>

        <div class="row g-3">
            <div class="col-md-6">
                <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="form-label fw-medium" AssociatedControlID="txtNombre" />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej: Juan" />
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                    ControlToValidate="txtNombre" ErrorMessage="El nombre es obligatorio" 
                    CssClass="text-danger small" Display="Dynamic" 
                    ValidationGroup="DatosPersonales" />
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblApellido" runat="server" Text="Apellido" CssClass="form-label fw-medium" AssociatedControlID="txtApellido" />
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ej: Pérez" />
                <asp:RequiredFieldValidator ID="rfvApellido" runat="server" 
                    ControlToValidate="txtApellido" ErrorMessage="El apellido es obligatorio" 
                    CssClass="text-danger small" Display="Dynamic" 
                    ValidationGroup="DatosPersonales" />
            </div>
            <div class="col-12">
                <asp:Label ID="lblLocalidad" runat="server" Text="Localidad (Opcional)" CssClass="form-label fw-medium" AssociatedControlID="txtLocalidad" />
                <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" placeholder="Ej: Paraná, Entre Ríos" />
            </div>
        </div>

        <hr class="my-4" />
        <div class="mb-3">
            <asp:Label ID="lblEmail" runat="server" Text="Dirección de Email" CssClass="form-label fw-medium" />
            <div class="input-group">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Enabled="false" />
                <asp:Button ID="btnMostrarPanelEmail" runat="server" Text="Cambiar Email" CssClass="btn btn-outline-secondary" OnClick="btnMostrarPanelEmail_Click" CausesValidation="false" />
            </div>
        </div>

        <asp:Panel ID="pnlCambiarEmail" runat="server" Visible="false" CssClass="p-3 border rounded bg-body-tertiary mb-3">
            <h5 class="fw-bold">Cambiar Email</h5>
            <p class="small text-body-secondary">
                Para confirmar el cambio, ingresa tu nuevo email y tu contraseña actual.
            </p>
            
            <div class="mb-3">
                <asp:Label ID="lblNuevoEmail" runat="server" Text="Nuevo Email" CssClass="form-label" AssociatedControlID="txtNuevoEmail" />
                <asp:TextBox ID="txtNuevoEmail" runat="server" CssClass="form-control" placeholder="nuevo@correo.com" />
                <asp:RequiredFieldValidator ID="rfvNuevoEmail" runat="server"
                    ControlToValidate="txtNuevoEmail" ErrorMessage="El nuevo email es obligatorio."
                    CssClass="text-danger small" Display="Dynamic"
                    ValidationGroup="CambiarEmail" />
                <asp:RegularExpressionValidator ID="revNuevoEmail" runat="server"
                    ControlToValidate="txtNuevoEmail" ErrorMessage="Ingresa un email válido."
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    CssClass="text-danger small" Display="Dynamic"
                    ValidationGroup="CambiarEmail" />
            </div>

            <div class="mb-3">
                <asp:Label ID="lblPassConfirmarEmail" runat="server" Text="Contraseña Actual" CssClass="form-label" AssociatedControlID="txtPassConfirmarEmail" />
                <div class="input-group">
                    <asp:TextBox ID="txtPassConfirmarEmail" runat="server" CssClass="form-control" TextMode="Password" />
                    <button class="btn btn-outline-secondary" type="button" id="btnShowPassConfirmarEmail"><i class="bi bi-eye"></i></button>
                </div>
                <asp:RequiredFieldValidator ID="rfvPassConfirmarEmail" runat="server"
                    ControlToValidate="txtPassConfirmarEmail" ErrorMessage="Tu contraseña actual es obligatoria."
                    CssClass="text-danger small" Display="Dynamic"
                    ValidationGroup="CambiarEmail" />
            </div>

            <asp:Button ID="btnConfirmarEmail" runat="server" Text="Confirmar y Cambiar Email" 
                CssClass="btn btn-primary" OnClick="btnConfirmarEmail_Click" 
                ValidationGroup="CambiarEmail" />
            <asp:Button ID="btnCancelarEmail" runat="server" Text="Cancelar" CssClass="btn btn-link text-muted" OnClick="btnCancelarEmail_Click" CausesValidation="false" />
        </asp:Panel>


        <hr class="my-4" />
        <div class="mb-3">
            <asp:Label ID="lblPassword" runat="server" Text="Contraseña" CssClass="form-label fw-medium" />
            <div class="input-group">
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" Text="••••••••••" Enabled="false" />
                <asp:Button ID="btnMostrarPanelPassword" runat="server" Text="Cambiar Contraseña" CssClass="btn btn-outline-secondary" OnClick="btnMostrarPanelPassword_Click" CausesValidation="false" />
            </div>
        </div>

        <asp:Panel ID="pnlCambiarPassword" runat="server" Visible="false" CssClass="p-3 border rounded bg-body-tertiary mb-3">
            <h5 class="fw-bold">Cambiar Contraseña</h5>
            <p class="small text-body-secondary">Para cambiar tu contraseña, primero debés ingresar tu contraseña actual.</p>
            
            <div class="row justify-content-center">
                <div class="col-lg-8">
                    
                    <div class="mb-3">
                        <asp:Label ID="lblPassActual" runat="server" Text="Contraseña Actual" CssClass="form-label" AssociatedControlID="txtPassActual" />
                        <div class="input-group">
                            <asp:TextBox ID="txtPassActual" runat="server" CssClass="form-control" TextMode="Password" />
                            <button class="btn btn-outline-secondary" type="button" id="btnShowPassActual"><i class="bi bi-eye"></i></button>
                        </div>
                        <asp:RequiredFieldValidator ID="rfvPassActual" runat="server"
                            ControlToValidate="txtPassActual" ErrorMessage="La contraseña actual es obligatoria."
                            CssClass="text-danger small" Display="Dynamic"
                            ValidationGroup="CambiarPassword" />
                    </div>

                    <div class="mb-3">
                        <asp:Label ID="lblPassNueva" runat="server" Text="Contraseña Nueva" CssClass="form-label" AssociatedControlID="txtPassNueva" />
                        <div class="input-group">
                            <asp:TextBox ID="txtPassNueva" runat="server" CssClass="form-control" TextMode="Password" />
                            <button class="btn btn-outline-secondary" type="button" id="btnShowPassNueva"><i class="bi bi-eye"></i></button>
                        </div>
                        <asp:RequiredFieldValidator ID="rfvPassNueva" runat="server"
                            ControlToValidate="txtPassNueva" ErrorMessage="La contraseña nueva es obligatoria."
                            CssClass="text-danger small" Display="Dynamic"
                            ValidationGroup="CambiarPassword" />
                    </div>

                    <div class="mb-3">
                        <asp:Label ID="lblPassRepetir" runat="server" Text="Repetir Contraseña Nueva" CssClass="form-label" AssociatedControlID="txtPassRepetir" />
                        <div class="input-group">
                            <asp:TextBox ID="txtPassRepetir" runat="server" CssClass="form-control" TextMode="Password" />
                            <button class="btn btn-outline-secondary" type="button" id="btnShowPassRepetir"><i class="bi bi-eye"></i></button>
                        </div>
                        <asp:RequiredFieldValidator ID="rfvPassRepetir" runat="server"
                            ControlToValidate="txtPassRepetir" ErrorMessage="Debes repetir la contraseña."
                            CssClass="text-danger small" Display="Dynamic"
                            ValidationGroup="CambiarPassword" />
                        <asp:CompareValidator ID="cvPassRepetir" runat="server"
                            ControlToValidate="txtPassRepetir" ControlToCompare="txtPassNueva"
                            ErrorMessage="Las contraseñas nuevas no coinciden."
                            CssClass="text-danger small" Display="Dynamic"
                            Operator="Equal" ValidationGroup="CambiarPassword" />
                    </div>

                    <div class="mt-4">
                        <asp:Button ID="btnConfirmarPassword" runat="server" Text="Guardar Contraseña" 
                            CssClass="btn btn-primary" OnClick="btnConfirmarPassword_Click" 
                            ValidationGroup="CambiarPassword" />
                        <asp:Button ID="btnCancelarPassword" runat="server" Text="Cancelar" CssClass="btn btn-link text-muted" OnClick="btnCancelarPassword_Click" CausesValidation="false" />
                    </div>

                </div> 
            </div> 
        </asp:Panel>

        <hr class="my-4" />
        <div class="text-end">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Datos Personales" 
                CssClass="btn btn-primary btn-lg" OnClick="btnGuardar_Click" 
                ValidationGroup="DatosPersonales" />
        </div>

    </div> </div> <script src="<%= ResolveUrl("~/Assets/js/togglePassword.js") %>"></script>
<script type="text/javascript">
    document.addEventListener('DOMContentLoaded', function () {
        // Los 3 del panel de contraseña
        togglePassword('#btnShowPassActual', '#<%= txtPassActual.ClientID %>');
        togglePassword('#btnShowPassNueva', '#<%= txtPassNueva.ClientID %>');
        togglePassword('#btnShowPassRepetir', '#<%= txtPassRepetir.ClientID %>');
        
        // El "ojito" nuevo para el panel de email
        togglePassword('#btnShowPassConfirmarEmail', '#<%= txtPassConfirmarEmail.ClientID %>');
    });
</script>
</asp:Content>

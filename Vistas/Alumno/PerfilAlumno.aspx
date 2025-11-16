<%@ Page Title="" Language="C#" MasterPageFile="~/Alumno.Master" AutoEventWireup="true" CodeBehind="PerfilAlumno.aspx.cs" Inherits="Vistas.Alumno.PerfilAlumno1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .avatar-editable {
            cursor: pointer;
            transition: opacity 0.2s ease-in-out;
        }

            .avatar-editable:hover {
                opacity: 0.8;
            }
    </style>
    

    <div class="card shadow-sm border-0">
        <div class="card-body p-4 p-md-5">

            <asp:UpdatePanel ID="updMensajeGlobal" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnlMensajeGlobal" runat="server" Visible="false" EnableViewState="false" CssClass="alert">
                        <asp:Literal ID="litMensajeGlobal" runat="server" />
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="row align-items-center mb-4 pb-4 border-bottom">
                <div class="col-auto">
                    <asp:Image ID="imgAvatar" runat="server" alt="Avatar" Width="96" Height="96"
                        CssClass="rounded-circle avatar-editable"
                        data-bs-toggle="modal" data-bs-target="#modalAvatar" />
                </div>
                <div class="col">
                    <h4 class="fw-bold mb-1">
                        <asp:Literal ID="litNombreUsuario" runat="server" Text="Nombre de Usuario" />
                    </h4>
                    <p class="text-muted mb-2">Haz clic en tu foto para cambiarla.</p>
                   
                </div>
            </div>

            <asp:UpdatePanel ID="updDatosPersonales" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row g-3">
                        <%-- ... (Tus campos txtNombre, txtApellido, txtLocalidad y sus validadores... perfectos) ... --%>
                        <div class="col-md-6">
                            <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="form-label fw-medium" AssociatedControlID="txtNombre" />
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej: Juan" />
                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="El nombre es obligatorio." CssClass="text-danger small" Display="Dynamic" ValidationGroup="DatosPersonales" />
                            <asp:RegularExpressionValidator ID="revNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="Debe tener entre 4 y 20 caracteres." ValidationExpression="^.{4,20}$" CssClass="text-danger small" Display="Dynamic" ValidationGroup="DatosPersonales" />
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblApellido" runat="server" Text="Apellido" CssClass="form-label fw-medium" AssociatedControlID="txtApellido" />
                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ej: Pérez" />
                            <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtApellido" ErrorMessage="El apellido es obligatorio." CssClass="text-danger small" Display="Dynamic" ValidationGroup="DatosPersonales" />
                            <asp:RegularExpressionValidator ID="revApellido" runat="server" ControlToValidate="txtApellido" ErrorMessage="Debe tener entre 4 y 20 caracteres." ValidationExpression="^.{4,20}$" CssClass="text-danger small" Display="Dynamic" ValidationGroup="DatosPersonales" />
                        </div>
                        <div class="col-12">
                            <asp:Label ID="lblLocalidad" runat="server" Text="Localidad (Opcional)" CssClass="form-label fw-medium" AssociatedControlID="txtLocalidad" />
                            <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" placeholder="Ej: Paraná, Entre Ríos" />
                            <asp:RegularExpressionValidator ID="revLocalidad" runat="server" ControlToValidate="txtLocalidad" ErrorMessage="Debe tener entre 4 y 20 caracteres." ValidationExpression="^$|^.{4,20}$" CssClass="text-danger small" Display="Dynamic" ValidationGroup="DatosPersonales" />
                        </div>
                    </div>

                    <hr class="my-4" />
                    <div class="text-end">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Datos Personales"
                            CssClass="btn btn-primary btn-lg" OnClick="btnGuardar_Click"
                            ValidationGroup="DatosPersonales" />
                    </div>
                </ContentTemplate>
                <Triggers>
                <asp:PostBackTrigger ControlID="btnGuardar" />
            </Triggers>
            </asp:UpdatePanel>

            <hr class="my-4" />
            <div class="mb-3">
                <asp:Label ID="lblEmail" runat="server" Text="Dirección de Email" CssClass="form-label fw-medium" />
                <div class="input-group">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Enabled="false" />
                    <asp:Button ID="btnMostrarPanelEmail" runat="server" Text="Cambiar Email" CssClass="btn btn-outline-secondary" OnClick="btnMostrarPanelEmail_Click" CausesValidation="false" />
                </div>
            </div>

            <asp:UpdatePanel ID="updEmail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnlCambiarEmail" runat="server" Visible="false" CssClass="p-3 border rounded bg-body-tertiary mb-3">
                        <h5 class="fw-bold">Cambiar Email</h5>
                        <p class="small text-body-secondary">
                            Para confirmar el cambio, ingresa tu nuevo email y tu contraseña actual.
                       
                        </p>

                        <asp:Panel ID="pnlErrorEmail" runat="server" Visible="false" EnableViewState="false" CssClass="alert alert-danger">
                            <asp:Literal ID="litErrorEmail" runat="server" />
                        </asp:Panel>

                        <div class="mb-3">
                            <%-- ... (tus campos y validadores de Nuevo Email... perfectos) ... --%>
                            <asp:Label ID="lblNuevoEmail" runat="server" Text="Nuevo Email" CssClass="form-label" AssociatedControlID="txtNuevoEmail" />
                            <asp:TextBox ID="txtNuevoEmail" runat="server" CssClass="form-control" placeholder="nuevo@correo.com" />
                            <asp:RequiredFieldValidator ID="rfvNuevoEmail" runat="server" ControlToValidate="txtNuevoEmail" ErrorMessage="El nuevo email es obligatorio." CssClass="text-danger small" Display="Dynamic" ValidationGroup="CambiarEmail" />
                            <asp:RegularExpressionValidator ID="revNuevoEmail" runat="server" ControlToValidate="txtNuevoEmail" ErrorMessage="Ingresa un email válido." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="text-danger small" Display="Dynamic" ValidationGroup="CambiarEmail" />
                        </div>
                        <div class="mb-3">
                            <%-- ... (tus campos y validadores de Contraseña Actual... perfectos) ... --%>
                            <asp:Label ID="lblPassConfirmarEmail" runat="server" Text="Contraseña Actual" CssClass="form-label" AssociatedControlID="txtPassConfirmarEmail" />
                            <div class="input-group">
                                <asp:TextBox ID="txtPassConfirmarEmail" runat="server" CssClass="form-control" TextMode="Password" />
                                <button class="btn btn-outline-secondary" type="button" id="btnShowPassConfirmarEmail"><i class="bi bi-eye"></i></button>
                            </div>
                            <asp:RequiredFieldValidator ID="rfvPassConfirmarEmail" runat="server" ControlToValidate="txtPassConfirmarEmail" ErrorMessage="Tu contraseña actual es obligatoria." CssClass="text-danger small" Display="Dynamic" ValidationGroup="CambiarEmail" />
                        </div>
                        <asp:Button ID="btnConfirmarEmail" runat="server" Text="Confirmar y Cambiar Email"
                            CssClass="btn btn-primary" OnClick="btnConfirmarEmail_Click"
                            ValidationGroup="CambiarEmail" />
                        <asp:Button ID="btnCancelarEmail" runat="server" Text="Cancelar" CssClass="btn btn-link text-muted" OnClick="btnCancelarEmail_Click" CausesValidation="false" />
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnMostrarPanelEmail" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

            <hr class="my-4" />
            <div class="mb-3">
                <asp:Label ID="lblPassword" runat="server" Text="Contraseña" CssClass="form-label fw-medium" />
                <div class="input-group">
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" Text="••••••••••" Enabled="false" />
                    <asp:Button ID="btnMostrarPanelPassword" runat="server" Text="Cambiar Contraseña" CssClass="btn btn-outline-secondary" OnClick="btnMostrarPanelPassword_Click" CausesValidation="false" />
                </div>
            </div>

            <asp:UpdatePanel ID="updPassword" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnlCambiarPassword" runat="server" Visible="false" CssClass="p-3 border rounded bg-body-tertiary mb-3">
                        <h5 class="fw-bold">Cambiar Contraseña</h5>
                        <p class="small text-body-secondary">Para cambiar tu contraseña, primero debés ingresar tu contraseña actual.</p>

                        <asp:Panel ID="pnlErrorPassword" runat="server" Visible="false" EnableViewState="false" CssClass="alert alert-danger">
                            <asp:Literal ID="litErrorPassword" runat="server" />
                        </asp:Panel>

                        <div class="row justify-content-center">
                            <div class="col-lg-8">
                                <%-- ... (tus campos y validadores de Contraseña Actual, Nueva y Repetir... perfectos) ... --%>
                                <div class="mb-3">
                                    <asp:Label ID="lblPassActual" runat="server" Text="Contraseña Actual" CssClass="form-label" AssociatedControlID="txtPassActual" />
                                    <div class="input-group">
                                        <asp:TextBox ID="txtPassActual" runat="server" CssClass="form-control" TextMode="Password" />
                                        <button class="btn btn-outline-secondary" type="button" id="btnShowPassActual"><i class="bi bi-eye"></i></button>
                                    </div>
                                    <asp:RequiredFieldValidator ID="rfvPassActual" runat="server" ControlToValidate="txtPassActual" ErrorMessage="La contraseña actual es obligatoria." CssClass="text-danger small" Display="Dynamic" ValidationGroup="CambiarPassword" />
                                </div>
                                <div class="mb-3">
                                    <asp:Label ID="lblPassNueva" runat="server" Text="Contraseña Nueva" CssClass="form-label" AssociatedControlID="txtPassNueva" />
                                    <div class="input-group">
                                        <asp:TextBox ID="txtPassNueva" runat="server" CssClass="form-control" TextMode="Password" />
                                        <button class="btn btn-outline-secondary" type="button" id="btnShowPassNueva"><i class="bi bi-eye"></i></button>
                                    </div>
                                    <asp:RequiredFieldValidator ID="rfvPassNueva" runat="server" ControlToValidate="txtPassNueva" ErrorMessage="La contraseña nueva es obligatoria." CssClass="text-danger small" Display="Dynamic" ValidationGroup="CambiarPassword" />
                                    <asp:RegularExpressionValidator ID="revPassNueva" runat="server" ErrorMessage="Mínimo 8 caracteres, 1 mayúscula, 1 minúscula y 1 número." ControlToValidate="txtPassNueva" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$" Display="Dynamic" CssClass="text-danger small" ValidationGroup="CambiarPassword" />
                                </div>
                                <div class="mb-3">
                                    <asp:Label ID="lblPassRepetir" runat="server" Text="Repetir Contraseña Nueva" CssClass="form-label" AssociatedControlID="txtPassRepetir" />
                                    <div class="input-group">
                                        <asp:TextBox ID="txtPassRepetir" runat="server" CssClass="form-control" TextMode="Password" />
                                        <button class="btn btn-outline-secondary" type="button" id="btnShowPassRepetir"><i class="bi bi-eye"></i></button>
                                    </div>
                                    <asp:RequiredFieldValidator ID="rfvPassRepetir" runat="server" ControlToValidate="txtPassRepetir" ErrorMessage="Debes repetir la contraseña." CssClass="text-danger small" Display="Dynamic" ValidationGroup="CambiarPassword" />
                                    <asp:CompareValidator ID="cvPassRepetir" runat="server" ControlToValidate="txtPassRepetir" ControlToCompare="txtPassNueva" ErrorMessage="Las contraseñas nuevas no coinciden." CssClass="text-danger small" Display="Dynamic" Operator="Equal" ValidationGroup="CambiarPassword" />
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
                </ContentTemplate>
               <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnMostrarPanelPassword" EventName="Click" />
            </Triggers>
               
            </asp:UpdatePanel>


        </div>

    </div>
    <div class="modal fade" id="modalAvatar" tabindex="-1" aria-labelledby="modalAvatarLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="modalAvatarLabel">Actualizar Foto de Perfil</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <p>Elegí una nueva foto para tu perfil. Se guardará al hacer clic en "Aceptar".</p>
                
                <div class="text-center mb-3">
                    <img id="imgPrecargaAvatar" src="#" alt="Vista previa" class="rounded-circle" style="width: 150px; height: 150px; display: none; margin: auto;" />
                </div>
                
                <asp:FileUpload ID="fileUploadModal" runat="server" CssClass="form-control" />
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                <asp:Button ID="btnConfirmarAvatar" runat="server" Text="Aceptar y Guardar Foto" 
                    CssClass="btn btn-primary" OnClick="btnConfirmarAvatar_Click" />
            </div>
        </div>
    </div>
</div>
    <script src="<%= ResolveUrl("~/Assets/js/togglePassword.js") %>"></script>
  <script type="text/javascript">

      // --- 1. Tu código para los "ojitos" (que ya estaba perfecto) ---
      function inicializarOjos() {
          togglePassword('#btnShowPassActual', '#<%= txtPassActual.ClientID %>');
        togglePassword('#btnShowPassNueva', '#<%= txtPassNueva.ClientID %>');
        togglePassword('#btnShowPassRepetir', '#<%= txtPassRepetir.ClientID %>');
        togglePassword('#btnShowPassConfirmarEmail', '#<%= txtPassConfirmarEmail.ClientID %>');
    }

    // --- 2. ¡¡NUEVO!! Código para la "Precarga" de la Foto en el MODAL ---
    function inicializarPrecargaAvatar() {
        // ¡¡OJO!! Busca el ID del FileUpload del MODAL
        var fileUploader = document.getElementById('<%= fileUploadModal.ClientID %>');
          var imgPreview = document.getElementById('imgPrecargaAvatar');

          if (!fileUploader || !imgPreview) return;

          fileUploader.addEventListener('change', function () {
              if (this.files && this.files[0]) {
                  var reader = new FileReader();

                  reader.onload = function (e) {
                      imgPreview.src = e.target.result;
                      imgPreview.style.display = 'block'; // La mostramos
                  };

                  reader.readAsDataURL(this.files[0]);
              }
          });
      }

      // --- 3. Ejecutamos todo ---
      document.addEventListener('DOMContentLoaded', function () {
          inicializarOjos();
          inicializarPrecargaAvatar(); // Llamamos a la nueva función
      });

      // Revivimos los "ojos" después de CUALQUIER carga AJAX
      if (typeof Sys !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
          Sys.WebForms.PageRequestManager.getInstance().add_endRequest(inicializarOjos);
      }
</script>



</asp:Content>

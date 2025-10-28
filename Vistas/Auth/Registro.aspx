<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Vistas.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center align-items-center" style="min-height: 60vh;">
        <div class="col-md-7 col-lg-6 col-xl-5"> 

            <div class="card shadow-sm border-0 rounded-lg">
                <div class="card-body p-4 p-sm-5">

                    <div class="text-center mb-4">
                         <i class="bi bi-person-plus-fill fs-1 text-primary"></i>
                        <h3 class="card-title fw-bold mt-2">Crear una cuenta</h3>
                        <p class="text-muted">Únete a nuestra comunidad y empieza a aprender.</p>
                    </div>

                    <div class="mb-3">
                        <label for="txtNombreCompleto" class="form-label">Nombre Completo</label>
                        <asp:TextBox runat="server" ID="txtNombreCompleto" CssClass="form-control form-control-lg" placeholder="Introduce tu nombre completo" />
                    </div>

                    <div class="mb-3">
                        <label for="txtEmailRegistro" class="form-label">Email</label>
                        <asp:TextBox runat="server" ID="txtEmailRegistro" type="email" CssClass="form-control form-control-lg" placeholder="tu@email.com" />
                    </div>

                    <div class="mb-3">
                        <label for="txtPasswordRegistro" class="form-label">Contraseña</label>
                        <div class="input-group input-group-lg">
                            <asp:TextBox runat="server" ID="txtPasswordRegistro" type="password" CssClass="form-control" placeholder="Crea una contraseña segura" />
                            <button class="btn btn-outline-secondary" type="button" id="togglePasswordRegistro">
                                <i class="bi bi-eye"></i>
                            </button>
                        </div>
                    </div>

                    <div class="mb-4"> 
                        <label for="txtConfirmPassword" class="form-label">Confirmar Contraseña</label>
                        <div class="input-group input-group-lg">
                            <asp:TextBox runat="server" ID="txtConfirmPassword" type="password" CssClass="form-control" placeholder="Vuelve a escribir la contraseña" />
                            <button class="btn btn-outline-secondary" type="button" id="toggleConfirmPassword">
                                 <i class="bi bi-eye-slash"></i> 
                            </button>
                        </div>
                    </div>

                    <div class="d-grid">
                        <%-- El botón que simula el registro y redirige a Loguin --%>
                        <asp:HyperLink NavigateUrl="~/Loguin.aspx" Text="Registrarse" CssClass="btn btn-primary btn-lg" runat="server" />
                    </div>

                    <div class="text-center mt-4">
                        <span class="text-muted small">¿Ya tienes una cuenta?</span>
                        <asp:HyperLink NavigateUrl="~/Loguin.aspx" Text="Inicia sesión" CssClass="fw-semibold text-decoration-none ms-1" runat="server" />
                    </div>

                </div>
            </div>

        </div>
    </div>
</asp:Content>

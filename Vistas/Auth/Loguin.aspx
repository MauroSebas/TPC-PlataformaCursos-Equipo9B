<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Loguin.aspx.cs" Inherits="Vistas.Loguin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center align-items-center" style="min-height: 60vh;"> <
    <div class="col-md-6 col-lg-5 col-xl-4"> 

        <div class="card shadow-sm border-0 rounded-lg">
            <div class="card-body p-4 p-sm-5"> 

                <div class="text-center mb-4">
                   
                     <i class="bi bi-person-circle fs-1 text-primary"></i>
                    <h3 class="card-title fw-bold mt-2">Bienvenido de nuevo</h3>
                </div>

                <div class="mb-3">
                    <label for="txtEmail" class="form-label">Email o Nombre de Usuario</label>
                    <asp:TextBox runat="server" ID="txtEmail" type="email" CssClass="form-control form-control-lg" placeholder="Ingresa tu email o nombre de usuario" Text="julieta.prandi@lms.com" />
                </div>

                <div class="mb-3">
                    <label for="txtPassword" class="form-label">Contraseña</label>
                    
                    <div class="input-group input-group-lg">
                        <asp:TextBox runat="server" ID="txtPassword" type="password" CssClass="form-control" placeholder="Ingresa tu contraseña" Text="********" />
                        <button class="btn btn-outline-secondary" type="button" id="togglePassword">
                            <i class="bi bi-eye"></i>
                        </button>
                    </div>
                </div>

                <div class="d-flex justify-content-end mb-4">
                    <asp:HyperLink NavigateUrl="#" Text="¿Olvidaste tu contraseña?" CssClass="small text-decoration-none" runat="server" />
                </div>

                <div class="d-grid">
                    
                    <asp:HyperLink NavigateUrl="~/MisCursos.aspx" Text="Iniciar Sesión" CssClass="btn btn-primary btn-lg" runat="server" />
                </div>

                <div class="text-center mt-4">
                    <span class="text-muted small">¿No tienes una cuenta?</span>
                    <asp:HyperLink NavigateUrl="~/Registro.aspx" Text="Regístrate" CssClass="fw-semibold text-decoration-none ms-1" runat="server" />
                </div>

            </div>
        </div>

    </div>
</div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ActivarCuenta.aspx.cs" Inherits="Vistas.Auth.ActivarCuenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center my-5 py-5">
        <div class="col-md-8 text-center">
            
            <div class="card shadow-sm border-0">
                <div class="card-body p-5">
                    
                    <!-- 
                        Usamos un Panel de ASP.NET.
                        El CodeBehind lo va a hacer visible y le va a poner 
                        clase 'alert-success' (si salió bien) o 'alert-danger' (si falló).
                    -->
                    <asp:Panel ID="pnlMensaje" runat="server" CssClass="alert" Visible="false" role="alert">
                        <h4 class="alert-heading">
                            <asp:Literal ID="litTitulo" runat="server" />
                        </h4>
                        <p>
                            <asp:Literal ID="litMensaje" runat="server" />
                        </p>
                        <hr>
                        <asp:HyperLink ID="hlLogin" NavigateUrl="~/Auth/Loguin.aspx" CssClass="btn btn-primary" Text="Ir a Iniciar Sesión" runat="server" Visible="false" />
                    </asp:Panel>

                </div>
            </div>

        </div>
    </div>
</asp:Content>

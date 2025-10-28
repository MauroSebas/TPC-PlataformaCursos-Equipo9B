<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="Vistas.Contacto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="text-center my-4 pt-4 mb-5">
        <h1 class="display-4 fw-bold">Ponte en contacto con nosotros</h1>
        <p class="lead text-muted mx-auto" style="max-width: 600px;">
            Estamos aquí para ayudarte. Rellena el formulario y te responderemos lo antes posible.
        </p>
    </div>

    <%-- Layout Principal (Formulario + Info Contacto) --%>
    <div class="row g-4 g-lg-5 justify-content-center">

        <%-- Columna Izquierda: Formulario --%>
        <div class="col-lg-7">
            <div class="card shadow-sm border-0 rounded-lg">
                <div class="card-body p-4 p-sm-5">
                   
                    <div class="row g-3 mb-3">
                        <div class="col-md-6">
                            <label for="txtNombreContacto" class="form-label">Nombre Completo</label>
                            <asp:TextBox runat="server" ID="txtNombreContacto" CssClass="form-control form-control-lg" placeholder="Introduce tu nombre completo" />
                        </div>
                        <div class="col-md-6">
                            <label for="txtEmailContacto" class="form-label">Correo Electrónico</label>
                            <asp:TextBox runat="server" ID="txtEmailContacto" type="email" CssClass="form-control form-control-lg" placeholder="ejemplo@dominio.com" />
                        </div>
                    </div>

                    <div class="mb-3">
                        <label for="txtAsunto" class="form-label">Asunto</label>
                        <asp:TextBox runat="server" ID="txtAsunto" CssClass="form-control form-control-lg" placeholder="¿Sobre qué quieres hablar?" />
                    </div>

                    <div class="mb-4">
                        <label for="txtMensaje" class="form-label">Tu Mensaje</label>
                        <asp:TextBox runat="server" ID="txtMensaje" TextMode="MultiLine" Rows="5" CssClass="form-control form-control-lg" placeholder="Escribe aquí tu consulta..." />
                    </div>

                    <div class="d-grid">
                        <asp:Button runat="server" ID="btnEnviarMensaje" Text="Enviar Mensaje" CssClass="btn btn-primary btn-lg"  />
                    </div>
                </div>
            </div>
        </div>

        <%-- Columna Derecha: Información de Contacto --%>
        <div class="col-lg-5">
             <div class="card shadow-sm border-0 rounded-lg h-100">
                <div class="card-body p-4 p-sm-5">
                    <h4 class="fw-bold mb-3">Otras formas de contactar</h4>
                    <p class="text-muted mb-4">
                        Si lo prefieres, puedes usar estos canales para comunicarte con nosotros directamente.
                    </p>

                    <ul class="list-unstyled mb-0">
                        <%-- Email --%>
                        <li class="d-flex align-items-start mb-4">
                            <div class="contact-icon-box me-3">
                                <i class="bi bi-envelope-fill fs-5"></i>
                            </div>
                            <div>
                                <h6 class="fw-semibold mb-0">Email</h6>
                                <asp:HyperLink NavigateUrl='mailto:<%= litEmailContacto.Text %>' Text='<%= litEmailContacto.Text %>' ToolTip="Enviar Email" CssClass="text-decoration-none text-muted" runat="server" />
                              
                                <asp:Literal runat="server" ID="litEmailContacto" Text="soporte@learnify.com" Visible="false" />
                            </div>
                        </li>

                        <%-- Teléfono --%>
                        <li class="d-flex align-items-start mb-4">
                            <div class="contact-icon-box me-3">
                                 <i class="bi bi-telephone-fill fs-5"></i>
                            </div>
                            <div>
                                <h6 class="fw-semibold mb-0">Teléfono</h6>
                                 <asp:HyperLink NavigateUrl='tel:<%= litTelefonoContacto.Text %>' Text='<%= litTelefonoContacto.Text %>' ToolTip="Llamar" CssClass="text-decoration-none text-muted" runat="server" />
                                <asp:Literal runat="server" ID="litTelefonoContacto" Text="+34 900 123 456" Visible="false" />
                            </div>
                        </li>

                        <%-- Dirección --%>
                         <li class="d-flex align-items-start">
                            <div class="contact-icon-box me-3">
                                <i class="bi bi-geo-alt-fill fs-5"></i>
                            </div>
                            <div>
                                <h6 class="fw-semibold mb-0">Oficina</h6>
                                <p class="text-muted mb-0">
                                    <asp:Literal runat="server" ID="litDireccion" Text="Calle Ficticia 123, 28080 Madrid, España" />
                                </p>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

    </div> 
</asp:Content>

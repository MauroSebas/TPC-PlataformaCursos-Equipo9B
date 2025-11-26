<%@ Page Title="" Language="C#" MasterPageFile="~/Alumno/Alumno.Master" AutoEventWireup="true" CodeBehind="Certificados.aspx.cs" Inherits="Vistas.Alumno.Certificados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Estilos para las tarjetas */
        .cert-card { transition: transform 0.2s ease; }
        .cert-card:hover { transform: translateY(-5px); }
        .cert-overlay { background: linear-gradient(to bottom, transparent 0%, rgba(0,0,0,0.8) 100%); }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container py-4">

      
      

       
        <div class="row row-cols-1 row-cols-md-2 row-cols-xl-3 g-4">
            
            <asp:Repeater ID="repCertificados" runat="server">
                <ItemTemplate>
                    <div class="col">
                        <div class="card h-100 border-0 shadow-sm rounded-4 overflow-hidden cert-card">
                            
                            <!-- Imagen Portada -->
                            <div class="position-relative" style="height: 180px;">
                                <img src='<%# ResolveUrl(Eval("UrlImagenCurso").ToString()) %>' 
                                     class="w-100 h-100 object-fit-cover" alt="Curso" 
                                     onerror="this.src='../../Assets/Images/placeholder.jpg';" />
                                
                                <!-- Sombra y Medalla -->
                                <div class="position-absolute top-0 start-0 w-100 h-100 cert-overlay"></div>
                                <div class="position-absolute top-50 start-50 translate-middle text-center w-100 px-3">
                                    <div class="bg-warning text-white rounded-circle d-inline-flex align-items-center justify-content-center shadow mb-2" 
                                         style="width: 56px; height: 56px;">
                                        <i class="bi bi-trophy-fill fs-3"></i>
                                    </div>
                                    <h6 class="text-white fw-bold text-uppercase small mb-0" style="letter-spacing: 1px;">Certificado Oficial</h6>
                                </div>
                            </div>

                            <!-- Datos y Botón -->
                            <div class="card-body text-center p-4 d-flex flex-column">
                                <h5 class="fw-bold text-body-emphasis mb-1"><%# Eval("NombreCurso") %></h5>
                                <p class="text-muted small mb-4">
                                    Emitido el <%# Eval("FechaEmision", "{0:dd/MM/yyyy}") %>
                                </p>

                                <div class="mt-auto">
                                    <a href='<%# ResolveUrl(Eval("UrlArchivo").ToString()) %>' target="_blank" 
                                       class="btn btn-primary w-100 rounded-pill fw-bold py-2 shadow-sm">
                                        <i class="bi bi-file-earmark-pdf-fill me-2"></i>Descargar PDF
                                    </a>
                                </div>
                            </div>

                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div> <!-- Fin Row -->

        <!-- 3. PANEL VACÍO (Afuera del Row para que ocupe todo el ancho) -->
        <asp:Panel ID="pnlVacio" runat="server" Visible="false" CssClass="text-center py-5">
            <div class="py-5">
                <div class="mb-3 bg-body-tertiary d-inline-flex p-4 rounded-circle">
                    <i class="bi bi-award fs-1 text-secondary opacity-50"></i>
                </div>
                <h4 class="fw-bold text-body-secondary mt-3">Aún no tienes certificados</h4>
                <p class="text-muted">Completa tus cursos y aprueba los exámenes finales para verlos aquí.</p>
                <a href="MisCursos.aspx" class="btn btn-outline-primary mt-3 px-4 rounded-pill">Ir a Mis Cursos</a>
            </div>
        </asp:Panel>

    </div> <!-- Fin Container -->
</asp:Content>

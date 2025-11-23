<%@ Page Title="" Language="C#" MasterPageFile="~/Alumno/Alumno.Master" AutoEventWireup="true" CodeBehind="MisCursos.aspx.cs" Inherits="Vistas.MisCursos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
   
    <h2 class="mb-4 fw-bold">Mis Cursos</h2>

    <%-- Grilla de Cursos (Dinámica) --%>
    <div class="row row-cols-1 row-cols-sm-2 row-cols-lg-3 g-4 mb-5">
        
        <asp:Repeater ID="repMisCursos" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card h-100 shadow-sm border-0">
                        <img src='<%# ObtenerImagen(Eval("UrlImagenPortada")) %>' class="card-img-top" alt="Portada" style="height: 200px; object-fit: cover;">
                        
                        <div class="card-body p-4 d-flex flex-column">
                            <p class="text-primary small fw-bold mb-1"><%# Eval("Categoria.Nombre") %></p>
                            <h5 class="card-title fw-bold mb-2"><%# Eval("Titulo") %></h5>
                            
                            <div class="mt-auto pt-3">
                                <div class="d-flex justify-content-between small mb-1">
                                    <span class="text-muted">Progreso</span>
                                    <span class="fw-medium">0%</span>
                                </div>
                                <div class="progress" role="progressbar" style="height: 8px;">
                                    <div class="progress-bar" style="width: 0%;"></div>
                                </div>
                                
                                <div class="d-grid mt-3">
                                    <asp:HyperLink NavigateUrl='<%# "~/Alumno/aula/Aula.aspx?id=" + Eval("Id") %>' 
                                        Text="Continuar Curso" CssClass="btn btn-primary" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
    
</asp:Content>

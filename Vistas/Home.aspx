<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Vistas.Home" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <link href="Assets/css/Home.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- HERO SECTION -->
    <div class="container my-5 py-4">
        <div class="row align-items-center g-5">
            <div class="col-lg-6 text-center text-lg-start">
                <h1 class="display-4 fw-bold lh-1 mb-3 text-body">Transforma tu futuro,<br />una habilidad a la vez.</h1>
                <p class="lead text-secondary mb-4">
                    Descubre cursos diseñados por expertos para impulsar tu carrera al siguiente nivel.
                </p>
                <div class="d-grid gap-2 d-md-flex justify-content-md-start">
                    <a href="#catalogo" class="btn btn-primary btn-lg px-5 rounded-pill fw-bold shadow-sm">Explorar Cursos</a>
                </div>
            </div>
            <div class="col-lg-6">
                <img src="https://lh3.googleusercontent.com/aida-public/AB6AXuCl1Cs_ED4qCV0rb-Ax5qA37yJG8GzelQ4f4WmALLuXFX2PHk1bTwETKRvifRP7Jlf8RNjVMyl1zwLhpAvdFFjOO2_2jZC_pTab_tnYRHL_Lpu47seYeuVOuQMSbRriTjN-1jWBvhmGhcVW16VPNKY8vTj1gu0mblebNT5WleKKEHaCwcLmLZ6JoO94jI-ZRlZy9UEEYr-aSwvywBKxLMWSxm67m1LmFCLSR8BWlts9icCI3fZoPJ8qNJ1o1LhxuLIkvLnYYpyDf1g" class="d-block mx-lg-auto img-fluid rounded-4 shadow-lg" alt="Learning" loading="lazy">
            </div>
        </div>
    </div>

    <!-- SECCIÓN CATÁLOGO -->
    <div class="bg-body-tertiary py-5" id="catalogo">
        <div class="container">

            <div class="text-center mb-5">
                <h2 class="fw-bold text-body mb-2">Nuestros Cursos</h2>
                <p class="text-secondary">Filtra por categoría y encontrá lo que buscás.</p>
            </div>

            <asp:UpdatePanel ID="updCatalogo" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    
                    <!-- CARRUSEL DE CATEGORÍAS -->
                    <div class="row justify-content-center mb-5">
                        <div class="col-12 col-lg-11">
                            
                            <div class="scroll-wrapper">
                                <button type="button" class="scroll-btn me-2 d-none d-md-flex" onclick="scrollCat(-200)">
                                    <i class="bi bi-chevron-left"></i>
                                </button>

                                <div class="category-scroll-container" id="catContainer">
                                    <asp:LinkButton ID="btnTodos" runat="server" OnClick="btnFiltroCategoria_Click" CommandArgument="0"
                                        CssClass='<%# "cat-pill text-decoration-none " + (CategoriaSeleccionadaId == 0 ? "active" : "") %>'>
                                        <i class="bi bi-grid-fill me-2"></i>Todos
                                    </asp:LinkButton>

                                    <asp:Repeater ID="repCategorias" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnCat" runat="server" OnClick="btnFiltroCategoria_Click" 
                                                CommandArgument='<%# Eval("Id") %>'
                                                CssClass='<%# "cat-pill text-decoration-none " + (Convert.ToInt32(Eval("Id")) == CategoriaSeleccionadaId ? "active" : "") %>'>
                                                <%# Eval("Nombre") %>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                                <button type="button" class="scroll-btn ms-2 d-none d-md-flex" onclick="scrollCat(200)">
                                    <i class="bi bi-chevron-right"></i>
                                </button>
                            </div>

                        </div>
                    </div>

                    <!-- GRILLA DE CURSOS -->
                    <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
                        
                        <asp:Repeater ID="repCursos" runat="server">
                            <ItemTemplate>
                                <div class="col">
                                    <asp:HyperLink NavigateUrl='<%# "~/CursoDetalle.aspx?id=" + Eval("Id") %>' CssClass="text-decoration-none" runat="server">
                                        
                                        <div class="card course-card h-100">
                                            
                                            <!-- Imagen + Badge -->
                                            <div class="card-img-wrapper">
                                                <img src='<%# ObtenerImagen(Eval("UrlImagenPortada")) %>' class="card-img-top" alt="Portada">
                                                <span class="cat-badge">
                                                    <%# Eval("Categoria.Nombre") %>
                                                </span>
                                            </div>

                                            <div class="card-body d-flex flex-column">
                                                
                                                <!-- Título -->
                                                <h5 class="card-title fw-bold text-body mb-3 lh-sm text-truncate">
                                                    <%# Eval("Titulo") %>
                                                </h5>
                                                
                                                <!-- Info Secundaria -->
                                                <div class="mt-auto d-flex flex-column gap-2">
                                                    
                                                    <div class="d-flex justify-content-between align-items-center">
                                                        <!-- Modalidad -->
                                                        <span class='<%# "badge rounded-pill px-3 py-2 " + (Eval("ModalidadPago").ToString() == "Gratuito" ? "text-bg-success" : "text-bg-dark") %>'>
                                                            <%# Eval("ModalidadPago") %>
                                                        </span>
                                                        
                                                        <!-- Duración -->
                                                        <div class="text-secondary small fw-bold d-flex align-items-center">
                                                            <i class="bi bi-clock me-1"></i>
                                                            <%# Convert.ToInt32(Eval("DuracionAccesoDias")) == 0 ? "Ilimitado" : Eval("DuracionAccesoDias") + " días" %>
                                                        </div>
                                                    </div>
                                                    
                                                    <hr class="my-3 opacity-10">

                                                    <!-- Precio -->
                                                    <div class="d-flex justify-content-between align-items-end">
                                                        <span class="small text-secondary text-uppercase fw-bold ls-1 mb-1">Precio</span>
                                                        <span class="fs-3 fw-black text-primary lh-1">
                                                            <%# Convert.ToDecimal(Eval("Precio")) == 0 ? "GRATIS" : String.Format("{0:C}", Eval("Precio")) %>
                                                        </span>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:HyperLink>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                    </div>

                    <!-- Mensaje Vacío -->
                    <asp:Panel ID="pnlSinCursos" runat="server" Visible="false" CssClass="text-center py-5">
                        <div class="py-5">
                            <i class="bi bi-search fs-1 text-secondary opacity-50"></i>
                            <h4 class="mt-3 text-body fw-bold">No encontramos cursos.</h4>
                            <p class="text-secondary">Intenta seleccionar otra categoría.</p>
                            <asp:LinkButton ID="btnVerTodosEmpty" runat="server" OnClick="btnFiltroCategoria_Click" CommandArgument="0" CssClass="btn btn-outline-primary rounded-pill mt-2 px-4">
                                Ver todos los cursos
                            </asp:LinkButton>
                        </div>
                    </asp:Panel>

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>

    <script>
        function scrollCat(scrollOffset) {
            var container = document.getElementById("catContainer");
            container.scrollLeft += scrollOffset;
        }
    </script>

</asp:Content>
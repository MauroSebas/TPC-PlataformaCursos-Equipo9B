<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CursoDetalle.aspx.cs" Inherits="Vistas.CursoDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .course-header-image {
            background-image: url('https://lh3.googleusercontent.com/aida-public/AB6AXuCuRQWZfRPVEnP6kdCoXxcdLIErxFUFJoJ0RyzKFbpHE0dZ7S8oLeErDwFN-V_vv8-zIJqCg_JBNX3FwKIb96kaadWEnD2lnWHhbLEpl1-c4aJciFP3EOCSn0HWVCw7MZbtji0_7WG3rIwZpNvWhyU5FDP4xOJJ5PEnPf1MhB45-NkCpmWtbgme2q8jIT3uWXiMWo7XZc_EUH5YyX6ckZ4hMEGOVpNvQETsP6YtSvs03dHmUxxkrpMlj_j-n-8bgKFvS-7MKk931MY'); /* Imagen del prototipo */
            background-size: cover;
            background-position: center;
            height: 350px; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- 1. Imagen de Encabezado --%>
    <div class="container px-0 mb-4">
        <div class="course-header-image rounded-3 shadow-sm">
           
        </div>
    </div>

    <%-- 2. Contenido Principal (Título, Descripción, Sidebar) --%>
    <div class="container">
        <div class="row g-5">

            <%-- Columna Izquierda: Detalles del Curso --%>
            <div class="col-lg-8">

                <%-- Breadcrumbs (Migas de Pan) --%>
                <nav aria-label="breadcrumb" class="mb-3">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><asp:HyperLink NavigateUrl="~/Home.aspx" Text="Inicio" runat="server" /></li>
                        <li class="breadcrumb-item"><asp:HyperLink NavigateUrl="~/Home.aspx#cursos" Text="Desarrollo" runat="server" /></li>
                        <li class="breadcrumb-item active" aria-current="page">Desarrollo Web</li>
                    </ol>
                </nav>

                <%-- Título y Descripción --%>
                <h1 class="display-5 fw-bold mb-3">Curso Completo de Desarrollo Web: De Cero a Experto</h1>
                <p class="lead text-muted mb-5">
                    Este curso está diseñado para llevarte desde los fundamentos de la web hasta convertirte en un desarrollador full-stack. Aprenderás a construir aplicaciones web interactivas y dinámicas utilizando las tecnologías más demandadas del mercado como HTML5, CSS3, JavaScript, React y Node.js. No se requiere experiencia previa.
                </p>

                <%-- Sección "Lo que aprenderás" --%>
                <div class="card border-0 shadow-sm mb-4 rounded-lg">
                    <div class="card-body p-4">
                        <h4 class="card-title fw-bold mb-3">Lo que aprenderás</h4>
                        <ul class="list-unstyled mb-0">
                            <li class="d-flex align-items-start mb-2">
                                <i class="bi bi-check-circle-fill text-primary me-2 mt-1"></i>
                                <span>Construir sitios web y aplicaciones web robustas desde cero.</span>
                            </li>
                            <li class="d-flex align-items-start mb-2">
                                <i class="bi bi-check-circle-fill text-primary me-2 mt-1"></i>
                                <span>Dominar HTML5, CSS3, y JavaScript moderno (ES6+).</span>
                            </li>
                            <li class="d-flex align-items-start mb-2">
                                <i class="bi bi-check-circle-fill text-primary me-2 mt-1"></i>
                                <span>Crear aplicaciones interactivas con React y manejar el estado de la aplicación.</span>
                            </li>
                             <li class="d-flex align-items-start mb-2">
                                <i class="bi bi-check-circle-fill text-primary me-2 mt-1"></i>
                                <span>Desarrollar servidores y APIs RESTful con Node.js y Express.</span>
                            </li>
                             <li class="d-flex align-items-start">
                                <i class="bi bi-check-circle-fill text-primary me-2 mt-1"></i>
                                <span>Interactuar con bases de datos como MongoDB y PostgreSQL.</span>
                            </li>
                        </ul>
                    </div>
                </div>

            </div> <%-- Fin Columna Izquierda --%>

            <%-- Columna Derecha: Sidebar de Compra --%>
            <div class="col-lg-4">
                <div class="sticky-top" style="top: 80px;">
                    <div class="card border-0 shadow-sm rounded-lg overflow-hidden">
                        <img src="https://lh3.googleusercontent.com/aida-public/AB6AXuAeIpC9Xeki8K6ezXauGeZiGg6y4w8QB_FCNA04WrpUgm0dnNq377-nix8YWpzX2AXZd9WUiYqqWHFXfCMzvOTl96K5jxkO2wteKaH3X5s6KClGrFAuNqzW09PutdEDQOVT5aAs-QnSDwKoJg6tck6ohPua0LlCkjPRcT0e2WEVqxb28h4q0WETb36o1dPCF_iqJnjNTCLg4RSwYUYbQjMxtIMziEIMSe7dG3N1kLr1yPriV6-hM9AW8SLwqSORkj7GHmC7CYpHD4A" class="card-img-top" alt="Thumbnail curso">
                        <div class="card-body p-4">
                            <h2 class="card-title display-6 fw-bold mb-3">$99.99</h2>
                            <div class="d-grid gap-2">
                                <asp:Button runat="server" ID="btnAgregarCarrito" Text="Añadir al Carrito" CssClass="btn btn-primary btn-lg" />
                                <%-- Simulamos que "Comprar" te lleva a Mis Cursos (como si ya estuvieras logueado) --%>
                                <asp:HyperLink NavigateUrl="~/MisCursos.aspx" Text="Comprar Curso" CssClass="btn btn-outline-primary btn-lg" runat="server" />
                            </div>

                            <hr class="my-4">

                            <h5 class="fw-semibold mb-3">Este curso incluye:</h5>
                            <ul class="list-unstyled text-muted small">
                                <li class="mb-2 d-flex align-items-center"><i class="bi bi-calendar-check me-2 fs-5"></i> Acceso por 365 días</li>
                                <li class="mb-2 d-flex align-items-center"><i class="bi bi-mortarboard me-2 fs-5"></i> Apto para principiantes</li>
                                <li class="mb-2 d-flex align-items-center"><i class="bi bi-translate me-2 fs-5"></i> Idioma: Español</li>
                                <li class="d-flex align-items-center"><i class="bi bi-patch-check me-2 fs-5"></i> Certificado de finalización</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div> 

        </div> 
    </div> 
</asp:Content>

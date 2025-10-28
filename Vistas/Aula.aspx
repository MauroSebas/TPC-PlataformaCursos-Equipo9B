<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Aula.aspx.cs" Inherits="Vistas.Aula" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Hacemos que el main ocupe toda la altura menos el header */
        .aula-main-container {
            display: flex;
            flex-grow: 1; /* Ocupa el espacio restante */
            overflow: hidden; /* Evita doble scrollbar */
        }

        .aula-sidebar {
            width: 320px; /* Ancho fijo para el sidebar */
            flex-shrink: 0; /* Evita que se encoja */
            overflow-y: auto; /* Scroll si el contenido es largo */
            border-right: 1px solid var(--bs-border-color);
        }

        .aula-content {
            flex-grow: 1; /* Ocupa el espacio restante */
            overflow-y: auto; /* Scroll para el contenido principal */
            padding: 2rem; /* Padding interno */
        }

        /* Estilos para los items del sidebar */
        .sidebar-item {
            display: flex;
            align-items-center;
            padding: 0.75rem 1rem;
            border-radius: var(--bs-border-radius);
            text-decoration: none;
            color: var(--bs-body-color);
            transition: background-color 0.2s ease;
        }

        .sidebar-item:hover {
            background-color: var(--bs-tertiary-bg);
        }

        .sidebar-item.active {
            background-color: var(--bs-primary-bg-subtle);
            color: var(--bs-primary);
            font-weight: 600;
            border: 1px solid var(--bs-primary);
        }

        .sidebar-item .icon {
            font-size: 1.5rem; /* Tamaño de los íconos */
            width: 40px; /* Espacio fijo para el ícono */
            text-align: center;
            margin-right: 0.5rem;
        }
         .sidebar-item .text {
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }

         /* Estilo para los títulos de módulo */
        .module-summary {
            padding: 0.5rem 1rem;
            font-weight: 600;
            cursor: pointer;
            border-radius: var(--bs-border-radius);
        }
         .module-summary:hover {
             background-color: var(--bs-tertiary-bg);
         }
         .module-summary .bi-chevron-down {
             transition: transform 0.3s ease;
         }
         .module-summary[aria-expanded="true"] .bi-chevron-down {
             transform: rotate(180deg);
         }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- Quitamos el container de la Master Page para usar el ancho completo --%>
    <%-- NOTA: Esto requiere un ajuste en Site1.Master si quieres que funcione bien --%>
    <%-- Por ahora, lo dejamos dentro del container normal --%>

    <div class="d-flex flex-grow-1 overflow-hidden" style="height: calc(100vh - 56px - 73px);"> <%-- Altura total menos header y footer (aprox) --%>

        <%-- Sidebar (Aside) --%>
        <aside class="aula-sidebar bg-body-tertiary p-3">
            <h5 class="fw-bold mb-2 px-2">Curso de Diseño UX/UI Avanzado</h5>
            <p class="small text-muted mb-3 px-2">Progreso: 25%</p>

            <div class="accordion accordion-flush" id="courseModules">

                <%-- Módulo 1 --%>
                <div class="accordion-item bg-transparent border-0">
                    <h2 class="accordion-header" id="headingOne">
                        <button class="accordion-button module-summary shadow-none bg-transparent px-2 py-2" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                            Módulo 1: Fundamentos
                        </button>
                    </h2>
                    <div id="collapseOne" class="accordion-collapse collapse show" aria-labelledby="headingOne" data-bs-parent="#courseModules">
                        <div class="py-2">
                             <a href="#" class="sidebar-item">
                                <span class="icon text-success"><i class="bi bi-check-circle-fill"></i></span>
                                <span class="text small">1.1 - Bienvenida al Curso</span>
                            </a>
                             <a href="#" class="sidebar-item active"> <%-- Lección activa --%>
                                <span class="icon text-primary"><i class="bi bi-play-circle-fill"></i></span>
                                <span class="text small">1.2 - Introducción a la Interfaz</span>
                            </a>
                            <a href="#" class="sidebar-item">
                                <span class="icon text-muted"><i class="bi bi-circle"></i></span>
                                <span class="text small">1.3 - Principios de Diseño</span>
                            </a>
                        </div>
                    </div>
                </div>

                 <%-- Módulo 2 --%>
                <div class="accordion-item bg-transparent border-0">
                    <h2 class="accordion-header" id="headingTwo">
                        <button class="accordion-button module-summary collapsed shadow-none bg-transparent px-2 py-2" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                            Módulo 2: Investigación
                        </button>
                    </h2>
                    <div id="collapseTwo" class="accordion-collapse collapse" aria-labelledby="headingTwo" data-bs-parent="#courseModules">
                         <div class="py-2">
                            <a href="#" class="sidebar-item">
                                <span class="icon text-muted"><i class="bi bi-circle"></i></span>
                                <span class="text small">2.1 - User Personas</span>
                            </a>
                            <a href="#" class="sidebar-item">
                                <span class="icon text-muted"><i class="bi bi-circle"></i></span>
                                <span class="text small">2.2 - Journey Maps</span>
                            </a>
                        </div>
                    </div>
                </div>
                <%-- Puedes agregar más módulos aquí --%>
            </div>
        </aside>

        <%-- Contenido Principal (Section) --%>
        <section class="aula-content">
            <div class="container-fluid"> <%-- Usamos container-fluid para que ocupe el ancho disponible --%>

                <%-- Video Player --%>
                <div class="ratio ratio-16x9 mb-4 rounded-3 overflow-hidden shadow-sm">
                     <iframe src="https://www.youtube.com/embed/SW14tOda_kI" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                </div>

                <%-- Título y Descripción de la Lección --%>
                <h2 class="fw-bold mb-3">1.2 - Introducción a la Interfaz de Usuario</h2>
                <p class="text-muted mb-4">
                    Bienvenido a la segunda lección. En este video, exploraremos la interfaz principal de nuestra herramienta de diseño. Aprenderás sobre las diferentes secciones, paneles y cómo navegar eficientemente para optimizar tu flujo de trabajo. Asegúrate de seguir los pasos para familiarizarte con el entorno.
                </p>

                <%-- Botones de Navegación --%>
                <div class="d-flex flex-column flex-sm-row justify-content-between gap-2 border-top pt-4">
                    <asp:Button runat="server" ID="btnAnterior" Text="Lección Anterior" CssClass="btn btn-outline-secondary" />
                    <asp:Button runat="server" ID="btnCompletada" Text="Marcar como Completada" CssClass="btn btn-primary" />
                </div>

            </div>
        </section>

    </div> <%-- Fin d-flex --%>
</asp:Content>

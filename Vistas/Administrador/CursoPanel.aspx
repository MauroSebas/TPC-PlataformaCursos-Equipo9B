<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CursoPanel.aspx.cs" Inherits="Vistas.Aministrador.CursoPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        body {
            /* Fuente personalizada del diseño original */
            font-family: 'Inter', sans-serif;
            /* bg-body-tertiary es el gris claro de Bootstrap */
            background-color: var(--bs-body-tertiary);
        }

        .material-symbols-outlined {
            font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24;
            font-size: 24px;
            vertical-align: middle;
        }
        /* Estilo para el ícono de "Courses" con relleno */
        .material-symbols-fill {
            font-variation-settings: 'FILL' 1, 'wght' 400, 'GRAD' 0, 'opsz' 24;
        }
        /* Ancho fijo del sidebar (w-64 de Tailwind) */
        .sidebar {
            width: 16rem; /* 64 * 0.25rem = 16rem */
            min-height: 100vh;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main class="flex-grow-1 p-4 p-lg-5">

        <div class="container-xl">

            <header class="d-flex flex-column flex-sm-row flex-wrap justify-content-between align-items-start gap-4 mb-5">
                <div class="d-flex flex-column gap-1">
                    <h1 class="h2 fw-bolder text-body-emphasis mb-0">Gestión de Cursos</h1>
                    <p class="text-body-secondary fs-6">Visualiza, filtra y gestiona todos los cursos de forma eficiente.</p>
                </div>
                <asp:LinkButton runat="server" ID="btnAgregarCurso"
                    CssClass="btn btn-primary btn-lg d-flex align-items-center gap-2 shadow-sm fw-bold small"
                    OnClick="btnAgregarCurso_Click1">
    
                    <span class="material-symbols-outlined fs-6">add_circle</span>
                    <span>Agregar Nuevo Curso</span>
    
                </asp:LinkButton>
                <%--                <button class="btn btn-primary btn-lg d-flex align-items-center gap-2 shadow-sm fw-bold small" >
                    <a href="LeccionForm.aspx"></a>
                    <span class="material-symbols-outlined fs-6">add_circle</span>
                    <span>Agregar Nuevo Curso</span>
                </button>--%>
            </header>

            <div class="card p-4 mb-4 rounded-3">
                <div class="row g-4">
                    <div class="col-md-4">
                        <div class="input-group">
                            <span class="input-group-text bg-body-tertiary">
                                <span class="material-symbols-outlined fs-6">search</span>
                            </span>
                            <input type="text" class="form-control bg-body-tertiary" placeholder="Buscar por título de curso...">
                        </div>
                    </div>

                    <div class="col-md-8 d-flex align-items-center gap-3">
                        <button class="btn btn-light border d-flex align-items-center gap-2 small">
                            <span>Categoría</span>
                            <span class="material-symbols-outlined fs-6 text-body-secondary">expand_more</span>
                        </button>
                        <button class="btn btn-light border d-flex align-items-center gap-2 small">
                            <span>Estado</span>
                            <span class="material-symbols-outlined fs-6 text-body-secondary">expand_more</span>
                        </button>
                    </div>
                </div>
            </div>

            <div class="card rounded-3 border-0">
                <div class="table-responsive">
                    <table class="table table-hover table-borderless align-middle mb-0">
                        <thead class="table-light text-body-secondary text-uppercase small border-bottom">
                            <tr>
                                <th scope="col" class="px-4 py-3 fw-bold">Título del Curso</th>
                                <th scope="col" class="px-4 py-3 fw-bold">Categoría</th>
                                <th scope="col" class="px-4 py-3 fw-bold">Precio</th>
                                <th scope="col" class="px-4 py-3 fw-bold">Estado</th>
                                <th scope="col" class="px-4 py-3 fw-bold text-end">Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr class="border-bottom">
                                <td class="px-4 py-3 fw-medium text-body-emphasis text-nowrap">Introducción al Diseño UX/UI</td>
                                <td class="px-4 py-3">Diseño</td>
                                <td class="px-4 py-3">$49.99</td>
                                <td class="px-4 py-3">
                                    <span class="badge rounded-pill bg-success-subtle text-success-emphasis fw-medium">Publicado</span>
                                </td>
                                <td class="px-4 py-3 text-end">
                                    <button class="btn btn-link text-body-secondary p-1">
                                        <span class="material-symbols-outlined fs-6">edit</span>
                                    </button>
                                    <button class="btn btn-link text-danger p-1">
                                        <span class="material-symbols-outlined fs-6">delete</span>
                                    </button>
                                </td>
                            </tr>
                            <tr class="border-bottom">
                                <td class="px-4 py-3 fw-medium text-body-emphasis text-nowrap">Marketing Digital para Principiantes</td>
                                <td class="px-4 py-3">Marketing</td>
                                <td class="px-4 py-3">$99.00</td>
                                <td class="px-4 py-3">
                                    <span class="badge rounded-pill bg-success-subtle text-success-emphasis fw-medium">Publicado</span>
                                </td>
                                <td class="px-4 py-3 text-end">
                                    <button class="btn btn-link text-body-secondary p-1"><span class="material-symbols-outlined fs-6">edit</span></button>
                                    <button class="btn btn-link text-danger p-1"><span class="material-symbols-outlined fs-6">delete</span></button>
                                </td>
                            </tr>
                            <tr class="border-bottom">
                                <td class="px-4 py-3 fw-medium text-body-emphasis text-nowrap">Fundamentos de React.js</td>
                                <td class="px-4 py-3">Programación</td>
                                <td class="px-4 py-3">Gratis</td>
                                <td class="px-4 py-3">
                                    <span class="badge rounded-pill bg-secondary-subtle text-secondary-emphasis fw-medium">Borrador</span>
                                </td>
                                <td class="px-4 py-3 text-end">
                                    <button class="btn btn-link text-body-secondary p-1"><span class="material-symbols-outlined fs-6">edit</span></button>
                                    <button class="btn btn-link text-danger p-1"><span class="material-symbols-outlined fs-6">delete</span></button>
                                </td>
                            </tr>
                            <tr>
                                <td class="px-4 py-3 fw-medium text-body-emphasis text-nowrap">Python para Ciencia de Datos</td>
                                <td class="px-4 py-3">Programación</td>
                                <td class="px-4 py-3">$149.99</td>
                                <td class="px-4 py-3">
                                    <span class="badge rounded-pill bg-success-subtle text-success-emphasis fw-medium">Publicado</span>
                                </td>
                                <td class="px-4 py-3 text-end">
                                    <button class="btn btn-link text-body-secondary p-1"><span class="material-symbols-outlined fs-6">edit</span></button>
                                    <button class="btn btn-link text-danger p-1"><span class="material-symbols-outlined fs-6">delete</span></button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <nav aria-label="Navegación de la tabla" class="d-flex flex-column flex-md-row justify-content-between align-items-start align-items-md-center gap-3 p-4">
                    <span class="small text-body-secondary">Mostrando <span class="fw-semibold text-body-emphasis">1-4</span> de <span class="fw-semibold text-body-emphasis">100</span>
                    </span>
                    <ul class="pagination pagination-sm mb-0">
                        <li class="page-item"><a class="page-link" href="#">Anterior</a></li>
                        <li class="page-item active" aria-current="page"><a class="page-link" href="#">1</a></li>
                        <li class="page-item"><a class="page-link" href="#">2</a></li>
                        <li class="page-item"><a class="page-link" href="#">...</a></li>
                        <li class="page-item"><a class="page-link" href="#">Siguiente</a></li>
                    </ul>
                </nav>
            </div>
        </div>
    </main>

</asp:Content>

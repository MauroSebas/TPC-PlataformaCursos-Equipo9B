<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CursoForm.aspx.cs" Inherits="Vistas.Aministrador.CursoForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<style>
    body {
        /* Fuente personalizada del diseño original */
        font-family: 'Lexend', sans-serif;
        /* bg-body-tertiary es el gris claro de Bootstrap */
        background-color: var(--bs-body-tertiary);
    }

    .material-symbols-outlined {
        vertical-align: middle;
        font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24;
    }

    /* Tamaño fijo del avatar (size-10 de Tailwind -> 2.5rem/40px) */
    .avatar {
        width: 40px;
        height: 40px;
    }
    /* Estilos para el campo de subida de archivos */
    .file-drop-zone {
        border: 2px dashed var(--bs-border-color);
        transition: border-color 0.15s ease-in-out, background-color 0.15s ease-in-out;
    }

    .file-drop-zone:hover {
        border-color: var(--bs-primary);
        background-color: var(--bs-tertiary-bg);
    }
    /* Estilos para el switch 'Publicado' */
    .form-switch .form-check-input {
        width: 2.75em; /* w-11 */
        height: 1.5em; /* h-6 */
    }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <main class="flex-grow-1 d-flex flex-column">

    <header class="sticky-top d-flex align-items-center justify-content-between gap-2 border-bottom bg-body px-4 px-md-5" style="height: 4rem;">
        <div>
            <h1 class="h5 fw-bold mb-0">Crear Nuevo Curso</h1>
        </div>
        <div class="d-flex align-items-center gap-2">
        </div>
    </header>

    <div class="flex-grow-1 p-4 p-md-5">
        <div class="container-lg">

            <div class="d-none d-lg-flex flex-wrap justify-content-between gap-3 mb-5">
                <div class="d-flex flex-column gap-2">
                    <h1 class="display-5 fw-bolder">Crear Nuevo Curso</h1>
                    <p class="fs-6 text-body-secondary">Rellena los detalles a continuación para configurar tu nuevo curso.</p>
                </div>
            </div>

            <div class="d-flex flex-column gap-4">

                <div class="mb-2">
                    <label for="courseName" class="form-label fw-medium">Nombre del Curso</label>
                    <input type="text" class="form-control form-control-lg" id="courseName" placeholder="Ej: Introducción al Diseño Web">
                </div>

                <div class="mb-2">
                    <label class="form-label fw-medium">Imagen de Portada del Curso</label>
                    <div class="row g-4 align-items-start">
                        <div class="col-lg">
                            <label class="file-drop-zone d-flex flex-column align-items-center justify-content-center w-100 rounded-3 text-center" style="min-height: 12rem; cursor: pointer;">
                                <div class="p-4">
                                    <span class="material-symbols-outlined fs-1 text-body-secondary mb-3">cloud_upload</span>
                                    <p class="mb-2 small text-body-secondary"><span class="fw-semibold">Haz clic para subir</span> o arrastra y suelta</p>
                                    <p class="text-body-secondary" style="font-size: 0.75rem;">SVG, PNG, JPG o GIF (MÁX. 800x400px)</p>
                                </div>
                                <input type="file" class="d-none" />
                            </label>
                        </div>
                        <div class="col-lg-auto">
                            <div class="bg-cover bg-center rounded-3" style="background-image: url('https://lh3.googleusercontent.com/aida-public/AB6AXuBd51B3ypeacVFO_xpQdZDcGXl5dflqgWdEKh3Hj4q7zDPKfLshwnoytz2AC1KjWMml9faMSFw0MYDBYLuOhgTVyGopAMyLX4b7teXKCqfxAe8R1h25MJRqv5ZfPK0g-XQSqE0YriuA0MTGDoh-GOPIk80RRljzMCw2vqtHG1jWk4jRfpTOf_qfDKG5Fqy6omdimIOxN0BJipYeafGA6BsSju3N11REp736QDjEgMmcz605aD7Dk_qSjlonXFhagnfDlk_EewSZzTU'); width: 13rem; height: 8rem;"></div>
                        </div>
                    </div>
                </div>

                <div class="mb-2">
                    <label for="courseDescription" class="form-label fw-medium">Descripción Detallada</label>
                    <textarea class="form-control form-control-lg" id="courseDescription" rows="8" placeholder="Proporciona una descripción detallada del curso, incluyendo objetivos de aprendizaje, público objetivo y esquema de contenido..."></textarea>
                </div>

                <hr class="my-3" />

                <div class="row g-4">
                    <div class="col-md-6">
                        <label for="category" class="form-label fw-medium">Categoría</label>
                        <select class="form-select form-select-lg" id="category">
                            <option>Desarrollo Web</option>
                            <option>Diseño UI/UX</option>
                            <option>Ciencia de Datos</option>
                            <option>Marketing</option>
                        </select>
                    </div>

                    <div class="col-md-6 d-flex align-items-end">
                        <div class="form-check form-switch fs-5 p-4 border rounded-3 bg-body w-100 d-flex justify-content-between align-items-center">
                            <label class="form-check-label fw-medium" for="status">Publicado</label>
                            <input class="form-check-input" type="checkbox" role="switch" id="status">
                        </div>
                    </div>
                </div>

                <hr class="my-3" />

                <div class="d-flex flex-column gap-3">
                    <h3 class="h5 fw-bold">Modelo de Precios</h3>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" name="pricing" id="free">
                        <label class="form-check-label fs-6" for="free">Gratis</label>
                    </div>
                    <div>
                        <div class="form-check">
                            <input class="form-check-input" type="radio" name="pricing" id="onetime" checked>
                            <label class="form-check-label fs-6" for="onetime">Pago Único</label>
                        </div>
                        <div class="input-group mt-2" style="max-width: 200px;">
                            <span class="input-group-text">$</span>
                            <input type="number" class="form-control" placeholder="99.00">
                        </div>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" name="pricing" id="subscription">
                        <label class="form-check-label fs-6" for="subscription">
                        Suscripción</Glabel>
                   
                    </div>
                </div>

                <hr class="my-3" />

                <div class="d-flex flex-column gap-3">
                    <h3 class="h5 fw-bold">Control de Acceso</h3>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" name="access" id="permanent" checked>
                        <label class="form-check-label fs-6" for="permanent">Acceso Permanente</label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" name="access" id="limited">
                        <label class="form-check-label fs-6" for="limited">Tiempo Limitado</label>
                    </div>
                </div>

                <hr class="my-3" />

                <div class="d-flex justify-content-end align-items-center gap-3 pt-2">
                    <asp:Button ID="btnCancelar" Text="Cancelar" runat="server" CssClass="btn btn-danger" OnClick="btnCancelar_Click" />
                    <asp:Button ID="btnGuardarCurso" Text="Guardar y Continuar" runat ="server" CssClass="btn btn-primary" OnClick="btnGuardarCurso_Click" />
                </div>

            </div>
        </div>
    </div>
</main>
</asp:Content>

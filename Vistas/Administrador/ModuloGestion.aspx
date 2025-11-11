<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="ModuloGestion.aspx.cs" Inherits="Vistas.Aministrador.ModuloGestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main class="flex-grow-1 overflow-y-auto">
        <div class="p-4 p-md-5">
            
            <nav aria-label="breadcrumb" class="mb-4">
                <ol class="breadcrumb small fw-medium">
                    <li class="breadcrumb-item"><a class="text-body-secondary text-decoration-none" href="#">Cursos</a></li>
                    <li class="breadcrumb-item"><a class="text-body-secondary text-decoration-none" href="#">Marketing Digital</a></li>
                    <li class="breadcrumb-item active text-body-emphasis" aria-current="page">Estructura</li>
                </ol>
            </nav>
            
            <header class="d-flex flex-wrap align-items-center justify-content-between gap-4 mb-5">
                <div class="flex-column">
                    <h1 class="h2 fw-bold mb-0">Editando: Introducción al Marketing Digital</h1>
                    <p class="text-body-secondary fs-6 mt-1">Organiza los módulos y lecciones de tu curso. Arrastra y suelta para reordenar.</p>
                </div>
                <div class="d-flex flex-shrink-0 gap-3">
                    <asp:Button ID="btnAgregarLeccion" runat="server" Text="+Agregar Leccion" CssClass="btn btn-light" OnClick="btnAgregarLeccion_Click"/>    
                    <button class="btn btn-primary d-flex align-items-center gap-2 fw-bold small">
                        <span class="material-symbols-outlined fs-6">add</span>
                        <span> Agregar Módulo</span>
                    </button>
                    <asp:Button ID="btnGuardarySalir" Text="Guardar y Finalizar Edicion" runat="server" CssClass="btn btn-primary" OnClick="btnGuardarySalir_Click"/>
                    <asp:Button ID="btnSalir" Text="Salir" runat="server" CssClass="btn btn-danger" OnClick="btnSalir_Click"/>
                </div>
            </header>

            <div class="d-flex flex-column gap-3">
                
                <details class="border rounded-3 bg-body shadow-sm" open>
                    <summary class="module-summary list-group-item-action d-flex align-items-center justify-content-between gap-4 p-3 rounded-top-3">
                        <div class="d-flex align-items-center gap-3">
                            <span class="material-symbols-outlined text-body-secondary" style="cursor: grab;">drag_indicator</span>
                            <h2 class="h6 fw-semibold mb-0">Módulo 1: Introducción al Curso</h2>
                        </div>
                        <div class="d-flex align-items-center gap-3">
                            <div class="module-actions d-flex align-items-center gap-1">
                                <button class="btn btn-link text-body-secondary p-1" title="Editar"><span class="material-symbols-outlined fs-6">edit</span></button>
                                <button class="btn btn-link text-danger p-1" title="Eliminar"><span class="material-symbols-outlined fs-6">delete</span></button>
                            </div>
                            <span class="material-symbols-outlined arrow-icon">expand_more</span>
                        </div>
                    </summary>
                    <div class="border-top p-4">
                        <ul class="list-unstyled d-flex flex-column gap-2">
                            <li class="lesson-item list-group-item-action rounded-3 d-flex align-items-center justify-content-between p-3">
                                <div class="d-flex align-items-center gap-3">
                                    <span class="material-symbols-outlined text-body-secondary" style="cursor: grab;">drag_indicator</span>
                                    <span class="material-symbols-outlined text-primary fs-5">play_circle</span>
                                    <p class="mb-0 small">Lección 1.1: Video de Bienvenida</p>
                                </div>
                                <div class="lesson-actions d-flex align-items-center gap-1">
                                    <button class="btn btn-link text-body-secondary p-1" title="Editar"><span class="material-symbols-outlined fs-6">edit</span></button>
                                    <button class="btn btn-link text-danger p-1" title="Eliminar"><span class="material-symbols-outlined fs-6">delete</span></button>
                                </div>
                            </li>
                            <li class="lesson-item list-group-item-action rounded-3 d-flex align-items-center justify-content-between p-3">
                                <div class="d-flex align-items-center gap-3">
                                    <span class="material-symbols-outlined text-body-secondary" style="cursor: grab;">drag_indicator</span>
                                    <span class="material-symbols-outlined text-primary fs-5">description</span>
                                    <p class="mb-0 small">Lección 1.2: Terminología Clave</p>
                                </div>
                                <div class="lesson-actions d-flex align-items-center gap-1">
                                    <button class="btn btn-link text-body-secondary p-1" title="Editar"><span class="material-symbols-outlined fs-6">edit</span></button>
                                    <button class="btn btn-link text-danger p-1" title="Eliminar"><span class="material-symbols-outlined fs-6">delete</span></button>
                                </div>
                            </li>
                        </ul>
                    </div>
                </details>

                <details class="border rounded-3 bg-body shadow-sm">
                    <summary class="module-summary list-group-item-action d-flex align-items-center justify-content-between gap-4 p-3 rounded-3">
                        <div class="d-flex align-items-center gap-3">
                            <span class="material-symbols-outlined text-body-secondary" style="cursor: grab;">drag_indicator</span>
                            <h2 class="h6 fw-semibold mb-0">Módulo 2: Conceptos Centrales de Marketing Digital</h2>
                        </div>
                        <div class="d-flex align-items-center gap-3">
                            <div class="module-actions d-flex align-items-center gap-1">
                                <button class="btn btn-link text-body-secondary p-1" title="Editar"><span class="material-symbols-outlined fs-6">edit</span></button>
                                <button class="btn btn-link text-danger p-1" title="Eliminar"><span class="material-symbols-outlined fs-6">delete</span></button>
                            </div>
                            <span class="material-symbols-outlined arrow-icon">expand_more</span>
                        </div>
                    </summary>
                    <div class="border-top p-4">
                        <ul class="list-unstyled d-flex flex-column gap-2">
                             <li class="lesson-item list-group-item-action rounded-3 d-flex align-items-center justify-content-between p-3">
                                <div class="d-flex align-items-center gap-3">
                                    <span class="material-symbols-outlined text-body-secondary" style="cursor: grab;">drag_indicator</span>
                                    <span class="material-symbols-outlined text-primary fs-5">description</span>
                                    <p class="mb-0 small">Lección 2.1: Entendiendo el SEO</p>
                                </div>
                                <div class="lesson-actions d-flex align-items-center gap-1">
                                    <button class="btn btn-link text-body-secondary p-1" title="Editar"><span class="material-symbols-outlined fs-6">edit</span></button>
                                    <button class="btn btn-link text-danger p-1" title="Eliminar"><span class="material-symbols-outlined fs-6">delete</span></button>
                                </div>
                            </li>
                            <li class="lesson-item list-group-item-action rounded-3 d-flex align-items-center justify-content-between p-3">
                                <div class="d-flex align-items-center gap-3">
                                    <span class="material-symbols-outlined text-body-secondary" style="cursor: grab;">drag_indicator</span>
                                    <span class="material-symbols-outlined text-primary fs-5">quiz</span>
                                    <p class="mb-0 small">Cuestionario: Fundamentos del Capítulo 2</p>
                                </div>
                                <div class="lesson-actions d-flex align-items-center gap-1">
                                    <button class="btn btn-link text-body-secondary p-1" title="Editar"><span class="material-symbols-outlined fs-6">edit</span></button>
                                    <button class="btn btn-link text-danger p-1" title="Eliminar"><span class="material-symbols-outlined fs-6">delete</span></button>
                                </div>
                            </li>
                        </ul>
                    </div>
                </details>

                <div class="text-center p-5 border border-2 border-dashed rounded-3 mt-4">
                    <span class="material-symbols-outlined fs-1 text-body-tertiary">school</span>
                    <h3 class="mt-2 fs-5 fw-semibold">Tu curso está vacío</h3>
                    <p class="mt-1 small text-body-secondary">Comienza a construir tu plan de estudios agregando un nuevo módulo.</p>
                    <button class="mt-4 btn btn-primary d-inline-flex align-items-center justify-content-center gap-2 fw-bold small">
                        <span class="material-symbols-outlined fs-6">add</span>
                        <span>+ Agregar Módulo</span>
                    </button>
                </div>
                
            </div>
        </div>
    </main>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="LeccionForm.aspx.cs" Inherits="Vistas.Aministrador.LeccionForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="modal fade show" id="addLessonModal" tabindex="-1" aria-labelledby="addLessonModalLabel" style="display: block;" aria-modal="true" role="dialog">
        <div class="modal-dialog modal-lg modal-dialog-centered">

            <div class="modal-content rounded-3 shadow-lg">

                <div class="modal-header p-4 p-md-5 border-bottom">
                    <div class="d-flex flex-column gap-1">
                        <h1 class="modal-title h4 fw-bold" id="addLessonModalLabel">Añadir Nueva Lección</h1>
                        <p class="text-body-secondary small mb-0">Completa los detalles a continuación para crear una nueva lección.</p>
                    </div>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>

                <div class="modal-body p-4 p-md-5 d-flex flex-column gap-4">

                    <div>
                        <label for="lessonTitle" class="form-label fw-medium">Título de la Lección</label>
                        <input type="text" class="form-control form-control-lg" id="lessonTitle" placeholder="Ej: Introducción al Cálculo">
                    </div>

                    <div>
                        <label for="videoUrl" class="form-label fw-medium">Incrustar Enlace de Video</label>
                        <div class="input-group input-group-lg">
                            <span class="input-group-text bg-body-tertiary">
                                <span class="material-symbols-outlined fs-5 text-body-secondary">link</span>
                            </span>
                            <input type="text" class="form-control" id="videoUrl" placeholder="Pega una URL de YouTube, Vimeo, etc.">
                        </div>
                    </div>

                    <div class="d-flex flex-column gap-3">
                        <p class="mb-0 fw-medium">Documentos Asociados</p>

                        <label class="file-drop-zone d-flex flex-column align-items-center justify-content-center w-100 rounded-3 text-center p-4" style="cursor: pointer;">
                            <div class="d-flex align-items-center justify-content-center rounded-circle bg-primary-subtle" style="width: 3rem; height: 3rem;">
                                <span class="material-symbols-outlined fs-4 text-primary">upload_file</span>
                            </div>
                            <div class="d-flex flex-column py-2">
                                <p class="mb-0 fw-medium">Arrastra y suelta archivos aquí, o haz clic para buscar.</p>
                                <p class="text-body-secondary small mb-0">Soporta PDF, DOCX, PPTX, y más</p>
                            </div>
                            <button type="button" class="btn btn-light fw-bold small mt-2">Buscar Archivos</button>
                            <input type="file" class="d-none" multiple />
                        </label>

                        <div class="d-flex flex-column gap-3">
                            <div class="d-flex align-items-center justify-content-between rounded-3 border bg-body p-3">
                                <div class="d-flex align-items-center gap-3">
                                    <span class="material-symbols-outlined fs-2 text-danger">picture_as_pdf</span>
                                    <div class="d-flex flex-column">
                                        <p class="small fw-medium text-body-emphasis mb-0">Capitulo_1_Notas.pdf</p>
                                        <p class="small text-body-secondary mb-0">1.2 MB</p>
                                    </div>
                                </div>
                                <button class="btn btn-link text-body-secondary p-1">
                                    <span class="material-symbols-outlined fs-5">delete</span>
                                </button>
                            </div>
                            <div class="d-flex align-items-center justify-content-between rounded-3 border bg-body p-3">
                                <div class="d-flex align-items-center gap-3">
                                    <span class="material-symbols-outlined fs-2 text-primary">description</span>
                                    <div class="d-flex flex-column">
                                        <p class="small fw-medium text-body-emphasis mb-0">Hoja_de_trabajo_leccion.docx</p>
                                        <p class="small text-body-secondary mb-0">450 KB</p>
                                    </div>
                                </div>
                                <button class="btn btn-link text-body-secondary p-1">
                                    <span class="material-symbols-outlined fs-5">delete</span>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer p-4 p-md-5 d-flex justify-content-end gap-3 border-top">
                    <asp:Button ID="Button1" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                    <asp:Button ID="Button2" runat="server" Text="Guardar y Volver a la Edicion De Modulos" OnClick="btnGuardaryContinuar_Click" />
                </div>

            </div>
        </div>
    </div>

</asp:Content>

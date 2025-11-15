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
                </div>

                <div class="modal-body p-4 p-md-5 d-flex flex-column gap-4">

                    <div>
                        <asp:Label ID="lblTituloLeccion" AssociatedControlID="txtTituloLeccion" CssClass="form-label" runat="server" Text="Titulo de la Lección"></asp:Label>
                        <asp:TextBox ID="txtTituloLeccion" runat="server" CssClass="form-control" placeholder ="Ej: Fundamentos del Algebra"></asp:TextBox>
                    </div>

                    <div>
                        <asp:Label ID="lblVideoUrl" AssociatedControlID="txtVideoUrl" CssClass="form-label" runat="server" Text="Ingrese el Enlace de Video"></asp:Label>
                        
                        <asp:TextBox ID="txtVideoUrl" runat="server" CssClass="form-control" placeholder ="Pega una URL de Youtube,Vimeo,etc."></asp:TextBox>

                    </div>

                    <div class="d-flex flex-column gap-3">
                        <p class="mb-0 fw-medium">Documentos Asociados</p>


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

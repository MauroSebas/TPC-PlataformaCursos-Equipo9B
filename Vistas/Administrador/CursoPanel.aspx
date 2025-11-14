<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CursoPanel.aspx.cs" Inherits="Vistas.Aministrador.CursoPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main class="flex-grow-1 p-4 p-lg-5">

        <div class="container-xl">

            <header class="d-flex flex-column flex-sm-row flex-wrap justify-content-between align-items-start gap-4 mb-5">
                <div class="d-flex flex-column gap-1">
                    <h1 class="h2 fw-bolder text-body-emphasis mb-0">Gestión de Cursos</h1>
                    <p class="text-body-secondary fs-6">Visualiza, filtra y gestiona todos los cursos de forma eficiente.</p>
                </div>

                <%-- Utilizo LinkButton para rediccionar a la pagina de CursoForm >--%>
                <asp:LinkButton runat="server" ID="btnAgregarCurso"
                    CssClass="btn btn-primary btn-lg d-flex align-items-center gap-2 shadow-sm fw-bold small"
                    OnClick="btnAgregarCurso_Click1">
                    <span class="material-symbols-outlined fs-6">add_circle</span>
                    <span>Agregar Nuevo Curso</span>
                </asp:LinkButton>

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

            <asp:GridView ID="dgvCurso" DataKeyNames="Id" OnSelectedIndexChanged="dgvCurso_SelectedIndexChanged" runat="server" CssClass="table" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="Id" />
                    <asp:BoundField HeaderText="Título" DataField="Titulo" />
                    <asp:BoundField HeaderText="Categoria" DataField="Categoria.Nombre" />
                    <asp:BoundField HeaderText="Precio" DataField="Precio" />
                    <asp:BoundField HeaderText="Estado" DataField="Publicado" />
                    <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" HeaderText="Acciones" />
                </Columns>
            </asp:GridView>
               

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
    </main>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CursoForm.aspx.cs" Inherits="Vistas.Aministrador.CursoForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                        <asp:Label ID="lblNombreCurso" AssociatedControlID="txtNombreCurso" CssClass="form-label" runat="server" Text="Nombre del Curso"></asp:Label>
                        <asp:TextBox ID="txtNombreCurso" runat="server" CssClass="form-control" placeholder="Ej: Introduccion a Base de Datos"></asp:TextBox>
                    </div>

                    <div class="mb-2">
                        <asp:Label ID="lblImagenPortada" AssociatedControlID="txtImagenPortada" CssClass="form-label" runat="server" Text="Ingrese la URL de la Imagen que utilizara como Portada"></asp:Label>
                        <asp:TextBox ID="txtImagenPortada" runat="server" CssClass="form-control"></asp:TextBox>
                        <%--Usar un updatepanel--%>
                    </div>

                    <div class="mb-2">
                        <asp:Label ID="lblDescripcion" AssociatedControlID="txtDescripcion" CssClass="form-label" runat="server" Text="Descripción Detallada"></asp:Label>
                        <asp:TextBox ID="txtDescripcion" TextMode="MultiLine" runat="server" CssClass="form-control" placeholder="Proporciona una descripción detallada del curso, incluyendo objetivos de aprendizaje, público objetivo y esquema de contenido..."></asp:TextBox>
                    </div>

                    <hr class="my-3" />

                    <div class="row g-4">
                        <div class="col-md-6">
                            <asp:Label ID="lblCategoria" AssociatedControlID="ddlCategoria" CssClass="form-label" runat="server" Text="Categoria"></asp:Label>
                            <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" DataTextField="Nombre" DataValueField="Id"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <hr class="my-3" />

                <div class="d-flex flex-column gap-3">
                    <h3 class="h5 fw-bold">Precio</h3>

                    <div class="mb-3">
                        <asp:Label ID="lblPrecio" runat="server" Text="Precio del Curso" CssClass="form-label fw-medium" AssociatedControlID="txtPrecio" />
                        <div class="input-group mt-2" style="max-width: 200px;">
                            <span class="input-group-text">$</span>
                            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" TextMode="Number" placeholder="99.00" step="0.01" />
                        </div>
                    </div>
                </div>
            </div>

            <hr class="my-3" />

            <div class="d-flex flex-column gap-3">
                <h3 class="h5 fw-bold">Control de Acceso</h3>

                <div class="form-check">
                    <asp:RadioButton ID="rbAccesoPermanente" runat="server" GroupName="ControlAcceso" CssClass="form-check-input" Checked="true" />
                    <asp:Label ID="lblAccesoPermanente" runat="server" Text="Acceso Permanente" CssClass="form-check-label fs-6" AssociatedControlID="rbAccesoPermanente" />
                </div>

                <div>
                    <div class="form-check mb-2">
                        <asp:RadioButton ID="rbTiempoLimitado" runat="server" GroupName="ControlAcceso" CssClass="form-check-input" />
                        <asp:Label ID="lblTiempoLimitado" runat="server" Text="Tiempo Limitado (en días)" CssClass="form-check-label fs-6" AssociatedControlID="rbTiempoLimitado" />
                    </div>

                    <div style="max-width: 200px;">
                        <asp:TextBox ID="txtDuracionDias" runat="server" CssClass="form-control" TextMode="Number" placeholder="Ej: 90" />
                    </div>
                </div>
            </div>

            <hr class="my-3" />

            <div class="d-flex justify-content-end align-items-center gap-3 pt-2">
                <asp:Button ID="btnCancelar" Text="Cancelar" runat="server" CssClass="btn btn-danger" OnClick="btnCancelar_Click" />
                <asp:Button ID="btnGuardarCurso" Text="Guardar y Continuar" runat="server" CssClass="btn btn-primary" OnClick="btnGuardarCurso_Click" />
            </div>

        </div>

    </main>
</asp:Content>


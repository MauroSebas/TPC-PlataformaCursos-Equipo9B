<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CursoForm.aspx.cs" Inherits="Vistas.Aministrador.CursoForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   

  <div class="d-flex flex-wrap justify-content-between align-items-center gap-3 mb-4 border-bottom pb-3">
        <div class="d-flex flex-column gap-1">
            <h1 class="h3 fw-bolder text-body-emphasis mb-0">
                <asp:Literal ID="litTituloPagina" runat="server" Text="Crear Nuevo Curso" />
            </h1>
            <p class="text-body-secondary fs-6 mb-0">Completa los datos principales del curso.</p>
        </div>
        <div>
             <asp:HyperLink ID="btnVolver" runat="server" NavigateUrl="~/Administrador/CursoPanel.aspx" CssClass="btn btn-outline-secondary d-flex align-items-center gap-2 fw-bold small">
                <i class="bi bi-arrow-left"></i>
                <span>Volver a Cursos</span>
            </asp:HyperLink>
        </div>
    </div>

    <asp:UpdatePanel ID="updMensajeGlobal" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlMensajeGlobal" runat="server" Visible="false" EnableViewState="false" CssClass="alert">
                <asp:Literal ID="litMensajeGlobal" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="card shadow-sm border-0 rounded-lg">
        <div class="card-body p-4 p-lg-5">
            <asp:UpdatePanel ID="updFormulario" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row g-4">
                        
                        <div class="col-lg-6 d-flex flex-column gap-4">
                            
                            <div class="form-group">
                                <asp:Label ID="lblTitulo" runat="server" Text="Título del Curso" CssClass="form-label fw-medium" AssociatedControlID="txtTitulo" />
                                <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" placeholder="Ej: Introducción a la Programación" MaxLength="100" />
                                <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtTitulo" ErrorMessage="El título es obligatorio." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Curso" />
                                <asp:RegularExpressionValidator ID="revTitulo" runat="server" ControlToValidate="txtTitulo" ErrorMessage="El título debe tener entre 5 y 100 caracteres." ValidationExpression="^.{5,100}$" CssClass="text-danger small" Display="Dynamic" ValidationGroup="Curso" />
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lblDescripcion" runat="server" Text="Descripción Larga" CssClass="form-label fw-medium" AssociatedControlID="txtDescripcion" />
                                <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control" placeholder="Describe de qué trata el curso, lo que el alumno aprenderá..." />
                                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="La descripción es obligatoria." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Curso" />
                                <asp:RegularExpressionValidator ID="revDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="La descripción debe tener al menos 20 caracteres." ValidationExpression="^.{20,4000}$" CssClass="text-danger small" Display="Dynamic" ValidationGroup="Curso" />
                            </div>

                             <div class="form-group">
                                <asp:Label ID="lblImagen" runat="server" Text="Imagen de Portada (Subir archivo o URL)" CssClass="form-label fw-medium" />
                                <p class="text-muted small mb-1">Puedes subir un archivo o pegar la URL de una imagen externa.</p>
                                
                                <div class="input-group mb-2">
                                    <span class="input-group-text">URL</span>
                                    <asp:TextBox ID="txtUrlImagen" runat="server" CssClass="form-control" placeholder="Opcional: Pegar URL de la imagen aquí (http://...)" />
                                </div>
                                
                                <asp:FileUpload ID="fileUploadPortada" runat="server" CssClass="form-control" />

                                <asp:RegularExpressionValidator ID="revUrlImagen" runat="server" ControlToValidate="txtUrlImagen" ErrorMessage="Ingresa una URL de imagen válida (http:// o https://)." ValidationExpression="(http|https):\/\/([\w\.]+\/?)\S*" CssClass="text-danger small" Display="Dynamic" ValidationGroup="Curso" />

                                <asp:Image ID="imgPortadaActual" runat="server" CssClass="img-fluid rounded mt-2" Visible="false" Style="max-height: 150px;" />
                            </div>
                        </div>

                        <div class="col-lg-6 d-flex flex-column gap-4">

                            <div class="form-group">
                                <asp:Label ID="lblCategoria" runat="server" Text="Categoría" CssClass="form-label fw-medium" AssociatedControlID="ddlCategoria" />
                                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" DataTextField="Nombre" DataValueField="Id" />
                                <asp:RequiredFieldValidator ID="rfvCategoria" runat="server" InitialValue="0" ControlToValidate="ddlCategoria" ErrorMessage="Debes seleccionar una categoría." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Curso" />
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lblPrecio" runat="server" Text="Precio (ARS)" CssClass="form-label fw-medium" AssociatedControlID="txtPrecio" />
                                <div class="input-group">
                                    <span class="input-group-text">$</span>
                                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" TextMode="Number" placeholder="99.99" />
                                </div>
                                <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" ControlToValidate="txtPrecio" ErrorMessage="El precio es obligatorio." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Curso" />
                                <asp:RangeValidator ID="rvPrecio" runat="server" ControlToValidate="txtPrecio" Type="Currency" MinimumValue="0" MaximumValue="1000000" ErrorMessage="El precio debe ser un número válido." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Curso" />
                            </div>

                             <div class="form-group">
                                <asp:Label ID="lblDuracion" runat="server" Text="Duración Acceso (Días)" CssClass="form-label fw-medium" AssociatedControlID="txtDuracionDias" />
                                <asp:TextBox ID="txtDuracionDias" runat="server" CssClass="form-control" TextMode="Number" placeholder="Ej: 365 (Un año)" />
                                <asp:RequiredFieldValidator ID="rfvDuracionDias" runat="server" ControlToValidate="txtDuracionDias" ErrorMessage="La duración es obligatoria (ej: 365 días)." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Curso" />
                            </div>
                            
                            <div class="form-group">
                                <asp:Label ID="lblModalidad" runat="server" Text="Modalidad de Pago" CssClass="form-label fw-medium" AssociatedControlID="ddlModalidadPago" />
                                <asp:DropDownList ID="ddlModalidadPago" runat="server" CssClass="form-select" />
                            </div>

                            </div>
                    </div>

                    <div class="d-flex justify-content-end gap-3 pt-4 mt-4 border-top">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Curso" CssClass="btn btn-primary btn-lg" OnClick="btnGuardar_Click" ValidationGroup="Curso" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
   
</asp:Content>


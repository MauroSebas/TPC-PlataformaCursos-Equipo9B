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
            <asp:HyperLink ID="btnVolver" runat="server" NavigateUrl="~/Administrador/Curso/CursoPanel.aspx"
                CssClass="btn btn-outline-secondary d-flex align-items-center gap-2 fw-bold small">
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
                    
                    <div class="row g-5"> <div class="col-lg-6 d-flex flex-column gap-4">
                            
                            <div class="form-group">
                                <asp:Label ID="lblTitulo" runat="server" Text="Título del Curso" CssClass="form-label fw-medium" AssociatedControlID="txtTitulo" />
                                <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" placeholder="Ej: Introducción a la Programación" MaxLength="100" />
                                <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtTitulo" ErrorMessage="El título es obligatorio." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Curso" />
                                <asp:RegularExpressionValidator ID="revTitulo" runat="server" ControlToValidate="txtTitulo" ErrorMessage="Entre 5 y 100 caracteres." ValidationExpression="^.{5,100}$" CssClass="text-danger small" Display="Dynamic" ValidationGroup="Curso" />
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lblDescripcion" runat="server" Text="Descripción Larga" CssClass="form-label fw-medium" AssociatedControlID="txtDescripcion" />
                                <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" placeholder="Describe de qué trata el curso..." />
                                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="La descripción es obligatoria." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Curso" />
                                <asp:RegularExpressionValidator ID="revDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Mínimo 20 caracteres." ValidationExpression="^[\s\S]{20,4000}$" CssClass="text-danger small" Display="Dynamic" ValidationGroup="Curso" />
                            </div>

                            <div class="form-group">
                                <div class="d-flex justify-content-between align-items-center mb-2">
                                    <label class="form-label fw-medium mb-0">Imagen de Portada</label>
                                    <small class="text-muted" style="font-size: 0.75rem;">
                                        <i class="bi bi-info-circle me-1"></i>1500x900 px (16:9)
                                    </small>
                                </div>

                                <div class="border rounded p-3 bg-body-tertiary"> <div class="row g-3">
                                        <div class="col-md-12">
                                            <div class="d-flex gap-3 mb-2">
                                                <div class="form-check">
                                                    <asp:RadioButton ID="rbImagenArchivo" runat="server" GroupName="TipoImagen" 
                                                        Text=" Subir Archivo" Checked="true" 
                                                        AutoPostBack="true" OnCheckedChanged="rbTipoImagen_CheckedChanged" />
                                                </div>
                                                <div class="form-check">
                                                    <asp:RadioButton ID="rbImagenUrl" runat="server" GroupName="TipoImagen" 
                                                        Text=" Usar URL" 
                                                        AutoPostBack="true" OnCheckedChanged="rbTipoImagen_CheckedChanged" />
                                                </div>
                                            </div>
                                            
                                            <div class="mb-2">
                                                <asp:FileUpload ID="fileUploadPortada" runat="server" CssClass="form-control form-control-sm" />
                                            </div>
                                            <div class="mb-0">
                                                <asp:TextBox ID="txtUrlImagen" runat="server" CssClass="form-control form-control-sm" placeholder="https://..." Enabled="false" />
                                                <asp:RegularExpressionValidator ID="revUrlImagen" runat="server" ControlToValidate="txtUrlImagen" ErrorMessage="URL inválida." ValidationExpression="(http|https):\/\/([\w\.]+\/?)\S*" CssClass="text-danger small" Display="Dynamic" ValidationGroup="Curso" />
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="d-flex justify-content-center align-items-center  border rounded overflow-hidden position-relative" style="height: 180px; border-style: dashed !important;">
                                                <asp:Image ID="imgPortadaActual" runat="server" CssClass="img-fluid w-100 h-100 object-fit-cover" Visible="false" />
                                                
                                                <% if (!imgPortadaActual.Visible) { %>
                                                    <div class="text-center text-secondary">
                                                        <i class="bi bi-image fs-1 opacity-25"></i>
                                                        <div class="small opacity-50">Vista Previa</div>
                                                    </div>
                                                <% } %>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div> <div class="col-lg-6 d-flex flex-column gap-4">

                            <div class="form-group">
                                <asp:Label ID="lblCategoria" runat="server" Text="Categoría" CssClass="form-label fw-medium" AssociatedControlID="ddlCategoria" />
                                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" DataTextField="Nombre" DataValueField="Id" />
                                <asp:RequiredFieldValidator ID="rfvCategoria" runat="server" InitialValue="0" ControlToValidate="ddlCategoria" ErrorMessage="Seleccioná una categoría." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Curso" />
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblModalidad" runat="server" Text="Modalidad" CssClass="form-label fw-medium" AssociatedControlID="ddlModalidadPago" />
                                        <asp:DropDownList ID="ddlModalidadPago" runat="server" CssClass="form-select"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlModalidadPago_SelectedIndexChanged" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblPrecio" runat="server" Text="Precio" CssClass="form-label fw-medium" AssociatedControlID="txtPrecio" />
                                        <div class="input-group">
                                            <span class="input-group-text">$</span>
                                            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" TextMode="Number" step="0.01" placeholder="0.00" />
                                        </div>
                                        <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" ControlToValidate="txtPrecio" ErrorMessage="Requerido." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Curso" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lblDuracion" runat="server" Text="Duración Acceso (Días)" CssClass="form-label fw-medium" AssociatedControlID="txtDuracionDias" />
                                <div class="input-group">
                                    <asp:TextBox ID="txtDuracionDias" runat="server" CssClass="form-control" TextMode="Number" placeholder="Ej: 365" />
                                    <span class="input-group-text text-muted">días</span>
                                </div>
                                <div class="form-text small">0 = Acceso ilimitado</div>
                                <asp:RequiredFieldValidator ID="rfvDuracionDias" runat="server" ControlToValidate="txtDuracionDias" ErrorMessage="Requerido." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Curso" />
                            </div>

                            <hr class="text-muted opacity-25" />

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblNivel" runat="server" Text="Nivel" CssClass="form-label fw-medium" AssociatedControlID="ddlNivel" />
                                        <asp:DropDownList ID="ddlNivel" runat="server" CssClass="form-select">
                                            <asp:ListItem Text="Principiante" Value="Principiante" />
                                            <asp:ListItem Text="Intermedio" Value="Intermedio" />
                                            <asp:ListItem Text="Avanzado" Value="Avanzado" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblIdioma" runat="server" Text="Idioma" CssClass="form-label fw-medium" AssociatedControlID="ddlIdioma" />
                                        <asp:DropDownList ID="ddlIdioma" runat="server" CssClass="form-select">
                                            <asp:ListItem Text="Español" Value="Español" />
                                            <asp:ListItem Text="Inglés" Value="Inglés" />
                                            <asp:ListItem Text="Portugués" Value="Portugués" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="p-3 bg-body-tertiary rounded d-flex align-items-center mt-2">
                                <div class="form-check form-switch m-0">
                                    <input class="form-check-input" type="checkbox" id="chkCertificadoHTML" runat="server" checked />
                                    <label class="form-check-label fw-medium ms-2" for="<%= chkCertificadoHTML.ClientID %>">
                                        Incluye Certificado de Finalización
                                    </label>
                                </div>
                            </div>
                            <div class="p-3 bg-body-tertiary rounded mt-2 border-top border-white">
    
    <div class="form-check form-switch">
        <input class="form-check-input" type="checkbox" id="chkRequiereExamen" runat="server" onchange="toggleExamenPanel()" />
        <label class="form-check-label fw-bold ms-2" for="<%= chkRequiereExamen.ClientID %>">
            Requiere Examen Final
        </label>
    </div>

    <div id="divLinkExamen" style="display: none;" class="mt-3 ps-1 animate-fade">
        <label class="form-label small text-muted mb-1">Link de la Consigna (Google Drive / PDF)</label>
        <div class="input-group input-group-sm">
            <span class="input-group-text "><i class="bi bi-link-45deg"></i></span>
            <asp:TextBox ID="txtUrlExamen" runat="server" CssClass="form-control" placeholder="https://..." />
        </div>
        <div class="form-text x-small mt-1">
            El alumno deberá descargar este archivo para realizar el trabajo práctico.
        </div>
    </div>

</div>

                        </div> 

                    </div> 
                    <div class="d-flex justify-content-end pt-4 mt-4 border-top">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Curso" CssClass="btn btn-primary btn-lg px-5" OnClick="btnGuardar_Click" ValidationGroup="Curso" />
                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnGuardar" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>


    <asp:UpdatePanel ID="updObjetivos" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlObjetivos" runat="server" Visible="false" CssClass="card shadow-sm border-0 rounded-lg mt-4 mb-5">
                
                <div class="card-header  border-bottom py-3">
                    <div class="d-flex align-items-center">
                        <div class="bg-primary bg-opacity-10 text-primary rounded p-2 me-3">
                            <i class="bi bi-list-check fs-5"></i>
                        </div>
                        <div>
                            <h5 class="mb-0 fw-bold">Lo que aprenderás</h5>
                            <small class="text-muted">Estos items aparecerán en la portada del curso.</small>
                        </div>
                    </div>
                </div>

                <div class="card-body p-4">
                    <div class="row">
                        <div class="col-lg-8 mx-auto">
                            
                            <div class="input-group mb-4 shadow-sm">
                                <asp:TextBox ID="txtNuevoObjetivo" runat="server" CssClass="form-control border-end-0" placeholder="Ej: Configurar SQL Server..." ValidationGroup="Objetivo"></asp:TextBox>
                                <asp:Button ID="btnAgregarObjetivo" runat="server" Text="Agregar" CssClass="btn btn-primary px-4" 
                                    OnClick="btnAgregarObjetivo_Click" ValidationGroup="Objetivo" />
                            </div>
                            <asp:RequiredFieldValidator ID="rfvObjetivo" runat="server" ControlToValidate="txtNuevoObjetivo" 
                                ErrorMessage="Escribí algo antes de agregar." CssClass="text-danger small d-block mb-2" 
                                Display="Dynamic" ValidationGroup="Objetivo" />

                            <div class="border rounded overflow-hidden">
                                <asp:GridView ID="dgvObjetivos" runat="server" CssClass="table table-hover align-middle mb-0" 
                                    AutoGenerateColumns="false" DataKeyNames="Id" GridLines="Horizontal" border="0"
                                    OnSelectedIndexChanged="dgvObjetivos_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Objetivos del Curso">
                                            <ItemTemplate>
                                                <div class="d-flex align-items-center">
                                                    <i class="bi bi-check-circle-fill text-success me-3"></i>
                                                    <%# Eval("Descripcion") %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField ItemStyle-Width="50px" ItemStyle-CssClass="text-end pe-3">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEliminarObj" runat="server" CommandName="Select" 
                                                    CssClass="btn btn-sm btn-light text-danger border-0" ToolTip="Eliminar">
                                                    <i class="bi bi-trash"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div class="text-center py-4 text-muted">
                                            <i class="bi bi-clipboard-x fs-3 d-block mb-2 opacity-50"></i>
                                            Aún no hay objetivos cargados.
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
    function toggleExamenPanel() {
        // Buscamos los controles por su ID de cliente (ASP.NET les cambia el ID al renderizar)
        var chk = document.getElementById('<%= chkRequiereExamen.ClientID %>');
        var panel = document.getElementById('divLinkExamen');

        if (chk && panel) {
            panel.style.display = chk.checked ? 'block' : 'none';
        }
    }

    // Ejecutar al cargar la página (por si viene editado con el check true)
    document.addEventListener("DOMContentLoaded", function () {
        toggleExamenPanel();
    });

    // Ejecutar después de que el UpdatePanel haga un postback parcial
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(toggleExamenPanel);
</script>
</asp:Content>


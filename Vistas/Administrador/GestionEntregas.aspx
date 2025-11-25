<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="GestionEntregas.aspx.cs" Inherits="Vistas.Administrador.GestionEntregas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="d-flex justify-content-between align-items-center mb-4">
        <h2 class="fw-bold text-body-emphasis">Corrección de Exámenes</h2>
    </div>

    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>

            <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert alert-success alert-dismissible fade show" role="alert">
                <asp:Literal ID="litMensaje" runat="server"></asp:Literal>
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </asp:Panel>

            <div class="card shadow-sm border-0 mb-4">
                
                <div class="card-header py-3 d-flex align-items-center gap-3">
                    <h5 class="mb-0 fw-bold me-auto"><i class="bi bi-journal-check text-primary me-2"></i>Listado de Entregas</h5>
                    
                    <label class="small fw-bold text-muted">Filtrar:</label>
                    <asp:DropDownList ID="ddlFiltroEstado" runat="server" CssClass="form-select form-select-sm w-auto shadow-sm" 
                        AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroEstado_SelectedIndexChanged">
                        <asp:ListItem Text="Pendientes" Value="Pendiente" Selected="True" />
                        <asp:ListItem Text="Aprobados" Value="Aprobado" />
                        <asp:ListItem Text="Rechazados" Value="Rechazado" />
                        <asp:ListItem Text="Todos" Value="Todos" />
                    </asp:DropDownList>
                </div>

                <div class="card-body p-0 table-responsive">
                    <asp:GridView ID="dgvEntregas" runat="server" CssClass="table table-hover mb-0 align-middle" 
                        AutoGenerateColumns="false" DataKeyNames="Id, InscripcionId" 
                        OnSelectedIndexChanged="dgvEntregas_SelectedIndexChanged" GridLines="None">
                        
                        <Columns>
                            <asp:BoundField DataField="FechaEntrega" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="NombreAlumno" HeaderText="Alumno" />
                            <asp:BoundField DataField="TituloCurso" HeaderText="Curso" />
                            
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <span class='badge rounded-pill <%# ObtenerClaseBadge(Eval("Estado").ToString()) %>'>
                                        <%# Eval("Estado") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Resolución">
                                <ItemTemplate>
                                    <a href='<%# Eval("UrlResolucion") %>' target="_blank" class="btn btn-sm btn-outline-primary border-0 fw-bold">
                                        <i class="bi bi-box-arrow-up-right me-1"></i>Ver Link
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Acción" ItemStyle-CssClass="text-end pe-3">
                                <ItemTemplate>
                                    <asp:Button ID="btnSeleccionar" runat="server" CommandName="Select" 
                                        Text='<%# Eval("Estado").ToString() == "Pendiente" ? "Corregir" : "Editar" %>' 
                                        CssClass='<%# Eval("Estado").ToString() == "Pendiente" ? "btn btn-primary btn-sm px-3 shadow-sm" : "btn btn-outline-secondary btn-sm px-3 border-0" %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <EmptyDataTemplate>
                            <div class="text-center py-5">
                                <i class="bi bi-check-circle fs-1 text-success opacity-50"></i>
                                <p class="mt-3 text-muted">No se encontraron entregas con este filtro.</p>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>

            <asp:Panel ID="pnlCorreccion" runat="server" Visible="false" CssClass="card shadow border-0 bg-body-tertiary animate-fade mb-5">
                <div class="card-header bg-transparent border-0 pt-4 px-4">
                    <h5 class="fw-bold mb-0">
                        <asp:Literal ID="litTituloAccion" runat="server" Text="Evaluación" />
                    </h5>
                </div>
                <div class="card-body p-4">
                    <div class="row g-4">
                        
                        <div class="col-lg-7">
                            <div class="form-group h-100 d-flex flex-column">
                                <label class="form-label fw-bold">Devolución del Profesor</label>
                                <asp:TextBox ID="txtDevolucion" runat="server" TextMode="MultiLine" 
                                    CssClass="form-control flex-grow-1" style="min-height: 150px;" 
                                    placeholder="Escribí aquí tus correcciones..."></asp:TextBox>
                                <div class="form-text text-muted">Este mensaje le llegará al alumno.</div>
                            </div>
                        </div>

                        <div class="col-lg-5">
                            <div class="p-3  rounded border h-100">
                                <h6 class="fw-bold border-bottom pb-2 mb-3">Veredicto Final</h6>
                                
                                <div class="mb-3">
                                    <label class="form-label small fw-bold text-success">
                                        <i class="bi bi-file-earmark-pdf-fill me-1"></i>Subir Certificado
                                    </label>
                                    <asp:FileUpload ID="fuCertificado" runat="server" CssClass="form-control form-control-sm" />
                                    <div class="form-text x-small">
                                        Obligatorio si apruebas. Debe ser PDF.
                                    </div>
                                </div>

                                <div class="d-grid gap-2">
                                    <asp:Button ID="btnAprobar" runat="server" Text="✅ APROBAR" 
                                        CssClass="btn btn-success fw-bold py-2" OnClick="btnAprobar_Click" />
                                    
                                    <asp:Button ID="btnRechazar" runat="server" Text="❌ RECHAZAR" 
                                        CssClass="btn btn-outline-danger py-2" OnClick="btnRechazar_Click" />
                                </div>

                                <div class="text-center mt-2">
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                                        CssClass="btn btn-link text-decoration-none btn-sm text-muted" OnClick="btnCancelar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnAprobar" />
        </Triggers>

    </asp:UpdatePanel>
</asp:Content>

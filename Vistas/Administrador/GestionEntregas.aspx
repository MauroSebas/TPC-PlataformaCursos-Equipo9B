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
                <div class="card-header  py-3">
                    <h5 class="mb-0 fw-bold"><i class="bi bi-hourglass-split text-warning me-2"></i>Entregas Pendientes</h5>
                </div>
                <div class="card-body p-0">
                    <asp:GridView ID="dgvEntregas" runat="server" CssClass="table table-hover mb-0 align-middle" 
                        AutoGenerateColumns="false" DataKeyNames="Id, InscripcionId" 
                        OnSelectedIndexChanged="dgvEntregas_SelectedIndexChanged" GridLines="None">
                        
                        <Columns>
                            <asp:BoundField DataField="FechaEntrega" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="NombreAlumno" HeaderText="Alumno" />
                            <asp:BoundField DataField="TituloCurso" HeaderText="Curso" />
                            
                            <asp:TemplateField HeaderText="Resolución">
                                <ItemTemplate>
                                    <a href='<%# Eval("UrlResolucion") %>' target="_blank" class="btn btn-sm btn-outline-primary">
                                        <i class="bi bi-box-arrow-up-right me-1"></i>Ver Trabajo
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Acción" ItemStyle-CssClass="text-end">
                                <ItemTemplate>
                                    <asp:Button ID="btnSeleccionar" runat="server" Text="Corregir" CommandName="Select" 
                                        CssClass="btn btn-primary btn-sm px-3" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <EmptyDataTemplate>
                            <div class="text-center py-5">
                                <i class="bi bi-check-circle fs-1 text-success opacity-50"></i>
                                <p class="mt-3 text-muted">¡Estás al día! No hay entregas pendientes.</p>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>

            <asp:Panel ID="pnlCorreccion" runat="server" Visible="false" CssClass="card shadow border-0 bg-body-tertiary animate-fade">
                <div class="card-body p-4">
                    <h5 class="fw-bold mb-3">Evaluación del Alumno</h5>
                    
                    <div class="row">
                        <div class="col-md-8">
                            <div class="form-group mb-3">
                                <label class="form-label fw-bold">Devolución / Comentarios</label>
                                <asp:TextBox ID="txtDevolucion" runat="server" TextMode="MultiLine" Rows="4" 
                                    CssClass="form-control" placeholder="Escribí aquí tus correcciones o felicitaciones..."></asp:TextBox>
                                <div class="form-text text-muted">Este mensaje le llegará al alumno.</div>
                            </div>
                        </div>
                        <div class="col-md-4 d-flex flex-column gap-2 justify-content-center">
                            <p class="small text-muted text-center mb-2">Seleccioná un veredicto:</p>
                            
                            <asp:Button ID="btnAprobar" runat="server" Text="✅ APROBAR" 
                                CssClass="btn btn-success w-100 py-2 fw-bold" OnClick="btnAprobar_Click" />
                            
                            <asp:Button ID="btnRechazar" runat="server" Text="❌ RECHAZAR" 
                                CssClass="btn btn-outline-danger w-100 py-2" OnClick="btnRechazar_Click" />
                            
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                                CssClass="btn btn-link text-decoration-none btn-sm mt-2" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CategoriaGestion.aspx.cs" Inherits="Vistas.CategoriaGestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <h1>Gestión de Categorias </h1>
    <h5>Crea,edita y gestiona todas las categorias de cursos.</h5>
    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalAgregar">
        + Agregar Nueva Categoria
    </button>
    <asp:Label ID="lblFiltro" class="form-label" runat="server" Text="Buscar Categoria"></asp:Label>
    <asp:TextBox ID="txtFiltro" runat="server"></asp:TextBox>


    <asp:GridView ID="dgvCategorias" DataKeyNames="Id" OnSelectedIndexChanged="dgvCategorias_SelectedIndexChanged" runat="server" CssClass="table table-hover" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" HeaderText="Accion" />
        </Columns>
    </asp:GridView>


    <!-- Modal Agregar -->
    <div class="modal fade" id="modalAgregar" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="staticBackdropLabel">Nueva Categoria</h1>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre: "></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                    <asp:Button ID="btnAgregarCategoria" runat="server" Text="Agregar" class="btn btn-primary" OnClick="btnAgregarCategoria_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CategoriaEliminarEditar.aspx.cs" Inherits="Vistas.CategoriaEliminarEditar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
    <asp:TextBox runat="server" ID="txtModificar" CssClass="form-control" />
    <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-primary" OnClick="btnModificar_Click"/>
    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-primary" OnClick="btnEliminar_Click" />
    <asp:Button ID="btnSalir" runat="server" Text="Salir" CssClass="btn btn-danger" OnClick="btnSalir_Click"/>
</asp:Content>

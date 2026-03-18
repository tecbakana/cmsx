<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="conteudoLista.aspx.cs" Inherits="conteudoLista" %>
<%@ Register Src="~/controles/menu.ascx" TagName="uc" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:uc ID="ucMenu" runat="server" />
<asp:DataList ID="listaApp" runat="server">
<ItemTemplate>
    <asp:Label runat="server" ID="lblTitulo" Text=<%# Eval("Titulo") %> />
    <asp:Label runat="server" ID="lblConteudo" Text=<%# Eval("Texto") %>/>
    <asp:Label runat="server" ID="Label1" Text=<%# Eval("Autor") %>/>
</ItemTemplate>
</asp:DataList>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/bootstrap_client.master" AutoEventWireup="true" CodeFile="produto.aspx.cs" Inherits="produto" %>
<%@ Register Src="~/controles/ucproduto.ascx" TagName="lsproduto" TagPrefix="asp" %>
<%@ Register Src="~/controles/ucProdutoDetalhe.ascx" TagPrefix="asp" TagName="ucProdutoDetalhe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="css/handhome.css" rel="stylesheet" />

   <%--       <div class="starter-template">
      </div>--%>
    <div class="container">
        <h1><asp:Label ID="lblTitulo" runat="server" Text=<%#Eval("titulo") %> CssClass="tituloConteudo" /></h1>
        <p class="lead">
            <asp:Label ID = "lblTexto" runat="server" Text=<%#Eval("texto") %> CssClass="conteudotexto" />
        </p>
        <asp:lsproduto ID="lsproduto" runat="server" TipoArea="9" />
    </div>

<asp:ucProdutoDetalhe runat="server" ID="ucProdutoDetalhe" />
</asp:Content>


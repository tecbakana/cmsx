<%@ Page Title="" Language="C#" MasterPageFile="~/InnerCliente.master" AutoEventWireup="true" CodeFile="remote_loadProduto.aspx.cs" Inherits="remote_loadProduto" %>
<%@ Register Src="~/controles/ucProdutoDetalhe.ascx" TagPrefix="uc1" TagName="ucProdutoDetalhe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucProdutoDetalhe runat="server" ID="ucProdutoDetalhe" tipo="true" />  
</asp:Content>


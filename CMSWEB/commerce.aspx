<%@ Page Title="" Language="C#" MasterPageFile="~/bootstrap_client.master" AutoEventWireup="true" CodeFile="commerce.aspx.cs" Inherits="commerce" %>

<%@ Register Src="~/controles/ucproduto.ascx" TagName="lstproduto" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="css/grid.css" rel="stylesheet" />
    <div class="container">
        <asp:lstproduto ID="lstProduto" runat="server" TipoArea="8" />
    </div>
</asp:Content>


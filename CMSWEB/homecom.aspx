<%@ Page Title="" Language="C#" MasterPageFile="~/CommerceMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="homecom.aspx.cs" Inherits="homecom" %>
<%@ Register Src="~/controles/ucHomeCommerce.ascx" TagPrefix="uc1" TagName="ucHomeCommerce" %>
<%@ Register Src="~/controles/ucCategorias.ascx" TagPrefix="uc1" TagName="ucCategorias" %>
<%@ Register Src="~/controles/ucProdutoLista.ascx" TagPrefix="uc1" TagName="ucProdutoLista" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cplMaster" Runat="Server">
    <div class="row">
        <asp:Panel CssClass="col-sm-3" ID="pnlListaCategoria" runat="server" >
            <uc1:ucCategorias runat="server" id="ucCategorias" />
        </asp:Panel> 
        <asp:Panel ID="pnlListaProduto" runat="server" CssClass="col-sm-9 padding-right">
            <uc1:ucProdutoLista runat="server" id="ucProdutoLista1" />
        </asp:Panel>
    </div>
</asp:Content>


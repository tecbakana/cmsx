<%@ Page Title="" Language="C#" MasterPageFile="~/CommerceMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="shop.aspx.cs" Inherits="shop" %>
<%@ Register Src="~/controles/ucHomeCommerce.ascx" TagPrefix="uc1" TagName="ucHomeCommerce" %>
<%@ Register Src="~/controles/ucCategorias.ascx" TagPrefix="uc1" TagName="ucCategorias" %>
<%@ Register Src="~/controles/ucProdutoLista.ascx" TagPrefix="uc1" TagName="ucProdutoLista" %>
<%@ Register Src="~/controles/ucProdutoDetalhe.ascx" TagPrefix="uc1" TagName="ucProdutoDetalhe" %>
<%@ Register Src="~/controles/ucPagamento.ascx" TagPrefix="uc1" TagName="ucPagamento" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cplMaster" Runat="Server">
    <div class="row">

        <asp:Panel ID="pnlDetalhe" runat="server" CssClass="col-sm-9 padding-right">
           <uc1:ucProdutoDetalhe runat="server" ID="ucProdutoDetalhe" />  
        </asp:Panel>
        <asp:Panel ID="pnlPagamento" runat="server" CssClass="col-sm-9 padding-right">
            <!-- Modal -->
            <div class="modal fade col-lg-12" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="myModalLabel"><asp:Literal ID="ltshopNome" runat="server" /></h4>
                    </div>
                    <div class="modal-body">
                    <uc1:ucPagamento runat="server" ID="ucPagamento" />
                    </div>
                    <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
                    </div>
                </div>
                </div>
            </div>
        </asp:Panel>
    </div>


</asp:Content>


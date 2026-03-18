<%@ Page Title="" Language="C#" MasterPageFile="~/bootstrap.master" AutoEventWireup="true" CodeFile="homehand.aspx.cs" Inherits="homehand" %>

<%@ Register Src="~/controles/menu_cliente.ascx" TagPrefix="uc1" TagName="menu_cliente" %>
<%@ Register Src="~/controles/ucproduto.ascx" TagName="lstproduto" TagPrefix="asp" %>
<%@ Register Src="~/controles/ucProdutoDetalhe.ascx" TagPrefix="asp" TagName="ucProdutoDetalhe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="css/handhome.css" rel="stylesheet" />
     <div class="navbar navbar-inverse navbar-fixed-top" role="navigation">
      <div class="navbar-inner">
          <div class="container">
            <div class="navbar-header">
              <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
              </button>
              <a  class="brand" href="#"> <img src="images/handcms.jpg" class="brand-logo" /></a>&nbsp;
            </div>
            <nav class="collapse navbar-collapse">
                <uc1:menu_cliente runat="server" ID="menu_cliente" />
            </nav><!--/.nav-collapse -->
          </div>
      </div>
    </div>

    <div class="container">
        <div class="col-md-7" style="display: block; position: absolute; margin: 0px; top: 120px; bottom: 0px; left: 0px; right: 0px; width: auto; z-index: 0; height: auto; visibility: visible;">
            <%--<iframe src="commerce.aspx" id="frconteudo" name="frconteudo" class="iframe" name="info" width="100%" height="100%" seamless=""></iframe>--%>
            <asp:UpdatePanel ID="pnlNavigation" ClientIDMode="Static" runat="server" Visible="false">
                <ContentTemplate>

                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="pnlCommerce" ClientIDMode="Static" runat="server" Visible="false" UpdateMode="Always">
                <ContentTemplate>
                    <link href="css/grid.css" rel="stylesheet" />
                    <div class="container">
                        <asp:lstproduto ID="lstProduto" runat="server" TipoArea="8" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="pnlProdutoDetalhe" runat="server" ClientIDMode="Static" Visible="false" UpdateMode="Always">
                <ContentTemplate>
                    <h1><asp:Label ID="lblTitulo" runat="server" Text=<%#Eval("titulo") %> CssClass="tituloConteudo" /></h1>
                    <p class="lead">
                        <asp:Label ID = "lblTexto" runat="server" Text=<%#Eval("texto") %> CssClass="conteudotexto" />
                    </p>
                    <asp:lstproduto ID="lstprodutoN" runat="server" TipoArea="9" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div><!-- /.container -->
</asp:Content>


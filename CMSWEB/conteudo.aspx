<%@ Page Language="C#" MasterPageFile="~/CommerceMaster.master" AutoEventWireup="true" CodeFile="conteudo.aspx.cs" Inherits="conteudo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplMaster" Runat="Server">
<!-- --------------------------------- DIV CONTEUDO -------------------------------------- -->

    <div class="container">

      <div class="starter-template">
        <h1><asp:Label ID="lblTitulo" runat="server" Text=<%#Eval("titulo") %> CssClass="tituloConteudo" /></h1>
        <p class="lead">
            <asp:Label ID = "lblTexto" runat="server" Text=<%#Eval("texto") %> CssClass="conteudotexto" />
        </p>
      </div>

    </div><!-- /.container -->
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente.master" AutoEventWireup="true" CodeFile="heritage.aspx.cs" Inherits="heritage" %>

<%@ Register Src="~/controles/menu_cliente.ascx" TagName="umc" TagPrefix="asp" %>
<%@ Register Src="~/controles/menulateral.ascx" TagName="uc" TagPrefix="asp" %>
<%@ Register Src="~/controles/menu_splash_lateral.ascx" TagName="ucs" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="Stylesheet" type="text/css" href="html/css/heritage.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="main">

    <div id="topo">
        <img src="images/topo_heritage.jpg" width="1000" />
    </div>
    <div id="menutopo">
    <asp:umc ID="mntopo" runat="server" />
    </div>

    <div class="agrega">
        <div id="barralateral">
            <img src="images/botao_reservaonline.gif" alt="acontecendo" />
            <br />
            <img src="images/botao_acontecendo.gif" alt="acontecendo" />
            <div id="menulateral">
              <asp:uc ID="mnlateral" runat="server" />
            </div>
            <br />
            <img src="images/botao_ofertas.gif" alt="acontecendo" />
            <div id="menusplash">
               <asp:ucs ID="mnsplash" runat="server" />
            </div>
        </div>
        <div id="conteudo">
            <iframe id="frconteudo" class="" width="750" height="620" frameborder="0" src="home.aspx">

            </iframe>
        </div>
    </div>
    <div id="rodape" >
       <img src="images/rodape.jpg" />
    </div>
</div>
</asp:Content>


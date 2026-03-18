<%@ Page Title="Heritage Residence" Language="C#" MasterPageFile="~/Cliente.master" AutoEventWireup="true" CodeFile="home_recorte.aspx.cs" Inherits="home_recorte" %>
<%@ Register Src="~/controles/menu_cliente.ascx" TagName="umc" TagPrefix="asp" %>
<%@ Register Src="~/controles/menulateral.ascx" TagName="uc" TagPrefix="asp" %>
<%@ Register Src="~/controles/menu_splash_lateral.ascx" TagName="ucs" TagPrefix="asp" %>
<%@ Register Src="~/controles/uctwitterbox.ascx" TagName="uct" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="html/css/heritage.css" />    
    <link href="Styles/dropdown.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="main" style="border:0px solid red">
<table id="Table_01" width="886" border="0" cellpadding="0" cellspacing="0" style="border:1px">
	<tr>
		<td width="885" height="101" colspan="4">
        <img src="images/topo_heritage.jpg" width="887" style="border:0px solid #1C4073;"  />
        </td>
	</tr>
	<tr>
		<td width="886" height="32" colspan="4"id="menutopo">
           <asp:umc ID="mntopo" runat="server" />
        </td>
	</tr>
	<tr>
		<td width="886" height="9" colspan="4" class="barras"></td>
	</tr>
	<tr>
		<td width="875" height="1" colspan="3"></td>
		<td width="11" height="638" rowspan="9" class="barras"></td>
	</tr>
	<tr>
		<td width="12" height="637" rowspan="8" class="barras"></td>
		<td width="227" height="0" style="border:0px solid black;visibility:hidden"></td>
		<td width="636" height="541" rowspan="6" style="vertical-align:top">
            <iframe id="frconteudo" name="frconteudo" class="" width="636" height="540" frameborder="0" src="home.aspx">

            </iframe>
        </td>
	</tr>
	<tr>
		<td width="227" height="27">
            <a href="reservasonline.aspx" target="frconteudo">
            <img src="images/botao_reservaonline.gif" alt="acontecendo" style="border:0px" />
            </a>
            <br />
            <img src="images/botao_acontecendo.gif" alt="acontecendo" />
        </td>
	</tr>
	<tr>
		<td width="227" height="198" id="menulateral">
              <asp:uc ID="mnlateral" runat="server" />
        </td>
	</tr>
	<tr>
		<td width="227" height="33"></td>
	</tr>
	<tr>
		<td width="227" height="170" id="menusplash">
             <img src="images//botao_ofertas.gif" alt="acontecendo" /><br />
               <asp:ucs ID="mnsplash" runat="server" /><br />
               <%--<asp:uct ID="twtbox" runat="server" _hashtag="HotelHeritage" _updatetime="20" />--%>
         </td>
	</tr>
	<tr>
		<td width="227" height="69"></td>
	</tr>
	<tr>
		<td colspan="2" class="rodape">
        <div style="position:absolute;top:30px;left:30px">
        <a href="https://twitter.com/#!/HotelHeritage " target="_blank" ><img src="images/icontwitter.jpg" border="0px" /></a>
        <a href="http://www.facebook.com/profile.php?id=100003298337679" target="_blank" ><img src="images/iconface.jpg" border="0px" /></a>
        </div>
        <div style="position:absolute;top:70px;left:20px;font-size:7pt;color:#666666" class="caixa">
        &copy;2012 | Todos os direitos reservados
        </div>
        <div style="position:absolute;float:right;top:70px;left:770px;font-size:7pt;color:#666666" class="caixa">
        By Flexsolution
        </div>
        <img src="images/rodape.jpg" />
        </td>
	</tr>
	<tr>
		<td width="863" height="12" colspan="2" style="background-color:#fff"></td>
	</tr>
</table>
</div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente.master" AutoEventWireup="true" CodeFile="home_heritage.aspx.cs" Inherits="home_heritage" %>
<%@ Register Src="~/controles/menu_cliente.ascx" TagName="umc" TagPrefix="asp" %>
<%@ Register Src="~/controles/menulateral.ascx" TagName="uc" TagPrefix="asp" %>
<%@ Register Src="~/controles/menu_splash_lateral.ascx" TagName="ucs" TagPrefix="asp" %>
<%@ Register Src="~/controles/uckeymenu.ascx" TagName="uck" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="html/css/heritage.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="main">
<table id="Table_01" width="887" border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td colspan="5">
			<img src="images/topo_heritage.jpg" width="887" />
        </td>
		<td>
         </td>
	</tr>
	<tr>
		<td colspan="5">
           <asp:umc ID="mntopo" runat="server" />
        </td>
		<td>
			<img src="images/spacer.gif" width="1" height="32" alt="" />
        </td>
	</tr>
	<tr>
		<td colspan="5">
			<img id="layout_aprovado_03" src="images/layout_aprovado_03.jpg" width="886" height="11" alt="" />
        </td>
		<td>
			<img src="images/spacer.gif" width="1" height="11" alt="" />
        </td>
	</tr>
	<tr>
		<td colspan="4">
			<img id="layout_aprovado_04" src="images/layout_aprovado_04.jpg" width="875" height="1" alt="" />
        </td>
		<td rowspan="10">
			<img id="layout_aprovado_05" src="images/layout_aprovado_05.jpg" width="11" height="638" alt="" />
        </td>
		<td>
			<img src="images/spacer.gif" width="1" height="1" alt="" /
        </td>
	</tr>
	<tr>
		<td rowspan="9">
			<img id="layout_aprovado_06" src="images/layout_aprovado_06.jpg" width="12" height="637" alt="" />
        </td>
		<td id="menulateral" >
			<asp:ucs ID="mnsplash" runat="server" />
        </td>
		<td colspan="2" rowspan="5">
			<img id="foto_home" src="images/foto_home.jpg" width="636" height="340" alt="" />
        </td>
		<td>
			<img src="images/spacer.gif" width="1" height="44" alt="" />
         </td>
	</tr>
	<tr>
		<td>
			<img id="menuLateral_acontecendo" src="images/menuLateral_acontecendo.jpg" width="227" height="27" alt="" />
        </td>
		<td>
			<img src="images/spacer.gif" width="1" height="27" alt="" />
        </td>
	</tr>
	<tr>
		<td runat="server" id="listahome">
            <asp:uc ID="mnlateral" runat="server" />
        </td>
		<td>
			<img src="images/spacer.gif" width="1" height="198" alt="" />
         </td>
	</tr>
	<tr>
		<td>
			<img id="menuLateral_ofertas" src="images/menuLateral_ofertas.jpg" width="227" height="33" alt="" />
        </td>
		<td>
			<img src="images/spacer.gif" width="1" height="33" alt="" /></td>
	</tr>
	<tr>
		<td rowspan="2">
			<img id="menuLateral_slpash" src="images/menuLateral_slpash.jpg" width="227" height="170" alt="" /></td>
		<td>
			<img src="images/spacer.gif" width="1" height="38" alt="" />
         </td>
	</tr>
	<tr>
		<td rowspan="2">
			<img id="texto_home" src="images/texto_home.jpg" width="445" height="201" alt="" />
        </td>
		<td rowspan="2" class="homeMenu">
			<asp:uck ID="mnhome" runat="server" keylocation="keyhome_" />
        </td>
		<td>
			<img src="images/spacer.gif" width="1" height="132" alt="" />
         </td>
	</tr>
	<tr>
		<td>
			<img id="layout_aprovado_15" src="images/layout_aprovado_15.jpg" width="227" height="69" alt="" />
        </td>
		<td>
			<img src="images/spacer.gif" width="1" height="69" alt="" />
         </td>
	</tr>
	<tr>
		<td colspan="3">
			<img src="images/rodape.jpg" />
        </td>
		<td>
			<img src="images/spacer.gif" width="1" height="84" alt="" />
         </td>
	</tr>
	<tr>
		<td colspan="3">
			<img id="layout_aprovado_17" src="images/layout_aprovado_17.jpg" width="863" height="12" alt="" />
        </td>
		<td>
			<img src="images/spacer.gif" width="1" height="12" alt="" />
        </td>
	</tr>
</table>
</div>
</asp:Content>


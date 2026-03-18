<%@ Page Title="" Language="C#" MasterPageFile="~/InnerCliente.master" AutoEventWireup="true" CodeFile="area.aspx.cs" Inherits="area" %>
<%@ Register Src="~/controles/ucareas.ascx" TagName="uck" TagPrefix="asp" %>
<%@ Register Src="~/controles/ucadrotator.ascx" TagName="ucr" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="html/css/heritage.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--<div class="areamain">

    <table id="Table_01" border="0" cellpadding="0" cellspacing="0" class="areamain">
	    <tr>
            <td class="areamenu" runat="server" id="menuarea">
                <asp:DataList ID="lstAreas" runat="server" RepeatDirection="Horizontal" CssClass="areamenu">
                   <ItemTemplate>
                     <a href="conteudo.aspx?AreaId=<%#Eval("AreaId") %>&local=menu"><%#Eval("Nome") %></a>
                   </ItemTemplate>
                </asp:DataList>
            </td>
		    <td class="areaFoto">
                <asp:Image ID="imgConteudo" runat="server" />
            </td>
	    </tr>
	    <tr>
		    <td class="areatexto" colspan="2" style="border:0px solid black;">
                <asp:Label ID="titulo" runat="server" Text=<%#Eval("titulo") %> CssClass="tituloConteudo" /><br />
                <asp:Label ID = "texto" runat="server" Text=<%#Eval("texto") %> />
            </td>
	    </tr>
    </table>
</div>--%>

<div id="conteudomain">
    <div id="conteudomenu" >
        <asp:DataList ID="lstAreas" runat="server" RepeatDirection="Horizontal" CssClass="areamenu" RepeatColumns="4">
            <ItemTemplate>
                <a href="conteudo.aspx?AreaId=<%#Eval("AreaId") %>&local=menu"><%#Eval("Nome") %></a>
            </ItemTemplate>
        </asp:DataList>
    </div>
    <div id="img">
        <asp:ucr ID="adrotator" runat="server" />
    </div>  
    <div id="conteudotexto">
        <h1 id="conteudotitulo">
            <asp:Label ID="titulo" runat="server" Text=<%#Eval("titulo") %> CssClass="tituloConteudo" />
        </h1>
        <asp:Label ID = "texto" runat="server" Text=<%#Eval("texto") %>  CssClass="conteudotexto" />
    </div>
</div>
</asp:Content>


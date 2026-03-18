<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente.master" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="home" %>
<%@ Register Src="~/controles/uckeymenu.ascx" TagName="uck" TagPrefix="asp" %>
<%@ Register Src="~/controles/ucadrotator.ascx" TagName="ucr" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="Stylesheet" type="text/css" href="html/css/heritage.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="homemain">

    <table id="Table_01" border="1" cellpadding="0" cellspacing="0" width="500px">
	    <tr>
		    <td class="homeFoto" colspan="2">
                <%--<asp:Image ID="imgConteudo" runat="server" />--%>
                <asp:ucr ID="adrotator" runat="server" />
            </td>
	    </tr>
	    <tr>
		    <td class="hometexto">
                <asp:DataList runat="server" ID="lstConteudo" CssClass="hometexto">
                    <ItemTemplate>
                        <asp:Label ID="titulo" runat="server" Text=<%#Eval("titulo") %> CssClass="tituloConteudo" /><br />
                        <asp:Label ID = "texto" runat="server" Text=<%#Eval("texto") %>  CssClass="conteudotexto"/>
                    </ItemTemplate>
                </asp:DataList>
            </td>
		    <td class="homeMenu">
			    <asp:uck ID="mnhome" runat="server" keylocation="keyhome_" />
            </td>
	    </tr>
    </table>
</div>
</asp:Content>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="menu_splash_lateral.ascx.cs" Inherits="controles_menu_splash_lateral" %>
<asp:DataList ID="lstMenuSplash" runat="server">
    <ItemTemplate>
       <a href=<%#Eval("Url") %>?areaid=<%#Eval("AreaId") %> target="frconteudo"><img src="images/<%#Eval("UrlImagem")%>" /></a>
    </ItemTemplate>
</asp:DataList>
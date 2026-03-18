<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucareas.ascx.cs" Inherits="controles_ucareas" %>
<asp:DataList ID="lstAreas" runat="server" RepeatDirection="Vertical"  CssClass="areamenu" RepeatColumns="4">
   <ItemTemplate>
     <a href="conteudo.aspx?AreaId=<%#Eval("AreaId") %>&local=menu"><%#Eval("Nome") %></a>
   </ItemTemplate>
</asp:DataList>
    
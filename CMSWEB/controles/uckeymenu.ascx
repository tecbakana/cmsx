<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uckeymenu.ascx.cs" Inherits="controles_uckeymenu" %>
<asp:DataList ID="lstMenuLateral" runat="server" CssClass="homeMenu">
    <ItemTemplate>
       <a href=<%#Eval("Url") %>?areaid=<%#Eval("AreaId") %> target="frconteudo">::<%#Eval("Descricao") + "..."%></a>
    </ItemTemplate>
    <SeparatorTemplate>
       <hr class="hrBlue" />
    </SeparatorTemplate>
</asp:DataList>
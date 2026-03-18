<%@ Control Language="C#" AutoEventWireup="true" CodeFile="menulateral.ascx.cs" Inherits="controles_menulateral" %>
<asp:DataList ID="lstMenuLateral" runat="server" CssClass="menulateral">
    <ItemTemplate>
       <a href=<%#Eval("Url") %>?areaid=<%#Eval("AreaId") %> target="frconteudo">::<%#Eval("Descricao") + "..."%></a>
    </ItemTemplate>
    <SeparatorTemplate>
       <hr class="hrBlue" />
    </SeparatorTemplate>
</asp:DataList>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucproduto.ascx.cs" Inherits="controles_produto" %>
<asp:Repeater ID="rptProdutos"  runat="server">
    <HeaderTemplate>
        <ul class="list-group" style="width:300px">
    </HeaderTemplate>
    <ItemTemplate>
        <li class="list-group-item">
            <asp:LinkButton runat="server" CommandArgument='<%#Eval("Url") + "," + Eval("AreaId")%>' CommandName="goLocation" ID="lkbGoLocation" Text='<%#Eval("Nome") %>' />
<%--            <a href=<%#Eval("Url") %>?areaid=<%#Eval("AreaId") %> target="frconteudo"><%#Eval("Nome") %></a>--%>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>

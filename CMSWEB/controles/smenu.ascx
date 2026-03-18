<%@ Control Language="C#" AutoEventWireup="true" CodeFile="smenu.ascx.cs" Inherits="controles_smenu" %>
<ul class="nav  navbar-nav" runat="server">
    <asp:ListView ID="ltMenu" runat="server" OnItemDataBound="ltMenu_ItemDataBound" >
        <ItemTemplate>
            <li>
        <%--        <asp:LinkButton runat="server" ClientIDMode="Static" ID="lstItemMenu" OnClick="lstItemMenu_Click" CommandArgument='<%#Eval("Url + AreaId") %>'>
                    <%#Eval("Nome") %>
                </asp:LinkButton>--%>
                <<a href='<%#Eval("Url") %>?areaid=<%#Eval("AreaId") %>' target="frconteudo"><%#Eval("Nome") %></a>
                <asp:ListView ID="ltMenuDrop" runat="server">
                </asp:ListView>
            </li>
        </ItemTemplate>
    </asp:ListView>
</ul>
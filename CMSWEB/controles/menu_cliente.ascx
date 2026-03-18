<%@ Control Language="C#" AutoEventWireup="true" CodeFile="menu_cliente.ascx.cs" Inherits="controles_menu_cliente" %>
<%--<asp:DataList runat="server" ID="lstMenus" RepeatDirection="Horizontal" >
    <ItemTemplate>
        <a href=<%#Eval("Url") %>?areaid=<%#Eval("AreaId") %> target="frconteudo"><%#Eval("Nome") %></a>
    </ItemTemplate>
</asp:DataList>--%>
<div class="menu">
<ul style="display:inline">
<asp:ListView ID="lstMenu" runat="server">
   <ItemTemplate>
        <li><a style="width:<%#Eval("Nome").ToString().Length*7.6 %>px" href=<%#Eval("Url") %>?areaid=<%#Eval("AreaId") %> target="frconteudo"><%#Eval("Nome") %></a>
           <ul>
                <asp:ListView ID="lstSubMenu" runat="server">
                    <ItemTemplate>
                        <li>
                            <asp:LinkButton runat="server" CommandArgument='<%#Eval("Url") + "," + Eval("AreaId")%>' CommandName="goLocation" ID="lkbGoLocation" Text='<%#Eval("Nome") %>' />
                            <%--<a style="top:6px" href=<%#Eval("Url") %>?areaid=<%#Eval("AreaId") %> target="frconteudo"><%#Eval("Nome") %></a>--%>
                        </li>
                    </ItemTemplate>
                </asp:ListView>
            </ul>
         </li>
   </ItemTemplate>
</asp:ListView>
</ul>
</div>
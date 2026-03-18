<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bstrap_menu_topo.ascx.cs" Inherits="controles_bstrap_menu_topo" %>
<div class="container">
    <div class="navbar-header">
        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
        </button>
        <a class="navbar-brand" href="#"><asp:Literal ID="ltTitulo" runat="server" /></a>
    </div>
    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
        <ul class="nav  navbar-nav">
            <asp:ListView ID="ltMenu" runat="server" OnItemDataBound="ltMenu_ItemDataBound" >
                <ItemTemplate>
                    <li>
                        <a href='<%#Eval("Url") %>?areaid=<%#Eval("AreaId") %>' target="_self"><%#Eval("Nome") %></a>
                        <asp:ListView ID="ltMenuDrop" runat="server">
                        </asp:ListView>
                    </li>
                </ItemTemplate>
            </asp:ListView>
        </ul>
    </div>
</div>
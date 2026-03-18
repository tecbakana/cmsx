<%@ Control Language="C#" AutoEventWireup="true" CodeFile="menu.ascx.cs" Inherits="controles_menu" %>
    <%-- id="pnlMenu" runat="server" Visible="false"--%>
    <div class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="navbar-header">
            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            </button>
            <span class="navbar-brand" href="#">
                <img src="images/text4076cmsx.png" class="image-responsive" style="max-width:50px" />
            </span>
        </div>
        <div class="collapse navbar-collapse">
            <ul class="nav navbar-nav">
            <asp:DataList runat="server" ID="lstmenu" 
                RepeatDirection="Horizontal" 
                OnItemDataBound="lstmenu_ItemDataBound"
                OnItemCommand="lstmenu_ItemCommand"
                OnSelectedIndexChanged="lstmenu_SelectedIndexChanged" >
                <ItemTemplate>
                    <li><a href="<%# Eval("Url") %>" class="navbar-brand" onclick=setTitle('<%# Eval("Nome") %>');><%# Eval("Nome") %></a></li>
                </ItemTemplate>
            </asp:DataList>
            </ul>
            <asp:Button ID="btExit" CssClass="btn btn-primary btn-sm" runat="server" Text="Sair" />
        </div><!--/.nav-collapse -->
        <asp:Label ID="lbltitulo" runat="server" class="lbltitulo" ClientIDMode="Static" Visible="false" />
    </div>
<br />
<br />
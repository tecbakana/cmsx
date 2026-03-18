<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uccarousel.ascx.cs" Inherits="controles_uccarousel" %>
<asp:Panel ID="pnlCarousel" runat="server" Visible="true">
    <div id="myCarousel" class="carousel slide">
        <div class="carousel-inner">
            <asp:Repeater runat="server" ID="rpCarousel" >
            <%--<asp:DataList runat="server" ID="dlCarousel" RepeatLayout="Flow">--%>
                <ItemTemplate>
                    <div class="item active">
                        <img src="images/<%#Eval("UrlImagem") %>" alt="">
                        <div class="container">
                            <div class="carousel-caption">
                                <h1></h1>
                                <p class="lead">
                                    <%#Eval("Descricao") %>
                                </p>
                                <a class="btn btn-large btn-primary" href=''>
                                    ver mais[+]...
                                </a>
                            </div>
                        </div>
                    </div>                
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <div class="item">
                        <img src="images/<%#Eval("UrlImagem") %>" alt="">
                        <div class="container">
                            <div class="carousel-caption">
                                <h1></h1>
                                <p class="lead">
                                    <%#Eval("Descricao") %>
                                </p>
                                <a class="btn btn-large btn-primary" href=''>
                                    ver mais[+]...
                                </a>
                            </div>
                        </div>
                    </div> 
                </AlternatingItemTemplate>
            <%--</asp:DataList>--%>
            </asp:Repeater>
        </div>
    </div>
</asp:Panel>

<%@ Page Title="" Language="C#" MasterPageFile="~/bootstrap.master" AutoEventWireup="true" CodeFile="home_comvil.aspx.cs" Inherits="home_comvil" %>
<%@ Register Src="~/controles/bstrap_menu_topo.ascx" TagName="menuTopo" TagPrefix="asp" %>
<%@ Register Src="~/controles/menulateral.ascx" TagName="uc" TagPrefix="asp" %>
<%@ Register Src="~/controles/menu_splash_lateral.ascx" TagName="ucs" TagPrefix="asp" %>
<%@ Register Src="~/controles/uckeymenu.ascx" TagName="uck" TagPrefix="asp" %>
<%@ Register Src="~/controles/uccarousel.ascx" TagName="ucr" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    $(document).ready(function () {
        $('#myCarousel').carousel();
        alert("running");
    });
    </script>
    <div class="col-md-12">
        <div class="span12 pagination-centered">
            <div class="row">
                <div class="col-md-12 col-md-offset-1" >
                    <img src="images/logo_comvil.jpg" style="width:900px;" />
                </div>
            </div>
            <div class="row">
                <div style="background-color:#fff;width:900px;height:2px">&nbsp;</div>
            </div>
            <div class="row">
                    <div  class="col-md-12 col-md-offset-1">
                        <div class="navbar navbar-inverse" style="width:900px;border-radius: 0 !important;-moz-border-radius: 0 !important;background-color:transparent !important;">
                            <div class="navbar-inner">
                            <div class="container">
                                <div class="navbar-header">
                                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                                    <span class="sr-only">Toggle navigation</span>
                                    <span class="icon-bar"></span>
                                    <span class="icon-bar"></span>
                                    <span class="icon-bar"></span>
                                    </button>
                                </div>
                                <div class="navbar-collapse collapse navbar-right">
                                        <asp:menuTopo ID="mnTopo" runat="server" />
                                </div>
                            </div>
                            </div>
                        </div>
                    </div>
            </div>
            <div>
                <asp:ucr ID="ucCarousel" runat="server" />
            </div>
            <div class="row">
                <div  class="col-md-12 col-md-offset-1">
                    <iframe id="frconteudo" name="frconteudo" class="" width="900" height="400" frameborder="0" >

                    </iframe>
                </div>
        </div>
        </div>
     </div>
</asp:Content>


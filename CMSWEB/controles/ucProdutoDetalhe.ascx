 <%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucProdutoDetalhe.ascx.cs" Inherits="controles_ucProdutoDetalhe" %>
<asp:UpdatePanel ID="updProduto" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="row">
            <div class="col-sm-9">
		        <div class="col-md-3 hidden-sm hidden-xs" id="slider-thumbs">
				    <!-- thumb navigation carousel items -->
                    <asp:Repeater runat="server" ID="dlProdutoImagem" OnItemDataBound="dlProdutoImagem_ItemDataBound">
                        <HeaderTemplate><ul class="list-inline"></HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Thumb").ToString().Replace("<span>","")%>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Thumb").ToString().Replace("<span>","") %>
                        </AlternatingItemTemplate>
                        <FooterTemplate></ul></FooterTemplate>
                    </asp:Repeater>
			    </div>
		
			    <div class="col-md-9" id="slider">
				    <div id="myCarousel" class="carousel slide">
				    <!-- main slider carousel items -->
				    <div class="carousel-inner">
                        <asp:Repeater runat="server" ID="rptDivCarrousel">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "InnerSlider")%>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "InnerSlider") %>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
				    </div>
				    <!-- main slider carousel nav controls --> 
				    <a class="carousel-control left hidden-md hidden-lg" href="#myCarousel" data-slide="prev">‹</a>
				    <a class="carousel-control right hidden-md hidden-lg" href="#myCarousel" data-slide="next">›</a>
				    </div>
			    </div>
            </div>  
        </div>
        <div class="row">
            <div class="col-sm-9">
            <h3><asp:Literal runat="server" ID="ltTitulo" ClientIDMode="Static" /></h3>
                <asp:HiddenField runat="server" ID="hdnProdId" />
                <div class="row">
                    <div class="col-xs-8 col-sm-8">
                        <ul class="nav nav-pills" role="tablist" id="tabProduto">
                            <li class="active"><a href="#descricao" role="tab" data-toggle="tab"><span class="glyphicon glyphicon-gift"></span>&nbsp;Descricao</a></li>
                            <li><a href="#detalhe" role="tab" data-toggle="tab"><span class="glyphicon glyphicon-wrench"></span>&nbsp;Informacoes tecnicas</a></li>
                        </ul>

                        <div class="tab-content">
                            <div class="tab-pane active" id="descricao">
                                <asp:Literal runat="server" ID="ltDescricao" ClientIDMode="Static" />
                            </div>
                            <div class="tab-pane" id="detalhe">
                                <asp:Literal runat="server" ID="ltTecnico" ClientIDMode="Static" />
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-4 col-sm-4">
                        <img src="images/text2987.png" data-toggle="modal" data-target="#myModal" data-remote='<%#Session["paypath"] %>'>
                    </div>
                </div>
            </div>
        </div>
        <script type='text/javascript'>
        
            $(document).ready(function () {
                $('#myCarousel').carousel({
                    interval: 4000
                });

                // handles the carousel thumbnails
                $('[id^=carousel-selector-]').click(function () {
                    var id_selector = $(this).attr("id");
                    var id = id_selector.substr(id_selector.length - 1);
                    id = parseInt(id);
                    $('#myCarousel').carousel(id);
                    $('[id^=carousel-selector-]').removeClass('selected');
                    $(this).addClass('selected');
                });

                // when the carousel slides, auto update
                $('#myCarousel').on('slid', function (e) {
                    var id = $('.item.active').data('slide-number');
                    id = parseInt(id);
                    $('[id^=carousel-selector-]').removeClass('selected');
                    $('[id^=carousel-selector-' + id + ']').addClass('selected');
                });
            });
        </script>
    </ContentTemplate>
</asp:UpdatePanel>


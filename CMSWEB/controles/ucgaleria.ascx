<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucgaleria.ascx.cs" Inherits="controles_ucgaleria" %>
<div class="row">
    <div class="col-sm-9">
		<div class="col-md-3 id="slider-thumbs">
		    <!-- thumb navigation carousel items -->
            <asp:Repeater runat="server" ID="dlProdutoImagem" >
                <HeaderTemplate><ul class="list-inline"></HeaderTemplate>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "Thumb") %>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "Thumb") %>
                </AlternatingItemTemplate>
                <FooterTemplate></ul></FooterTemplate>
            </asp:Repeater>
	    </div>
	    <div class="col-md-9" id="slider">
		    <div id="myCarousel" class="carousel slide" style="max-width:400px">
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
		        <!-- main slider carousel nav controls
		        <a class="carousel-control left hidden-xs hidden-sm hidden-md hidden-lg" href="#myCarousel" data-slide="prev">‹</a>
		        <a class="carousel-control right hidden-xs hidden-sm hidden-md hidden-lg" href="#myCarousel" data-slide="next">›</a> --> 
		    </div>
	    </div>
    </div>
</div>  
<script type="text/javascript">
    function setImage(obj)
    {
        var data = obj.split(";");
        var win = this.window.parent;
        hid = win.document.getElementById("hdnImages");
        hid.value = data[0];

        img = win.document.getElementById("preview");
        img.src = data[1];

        alert("A imagem foi escolhida, verifique no painel ao lado.");
    }

        
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
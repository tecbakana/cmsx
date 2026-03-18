<%@ Page Title="" Language="C#" MasterPageFile="~/CommerceMaster.master" AutoEventWireup="true" CodeFile="callWebApi.aspx.cs" Inherits="callWebApi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplMaster" Runat="Server">
<div id="products" class="row">

</div>
<script type="text/javascript">
    function getProducts() {
        $.getJSON("http://localhost/webapi/api/Default1/",
            function (data) {
                $('#products').empty(); // Clear the table body.

                //Loop through the list of products.
                $.each(data, function (key, val) {
                    // Add a table row for the product.
                    var row = '<div class="col-sm-3">' + val.Nome + '</div><div class="col-sm-3">' + val.Valor + '</div><div class="col-sm-3">' + val.Descricao + '</div>';
                    $('<tr/>', { html: row })  // Append the name.
                        .appendTo($('#products'));
                });
            });
    }

    $(document).ready(getProducts);
</script>
</asp:Content>


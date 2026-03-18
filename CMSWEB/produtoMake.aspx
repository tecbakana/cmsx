<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="produtoMake.aspx.cs" Inherits="produtoMake"  ValidateRequest="false" %>
<%@ Register Src="~/controles/menu.ascx" TagName="uc" TagPrefix="asp" %>
<%@ Register Src="~/controles/ucescolheimagem.ascx" TagName="uci" TagPrefix="asp" %>
<%@ Register Src="~/controles/ucMakeProduto.ascx" TagPrefix="asp" TagName="ucMakeProduto" %>
<%@ Register Src="~/controles/ucAtributos_opcao.ascx" TagPrefix="asp" TagName="ucAtributos_opcao" %>
<%@ Register Src="~/controles/ucGaleriaMake.ascx" TagPrefix="asp" TagName="ucGaleriaMake" %>
<%@ Register Src="~/controles/ucListaProduto.ascx" TagPrefix="asp" TagName="ucListaProduto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="menu" ContentPlaceHolderID="plcManager" runat="server">
    <asp:uc ID="ucMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" > 
    <script type="text/javascript">
        $(document).ready(function () {
            $('.summernote').summernote({
                height: "400px",
                onImageUpload: function (files, editor, welEditable) {
                    sendFile(files[0], editor, welEditable);
                }
            });
        });

        function sendFile(file, editor, welEditable)
        {
            data = new FormData();
            data.append("file", file);
            $.ajax({
                    data: data,
                    type: "POST",
                    url: "savefile.ashx?id=<%=((dynamic)(Session["Objeto"])).aplicacaoid.ToString()%>",
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (url) {
                        editor.insertImage(welEditable, url);
                    }
            });
        }

        function setContent()
        {
            var sHTML = $('.summernote').eq(0).code();
            document.getElementById("hd").value = sHTML;
            var sHTML = $('.summernote').eq(1).code();
            document.getElementById("hs").value = sHTML;
        }
        

        function getContent()
        {
            $('.summernote').code("teste");
        }
    </script>
            <div class="panel panel-default" >
                <div class="panel-heading">
                    <h1>LOJA</h1>
                    <p>Cadastro de Produtos</p>
                </div>
                <div class="panel-body">
                <div class="container-fluid" id="masterd">
                    <div class="row-fluid"> 
                        <ul class="nav nav-pills" role="tablist" id="tabProduto">
                          <li class="active"><a href="#criar" role="tab" data-toggle="tab"><span class="glyphicon glyphicon-wrench"></span>&nbsp;Criar novo produto</a></li>
                          <li><a href="#atributos" role="tab" data-toggle="tab"><span class="glyphicon glyphicon-tasks"></span>&nbsp;Atributos/Opcoes</a></li>
                          <li><a href="#images" role="tab" data-toggle="tab"><span class="glyphicon glyphicon-picture"></span>&nbsp;Imagens</a></li>
                          <li><a href="#lista" role="tab" data-toggle="tab"><span class="glyphicon glyphicon-gift"></span>&nbsp;Lista de Produtos</a></li>
                        </ul>

                        <div class="tab-content">
                          <div class="tab-pane active" id="criar">
                               <asp:ucMakeProduto runat="server" ID="ucMakeProduto" />
                          </div>
                          <div class="tab-pane" id="atributos">
                              <asp:ucAtributos_opcao runat="server" ID="ucAtributos_opcao" />
                          </div>
                          <div class="tab-pane" id="images">
                              <asp:ucGaleriaMake runat="server" ID="ucGaleriaMake" tipo="1" />
                          </div>
                          <div class="tab-pane" id="lista">
                               <asp:ucListaProduto runat="server" id="ucListaProduto" />
                          </div>
                        </div>
                    </div>
                </div>
                </div>
                <div class="panel-footer">
                    flamet <span class="fa-copy"></span>2014
                </div>
            </div>
   <script>

       function callTab(tabId)
       {
           $('#tabProduto a[href="#' + tabId + '"]').tab('show');
           var aTag = $("#masterd");
           $('html,body').animate({ scrollTop: aTag.offset().top-150 }, 'slow');
       }

   </script>

</asp:Content>


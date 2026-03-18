<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="teste_place.aspx.cs" Inherits="teste_place" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="menu" ContentPlaceHolderID="plcManager" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" > 
    <asp:TextBox ID="txtTest" runat="server" TextMode="MultiLine"></asp:TextBox>
        <ul class="nav nav-pills" role="tablist" id="tabDescritivo">
                    <li class="active"><a href="#description" role="tab" data-toggle="tab"><span class="glyphicon glyphicon-pencil"></span>&nbsp;Descricao</a></li>
                    <li class=""><a href="#techdata" role="tab" data-toggle="tab"><span class="glyphicon glyphicon-th"></span>&nbsp;Dados tecnicos</a></li>
                </ul>
    <div class="tab-content">
                    <div class="tab-pane active" id="description">
                        <textarea id="txtDescricao" runat="server" name="txtDescricao" placeholder="Descreva o produto/artigo/item"></textarea>
                    <div class="summernote-container">
                        <div class="summernote" id="hde"></div>
                        <asp:HiddenField ID="hd" runat="server" />
                    </div>
                    </div>
                    <div class="tab-pane" id="techdata">
                        <div class="summernote-container">
                            <div class="summernote" id="hse"></div>
                            <asp:HiddenField ID="hs" runat="server" />
                        </div>
                    </div>
                </div>

    <script type="text/javascript">
        $(document).ready(function () {
            alert($('.summernote').length);
            $('.summernote').summernote({
                height: "400px",
                onImageUpload: function (files, editor, welEditable) {
                    sendFile(files[0], editor, welEditable);
                }
            });
        });

        //$(document).ready(function () {
        //    var editors = $('.summernote');
        //    for (var i = 0; i <= editors.length; i++) {
        //        alert($('.summernote')[i]);
        //        editors[i].summernote({
        //            height: "400px"/*,
        //            onImageUpload: function (files, editor, welEditable) {
        //                if (files.length >= 1) {
        //                    sendFile(files[0], editor, welEditable);
        //                }
        //            }*/
        //        });
        //    }

        //});

        function sendFile(file, editor, welEditable) {
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

            function setContent() {
                var editors = $('.summernote');
                for (var i = 0; i <= editors.length - 1; i++) {
                    //var content = $('.summernote').code();

                    var destino = (i == 0 ? "hdesc" : "hset");
                    document.getElementById(destino).value = editors[i].code();
                    editors[i].code();
                }
            }

            function getContent() {
                $('.summernote').code("teste");
            }
    </script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/bootstrap_client.master" AutoEventWireup="true" CodeFile="pagebuilder.aspx.cs" Inherits="conteudomake_new" ValidateRequest="false" %>
<%@ Register Src="~/controles/ucescolheimagem.ascx" TagName="uci" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
<script type="text/javascript">
    $(document).ready(function () {
        $('.summernote').summernote({
            height: "300px",
            onImageUpload: function (files, editor, welEditable) {
                sendFile(files[0], editor, welEditable);
            }
        });
    });

    function sendFile(file,editor,welEditable)
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
            success: function(url) {
                editor.insertImage(welEditable, url);
            }
        });
    }
    
    function setContent() {
            debugger;
            var content = $('.summernote').code();
            //var imgs = $('.summernote').find('img');
            //var ln = imgs.length;

            //for (i = 0; i < ln; i++) {
            //    document.getElementById("Response.Write(hdnImages.ClientID); %>").value = imgs[i].src + "|";
            //        $('.summernote').find('img')[i].src = "codehere" + i;
            //    }

            document.getElementById("<%Response.Write(ha.ClientID); %>").value = $('.summernote').code();
            $(".summernote").code("");
        }

        function getContent() {
            $('.summernote').code("teste");
        }


    </script>
    <div id="noVisible"></div>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="col-md-4">
                <asp:UpdatePanel ID="updListas" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                    </ContentTemplate>
                </asp:UpdatePanel>
                        <asp:ListBox runat="server" ID="lstAreas" Rows="1"  AutoPostBack="true" Width="200" CssClass="form-control" />
                        <asp:Panel ID="pnlFilhas" runat="server" CssClass="well well-lg" Width="200" Visible="false">
                            <h2 style="font-size:58px;margin-top:0px"><small>Área Secundária</small></h2>
                            <asp:ListBox ID="lstAreasFilha" runat="server" Rows="1"  AutoPostBack="true" CssClass="form-control" Visible="true" />
                        </asp:Panel>
                        <hr />
                        <asp:Label ID="lbltitulo" runat="server" Text="Titulo" /><br />
                        <asp:TextBox ID="txtTitulo" CssClass="form-control" runat="server" Width="230" /><br />
                        <asp:Label ID="lblDescricao" runat="server" Text="Descricao" /><br />
                        <asp:TextBox ID="txtDescricao" CssClass="form-control" runat="server" Width="230" /><br /><br />
                        <asp:CheckBox ID="ckbimagem" runat="server" Text="Permitir vínculo a alguma imagem?" AutoPostBack="true" OnCheckedChanged="HabilitaImagem" />
                        <br />
                        <asp:Panel ID="pnlimagem" runat="server" CssClass="well well-lg" Width="200" Visible="false">
                            <asp:HiddenField ID="imgurl" runat="server" ClientIDMode="Static" />
                            <asp:uci ID="ucimage" runat="server" acao="2" /> 
                            <br />   
                        </asp:Panel>
                        <asp:Panel ID="pnlgaleria" runat="server" CssClass="well well-lg" Width="200" Visible="false">
                            Olha, essa área que você escolheu já possui uma galeria e você pode utiliza-la no conteúdo que vai cadastrar, marcando a caixa abaixo.
                            <br />
                            <asp:CheckBox ID="ckbgaleria" runat="server" Text="&nbsp;Anexar galeria." Enabled="false" AutoPostBack="true" OnCheckedChanged="HabilitaGaleria" />
                        </asp:Panel><br />
            </div>

            <div class="col-md-8">       
                <div class="summernote-container">
                    <div class="summernote"><%Response.Write(Session["texto"]); %></div>
                </div>
                <asp:HiddenField ID="ha" runat="server" />
                <asp:HiddenField ID="hdnImages" runat="server" />
                <asp:HiddenField ID="hdnConteudoId" runat="server" />
                <asp:HiddenField ID="hdnAreaId" runat="server" />
                <asp:Button runat="server" ID="btnSalvar" CssClass="btn btn-primary" Text="Salvar" OnClick="btnSalvar_Click" OnClientClick="setContent()" />
                <asp:Button runat="server" ID="btnEditar" CssClass="btn btn-warning" Text="Editar" OnClick="btnEditar_Click" OnClientClick="setContent()"  />
            </div>
        </div>
        <div class="row-fluid">
            <div class="col-lg-10">
                <asp:UpdatePanel ID="pnlConteudo" runat="server" UpdateMode="Always" >
                    <ContentTemplate>
                    </ContentTemplate>
                </asp:UpdatePanel>
                        <div class="col-md-5">
                            <h1 style="font-size:58px"><small>Conteudo - Principal</small></h1>
                            <asp:GridView ID="dlconteudo" 
                                runat="server" 
                                OnRowCommand="dlConteudo_RowCommand" 
                                AutoGenerateColumns="false" 
                                CssClass="table table-hover" EnableViewState="true">
                            <Columns>
                                <asp:BoundField HeaderText="Titulo" DataField="Titulo" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnedicao" CssClass="btn btn-primary btn-sm" runat="server" Text="editar" CommandName="EditarConteudo" CommandArgument=<%#Eval("ConteudoId") %> OnClientClick="getContent" />
                                        <asp:Button ID="btnstatus" CssClass="btn btn-danger btn-sm" runat="server" Text="&times;" CommandName="InativarConteudo" CommandArgument=<%#Eval("ConteudoId") %> />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            </asp:GridView>         
                            <hr />
                            <h1 style="font-size:58px"><small>Conteudo - Secundário</small></h1>
                            <asp:GridView ID="dlConteudoSec" EnableViewState="true" runat="server" OnRowCommand="dlConteudo_RowCommand" AutoGenerateColumns="false" CssClass="table table-hover">
                            <Columns>
                                <asp:BoundField HeaderText="Titulo" DataField="Titulo" />
                               <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnedicao" CssClass="btn btn-primary btn-sm" runat="server" Text="editar" CommandName="EditarConteudo" CommandArgument=<%#Eval("ConteudoId") %> />
                                        <asp:Button ID="btnstatus" CssClass="btn btn-danger btn-sm" runat="server" Text="&times;" CommandName="InativarConteudo" CommandArgument=<%#Eval("ConteudoId") %> />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ButtonField ControlStyle-CssClass="btn btn-primary btn-sm" Text="Editar" CommandName="EditarConteudo" ButtonType="Button" />

                            </Columns>
                            </asp:GridView>                  
                        </div><!--  lado lista -->
            </div>
        </div>
    </div>

</asp:Content>


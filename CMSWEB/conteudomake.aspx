<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="conteudomake.aspx.cs" Inherits="conteudomake_new" ValidateRequest="false" %>
<%@ Register Src="~/controles/menu.ascx" TagName="uc" TagPrefix="asp" %>
<%@ Register Src="~/controles/ucgaleria.ascx" TagName="ucg" TagPrefix="asp" %>
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
            success: function (url) {
                //url = "../../cms/" + url;
                
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
                <div class="panel panel-primary">
                <div class="panel-heading">
                <h3 class="panel-title">Edi&ccedil;&atilde;o de Conte&uacute;do</h3>
                </div>
                <div class="panel-body">
            <div class="col-md-4">
                        <asp:UpdatePanel ID="updListas" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%--<asp:Label ID="Categorias" runat="server" Width="400" >Categoria principal</asp:Label><br />
                        <asp:ListBox ID="lstCategorias" AutoPostBack="true" CssClass="form-control" runat="server" DataTextField="Nome" DataValueField="CategoriaId" Rows="1" Width="200" OnSelectedIndexChanged="lstCategorias_SelectedIndexChanged" >
                            <asp:ListItem Text="Escolha a Categoria principal" />
                        </asp:ListBox><br />
                        <asp:Panel ID="pnlCat" runat="server" CssClass="well well-lg" Width="230" Visible="false">
                            <h2 style="font-size:58px;margin-top:0px"><small>SubCategoria</small></h2>
                            <asp:ListBox ID="lstSubCategorias" CssClass="form-control" AutoPostBack="true" runat="server" DataTextField="Nome" DataValueField="CategoriaId" Rows="1" Width="200" >
                                <asp:ListItem Text="Escolha a subcategoria" />
                            </asp:ListBox><br />
                        </asp:Panel>--%>
                        <asp:Panel runat="server" CssClass="panel panel-primary" ID="pnlCategoria" ClientIDMode="Static">
                            <div class="panel-heading">Utilize este campo para criar rapidamente uma Categoria</div>
                            <div class="panel-body">
                                <div class="input-group input-group-sm">
                                <input type="text" id="txtNomeCategoria" runat="server" placeholder="Nome da categoria" class="form-control" />
                                <span class="input-group-btn">
                                    <asp:LinkButton ID="btnAddCategoria" 
                                                runat="server" 
                                                CssClass="btn btn-info btn-xs"    
                                                CommandName="adCat" 
                                                OnClick="AddCategoria" >
                                            <i aria-hidden="true" class="glyphicon glyphicon-plus" style="color:#ffffff"></i>
                                    </asp:LinkButton>
                                </span>
                                </div>
                                <br />
                                <asp:ListBox ID="lstCategorias" OnSelectedIndexChanged="lstCategorias_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server" DataTextField="Nome" DataValueField="CategoriaId" Rows="1" Width="200" >
                                    <asp:ListItem Text="Escolha a Categoria principal" />
                                </asp:ListBox>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" CssClass="panel panel-primary" ID="pnlPagina" ClientIDMode="Static">
                            <div class="panel-heading">Utilize este campo para criar rapidamente uma página, e depois voce pode utilizar o menu "Páginas" para administra-la</div>
                            <div class="panel-body">
                                <div class="input-group input-group-sm">
                                    <input type="text" id="txtNomeArea" runat="server" placeholder="Nome da Página" class="form-control input-large" />
                                    <span class="input-group-btn">
                                        <asp:LinkButton ID="btnAddArea" 
                                                runat="server" 
                                                CssClass="btn btn-info btn-xs"    
                                                CommandName="adArea" 
                                                OnClick="btnAddArea_Click">
                                            <i aria-hidden="true" class="glyphicon glyphicon-plus" style="color:#ffffff"></i>
                                        </asp:LinkButton>
                                    </span>
                                </div>
                                <br />
                                <asp:ListBox runat="server" ID="lstAreas" Rows="1"  AutoPostBack="true" Width="200" CssClass="form-control" />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlFilhas" runat="server" CssClass="well well-lg" Width="200" Visible="false">
                            <h2 style="font-size:58px;margin-top:0px"><small>Área Secundária</small></h2>
                            <asp:ListBox ID="lstAreasFilha" runat="server" Rows="1"  AutoPostBack="true" CssClass="form-control" Visible="false" />
                        </asp:Panel>
                        <hr />
                        <asp:Label ID="lbltitulo" runat="server" Text="Titulo" />
                        <asp:TextBox ID="Titulo" CssClass="form-control" runat="server" Width="200" /><br />
                        <asp:Label ID="lblAutor" runat="server" Text="Autor" />
                        <asp:TextBox ID="Autor" CssClass="form-control" runat="server" Width="200" /><br /><br />
                        <asp:Panel ID="pnlgaleria" runat="server" CssClass="well well-lg" Width="200" Visible="false">
                            Olha, essa área que você escolheu já possui uma galeria e você pode utiliza-la no conteúdo que vai cadastrar, marcando a caixa abaixo.
                            <br />
                            <asp:CheckBox ID="ckbgaleria" runat="server" Text="&nbsp;Anexar galeria." Enabled="false" AutoPostBack="true" OnCheckedChanged="HabilitaGaleria" />
                        </asp:Panel><br />
            </div>
            <div class="col-md-8">  
                <div class="row">
                    <div class="col-md-12">
                       <asp:CheckBox ID="ckbimagem" runat="server" Text="Permitir vínculo a alguma imagem?" Visible="false" AutoPostBack="true" OnCheckedChanged="HabilitaImagem" />
                        <br />
                        <asp:Panel ID="pnlimagem" runat="server" CssClass="well well-lg" Width="200" Visible="false">
                            <asp:HiddenField ID="imgurl" runat="server" ClientIDMode="Static" />
                            <img src="" class="img-responsive" style="width:80px" id="preview" />
                            <br />   
                        </asp:Panel>
                    </div>
                </div>     
                <div class="row">
                    <div class="col-md-12">
                        <div class="summernote-container">
                            <div class="summernote"><%Response.Write(Session["texto"]); %></div>
                        </div>
                        <asp:HiddenField ID="ha" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hdnImages" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hdnConteudoId" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hdnAreaId" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hdnCategoriaId" runat="server" ClientIDMode="Static" />
                        <asp:Button runat="server" ID="btnSalvar" CssClass="btn btn-primary" Text="Salvar" OnClick="btnSalvar_Click" OnClientClick="setContent()" />
                        <asp:Button runat="server" ID="btnEditar" CssClass="btn btn-warning" Text="Editar" OnClick="btnEditar_Click" OnClientClick="setContent()"  />
                    </div>
                </div>
            </div>
                </div>
                <div class="panel-footer">Panel footer</div>
                </div>
        </div>
        <div class="row-fluid">
            <div class="col-lg-12">
                <h1 style="font-size:58px"><small>Conteudos do site</small></h1>
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
            </div>
        </div>
    </div>
    <asp:Button ID="btn1" runat="server"  />
              <asp:Panel ID="ModalPanel" runat="server" Width="700px" Height="500px" >
              
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                        <button type="button" id="OKButton" class="close" data-dismiss="modal" aria-hidden="true">fechar</button>
                        <h4 class="modal-title" id="myModalLabel">Incluir imagem</h4>
                        </div>
                        <div class="modal-body">
                            <%--<iframe src="galeria.aspx" style="width:400px;height:500px;border:0px" ></iframe>--%>
                            <asp:ucg ID="glr" runat="server" acao="2"/>
                        </div>
                    </div>
                </div>
              </asp:Panel>   
              <ajx:ModalPopupExtender ID="mpe" runat="server" DropShadow="true" TargetControlId="btn1" ClientIDMode="Static" 
                PopupControlID="ModalPanel" OkControlID="OKButton" BackgroundCssClass="modal-backdrop" />
</asp:Content>


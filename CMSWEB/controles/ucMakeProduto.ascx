<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMakeProduto.ascx.cs" Inherits="controles_ucMakeProduto"  %>

<style>
    a 
    {
        color:#fff;
        text-decoration:none;
    }
</style>
<div class="container-fluid">
    <div class="form-group">
        <asp:HiddenField runat="server" ID="hdnAreaId" />

            <!-- categorias -->
        <asp:UpdatePanel ID="updProduto" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-5">
                        <div class="panel panel-info">
                            <div class="panel-heading">Categoria</div>
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
                                <asp:ListBox ID="lstCategorias" AutoPostBack="true" CssClass="form-control" runat="server" DataTextField="Nome" DataValueField="CategoriaId" Rows="1" Width="200" OnSelectedIndexChanged="listaSubCategoria" >
                                    <asp:ListItem Text="Escolha a Categoria principal" />
                                </asp:ListBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-1">&nbsp;</div>
                    <div class="col-sm-5">
                        <asp:Panel ID="pnlCat" runat="server" CssClass="panel panel-info" Visible="false">
                            <div class="panel-heading">SubCategoria</div>
                            <div class="panel-body">
                                <div class="input-group input-group-sm">
                                    <input type="text" id="txtNomeSubCategoria" runat="server" placeholder="Nome da sub-categoria" class="form-control" />
                                    <span class="input-group-btn">
                                        <asp:LinkButton ID="btnAddCategoriaSub" 
                                                    runat="server" 
                                                    CssClass="btn btn-info btn-xs"    
                                                    CommandName="adCatSub" 
                                                    OnClick="AddCategoria">
                                            <i aria-hidden="true" class="glyphicon glyphicon-plus" style="color:#ffffff"></i>
                                        </asp:LinkButton>
                                    </span>
                                </div>
                                <asp:RequiredFieldValidator ID="rqVal" runat="server"  ControlToValidate="lstCategorias" InitialValue="0" ErrorMessage="Selecione uma categoria pai" />
                                <asp:ListBox ID="lstSubCategorias" CssClass="form-control" AutoPostBack="true" runat="server" DataTextField="Nome" DataValueField="CategoriaId" Rows="1" Width="200" >
                                    <asp:ListItem Text="Escolha a subcategoria" />
                                </asp:ListBox>
                           </div>
                        </asp:Panel>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="pgrCategorias" runat="server">
            <ProgressTemplate>
                <div class="container-fluid">
                    <div class="progress progress-striped active">
                        <div class="bar" style="width: 100%;"></div>
                    </div>
                </div>​
            </ProgressTemplate>
        </asp:UpdateProgress>
        <!-- Text input-->
        <div class="row">
            <div class="col-sm-3">
                <div class="control-group">
                    <label class="control-label" for="txtNomeProduto">Nome</label>
                    <div class="controls">
                        <input id="txtNomeProduto" runat="server" name="txtNomeProduto" type="text" placeholder="nome do produto" class="input-large" />
                    </div>
                </div>
            </div>
        </div>
        <!-- Short Description-->
        <div class="row">
            <div class="col-sm-3">
                <div class="control-group">
                    <label class="control-label" for="txtShortDesc">Descri&ccedil;&atilde;o Curta</label>
                    <div class="controls">
                        <textarea cols="30" id="txtShortDesc" runat="server" placeholder="Escreva uma descricao curta"></textarea>
                     </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-10">
                <ul class="nav nav-pills" role="tablist" id="tabDescritivo">
                    <li class="active"><a href="#description" role="tab" data-toggle="tab"><span class="glyphicon glyphicon-pencil"></span>&nbsp;Descricao</a></li>
                    <li class=""><a href="#techdata" role="tab" data-toggle="tab"><span class="glyphicon glyphicon-th"></span>&nbsp;Dados tecnicos</a></li>
                </ul>

                <!-- Textarea -->
                <div class="tab-content">
                    <div class="tab-pane active" id="description">
                        <div class="summernote-container">
                            <div class="summernote" id="hdesc"></div>
                        </div>
                    </div>
                    <div class="tab-pane" id="techdata">
                        <div class="summernote-container">
                            <div class="summernote" id="hset"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Valor/Unidades -->
        <div class="row">
            <div class="col-sm-3">
                <div class="control-group">
                    <label class="control-label" for="txtDescricao">Preco</label>
                    <div class="controls">                     
                        <asp:TextBox ClientIDMode="Static" id="txtPreco"  runat="server" placeholder="valor do produto" cssclass="input-large" />
                    </div>
                </div>
            </div>
            <div class="col-xs-1">&nbsp;</div>
            <div class="col-sm-3">
                <div class="control-group">
                    <label class="control-label" for="ddlUnidade">Unidade</label>
                    <div class="controls"> 
                        <asp:DropDownList ID="ddlUnidade" runat="server" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Destaque -->
        <div class="row">
            <div class="col-sm-3">
            <div class="control-group">
                <label class="control-label" for="txtDescricao">Destacar?</label> <asp:CheckBox ID="chkDestaque" runat="server" />
            </div>
            </div>
            <div class="col-sm-3">
            <div class="control-group">
                <label class="control-label" for="txtDescricao">SKU</label>
                <div class="controls">                     
                <asp:TextBox runat="server" ID="txtSku" CssClass="input-large" />
                </div>
            </div>
            </div>
        </div>

        <!-- Destaque -->
        <div class="row">
            <div class="col-lg-3">
            <div class="control-group">
                <label class="control-label" for="txtPagSeguroBotao">Botao PagSeguro</label>
                <div class="controls">                     
                    <%--<textarea id="txtPagSeguroBotao" runat="server" name="txtPagSeguroBotao" placeholder="Cole aqui o codigo do botao 'PagSeguro', se voce ja cadastrou para este produto" rows="4" cols="50"></textarea>--%>
                    <asp:HiddenField ID="txtPagSeguroBotao" runat="server" ClientIDMode="Static" />
                </div>
            </div>
            </div>
        </div>

        <!-- Buttons -->
        <div class="row">
            <div class="col-sm-3">
            <div class="control-group">
                <div class="controls">
                    <asp:HiddenField ID="hd" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hs" runat="server" ClientIDMode="Static" />
                    <asp:Button runat="server" ID="btnSalvar" Text="Avan&ccedil;ar" CssClass="btn btn-primary btn-sm" OnClick="btnSalvar_Click" OnClientClick="setContent()" CommandArgument="save" />
                    <asp:Button runat="server" ID="btnEditar" Text="Editar" CssClass="btn btn-primary btn-sm" OnClick="btnSalvar_Click" CommandArgument="edit" />
                </div>
            </div>
            </div>
        </div>

        <script>
            $(function () {
                $("#txtPreco").maskMoney({ prefix: 'R$ ', allowNegative: false, thousands: '.', decimal: ',', affixesStay: false });
            })
        </script>
    </div>
</div>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucAtributos_opcao.ascx.cs" Inherits="controles_ucAtributos_opcao" %>

<asp:UpdatePanel ID="updAtributo" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
        <div class="container-fluid">
            <div class="form-group">
                <div class="row">
                    <div class="col-sm-5">
                        <div class="control-group">
                            <h3><asp:Literal ID="ltProduto" runat="server" /></h3>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4">
                        <div class="control-group input-group-sm">
                            <asp:HiddenField ID="hdnProdutoId" runat="server" />
                            <div class="input-group input-group-sm">
                                <span class="input-group-addon">nome</span><input id="txtNomeAtributo" runat="server" name="txtNomeAtributo" type="text" placeholder="nome do atributo" class="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-1">&nbsp;</div>
                    <div class="col-sm-5">
                        <div class="control-group input-group-sm">
                            <div class="input-group  input-group-sm">     
                                <span class="input-group-addon">descricao</span>                
                                <input id="txtDescricaoAtributo" runat="server" name="txtDescricaoAtributo" type="text" placeholder="Descricao do atributo" class="form-control" />
                                <span class="input-group-btn">
                                    <asp:LinkButton ID="btnAddAttrib" 
                                                runat="server" 
                                                CssClass="btn btn-info btn-sm"    
                                                OnClick="AddAtributo">
                                        <i aria-hidden="true" class="glyphicon glyphicon-plus" style="color:#ffffff"></i>
                                    </asp:LinkButton>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <div class="control-group">
                            <asp:GridView ID="gdrAtributos" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="Nome" HeaderText="Nome" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField Value='<%#Eval("AtributoId") %>' runat="server" ID="hdnAtributo" />
                                            <asp:Button ID="btnAddOp" CssClass="btn btn-primary btn-sm" runat="server" Text="editar" CommandName="AddOpt" CommandArgument=<%#Eval("AtributoId") %> />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <asp:Panel ID="pnlOpcao" runat="server" Visible="false">
                    <hr />
                    <h2>Opcoes</h2>
                    <div class="row">
                            <div class="container-fluid">
                                <div class="col-sm-4">
                                    <input id="txtNomeOpcao" runat="server" name="txtNomeOpcao" type="text" placeholder="Nome da opcao" class="input-large" />
                                    <input id="txtQtdEstoque" runat="server" name="txtQtdEstoque" type="text" placeholder="Quantidade em estoque" class="input-large" />
                                </div>
                                <div class="col-sm-6">
                                    <input id="txtDescricaoOpcao" runat="server" name="txtDescricaoOpcao" type="text" placeholder="Descricao" class="input-large" />
                                    <label class="checkbox">
                                    <asp:CheckBox runat="server" ID="ckbGerenciaEstoque" />Gerenciar estoque?
                                    </label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="btnAddOption" 
                                            runat="server" 
                                            CssClass="btn btn-sm"    
                                            OnClick="AddOpcao">
                                    <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>
                                </asp:LinkButton>
                                </div>
                            </div>
                    </div>
                    <div class="row">
                        <div class="container-fluid">
                            <asp:UpdatePanel ID="updOpc" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdOpcoes" runat="server">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField Value='<%#Eval("OpcaoId") %>' runat="server" ID="hdnOpcao" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nome" HeaderText="Nome" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnAddAttrib" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btnAddOption" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
<input type="button" class="btn btn-primary btn-sm" value="Avan&ccedil;ar" onclick="javascript: callTab('images');" />
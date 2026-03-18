<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucListaProduto.ascx.cs" Inherits="controles_ucListaProduto" %>

<asp:UpdatePanel ID="updProduto" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="container-fluid">
        <div class="form-group">
            <asp:HiddenField runat="server" ID="hdnAreaId" />
            <!-- Buttons -->
            <div class="row">
              <div class="col-lg-10">
                <asp:GridView ID="grdProduto" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" Width="500">
                    <Columns>
                        <asp:BoundField DataField="Nome" HeaderText="Nome" ReadOnly="true" ItemStyle-Width="200" />
                        <%--<asp:BoundField DataField="Descricao" HeaderText="Descricao" ItemStyle-Width="250" />--%>
                        <asp:TemplateField>
                            <InsertItemTemplate>
                                <%#Eval("Descricao") %>
                            </InsertItemTemplate>
                            <AlternatingItemTemplate>
                                <%#Eval("Descricao") %>
                            </AlternatingItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SKU" HeaderText="Sku" ItemStyle-Width="100" />
                        <asp:TemplateField >
                            <HeaderTemplate>
                                Destaque?
                            </HeaderTemplate>
                            <ItemTemplate>
                                <span class="input-group"><asp:CheckBox ID="cbkDestaque" runat="server" /></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Incluir Atributo (dimensoes/cores/modelos)
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Button ID="btneditcao" CssClass="btn btn-primary btn-sm" runat="server" Text="editar" CommandName="InserirAtributos" CommandArgument=<%#Eval("ProdutoId") + ";" + Eval("Nome") %>  />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>Inativar</HeaderTemplate>
                            <ItemTemplate>
                                <asp:Button ID="btnstatus" CssClass="btn btn-danger btn-sm" runat="server" Text="&times;" CommandName="InativaAtributo" CommandArgument=<%#Eval("ProdutoId") %> />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
              </div>
            </div>
        </div>
        </div>
    </ContentTemplate>

</asp:UpdatePanel>
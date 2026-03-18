<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="old_produtoMake.aspx.cs" Inherits="areasMake" %>
<%@ Register Src="~/controles/menu.ascx" TagName="uc" TagPrefix="asp" %>
<%@ Register Src="~/controles/ucescolheimagem.ascx" TagName="uci" TagPrefix="asp" %>
<%@ Register Src="~/controles/ucMakeProduto.ascx" TagPrefix="asp" TagName="ucMakeProduto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="menu" ContentPlaceHolderID="plcManager" runat="server">
    <asp:uc ID="ucMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" > 
     <asp:UpdatePanel ID="pnlformapp" runat="server" UpdateMode="Always">
     <ContentTemplate>
     <div class="col-md-4">
        <div class="container-fluid">
            <div class="row-fluid">
                <div class="col">
                    <h1 style="font-size:58px"><small>Produtos</small></h1>
                    <div class="col-md-5">
                    <asp:Label ID="Categorias" runat="server" Width="400" >Categoria principal</asp:Label><br />
                    <asp:ListBox ID="lstCategorias" AutoPostBack="true" CssClass="form-control" runat="server" DataTextField="Nome" DataValueField="CategoriaId" Rows="1" Width="200" OnSelectedIndexChanged="lstCategorias_SelectedIndexChanged" >
                        <asp:ListItem Text="Escolha a Categoria principal" />
                    </asp:ListBox><br />
                    <asp:Panel ID="pnlCat" runat="server" CssClass="well well-lg" Width="230" Visible="false">
                        <h2 style="font-size:58px;margin-top:0px"><small>SubCategoria</small></h2>
                        <asp:ListBox ID="lstSubCategorias" CssClass="form-control" AutoPostBack="true" runat="server" DataTextField="Nome" DataValueField="CategoriaId" Rows="1" Width="200" >
                            <asp:ListItem Text="Escolha a subcategoria" />
                        </asp:ListBox><br />
                    </asp:Panel>
                    <asp:Label ID="lblAreas" runat="server" Width="400" >Escolha a produto pai (Opcional)</asp:Label><br />
                    <asp:ListBox ID="lstareas" CssClass="form-control" runat="server" DataTextField="Descricao" DataValueField="AreaId" Rows="1" Width="200" >
                        <asp:ListItem Text="Escolha a pagina a abrir" />
                    </asp:ListBox><br />
                    <asp:Label ID="lblname" runat="server" Text="Nome" /><br /><asp:TextBox ID="txtareaname" runat="server" CssClass="form-control" Width="200"  /><br />
                    <asp:Label ID="lbldesc" runat="server" Text="Descrição" /><br /><asp:TextBox ID="txtareadesc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Columns="35" Width="200"  /><br />
                    <asp:Panel ID="pnlimagem" runat="server" CssClass="caixaform" Width="200" Visible="false">
                        <asp:HiddenField ID="imgurl" runat="server" ClientIDMode="Static" />
                        <asp:uci ID="ucimage" runat="server" acao="2" />    
                    </asp:Panel> 
                    <asp:ListBox ID="lstTipo" runat="server" CssClass="form-control" Rows="1" Width="200" >
                        <asp:ListItem Text="Escolha o tipo do Produto" />
                        <asp:ListItem Text="Produto Complexo" Value="8" />
                        <asp:ListItem Text="Produto Simples" Value="9" />
                    </asp:ListBox>
                    <asp:ListBox ID="lstUrl" runat="server" Rows="1" CssClass="form-control" Width="200" >
                        <asp:ListItem Text="Escolha a pagina a abrir" />
                        <asp:ListItem Text="Descricao do Produto" Value="conteudo.aspx" />
                        <asp:ListItem Text="Lista de Produtos" Value="produto.aspx" />
                    </asp:ListBox>
                    <asp:Button ID="btnareamk" CssClass="btn btn-primary" runat="server" Text="criar" />
                    <asp:Button ID="btnareaupd" CssClass="btn btn-warning" runat="server" Text="alterar" Enabled="false" />
                    <asp:HiddenField ID="hdnAreaId" runat="server" />
                    <asp:HiddenField ID="hdnAreaPaiId" runat="server" />
                   <br />
                    <hr class="divider" />
                      <asp:GridView ID="gdrAreas" runat="server" 
                          AutoGenerateColumns="false" 
                          OnRowCommand="EditaLinha" Width="300" >
                        <Columns>
                             <asp:BoundField DataField="NomePai" HeaderText="Produto Complexo" ReadOnly="true" ItemStyle-Width="250" />
                             <asp:BoundField DataField="Nome" HeaderText="Produto" ItemStyle-Width="250" />
                             <asp:TemplateField  ItemStyle-Width="250" >
                                 <ItemTemplate>
                                    <asp:Button ID="btneditcao" CssClass="btn btn-primary btn-sm" runat="server" Text="editar" CommandName="EditarArea" CommandArgument=<%#Eval("AreaId") %> />
                                    <asp:Button ID="btnstatus" CssClass="btn btn-danger btn-sm" runat="server" Text="&times;" CommandName="InativarArea" CommandArgument=<%#Eval("AreaId") %> />
                                 </ItemTemplate>
                             </asp:TemplateField>
                          </Columns>
                       </asp:GridView>
                     </div>
                 </div>
                <!-- FIM  ---->
                </div><!-- container -->
            </div><!-- lado edição -->
        </div>
        <div class="col-md-8">
            <h1 style="font-size:58px"><small>Descricao</small></h1>
            <asp:ucMakeProduto runat="server" ID="ucMakeProduto" />
        </div><!--  lado lista --> 
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="categoriaMake.aspx.cs" Inherits="CategoriaMake" %>
<%@ Register Src="~/controles/menu.ascx" TagName="uc" TagPrefix="asp" %>
<%@ Register Src="~/controles/ucescolheimagem.ascx" TagName="uci" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="menu" ContentPlaceHolderID="plcManager" runat="server">
    <asp:uc ID="ucMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" > 
     <asp:UpdatePanel ID="pnlformapp" runat="server" UpdateMode="Conditional">
     <ContentTemplate>
     <div class="col-md-4">
        <div class="container-fluid">
            <div class="row-fluid">
                <div class="col">
                    <h1 style="font-size:58px"><small>Categoria</small></h1><br />
                    <div class="col-md-5">
<%--                    <asp:Label ID="Categorias" runat="server" Width="400" >Escolha a categoria pai (Opcional)</asp:Label><br />
                    <asp:ListBox ID="lstCategorias" CssClass="form-control" runat="server" DataTextField="Descricao" DataValueField="CategoriaId" Rows="1" Width="200" >
                        <asp:ListItem Text="Escolha a Categoria principal" />
                    </asp:ListBox><br />--%>

                    <asp:Label ID="lblname" runat="server" Text="Nome" /><br /><asp:TextBox ID="txtCatNome" runat="server" CssClass="form-control" Width="200"  /><br />
                    <asp:Label ID="lbldesc" runat="server" Text="Descrição" /><br /><asp:TextBox ID="txtCatDesc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Columns="35" Width="200"  /><br />
                    <br />
                    <asp:Button ID="btnMake" CssClass="btn btn-primary" runat="server" Text="criar" />
                    <asp:HiddenField ID="hdnAreaId" runat="server" />
                    <asp:HiddenField ID="hdnAreaPaiId" runat="server" />
                    <asp:Button ID="btnUpdate" CssClass="btn btn-warning" runat="server" Text="alterar" Enabled="false" />
                    </div>
                    <div class="col-md-6">
                    <asp:DataList ID="gdrAreasx" ClientIDMode="Static" runat="server" >
                        <ItemTemplate>
                            <tr id="<%#Eval("posicao") %>">
                                <td><%#Eval("Nome")%>::<%#Eval("NomePai")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:DataList>
                            </div>
                 </div>
                <!-- FIM  ---->
                </div><!-- container -->
            </div><!-- lado edição -->
        </div>
        <div class="col-md-8">
            <h1 style="font-size:58px"><small>Categorias - Listagem</small></h1><br />
              <asp:GridView ID="gdrCategoria" runat="server" 
                  AutoGenerateColumns="false" 
                  OnRowCommand="EditaLinha" >
                <Columns>
                     <asp:BoundField DataField="NomePai" HeaderText="Categoria Principal" ReadOnly="true" ItemStyle-Width="100" />
                     <asp:BoundField DataField="Nome" HeaderText="SubCategoria" ItemStyle-Width="100" />
                     <asp:TemplateField  ItemStyle-Width="500" >
                         <ItemTemplate>
                            <asp:Button ID="btneditcao" CssClass="btn btn-primary btn-sm" runat="server" Text="editar" CommandName="EditarArea" CommandArgument=<%#Eval("CategoriaId") %> />
                            <asp:Button ID="btnstatus" CssClass="btn btn-danger btn-sm" runat="server" Text="&times;" CommandName="InativarArea" CommandArgument=<%#Eval("CategoriaId") %> />
                         </ItemTemplate>
                     </asp:TemplateField>
                  </Columns>
               </asp:GridView>
         </div><!--  lado lista -->

         
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


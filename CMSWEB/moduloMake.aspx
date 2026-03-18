<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="moduloMake.aspx.cs" Inherits="moduloMake" %>
<%@ Register Src="~/controles/menu.ascx" TagName="uc" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:uc ID="ucMenu" runat="server" />
<br />
<br />
<br />
<div class="container">
    <div class="starter-template">
       <asp:UpdatePanel ID="pnlformapp" runat="server" UpdateMode="Conditional">
           <ContentTemplate>
             <div class="col-md-4">
               <asp:Label ID="lblNome" runat="server" Text="Nome" /><br /><asp:TextBox ID="txtmodnome" runat="server" CssClass="form-control" /><br />
               <asp:Label ID="lblUrl" runat="server" Text="Escolha a página" /><br />
                  <asp:ListBox ID="lstPaginas" runat="server" CssClass="form-control" />
               <br />
               <asp:Button ID="btnmodmake" CssClass="btn btn-primary" runat="server" Text="criar" />
               </div>
               <div  class="col-md-6">
               <div class="bs-example">
                   <asp:DataList ID="lstMod" RepeatLayout="Table" runat="server" CssClass="table table-condensed table-hover">
                       <HeaderTemplate>
                           <th>Modulo</th>
                           <th>Página</th>
                       </HeaderTemplate>
                       <ItemTemplate>
                           <td><%#Eval("Nome")%></td>
                           <td><%#Eval("Url")%></td>
                       </ItemTemplate>
                   </asp:DataList>
               </div>

              <%-- <asp:GridView ID="gdrMod" runat="server" AutoGenerateColumns="false" >
                  <Columns>
                     <asp:BoundField DataField="Nome" HeaderText="Modulo" />
                     <asp:BoundField DataField="Url" HeaderText="Pagina" ReadOnly="true" />

                     <%--<asp:CheckBoxField DataField="Data" HeaderText="Menu Lateral?" />
                     <asp:CheckBoxField DataField="Imagem" HeaderText="Possui vinculo com imagem?" ReadOnly="true" />
                     <asp:TemplateField>
                         <ItemTemplate>
                             <asp:Button ID="btnstatus" CssClass="caixa_botao" runat="server" Text="Inativar" CommandName="InativarArea" CommandArgument=<%#Eval("AreaId") %> />
                         </ItemTemplate>
                     </asp:TemplateField>-->
                  </Columns>
               </asp:GridView>--%>
               </div>
           </ContentTemplate>
       </asp:UpdatePanel>
    </div>
</div>
</asp:Content>


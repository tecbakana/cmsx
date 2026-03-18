<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucResortsList.ascx.cs" Inherits="controles_ucResortsList" %>
<asp:DataList ID="dlResorts" runat="server" RepeatDirection="Vertical">
   <ItemTemplate>
       <div style="width:500px;text-align:left;background-color:#fff">
       <asp:Label ID="lblCidadeNome" runat="server"><a href="Resorts.aspx?cidadeId=<%# Request.QueryString["cidadeId"]%>&fornId=<%#Eval("idFornecedor")%>"><%#Eval("fornecedor") %></a></asp:Label>
       <br />
   </ItemTemplate>
</asp:DataList>
<asp:Panel ID="pnlDetalhe" runat="server" Visible="false">
<div style="width:100%;background-color:#fff">
   <h3><asp:Literal ID="ltTitulo" runat="server" /></h3>
   <img src="" style="width:1024px;height:400px;border:1px solid #fff" />
   <div>
        <asp:Literal ID="ltConteudo" runat="server" />
   </div>
   <hr />
   <label for="periodo1">De:</label><input type="text" class="caixa" id="periodo1" /><label for="periodo2">   até:</label><input type="text" class="caixa" id="periodo2" />
   <asp:GridView ID="gdrDetalhe" runat="server" OnRowDataBound="gdrDetalhe_RowDataBound" OnRowCommand="gdrDetalhe_RowCommand" AutoGenerateColumns="false">
      <Columns>
         <asp:TemplateField>
            <ItemTemplate>
                   <%#Eval("idRoteiro") %><%#Eval("textoRoteiro") %><asp:HiddenField ID="hdnIdRot" runat="server" Value=<%#Eval("idRoteiro") %> />
               </td>
               <td>
                   <asp:DropDownList ID="ddlRot" runat="server" AutoPostBack="true" AppendDataBoundItems="true" >
                      <asp:ListItem>Selecione uma das op&ccedil;&otilde;es</asp:ListItem>
                   </asp:DropDownList>
            </ItemTemplate>
         </asp:TemplateField>
      </Columns>
   </asp:GridView>
   <asp:HiddenField ID="abreDetalhe" runat="server" ClientIDMode="Static" />
</div>
</asp:Panel>

<asp:Panel ID="pnlDetalheResort" runat="server" Visible="false" CssClass="pnlwhite">

<h2 class="orangeb"><asp:Literal ID="ltTituloDet" runat="server" /></h2>
<h4 class="gray"><asp:Literal ID="ltPeriodoDet" runat="server"/></h4>
<h4 class="orange"><asp:Literal ID="ltCidOrigemDet" runat="server" /></h4>
<span class="orangeb">
texto</span><br />
<span class="gray">
texto
</span><br />
<!-- LISTA OPÇOES -->
<asp:DataList ID="dlOpcoes" runat="server">
   <ItemTemplate>
      <asp:Literal ID="ltIdMkt" runat="server" Text=<%#Eval("Acomodacoes") %> /></td><td><asp:Literal ID="Literal1" runat="server" Text=<%#Eval("Regime") %> />
   </ItemTemplate>
</asp:DataList>

</asp:Panel>
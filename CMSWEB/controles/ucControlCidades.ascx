<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucControlCidades.ascx.cs" Inherits="controles_ucControlCidades" %>
<asp:DataList ID="dlCidades" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
   <ItemTemplate>
       <div style="width:300px;text-align:center">
       <asp:Label ID="lblCidadeNome" runat="server"><a href=<%#"Resorts.aspx?cidadeid=" + Eval("idCidade") %>><%#Eval("cidade") %></a></asp:Label>
       <br />
       <asp:Image ID="imgCidade" runat="server" ImageUrl=<%#"http://www.turnet.com.br/mkt_img/" + Eval("imagem")%> Width="210" Height="69" />
       </div>
       <br /> 
    </ItemTemplate>
</asp:DataList>
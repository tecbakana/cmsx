<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucgaleriacliente.ascx.cs" Inherits="controles_ucgaleria" %>

<asp:Panel runat="server" ID="pnlpool" CssClass="imgPool" Width="500" Height="200">
<asp:Image runat="server" ID="imgpreview" Width="200" ClientIDMode="Static" CssClass="imgPool" />
</asp:Panel>
<asp:DataList ID="gdrimg" runat="server" AutoGenerateColumns="true" RepeatColumns="3" RepeatDirection="Horizontal" OnItemDataBound="gdrimg_ItemDataBound">
<ItemTemplate>
    <asp:Image ID="imgGal" runat="server" Width="120" ImageUrl=<%# Eval("Url") %>  />
    <br />
    <asp:CheckBox ID="ckbImage" runat="server" Text=<%# Eval("Descricao") %> CssClass="caixa" /><br />
    <asp:Label ID="lblDesc" runat="server" Text="Descrição:" /><asp:TextBox ID="txtDesc" runat="server" CssClass="caixa" />
</ItemTemplate>
</asp:DataList>
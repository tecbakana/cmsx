<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucsugestao.ascx.cs" Inherits="controles_ucsugestao" %>
<div class="formelement">
    <asp:ListBox runat="server" ID="lstAssunto" Rows="1" CssClass="form-control">
        <asp:ListItem Text="Escolha o assunto:" />
        <asp:ListItem Text="Sugestões" Value="Sugestões" />
        <asp:ListItem Text="Contato"   Value="Contato"  />
        <asp:ListItem Text="Reclamações" Value="Reclamações" />
    </asp:ListBox>
</div>
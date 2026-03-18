<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucmail.ascx.cs" Inherits="controles_ucmail" %>
<%@ Register TagPrefix="ajx" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<div class="formelement">
    <!-- NOME  -->
    <asp:Label ID="lblNome" runat="server" Text="Nome" CssClass="col-sm-2 control-label" />:
    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" Width="320" /><br />

    <!-- EMAIL -->
    <asp:Label ID="lblmail" runat="server" Text="Email" CssClass="col-sm-2 control-label" />:
    <asp:TextBox ID="txtmail" runat="server" CssClass="form-control" Width="320" /><br />

    <!-- TELEFONE  -->
    <asp:Label ID="lblTelefone" runat="server" Text="Telefone" CssClass="col-sm-2 control-label" />:
    <asp:TextBox ID="txtTelefone" runat="server" CssClass="form-control" Width="320" /><br />
    <ajx:MaskedEditExtender TargetControlID="txtTelefone" Mask="(99)9999-9999" ID="mskTelefone" ClearMaskOnLostFocus="false" runat="server" />

    <!--- teste  -->
<%--    <asp:TextBox
        ID="txtComments"
        TextMode="MultiLine"
        Columns="60"
        Rows="8"
        runat="server" />
 
<ajx:HtmlEditorExtender
        TargetControlID="txtComments"
        runat="server" />--%>
</div>
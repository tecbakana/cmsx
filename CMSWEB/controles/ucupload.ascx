<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucupload.ascx.cs" Inherits="controles_ucupload" %>
<div class="formelement" style="font:normal 9pt tahoma,arial">
<asp:Label ID="lblupload" runat="server" Text="Enviar arquivo (4mb)" CssClass="col-sm-2 control-label"/>
<asp:FileUpload ID="fupload" runat="server" CssClass="form-control"  />
</div>
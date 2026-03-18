<%@ Control Language="C#" AutoEventWireup="true" CodeFile="acesso.ascx.cs" Inherits="controles_acesso" %>
<div class="row">
    <div class="col-lg-4">
        <img src="images/text4076cmsx.png" class="image-responsive img-rounded" />
    </div>
</div>
<div class="row">
    <div class="col-lg-4">
        <asp:Panel ID="pnlLogin" runat="server" Visible="true" CssClass="form">
            <br />
            <div class="input-group input-group-sm">
                <span class="input-group-addon">@</span>
                <input type="text" ID="txtLogin" runat="server" class="form-control" placeholder="Login" required autofocus />
            </div>
            <br />
            <div class="input-group input-group-sm">
                <span class="input-group-addon">#</span>
                <input type="password" ID="txtPsw" runat="server" class="form-control" placeholder="Senha" required />
                <span class="input-group-btn">
                    <asp:Button ID="btLogar" runat="server" CssClass="btn btn-info" Text="Acessar" />
                </span>
            </div>
        </asp:Panel>
    </div>
</div>
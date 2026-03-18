<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucModuloUsuario.ascx.cs" Inherits="controles_ucModuloUsuario" %>
<asp:UpdatePanel ID="updEdita" runat="server">
    <ContentTemplate>
        <div class="form-group">
                <asp:HiddenField runat="server" ID="hdnUsuarioID" />
                <!-- Text input-->
                <div class="control-group">
                  <label class="control-label" for="txtNomeProduto">Nome</label>
                  <div class="controls">
                    <asp:Literal ID="ltNome" runat="server" />
                  </div>
                </div>

                <!-- Textarea -->
                <div class="control-group">
                  <label class="control-label" for="txtDescricao">Modulos</label>
                  <div class="controls">                     
                    <asp:ListBox ID="lstModulos"
                         ClientIDMode="Static"
                         CssClass="form-control"
                         runat="server"
                         DataTextField="Nome"
                         DataValueField="ModuloId"
                         Rows="1"
                         SelectionMode="Multiple" >
                        <asp:ListItem Text="Escolha um ou mais modulos para este usuario" />
                    </asp:ListBox><br />              
                  </div>
                </div>

                <!-- Buttons -->
                <div class="control-group">
                    <div class="controls">
                        <asp:Button runat="server" ClientIDMode="Static" ID="btnSalvar" Text="Salvar" CssClass="btn btn-primary btn-sm" OnClick="btnSalvar_Click" CommandArgument="save" />
                    </div>
                </div>
            </div>
    </ContentTemplate>
</asp:UpdatePanel>


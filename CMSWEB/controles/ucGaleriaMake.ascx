<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucGaleriaMake.ascx.cs" Inherits="controles_ucGaleriaMake" %>
<asp:UpdatePanel ID="pnlformapp" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
        
        <div class="container-fluid">
            <div class="form-group">
                <div class="row">
                    <div class="control-group input-group-sm">
                        <div class="col-sm-3">
                           <%-- <asp:ListBox ID="lstTipo" CssClass="form-control" runat="server" Rows="1" >
                                <asp:ListItem Text="Escolha o tipo do arquivo" />
                                <asp:ListItem Text="Imagem"   Value="e1a41094-4ffd-11e1-8664-07b98c902e34" />
                                <asp:ListItem Text="Logotipo" Value="98533dge-4ffe-11e1-8664-07b98c902e34" />
                                <asp:ListItem Text="Texto"    Value="e885ffb6-4ffd-11e1-8664-07b98c902e34" />
                                <asp:ListItem Text="Pdf"      Value="f78bcda9-4ffd-11e1-8664-07b98c902e34" />
                                <asp:ListItem Text="Doc"      Value="ff9d5a44-4ffd-11e1-8664-07b98c902e34" />
                                <asp:ListItem Text="Excel"    Value="05133708-4ffe-11e1-8664-07b98c902e34" />
                            </asp:ListBox><br />
                            <br />--%>

                            <div class="input-group input-group-sm">
                                <asp:FileUpload ID="upf1" runat="server" CssClass="form-group" />
                                <input id="txtDescricao1" runat="server" name="txtDescricao" type="text" placeholder="Breve descricao" class="form-group" />

                                <asp:FileUpload ID="upf2" runat="server" CssClass="form-group" />
                                <input id="txtDescricao2" runat="server" name="txtDescricao" type="text" placeholder="Breve descricao" class="form-group" />

                                <asp:FileUpload ID="upf3" runat="server" CssClass="form-group" />
                                <input id="txtDescricao3" runat="server" name="txtDescricao" type="text" placeholder="Breve descricao" class="form-group" />

                                <asp:FileUpload ID="upf4" runat="server" CssClass="form-group" />
                                <input id="txtDescricao4" runat="server" name="txtDescricao" type="text" placeholder="Breve descricao" class="form-group" />

                                <asp:FileUpload ID="upf5" runat="server" CssClass="form-group" />
                                <input id="txtDescricao5" runat="server" name="txtDescricao" type="text" placeholder="Breve descricao" class="form-group" />

                                 <asp:Button ID="btnfilemake" CssClass="btn btn-info btn-xs" runat="server" Text="criar" OnClick="btnfilemake_Click" />
                            </div>
                            <br />
                            <asp:Label ID="lblErro" runat="server" Visible="false" CssClass="erro" />
                        </div>
                    </div>
                </div>
<%--            </div>
            <div class="row">--%>
                <div class="col-sm-9">
                    <div class="control-group">
                        <br />
                        <asp:DataList ID="gdrimg" runat="server" RepeatColumns="4"  >
                            <ItemTemplate>
                                <asp:Image ID="img" runat="server" CssClass="img-responsive" />
                                <asp:CheckBox ID="ckbImagemDestaque" runat="server" />
                                <asp:CheckBox ID="ckbSlider" runat="server" />
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <asp:Image ID="img" runat="server" CssClass="img-responsive" />
                                <asp:CheckBox ID="ckbImagemDestaque" runat="server" />
                                <asp:CheckBox ID="ckbSlider" runat="server" />
                            </AlternatingItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnfilemake" />
        <asp:AsyncPostBackTrigger ControlID="gdrimg" EventName="ItemDataBound" />
    </Triggers>
</asp:UpdatePanel>
<input type="button" id="btavancar" runat="server" clienteidmode="static" class="btn btn-primary btn-sm" value="Avan&ccedil;ar" onclick="javascript: callTab('lista');" />
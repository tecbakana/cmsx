<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="appMake.aspx.cs" Inherits="appMake" ValidateRequest="false" EnableEventValidation="false" %>
<%@ Register Src="~/controles/menu.ascx" TagName="uc" TagPrefix="asp" %>
<%@ Register Src="~/controles/ucgaleria.ascx" TagName="ucg" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="menu" runat="server" ContentPlaceHolderID="plcManager">
    <asp:uc ID="ucMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:TextBox ID="txtTest" runat="server" TextMode="MultiLine"></asp:TextBox>
    <asp:UpdatePanel ID="pnlformapp" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="col-md-4">
                <asp:TextBox ID="txtappname" runat="server" CssClass="form-control" placeholder="Nome da aplicacao" /><br />
                <asp:TextBox ID="txtappurl" runat="server" CssClass="form-control" placeholder="Url da aplicacao" /><br />
                <div class="checkbox">
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <span class="label label-default">Logotipo</span>
                        <asp:FileUpload ID="upFile" runat="server" CssClass="form-group" />
                    </div>
                </div>
                <asp:Label ID="lblTemplate" runat="server" Text ="Templates" Width="80" />
                <asp:DropDownList ID="ddlTemplate" runat="server">
                    <asp:ListItem Text="Escolha o template" Value="-1" />
                    <asp:ListItem Text="Basico I" Value="_Layout.cshtml" />
                    <asp:ListItem Text="Basico II" Value="_LayoutBasic.cshtml" />
                    <asp:ListItem Text="OnePage" Value="_LayoutFlame.cshtml" />
                    <asp:ListItem Text="Loja" Value="_LayoutLoja.cshtml" />
                </asp:DropDownList><br />
                <asp:TextBox ID="txtEmail" runat="server" placeholder="Digite seu email" CssClass="form-control" /><br />
                <asp:TextBox ID="txtToken" runat="server" placeholder="TokenPagSeguro" CssClass="form-control" /><br />
                <asp:TextBox ID="txtAdSense" runat="server" placeholder="Cole aqui o codigo do Google AdSense" CssClass="form-control" TextMode="MultiLine" MaxLength="350" /><br />
                <asp:Repeater ID="rptRedes" runat="server">
                    <ItemTemplate>
                        <div class="control-group input-group-sm">
                            <div class="input-group  input-group-sm">     
                                <asp:Label ID="lblRede"  runat="server" CssClass="input-group-addon input-medium" Text='<%#Eval("SocialMediaName")%>' ClientIDMode="Static" />
                                <asp:TextBox ID="txtRede" runat="server" placeholder="Digite seu identificador" CssClass="form-control" ClientIDMode="Static"  />  
                            </div>
                        </div>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <div class="control-group input-group-sm">
                            <div class="input-group  input-group-sm">     
                                <asp:Label ID="lblRede" runat="server" CssClass="input-group-addon input-medium" Text='<%#Eval("SocialMediaName")%>' ClientIDMode="Static" />
                                <asp:TextBox ID="txtRede" runat="server" placeholder="Digite seu email" CssClass="form-control" ClientIDMode="Static" />  
                            </div>
                        </div>
                    </AlternatingItemTemplate>
                    <HeaderTemplate>
                        <div class="row">   
                    </HeaderTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>    
                <asp:HiddenField runat="server" ID="hdnEditId" />
                <asp:Button ID="btnEditar" runat="server" Text="editar" CssClass="btn btn-default" Enabled="false" />
                <asp:Button ID="btnappmake" runat="server" Text="criar" CssClass="btn btn-default" />
                <asp:PlaceHolder ID="plcBtEditar" runat="server" />
            </div>
            <div class="col-md-6">
                <asp:GridView ID="gdrapp" runat="server" AutoGenerateColumns="false" OnRowDataBound="gdrapp_RowDataBound" OnRowCommand="gdrapp_RowCommand" DataKeyNames="AplicacaoId,Nome,DataFinal" CssClass="table table-hover">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ID="appLogo" runat="server" CssClass="img-thumbnail" />
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <asp:Image ID="appLogo" runat="server" CssClass="img-thumbnail" />
                            </AlternatingItemTemplate>
                        </asp:TemplateField>
                        <asp:ImageField DataImageUrlField="ImagemUrl" ControlStyle-CssClass="img-thumbnail" />
                        <asp:BoundField DataField="Nome" HeaderText="Aplicação" />
                        <asp:BoundField DataField="Url" HeaderText="Url" ReadOnly="true" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" CssClass="caixa_botao" runat="server" Text="Editar"    CommandName="Editar" CommandArgument=<%#Eval("AplicacaoId") %> OnCommand="btnstatus_Command"/>
                                <asp:Button ID="btnstatusoff" CssClass="caixa_botao" runat="server" Text="Inativar" CommandName="InativarAplicacao" CommandArgument=<%#Eval("AplicacaoId") %> OnCommand="btnstatus_Command"/>
                                <asp:Button ID="btnstatuson" CssClass="caixa_botao" runat="server" Text="Ativar"    CommandName="AtivarAplicacao" CommandArgument=<%#Eval("AplicacaoId") %> OnCommand="btnstatus_Command" Visible="false"/>
                                <asp:Button ID="btnDel" CssClass="caixa_botao" runat="server" Text="Excluir" CommandName="ExcluirAplicacao" CommandArgument=<%#Eval("AplicacaoId") %> OnCommand="btnstatus_Command"/>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <asp:Button ID="btnEdit" CssClass="caixa_botao" runat="server" Text="Editar"    CommandName="Editar" CommandArgument=<%#Eval("AplicacaoId") %> OnCommand="btnstatus_Command"/>
                                <asp:Button ID="btnstatusoff" CssClass="caixa_botao" runat="server" Text="Inativar" CommandName="InativarAplicacao" CommandArgument=<%#Eval("AplicacaoId") %> OnCommand="btnstatus_Command"/>
                                <asp:Button ID="btnstatuson" CssClass="caixa_botao" runat="server" Text="Ativar"    CommandName="AtivarAplicacao" CommandArgument=<%#Eval("AplicacaoId") %> OnCommand="btnstatus_Command" Visible="false"/>
                                <asp:Button ID="btnDel" CssClass="caixa_botao" runat="server" Text="Excluir" CommandName="ExcluirAplicacao" CommandArgument=<%#Eval("AplicacaoId") %> OnCommand="btnstatus_Command"/>
                            </AlternatingItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnEditar" />
            <asp:PostBackTrigger ControlID="btnappmake" />
            <%--<asp:AsyncPostBackTrigger ControlID="btnstatuson" EventName="OnCommand" />
            <asp:AsyncPostBackTrigger ControlID="btnstatusoff" EventName="OnCommand" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <asp:HiddenField ID="ctl" runat="server" />
           <asp:Button ID="btn1" runat="server"  />
              <asp:Panel ID="ModalPanel" runat="server" Width="700px" Height="500px" >
              
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                        <button type="button" id="OKButton" class="close" data-dismiss="modal" aria-hidden="true">fechar</button>
                        <h4 class="modal-title" id="myModalLabel">Incluir imagem</h4>
                        </div>
                        <div class="modal-body">
                            <%--<iframe src="galeria.aspx" style="width:400px;height:500px;border:0px" ></iframe>--%>
                            <asp:ucg ID="glr" runat="server" acao="2"/>
                        </div>
                    </div>
                </div>
              </asp:Panel>   
              <ajx:ModalPopupExtender ID="mpe" runat="server" DropShadow="true" TargetControlId="btn1" ClientIDMode="Static" 
                PopupControlID="ModalPanel" OkControlID="OKButton" BackgroundCssClass="modal-backdrop" />
</asp:Content>


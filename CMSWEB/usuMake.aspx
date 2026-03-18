<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="usuMake.aspx.cs" Inherits="usuMake" %>
<%@ Register Src="~/controles/menu.ascx" TagName="uc" TagPrefix="asp" %>
<%--<%@ Register Src="~/controles/ucModuloUsuario.ascx" TagPrefix="asp" TagName="ucModuloUsuario" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:uc ID="ucMenu" runat="server" />
<br />
<br />
<br />
<div class="container">
    <div class="starter-template">

            <div class="row">
             <div class="col-lg-4">
               <asp:Label ID="lblApp" runat="server">Escolha a Aplicacao</asp:Label><br />
               <asp:ListBox ID="lstApp" CssClass="form-control" runat="server" DataTextField="Nome" DataValueField="AplicacaoId" Rows="1" >
                  <asp:ListItem Text="Escolha a aplicação à qual este usuario estará vinculado" />
               </asp:ListBox><br />
           
               <asp:ListBox ID="lstModulos" 
                   CssClass="form-control" 
                   runat="server" DataTextField="Nome" DataValueField="ModuloId" Rows="10" SelectionMode="Multiple" >
                  <asp:ListItem Text="Escolha um ou mais modulos para este usuario" />
               </asp:ListBox><br />

               <asp:Label ID="lblNome" runat="server" Text="Nome" /><br /><asp:TextBox ID="txtusunome" runat="server" CssClass="caixa" /><br />
               <asp:Label ID="lblSobrenome" runat="server" Text="Sobrenome" /><br /><asp:TextBox ID="txtususobrenome" runat="server" CssClass="caixa" /><br />
               <asp:Label ID="lblApelido" runat="server" Text="Apelido/Login" /><br /><asp:TextBox ID="txtusuapelido" runat="server" CssClass="caixa" MaxLength="6" /><br />
               <asp:Label ID="lblSenha" runat="server" Text="Senha" /><br /><asp:TextBox ID="txtususenha1" runat="server" CssClass="caixa" TextMode="Password" MaxLength="12" /><br />
               <asp:Label ID="lblSenhaConf" runat="server" Text="Redigite a Senha" /><br /><asp:TextBox ID="txtususenha2" runat="server" CssClass="caixa" TextMode="Password" /><br />
               <br />
               <asp:Label ID="lblErro" runat="server" Visible="false" CssClass="erro" />
               <br />
               <asp:Button ID="btnusumake" CssClass="caixa_botao" runat="server" Text="criar" />
               </div>
             <div class="col-lg-6">
              <div class="row">
                        <div class="col-sm-2"> <asp:Label ID="apelido" runat="server" Text="Apelido" /></div>  
                        <div class="col-sm-3"> <asp:Label ID="aplicacao" runat="server" Text="Aplicacao" /></div>         
                        <div class="col-sm-4"> <asp:Label ID="Data" runat="server" Text="Data" /></div>
                        <div class="col-sm-3"> <asp:Label ID="acao" runat="server" Text="*"/></div>
                </div>
                <asp:UpdatePanel ID="pnlformapp" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Repeater  ID="lstUsu"  OnItemCommand="lstUsu_ItemCommand" runat="server" OnItemDataBound="lstUsu_ItemDataBound"  >
                            <ItemTemplate> 
                                <div class="row">  
                                    <div class="col-sm-2"><%#Eval("Apelido")%></div>
                                    <div class="col-sm-3"><%#Eval("Aplicacao")%></div>
                                    <div class="col-sm-4"><%#Eval("DataInclusao")%></div>
                                    <div class="col-sm-3">
                                        <asp:Button runat="server" ID="btnModal" CommandName="edtModul" CommandArgument='<%#Eval("UserId")%>' Text="Editar" CssClass="btn-primary" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ContentTemplate>
                </asp:UpdatePanel>

              <asp:Button ID="btn1" runat="server"  />
              <asp:Panel ID="ModalPanel" runat="server" Width="500px" Height="500px" >
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                        <button type="button" id="OKButton" class="close" data-dismiss="modal" aria-hidden="true">fechar</button>
                        <h4 class="modal-title" id="myModalLabel">Editar usuario</h4>
                        </div>
                        <div class="modal-body">
                            <h3>Modal Body</h3>
                            <div class="modal-form">
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
                                            <asp:HiddenField ID="hdnUser" runat="server" />
                                            <label class="control-label" for="txtDescricao">Modulos</label>
                                            <div class="controls">     
                                                    <asp:ListBox ID="lstModulosUsuario"
                                                            ClientIDMode="Static"
                                                            CssClass="form-control"
                                                            runat="server"
                                                            DataTextField="Nome"
                                                            DataValueField="ModuloId"
                                                            Rows="10"
                                                            SelectionMode="Multiple" >
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

                            </div>
                        </div>
                    </div>
                </div>
              </asp:Panel>             
              <ajx:ModalPopupExtender ID="mpe" runat="server" DropShadow="true" TargetControlId="btn1" ClientIDMode="Static" 
                PopupControlID="ModalPanel" OkControlID="OKButton" BackgroundCssClass="modal-backdrop" />
    
<%--               <asp:GridView ID="gdrUsu" runat="server" AutoGenerateColumns="false"  >
                  <Columns>
                     <asp:BoundField DataField="Apelido" HeaderText="Usuario" />
                     <asp:BoundField DataField="Aplicacao" HeaderText="Aplicação" ReadOnly="true" />
                     <asp:BoundField DataField="DataInclusao" HeaderText="Data de Inclusão" ReadOnly="true" />

                     <%--<asp:CheckBoxField DataField="Data" HeaderText="Menu Lateral?" />
                     <asp:CheckBoxField DataField="Imagem" HeaderText="Possui vinculo com imagem?" ReadOnly="true" />
                     <asp:TemplateField>
                         <ItemTemplate>
                                
                         </ItemTemplate>
                     </asp:TemplateField>-->
                  </Columns>
               </asp:GridView>--%>
               </div>
              </div>
    </div>



</div>
</asp:Content>




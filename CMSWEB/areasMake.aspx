<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="areasMake.aspx.cs" Inherits="areasMake" %>
<%@ Register Src="~/controles/menu.ascx" TagName="uc" TagPrefix="asp" %>
<%@ Register Src="~/controles/ucgaleria.ascx" TagName="ucg" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="menu" ContentPlaceHolderID="plcManager" runat="server">
    <asp:uc ID="ucMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" > 
<%--     <asp:UpdatePanel ID="pnlformapp" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
     <ContentTemplate>--%>
     <div class="col-md-4">
        <div class="container-fluid">
            <div class="row-fluid">
                <div class="col">
                    <h1 style="font-size:58px"><small>Página</small></h1>
                    <hr />
                    <div class="col-md-5">
                    <%--<asp:Label ID="lblCategoria" runat="server" Width="400" >Escolha a categoria</asp:Label><br />
                    <asp:ListBox ID="lstCategoria" CssClass="form-control" runat="server" DataTextField="Nome" DataValueField="CategoriaId" Rows="1" Width="200" >
                        <asp:ListItem Text="Escolha a categoria" />
                    </asp:ListBox><br />--%>
                    <asp:Label ID="lblAreas" runat="server" Width="400" >Escolha a Página pai (Opcional)</asp:Label><br />
                    <asp:ListBox ID="lstareas" CssClass="form-control" runat="server" DataTextField="Nome" DataValueField="AreaId" Rows="1" Width="200" >
                        <asp:ListItem Text="Escolha a pagina a abrir" />
                    </asp:ListBox><br />
                    <div class="checkbox">
                    <asp:CheckBox ID="ckbhome" runat="server" Text="Deseja fazer deste menu um item interno da home?(Apenas texto)" Width="200" Visible="false"  />
                    </div>
                    <asp:Label ID="lblname" runat="server" Text="Nome" /><br /><asp:TextBox ID="txtareaname" runat="server" CssClass="form-control" Width="200"  /><br />
                    <asp:Label ID="lbldesc" runat="server" Text="Descrição" /><br /><asp:TextBox ID="txtareadesc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Columns="35" Width="200"  /><br />
                    <asp:HiddenField ID="hdnPosicao" runat="server" /><br />
                    <div class="checkbox">
                    <asp:CheckBox ID="ckbimagem" runat="server" Text="Vincular Imagem?" AutoPostBack="true" Width="200" OnCheckedChanged="HabilitaImagem" />
                    </div>
                    <asp:HiddenField ID="hdnImages" runat="server" ClientIDMode="Static" />
                    <div class="row">
                        <asp:Panel ID="pnlimagem" runat="server" CssClass="col-lg-20" Visible="false" Height="100">
                            <asp:HiddenField ID="imgurl" runat="server" ClientIDMode="Static" />
                            <img id="preview" class="img-responsive" /> 
                             <%--<asp:Label runat="server" ID="lblurl" Text="Apelido da imagem Ex.: botao_home" />&nbsp;<asp:TextBox ID="info" runat="server" Width="145" CssClass="caixa" ClientIDMode="Static" Text="imagem"/>
                           <asp:uci ID="ucimage" runat="server" acao="2" /> 
                             <asp:Button ID="btn1" runat="server" CssClass="btn-small btn-details"  /> --%> 
                        </asp:Panel> 
                    </div>
                    <asp:ListBox ID="lstTipo" runat="server" CssClass="form-control" Rows="1" Width="200" >
                        <asp:ListItem Text="Escolha o tipo do menu" />
                        <asp:ListItem Text="Menu Lateral" Value="0" />
                        <asp:ListItem Text="Menu Splash" Value="1" />
                        <asp:ListItem Text="Menu Principal" Value="2" />
                        <%--<asp:ListItem Text="Produto Complexo" Value="8" />
                        <asp:ListItem Text="Produto Simples" Value="9" />--%>
                    </asp:ListBox>
                    <asp:ListBox ID="lstUrl" runat="server" Rows="1" CssClass="form-control" Width="200" >
                        <asp:ListItem Text="Escolha a pagina a abrir" />
                        <asp:ListItem Text="Home" Value="home.aspx" />
                        <asp:ListItem Text="Conteudo" Value="conteudo.aspx" />
                        <asp:ListItem Text="Produtos" Value="commerce.aspx" />
                        <asp:ListItem Text="Servicos" Value="business.aspx" />
<%--                        <asp:ListItem Text="Grupo"    Value="area.aspx" />
                        <asp:ListItem Text="Galeria"    Value="galeria_cliente.aspx" />--%>
                        <asp:ListItem Text="Formulario" Value="formulario.aspx" />
<%--                        <asp:ListItem Text="Produtos/Servicos" Value="commerce.aspx" />
                        <asp:ListItem Text="Produto/Servico" Value="produto.aspx" />--%>
                    </asp:ListBox>
                    <div class="checkbox">
                    <asp:CheckBox ID="ckbCustomPage" AutoPostBack="true" runat="server" Text="Vincular pagina?" Width="200" OnCheckedChanged="ckbCustomPage_CheckedChanged" Visible="false" />
                    </div>
                    <asp:ListBox ID="lstPaginas" runat="server" Rows="1" Enabled="false" CssClass="form-control" Width="200" Visible="false"  />
                    <br />
                    <asp:Button ID="btnareamk" CssClass="btn btn-primary" runat="server" Text="criar" />
                    <asp:HiddenField ID="hdnAreaId" runat="server" />
                    <asp:HiddenField ID="hdnAreaPaiId" runat="server" />
                    <asp:Button ID="btnareaupd" CssClass="btn btn-warning" runat="server" Text="alterar" Enabled="false" />
                    </div>
                    <div class="col-md-6">
                    <asp:DataList ID="gdrAreasx" ClientIDMode="Static" runat="server" >
                        <ItemTemplate>
                            <tr id="<%#Eval("posicao") %>">
                                <td><%#Eval("Nome")%>::<%#Eval("NomePai")%></td>
                                <td><img src="images/ck<%#Eval("MenuLateral")%>.png" /></td>
                                <td>
                                    <img src="images/ck<%#Eval("Imagem")%>.png" />
                                    <input type="hidden" id="hdnId" name="hdnId" value="<%#Eval("AreaId") %>" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:DataList>
                            </div>
                 </div>
                <!-- FIM  ---->
                </div><!-- container -->
            </div><!-- lado edição -->
        </div>
        <div class="col-md-8">
            <h1 style="font-size:58px"><small>Página - Listagem</small></h1>
            <hr />
            <div class="row">
            
            <asp:Repeater ID="rptAreas" runat="server" OnItemCommand="EditarRepeater" >
                <HeaderTemplate>
                    <div id="listaAreas">
                        <div class="row">
                            <div class="col-sm-1">&nbsp;</div>
                            <div class="col-sm-2">Página</div>
                            <div class="col-sm-2">Página Pai</div>
                            <div class="col-sm-4">*</div>
                        </div>
                        <div class="row">&nbsp;</div>
                </HeaderTemplate>
                <FooterTemplate></div></FooterTemplate>
                <ItemTemplate>
                    <div class="row" id='<%#Eval("AreaId")%>'>
                        <div class="col-sm-1 glyphicon glyphicon-sort" style="cursor:move"></div>
                        <div class="col-sm-2"><%#Eval("Nome") %></div> 
                        <div class="col-sm-2"><%#Eval("NomePai") %></div> 
                        <div class="col-sm-4">
                            <asp:Button ID="btneditcao" CssClass="btn btn-primary btn-sm" runat="server" Text="editar" CommandName="EditarArea" CommandArgument=<%#Eval("AreaId") %> />
                            <asp:Button ID="btnstatus" CssClass="btn btn-danger btn-sm" runat="server" Text="&times;" CommandName="InativarArea" CommandArgument=<%#Eval("AreaId") %> />
                        </div> 
                        <hr class="separator" />
                    </div>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <div class="row" id='<%#Eval("AreaId")%>'>
                        <div class="col-sm-1 glyphicon glyphicon-sort" style="cursor:move"></div>
                        <div class="col-sm-2"><%#Eval("Nome") %></div> 
                        <div class="col-sm-2"><%#Eval("NomePai") %></div> 
                        <div class="col-sm-4">
                            <asp:Button ID="btneditcao" CssClass="btn btn-primary btn-sm" runat="server" Text="editar" CommandName="EditarArea" CommandArgument=<%#Eval("AreaId") %> />
                            <asp:Button ID="btnstatus" CssClass="btn btn-danger btn-sm" runat="server" Text="&times;" CommandName="InativarArea" CommandArgument=<%#Eval("AreaId") %> />
                        </div>
                        <hr class="separator" />
                    </div>
                </AlternatingItemTemplate>
            </asp:Repeater>
            </div>  
         </div><!--  lado lista -->
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
<%--        </ContentTemplate>
         <Triggers>
             <asp:AsyncPostBackTrigger ControlID="ckbImagem" EventName="CheckedChanged" />
             <asp:AsyncPostBackTrigger ControlID="rptAreas" EventName="ItemCommand" />
         </Triggers>
    </asp:UpdatePanel>--%>
              

    <script>
        $(document).ready(function () {
            $('#listaAreas').sortable({
                update: function (event, ui) {
                    var areaOrder = $(this).sortable('toArray').toString();
                    //var areaOrder = $(this).serialize();
                    $.get('sort.ashx', { areaOrder: areaOrder });
                }
            });
        });

    </script>
</asp:Content>


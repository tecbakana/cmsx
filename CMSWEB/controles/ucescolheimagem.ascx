<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucescolheimagem.ascx.cs" Inherits="controles_ucescolheimagem" %>
<asp:Label runat="server" ID="lblurl" Text="Apelido da imagem Ex.: botao_home" />&nbsp;<asp:TextBox ID="info" runat="server" Width="145" CssClass="caixa" ClientIDMode="Static" Text="imagem"/>
<%--<a class="ex2trigger" href='#' onclick="openmypageparm('galeria.aspx?acao=2','650','500','Escolher imagem');" title="Escolher imagem">
Escolher imagem
</a>--%>
     <img src="images/text2987.png" data-toggle="modal" data-target="#myModal" >
    <!-- Modal -->
    <div class="modal fade col-lg-12" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
            <h4 class="modal-title" id="myModalLabel"><asp:Literal ID="ltshopNome" runat="server" /></h4>
            </div>
            <div class="modal-body">
                    
            </div>
            <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
            </div>
        </div>
        </div>
    </div>
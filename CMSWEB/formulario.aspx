<%@ Page Title="" Language="C#" MasterPageFile="~/CommerceMaster.master" AutoEventWireup="true" CodeFile="formulario.aspx.cs" Inherits="formulario" Async="true" %>
<%@ Register Src="~/controles/ucmail.ascx" TagName="ucm" TagPrefix="asp" %>
<%@ Register Src="~/controles/uctexto.ascx" TagName="uct" TagPrefix="asp" %>
<%@ Register Src="~/controles/ucsugestao.ascx" TagName="ucs" TagPrefix="asp" %>
<%@ Register Src="~/controles/ucupload.ascx" TagName="ucu" TagPrefix="asp" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cplMaster" Runat="Server">

<div class="row">
    <div class="col-md-8">
        <div id="pnlMsg" style="background-color:transparent">
            <span class="bg-success">
            <asp:Label ID="retmsg" runat="server" />
            <button type="button" class="close" aria-hidden="true" onclick="javascript:handMsg(2);">&times;</button>
            </span>
        </div>
    </div>
</div>

<div class="row">
    <div class="col-md-8">
        <h2>Aqui você pode deixar suas dúvidas e sugestões.</h2><br />
        <asp:UpdatePanel runat="server" ID="pnlpool" CssClass="form-actions" Width="500" Height="400" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:ucm runat="server" ID="ucmail" Visible="false" /><br />
                <asp:ucs runat="server" ID="ucsugestao" Visible="false" /><br />
                <asp:uct runat="server" ID="uctexto" Visible="false" /><br />
                <asp:ucu runat="server" ID="ucupload" Visible="false" /><br />
                <asp:Label runat="server" ID="lblRetorno" CssClass="obs" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <asp:Button ID="btsend" runat="server" class="btn btn-default" Text="Enviar" OnClick="btsend_Click" />
    </div>
</div>
    
    <script type="text/javascript">

    /*
    *  FECHA A MENSAGEM
    */
    function handMsg(p) {
        obj = document.getElementById("pnlMsg");
        obj.style.display = (p == 1 ? "block" : "none");
    }
   
</script>
</asp:Content>


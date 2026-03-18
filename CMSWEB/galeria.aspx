<%@ Page Title="" Language="C#" MasterPageFile="~/InnerCliente.master" AutoEventWireup="true" CodeFile="galeria.aspx.cs" Inherits="galeria" %>
<%@ Register Src="~/controles/ucgaleria.ascx" TagName="uc" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function setImage(obj)
    {

        var win = this.window.parent;
        info = win.document.getElementById("info");
        info.value = obj;

        hid = win.document.getElementById("imgurl");
        hid.value = obj;

        alert("Imagem escolhida, pode fechar ou fazer nova escolha.");
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-md-12">
        <asp:uc ID="glr" runat="server" acao="2"/>
    </div>
</asp:Content>


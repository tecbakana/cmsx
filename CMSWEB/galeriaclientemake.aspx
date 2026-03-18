<%@ Page Title="Galerias" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="galeriaclientemake.aspx.cs" Inherits="galeriaclientemake" %>
<%@ Register Src="~/controles/menu.ascx" TagName="uc" TagPrefix="asp" %>
<%@ Register Src="~/controles/ucgaleriacliente.ascx" TagName="ucg" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function setImage(obj) {
        var win = window.parent;
        info = win.document.getElementById("info");
        info.value = obj;

        hid = win.document.getElementById("imgurl");
        hid.value = obj;

        alert("Imagem escolhida, pode fechar ou fazer nova escolha.");
    }
</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:uc ID="ucMenu" runat="server" />
<br />
<br />
<br />
<div class="container">
    <div class="starter-template">
       <asp:UpdatePanel ID="pnlformapp" runat="server" UpdateMode="Conditional">
           <ContentTemplate>
             <div id="ladoForm" style="width:400px">

               <asp:ListBox ID="lstAreas" AutoPostBack="true" CssClass="caixa" runat="server" Rows="1" />
               <br />
               <asp:ListBox ID="lstAreasFilha" runat="server" Rows="1"  AutoPostBack="true" CssClass="caixa" Visible="false"></asp:ListBox>
               <br />
               <asp:Label ID="lblErro" runat="server" Visible="false" CssClass="erro" />
               <br />

               <asp:Button ID="btngalerymake" CssClass="caixa_botao" runat="server" Text="criar" OnClick="btngalerymake_Click" />
               </div>
               <div id="ladoLista" style="left:400px">
               <asp:ucg ID="ucgaleria" runat="server" />
               <%--<asp:DataList ID="gdrimg" runat="server" AutoGenerateColumns="true" RepeatColumns="5" RepeatDirection="Horizontal" OnItemDataBound="gdrimg_ItemDataBound">
                <ItemTemplate>
                    <asp:Image ID="imgGal" runat="server" Width="120" ImageUrl=<%# Eval("Url") %>  />
                </ItemTemplate>
               </asp:DataList>--%>
               </div>
           </ContentTemplate>
           <Triggers>
              <asp:PostBackTrigger ControlID="btngalerymake" />
           </Triggers>
       </asp:UpdatePanel>
    </div>
</div>
</asp:Content>

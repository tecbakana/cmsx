<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente.master" AutoEventWireup="true" CodeFile="galeria_cliente.aspx.cs" Inherits="galeria_cliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function setImage(obj, txtval)
    {

        alert(txtval);
        info = document.getElementById("info");
        info.innerText = txtval;

        hid = document.getElementById("imgurl");
        hid.value = obj;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
   <tr>
      <td>
        <asp:Panel runat="server" ID="pnlpool" CssClass="imgPool" Width="400" Height="400">
            <asp:Image runat="server" ID="imgpreview" Width="400" ClientIDMode="Static" /><br />
            <asp:Label ID="info" runat="server" Text="Descrição:" CssClass="caixa" ClientIDMode="Static"/>
        </asp:Panel>      
      </td>
      <td>
        <asp:DataList ID="gdrimg" runat="server" AutoGenerateColumns="true" RepeatColumns="2" RepeatDirection="Horizontal" OnItemDataBound="gdrimg_ItemDataBound">
            <ItemTemplate>
                <asp:Image ID="imgGal" runat="server" Width="80" ImageUrl=<%# Eval("Url") %>  />
                <asp:HiddenField ID="hdndesc" runat="server" Value=<%#Eval("Descricao") %> />
            </ItemTemplate>
        </asp:DataList>      
      </td>
   </tr>
</table>


</asp:Content>


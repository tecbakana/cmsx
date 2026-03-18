<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="formulariomake.aspx.cs" Inherits="formulariomake" %>
<%@ Register Src="~/controles/menu.ascx" TagName="uc" TagPrefix="asp" %>
<%@ Register Src="~/controles/ucescolheimagem.ascx" TagName="uci" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
<asp:uc ID="ucMenu" runat="server" />
<asp:UpdatePanel ID="pnlForm" runat="server" >
   <ContentTemplate>
         <div id="ladoForm" style="width:400px">
    <asp:Label ID="lblCabecalho" runat="server" CssClass="titulo">Administração de formularios</asp:Label>
    <br />
    <br />
    <asp:Label ID="lblListaAreas" runat="server" Text="Area a ser vinculada" /><br />
    <asp:ListBox runat="server" ID="lstAreas" Rows="1" />
    <br />
    <asp:Label ID="lblNome" runat="server" Text="Nome do formulário" /><br />
    <asp:TextBox ID="txtNome" CssClass="caixa" runat="server" />
    <br />
    <asp:CheckBoxList ID="ckbItems" runat="server">
        <asp:ListItem Text="Nome,Email e Telefone" Value="email" />
        <asp:ListItem Text="Texto" Value="texto" />
        <asp:ListItem Text="Sugestões" Value="sugestao" />
        <asp:ListItem Text="Envio de arquivo" Value="upload" />
    </asp:CheckBoxList>

    <asp:Button runat="server" ID="btSend" CssClass="caixa_botao" Text="Gravar" OnClick="send_content" />
    </div>
    <div id="ladoLista" style="left:400px">
    <asp:GridView ID="dlformulario" runat="server" AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField HeaderText="Nome" DataField="Nome" />
        <asp:BoundField HeaderText="Area" DataField="AreaNome" />
    </Columns>
    </asp:GridView>
    </div>
   </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

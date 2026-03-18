<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="appLista.aspx.cs" Inherits="appLista" %>
<%@ Register Src="~/controles/menu.ascx" TagName="uc" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:uc ID="ucMenu" runat="server" />
<asp:DataList ID="listaApp" runat="server">
   <ItemTemplate>
       <asp:Label runat="server" ID="lblAplicacao" Text=<%# Eval("AplicacaoId") %>/>
       <asp:Label runat="server" ID="lblNome" Text=<%# Eval("Nome") %>/>
   </ItemTemplate>
</asp:DataList>
</asp:Content>


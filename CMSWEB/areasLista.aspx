<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="areasLista.aspx.cs" Inherits="areasLista" %>
<%@ Register Src="~/controles/menu.ascx" TagName="uc" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:uc ID="ucMenu" runat="server" />
<asp:DataList runat="server" ID="dlAreas">
   <ItemTemplate>
     <asp:Label ID="lblNome" runat="server" Text=<%# Eval("Nome") %> />
   </ItemTemplate>
</asp:DataList>
</asp:Content>


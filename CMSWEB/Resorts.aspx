<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente.master" AutoEventWireup="true" CodeFile="Resorts.aspx.cs" Inherits="Resorts" %>
<%@register Src="~/controles/ucResortsList.ascx" TagName="resort" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:resort ID="ucResList" runat="server" />
</asp:Content>


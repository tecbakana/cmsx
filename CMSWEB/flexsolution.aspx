<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente.master" AutoEventWireup="true" CodeFile="flexsolution.aspx.cs" Inherits="flexsolution" %>
<%@ Register Src="~/controles/menu_splash_lateral.ascx" TagName="ucs" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="menusplash">
       <asp:ucs ID="mnsplash" runat="server" />
    </div>
</asp:Content>


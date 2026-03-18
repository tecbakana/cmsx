<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente.master" AutoEventWireup="true" CodeFile="rotCidades.aspx.cs" Inherits="rotCidades" %>
<%@register Src="~/controles/ucControlCidades.ascx" TagName="cid" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:cid ID="plcCidade" runat="server" />
</asp:Content>


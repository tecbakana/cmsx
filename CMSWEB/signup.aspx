<%@ Page Title="" Language="C#" MasterPageFile="~/bootstrap_client.master" AutoEventWireup="true" CodeFile="signup.aspx.cs" Inherits="signup" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
    <div class="container theme-showcase" role="main">

      <!-- Main jumbotron for a primary marketing message or call to action -->
      <div class="jumbotron">
        <h1><asp:Literal runat="server" ID="ltOla" /></h1>
        <p>This is a template for a simple marketing or informational website. It includes a large callout called a jumbotron and three supporting pieces of content. Use it as a starting point to create something more unique.</p>
        <p><asp:HyperLink CssClass="btn btn-primary btn-lg" NavigateUrl="#" runat="server" ID="lkMais">&raquo;</asp:HyperLink>
        </p>
      </div>

    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="galeriamake.aspx.cs" Inherits="galeriamake" %>
<%@ Register Src="~/controles/menu.ascx" TagName="uc" TagPrefix="asp" %>

<%@ Register Src="~/controles/ucGaleriaMake.ascx" TagPrefix="uc1" TagName="ucGaleriaMake" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-lg-3">
        <div class="container-fluid" id="masterd">
            <div class="row-fluid"> 
                <asp:uc ID="ucMenu" runat="server" />
                <br />
                <br />
                <uc1:ucGaleriaMake runat="server" ID="ucGaleriaMake" tipo="2" />
            </div>
        </div>
    </div>
</asp:Content>



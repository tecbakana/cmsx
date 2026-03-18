<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="sumernote.aspx.cs" Inherits="sumernote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function ()
        {
            $('.summernote').summernote({
                height:300
            });
        });

    </script>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="col-md-8">  
                <div class="summernote-container">
                    <div class="summernote"><%Response.Write(Session["texto"]); %></div>
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="btnAtualiza" runat="server" OnClick="btnAtualiza_Click" Text="atualizar" UseSubmitBehavior="false" />
</asp:Content>
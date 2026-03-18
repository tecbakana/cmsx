<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucProdutoLista.ascx.cs" Inherits="controles_ucProdutoLista" %>
<%@ Register Assembly="cmsxControls" Namespace="CmsxControls" TagPrefix="cc1" %>

<div class="row">
    <div id="carousel-example" class="carousel slide hidden-xs" data-ride="carousel">
        <!-- Wrapper for slides -->
        <div class="container">
            <!-- dynamic load  -->
            
            <cc1:aiListView  ID="lvProduto" runat="server" GroupItemCount="4" ClientIDMode="Static">
                <GroupTemplate>
                    <div class="item active">
                        <div class="row">
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server">
                            
                            </asp:PlaceHolder>
                        </div>
                    </div>
                </GroupTemplate>                
                
                <GroupSeparatorTemplate>
                    <div class="row">
                        <br />
                    </div>
                </GroupSeparatorTemplate>
                <ItemTemplate>
                    <asp:HiddenField ID="hdnProdutoId" runat="server" Value='<%#Eval("ProdutoId") %>' />
                    <div class="col-sm-2">
                                <div class="col-item">
                                    <div class="photo">
                                        <asp:Image ID="imgThumb" runat="server" CssClass="image-responsive" />
                                    </div>
                                    <div class="info">
                                        <div class="row">
                                            <div class="price col-md-6">
                                                <h5><%#Eval("Nome") %></h5>
                                                <h5 class="price-text-color">
                                                    R$ <%#Eval("Valor") %></h5>
                                            </div>
                                        </div>
                                        <div class="separator clear-left">
                                            <p class="btn-add">
                                                <asp:LinkButton CssClass="label label-primary" id="btnShop" runat="server" Text="Comprar" onclick="btnShop_Click" CommandArgument='<%#Eval("ProdutoId") %>' />
                                            </p>
                                            <p class="btn-details">
                                                <asp:LinkButton CssClass="label label-primary" id="btnShowDet" runat="server" Text="[+]mais.." onclick="btnShowDet_Click" CommandArgument='<%#Eval("ProdutoId") %>' />
                                            </p>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                </div>
                            </div>    
                </ItemTemplate>
            </cc1:aiListView>
        </div>
    </div>
</div>



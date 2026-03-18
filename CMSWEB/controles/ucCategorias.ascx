<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCategorias.ascx.cs" Inherits="controles_ucCategorias" %>
<asp:UpdatePanel ID="pnlCategoria" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <div class="left-sidebar">
            <h2>Categorias</h2>
            <asp:Repeater ID="rptCat" runat="server">
                <HeaderTemplate>
                <div class="panel-group category-products">
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:HiddenField ID="hdnCatIdPai" runat="server" Value='<%#Eval("CategoriaId") %>' ClientIDMode="Static" />
                    <div class="panel panel-default">
                        <div class="panel panel-heading">
                            <h4 class="panel-title">
					            <a data-toggle="collapse" data-parent="#accordian" href='#<%#Eval("Nome") %>' class="collapsed">
						            <span class="badge pull-right"><i class="fa fa-plus"></i></span>
						            <%#Eval("Nome") %>
					            </a>
				            </h4>
                        </div>
                        <div id='<%#Eval("Nome") %>' class="panel-collapse collapse" style="height: 0px;">
		                    <div class="panel-body">
                                <asp:DataList runat="server" ID="lstSubCat" ClientIDMode="Static">
                                    <HeaderTemplate>
                                        <ul>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <asp:LinkButton runat="server" CommandName="goLocation" CommandArgument='<%#Eval("CategoriaId") %>' Text='<%#Eval("Nome") %>' OnClick="lkbt_Click"  />
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:DataList>
		                    </div>
	                    </div>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                </div>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
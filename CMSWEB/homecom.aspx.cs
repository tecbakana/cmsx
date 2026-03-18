using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;
using CMSDAL;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;

public partial class homecom: BasePage
{
    public Guid categoriaId { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        ucCategorias.loadProduto += ucCategorias_loadProduto;
        ucProdutoLista1.compraProduto += ucProdutoLista1_compraProduto;
    }

    /// <summary>
    /// Lista produtos com base na categoria
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ucCategorias_loadProduto(object sender, EventArgs e)
    {
        controles_ucProdutoLista uc = ucProdutoLista1;
        LinkButton lk = (LinkButton)sender;
        uc.ListaProdutoXCategoria(lk.CommandArgument);
    }

    /// <summary>
    /// Detalhe do produto
    /// </summary>
    /// <param name="parm"></param>
    protected void ucProdutoLista1_compraProduto(string parm)
    {
        Session["produto"] = new System.Guid(parm);
        Response.Redirect("shop.aspx");
    }

}
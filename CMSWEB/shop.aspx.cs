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

public partial class shop : BasePage
{
    public Guid categoriaId { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        ucProdutoDetalhe.pagarProduto += ucProdutoDetalhe_pagarProduto;
    }

    protected void ucProdutoDetalhe_pagarProduto(string parm)
    {
        ltshopNome.Text = parm;
    }


}
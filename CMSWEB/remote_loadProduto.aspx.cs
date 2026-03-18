using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;
using CMSDAL;

public partial class remote_loadProduto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["produto"] = Request.QueryString["id"];
        controles_ucProdutoDetalhe uc = ucProdutoDetalhe;
        uc._url = Request.QueryString["cliente"];
    }
}
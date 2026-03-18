using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sumernote : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["texto"] = "digite aqui o texto";
    }
    protected void btnAtualiza_Click(object sender, EventArgs e)
    {
        Session["texto"] = "Atualizado";
        if (!ClientScript.IsClientScriptBlockRegistered("carrega"))
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "carrega", "<script>$('.summernote').code('faster');</script>");
        }
    }
}
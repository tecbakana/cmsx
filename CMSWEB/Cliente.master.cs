using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;

public partial class Cliente : BaseMasterPage
{
    public Aplicacao app;
    protected void Page_Load(object sender, EventArgs e)
    {

        app = (Aplicacao)Session["cliente"];
        //Response.Write(Request.QueryString["cliente"]);
    }


}

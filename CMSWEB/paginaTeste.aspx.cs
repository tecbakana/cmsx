using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class paginaTeste : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*if (Session["cliente"] == null)
        {
            Response.Write("Sessão não existe");
            _obj.parms = 1;
            _obj.urlcliente = "advir";
            _apprepo.MakeConnection(_obj);
            
            _apprepo.RegistraAplicacao();
            Session["cliente"] = "teste";
            Response.Redirect("paginaTeste.aspx");
        }

        if (Session["Objeto"] == null)
        {
            Response.Write("Sessão objeto não existe");
        }
        else
        {
            Response.Write("Sessão objeto existe");
        }*/
        Session["cliente"] = null;
    }
}
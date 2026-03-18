using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMSXBLL;
using System.Configuration;

public partial class bootstrap : BaseMasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["cliente"] == null)
        {
            RegistraCliente();
        }
        else
        {
            Aplicacao ax = (Aplicacao)Session["cliente"];
        }
    }

    protected void RegistraCliente()
    {
        try
        {
            _obj.parms = 1;
            _obj.urlcliente = ConfigurationManager.AppSettings["applicationPath"].ToString();
            _apprepo.MakeConnection(_obj);
            Session["cliente"] = _apprepo.RegistraAplicacao();
            try
            {
                Aplicacao ax = (Aplicacao)Session["cliente"];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    private void LoadConfig()
    {
        var js = new HtmlGenericControl("script");
        js.Attributes["type"] = "text/javascript";
        js.Attributes["src"] = "myFile.js"; 
        Page.Header.Controls.Add(js);
    }
}

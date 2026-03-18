using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;

public partial class flexsolution : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["cliente"] == null)
        {
            RegistraCliente();
        }
        else
        {
            // Response.Write(Session["cliente"].ToString());
        }
    }

    protected void RegistraCliente()
    {
        try
        {
            _obj.parms = 1;
            _obj.urlcliente = "flex";//Request.QueryString["cliente"];
            _apprepo.MakeConnection(_obj);
            Session["cliente"] = _apprepo.RegistraAplicacao();
            try
            {
                Aplicacao ax = (Aplicacao)Session["cliente"];
                Response.Write(ax.Nome);
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
}
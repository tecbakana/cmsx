using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;

public partial class home_recorte : BasePage
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
            _app = (Aplicacao)Session["cliente"];
            //Response.Write(ax.Nome);
        }
    }

    protected void RegistraCliente()
    {
        try
        {
            _obj.parms = 1;
            _obj.urlcliente = "heritage";//Request.QueryString["cliente"];
            _apprepo.MakeConnection(_obj);
            Session["cliente"] = _apprepo.RegistraAplicacao();
            try
            {
                Aplicacao ax = (Aplicacao)Session["cliente"];
               // Response.Write(ax.Nome);
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
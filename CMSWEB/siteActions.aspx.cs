using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;
using System.Configuration;

public partial class siteActions : BasePage
{
    private string id
    {
        get
        {
            return Request.QueryString["id"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.id))
        {
            ActivateAccount();
        }
    }

    protected void ActivateAccount()
    {
        try
        {
            _obj.parms = 1;
            _obj.aplicacao = new Aplicacao() { AplicacaoId = new System.Guid(this.id) };
            _apprepo.MakeConnection(_obj);
            var url = _apprepo.AtivaAplicacao();
            if (!string.IsNullOrEmpty(url))
            {
                string pth = ConfigurationManager.AppSettings["pathAbs"] + "/" + url;
                Response.Redirect(pth);
            }
            else
            {
                Response.Write("Esta conta nao existe ou nao pode ser ativada, tente novamente mais tarde");
            }
        }
        catch (Exception ex)
        {
            Response.Write("Erro ao processar a requisicao. Tente novamente mais tarde!");
        }
        finally
        {
        }
    }

}
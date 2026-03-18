using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;
using System.Configuration;

public partial class homehand : BasePage
{

    /* delegates */
    delegate void dGoLocation(object[] Parameters);

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


        /* metodos delegados */
       dGoLocation dgol = new dGoLocation(GoLocation);
        //Set method reference to a user control delegate
       this.menu_cliente.ucGoLocation = dgol;
       this.lstProduto.ucGoLocation = dgol;
       this.lstprodutoN.ucGoLocation = dgol;
    }

    protected void RegistraCliente()
    {
        try
        {
            _obj.parms = 1;
            _obj.urlcliente = Request.QueryString["cliente"];// ConfigurationManager.AppSettings["applicationPath"].ToString();
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

    protected void GoLocation(object[] parms)
    {
        string acao = parms[0].ToString();

        if(acao.Contains("commerce"))
        {
            lstProduto.AreaIdParm = parms[0].ToString();
            pnlCommerce.Visible = true;
        }
        else if(acao.Contains("produto"))
        {
            
        }
    }

    protected void menu_cliente_UserControlButtonClicked(object sender, EventArgs e)
    {
        string x = e.ToString();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;

public partial class controles_menu_controle : BaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CarregaMenu();
    }

    protected void CarregaMenu()
    {
        try
        {
            _obj.parms = 1;
            _obj.urlcliente = "advir";//Request.QueryString["cliente"];
            _apprepo.MakeConnection(_obj);
            _app = _apprepo.RegistraAplicacao();

            _obj.appid = _app.AplicacaoId;
            _areasrepo.MakeConnection(_obj);
             //lstMenu.DataSource = _areasrepo.ListaAreaMenu();
             //lst/enu.DataBind();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
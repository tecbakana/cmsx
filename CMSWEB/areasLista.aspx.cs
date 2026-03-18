using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class areasLista : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //_obj.banco = "SqlServer";
        ListaAreas();
    }

    protected void ListaAreas()
    {
        _obj.parms = 1;
        _obj.appid = "8C506292-6DF6-43C1-883E-6E525300E83F";
        _areasrepo.MakeConnection(_obj);
        
        dlAreas.DataSource =_areasrepo.ListaAreas();
        dlAreas.DataBind();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;

public partial class controles_ucControlCidades : BaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CarregaCidades();
    }

    protected void CarregaCidades()
    {
        dlCidades.DataSource = null;
        dlCidades.DataBind();

        _obj.parms = 2;
        _obj.clienteId = 2;
        _obj.tabelaid = 1;

        _rotrepo.MakeConnection(_obj);
        List<RoteiroBLL> _roteiros = _rotrepo.ListaRoteiroPorCidade();
        dlCidades.DataSource = _roteiros;
        dlCidades.DataBind();
    }
}
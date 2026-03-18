using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;

public partial class controles_menu_splash_lateral : BaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CarregaMenu();
    }

    protected void CarregaMenu()
    {
        _obj.parms = 1;
        _app = (Aplicacao)Session["cliente"];
        _obj.appid = _app.AplicacaoId;
        _areasrepo.MakeConnection(_obj);

        List<Areas> _lst = _areasrepo.ListaAreaMenuSplash();

        foreach (Areas _area in _lst)
        {
            _area.Descricao = string.IsNullOrEmpty(_area.Descricao) ? "--" : _area.Descricao.Substring(0, (_area.Descricao.Length < 20 ? _area.Descricao.Length : 20));
        }


        lstMenuSplash.DataSource = _lst;
        lstMenuSplash.DataBind();
    }
}
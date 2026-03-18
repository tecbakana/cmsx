using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using CMSXBLL;

public partial class controles_produto_cliente : BaseControl
{
    public int _tipoArea;

    protected void Page_Load(object sender, EventArgs e)
    {
        lstMenu.ItemDataBound += new EventHandler<ListViewItemEventArgs>(lstMenu_ItemDataBound);
        if (Session["cliente"] != null)
        {
            CarregaMenu();
        }
    }

    protected void CarregaMenu()
    {
        try
        {
            _obj.parms = 1;
            _app = (Aplicacao)Session["cliente"];
            _obj.appid = _app.AplicacaoId;
            _areasrepo.MakeConnection(_obj);
            _areasrepo.ListaAreaMenu();
           lstMenu.DataSource = _areasrepo.ListaAreaMenu();
           lstMenu.DataBind();
        }
        catch (Exception ex)
        {
           throw new Exception(ex.StackTrace);
        }
    }

    protected void lstMenu_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListView _lst = (ListView)sender;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            CMSXBLL.Areas  _area = (Areas)e.Item.DataItem;

            _obj.parms = 2;
            _app = (Aplicacao)Session["cliente"];
            _obj.appid = _app.AplicacaoId;

            _obj.areaIdPai = _area.AreaId;
            _areasrepo.MakeConnection(_obj);

            ListView _slst = (ListView)e.Item.FindControl("lstSubMenu");

            _slst.DataSource = _areasrepo.ListaAreasFilha();
            _slst.DataBind();
        }
    }


}
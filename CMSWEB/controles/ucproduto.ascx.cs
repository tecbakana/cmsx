using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using CMSXBLL;

public partial class controles_produto : BaseControl
{


    #region PROPERTIES
    public int TipoArea { get; set; }
    public string AreaIdParm { get; set; }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //dltProdutos.ItemDataBound += new EventHandler<ListViewItemEventArgs>(lstMenu_ItemDataBound);
        rptProdutos.ItemCommand += rptProdutos_ItemCommand;
        if (Session["cliente"] != null)
        {
            CarregaMenu();
        }
    }

    protected void CarregaMenu()
    {
        try
        {
            _obj.parms = 2;
            _app = (Aplicacao)Session["cliente"];
            _obj.appid = _app.AplicacaoId;

            //if (Request.QueryString["areaId"] != null)
            //{
            //    _obj.areaIdPai = Request.QueryString["areaId"];
            if(AreaIdParm != null)
            {
                _obj.areaIdPai = AreaIdParm;
            }
            else
            {
                _obj.tipoArea = TipoArea;
            }

            _areasrepo.MakeConnection(_obj);
            rptProdutos.DataSource = (Request.QueryString["areaId"] != null ? _areasrepo.ListaAreasFilha() : _areasrepo.ListaAreasPorTipo());
            rptProdutos.DataBind();

            //if (TipoArea == 9)
            //{
            //    dltProdutos.RepeatDirection = RepeatDirection.Vertical;
            //}
        }
        catch (Exception ex)
        {
           throw new Exception(ex.StackTrace);
        }
    }

    protected void rptProdutos_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "goLocation":
                object objx = setParm(e.CommandArgument.ToString());
                _delGoLocation.DynamicInvoke(objx);
                break;
        }
    }

   /* protected void lstMenu_ItemDataBound(object sender, ListViewItemEventArgs e)
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
    }*/


}
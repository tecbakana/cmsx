using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;

public partial class controles_smenu : BaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CarregaMenu();
    }

    #region metodos

    protected void CarregaMenu()
    {
        _obj.parms = 1;
        _app = (Aplicacao)Session["cliente"];
        _obj.appid = _app.AplicacaoId;
        _areasrepo.MakeConnection(_obj);

        List<Areas> _lst = _areasrepo.ListaAreaMenu();
        foreach (Areas _area in _lst)
        {
            _area.Descricao = string.IsNullOrEmpty(_area.Descricao) ? "--" : _area.Descricao.Substring(0, (_area.Descricao.Length < 20 ? _area.Descricao.Length : 20));
        }
        ltMenu.DataSource = _lst;
        ltMenu.DataBind();
    }

    /// <summary>
    /// Metodo para carregar subitens de menu conforme o Id da área superior
    /// </summary>
    /// <param name="areaId">Guid - id da área que tem subitens / area pai</param>
    /// <param name="item">MenuItem - controle que vai receber os subitens</param>
    protected void CarregaMenuTree(Guid areaId, MenuItem item)
    {
        _obj.parms = 2;
        _app = (Aplicacao)Session["cliente"];
        _obj.appid = _app.AplicacaoId;
        _obj.areaIdPai = areaId;
        _areasrepo.MakeConnection(_obj);

        List<Areas> _lstChildren = _areasrepo.ListaAreasFilha();
        if (_lstChildren.Count >= 1)
        {
            foreach (Areas _area in _lstChildren)
            {
                MenuItem _chdMenu = new MenuItem();

                _chdMenu.Value = "~/" + _area.Url + "?areaid=" + _area.AreaId.ToString();
                _area.Descricao = string.IsNullOrEmpty(_area.Descricao) ? "--" : _area.Descricao.Substring(0, (_area.Descricao.Length < 20 ? _area.Descricao.Length : 20));
                _chdMenu.Text = _area.Nome;

                item.ChildItems.Add(_chdMenu);
            }
        }
    }


    #endregion

    #region Eventos

    /// <summary>
    /// Ao carregar os dados do menu, checar se os itens possuem subitens e então popular os items com dropdown
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void ltMenu_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewItem lv = e.Item;
            string x = lv.ToString();
        }
    }


    #endregion
    protected void lstItemMenu_Click(object sender, EventArgs e)
    {

    }
}
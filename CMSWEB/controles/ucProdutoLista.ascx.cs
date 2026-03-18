using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;
using CMSDAL;

public partial class controles_ucProdutoLista : BaseControl
{
    #region Propriedades

    public event ChildDelegateEvent compraProduto;
    public delegate void ChildDelegateEvent(string parm);

    #endregion

    #region Eventos
    protected override void OnInit(EventArgs e)
    {
        lvProduto.ItemDataBound += lvProduto_ItemDataBound;
        base.OnInit(e);
        CmsxControls.aiListView ail = new CmsxControls.aiListView();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListaProduto();
        }
    }

    protected void lvProduto_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewItem it = (ListViewItem)e.Item;
            _obj.parms = 1;
            _obj.pId = ((HiddenField)it.FindControl("hdnProdutoId")).Value;
            _imgrepo.MakeConnection(_obj);
            List<Imagem> lstimg = _imgrepo.GaleriaParentId();
            Image img = (Image)it.FindControl("imgThumb");
            if (lstimg.Count >= 1)
            {
                img.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["pathSaveOut"].ToString() + lstimg[0].Url;
                img.AlternateText = lstimg[0].Descricao;
                lstimg[0].Url = System.Configuration.ConfigurationManager.AppSettings["pathSaveOut"].ToString() + lstimg[0].Url;
            }
        }
    }

    protected void rptProduto_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            RepeaterItem it = (RepeaterItem)e.Item;
            _obj.parms = 1;
            _obj.pId = ((HiddenField)it.FindControl("hdnProdutoId")).Value;
            _imgrepo.MakeConnection(_obj);
            List<Imagem> lstimg = _imgrepo.GaleriaParentId();
            Image img = (Image)it.FindControl("imgThumb");
            if (lstimg.Count >= 1)
            {
                img.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["pathSaveOut"].ToString() + lstimg[0].Url;
                img.AlternateText = lstimg[0].Descricao;
                lstimg[0].Url = System.Configuration.ConfigurationManager.AppSettings["pathSaveOut"].ToString() + lstimg[0].Url;
            }
        }
    }

    protected void btnShop_Click(object sender, EventArgs e)
    {
        string cmd = ((LinkButton)sender).CommandArgument;
        compraProduto(cmd);
    }

    #endregion

    #region Metodos

    /// <summary>
    /// Metodo para listar os produtos da loja
    /// </summary>
    protected void ListaProduto()
    {
        _obj.parms = 1;
        _app = (Aplicacao)Session["cliente"];
        _obj.appid = _app.AplicacaoId.ToString();
        _prodrepo.MakeConnection(_obj);
        lvProduto.DataSource = _prodrepo.ListaProduto();
        lvProduto.DataBind();
    }

    public void AtualizaLista()
    {
        ListaProduto();
    }

    public void ListaProdutoXCategoria(string catId)
    {
        _obj.parms = 1;
        _obj.ctId = catId;
        _prodrepo.MakeConnection(_obj);
        lvProduto.DataSource = _prodrepo.ListaProdutoXCategoria();
        lvProduto.DataBind();
    }

    #endregion
    protected void btnShowDet_Click(object sender, EventArgs e)
    {

    }
}
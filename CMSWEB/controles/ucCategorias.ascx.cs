using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;
using CMSDAL;

public partial class controles_ucCategorias : BaseControl
{

    public event EventHandler loadProduto;

    #region eventos
    protected override void OnInit(EventArgs e)
    {
        rptCat.ItemDataBound += rptCat_ItemDataBound;
        
        base.OnInit(e);
    }

    protected void rptCat_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item)||(e.Item.ItemType == ListItemType.AlternatingItem))
        {
            RepeaterItem it = (RepeaterItem)e.Item;
            _obj.parms = 1;
            _obj.cpid = ((HiddenField)it.FindControl("hdnCatIdPai")).Value;
            _catrepo.MakeConnection(_obj);
            List<Categoria> scatls = _catrepo.ListaSubCategoria();
            
            DataList dt = (DataList)it.FindControl("lstSubCat");
            dt.DataSource = scatls;
            dt.DataBind();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        MontaCategorias();
    }

    protected void lkbt_Click(object sender, EventArgs e)
    {
        if (loadProduto != null)
            loadProduto(sender, new EventArgs());
    }
    #endregion

    #region Metodos
    protected void MontaCategorias()
    {
        _obj.parms = 1;
        _app = (Aplicacao)Session["cliente"];
        _obj.appid = _app.AplicacaoId.ToString();
        _catrepo.MakeConnection(_obj);
        List<Categoria> catls = _catrepo.ListaCategoriaPai();

        rptCat.DataSource = from cat in catls
                            where string.IsNullOrEmpty(cat.CategoriaIdPai.ToString())==true
                            select cat;
        rptCat.DataBind();

    }

    #endregion


}
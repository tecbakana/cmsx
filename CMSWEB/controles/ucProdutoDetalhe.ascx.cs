using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMSXBLL;
using CMSDAL;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using System.Configuration;

public partial class controles_ucProdutoDetalhe : BaseControl
{
    #region propriedades
    public event ChildDelegateEvent pagarProduto;
    public delegate void ChildDelegateEvent(string parm);
    public Guid pId;
    public bool tipo;
    public string _url;
    #endregion

    #region eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             CarregaProduto();
        }
    }

    
    
    #endregion

    #region metodos

    private void CarregaProduto()
    {
        if (Session["produto"] != null)
        {
            Produto p = new Produto();
            _obj.parms = 1;
            _obj.prodId = Session["produto"].ToString();
            _prodrepo.MakeConnection(_obj);
            p = _prodrepo.ListaProdutoXId()[0];

            //------------------------------------

            LoadImages(Session["produto"].ToString());

            //------------------------------------
            //ltTitulo.Text = p.Nome;
            ltDescricao.Text = p.Descricao;
            ltTecnico.Text = p.DetalheTecnico;
            hdnProdId.Value = p.ProdutoId.ToString();

            //-----------------LISTA DE PRODUTOS ----------------
            List<Produto> produtos = new List<Produto>();
            produtos.Add(p);
            Session["listaProduto"] = produtos;
            if (!tipo)
            {
                pagarProduto(p.Nome);
            }
        }
    }

    public void LoadImages(string parentId)
    {
        dlProdutoImagem.DataSource = null;
        dlProdutoImagem.DataBind();

        _obj.parms = 1;
        _obj.pId = parentId;
        _imgrepo.MakeConnection(_obj);
        var lsim = _imgrepo.GaleriaParentId();

        // Create sample data for the DataList control.
        DataTable dt = new DataTable();
        DataRow dr;

        // Define the columns of the table.
        dt.Columns.Add(new DataColumn("Thumb", typeof(String)));
        dt.Columns.Add(new DataColumn("InnerSlider", typeof(String)));

        // Populate the table with sample values.
        int ctrl = 0;
        foreach(var item in lsim)
        {
            dr = dt.NewRow();

            ////<li class="col-md-4 h5"><a id="carousel-selector-0" class="selected"><img src="http://placehold.it/80x60&amp;text=uno" class="img-responsive"></a></li>
            string imagem = "<li class='col-md-4 h5'>";
            //inserindo o id
            imagem += "<a id='carousel-selector-" + ctrl.ToString() + "' " + (ctrl == 0 ? "class='selected'" : " ") + ">";
            imagem += "<img src='../../../cms/clientes/" + _url + "/images/_" + item.Url + "' class='img-responsive'></a></li>";

 
            dr[0] = imagem;

            ////div guide
            ////<div class="active item" data-slide-number="0"><img src="http://placehold.it/1200x980&amp;text=uno" class="img-responsive"></div>

            string div = "<div class='" + (ctrl == 0 ? "active item" : "item") + "' data-slide-number='" + ctrl.ToString() + "'>";
            string pth = ConfigurationManager.AppSettings["pathAbs"];
            div += "<img src='" + pth + "/cms/clientes/" + _url + "/images/_" + item.Url + "' class='img-responsive'></div>";

            dr[1] = div;

            dt.Rows.Add(dr);

            ctrl++;
        }

        DataView dv = new DataView(dt);

        dlProdutoImagem.DataSource = dv;
        dlProdutoImagem.DataBind();

        rptDivCarrousel.DataSource = dv;
        rptDivCarrousel.DataBind();

    }

    protected void btnPagamento_Click(object sender, EventArgs e)
    {
        //Show Payment modal
        pagarProduto(Session["produto"].ToString());
    }

    protected void callModal()
    {
        //Render the function invocation. 
        string funcCall = "callModal()";
        Type tp = this.GetType();
        string skey = "callModal";
        ScriptManager.RegisterStartupScript(this, tp,skey, funcCall, true);
    }

    protected void CreateDataSource()
    {

        
    }

    protected void dlProdutoImagem_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        //{
        //    ///li image guide
        //    Imagem img = (Imagem)e.Item.DataItem;
        //    ////<li class="col-md-4 h5"><a id="carousel-selector-0" class="selected"><img src="http://placehold.it/80x60&amp;text=uno" class="img-responsive"></a></li>
        //    Literal lt = (Literal)e.Item.FindControl("ltImagem");
        //    string imagem = "<li class='col-md-4 h5'>";
        //    //inserindo o id
        //    imagem += "<a id='carousel-selector-" + e.Item.ItemIndex.ToString() + "'" + (e.Item.ItemIndex == 0 ? "class='selected'" : " ") + ">";
        //    imagem += "<img src='images/" + img.Url + "' class='img-responsive'></a></li>";

        //    lt.Text = imagem;
        //}
    }

    #endregion

}
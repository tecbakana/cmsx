using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using CMSXBLL;

public partial class galeriaclientemake : BasePage
{

    private dynamic ambiente;
    protected int indice;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!ValidateSession)
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ambiente = Session["Objeto"];
        if (!IsPostBack)
        {
            CarregaAreas();
            MontaComboAreas();
        }
        lstAreas.SelectedIndexChanged += new EventHandler(lstAreas_SelectedIndexChanged);
        lstAreasFilha.SelectedIndexChanged += new EventHandler(lstAreasFilha_SelectedIndexChanged);
    }

    protected void lstAreas_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBox lb = (ListBox)sender;
        indice = lb.SelectedIndex;
        if (indice >= 1)
        {
            MontaComboAreasFilha(new System.Guid(lb.SelectedValue));
            if (lstAreasFilha.Items.Count <= 0)
                ListaConteudoPorArea(new Guid(lb.SelectedValue));
        }
    }

    protected void lstAreasFilha_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBox lb = (ListBox)sender;
        indice = lb.SelectedIndex;
        if (indice > -1)
            ListaConteudoPorArea(new Guid(lb.SelectedValue));
    }

    protected void btngalerymake_Click(object sender, EventArgs e)
    {
        /* criando a galeria */
        DataList gdr = (DataList)ucgaleria.FindControl("gdrimg");
        foreach (DataListItem item in gdr.Items)
        {
            CheckBox ckb = (CheckBox)item.FindControl("ckbImage");
            TextBox txt = (TextBox)item.FindControl("txtDesc");
            Image img = (Image)item.FindControl("imgGal");

            /* AREA ID VALIDO */
            Guid areaIdimg = Guid.NewGuid();
            if (lstAreasFilha.Items.Count >= 2)
            {
                areaIdimg = new System.Guid(lstAreasFilha.SelectedValue);
            }
            else
            {
                areaIdimg = new System.Guid(lstAreas.SelectedValue);
            }

            if (ckb.Checked == true)
            {
                _obj.parms = 4;
                Guid imgid = System.Guid.NewGuid();
                _obj.imagemid = imgid;
                _obj.areaid = areaIdimg;
                _obj.url = img.ImageUrl;
                _obj.descricao = txt.Text;
                _imgrepo.MakeConnection(_obj);
                _imgrepo.InsereImagemGaleria();
            }
        }
    }

    protected void ListaConteudoPorArea(Guid AreaId)
    {
        _obj.parms = 2;
        _obj.areaid = AreaId;
        _obj.ativos = 1;
        _conteudorepo.MakeConnection(_obj);
    }

    protected void CarregaAreas()
    {
        _obj.parms = 2;
        _obj.appid = Session["AplicacaoId"];// ((Aplicacao)Session["cliente"]).AplicacaoId;
        _obj.tpArea = null;
        _areasrepo.MakeConnection(_obj);
        lstAreas.Items.Clear();

        List<Areas> lst = _areasrepo.ListaAreasPai();

        foreach (Areas _area in lst)
        {
            ListItem _item = new ListItem(_area.Nome, _area.AreaId.ToString());
            lstAreas.Items.Add(_item);
        }
    }

    protected void MontaComboAreas()
    {
        _obj.parms = 2;
        _obj.appid = Session["AplicacaoId"];// ((Aplicacao)Session["cliente"]).AplicacaoId;
        _obj.tpArea = null;
        _areasrepo.MakeConnection(_obj);
        lstAreas.Items.Clear();

        lstAreas.Items.Add(new ListItem("Escolha a area"));
        foreach (Areas _area in _areasrepo.ListaAreasPai())
        {
            ListItem _item = new ListItem(_area.Nome, _area.AreaId.ToString());
            lstAreas.Items.Add(_item);
        }

        if (lstAreas.Items.Count > 1 && lstAreasFilha.Items.Count == 2)
        {
           // ListaConteudoPorArea(new Guid(lstAreas.Items[1].Value));
        }
    }

    protected void MontaComboAreasFilha(Guid AreaIdPai)
    {
        _obj.parms = 2;
        _obj.appid = Session["AplicacaoId"];// ((Aplicacao)Session["cliente"]).AplicacaoId;
        _obj.areaIdPai = AreaIdPai;
        _areasrepo.MakeConnection(_obj);
        lstAreasFilha.Items.Clear();


        lstAreasFilha.Items.Add(new ListItem("Escolha a area"));

        foreach (Areas _area in _areasrepo.ListaAreasFilha())
        {
            ListItem _item = new ListItem(_area.Nome, _area.AreaId.ToString());
            lstAreasFilha.Items.Add(_item);
        }

        if (lstAreasFilha.Items.Count > 1)
        {
            lstAreasFilha.Visible = true;
        }
        else
        {
            lstAreasFilha.Visible = false;
        }


    }

}
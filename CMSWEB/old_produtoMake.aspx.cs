using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;
using System.Dynamic;
using System.IO;

public partial class areasMake : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //_obj.banco = "SqlServer";
        btnareamk.Click += new EventHandler(btnareamk_Click);
        btnareaupd.Click += btnareaupd_Click;

        if (!IsPostBack)
        {
            ListaAreas();
            ListaCategorias();
        }
    }

    protected void ckbCustomPage_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox obj = (CheckBox)sender;
        if (obj.Checked)
        {
            lstUrl.Enabled = false;
        }
        else
        {
            lstUrl.Enabled = true;
        }
    }

    protected void gdrAreas_RowDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            
        }

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //    CheckBox imagem = (CheckBox)e.Row.Cells[2].Controls[0];
        //    imagem.Checked = ((Areas)e.Row.DataItem).Imagem;

        //    CheckBox tipo = (CheckBox)e.Row.Cells[3].Controls[0];
        //    tipo.Checked = ((Areas)e.Row.DataItem).MenuLateral;
        //}
    }
    
    protected void HabilitaImagem(object sender, EventArgs e)
    {
        pnlimagem.Visible = pnlimagem.Visible == true ? false : true;
    }

    protected void EditaLinha(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "InativarArea":
                InativaArea(new Guid(e.CommandArgument.ToString()));
                break;
            case "EditarArea":
                Editar(new Guid(e.CommandArgument.ToString()));
                break;
        }
    }

    protected void btnareamk_Click(object sender, EventArgs e)
    {
        CriaArea();
    }

    void btnareaupd_Click(object sender, EventArgs e)
    {
        EditarArea();
    }

    protected void CarregaDropDown()
    {
        lstTipo.DataSource = null;
        lstTipo.DataBind();

        var objx = (ExpandoObject)Session["Objeto"];
        _obj.parms = 0;
        _areasrepo.MakeConnection(_obj);

        lstTipo.DataSource = _areasrepo.ListaAreasPorTipo();
        lstTipo.DataBind();
    }

    protected void ListaAreas()
    {
        ResetaForm();
        var objx = (ExpandoObject)Session["Objeto"];
        _obj.parms = 2;
        _obj.appid = Session["Aplicacaoid"].ToString();
        _obj.tpArea = 8;

        lstareas.DataSource = null;
        lstareas.DataBind();

        gdrAreas.DataSource = null;
        gdrAreas.DataBind();

        List<Areas> lst = new List<Areas>();
        Areas x = Areas.ObterNovaArea();
        x.Descricao = "Escolha a area abaixo";
        lst.Add(x);
        _areasrepo.MakeConnection(_obj);
        List<Areas> lstP = _areasrepo.ListaAreasPai();

        foreach (Areas item in lstP )
        {
            lst.Add(item);
        }

        lstareas.DataSource = lst;// _areasrepo.ListaAreasPai();
        lstareas.DataBind();

        gdrAreas.DataSource = _areasrepo.ListaAreas();
        gdrAreas.DataBind();

    }

    protected void ListaCategorias()
    {
        _obj.parms = 1;
        _obj.appid = Session["AplicacaoId"];// ((Aplicacao)Session["cliente"]).AplicacaoId;
        _catrepo.MakeConnection(_obj);
        lstCategorias.Items.Clear();

        lstCategorias.Items.Add(new ListItem("Escolha a categoria"));
        foreach (Categoria cat in _catrepo.ListaCategoriaPai())
        {
            ListItem _item = new ListItem(cat.Nome,cat.CategoriaId.ToString());
            lstCategorias.Items.Add(_item);
        }
    }

    protected void ListaSubCategorias(Guid categoriaId)
    {
        _obj.parms = 1;
        //_obj.appid = Session["AplicacaoId"];// ((Aplicacao)Session["cliente"]).AplicacaoId;
        _obj.cpid = categoriaId;
        _catrepo.MakeConnection(_obj);

        lstSubCategorias.Items.Clear();
        lstSubCategorias.Items.Add(new ListItem("Escolha a subcategoria"));
        
        foreach (Categoria cat in _catrepo.ListaSubCategoria())
        {
            ListItem _item = new ListItem(cat.Nome, cat.CategoriaId.ToString());
            lstSubCategorias.Items.Add(_item);
        }

        if (lstSubCategorias.Items.Count > 1)
        {
            //ListaConteudoPorArea(new Guid(lstAreasFilha.Items[1].Value), dlConteudoSec);
            pnlCat.Visible = true;
            lstSubCategorias.Visible = true;
        }
        else
        {
            //dlConteudoSec.DataSource = null;
            //dlConteudoSec.DataBind();
            pnlCat.Visible = false;
            lstSubCategorias.Visible = false;
        }
    }

    protected void CriaArea()
    {
        _obj.parms = 11;
        Guid areaId = System.Guid.NewGuid();
        _obj.areaid = areaId;
        _obj.appid = Session["Aplicacaoid"].ToString();
        _obj.areaidpai = lstareas.SelectedIndex==0?"":lstareas.SelectedValue.ToString();
        _obj.nome = txtareaname.Text;

        /* DEFININDO A URL A SER CHAMADA */
        string _url = string.Empty;

        _url = lstUrl.SelectedValue;

        _obj.url = _url;
        _obj.descricao = txtareadesc.Text;
        _obj.imagem = null;

        /* VERIFICANDO QUAL O TIPO DE MENU */

        _obj.menulateral = (lstTipo.SelectedValue  == "0" ? true : false);
        _obj.menusplash  = (lstTipo.SelectedValue  == "1" ? true : false);
        _obj.menucentral = (lstTipo.SelectedValue  == "2" ? true : false);
        _obj.tipoArea = lstTipo.SelectedValue;

        _areasrepo.MakeConnection(_obj);
        _areasrepo.CriaNovaArea();

        ListaAreas();
    }

    protected void EditarArea()
    {
        _obj.parms = 10;
        Guid areaId = new Guid(hdnAreaId.Value);
        _obj.areaid = areaId;
        _obj.appid = Session["Aplicacaoid"].ToString();
        if (!string.IsNullOrEmpty(hdnAreaPaiId.Value))
        {
            Guid areaIdPai = new Guid(hdnAreaPaiId.Value);
            _obj.areaidpai = areaIdPai;
        }
        else
        {
            _obj.areaidpai = null;
        }

        _obj.nome = txtareaname.Text;

        /* DEFININDO A URL A SER CHAMADA */
        string _url = string.Empty;

        _url = lstUrl.SelectedValue;

        _obj.url = _url;
        _obj.descricao = txtareadesc.Text;
        _obj.imagem = null;

        /* VERIFICANDO QUAL O TIPO DE MENU */

        _obj.menulateral = (lstTipo.SelectedValue == "0" ? true : false);
        _obj.menusplash = (lstTipo.SelectedValue == "1" ? true : false);
        _obj.menucentral = (lstTipo.SelectedValue == "2" ? true : false);
        _obj.tipoArea = lstTipo.SelectedValue;

         _areasrepo.MakeConnection(_obj);
         _areasrepo.EditaArea();

        ListaAreas();
    }

    protected void InativaArea(Guid area)
    {
        _obj.parms = 1;
        _obj.areaid = area;
        _areasrepo.MakeConnection(_obj);
        _areasrepo.InativaArea();
        ListaAreas();
    }

    protected void Editar(Guid areaid)
    {
        _obj.parms = 1;
        _obj.areaid = areaid;
        _areasrepo.MakeConnection(_obj);
        Areas area = _areasrepo.ObtemAreaPorId();

        /* CARREGANDO VALORES NO FORM */
        txtareaname.Text = area.Nome;
        txtareadesc.Text = area.Descricao;
        if(!string.IsNullOrEmpty(area.Url) )
        {
            hdnAreaId.Value = area.AreaId.ToString();
            btnareaupd.Enabled = true;
        }
        lstTipo.SelectedValue = area.MenuLateral == true ? "0" : (area.Imagem == true ? "1" : null);
        
        /* CARREGANDO O ID DA AREA PARA GRAVAR O CONTEUDO [PRODUTO] */
        ucMakeProduto.LoadContent(area.AreaId.ToString());

    }

    protected void ResetaForm()
    {
        txtareaname.Text = string.Empty;
        txtareadesc.Text = string.Empty;
        lstTipo.SelectedIndex = 0;
    }
    protected void lstCategorias_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBox lb = (ListBox)sender;
        ListaSubCategorias(new System.Guid(lb.SelectedValue));
    }
}
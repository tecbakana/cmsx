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
    public event ChildDelegateEvent openModal;
    public event ChildDelegateEvent atualizaListas;
    public delegate void ChildDelegateEvent();

    protected void Page_Load(object sender, EventArgs e)
    {
        //_obj.banco = "SqlServer";
        btnareamk.Click += new EventHandler(btnareamk_Click);
        btnareaupd.Click += btnareaupd_Click;

        btnareamk.Enabled = true;
        btnareaupd.Enabled = false;

        if (!IsPostBack)
        {
            ListaAreas();
            VarreDiretorio();
        }
    }

    protected void ckbCustomPage_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox obj = (CheckBox)sender;
        if (obj.Checked)
        {
            lstUrl.Enabled = false;
            lstPaginas.Enabled = true;
        }
        else
        {
            lstUrl.Enabled = true;
            lstPaginas.Enabled = false;
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
        CheckBox ckb = (CheckBox)sender;
        if (ckb.Checked)
        {
            this.mpe.Show();
        }

        if (Session["editando"] != null)
        {
            btnareamk.Enabled = false;
            btnareaupd.Enabled = true;
        }

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

    protected void btnareaupd_Click(object sender, EventArgs e)
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
        _obj.appid = Session["AplicacaoId"];
        _obj.tpArea = null;
        _areasrepo.MakeConnection(_obj);

        lstareas.DataSource = null;
        lstareas.DataBind();

        //gdrAreas.DataSource = null;
        //gdrAreas.DataBind();

        rptAreas.DataSource = null;
        rptAreas.DataBind();

        List<Areas> lst = new List<Areas>();
        Areas x = Areas.ObterNovaArea();
        x.Descricao = "Escolha a area abaixo";
        lst.Add(x);
        foreach (Areas item in _areasrepo.ListaAreasPai())
        {
            lst.Add(item);
        }

        lstareas.DataSource = lst;// _areasrepo.ListaAreasPai();
        lstareas.DataBind();

        //gdrAreas.DataSource = _areasrepo.ListaAreas();
        //gdrAreas.DataBind();

        rptAreas.DataSource = _areasrepo.ListaAreas();
        rptAreas.DataBind();
        
        //-------------------- LISTANDO CATEGORIAS
        //_catrepo.MakeConnection(_obj);
        //lstCategoria.DataSource = _catrepo.ListaCategoria();
        //lstCategoria.DataBind();

    }

    protected void CriaArea()
    {
        _obj.parms = 12;
        Guid areaId = System.Guid.NewGuid();
        _obj.areaid = areaId;
        _obj.appid = Session["Aplicacaoid"].ToString();
        _obj.areaidpai = lstareas.SelectedIndex==0?"":lstareas.SelectedValue.ToString();
        _obj.nome = ckbhome.Checked == true ? "keyhome_" + txtareaname.Text : txtareaname.Text;

        /* DEFININDO A URL A SER CHAMADA */
        string _url = string.Empty;

        if (ckbCustomPage.Checked)
        {
            _url = lstPaginas.SelectedValue;
        }
        else
        {
            _url = lstUrl.SelectedValue;
        }

        _obj.url = _url;
        _obj.descricao = txtareadesc.Text;
        _obj.imagem = ckbimagem.Checked;
        _obj.posicao = lstareas.Items.Count-1;

        /* VERIFICANDO QUAL O TIPO DE MENU */

        _obj.menulateral = (lstTipo.SelectedValue  == "0" ? true : false);
        _obj.menusplash  = (lstTipo.SelectedValue  == "1" ? true : false);
        _obj.menucentral = (lstTipo.SelectedValue  == "2" ? true : false);
        _obj.tipoArea = lstTipo.SelectedValue;

        _areasrepo.MakeConnection(_obj);
        _areasrepo.CriaNovaArea();

        /* INCLUSAO DE DADOS DA IMAGEM QUANDO NECESSÁRIO */
        if (ckbimagem.Checked)
        {
            _obj.parms = 5;
            _obj.url = imgurl.Value;
            _obj.altura = "";// imghgt.Text;
            _obj.largura = "";//imgwdt.Text;
            _obj.areaid = areaId;
            _obj.conteudoid = null;

            _imgrepo.MakeConnection(_obj);
            _imgrepo.CriaNovaImagem();
        }

        ListaAreas();
        ResetaForm();
    }

    protected void EditarArea()
    {
        _obj.parms = 11;
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

        _obj.nome = ckbhome.Checked == true ? "keyhome_" + txtareaname.Text : txtareaname.Text;

        /* DEFININDO A URL A SER CHAMADA */
        string _url = string.Empty;

        if (ckbCustomPage.Checked)
        {
            _url = lstPaginas.SelectedValue;
        }
        else
        {
            _url = lstUrl.SelectedValue;
        }

        _obj.url = _url;
        _obj.descricao = txtareadesc.Text;
        _obj.imagem = ckbimagem.Checked;
        _obj.posicao = hdnPosicao.Value;

        /* VERIFICANDO QUAL O TIPO DE MENU */

        _obj.menulateral = (lstTipo.SelectedValue == "0" ? true : false);
        _obj.menusplash = (lstTipo.SelectedValue == "1" ? true : false);
        _obj.menucentral = (lstTipo.SelectedValue == "2" ? true : false);

         _areasrepo.MakeConnection(_obj);
         _areasrepo.EditaArea();

        /* INCLUSAO DE DADOS DA IMAGEM QUANDO NECESSÁRIO */
        if (ckbimagem.Checked)
        {
            _obj.parms = 5;
            _obj.imagemId = hdnImages.Value;
            _obj.altura = "";// imghgt.Text;
            _obj.largura = "";//imgwdt.Text;
            _obj.areaid = areaId;
            _obj.conteudoid = null;

            _imgrepo.MakeConnection(_obj);
            _imgrepo.AtualizaGaleria();
        }

        ListaAreas();
        ResetaForm();
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
        Session["editando"] = true;
        btnareamk.Enabled = false;
        btnareaupd.Enabled = true;
        _obj.parms = 1;
        _obj.areaid = areaid;
        _areasrepo.MakeConnection(_obj);
        Areas area = _areasrepo.ObtemAreaPorId();

        /* CARREGANDO VALORES NO FORM */
        txtareaname.Text = area.Nome;
        txtareadesc.Text = area.Descricao;
        if(!string.IsNullOrEmpty(area.Url) )
        {
            lstUrl.SelectedValue = area.Url;
            lstPaginas.SelectedValue = area.Url;
            hdnAreaId.Value = area.AreaId.ToString();
            lstPaginas.Enabled = true;
            btnareaupd.Enabled = true;
        }

        hdnPosicao.Value = area.Posicao.ToString();
        lstTipo.SelectedValue = area.TipoMenu;//area.MenuLateral == true ? "0" : (area.Imagem == true ? "1" : null);
    }

    protected void ResetaForm()
    {
        txtareaname.Text = string.Empty;
        txtareadesc.Text = string.Empty;
        ckbimagem.Checked = false;
        lstTipo.SelectedIndex = 0;
        pnlimagem.Visible = false;

        btnareamk.Enabled = true;
        btnareaupd.Enabled = false;

        Session["editando"] = false;
    }

    protected void VarreDiretorio()
    {
        DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/"));
        FileInfo[] rgFiles = di.GetFiles("*.aspx",SearchOption.TopDirectoryOnly);

        lstPaginas.Items.Add(new ListItem("Defina a pagina"));
        foreach (FileInfo fi in rgFiles)
        {
            if (!fi.Name.Contains("ake"))
            {
                lstPaginas.Items.Add(new ListItem(fi.Name, fi.Name));
            }
        }
    }

    protected void EditarRepeater(object source, RepeaterCommandEventArgs e)
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

    protected void ckbimagem_CheckedChanged(object sender, EventArgs e)
    {
        mpe.Show();
        btnareamk.Enabled = false;
        btnareaupd.Enabled = true;
    }
}
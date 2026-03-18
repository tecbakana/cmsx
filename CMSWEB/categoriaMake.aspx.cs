using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;
using System.Dynamic;
using System.IO;

public partial class CategoriaMake : BasePage
{

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
        //_obj.banco = "SqlServer";
        
        btnMake.Click += new EventHandler(btnMake_Click);
        btnUpdate.Click += btnMake_Click;

        if (!IsPostBack)
        {
            ResetaForm();
            ListaCategorias();
        }
    }

    protected void gdrCategoria_RowDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            
        }

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //    CheckBox imagem = (CheckBox)e.Row.Cells[2].Controls[0];
        //    imagem.Checked = ((Categoria)e.Row.DataItem).Imagem;

        //    CheckBox tipo = (CheckBox)e.Row.Cells[3].Controls[0];
        //    tipo.Checked = ((Categoria)e.Row.DataItem).MenuLateral;
        //}
    }
    

    protected void EditaLinha(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "InativarArea":
                InativaCategoria(new Guid(e.CommandArgument.ToString()));
                break;
            case "EditarArea":
                Editar(new Guid(e.CommandArgument.ToString()));
                break;
        }
    }

    protected void btnMake_Click(object sender, EventArgs e)
    {
        CriaCategoria();
    }

    void btnUpdate_Click(object sender, EventArgs e)
    {
        EditarCategoria();
    }

    protected void ListaCategorias()
    {
        var objx = (ExpandoObject)Session["Objeto"];
        _obj.parms = 1;
        _obj.appid = Session["Aplicacaoid"].ToString();
        _catrepo.MakeConnection(_obj);

        //lstCategorias.DataSource = null;
        //lstCategorias.DataBind();

        gdrCategoria.DataSource = null;
        gdrCategoria.DataBind();

        List<Categoria> lst = new List<Categoria>();
        Categoria x = Categoria.ObterNovaCategoria();
        x.Descricao = "Escolha a area abaixo";
        lst.Add(x);
        foreach (Categoria item in _catrepo.ListaCategoriaPai())
        {
            lst.Add(item);
        }

        //lstCategorias.DataSource = lst;// _Categoriarepo.ListaCategoriaPai();
        //lstCategorias.DataBind();

        gdrCategoria.DataSource = _catrepo.ListaCategoria();
        gdrCategoria.DataBind();

        //if (lstCategorias.Items.Count >= 1)
        //{
        //    lstCategorias.SelectedIndex = -1;
        //}

    }

    protected void CriaCategoria()
    {
        _obj.parms = 6;

        Guid catId = System.Guid.NewGuid();

        _obj.categoriaid = catId;
        _obj.nome= txtCatNome.Text;
        _obj.descricao = txtCatDesc.Text;
        _obj.TipoCategoria = 0;
        _obj.aplicacaoId = Session["Aplicacaoid"].ToString();
        _obj.categoriaIdPai = null;// lstCategorias.SelectedIndex == 0 ? null : lstCategorias.SelectedValue.ToString();

        _catrepo.MakeConnection(_obj);
        _catrepo.CriaNovaCategoria();

        ResetaForm();
        ListaCategorias();
    }

    protected void EditarCategoria()
    {
        //_obj.parms = 10;
        //Guid areaId = new Guid(hdnAreaId.Value);
        //_obj.areaid = areaId;
        //_obj.appid = Session["Aplicacaoid"].ToString();
        //if (!string.IsNullOrEmpty(hdnAreaPaiId.Value))
        //{
        //    Guid areaIdPai = new Guid(hdnAreaPaiId.Value);
        //    _obj.areaidpai = areaIdPai;
        //}
        //else
        //{
        //    _obj.areaidpai = null;
        //}


        ///* DEFININDO A URL A SER CHAMADA */
        //string _url = string.Empty;

        //if (ckbCustomPage.Checked)
        //{
        //    _url = lstPaginas.SelectedValue;
        //}
        //else
        //{
        //    _url = lstUrl.SelectedValue;
        //}

        //_obj.url = _url;
        //_obj.descricao = txtareadesc.Text;
        //_obj.imagem = ckbimagem.Checked;

        ///* VERIFICANDO QUAL O TIPO DE MENU */

        //_obj.menulateral = (lstTipo.SelectedValue == "0" ? true : false);
        //_obj.menusplash = (lstTipo.SelectedValue == "1" ? true : false);
        //_obj.menucentral = (lstTipo.SelectedValue == "2" ? true : false);

        // _Categoriarepo.MakeConnection(_obj);
        // _Categoriarepo.EditaArea();

        ///* INCLUSAO DE DADOS DA IMAGEM QUANDO NECESSÁRIO */
        //if (ckbimagem.Checked)
        //{
        //    _obj.parms = 5;
        //    _obj.url = imgurl.Value;
        //    _obj.altura = "";// imghgt.Text;
        //    _obj.largura = "";//imgwdt.Text;
        //    _obj.areaid = areaId;
        //    _obj.conteudoid = null;

        //    _imgrepo.MakeConnection(_obj);
        //    _imgrepo.CriaNovaImagem();
        //}

        //ListaCategoria();
    }

    protected void InativaCategoria(Guid area)
    {
        //_obj.parms = 1;
        //_obj.areaid = area;
        //_Categoriarepo.MakeConnection(_obj);
        //_Categoriarepo.InativaArea();
        //ListaCategoria();
    }

    protected void Editar(Guid areaid)
    {
        //_obj.parms = 1;
        //_obj.areaid = areaid;
        //_Categoriarepo.MakeConnection(_obj);
        //Categoria area = _Categoriarepo.ObtemAreaPorId();

        ///* CARREGANDO VALORES NO FORM */
        //txtareaname.Text = area.Nome;
        //txtareadesc.Text = area.Descricao;
        //if(!string.IsNullOrEmpty(area.Url) )
        //{
        //    lstPaginas.SelectedValue = area.Url;
        //    hdnAreaId.Value = area.AreaId.ToString();
        //    lstPaginas.Enabled = true;
        //    btnareaupd.Enabled = true;
        //}
        //lstTipo.SelectedValue = area.MenuLateral == true ? "0" : (area.Imagem == true ? "1" : null);

    }

    protected void ResetaForm()
    {
        txtCatNome.Text = string.Empty;
        txtCatDesc.Text = string.Empty;
    }

}
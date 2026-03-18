using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using CMSXBLL;

public partial class controles_ucMakeProduto : BaseControl
{
    private string _areaId;
    private bool _tipoAcaoValor=false;

    public event ChildDelegateEvent AtualizaLista;
    public event ChildDelegateEvent editaProduto;
    public delegate void ChildDelegateEvent(string[] parm);

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnEditar.Enabled = false;
            btnSalvar.Enabled = true;
            ListaCategorias();
            ListaUnidades();
        }
    }

    #region eventos
    protected void btnSalvar_Click(object sender, EventArgs e)
    {        
        if((ddlUnidade.SelectedIndex!=0)||(!string.IsNullOrEmpty(txtPreco.Text)))
        {
            switch (((Button)sender).CommandArgument)
            {
                case "edit":
                    EditContent();
                    break;
                case "save":
                    SaveContent();
                    break;
            }
        }
    }

    protected void listaSubCategoria(object sender, EventArgs e)
    {
        ListBox lb = (ListBox)sender;
        if(lb.SelectedValue!="-1")
        ListaSubCategorias(new System.Guid(lb.SelectedValue));
    }
    #endregion

    #region metodos
    public void LoadContent(string ProdutoId)
    {
        /*Session["areaid"] = AreaId;
        if (IsPostBack)
        {
            _obj.parms = 1;
            _obj.prodId =
            _areaId = ProdutoId;

            _prodrepo.MakeConnection(_obj);
            List<Produto> prod = _prodrepo.ListaProdutoXId();
            List<Conteudo> lstConteudo = _conteudorepo.ListaConteudoPorAreaId();
            if (lstConteudo.Count() >= 1)
            {
                txtNomeProduto.Value = lstConteudo[0].Titulo;
                txtDescricao.Value = lstConteudo[0].Texto.ToString();
                hdnAreaId.Value = lstConteudo[0].AreaId.ToString();
                if (!string.IsNullOrEmpty(lstConteudo[0].UnidadeId))
                {
                    ddlUnidade.SelectedValue = lstConteudo[0].UnidadeId.ToString();
                    txtPreco.Text = lstConteudo[0].Valor.ToString();
                    _tipoAcaoValor = true;
                }

                Session["idConteudo"] = lstConteudo[0].ConteudoId.ToString();

                btnEditar.Enabled = true;
                btnSalvar.Enabled = false;
            }
            else
            {
                btnEditar.Enabled = false;
                btnSalvar.Enabled = true;
            }
        }*/
    }

    protected void SaveContent()
    {

        Produto p = new Produto();
        p.ProdutoId = System.Guid.NewGuid();
        p.AplicacaoId = new System.Guid(Session["AplicacaoId"].ToString());
        p.CategoriaId = new System.Guid(lstSubCategorias.SelectedValue);
        p.Nome = txtNomeProduto.Value;
        p.Descricao = hd.Value;
        p.DescricaoCurta = txtShortDesc.Value;
        p.DetalheTecnico = hs.Value;
        p.Sku = txtSku.Text;
        p.Valor = decimal.Parse(txtPreco.Text);
        p.Destaque = chkDestaque.Checked ? 1 : 0;
        p.PagSeguroBotao = string.IsNullOrEmpty(txtPagSeguroBotao.Value) ? "" : txtPagSeguroBotao.Value;
        p.Tipo = int.Parse(ddlUnidade.SelectedValue);
        _obj.parms = 0;
        _prodrepo.MakeConnection(_obj);
        _prodrepo.CriaProduto(p);

       // -----------------------------------------------
        string[] parm = (p.ProdutoId.ToString() + ";" + p.Nome).Split(';');
        Session["prodId"] = parm[0];
        editaProduto(parm);
    }

    protected void EditContent()
    {


    }

    protected void ListaCategorias()
    {
        _obj.parms = 1;
        _obj.appid = Session["AplicacaoId"].ToString();
        _catrepo.MakeConnection(_obj);
        lstCategorias.Items.Clear();

        lstCategorias.Items.Add(new ListItem("Escolha a categoria","-1"));
        foreach (Categoria cat in _catrepo.ListaCategoriaPai())
        {
            ListItem _item = new ListItem(cat.Nome, cat.CategoriaId.ToString());
            lstCategorias.Items.Add(_item);
        }
    }

    protected void ListaSubCategorias(Guid categoriaId)
    {
        
        _obj.parms = 1;
        _obj.cpid = categoriaId.ToString();
        _catrepo.MakeConnection(_obj);

        lstSubCategorias.Items.Clear();
        lstSubCategorias.Items.Add(new ListItem("Escolha a subcategoria","-1"));

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
            pnlCat.Visible = true;
            lstSubCategorias.Visible = false;
        }

        //Render the function invocation. 
        string funcCall = "loadSummernote()";
        Type tp = this.GetType();
        string skey = "ldsum";
        ClientScriptManager cl = this.Page.ClientScript;
        if (!cl.IsClientScriptBlockRegistered(tp, skey))
        {
            cl.RegisterClientScriptBlock(tp, skey, funcCall);
        }
    }

    protected void ListaUnidades()
    {
        _obj.parms = 0;
        _undrepo.MakeConnection(_obj);
        ddlUnidade.Items.Clear();

        ddlUnidade.Items.Add(new ListItem("Escolha a unidade"));
        foreach (Unidade und in _undrepo.ListaUnidade())
        {
            ListItem _item = new ListItem(und.sigla, und.unidadeId.ToString());
            ddlUnidade.Items.Add(_item);
        }
    }

    private void AtribuiValor(Guid localConteudoId)
    {
        if (_tipoAcaoValor == false)//cria valor
        {
            _obj.parms = 3;
            _obj.conteudoid = localConteudoId;
            _obj.unidadeid = ddlUnidade.SelectedValue;
            _obj.valor = txtPreco.Text.Replace(',','.');
            _conteudorepo.MakeConnection(_obj);
            _conteudorepo.CreateValue();
        }
        else//atualiza valor
        {
            _obj.parms = 3;
            _obj.conteudoid = localConteudoId;
            _obj.unidadeid = ddlUnidade.SelectedValue;
            _obj.valor = txtPreco.Text.Replace(',','.');
            _conteudorepo.MakeConnection(_obj);
            _conteudorepo.EditValue();
        }

    }

    private void LoadAttrControl(string ProdutoId)
    {
        
    }

    protected void AddCategoria(object sender, EventArgs e)
    {
        _obj.parms = 1;
        LinkButton lkbt = (LinkButton)sender;

        Guid catId = System.Guid.NewGuid();
        Categoria cat = new Categoria();
        cat.CategoriaId = System.Guid.NewGuid();
        cat.TipoCategoria = 0;
        cat.AplicacaoId = new System.Guid(Session["Aplicacaoid"].ToString());

        if (lkbt.CommandName == "adCatSub")
        {
            if (lstCategorias.SelectedIndex > 0)
            {
                Guid cpai = new System.Guid(lstCategorias.SelectedValue.ToString());
                cat.CategoriaIdPai = cpai;
                cat.Nome = txtNomeSubCategoria.Value;
                cat.Descricao = txtNomeSubCategoria.Value;
                _obj.categoria = cat;
                _catrepo.MakeConnection(_obj);
                _catrepo.CategoriaRapida();
                ListaSubCategorias(cpai);
            }
        }
        else
        {
            cat.Nome = txtNomeCategoria.Value;
            cat.Descricao = txtNomeCategoria.Value;
            _obj.categoria = cat;
            _catrepo.MakeConnection(_obj);
            _catrepo.CategoriaRapida();
            ListaCategorias();
        }




        ////-------------------------------------------------//


    }

    #endregion
}
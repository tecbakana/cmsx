using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using CMSXBLL;

public partial class controles_ucListaProduto : BaseControl
{
    private string _areaId;
    private bool _tipoAcaoValor=false;
    public event ChildDelegateEvent editaProduto;
    public delegate void ChildDelegateEvent(string[] parm);

    protected override void OnInit(EventArgs e)
    {
        grdProduto.RowDataBound += grdProduto_RowDataBound;
        grdProduto.RowCommand += grdProduto_RowCommand;
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListaProduto();
        }
    }

    protected void grdProduto_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow gr = e.Row;
            CheckBox dest = (CheckBox)e.Row.Cells[3].Controls[1];
            dest.Checked = (((Produto)e.Row.DataItem).Destaque == 0 ? false : true);
        } 
    }

    protected void grdProduto_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "InserirAtributos":
                //carregar o id do produto no controle de atributos
                //habilitar a tela de atributos
                if (editaProduto != null)
                {
                    string[] parm = e.CommandArgument.ToString().Split(';');
                    Session["prodId"] = parm[0];
                    editaProduto(parm);
                }
                break;
            case "InativaAtributo":
                string[] obj = e.CommandArgument.ToString().Split(';');
                InativaProduto(obj[0]);
                break;
        }
    }

    #region eventos

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

    protected void ListaProduto()
    {
        _obj.parms = 1;
        _obj.appid = Session["AplicacaoID"].ToString();
        _prodrepo.MakeConnection(_obj);

        grdProduto.DataSource = _prodrepo.ListaProduto();
        
        grdProduto.DataBind();
    }

    public void AtulalizaLista()
    {
        ListaProduto();
    }

    private void LoadAttrControl(string ProdutoId)
    {
        
    }

    protected void addAtributos(string produtoId)
    {


        //Render the function invocation. 
        string funcCall = "callTab('atributos')";
        Type tp = this.GetType();
        string skey = "CallTabAttrib";
        ClientScriptManager cl = this.Page.ClientScript;
        if (!cl.IsClientScriptBlockRegistered(tp,skey))
        {
            //cl.RegisterStartupScript(tp, skey, funcCall, true);
            cl.RegisterClientScriptBlock(tp, skey, funcCall);
        }

    }

    protected void InativaProduto(string id)
    {
        Produto p = new Produto();
        p.ProdutoId = new System.Guid(id);
        _obj.parms = 0;
        _prodrepo.MakeConnection(_obj);
        _prodrepo.EditaProduto(p);
    }
    #endregion
}
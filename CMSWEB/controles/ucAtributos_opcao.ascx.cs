using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using CMSXBLL;

public partial class controles_ucAtributos_opcao : BaseControl
{
    #region PROPRIEDADES

    private List<Atributo> lsat = new List<Atributo>();
    private List<Opcao> lsop = new List<Opcao>();

    #endregion

    #region EVENTOS

    protected void Page_Load(object sender, EventArgs e)
    {
        gdrAtributos.RowCommand += gdrAtributos_RowCommand;
    }

    protected void gdrAtributos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddOpt")
        {
            Session["AtributoId"] = e.CommandArgument;
            LoadOpcoes();
            pnlOpcao.Visible = true;
        }
    }
    
    protected void AddAtributo(object sender, EventArgs e)
    {
        Atributo at = new Atributo();
        at.Nome = txtNomeAtributo.Value;
        at.Descricao = txtDescricaoAtributo.Value;
        at.ProdutoId = new System.Guid(Session["prodId"].ToString());

        _obj.parms = 0;
        _attrepo.MakeConnection(_obj);

        _attrepo.CriaAtributo(at);

        //-------------------------------------------------//

        LoadAtributos(string.Empty);
    }

    protected void AddOpcao(object sender, EventArgs e)
    {
        Opcao op = new Opcao();
        op.Nome         = txtNomeOpcao.Value;
        op.Descricao    = txtDescricaoOpcao.Value;
        op.AtributoId   = Guid.Parse(Session["AtributoId"].ToString());
        op.OpcaoId      =  System.Guid.NewGuid();
        if (ckbGerenciaEstoque.Checked)
        {
            op.Estoque = true;
            op.Qtd = int.Parse(txtQtdEstoque.Value);
        }

        _obj.parms = 0;
        _opcrepo.MakeConnection(_obj);

        _opcrepo.CriaOpcao(op);

        ////-------------------------------------------------//

        LoadOpcoes();
    }

    #endregion

    #region METODOS

    public void LoadAtributos(string atr)
    {
        gdrAtributos.DataSource = null;
        gdrAtributos.DataBind();

        string pid = string.IsNullOrEmpty(atr)?Session["prodId"].ToString():atr;

        if (!string.IsNullOrEmpty(atr))
        {
            _obj.parms = 1;
            _obj.prodId = pid;
            _attrepo.MakeConnection(_obj);

            lsat = _attrepo.ListaAtributoXProduto();
        }
        else
        {
            lsat = null;
        }

        gdrAtributos.DataSource = lsat;
        gdrAtributos.DataBind();

        //-----------------------------------------
        //LoadOpcoes();

    }

    public void LoadOpcoes()
    {
        grdOpcoes.DataSource = null;
        grdOpcoes.DataBind();

        string aid = Session["AtributoId"].ToString();

        if (!string.IsNullOrEmpty(aid))
        {
            _obj.parms = 1;
            _obj.atrId = aid;
            _opcrepo.MakeConnection(_obj);

            lsop = _opcrepo.ListaOpcao();
        }
        else
        {
            lsop = null;
        }

        grdOpcoes.DataSource = lsop;
        grdOpcoes.DataBind();

        updOpc.Update();
    }

    #endregion
}
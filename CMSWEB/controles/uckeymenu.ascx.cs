using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;

public partial class controles_uckeymenu : BaseControl
{
    public string keylocation { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        CarregaMenu();
    }

    protected void CarregaMenu()
    {
        _obj.parms = 2;
        _app = (Aplicacao)Session["cliente"];
        _obj.keylocation = this.keylocation;
        _obj.appid = _app.AplicacaoId;
        _areasrepo.MakeConnection(_obj);

        List<Areas> _lst = _areasrepo.ListaAreaMenuLateral();

        foreach (Areas _area in _lst)
        {
            _area.Descricao = string.IsNullOrEmpty(_area.Descricao) ? "--" : _area.Descricao.Substring(0, (_area.Descricao.Length < 20 ? _area.Descricao.Length : 20));
        }


        lstMenuLateral.DataSource = _lst;
        lstMenuLateral.DataBind();
    }

    protected string CarregaResenha(Guid AreaId)
    {
        string resenha = string.Empty;

        _obj.parms = 2;
        _obj.areaid = AreaId;
        _obj.ativos = 1;

        _conteudorepo.MakeConnection(_obj);
        List<Conteudo> conteudos = _conteudorepo.ListaConteudoPorAreaId();

        if (conteudos.Count == 1)
        {
            resenha = conteudos[0].Texto.Substring(0, 50) + "...";
        }
        return resenha;
    }
}
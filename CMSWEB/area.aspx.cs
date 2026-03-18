using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;

public partial class area : BasePage
{
    public string areaId;
    public string firstSon;

    protected void Page_Load(object sender, EventArgs e)
    {

        _app = (Aplicacao)Session["cliente"];
        ListaAreas();
        MostraConteudo();
    }

    public void ListaAreas()
    {
        _obj.parms = 2;
        _obj.appid =  _app.AplicacaoId;
        _obj.areaIdPai = !string.IsNullOrEmpty(Request.QueryString["local"]) ? AreaIdPai(new System.Guid(areaId)).ToString() : Request.QueryString["AreaId"];

        _areasrepo.MakeConnection(_obj);
        List<Areas> _filhos = _areasrepo.ListaAreasFilha();
        lstAreas.DataSource = _filhos;
        lstAreas.DataBind();

        /* disponibilizando o id do primeiro item da lista para uso eventual */
        if (_filhos.Count > 0)
        {
            firstSon = _filhos[0].AreaId.ToString();
        }
    }

    public Guid? AreaIdPai(Guid areaid)
    {
        _obj.parms = 2;
        Aplicacao _app = (Aplicacao)Session["cliente"];
        _obj.appid = _app.AplicacaoId;
        _obj.areaid = areaid;
        _areasrepo.MakeConnection(_obj);
        Areas areapai = _areasrepo.ObtemAreaPorId();
        return areapai.AreaIdPai;
    }

    protected void MostraConteudo()
    {
        _obj.parms = 2;
        Aplicacao _app = (Aplicacao)Session["cliente"];
        _obj.areaid = firstSon;// ((Control)menuarea.FindControl("menuareas")).firstSon;
        _obj.ativos = 1;
        _conteudorepo.MakeConnection(_obj);
        List<Conteudo> _conteudo = _conteudorepo.ListaConteudoPorAreaId();
        titulo.Text = _conteudo[0].Titulo;
        texto.Text = _conteudo[0].Texto;
        AdRotatorFlex(_conteudo[0].ConteudoId);
        //if (!string.IsNullOrEmpty(_conteudo[0].UrlImg))
        //{
        //    imgConteudo.ImageUrl = "images/" + _conteudo[0].UrlImg;
        //    imgConteudo.Width = 500;
        //}
        //else
        //{
        //    imgConteudo.Visible = false;
        //}

    }

    protected void AdRotatorFlex(Guid _conteudoid)
    {
        //Control uc = LoadControl("controles/ucadrotator.ascx");
        //uc.ID = "adrotator";
        //img.Controls.Add(uc);
        // ((ASP.controles_ucadrotator_ascx)img.FindControl("adrotator")).RotatorConfig = GetObjectList(_conteudoid);
        adrotator.RotatorConfig = GetObjectList(_conteudoid);
    }

    public List<Imagem> GetObjectList(Guid conteudoId)
    {

        _obj.parms = 1;
        _obj.conteudoid = conteudoId;
        _imgrepo.MakeConnection(_obj);
        return _imgrepo.GaleriaConteudo();
    }
}
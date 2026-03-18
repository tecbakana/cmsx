using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;

public partial class home : BasePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MostraConteudo();
    }

    protected void MostraConteudo()
    {
        /* recuperando a area Home */
        _obj.parms = 1;
        _obj.appid = ((Aplicacao)Session["cliente"]).AplicacaoId;
        _areasrepo.MakeConnection(_obj);

        List<Areas> areas = _areasrepo.ListaAreas();
        string idArea = string.Empty;
        foreach (Areas area in areas)
        {
            if (area.Nome.ToLower() == "home")
            {
                idArea = area.AreaId.ToString();
            }
        }

        _obj.parms = 2;
        Aplicacao _app = (Aplicacao)Session["cliente"];
        _obj.areaid = idArea;
        _obj.ativos = 1;
        _conteudorepo.MakeConnection(_obj);
        List<Conteudo> _conteudo = _conteudorepo.ListaConteudoPorAreaId();
        lstConteudo.DataSource = _conteudo;
        lstConteudo.DataBind();
        AdRotatorFlex(_conteudo[0].ConteudoId);

        //if (_conteudo.Count > 0)
        //{
        //    if (!string.IsNullOrEmpty(_conteudo[0].UrlImg))
        //    {
        //        imgConteudo.ImageUrl = "images/" + _conteudo[0].UrlImg;
        //        imgConteudo.Width = 500;
        //    }
        //    else
        //    {
        //        imgConteudo.Visible = false;
        //    }
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
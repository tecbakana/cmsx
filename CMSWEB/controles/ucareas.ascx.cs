using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;

public partial class controles_ucareas : BaseControl
{
    public string areaId;
    public string firstSon;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["AreaId"]))
        {
            ListaAreas();
        }
    }
    
    public void ListaAreas()
    {
        _obj.parms = 2;
        Aplicacao _app = (Aplicacao)Session["cliente"];
        _obj.appid = _app.AplicacaoId;
        _obj.areaIdPai = !string.IsNullOrEmpty(Request.QueryString["local"])?AreaIdPai(new System.Guid(areaId)).ToString():Request.QueryString["AreaId"];

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
}
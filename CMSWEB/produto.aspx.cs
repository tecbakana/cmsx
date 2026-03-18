using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;

public partial class produto : BasePage
{
    public string _areaid;
    public string _conteudoid;

    protected void Page_Load(object sender, EventArgs e)
    {
       // MostraConteudo();
        _areaid = Request.QueryString["AreaId"];
    }

    /*protected void MostraConteudo()
    {
        _obj.parms = 2;
        Aplicacao _app = (Aplicacao)Session["cliente"];
        _obj.areaid = Request.QueryString["AreaId"];
        _obj.ativos = 1;
        _conteudorepo.MakeConnection(_obj);
        List<Conteudo> _conteudo = _conteudorepo.ListaConteudoPorAreaId();


        if (_conteudo.Count > 0)
        {
            lblTitulo.Text = _conteudo[0].Titulo;
            lblTexto.Text = _conteudo[0].Texto;
            if (!string.IsNullOrEmpty(_conteudo[0].UnidadeId.ToString()))
            {

            }
        }
    }*/
}
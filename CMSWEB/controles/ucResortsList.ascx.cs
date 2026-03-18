using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;

public partial class controles_ucResortsList : BaseControl
{
    /* PROPERTIES */
    public int _cidadeId { get; set; }
    public List<RoteiroBLL> _detrot;
    public List<ResortBLL> _detres;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if ((string.IsNullOrEmpty(Request.QueryString["fornId"])) &&(string.IsNullOrEmpty(Request.QueryString["cidorig"])))
        {
            ListaResorts();
        }
        else if ((!string.IsNullOrEmpty(Request.QueryString["fornId"])&&(string.IsNullOrEmpty(Request.QueryString["cidorig"]))))
        {
                dlResorts.Visible = false;
                pnlDetalhe.Visible = true;
                ltTitulo.Text = "Título";
                ltConteudo.Text = "Este é o conteúdo";
                /* LISTANDO OS ROTEIROS DO ITEM SELECIONADO */
                ListaRoteirosPorFornIdCidadeId();
                ListaDetalheRoteiro();
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["cidorig"]))
        {
             /* -- TO DO   */
            dlResorts.Visible = false;
            pnlDetalhe.Visible = false;
            pnlDetalheResort.Visible = true;
            carregaDetalhe();

        }
    }

    protected void gdrDetalhe_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        throw new NotImplementedException();
    }

    protected void gdrDetalhe_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddl = (DropDownList)e.Row.Cells[0].FindControl("ddlRot");
            ddl.Attributes.Add("onChange", "javascript:return gotoPage('" + ddl.ClientID + "',this.options[this.options.selectedIndex].value);");

            int idrot = int.Parse(((HiddenField)e.Row.Cells[0].FindControl("hdnIdRot")).Value);

            var lst = (from dr in _detrot
                        where (dr.idTabrot == idrot)
                        select new
                        {
                            idcido = dr.idCidOrig + ";" + idrot,
                            cdorg = dr.cidadeorigem
                        }).Distinct();

            ddl.DataTextField = "cdorg";
            ddl.DataValueField = "idcido";
            ddl.DataSource = lst;
            ddl.DataBind();
        }
    }

    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    /* METHODS */
    public void ListaResorts()
    {
        dlResorts.DataSource = null;
        dlResorts.DataBind();

        _obj.parms = 6;
        _obj.appid = _app.AplicacaoId;
        _obj.clienteId = 2;
        _obj.tabelaid = 1;
        _obj.cidadeId = Request.QueryString["cidadeid"];
        _obj.fornecedorId = "";
        _obj.tiporoteiro = "RESORTS";

        _rotrepo.MakeConnection(_obj);
        List<RoteiroBLL> _roteiros = _rotrepo.ListaRoteiroPorFornecedor(2);
        dlResorts.DataSource = _roteiros;
        dlResorts.DataBind();
    }

    public void ListaRoteirosPorFornIdCidadeId()
    {
        _obj.parms = 6;
        _obj.appid = _app.AplicacaoId;
        _obj.clienteId = 2;
        _obj.tabelaid = 1;
        _obj.cidadeId = Request.QueryString["cidadeid"];
        _obj.fornecedorId = Request.QueryString["fornId"];
        _obj.tiporoteiro = "RESORTS";

        _rotrepo.MakeConnection(_obj);
        List<RoteiroBLL> _roteiros = _rotrepo.ListaRoteiroPorFornecedor(2);
    }

    public void ListaDetalheRoteiro()
    {
        _obj.parms = 4;
        _obj.clienteId = 2;
        _obj.cidadeId = Request.QueryString["cidadeid"];
        _obj.fornecedorId = Request.QueryString["fornId"];
        _obj.tiporoteiro = "RESORTS";

        _rotrepo.MakeConnection(_obj);
        _detrot = _rotrepo.ListaDetalheRoteiro();

        /* LINQ  */
        var roteiros = (from d in _detrot
                       select new
                       {
                           idrot = d.idTabrot,
                           txrot = d.textoRoteiro
                       }).Distinct();

        List<RoteiroBLL> _rot = new List<RoteiroBLL>();
        foreach (var rs in roteiros)
        {
            RoteiroBLL bll = RoteiroBLL.ObtemRoteiro();
            bll.idRoteiro = rs.idrot;
            bll.textoRoteiro = rs.txrot;
            _rot.Add(bll);
        }
        gdrDetalhe.DataSource = _rot;
        gdrDetalhe.DataBind();
    }

    public void carregaDetalhe()
    {
        _obj.parms = 5;
        _obj.clienteId = 2;
        _obj.cidorig = Request.QueryString["cidorig"].ToString();
        _obj.idtabrot = Request.QueryString["idtabrot"].ToString();
        _obj.dtini = Request.QueryString["dtini"].ToString();
        _obj.dtfim = Request.QueryString["dtfim"].ToString();

        _resrepo.MakeConnection(_obj);

        _detres = _resrepo.ListaResorts();

        if (_detres.Count <= 0)
        {
            ltTituloDet.Text = "Não foram encontradas opções para o período selecionado.";
        }
        else
        {

            ltTituloDet.Text = _detres[0].Titulo;
            ltPeriodoDet.Text = _detres[0].Texto;
            /*  POPULANDO   */
            dlOpcoes.DataSource = _detres;
            dlOpcoes.DataBind();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;
using CMSXBLL.Repositorio;
using System.Dynamic;
using System.IO;
using HtmlAgilityPack;

public partial class conteudomake_new : BasePage
{

    #region PROPRIEDADES

    protected int indice;   

    #endregion

    #region EVENTOS

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!ValidateSession)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            switch (tApp.Layout)
            {
                case "_Layout.cshtml":
                    pnlCategoria.Visible = false;
                    break;
                case "_LayoutBasic.cshtml":
                    pnlPagina.Visible = false;
                    break;
                case "_LayoutFlame.cshtml":
                    pnlCategoria.Visible = false;
                    break;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["texto"] = "Insira aqui o conteúdo";
            MontaComboAreas();
            ListaCategorias();
        }

        //lstAreas.SelectedIndexChanged += new EventHandler(lstAreas_SelectedIndexChanged);
        //lstAreasFilha.SelectedIndexChanged += new EventHandler(lstAreasFilha_SelectedIndexChanged);
    }

    protected void lstAreas_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBox lb = (ListBox)sender;
        indice = lb.SelectedIndex;
        if (indice >= 1)
        {
            MontaComboAreasFilha(new System.Guid(lb.SelectedValue));
            // if(lstAreasFilha.Items.Count<=0)
            ListaConteudoPorArea(new Guid(lb.SelectedValue), dlconteudo);
            ListaImagensPorArea(new Guid(lb.SelectedValue));
        }
    }

    protected void lstAreasFilha_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBox lb = (ListBox)sender;
        indice = lb.SelectedIndex;
        if (indice >= 1)
        {
            //ListaConteudoPorArea(new Guid(lb.SelectedValue), dlConteudoSec);
            ListaImagensPorArea(new Guid(lb.SelectedValue));
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        CriaConteudo();
    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        EditaConteudo();
    }

    protected void dlConteudo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "InativarConteudo":
                InativaConteudo(new Guid(e.CommandArgument.ToString()));
                LimpaForm();
                break;
            case "EditarConteudo":
                //CarregaConteudo(new Guid(e.CommandArgument.ToString()));

                _obj.parms = 2;
                _obj.conteudoId = new Guid(e.CommandArgument.ToString());
                _obj.ativos = 1;
                _conteudorepo.MakeConnection(_obj);
                Conteudo cnt = _conteudorepo.ObtemConteudoPorId()[0];

                Titulo.Text = cnt.Titulo;
                Autor.Text = cnt.Autor;
                hdnConteudoId.Value = cnt.ConteudoId.ToString();
                hdnAreaId.Value = cnt.AreaId.ToString();
                Session["idConteudo"] = cnt.ConteudoId.ToString();

                btnEditar.Enabled = true;
                btnSalvar.Enabled = false;
                cnt.Texto = cnt.Texto.Replace("\"", "'");
                Session["texto"] = cnt.Texto;

                if (!ClientScript.IsClientScriptBlockRegistered("carrega"))
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "carrega", "<script>$('.summernote').code('faster');</script>");
                }

                btnEditar.Enabled = true;
                break;
        }
    }

    protected void btnAtualiza_Click(object sender, EventArgs e)
    {
        if (!ClientScript.IsClientScriptBlockRegistered("carrega"))
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "carrega", "<script>$('.summernote').code('faster');</script>");
        }
    }

    #endregion

    #region METODOS

    protected bool CriaConteudo()
    {
        string texto = ha.Value;

        Conteudo cnt = new Conteudo()
        {
            ConteudoId = Guid.NewGuid(),
            CategoriaId = lstCategorias.Visible!=false?new Guid(lstCategorias.SelectedValue):Guid.Empty,
            AreaId = lstAreas.Visible != false?new Guid(lstAreas.SelectedValue):Guid.Empty,
            Titulo = Titulo.Text,
            Texto = texto,
            UrlImg = hdnImages.Value,
            Autor = Autor.Text
        };

        _obj.parms = 1;
        _obj.conteudo = cnt;
        _conteudorepo.MakeConnection(_obj);
        _conteudorepo.CreateContent();


        LimpaForm();
        MontaComboAreas();
        lstAreas.SelectedIndex = 0;
        //lstAreasFilha.SelectedIndex = 0;
        return true;
    }

    protected void AddCategoria(object sender, EventArgs e)
    {
        _obj.parms = 1;
        LinkButton lkbt = (LinkButton)sender;

        Guid catId = System.Guid.NewGuid();
        Categoria cat = new Categoria();
        cat.CategoriaId = System.Guid.NewGuid();
        cat.AplicacaoId = new System.Guid(Session["Aplicacaoid"].ToString());
        cat.TipoCategoria = 1;

        cat.Nome = txtNomeCategoria.Value;
        cat.Descricao = txtNomeCategoria.Value;
        _obj.categoria = cat;
        _catrepo.MakeConnection(_obj);
        _catrepo.CategoriaRapida();
        ListaCategorias();

        ////-------------------------------------------------//


    }


    //EDIÇÃO DE CONTEUDO JÁ CADASTRADO
    protected void CarregaConteudo(Guid conteudoId)
    {

        _obj.parms = 2;
        _obj.conteudoId = conteudoId;
        _obj.ativos = 1;
        _conteudorepo.MakeConnection(_obj);
        Conteudo cnt = _conteudorepo.ObtemConteudoPorId()[0];

        Titulo.Text = cnt.Titulo;
        Autor.Text = cnt.Autor;
        hdnConteudoId.Value = cnt.ConteudoId.ToString();
        hdnAreaId.Value = cnt.AreaId.ToString();
        hdnCategoriaId.Value = cnt.CategoriaId.ToString();

        if (lstCategorias.Visible == true)
        {
            lstCategorias.SelectedValue = cnt.CategoriaId.ToString();
        }

        if (lstAreas.Visible == true)
        {
            lstAreas.SelectedValue = cnt.AreaId.ToString();
        }

        Session["idConteudo"] = cnt.ConteudoId.ToString();

        btnEditar.Enabled = true;
        btnSalvar.Enabled = false;
        cnt.Texto = cnt.Texto.Replace("\"", "'");
        Session["texto"] = cnt.Texto;

        if (!ClientScript.IsClientScriptBlockRegistered("carrega"))
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "carrega", "<script>$('.summernote').code('faster');</script>");
        }
    }
       
    protected void EditaConteudo()
    {
        _obj.parms = 1;

        string texto = ha.Value;
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(texto);

        Conteudo ct = new Conteudo()
        {
            ConteudoId = new Guid(Session["idConteudo"].ToString()),
            CategoriaId = (!string.IsNullOrEmpty(hdnCategoriaId.Value)?new Guid(hdnCategoriaId.Value):Guid.Empty),
            AreaId = (!string.IsNullOrEmpty(hdnAreaId.Value)?new Guid(hdnAreaId.Value):Guid.Empty),
            Titulo = Titulo.Text,
            Texto = texto,
            Autor = Autor.Text,
            UrlImg = hdnImages.Value
        };

        _obj.conteudo = ct;

        _conteudorepo.MakeConnection(_obj);
        _conteudorepo.Edita();

        LimpaForm();
        MontaComboAreas();
        //lstAreas.SelectedIndex = 0;
        //lstAreasFilha.SelectedIndex = 0;

    }

    protected void InativaConteudo(Guid conteudoId)
    {
        _obj.parms = 1;
        _obj.conteudoid = conteudoId;
        _conteudorepo.MakeConnection(_obj);
        _conteudorepo.InativaConteudo();

        LimpaForm();
        MontaComboAreas();
        lstAreas.SelectedIndex = 0;
        lstAreasFilha.SelectedIndex = 0;
    }

    protected void MontaComboAreas()
    {
        _obj.appid = tApp.AplicacaoId;// Session["AplicacaoId"];// ((Aplicacao)Session["cliente"]).AplicacaoId;
        _obj.tpArea = "null";
        _obj.parms = 2;
        _areasrepo.MakeConnection(_obj);
        lstAreas.Items.Clear();

        lstAreas.Items.Add(new ListItem("Escolha a página"));
        foreach (Areas _area in _areasrepo.ListaAreasPai())
        {
            ListItem _item = new ListItem(_area.Nome, _area.AreaId.ToString());
            lstAreas.Items.Add(_item);
        }

        ListaConteudo(dlconteudo);

        //if (lstAreas.Items.Count > 1 && lstAreasFilha.Items.Count == 1)
        //{
        //    ListaConteudoPorArea(new Guid(lstAreas.Items[1].Value), dlConteudoSec);
        //    ListaImagensPorArea(new Guid(lstAreas.Items[1].Value));
        //}
    }

    protected void HabilitaImagem(object sender, EventArgs e)
    {
        pnlimagem.Visible = pnlimagem.Visible == true ? false : true;
        CheckBox ckb = (CheckBox)sender;
        if (ckb.Checked)
        {
            this.mpe.Show();
        }
    }

    protected void HabilitaGaleria(object sender, EventArgs e)
    {
        //pnlgaleria.Visible = pnlgaleria.Visible == true ? false : true;
    }

    protected void ListaImagensPorArea(Guid AreaId)
    {
        _obj.parms = 1;
        _obj.areaid = AreaId;

        _imgrepo.MakeConnection(_obj);

        List<Imagem> limg = _imgrepo.Galeria();
        if (limg.Count > 0)
        {
            ckbgaleria.Enabled = true;
            pnlgaleria.Visible = true;
        }
    }

    protected void MontaComboAreasFilha(Guid AreaIdPai)
    {
        _obj.parms = 2;
        _obj.appid = tApp.AplicacaoId;// Session["AplicacaoId"];// ((Aplicacao)Session["cliente"]).AplicacaoId;
        _obj.areaIdPai = AreaIdPai;
        _areasrepo.MakeConnection(_obj);

        lstAreasFilha.Items.Clear();
        lstAreasFilha.Items.Add(new ListItem("Escolha a página"));

        foreach (Areas _area in _areasrepo.ListaAreasFilha())
        {
            ListItem _item = new ListItem(_area.Nome, _area.AreaId.ToString());
            lstAreasFilha.Items.Add(_item);
        }

        //if (lstAreasFilha.Items.Count > 1)
        //{
        //    ListaConteudoPorArea(new Guid(lstAreasFilha.Items[1].Value), dlConteudoSec);
        //    pnlFilhas.Visible = true;
        //    lstAreasFilha.Visible = true;
        //}
        //else
        //{
        //    dlConteudoSec.DataSource = null;
        //    dlConteudoSec.DataBind();
        //    pnlFilhas.Visible = false;
        //    lstAreasFilha.Visible = false;
        //}
    }

    protected void ListaConteudoPorArea(Guid AreaId, GridView grDestino)
    {
        _obj.parms = 2;
        _obj.areaid = AreaId;
        _obj.ativos = 1;
        _conteudorepo.MakeConnection(_obj);
        List<Conteudo> lst = _conteudorepo.ListaConteudoPorAplicacaoId();
        if (lst.Count >= 1)
        {
            grDestino.DataSource = lst;
            grDestino.DataBind();
        }
        else
        {
            grDestino.DataSource = null;
            grDestino.DataBind();
        }
    }

    protected void ListaConteudo(Guid AreaId, GridView grDestino)
    {
        _obj.parms = 2;
        _obj.appid = tApp.AplicacaoId;// Session["AplicacaoId"];
        _obj.ativos = 1;
        _conteudorepo.MakeConnection(_obj);
        List<Conteudo> lst = _conteudorepo.ListaConteudoPorAplicacaoId();
        if (lst.Count >= 1)
        {
            grDestino.DataSource = lst;
            grDestino.DataBind();
        }
        else
        {
            grDestino.DataSource = null;
            grDestino.DataBind();
        }
    }

    protected void ListaConteudo(GridView grDestino)
    {
        _obj.parms = 2;
        // _obj.areaid = AreaId;
        _obj.appid = tApp.AplicacaoId;// Session["AplicacaoId"];
        _obj.ativos = 1;
        _conteudorepo.MakeConnection(_obj);
        //List<Conteudo> lst = (tApp.Layout.Contains("Blog")?_conteudorepo.ListaConteudoPorCategoria():_conteudorepo.ListaConteudoPorAreaId());
        List<Conteudo> lst = _conteudorepo.ListaConteudoPorAplicacaoId();
        if (lst.Count >= 1)
        {
            grDestino.DataSource = lst;
            grDestino.DataBind();
        }
        else
        {
            grDestino.DataSource = null;
            grDestino.DataBind();
        }
    }

    protected void LimpaForm()
    {
        Session["idConteudo"] = null;
        Titulo.Text = string.Empty;
        lstAreas.SelectedIndex = 0;
        Autor.Text = string.Empty;
        //textoConteudo.Text = string.Empty;
        ha.Value = string.Empty;
        hdnAreaId.Value = string.Empty;
        hdnConteudoId.Value = string.Empty;
        ckbimagem.Checked = false;
        Session["texto"] = null;
    }

    protected void SalvaImagem(string fullOutputPath, string base64String)
    {

        // Convert Base64 String to byte[]
        byte[] imageBytes = Convert.FromBase64String(base64String);

        string uploadPath = Server.MapPath("~/"+ fullOutputPath);

        // store the byte[] directly, without converting to Bitmap first 
        if (File.Exists(uploadPath))
        {
            File.Delete(uploadPath);
        }
        using (FileStream fs = File.Create(uploadPath))
        using (BinaryWriter bw = new BinaryWriter(fs))
            bw.Write(imageBytes);

    }

    //------------------ CATEGORIAS

    protected void ListaCategorias()
    {
        _obj.parms = 1;
        _obj.appid = Session["AplicacaoId"];// ((Aplicacao)Session["cliente"]).AplicacaoId;
        _catrepo.MakeConnection(_obj);
        lstCategorias.Items.Clear();

        lstCategorias.Items.Add(new ListItem("Escolha a categoria"));
        foreach (Categoria cat in _catrepo.ListaCategoriaPai())
        {
            ListItem _item = new ListItem(cat.Nome, cat.CategoriaId.ToString());
            lstCategorias.Items.Add(_item);
        }
    }

    //protected void ListaSubCategorias(Guid categoriaId)
    //{
    //    _obj.parms = 1;
    //    //_obj.appid = Session["AplicacaoId"];// ((Aplicacao)Session["cliente"]).AplicacaoId;
    //    _obj.cpid = categoriaId;
    //    _catrepo.MakeConnection(_obj);

    //    lstSubCategorias.Items.Clear();
    //    lstSubCategorias.Items.Add(new ListItem("Escolha a subcategoria"));

    //    foreach (Categoria cat in _catrepo.ListaSubCategoria())
    //    {
    //        ListItem _item = new ListItem(cat.Nome, cat.CategoriaId.ToString());
    //        lstSubCategorias.Items.Add(_item);
    //    }

    //    if (lstSubCategorias.Items.Count > 1)
    //    {
    //        //ListaConteudoPorArea(new Guid(lstAreasFilha.Items[1].Value), dlConteudoSec);
    //        pnlCat.Visible = true;
    //        lstSubCategorias.Visible = true;
    //    }
    //    else
    //    {
    //        //dlConteudoSec.DataSource = null;
    //        //dlConteudoSec.DataBind();
    //        pnlCat.Visible = false;
    //        lstSubCategorias.Visible = false;
    //    }
    //}

    protected void lstCategorias_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBox lb = (ListBox)sender;
        //ListaSubCategorias(new System.Guid(lb.SelectedValue));
        hdnCategoriaId.Value = lb.SelectedValue;
    }

    protected void btnAddArea_Click(object sender, EventArgs e)
    {
        _obj.parms = 1;
        LinkButton lkbt = (LinkButton)sender;

        //_obj.parms = 12;
        Areas area = new Areas();
        Guid areaId = System.Guid.NewGuid();
        area.AreaId = areaId;   
        area.AplicacaoId = new System.Guid(Session["Aplicacaoid"].ToString());
        //area.AreaIdPai = null;
        area.Nome = txtNomeArea.Value;

        /* DEFININDO A URL A SER CHAMADA */
        area.Url = "conteudo.aspx";
        area.Descricao = "Area rapida criada na pagina de conteudo";
        area.Imagem = false;
        area.Posicao = (lstAreas.Items.Count)-1;
        area.TipoArea = 2;
        _obj.area = area;

        _areasrepo.MakeConnection(_obj);
        _areasrepo.AreaRapida();
        //_areasrepo.CriaNovaArea();
        MontaComboAreas();
        lstAreas.SelectedValue = area.AreaId.ToString();
        Titulo.Focus();
    }

    #endregion
}
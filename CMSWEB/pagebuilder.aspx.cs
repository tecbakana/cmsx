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
    
    protected void Page_Load(object sender, EventArgs e)
    {
 
        if (!IsPostBack)
        {
            Session["texto"] = "Insira aqui o conteúdo";
            MontaComboAreas();
        }

        lstAreas.SelectedIndexChanged += new EventHandler(lstAreas_SelectedIndexChanged);
        lstAreasFilha.SelectedIndexChanged += new EventHandler(lstAreasFilha_SelectedIndexChanged);
    }

    protected void lstAreas_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBox lb = (ListBox)sender;
        indice = lb.SelectedIndex;
       // MontaComboAreasFilha(new System.Guid(lb.SelectedValue));
        // if(lstAreasFilha.Items.Count<=0)
       // ListaConteudoPorArea(new Guid(lb.SelectedValue), dlconteudo);
       // ListaImagensPorArea(new Guid(lb.SelectedValue));
    }

    protected void lstAreasFilha_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBox lb = (ListBox)sender;
        //indice = lb.SelectedIndex;
        //if (indice >= 1)
        //{
        //    ListaConteudoPorArea(new Guid(lb.SelectedValue), dlConteudoSec);
        //    ListaImagensPorArea(new Guid(lb.SelectedValue));
        //}
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

                txtTitulo.Text = cnt.Titulo;
                txtDescricao.Text = cnt.Autor;
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

        _obj.parms = 10;
        Guid _idArea = System.Guid.NewGuid();
        _obj.areaid = _idArea;
        _obj.appid = Session["Aplicacaoid"].ToString();
        _obj.areaidpai = lstAreas.SelectedIndex == 0 ? "" : lstAreas.SelectedValue.ToString();
        _obj.nome = txtTitulo.Text;

        /* DEFININDO A URL A SER CHAMADA */
        string _url = string.Empty;
        _url = "conteudo.aspx";

        _obj.url = _url;
        _obj.descricao = txtDescricao.Text;
        _obj.imagem = ckbimagem.Checked;

        /* VERIFICANDO QUAL O TIPO DE MENU */
        _obj.menulateral = false;
        _obj.menusplash  = false;
        _obj.menucentral = true;

        _areasrepo.MakeConnection(_obj);
        _areasrepo.CriaNovaArea();

        //conteudo novo -> cria uma area simples
        Guid conteudoId = System.Guid.NewGuid();
        _obj.parms = 7;
        _obj.conteudoid = conteudoId;
        _obj.titulo = txtTitulo.Text;
        _obj.autor = "-";

        string texto = ha.Value;
        _obj.texto = texto;

        _obj.areaid = _idArea;

        _conteudorepo.MakeConnection(_obj);
        _conteudorepo.CreateContent();

        return true;
    }

    //EDIÇÃO DE CONTEUDO JÁ CADASTRADO
    protected void CarregaConteudo(Guid conteudoId)
    {

        _obj.parms = 2;
        _obj.conteudoId = conteudoId;
        _obj.ativos = 1;
        _conteudorepo.MakeConnection(_obj);
        Conteudo cnt = _conteudorepo.ObtemConteudoPorId()[0];

        txtTitulo.Text = cnt.Titulo;
        txtDescricao.Text = cnt.Autor;
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
    }
       
    protected void EditaConteudo()
    {
        _obj.parms = 4;
        //Guid _idArea = new Guid(hdnAreaId.Value);
        Guid conteudoId = new Guid(Session["idConteudo"].ToString());
        _obj.conteudoid = conteudoId;
        _obj.titulo = txtTitulo.Text;
        _obj.autor = txtDescricao.Text;

        string texto = ha.Value;
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(texto);

        var itemList = doc.DocumentNode.SelectNodes("//img")//this xpath selects all span tag having its class as hidden first
                  //.Select(p => p.InnerText)
                  .ToList();

        //if (texto.Contains("codehere"))
        //{
        //    string[] imgs = hdnImages.Value.Split('|');

        //    for (int i = 0; i < imgs.Length; i++)
        //    {
        //        if (!string.IsNullOrEmpty(imgs[i]))
        //        {
        //            string output = @"images/" + conteudoId.ToString() + "_img" + i.ToString() + ".jpg";
        //            texto = texto.Replace("codehere" + i.ToString(), output);
        //            if (imgs[i].Contains("data:image/jpeg;base64"))
        //            {
        //                string imgbase64 = imgs[i].Replace("data:image/jpeg;base64,", string.Empty);
        //                SalvaImagem(output, imgbase64);
        //            }
        //        }
        //    }
        //}

        _obj.texto = texto;
        _conteudorepo.MakeConnection(_obj);
        _conteudorepo.EditContent();

        LimpaForm();
        MontaComboAreas();
        lstAreas.SelectedIndex = 0;
        lstAreasFilha.SelectedIndex = 0;

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
        _obj.parms = 1;
        _obj.appid = Session["AplicacaoId"];// ((Aplicacao)Session["cliente"]).AplicacaoId;
        _areasrepo.MakeConnection(_obj);
        lstAreas.Items.Clear();

        lstAreas.Items.Add(new ListItem("Escolha a area"));
        foreach (Areas _area in _areasrepo.ListaAreasPai())
        {
            ListItem _item = new ListItem(_area.Nome, _area.AreaId.ToString());
            lstAreas.Items.Add(_item);
        }

        if (lstAreas.Items.Count > 1 && lstAreasFilha.Items.Count == 1)
        {
            ListaConteudoPorArea(new Guid(lstAreas.Items[1].Value), dlConteudoSec);
            ListaImagensPorArea(new Guid(lstAreas.Items[1].Value));
        }
    }

    protected void HabilitaImagem(object sender, EventArgs e)
    {
        pnlimagem.Visible = pnlimagem.Visible == true ? false : true;
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
        _obj.appid = Session["AplicacaoId"];// ((Aplicacao)Session["cliente"]).AplicacaoId;
        _obj.areaIdPai = AreaIdPai;
        _areasrepo.MakeConnection(_obj);

        lstAreasFilha.Items.Clear();
        lstAreasFilha.Items.Add(new ListItem("Escolha a area"));

        foreach (Areas _area in _areasrepo.ListaAreasFilha())
        {
            ListItem _item = new ListItem(_area.Nome, _area.AreaId.ToString());
            lstAreasFilha.Items.Add(_item);
        }

        if (lstAreasFilha.Items.Count > 1)
        {
            ListaConteudoPorArea(new Guid(lstAreasFilha.Items[1].Value), dlConteudoSec);
            pnlFilhas.Visible = true;
            lstAreasFilha.Visible = true;
        }
        else
        {
            dlConteudoSec.DataSource = null;
            dlConteudoSec.DataBind();
            pnlFilhas.Visible = false;
            lstAreasFilha.Visible = false;
        }
    }

    protected void ListaConteudoPorArea(Guid AreaId, GridView grDestino)
    {
        _obj.parms = 2;
        _obj.areaid = AreaId;
        _obj.ativos = 1;
        _conteudorepo.MakeConnection(_obj);
        List<Conteudo> lst = _conteudorepo.ListaConteudoPorAreaId();
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
        txtTitulo.Text = string.Empty;
        lstAreas.SelectedIndex = 0;
        txtDescricao.Text = string.Empty;
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
    #endregion

}
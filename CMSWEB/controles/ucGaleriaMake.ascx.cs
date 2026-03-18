using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using CMSXBLL;

public partial class controles_ucGaleriaMake : BaseControl
{

    private dynamic ambiente;
    protected int indice;
    private List<Imagem> lsim = new List<Imagem>();
    public string produtoId;
    public string areaId;
    public string conteudoId;
    public int tipo;
    private string cliFolder;

    protected void Page_Load(object sender, EventArgs e)
    {
        /*PROPERTIES*/
        cliFolder = Path.Combine(Directory.GetParent(Server.MapPath("")).FullName, System.Configuration.ConfigurationManager.AppSettings["pathMakeCliFolder"]);

        gdrimg.ItemDataBound += gdrimg_ItemDataBound;

        if (IsPostBack)
        {
            if (Session["AplicacaoId"] != null)
            {
                LoadImages();
            }
        }
    }

    protected void gdrimg_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            Imagem img = (Imagem)e.Item.DataItem;
            DirectoryInfo _dirClienteImagem = new DirectoryInfo(cliFolder + "/" + ((dynamic)Session["objeto"]).url + "/images");
            ((Image)e.Item.FindControl("img")).ImageUrl = System.Configuration.ConfigurationManager.AppSettings["pathSite"] + "/" + ((dynamic)Session["objeto"]).url + "/images" + "/_" + img.Url;
        }
    }


    protected void btnfilemake_Click(object sender, EventArgs e)
    {
        /* MONTA O NOME DO ARQUIVO CONCATENANDO O ID DA APLICACAO */
        string parentId = (Session["prodId"] == null ? Session["AplicacaoId"].ToString() : Session["prodId"].ToString());
        if (upf1.HasFile)
        {
            SendFiles(upf1, txtDescricao1);
        }
        if (upf2.HasFile)
        {
            SendFiles(upf2, txtDescricao2);
        }
        if (upf3.HasFile)
        {
            SendFiles(upf3, txtDescricao3);
        }
        if (upf4.HasFile)
        {
            SendFiles(upf4, txtDescricao4);
        }
        if (upf5.HasFile)
        {
            SendFiles(upf5, txtDescricao5);
        }

        string funcCall = "callTab('images')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "imgScript", funcCall, true);
        LoadImages();
    }

    public void SendFiles(FileUpload upl, HtmlInputText txtDescricao)
    {
        string parentId = (Session["prodId"] == null ? Session["AplicacaoId"].ToString() : Session["prodId"].ToString());
        string pId = parentId;
        string fName = pId + upl.FileName;

        DirectoryInfo _dirClienteImagem = new DirectoryInfo(cliFolder + "/" + ((dynamic)Session["objeto"]).url + "/images");

        string path = _dirClienteImagem.FullName + "/_" + fName;

        upl.SaveAs(path);
        //--------------------------------------- saving to bd
        _obj.parms = 1;
        Guid imgid = System.Guid.NewGuid();
        Imagem img = new Imagem();
        img.Url = fName;
        img.ImagemId = imgid;
        img.ParentId = new System.Guid(pId);
        img.Descricao = txtDescricao.Value;
        img.TipoId = new System.Guid("e1a41094-4ffd-11e1-8664-07b98c902e34");

        _obj.imgObj = img;

        _imgrepo.MakeConnection(_obj);
        _imgrepo.InsereImagemGaleria();

    }

    public void LoadImages()
    {

        if (tipo == 2)
        {
            gdrimg.DataSource = null;
            gdrimg.DataBind();


            if (Session["AplicacaoId"] != null)
            {
                _obj.parms = 1;
                _obj.pId = Session["AplicacaoId"].ToString();
                _imgrepo.MakeConnection(_obj);
                lsim = _imgrepo.GaleriaParentId();
            }
            else
            {
                lsim = null;
            }

            gdrimg.DataSource = lsim;
            gdrimg.DataBind();

            pnlformapp.Update();

            if (gdrimg.Items.Count >= 1)
            {
                ///desabilita envio de arquivos

                for (int i = 1; i <= gdrimg.Items.Count; i++)
                {
                    /*upf1.Enabled = false;
                    upf2.Enabled = false;
                    upf3.Enabled = false;
                    upf4.Enabled = false;
                    upf5.Enabled = false;*/
                    FileUpload f = (FileUpload)Controls[0].FindControl("upf" + i.ToString());
                    if (f != null)
                    {
                        f.Enabled = false;
                    }
                }
            }
        }
    }

    public void LoadImages(string parentId)
    {

        gdrimg.DataSource = null;
        gdrimg.DataBind();
        
        _obj.parms = 1;
        _obj.pId = parentId;
        _imgrepo.MakeConnection(_obj);
        lsim = _imgrepo.GaleriaParentId();
     

        gdrimg.DataSource = lsim;
        gdrimg.DataBind();

        if (gdrimg.Items.Count >= 1)
        {
            ///desabilita envio de arquivos

            for (int i = 1; i <= gdrimg.Items.Count; i++)
            {
                FileUpload f = (FileUpload)Controls[0].FindControl("upf" + i.ToString());
                if (f != null)
                {
                    f.Enabled = false;
                }

            }

            //btnfilemake.Enabled = false;
        }

        pnlformapp.Update();
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using CMSXBLL;

public partial class controles_ucgaleria : BaseControl
{
    public int acao { get; set; }
    private Guid _appid;

    protected void Page_Load(object sender, EventArgs e)
    {
        _appid = (Guid)Session["Aplicacaoid"];
        if (!IsPostBack)
        {
            ListaImages();
        }
    }

    protected void ListaImages()
    {
        gdrimg.DataSource = null;
        gdrimg.DataBind();

        string cliFolder = Path.Combine(Directory.GetParent(HttpContext.Current.Server.MapPath("")).FullName, System.Configuration.ConfigurationManager.AppSettings["pathMakeCliFolder"]);
        DirectoryInfo _dirClienteImagem = new DirectoryInfo(cliFolder + "/" + ((dynamic)HttpContext.Current.Session["objeto"]).url + "/images");

        //DirectoryInfo _dir = new DirectoryInfo(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["pathSaveIn"].ToString()));
        FileInfo[] rgFiles = _dirClienteImagem.GetFiles("*.*");

        List<Imagem> images = new List<Imagem>();
        foreach (FileInfo fi in rgFiles)
        {
            if (fi.Name.Contains(_appid.ToString()))
            {
                Imagem img = Imagem.ObterImagem();

                img.Url = _dirClienteImagem.FullName + "/_" + fi.Name;
                img.Descricao = fi.Name.Replace(_appid.ToString(), "");
                images.Add(img);
            }
        }
        gdrimg.DataSource = images;
        gdrimg.DataBind();
    }

    protected void gdrimg_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        DataList dl = (DataList)sender;
        Image img = (Image)e.Item.FindControl("imgGal");
        img.Attributes.Add("onmouseover", "document.getElementById(\"imgpreview\").src=\"" + img.ImageUrl.Replace("~/", "") + "\"");
        img.Attributes.Add("onmouseout", "document.getElementById(\"imgpreview\").src=\"\"");

        if (acao == 2)
        {
            string[] imgattrib = img.ImageUrl.Split('/');

            img.Attributes.Add("onclick", "javascript:setImage('" + imgattrib[imgattrib.Length-1] + "');");
        }
    }

}
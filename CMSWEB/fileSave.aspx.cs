using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fileSave : BasePage
{
    private dynamic ambiente;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request.Files.Count>=1)
            {
                ambiente = Session["Objeto"];
                string url = SalvaImagem(Request.Files);
                Response.Redirect("savefile.ashx?url=" + url);
            }
        }
    }

    protected string SalvaImagem(HttpFileCollection req)
    {
        string fName = ambiente.aplicacaoid.ToString() + req[0].FileName;
        string savepath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["pathSaveIn"].ToString()) + Session["idConteudo"] + "_" + fName;
        string imgpath = System.Configuration.ConfigurationManager.AppSettings["pathSaveOut"].ToString() + Session["idConteudo"] + "_"  + fName;
        req[0].SaveAs(savepath);
        imgpath = imgpath.Replace(@"~/","");
        return imgpath;
    }
}
<%@ WebHandler Language="C#" Class="savefile" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.IO;

public class savefile : IHttpHandler, IReadOnlySessionState
{
    
    private HttpContext httpcont;
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        httpcont = context;
        if (context.Request.Files.Count >= 1)
        {
            string url = SalvaImagem(context.Request.Files);
            context.Response.Write(url);
        }
    }

    protected string SalvaImagem(HttpFileCollection req)
    {
        string fName = httpcont.Request.QueryString["id"].ToString() + req[0].FileName;
        string cliFolder = Path.Combine(Directory.GetParent(HttpContext.Current.Server.MapPath("")).FullName, System.Configuration.ConfigurationManager.AppSettings["pathMakeCliFolder"]);
        DirectoryInfo _dirClienteImagem = new DirectoryInfo(cliFolder + "/" + ((dynamic)HttpContext.Current.Session["objeto"]).url + "/images");

        string savepath = _dirClienteImagem.FullName + "/_" + fName;

        string imgpath = System.Configuration.ConfigurationManager.AppSettings["pathSite"] + "/" + ((dynamic)HttpContext.Current.Session["objeto"]).url + "/images/_" + fName;
        req[0].SaveAs(savepath);
        imgpath = imgpath.Replace(@"~/", "");
        return imgpath;
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}
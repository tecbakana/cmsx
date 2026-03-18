using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security;
using System.Security.Principal;
using System.Security.AccessControl;
using CMSXBLL;
using CMSXBLL.Repositorio;
using System.IO;
using System.Text;
using System.Configuration;

public partial class teste_place : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string webRootPath = Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["pathMakeCliFolder"]);
        string cliFolder = Path.Combine(Directory.GetParent(Server.MapPath("")).FullName, "clientes");//Server.MapPath("~\" + System.Configuration.ConfigurationManager.AppSettings["pathMakeCliFolder"]); //new DirectoryInfo(HttpContext.Current.Server.MapPath(webRootPath + System.Configuration.ConfigurationManager.AppSettings["pathMakeCliFolder"].ToString())).Parent.FullName;
        DirectoryInfo imgFolder = new DirectoryInfo(cliFolder + "/bfmake");
        DirectoryInfo pathSaveImage = new DirectoryInfo(cliFolder + "/bfmake/images");

        txtTest.Text = cliFolder + ":" + imgFolder.FullName + ":" + pathSaveImage.FullName;

        /* CRIAR FOLDER RELATIVO A NOVA APLICACAO */
        DirectoryInfo _dir = new DirectoryInfo(cliFolder);//Path.GetFullPath(Path.Combine(webRootPath, cliFolder)));
    }
}
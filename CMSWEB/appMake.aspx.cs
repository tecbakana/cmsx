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

public partial class appMake : BasePage
{

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!ValidateSession)
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        CarregaRedes();
        //_obj.banco = "SqlServer";
        btnappmake.Click += new EventHandler(btnappmake_Click);

        btnEditar.Click += btnEditar_Click;
        ListaAplicacao();
    }

    protected void btnappmake_Click(object sender, EventArgs e)
    {
        CriaApp();
        ListaAplicacao();
    }

    protected void CriaApp()
    {
        _obj.parms = 1;
        Aplicacao app = new Aplicacao();

        //_obj.appId = System.Guid.NewGuid();
        //_obj.appnome = txtappname.Text;
        //_obj.appurl = txtappurl.Text;
        //_obj.appid = System.Guid.NewGuid();
        //_obj.admin = Session["USUARIO"].ToString();

        app.AplicacaoId     = System.Guid.NewGuid();
        app.Nome            = txtappname.Text;
        app.Url             = txtappurl.Text;
        app.mailUser        = txtEmail.Text;
        app.PagSeguroToken  = txtToken.Text;
        app.LayoutChoose    = ddlTemplate.SelectedValue == "-1" ? "_Layout.cshtml" : ddlTemplate.SelectedValue;
        app.googleAdSense = txtAdSense.Text;
        app.IdUsuarioInicio = Session["USUARIO"].ToString();
    
        foreach (RepeaterItem item in rptRedes.Items)
        {
            string rede = ((Label)item.Controls[1]).Text;
            switch(rede.ToLower())
            {
                case "facebook":
                    app.pageFacebook = ((TextBox)item.Controls[3]).Text;
                    break;
                case "twitter":
                    app.pageTwitter = ((TextBox)item.Controls[3]).Text;
                    break;
                case "instagram":
                    app.pageInstagram = ((TextBox)item.Controls[3]).Text;
                    break;
                case "flicker":
                    app.pageFlicker = ((TextBox)item.Controls[3]).Text;
                    break;
                case "linkedin":
                    app.pageLinkedin = ((TextBox)item.Controls[3]).Text;
                    break;
                case "pinteres":
                    app.pagePinterest = ((TextBox)item.Controls[3]).Text;
                    break;
            }
             
        }

        _obj.aplicacao = app;

        _apprepo.MakeConnection(_obj);
        _apprepo.CriaAplicacao();

        txtappname.Text = string.Empty;
        txtappurl.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtToken.Text = string.Empty;
        txtAdSense.Text = string.Empty;

        /*PROPERTIES*/
        string cliFolder = Path.Combine(Directory.GetParent(Server.MapPath("")).FullName, System.Configuration.ConfigurationManager.AppSettings["pathMakeCliFolder"]);

        /* CRIAR FOLDER RELATIVO A NOVA APLICACAO */
        DirectoryInfo _dir = new DirectoryInfo(cliFolder);
        _dir.CreateSubdirectory(app.Url);


        /* CRIAR FOLDER DE IMAGENS - TODAS AS IMAGENS ENVIADAS PELO CLIENTE FICAM ARMAZENADAS AQUI */
        DirectoryInfo _dirCliente = new DirectoryInfo(cliFolder + "/" + app.Url);
        _dirCliente.CreateSubdirectory("images");

        DirectoryInfo _dirClienteImagem = new DirectoryInfo(cliFolder + "/" + app.Url + "/images");
        if (_dirClienteImagem.Exists)
        {
            if (upFile.HasFile)
            {
                string parentId = app.AplicacaoId.ToString();
                string pId = parentId;
                string fName = pId + upFile.FileName;
                string path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["pathSaveIn"].ToString()) + "/" + ((dynamic)Session["objeto"]).url + "/images/_" + fName;

                upFile.SaveAs(path);
                //--------------------------------------- saving to bd
                _obj.parms = 1;
                Guid imgid = System.Guid.NewGuid();
                Imagem img = new Imagem();
                img.Url = fName;
                img.ImagemId = imgid;
                img.ParentId = new System.Guid(pId);
                img.Descricao = "Logotipo " + txtappname.Text;
                img.TipoId = new System.Guid("24a57e31-4ffe-11e1-8664-07b98c902e34");

                _obj.imgObj = img;

                _imgrepo.MakeConnection(_obj);
                _imgrepo.InsereImagemGaleria();
            }
        }

        ///CRIAR ARQUIVO INDEX NA PASTA DO CLIENTE, QUE VAI APONTAR PARA O DIRETORIO MVC
        string file = _dirCliente.FullName;
        file = file.Replace("/", "\\");

        string htmlInitial = @"<!DOCTYPE html>
                                <html lang='eng'>
                                    <head>
                                        <Title>
                                            " + app.Nome;
    htmlInitial +=                     @" </Title>
                                        <style>
                                            html, body, iframe {
                                                margin:0; /* remove any margins of the IFrame and the body tag */
                                                padding:0;
                                                height:100%; /* set height 100% so that it fills the entire view port*/
                                            }
                                            iframe {
                                                display:block; /* force the iframe to display as block */   
                                                height:100%; /* assign 100% height */
                                                width: 100%;
                                                border:none;
                                            }
                                        </style>
                                    </head>
                                    <body>";
        string pth = ConfigurationManager.AppSettings["pathAbs"];
        htmlInitial += "               <iframe src='" + pth + "/main?cliente=" + app.Url + "'  />";
        htmlInitial +=             @"</body>
                                </html>";

        // Create a new file 
        string fl = file + "/index.html";

        using (FileStream fs = File.Create(fl))
        {
            // Add some text to file
            Byte[] content = new UTF8Encoding(true).GetBytes(htmlInitial);
            fs.Write(content, 0, content.Length);
        }

    }

    protected void ListaAplicacao()
    {
        gdrapp.DataSource = null;
        gdrapp.DataBind();
        _obj.parms = 1;
        _obj.admin = Session["USUARIO"].ToString();
        _apprepo.MakeConnection(_obj);
        gdrapp.DataSource = _apprepo.ListaAplicacao();
        gdrapp.DataBind();
        pnlformapp.Update();
    }

    protected void btnstatus_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "InativarAplicacao")
        {
            _obj.parms = 2;
            _obj.appId = e.CommandArgument.ToString();
            _obj.act = 0;
            _apprepo.MakeConnection(_obj);
            _apprepo.InativaAplicacao();
            ListaAplicacao();
        }
        else if (e.CommandName == "ExcluirAplicacao")
        {
            ExcluiApp(e.CommandArgument.ToString());
            ListaAplicacao();
        }
        else if (e.CommandName == "AtivarAplicacao")
        {
            _obj.parms = 2;
            _obj.appId = e.CommandArgument.ToString();
            _obj.act = 1;
            _apprepo.MakeConnection(_obj);
            _apprepo.InativaAplicacao();
            ListaAplicacao();
        }
        else if (e.CommandName == "Editar")
        {
            _obj.parms = 1;
            _obj.appId = e.CommandArgument.ToString();
            _apprepo.MakeConnection(_obj);
            var app = _apprepo.ObtemAplicacaoPorId(System.Guid.NewGuid());

            txtappname.Text = app.Nome;
            txtappurl.Text = app.Url;
            ddlTemplate.SelectedValue = app.Layout;
            txtEmail.Text = app.mailUser;
            txtToken.Text = app.PagSeguroToken;
            hdnEditId.Value = app.AplicacaoId.ToString();
            btnappmake.Enabled = false;
            btnEditar.Enabled = true;


            foreach (RepeaterItem item in rptRedes.Items)
            {
                string rede = ((Label)item.Controls[1]).Text;
                switch (rede.ToLower())
                {
                    case "facebook":
                        ((TextBox)item.Controls[3]).Text = string.IsNullOrEmpty(app.pageFacebook) ? string.Empty : app.pageFacebook;
                        break;
                    case "twitter":
                        ((TextBox)item.Controls[3]).Text = string.IsNullOrEmpty(app.pageInstagram) ? string.Empty : app.pageInstagram;
                        break;
                    case "instagram":
                        ((TextBox)item.Controls[3]).Text = string.IsNullOrEmpty(app.pageInstagram) ? string.Empty : app.pageInstagram;
                        break;
                    case "flicker":
                        ((TextBox)item.Controls[3]).Text = string.IsNullOrEmpty(app.pageFlicker) ? string.Empty : app.pageFlicker;
                        break;
                    case "linkedin":
                        ((TextBox)item.Controls[3]).Text = string.IsNullOrEmpty(app.pageLinkedin) ? string.Empty : app.pageLinkedin;
                        break;
                    case "pinterest":
                        ((TextBox)item.Controls[3]).Text = string.IsNullOrEmpty(app.pageLinkedin) ? string.Empty : app.pagePinterest;
                        break;
                }

            }

            pnlformapp.Update();
        }
    }

    protected void gdrapp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow gr = e.Row;
            Button bton = (Button)gr.FindControl("btnstatuson");
            Button btoff = (Button)gr.FindControl("btnstatusoff");
            Image img = (Image)gr.FindControl("appLogo");
            Aplicacao app = (Aplicacao)e.Row.DataItem;
            

            ScriptManager.GetCurrent(this).RegisterPostBackControl(bton);
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btoff);

            if (string.IsNullOrEmpty(gdrapp.DataKeys[e.Row.RowIndex].Values[2].ToString()))
            {
                gr.BackColor = System.Drawing.Color.White;
                bton.Visible = false;
            }
            else
            {
                gr.BackColor = System.Drawing.Color.LightGray;
                btoff.Visible = false;
                bton.Visible = true;
            }



            img.ImageUrl = "clientes/" + app.Url + "/images/_" + app.ImagemUrl;

        }
    }

    protected void gdrapp_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        _obj.parms = 1;
        Aplicacao app = new Aplicacao();

        app.AplicacaoId = new System.Guid(hdnEditId.Value);
        app.Nome = txtappname.Text;
        app.Url = txtappurl.Text;
        app.mailUser = txtEmail.Text;
        app.PagSeguroToken = txtToken.Text;
        app.googleAdSense = txtAdSense.Text;
        app.LayoutChoose = ddlTemplate.SelectedValue == "-1" ? "_Layout.cshtml" : ddlTemplate.SelectedValue;
        app.IdUsuarioInicio = Session["USUARIO"].ToString();


        foreach (RepeaterItem item in rptRedes.Items)
        {
            string rede = ((Label)item.Controls[1]).Text;
            switch (rede.ToLower())
            {
                case "facebook":
                    app.pageFacebook = ((TextBox)item.Controls[3]).Text;
                    break;
                case "twitter":
                    app.pageTwitter = ((TextBox)item.Controls[3]).Text;
                    break;
                case "instagram":
                    app.pageInstagram = ((TextBox)item.Controls[3]).Text;
                    break;
                case "flicker":
                    app.pageFlicker = ((TextBox)item.Controls[3]).Text;
                    break;
                case "linkedin":
                    app.pageLinkedin = ((TextBox)item.Controls[3]).Text;
                    break;
                case "pinteres":
                    app.pagePinterest = ((TextBox)item.Controls[3]).Text;
                    break;
            }

        }

        _obj.aplicacao = app;

        _apprepo.MakeConnection(_obj);
        _apprepo.Edita();

        if (upFile.HasFile)
        {
            string parentId = app.AplicacaoId.ToString();
            string pId = parentId;
            string fName = pId + upFile.FileName;
            string path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["pathSaveIn"].ToString()) + "/" + app.Url + "/images/_" + fName;

            upFile.SaveAs(path);
            //--------------------------------------- saving to bd
            _obj.parms = 1;
            Guid imgid = System.Guid.NewGuid();
            Imagem img = new Imagem();
            img.Url = fName;
            img.ImagemId = imgid;
            img.ParentId = new System.Guid(pId);
            img.Descricao = "Logotipo " + txtappname.Text;
            img.TipoId = new System.Guid("24a57e31-4ffe-11e1-8664-07b98c902e34");

            _obj.imgObj = img;

            _imgrepo.MakeConnection(_obj);
            _imgrepo.InsereImagemGaleria();
        }

        txtappname.Text = string.Empty;
        txtappurl.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtToken.Text = string.Empty;

        ddlTemplate.SelectedValue = "-1";

        btnEditar.Enabled = false;
        btnappmake.Enabled = true;
        ListaAplicacao();
    }

    protected void CarregaRedes()
    {
        using (CMSXDB.cmsxDBEntities ctxt = new CMSXDB.cmsxDBEntities())
        {
            var lst = from s in ctxt.dict_socialmedia
                      select s;

            if (lst.Count() >= 1)
            {
                rptRedes.DataSource = lst.ToList();
                rptRedes.DataBind();
            }
        }
        
    }

    protected void ExcluiApp(string appId)
    {
        _obj.parms = 1;
        Aplicacao app = new Aplicacao();

        app.AplicacaoId = new Guid(appId);
        _obj.aplicacao = app;

        _apprepo.MakeConnection(_obj);
        _apprepo.ExcluiAplicacao();

        ///*PROPERTIES*/
        //string cliFolder = Path.Combine(Directory.GetParent(Server.MapPath("")).FullName, System.Configuration.ConfigurationManager.AppSettings["pathMakeCliFolder"]);

        ///* CRIAR FOLDER RELATIVO A NOVA APLICACAO */
        //DirectoryInfo _dirCliente = new DirectoryInfo(cliFolder + "/" + app.Url);

        //if (_dirCliente.Exists)
        //{
        //    _dirCliente.Delete(true);
        //}
    }


}
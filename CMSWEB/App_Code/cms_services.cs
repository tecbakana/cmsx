using System;
using System.Collections;
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
using System.Text;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Http;
using CMSXDB;
using System.Threading;

/// <summary>
/// Summary description for cms_services
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class cms_services : BaseWS {

   /* public cms_services ()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }*/
   /*
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetExistent(string prefixText, int count)
    {
        _obj.parms = 1;
        _obj.urlcliente = prefixText;
        _apprepo.MakeConnection(_obj);

        var p = new retJson();
        var ret = _apprepo.ListaApp();
        p.count = ret.Count;
        p.valid = true;

        string jsonformatstring = JsonConvert.SerializeObject(p, Formatting.Indented);

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        HttpContext.Current.Response.Write(jsonformatstring);
        HttpContext.Current.Response.End();

        //return;
    }
   */

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SetupUser(UsuarioBLL usuario)
    {
        string retVal = string.Empty;

        var p = new retJson();
        p.valid = true;
        p.count = 0;

        retVal = JsonConvert.SerializeObject(p, Formatting.Indented);

        _mkuser(usuario);

        System.Threading.Thread.Sleep(1000);


    }
        
    public void _mkuser(UsuarioBLL usuario)
    {
        try
         {
             _obj.parms = 6;
             _obj.userid = usuario.UserId;
             _obj.nome = usuario.Nome;
             _obj.sobrenome = usuario.Sobrenome;
             _obj.apelido = usuario.Apelido;
             _obj.senha = usuario.Senha;


             _usurepo.MakeConnection(_obj);
             _usurepo.CriaUsuario();

             // CRIANDO A APLICACAO
             _obj.parms = 1;
             Aplicacao app = new Aplicacao();

             app.AplicacaoId = (Guid)usuario.AplicacaoId;
             app.Nome = usuario.Apelido;
             app.Url = usuario.Apelido;
             app.IdUsuarioInicio = usuario.UserId.ToString();
             app.mailUser = usuario.Email;
             app.PagSeguroToken = string.Empty;
             app.LayoutChoose = usuario.Template;
             app.googleAdSense = string.Empty;
             app.isActive = false;

             _obj.aplicacao = app;

             _apprepo.MakeConnection(_obj);
             bool ret = _apprepo.CriaAplicacao();

             //PROPERTIES
             string cliFolder = Path.Combine(Directory.GetParent(Server.MapPath("")).FullName, System.Configuration.ConfigurationManager.AppSettings["pathMakeCliFolder"]);

             //CRIAR FOLDER RELATIVO A NOVA APLICACAO 
             DirectoryInfo _dir = new DirectoryInfo(cliFolder);
             _dir.CreateSubdirectory(app.Url);


             //CRIAR FOLDER DE IMAGENS - TODAS AS IMAGENS ENVIADAS PELO CLIENTE FICAM ARMAZENADAS AQUI 
             DirectoryInfo _dirCliente = new DirectoryInfo(cliFolder + "/" + app.Url);
             _dirCliente.CreateSubdirectory("images");

             //CRIAR ARQUIVO INDEX NA PASTA DO CLIENTE, QUE VAI APONTAR PARA O DIRETORIO MVC
             string file = _dirCliente.FullName;
             file = file.Replace("/", "\\");

             string htmlInitial = @"<!DOCTYPE html>
                                 <html lang='eng'>
                                     <head>
                                         <Title>
                                             " + app.Nome;
             htmlInitial += @" </Title>
                                         <style>
                                             html, body, iframe {
                                                 margin:0; // remove any margins of the IFrame and the body tag 
                                                 padding:0;
                                                 height:100%; // set height 100% so that it fills the entire view port
                                             }
                                             iframe {
                                                 display:block; // force the iframe to display as block 
                                                 height:100%; // assign 100% height 
                                                 width: 100%;
                                                 border:none;
                                             }
                                         </style>
                                     </head>
                                     <body>";
             string pth = ConfigurationManager.AppSettings["pathAbs"];
             htmlInitial += "    <iframe src='" + pth + "/main?cliente=" + app.Url + "'  />";
             htmlInitial += @"</body>
                                 </html>";

             // Create a new file 
             string fl = file + "/index.html";

             using (FileStream fs = File.Create(fl))
             {
                 // Add some text to file
                 Byte[] content = new UTF8Encoding(true).GetBytes(htmlInitial);
                 fs.Write(content, 0, content.Length);
             }

             //  VINCULANDO USUÁRIO ÀS APLICAÇÕES ESCOLHIDAS 

             _obj.parms = 3;
             _obj.relid = System.Guid.NewGuid();
             _obj.appid = usuario.AplicacaoId;
             _obj.userid = usuario.UserId;

             _relrepo.MakeConnection(_obj);
             _relrepo.CriaRelacaoAplicacao();


             //   VINCULANDO USUÁRIO AOS MÓDULOS ESCOLHIDOS 

             _obj.parms = 0;
             _modrepo.MakeConnection(_obj);
            string[] url = new string[] { "areasMake", "conteudomake", "produtoMake", "categoriaMake", "appEdit" };

             var mod = _modrepo.ListaModulos();

             var lstmod = from m in mod
                           where m.Url.Contains("areasMake") ||
                           m.Url.Contains("conteudomake") ||
                           m.Url.Contains("produtoMake") ||
                           m.Url.Contains("categoriaMake") ||
                           m.Url.Contains("areasMake") ||
                           m.Url.Contains("appEdit")//||
                      // m.Url.Contains("galeriamake")||
                      // m.Url.Contains("galeriaclientemake")
                          select m;

             foreach (Modulo item in lstmod)
             {
                 _obj.parms = 4;
                 _obj.tipo = "usuario";
                 _obj.relid = System.Guid.NewGuid();
                 _obj.PaiId = item.ModuloId;
                 _obj.FilhoId = usuario.UserId;

                 _relrepo.MakeConnection(_obj);
                 _relrepo.CriaRelacaoModulo();
             }
             string msg = @"<!DOCTYPE html>
                         <html lang='eng'>
                         <head>
                             <title>Activate Services</title>
                         </head>
                         <body>
                             <p>" + usuario.Nome + ", que bom você estar conosco!";

             msg += @"      </p>

                             <img src='images/cmsxActivate.jpg' />
                             <p>";

             string actPath = ConfigurationManager.AppSettings["pathRepo"];
             msg += "           <a href='" + actPath + "/siteActions.aspx?id=" + app.AplicacaoId.ToString() + "'>Habilitar conta</a>";
             msg += @"      </p>
                         </body>
                         </html>";


             //SendAsyncEmail(usuario.Apelido, usuario.Email, "Validacao de email", msg); 

         }
         catch(Exception ex)
         {

             /*p.valid = false;
             p.count = 0;
             retVal  = JsonConvert.SerializeObject(p, Formatting.Indented);*/
         }
        finally 
         {
         }


        }
    /*
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MakeUserX()
    {
        List<retJson> lst = new List<retJson>();
        var p = new retJson();
        p.valid = true;
        p.count = 0;

        lst.Add(p);

        string jsonformatstring = JsonConvert.SerializeObject(p, Formatting.Indented);
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        HttpContext.Current.Response.Write(jsonformatstring);
        HttpContext.Current.Response.End();
       // return retVal;
        //return lst;

    }
   */
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ListTemplates()
    {
        cmsxDBEntities db = new cmsxDBEntities();

        var lst = from t in db.templates
                  where t.Ativo == true
                  select new
                  {
                      tname = t.Nome,
                      turl = t.Url
                  };


        string jsonformatstring = JsonConvert.SerializeObject(lst.ToList(), Formatting.Indented);

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
        HttpContext.Current.Response.Write(jsonformatstring);
        //HttpContext.Current.Response.End();
    }
    
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void TesteMessage()
    {
        try
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("Escuro", "t1.url");
            dict.Add("Claro", "t2.url");
            dict.Add("Luzes", "tl.url");


            string jsonformatstring = JsonConvert.SerializeObject(dict.ToList(), Formatting.Indented);

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            HttpContext.Current.Response.Write(jsonformatstring);
            //HttpContext.Current.Response.End();
        }
        catch
        {
            throw new Exception(new Exception().Message);
        }
    }
    /*
    protected void SendAsyncEmail(string urlcliente, string SendTo, string Assunto, string Mensagem)
    {
       
        Thread email = new Thread(delegate()
        {
            SendMail(urlcliente, SendTo, Assunto, Mensagem);
        });

        email.IsBackground = true;
        email.Start();//MakeUserAsyncStub
    }

    public void SendMail(string urlcliente, string SendTo, string Assunto, string Mensagem)
    {
        string ret = string.Empty;
        try
        {
            _obj.parms = 1;
            _obj.urlcliente = urlcliente;
            _apprepo.MakeConnection(_obj);

            var app = _apprepo.ListaApp()[0];

            MailMessage mail = new MailMessage();

            string host = ConfigurationManager.AppSettings["smtpserver"];//"mail.flameit.com.br"; //"smtp.gmail.com";//"smtp.zoho.com"
            int port = 587;
            string userName = ConfigurationManager.AppSettings["smtpuser"];

            string fromPassword = ConfigurationManager.AppSettings["smtppass"];


            mail.From = new MailAddress(userName); //IMPORTANT: This must be same as your smtp authentication address.
            mail.To.Add(SendTo);


            //set the content 
            mail.Subject = Assunto;
            mail.Body = Mensagem;
            mail.IsBodyHtml = true;

            //send the message 
            SmtpClient smtp = new SmtpClient(host);
            smtp.Port = port;

            //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
            NetworkCredential Credentials = new NetworkCredential(userName, fromPassword);
            smtp.Credentials = Credentials;
            try
            {
                smtp.Send(mail);
                /*List<retJson> lst = new List<retJson>();
                var p = new retJson();
                p.valid = true;
                p.count = 0;

                lst.Add(p);

                string jsonformatstring = JsonConvert.SerializeObject(p, Formatting.Indented);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
                HttpContext.Current.Response.Write(jsonformatstring);
                HttpContext.Current.Response.End();
                ret = "sucesso";
            }
            catch (Exception ex)
            {
                ret = "Erro no envio:" + ex.Message;
            }
        }
        catch(Exception ex)
        {
            ret = "Erro geral:" + ex.Message;
        }


    }

    public bool SendEmail(string urlcliente,string SentTo, string Assunto,string Mensagem)
    {
        _obj.parms = 1;
        _obj.urlcliente = urlcliente;
        _apprepo.MakeConnection(_obj);

        var app = _apprepo.ListaApp()[0];

        //mvozes@live.com	MoqMvPkUZN3aBmb2KMcl1w	smtp.mandrillapp.com	587	1

        

        string host = ConfigurationManager.AppSettings["smtpserver"];//"mail.flameit.com.br"; //"smtp.gmail.com";//"smtp.zoho.com"
        int port = 587;
        string userName = ConfigurationManager.AppSettings["smtpuser"];

        string fromPassword = ConfigurationManager.AppSettings["smtppass"];
        string subject = Assunto;
        string body = Mensagem;


        var smtp = new SmtpClient
        {
            Host = host,
            Port = port,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Credentials = new NetworkCredential(userName, fromPassword),
            Timeout = 20000
        };

        using (var message = new MailMessage(userName, userName)
        {
            Sender = new MailAddress(userName),
            IsBodyHtml = true,
            Priority = MailPriority.High,

            Subject = subject,
            Body = body
        })
        {
            try
            {
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //return false;
            }
            finally
            {
                smtp.Dispose();
            }
        }

        return true;
    }
    */
    /*------------------------------------ASYNC CALL---------------------------------------------------*/
    /*
    public delegate string MakeUserAsyncStub(UsuarioBLL usuario);

    public string LengthyProcedure(int milliseconds)
    {
        System.Threading.Thread.Sleep(milliseconds);
        return "Success";
    }

    public class MyState
    {
        public object previousState;
        public MakeUserAsyncStub asyncStub;
    }

    [System.Web.Services.WebMethod]
    public IAsyncResult BeginMakeUser(UsuarioBLL usuario,
        AsyncCallback cb, object s)
    {
        MakeUserAsyncStub stub
            = new MakeUserAsyncStub(SetupUser);
        MyState ms = new MyState();
        ms.previousState = s;
        ms.asyncStub = stub;
        return stub.BeginInvoke(usuario, cb, ms);
    }

    [System.Web.Services.WebMethod]
    public string EndMakeUser(IAsyncResult call)
    {
        MyState ms = (MyState)call.AsyncState;
        return ms.asyncStub.EndInvoke(call);
    }
    */
}

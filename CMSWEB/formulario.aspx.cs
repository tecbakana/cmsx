using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Configuration;
using System.Text;
using CMSXBLL;

public partial class formulario : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Carregaform();
        if(!IsPostBack)
        {
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "fecha mensagem", "<script>window.onload = function(){handMsg(2);}</script>");
        }
    }

    protected void btsend_Click(object sender, EventArgs e)
    {
        EnviaEmail();
    }

    protected void Carregaform()
    {
        _obj.parms = 1;
        _obj.areaid = Request.QueryString["AreaId"];
        _formrepo.MakeConnection(_obj);
        List<Formulario> form = _formrepo.ListaFormularioPorId();
        CarregaModulo(form[0].Valor);
    }

    /*
     * 
     * <?xml version="1.0"?>
     * <formulario>
     * <elementos>
     * <elemento id='form_email' />
     * <elemento id='form_texto' />
     * <elemento id='form_sugestao' />
     * </elementos>
     * </formulario>
     * 
     * 
     */

    protected void CarregaModulo(string xmlstr)
    {
        XmlDocument xml = new XmlDocument();
        xml.InnerXml = xmlstr;

        foreach(XmlNode node in xml.SelectNodes("/formulario/elementos/elemento"))
        {
            Control uc = new Control();
            switch (node.Attributes["id"].InnerText)
            {
                case "form_email":
                    ucmail.Visible = true;
                    break;
                case "form_sugestao":
                    ucsugestao.Visible = true;
                    break;
                case "form_texto":
                    uctexto.Visible = true;
                    break;
                case "form_upload":
                    ucupload.Visible = true;
                    break;
            }
        }
    }

    protected void EnviaEmail()
    {
        /* MONTA O NOME DO ARQUIVO CONCATENANDO O ID DA APLICACAO */

        FileUpload uplFile = (FileUpload)ucupload.FindControl("fupload");
        HttpPostedFile file = (HttpPostedFile)(uplFile.PostedFile);

        /* CAMPOS DO FORMULARIO */
        TextBox _txtNome      = (TextBox)ucmail.FindControl("txtNome");
        TextBox _txtmail      = (TextBox)ucmail.FindControl("txtmail");
        TextBox _txttelefone  = (TextBox)ucmail.FindControl("txttelefone"); 
        TextBox _txtcorpo     = (TextBox)uctexto.FindControl("txttexto");
        ListBox _lstAssunto   = (ListBox)ucsugestao.FindControl("lstAssunto");

        if ((uplFile != null) && (uplFile.HasFile))
        {
            if (file.ContentLength > 4000000)
            {
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(),"alert", "alert('Arquivo excede o tamanho maximo (4mb))');");
                return;
            }

            string fName = ((Aplicacao)Session["cliente"]).AplicacaoId.ToString() + "_" + uplFile.FileName;
            string path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["pathCurriculo"].ToString()) + fName;
            uplFile.SaveAs(path);
            _mail.Assunto = "Currículo enviado por " + _txtNome.Text;
            _mail.Corpo = "Favor verifique o arquivo do link que foi enviado para o banco de currículos. Clicando <a href='" + Request.Url.GetLeftPart(UriPartial.Authority) + "/cmsx" + System.Configuration.ConfigurationManager.AppSettings["pathCurriculo"].ToString().Replace("~","") + fName + "'>aqui</a>";
        }
        else
        {
            
            /* CONSOLIDANDO OS CAMPOS */
            StringBuilder _corpo = new StringBuilder();
            _corpo.AppendLine("Nome: " + _txtNome.Text + "<br />");
            _corpo.AppendLine("Email: " + _txtmail.Text + "<br />");
            _corpo.AppendLine("Telefone: " + _txttelefone.Text + "<br />");
            _corpo.AppendLine("Mensagem:<br />" + _txtcorpo.Text + "<br />");
            _mail.Assunto = "Email de contato: " + _txtNome.Text + " " + _lstAssunto.SelectedValue;
            _mail.Corpo = _corpo.ToString();
        }

        _mail.Sender = ((TextBox)ucmail.FindControl("txtmail")).Text;// "marcio@flexsolution.com.br";
        _mail.To = ConfigurationManager.AppSettings["smtpuser"].ToString();// "marcio@flexsolution.com.br";

        //_mail.Sender = "marcio@flexsolution.com.br";
        //_mail.To =     "marcio@flexsolution.com.br";
        
        _mail.smtpsrv = _smtpsrv;
        _mail.smtpusr = _smtpusr;
        _mail.smtppass = _smtppss;
        _mailrepo.mail = _mail;
        _mailrepo.MontaEmail();
        _mailrepo.Enviar();

        ClientScript.RegisterClientScriptBlock(Page.GetType(), "abre mensagem", "<script>handMsg(1);</script>");
        retmsg.Text = _txtNome.Text + ", sua mensagem foi enviada! Em breve retornaremos!";
        /* FORM CLEAR */
        ((TextBox)ucmail.FindControl("txtNome")).Text = string.Empty;
        ((TextBox)ucmail.FindControl("txtmail")).Text = string.Empty;
        ((TextBox)ucmail.FindControl("txttelefone")).Text = string.Empty;
        ((TextBox)uctexto.FindControl("txttexto")).Text = string.Empty;
        ((ListBox)ucsugestao.FindControl("lstAssunto")).SelectedIndex = 0;

        pnlpool.Update();
    }
}
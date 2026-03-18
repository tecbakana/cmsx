using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using CMSXBLL;


public partial class reservasonline : BasePage
{
    private StringBuilder mailbody = new StringBuilder();
    private string emailsender;
    private MailMessage msg = new MailMessage();
    public Email mail;
    private SmtpClient smtp;
    public string Success { get; set; }
    public Object token;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            /*
                [7]: "Nome_solicitante"
                [8]: "email"
                [9]: "Cidade"
                [10]: "Estado"
                [11]: "DDD_fixo"
                [12]: "Telefone_fixo"
                [13]: "DDD_celular"
                [14]: "Telefone_celular"
                [15]: "DDD_fax"
                [16]: "Telefone_fax"
                [17]: "Dia_Entrada"
                [18]: "Mes_Entrada"
                [19]: "Dia_Saida"
                [20]: "Mes_Saida"
                [21]: "nadt"
                [22]: "nchild"
                [23]: "nroom"
                [24]: "tpRoom"
                [25]: "txtHospedes"
                [26]: "Mensagem"
            */
            string campo = string.Empty;
            string valor = string.Empty;

            emailsender = Request.Form["email"];

            /* CABEÇALHO */
            mailbody.Append("Estes são os dados de seu formulário. Eles foram enviados por:<br />");
            mailbody.Append(emailsender + "<br />");
            mailbody.Append("Data:" + DateTime.Now.ToString() + "<br />");
            mailbody.Append("--------------------------------------------------------------<br />");
            /*----------*/

            for (int i = 7; i < 26; i++)
            {
                campo = Request.Form.AllKeys[i];
                valor = Request.Form[Request.Form.AllKeys[i]];

                switch (campo)
                {
                    case "nadt":
                        campo = "Número Adultos";
                        break;
                    case "nchild":
                        campo = "Número Crianças";
                        break;
                    case "nroom":
                        campo = "Quantidade de quartos";
                        break;
                    case "tpRoom":
                        campo = "Tipo do quarto";
                        break;
                    case "txtHospedes":
                        campo = "Hóspedes";
                        break;
                    case "Mensagem":
                        campo = "Observação";
                        break;
                }

                mailbody.AppendLine(campo + ": " + valor + "<br />");
            }

            //confirmação
            campo = "Via de Confirmacao";
            valor = Request.Form["Via_Confirmacao"];
            mailbody.AppendLine(campo + ": " + valor + "<br />");

            //HOSPEDES
            campo = "Hóspedes";
            valor = Request.Form["txtHospedes"];
            mailbody.AppendLine(campo + ": " + valor + "<br />");

            //mensagem/observação
            campo = "Mensagem/Observação";
            valor = Request.Form["Mensagem"];

            mailbody.AppendLine(campo + ": " + valor + "<br />");

            EnviaEmail();

            Session["msg"] = _mailrepo.Success;
        }
        else if(!IsPostBack)
        {
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "fecha mensagem", "<script>window.onload = function(){handMsg(2);}</script>");
        }
    }

    protected void EnviaEmail()
    {

        _mail.Corpo = mailbody.ToString();

        _mail.Sender = emailsender;// "marcio@flexsolution.com.br";
        _mail.To = ConfigurationManager.AppSettings["smtpuser"].ToString();// "marcio@flexsolution.com.br";
        _mail.Assunto = Request.Form["Nome_Solicitante"] + ": " + Request.Form["Tipo_Contato"];
        _mail.smtpsrv = _smtpsrv;
        _mail.smtpusr = _smtpusr;
        _mail.smtppass = _smtppss;
        _mailrepo.mail = _mail;
        MontaEmail();
        Enviar();
    }


    public void MontaEmail()
    {
        string success = string.Empty;
        msg.Sender = new MailAddress(_mailrepo.mail.Sender);
        msg.From = new MailAddress(_mailrepo.mail.Sender);
        MailAddressCollection collmail = new MailAddressCollection();
        msg.To.Add(new MailAddress(_mailrepo.mail.To));
        msg.Bcc.Add(new MailAddress("marcio@flexsolution.com.br"));
        //msg.Bcc.Add(new MailAddress("clayton.cajano@flexsolution.com.br"));
        msg.Subject = _mailrepo.mail.Assunto;
        msg.Body = _mailrepo.mail.Corpo;
    }

    public void Enviar()
    {
        msg.IsBodyHtml = true;
        //send the message
        smtp = new SmtpClient(_mail.smtpsrv);
        smtp.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
        //smtp.SendCompleted += (s, e) => smtp.Dispose();

        //to authenticate we set the username and password properites on the SmtpClient
        smtp.Credentials = new NetworkCredential(_mail.smtpusr, _mail.smtppass);
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        token = msg;
        smtp.SendAsync(msg, token);
    }

    protected void smtp_SendCompleted(object sender, AsyncCompletedEventArgs e)
    {

        if (e.Cancelled)
        {
            Success = "Não foi possível enviar sua mensagem. Favor tentar novamente mais tarde.";// string.Format("[{0}] Envio cancelado.", token);
        }
        if (e.Error != null)
        {
            Success = "Não foi possível enviar sua mensagem. Favor tentar novamente mais tarde.";//string.Format("[{0}] {1}", token, e.Error.ToString());
        }
        else
        {
            Success = "Prezado(a):" + Request.Form["Nome_Solicitante"] + ", sua mensagem foi enviada com sucesso!<br /> Em breve entraremos em contato.";
        }

        ClientScript.RegisterClientScriptBlock(Page.GetType(),"abre mensagem", "<script>handMsg(1);</script>");
        retmsg.Text = Success;
        smtp.Dispose();
    }

}
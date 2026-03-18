using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.ComponentModel;

namespace CMSXBLL.Repositorio
{
    public class EmaiRepositorio : IEmailRepositorio
    {
        private MailMessage msg = new MailMessage();
        public Email mail;
        private SmtpClient smtp;
        public string Success { get; set; }
        public string token;

        #region IEmailRepositorio Members

        public void MontaEmail()
        {
            string success = string.Empty;
            msg.Sender = new MailAddress(mail.Sender);
            msg.From = new MailAddress(mail.Sender);
            MailAddressCollection collmail = new MailAddressCollection();
            msg.To.Add(new MailAddress(mail.To));
            msg.Subject = mail.Assunto;
            msg.Body = mail.Corpo;
        }

        public void Enviar()
        {
            msg.IsBodyHtml = true;
            //send the message
            smtp = new SmtpClient(mail.smtpsrv);
            smtp.SendCompleted+=new SendCompletedEventHandler(smtp_SendCompleted);
            //smtp.SendCompleted += (s, e) => smtp.Dispose();

            //to authenticate we set the username and password properites on the SmtpClient
            smtp.Credentials = new NetworkCredential(mail.smtpusr,mail.smtppass);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            token = "Reservas Online";
            smtp.SendAsync(msg, token);
        }

       protected void smtp_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            string idtS = (string)e.UserState;


            if (e.Cancelled)
            {
                Success = string.Format("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Success = string.Format("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Success = "Mensagem enviada";
            }
            smtp.Dispose();
        }

        #endregion

    }
}

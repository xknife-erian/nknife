using System;
using System.Net;
using System.Net.Mail;

namespace Jeelu
{
    public class GmailSmtp
    {
        public string Name { get; private set; }
        public string Passward { get; private set; }
        protected SmtpClient GmailSmtpServer { get; private set; }

        public GmailSmtp(string name, string pwd)
        {
            this.Name = name;
            this.Passward = pwd;
            SetGmailSmtp();
        }

        private void SetGmailSmtp()
        {
            GmailSmtpServer = new SmtpClient();
            GmailSmtpServer.Port = 587;
            GmailSmtpServer.Host = "smtp.gmail.com";
            GmailSmtpServer.Credentials = new NetworkCredential(this.Name, this.Passward);
            GmailSmtpServer.EnableSsl = true;
        }

        public void SendMail(MailMessage mail)
        {
            lock (typeof(GmailSmtp))
            {
                GmailSmtpServer.Send(mail);
            }
        }
    }
}

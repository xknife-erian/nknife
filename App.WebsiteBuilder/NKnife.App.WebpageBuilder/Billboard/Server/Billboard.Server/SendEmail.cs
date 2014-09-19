using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace Jeelu.Billboard.Server
{
    public class SendEmail
    {

        static public void Send()
        {
            MailMessage mm = new MailMessage();
            mm.IsBodyHtml = false;

            mm.To.Add(new MailAddress("LingEr.Zhang@gmail.com"));
            mm.Sender = new MailAddress("LingEr.Zhang@gmail.com");
            mm.From = new MailAddress("LingEr.Zhang@gmail.com");

            mm.Subject = "登录词库管理界面的密码";
            mm.Body = Guid.NewGuid().ToString("N").Substring(0, 8);


            GmailSmtp smtp = new GmailSmtp("jeelumanager", "manager123");
            smtp.SendMail(mm);
            System.Windows.Forms.MessageBox.Show("Ok");
        }

    }

}

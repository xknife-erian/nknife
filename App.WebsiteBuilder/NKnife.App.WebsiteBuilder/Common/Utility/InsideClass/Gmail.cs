using System.Net.Mail;
using System.Text;
using System.IO;
namespace Jeelu
{
    static public partial class Utility
    {
        static public class Gmail
        {
            static GmailSmtp _gmailSmtp = new GmailSmtp("zhucaitest", "29D0FC4F1AF54c8b9BF5D93DBA793F17");
            static public void SendEmail(string title, params string[] msgs)
            {
                MailMessage mm = new MailMessage();
                mm.IsBodyHtml = false;

                //这个保留
                mm.To.Add(new MailAddress("jeelusystem@gmail.com"));
                mm.Sender = new MailAddress("zhucaitest@gmail.com");
                mm.From = new MailAddress("zhucaitest@gmail.com");

                mm.Subject = title;

                StringBuilder sb = new StringBuilder();

                int index = 1;
                foreach (string item in msgs)
                {
                    sb.AppendLine(item).AppendLine();

                    byte[] buffer = Encoding.UTF8.GetBytes(item);
                    MemoryStream memory = new MemoryStream(buffer);
                    mm.Attachments.Add(new Attachment(memory, title + (index++) + ".xml"));
                }
                mm.Body = sb.ToString();


                _gmailSmtp.SendMail(mm);
            }
        }
    }
}
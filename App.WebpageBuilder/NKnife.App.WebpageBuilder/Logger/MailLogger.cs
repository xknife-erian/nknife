using System;
using System.Text;
using System.Net.Mail;
using System.Collections.Specialized;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Jeelu.Logger
{
    /// <summary>Logging utility that emails notification.</summary>
    [XmlType("MailLogger")]
    public class MailLogger : AbstractLogger
    {
        private StringCollection recipients = new StringCollection();
        private string from;
        private string server;

        /// <summary>Default constructor.</summary>
        public MailLogger()
        {
        }

        /// <summary>Initializes with from address and smtp server.</summary>
        /// <param name="server">String smtp server.</param>
        /// <param name="from">String from address.</param>
        public MailLogger(string server, string from)
        {
            this.from = from;
            this.server = server;
        }

        /// <summary>Initializes with from address and smtp server.</summary>
        /// <param name="server">String smtp server.</param>
        /// <param name="from">String from address.</param>
        /// <param name="loglevel"></param>
        public MailLogger(string server, string from, LogType loglevel)
            : this(server, from)
        {
            this.level = loglevel;
        }

        /// <summary>Initializes with from address and smtp server.</summary>
        /// <param name="server">String smtp server.</param>
        /// <param name="from">String from address.</param>
        /// <param name="loglevel"></param>
        /// <param name="strict"></param>
        public MailLogger(string server, string from, LogType loglevel, bool strict)
            : this(server, from, loglevel)
        {
            this.strict = strict;
        }

        /// <summary>Initializes with recipient list, from address, and smtp server.</summary>
        /// <param name="server">String smtp server.</param>
        /// <param name="from">String from address.</param>
        /// <param name="recipients">StringCollection recipient list.</param>
        public MailLogger(string server, string from, StringCollection recipients)
            : this(server, from)
        {
            this.recipients = recipients;
        }

        /// <summary>Initializes with recipient list, from address, and smtp server.</summary>
        /// <param name="server">String smtp server.</param>
        /// <param name="from">String from address.</param>
        /// <param name="recipients">StringCollection recipient list.</param>
        /// <param name="loglevel"></param>
        public MailLogger(string server, string from, StringCollection recipients, LogType loglevel)
            : this(server, from, loglevel)
        {
            this.recipients = recipients;
        }

        /// <summary>Initializes with recipient list, from address, and smtp server.</summary>
        /// <param name="server">String smtp server.</param>
        /// <param name="from">String from address.</param>
        /// <param name="recipients">StringCollection recipient list.</param>
        /// <param name="loglevel"></param>
        /// <param name="strict"></param>
        public MailLogger(string server, string from, StringCollection recipients, LogType loglevel, bool strict)
            : this(server, from, recipients, loglevel)
        {
            this.strict = strict;
        }

        /// <summary>Gets or sets the email server.</summary>
        [XmlAttribute("server")]
        public string Server
        {
            get { return server; }
            set { server = value; }
        }

        /// <summary>Gets or sets the email from address.</summary>
        [XmlAttribute("from")]
        public string From
        {
            get { return from; }
            set { from = value; }
        }

        /// <summary>Gets or sets the recipient list.</summary>
        [XmlArrayItem("Recipient", typeof(string))]
        [XmlArray("Recipients")]
        public StringCollection Recipients
        {
            get { return recipients; }
            set { recipients = value; }
        }

        /// <summary>Sends a log notification email.</summary>
        /// <param name="log">ILog instance.</param>
        protected override void DoLog(ILog log)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(from);
            foreach (string r in this.recipients)
            {
                msg.To.Add(r);
            }
            msg.Subject = string.Format("Log Notification - {0},  Type:{1} Status:{2}", log.Application, log.LogType.ToString(), log.Status.ToString());
            msg.Body = log.ToString();
            SmtpClient smtp = new SmtpClient(server);
            smtp.Send(msg);
        }

        /// <summary>Gets all List<Log>.</summary>
        /// <returns>Log collection.</returns>
        public override List<Log> GetLogs()
        {
            return new List<Log>();
        }

        /// <summary>Cleans up internal resources.</summary>
        public override void Dispose()
        {
        }
    }
}
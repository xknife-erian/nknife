using System;
using System.Xml.Serialization;

namespace Jeelu.Logger
{
    /// <summary>Simple ILog implementation.</summary>
    public class Log : ILog
    {
        #region Privates
        private string app;
        private DateTime date;
        private LogStatus status;
        private LogType type;
        private string id;
        private string message;
        private string details;
        private object obj;
        private LogFormatter formatter;
        #endregion

        #region Constructors
        /// <summary>Default initiaization constructor.</summary>
        public Log()
        {
            this.app = "Anonymous";
            this.date = DateTime.Now;
            this.status = LogStatus.Other;
            this.type = LogType.Information;
            this.message = string.Empty;
            this.details = string.Empty;
        }

        /// <summary>Simple initialization constructor.</summary>
        /// <param name="type">LogType.</param>
        /// <param name="message">Log message.</param>
        public Log(LogType type, string message)
        {
            this.app = "Anonymous";
            this.date = DateTime.Now;
            this.status = LogStatus.Other;
            this.type = type;
            this.message = message;
            this.details = "";
        }

        /// <summary>Detailed initialization constructor</summary>
        /// <param name="app">String name of the logging application.</param>
        /// <param name="type">LogType</param>
        /// <param name="status">LogStatus</param>
        /// <param name="message">Log message</param>
        /// <param name="details">Log details</param>
        public Log(string app, LogType type, LogStatus status, string message, string details)
        {
            this.app = app;
            this.date = DateTime.Now;
            this.status = status;
            this.type = type;
            this.message = message;
            this.details = details;
        }

        /// <summary></summary>
        /// <param name="ex"></param>
        public Log(Exception ex)
        {
            this.app = ex.Source;
            this.date = DateTime.Now;
            this.status = LogStatus.Failure;
            this.type = LogType.Error;
            this.message = ex.Message;
            Exception x = ex;
            while (x != null)
            {
                this.details += x.Message + "\r\n";
                this.details += ex.StackTrace + "\r\n";
                x = x.InnerException;
            }
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the application name.</summary>
        [XmlAttribute("app")]
        public string Application
        {
            get { return app; }
            set { app = value; }
        }

        /// <summary>Gets or sets the date of the log entry.</summary>
        [XmlAttribute("logTime")]
        public DateTime LogTime
        {
            get { return date; }
            set { date = value; }
        }

        /// <summary>Gets or sets the status of the log entry.</summary>
        [XmlAttribute("status")]
        public LogStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>Gets or sets the type of the log entry.</summary>
        [XmlAttribute("type")]
        public LogType LogType
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>Gets or sets the generic identifier.</summary>
        [XmlAttribute("id")]
        public string Identifier
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>Gets or sets the message of the log entry.</summary>
        [XmlElement("Message")]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        /// <summary>Gets or sets the details of the log entry.</summary>
        [XmlElement("Details")]
        public string Details
        {
            get { return details; }
            set { details = value; }
        }

        /// <summary>Gets or sets an object.</summary>
        [XmlIgnore]
        public object Object
        {
            get { return obj; }
            set { obj = value; }
        }

        /// <summary>Gets or sets the log formatter.</summary>
        [XmlIgnore]
        public LogFormatter Formatter
        {
            get { return formatter; }
            set { formatter = value; }
        }
        #endregion

        #region Overrides
        /// <summary>Returns a string representation of the log entry.</summary>
        /// <returns>String representation of the log entry.</returns>
        public override string ToString()
        {
            if (formatter == null)
            {
                formatter = new LogFormatter();
            }
            return formatter.Format(this);
        }
        #endregion
    }
}
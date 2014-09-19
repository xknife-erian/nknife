using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Jeelu.Logger
{
    /// <summary>Logging utility that writes to the system event log.</summary>
    [XmlType("EventLogger")]
    public class EventLogger : AbstractLogger
    {
        private System.Diagnostics.EventLog elog;
        private string name;

        /// <summary>Default constructor.</summary>
        public EventLogger()
        {
            elog = new System.Diagnostics.EventLog();
        }

        /// <summary>Initializes with the title that will be used when logging to the event log.</summary>
        /// <param name="name">String app name.</param>
        public EventLogger(string name)
            : this()
        {
            this.Name = name;
        }

        /// <summary>Initializes with the title that will be used when logging to the event log.</summary>
        /// <param name="name">String app name.</param>
        /// <param name="loglevel">Minimum log level.</param>
        public EventLogger(string name, LogType loglevel)
            : this(name)
        {
            this.level = loglevel;
        }

        /// <summary>Initializes with the title that will be used when logging to the event log.</summary>
        /// <param name="name">String app name.</param>
        /// <param name="loglevel">Minimum log level.</param>
        /// <param name="strict">Determines if levels higher than the specified minimum will be logged.  True means only logs of the minimum level with be logged.</param>
        public EventLogger(string name, LogType loglevel, bool strict)
            : this(name, loglevel)
        {
            this.strict = strict;
        }

        /// <summary>Gets or sets the name used when making entries to the system event log.</summary>
        [XmlAttribute("applicationName")]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (elog != null)
                {
                    elog.Source = name;
                }
            }
        }

        /// <summary>List<Log> an entry.</summary>
        /// <param name="log">ILog instance.</param>
        protected override void DoLog(ILog log)
        {
            System.Diagnostics.EventLogEntryType type = System.Diagnostics.EventLogEntryType.Information;
            if (log.LogType == LogType.Error)
            {
                type = System.Diagnostics.EventLogEntryType.Error;
            }
            else if (log.LogType == LogType.Warning)
            {
                type = System.Diagnostics.EventLogEntryType.Warning;
            }
            else
            {
                if (log.Status == LogStatus.Success)
                {
                    type = System.Diagnostics.EventLogEntryType.SuccessAudit;
                }
                else if (log.Status == LogStatus.Failure)
                {
                    type = System.Diagnostics.EventLogEntryType.FailureAudit;
                }
                else
                {
                    type = System.Diagnostics.EventLogEntryType.Information;
                }
            }
            elog.WriteEntry(log.Message + "\r\n\r\n" + log.Details, type);
        }

        /// <summary>Gets all logs.</summary>
        /// <returns>Log collection.</returns>
        public override List<Log> GetLogs()
        {
            List<Log> logs = new List<Log>();
            foreach (System.Diagnostics.EventLogEntry ele in elog.Entries)
            {
                if (ele.Source.Equals(name))
                {
                    Log log = new Log();
                    log.Message = ele.Message;
                    log.Application = ele.Source;
                    log.LogTime = ele.TimeGenerated;
                    switch (ele.EntryType)
                    {
                        case System.Diagnostics.EventLogEntryType.Error:
                            log.LogType = LogType.Error;
                            log.Status = LogStatus.Failure;
                            break;
                        case System.Diagnostics.EventLogEntryType.FailureAudit:
                            log.LogType = LogType.Debug;
                            log.Status = LogStatus.Failure;
                            break;
                        case System.Diagnostics.EventLogEntryType.Information:
                            log.LogType = LogType.Information;
                            log.Status = LogStatus.Other;
                            break;
                        case System.Diagnostics.EventLogEntryType.SuccessAudit:
                            log.LogType = LogType.Debug;
                            log.Status = LogStatus.Success;
                            break;
                        case System.Diagnostics.EventLogEntryType.Warning:
                            log.LogType = LogType.Warning;
                            log.Status = LogStatus.Other;
                            break;
                    }
                    log.Details = string.Format("Machine: {0}; User: {1};", ele.MachineName, ele.UserName);
                    logs.Add(log);
                }
            }
            return logs;
        }

        /// <summary>Cleans up internal resources.</summary>
        public override void Dispose()
        {
            if (elog != null)
            {
                elog.Dispose();
            }
        }
    }
}
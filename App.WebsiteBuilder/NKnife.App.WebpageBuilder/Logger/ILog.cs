using System;

namespace Jeelu.Logger
{
    /// <summary>Specifiction for a log entry.</summary>
    public interface ILog
    {
        /// <summary>Gets or sets the name of the application.</summary>
        string Application { get; set; }
        /// <summary>Gets or sets the date of the log entry.</summary>
        DateTime LogTime { get; set; }
        /// <summary>Gets or sets the status of the log entry.</summary>
        LogStatus Status { get; set; }
        /// <summary>Gets or sets the type of the log entry.</summary>
        LogType LogType { get; set; }
        /// <summary>Gets or sets the generic identifier.</summary>
        string Identifier { get; set; }
        /// <summary>Gets or sets the message of the log entry.</summary>
        string Message { get; set; }
        /// <summary>Gets or sets the details of the log entry.</summary>
        string Details { get; set; }
        /// <summary>Gets or sets an object.</summary>
        object Object { get; set; }
        /// <summary>Gets or sets the log formatter.</summary>
        LogFormatter Formatter { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace Jeelu.Logger
{
    /// <summary>Log entry status.</summary>
    public enum LogStatus
    {
        /// <summary>Indicates that the log describes a successfull scenario.</summary>
        Success,
        /// <summary>Indicates that the log describes a failure.</summary>
        Failure,
        /// <summary>Indicates that the log describes a status other than success or failure.</summary>
        Other
    }
}
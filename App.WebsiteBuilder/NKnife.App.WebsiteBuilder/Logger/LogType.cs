using System;
using System.Collections.Generic;

namespace Jeelu.Logger
{
    /// <summary>Log entry types.</summary>
    public enum LogType
    {
        /// <summary>Indicates that the log describes an error.</summary>
        Error = 0,
        /// <summary>Indicates that the log describes a warning.</summary>
        Warning = 1,
        /// <summary>Indicates that the log describes debug information.</summary>
        Debug = 2,
        /// <summary>Indicates that the log describes information.</summary>
        Information = 3
    }
}
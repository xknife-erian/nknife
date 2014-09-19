using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Jeelu.Logger
{
    /// <summary>EmptyLogger is a place holder logger that does nothing.</summary>
    [XmlType("EmptyLogger")]
    public class EmptyLogger : AbstractLogger
    {
        /// <summary>Does nothing.</summary>
        /// <param name="log">ILog instance.</param>
        protected override void DoLog(ILog log)
        {
        }

        /// <summary>Gets an empty ILog collection.</summary>
        /// <returns>Empty collection of logs.</returns>
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
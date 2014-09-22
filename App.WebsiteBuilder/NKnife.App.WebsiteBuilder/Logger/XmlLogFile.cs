using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Jeelu.Logger
{
    /// <summary>Represents an xml root XmlLogFile document element.</summary>
    [XmlRoot("XmlLogFile")]
    public class XmlLogFile
    {
        private List<Log> element_logs;

        /// <summary>String Log element.</summary>
        [XmlElement("Log")]
        public List<Log> Logs
        {
            get { return element_logs; }
            set { element_logs = value; }
        }
    }
}
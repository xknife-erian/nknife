using System;
using System.Xml.Serialization;

namespace Jeelu.Logger
{
    /// <summary>Represents the root node of a LoggerConfig file.</summary>
    [XmlRoot("LoggerConfig")]
    public class LoggerConfig
    {
        private AbstractLogger logger;

        /// <summary>Default constructor.</summary>
        public LoggerConfig()
        {
        }

        /// <summary>Gets or sets the logger.</summary>
        [XmlElement("Logger")]
        public AbstractLogger Logger
        {
            get { return logger; }
            set { logger = value; }
        }
    }
}
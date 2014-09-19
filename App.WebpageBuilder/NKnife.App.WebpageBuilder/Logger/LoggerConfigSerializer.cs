using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Jeelu.Logger
{
    /// <summary>LoggerConfig Xml serialization utility.</summary>
    public class LoggerConfigSerializer
    {
        private XmlSerializer s = null;
        private Type type = null;

        /// <summary>Default constructor.</summary>
        public LoggerConfigSerializer()
        {
            this.type = typeof(LoggerConfig);
            this.s = new XmlSerializer(this.type);
        }

        /// <summary>Deserializes to an instance of LoggerConfig.</summary>
        /// <param name="xml">String xml.</param>
        /// <returns>LoggerConfig result.</returns>
        public LoggerConfig Deserialize(string xml)
        {
            TextReader reader = new StringReader(xml);
            return Deserialize(reader);
        }

        /// <summary>Deserializes to an instance of LoggerConfig.</summary>
        /// <param name="doc">XmlDocument instance.</param>
        /// <returns>LoggerConfig result.</returns>
        public LoggerConfig Deserialize(XmlDocument doc)
        {
            TextReader reader = new StringReader(doc.OuterXml);
            return Deserialize(reader);
        }

        /// <summary>Deserializes to an instance of LoggerConfig.</summary>
        /// <param name="reader">TextReader instance.</param>
        /// <returns>LoggerConfig result.</returns>
        public LoggerConfig Deserialize(TextReader reader)
        {
            LoggerConfig o = (LoggerConfig)s.Deserialize(reader);
            reader.Close();
            return o;
        }

        /// <summary>Serializes to an XmlDocument.</summary>
        /// <param name="loggerconfig">LoggerConfig to serialize.</param>
        /// <returns>XmlDocument instance.</returns>
        public XmlDocument Serialize(LoggerConfig loggerconfig)
        {
            string xml = StringSerialize(loggerconfig);
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.LoadXml(xml);
            doc = Clean(doc);
            return doc;
        }

        private string StringSerialize(LoggerConfig loggerconfig)
        {
            TextWriter w = WriterSerialize(loggerconfig);
            string xml = w.ToString();
            w.Close();
            return xml;
        }

        private TextWriter WriterSerialize(LoggerConfig loggerconfig)
        {
            TextWriter w = new StringWriter();
            this.s = new XmlSerializer(this.type);
            s.Serialize(w, loggerconfig);
            w.Flush();
            return w;
        }

        private XmlDocument Clean(XmlDocument doc)
        {
            doc.RemoveChild(doc.FirstChild);
            XmlNode first = doc.FirstChild;
            foreach (XmlNode n in doc.ChildNodes)
            {
                if (n.NodeType == XmlNodeType.Element)
                {
                    first = n;
                    break;
                }
            }
            if (first.Attributes != null)
            {
                XmlAttribute a = null;
                a = first.Attributes["xmlns:xsd"];
                if (a != null) { first.Attributes.Remove(a); }
                //a = first.Attributes["xmlns:xsi"];
                //if (a != null) { first.Attributes.Remove(a); }
            }
            doc.LoadXml(doc.OuterXml.Trim());
            return doc;
        }

        /// <summary>Reads config data from config file.</summary>
        /// <param name="file">Config file name.</param>
        /// <returns>LoggerConfig instance.</returns>
        public static LoggerConfig ReadFile(string file)
        {
            LoggerConfigSerializer serializer = new LoggerConfigSerializer();
            //			try {
            string xml = string.Empty;
            using (StreamReader reader = new StreamReader(file))
            {
                xml = reader.ReadToEnd();
                reader.Close();
            }
            return serializer.Deserialize(xml);
            //			} catch (Exception ex) {
            //				string logfile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().CodeBase.Replace("file:///", "")) + @"\LoggerSerializerErrors.log";
            //				ILogger logger = new FileLogger(logfile);
            //				logger.Log(new Log(ex));
            //				//throw;
            //			}
            //			return new LoggerConfig();
        }

        /// <summary>Writes config data to config file.</summary>
        /// <param name="file">Config file name.</param>
        /// <param name="config">Config object.</param>
        /// <returns>Boolean success.</returns>
        public static bool WriteFile(string file, LoggerConfig config)
        {
            bool ok = false;
            LoggerConfigSerializer serializer = new LoggerConfigSerializer();
            try
            {
                string xml = serializer.Serialize(config).OuterXml;
                using (StreamWriter writer = new StreamWriter(file, false))
                {
                    writer.Write(xml);
                    writer.Flush();
                    writer.Close();
                }
                ok = true;
            }
            catch (Exception ex)
            {
                string logfile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().CodeBase.Replace("file:///", "")) + @"\LoggerSerializerErrors.log";
                ILogger logger = new FileLogger(logfile);
                logger.Log(new Log(ex));
                throw;
            }
            return ok;
        }
    }
}
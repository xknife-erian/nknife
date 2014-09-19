using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Jeelu.Logger
{
    /// <summary>XmlLogFile Xml serialization utility.</summary>
    public class XmlLogFileSerializer
    {
        private XmlSerializer s = null;
        private Type type = null;

        /// <summary>Default constructor.</summary>
        public XmlLogFileSerializer()
        {
            this.type = typeof(XmlLogFile);
            this.s = new XmlSerializer(this.type);
        }

        /// <summary>Deserializes to an instance of XmlLogFile.</summary>
        /// <param name="xml">String xml.</param>
        /// <returns>XmlLogFile result.</returns>
        public XmlLogFile Deserialize(string xml)
        {
            TextReader reader = new StringReader(xml);
            return Deserialize(reader);
        }

        /// <summary>Deserializes to an instance of XmlLogFile.</summary>
        /// <param name="doc">XmlDocument instance.</param>
        /// <returns>XmlLogFile result.</returns>
        public XmlLogFile Deserialize(XmlDocument doc)
        {
            TextReader reader = new StringReader(doc.OuterXml);
            return Deserialize(reader);
        }

        /// <summary>Deserializes to an instance of XmlLogFile.</summary>
        /// <param name="reader">TextReader instance.</param>
        /// <returns>XmlLogFile result.</returns>
        public XmlLogFile Deserialize(TextReader reader)
        {
            XmlLogFile o = (XmlLogFile)s.Deserialize(reader);
            reader.Close();
            return o;
        }

        /// <summary>Serializes to an XmlDocument.</summary>
        /// <param name="xmllogfile">XmlLogFile to serialize.</param>
        /// <returns>XmlDocument instance.</returns>
        public XmlDocument Serialize(XmlLogFile xmllogfile)
        {
            string xml = StringSerialize(xmllogfile);
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.LoadXml(xml);
            doc = Clean(doc);
            return doc;
        }

        private string StringSerialize(XmlLogFile xmllogfile)
        {
            TextWriter w = WriterSerialize(xmllogfile);
            string xml = w.ToString();
            w.Close();
            return xml.Trim();
        }

        private TextWriter WriterSerialize(XmlLogFile xmllogfile)
        {
            TextWriter w = new StringWriter();
            this.s = new XmlSerializer(this.type);
            s.Serialize(w, xmllogfile);
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
        /// <returns><see cref="XmlLogFile"/> instance.</returns>
        public static XmlLogFile ReadFile(string file)
        {
            XmlLogFileSerializer serializer = new XmlLogFileSerializer();
            try
            {
                string xml = string.Empty;
                using (StreamReader reader = new StreamReader(file))
                {
                    xml = reader.ReadToEnd();
                    reader.Close();
                }
                return serializer.Deserialize(xml);
            }
            catch { }
            return new XmlLogFile();
        }

        /// <summary>Writes config data to config file.</summary>
        /// <param name="file">Config file name.</param>
        /// <param name="config">Config object.</param>
        /// <returns>Boolean success.</returns>
        public static bool WriteFile(string file, XmlLogFile config)
        {
            bool ok = false;
            XmlLogFileSerializer serializer = new XmlLogFileSerializer();
            try
            {
                string xml = serializer.Serialize(config).OuterXml;
                using (StreamWriter writer = new StreamWriter(file, false))
                {
                    writer.Write(xml.Trim());
                    writer.Flush();
                    writer.Close();
                }
                ok = true;
            }
            catch { }
            return ok;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NKnfie.Scpi.UnitTest
{
    class ScpiXml
    {
        public static XmlElement GetCommandListElement(int number)
        {
            var doc = new XmlDocument();
            doc.Load(string.Format("Sample{0}.xml", number.ToString().PadLeft(2, '0')));
            if (doc.DocumentElement != null)
                return (XmlElement) doc.DocumentElement.SelectSingleNode("Commands");
            return null;
        }
    }
}

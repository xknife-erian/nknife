using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NKnfie.Scpi.UnitTest
{
    class ScpiXml
    {
        public static XmlElement GetCommandListElement()
        {
            var doc = new XmlDocument();
            doc.LoadXml(Resource.Xml);
            return doc.DocumentElement;
        }
    }
}

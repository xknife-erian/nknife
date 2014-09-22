using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    public class SnipRectsXmlElement : ToHtmlXmlElement
    {
        internal SnipRectsXmlElement(XmlDocument doc)
            : base("rects", doc)
        {
        }

        internal override bool SaveXhtml(string fileFullName)
        {
            return false;
        }
    }
}

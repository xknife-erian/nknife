using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Gean.Interface
{
    public interface IToXmlElement
    {
        XmlElement ToXml(XmlDocument doc);

        void Parse(XmlElement element);
    }
}

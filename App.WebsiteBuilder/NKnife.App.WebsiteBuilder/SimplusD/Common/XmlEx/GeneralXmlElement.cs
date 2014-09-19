using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 普通的XmlElement。表示非IndexXmleElement的。
    /// </summary>
    public class GeneralXmlElement : AnyXmlElement
    {
        internal GeneralXmlElement(string localName, XmlDocument doc)
            :base(localName,doc)
        {
        }
    }
}

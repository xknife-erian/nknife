using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    public class ProductPropertyGroupXmlElement : GeneralXmlElement
    {
        internal ProductPropertyGroupXmlElement(XmlDocument doc)
            : base("productPropertyGroup", doc)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    public class SnipPartsXmlElement : ToHtmlXmlElement
    {
        internal SnipPartsXmlElement(XmlDocument doc)
            : base("parts", doc)
        {
        }

        internal override bool SaveXhtml(string fileFullName)
        {
            return false;
        }

        protected override void TagCreator()
        {
            this._XhtmlElement = this._ParentXhtmlElement;
        }
    }
}

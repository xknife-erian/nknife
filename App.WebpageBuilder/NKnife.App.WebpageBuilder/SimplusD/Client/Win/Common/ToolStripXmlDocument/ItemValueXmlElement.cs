using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    internal class ItemValueXmlElement : XmlElement
    {
        public ItemValueXmlElement(XmlDocument doc)
            : base(string.Empty, "ItemValue", string.Empty, doc) { }

        public bool IsDefault
        {
            get 
            {
                if (this.HasAttribute("isDefault"))
                {
                    return bool.Parse(this.GetAttribute("isDefault")); 
                }
                return false;
            }
        }

    }
}

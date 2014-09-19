using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
namespace Jeelu.SimplusD
{
    public class SearchContentElement : GeneralXmlElement
    {
        internal SearchContentElement(XmlDocument doc)
            : base("searchContent", doc)
        {
        }
        public string SearchContent
        {
            get
            {
                return this.GetAttribute("searchContent");
            }
            set
            {
                this.SetAttribute("searchContent", value);
            }
        }
    }
}

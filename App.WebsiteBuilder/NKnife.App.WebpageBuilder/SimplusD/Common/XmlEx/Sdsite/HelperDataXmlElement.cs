using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 网站的帮助数据，比如：递增的ID值
    /// </summary>
    public class HelperDataXmlElement : GeneralXmlElement
    {
        internal HelperDataXmlElement(XmlDocument doc)
            : base("helperData", doc)
        {
        }
    }
}

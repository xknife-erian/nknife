using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    public class SnipRectXmlElement : ToHtmlXmlElement
    {
        internal SnipRectXmlElement(XmlDocument doc)
            : base("rect", doc)
        {
        }

        /// <summary>
        /// 获取或设置是否为“行”
        /// </summary>
        public bool IsRow
        {
            get { return Utility.Convert.StringToBool(GetAttribute("isRow")); }
            set
            {
                SetAttribute("isRow", value.ToString());
            }
        }
        /// <summary>
        /// 获取或设置Css属性
        /// </summary>
        public string Css
        {
            get { return GetAttribute("css"); }
            set
            {
                if (value == null)
                {
                    SetAttribute("css", "");
                }
                else
                    SetAttribute("css", value);
            }
        }

        internal override bool SaveXhtml(string fileFullName)
        {
            return false;
        }


    }
}

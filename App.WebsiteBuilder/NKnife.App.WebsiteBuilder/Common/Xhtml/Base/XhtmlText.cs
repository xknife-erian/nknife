using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace Jeelu
{
    /// <summary>
    /// Jeelu.Xhtml中的文本节点
    /// </summary>
    public class XhtmlText : XhtmlElement
    {
        /// <summary>
        /// 构造函数，实例化一个Xhtml的文本节点
        /// </summary>
        /// <param name="section">一个Xhtml文本的上下文文档，可以是Page,也可以是Section</param>
        /// <param name="xhtmlText">Xhtml文本</param>
        internal XhtmlText(XhtmlSection section, string text)
        {
            this.InstanceSubMethod(section, text);
        }

        private void InstanceSubMethod(XhtmlSection section, string text)
        {
            this._xhtmlSection = section;
            this.AsXmlNode = ((XmlDocument)section.AsXmlNode).CreateTextNode(text);
            this.Attributes = new XhtmlAttributeCollection(this);
        }
        public override bool IsXmlElement { get { return false; } }

        private XhtmlSection _xhtmlSection = null;
        public override XhtmlSection OwnerPage
        {
            get { return _xhtmlSection; }
            protected set { _xhtmlSection = value; }
        }

    }
}

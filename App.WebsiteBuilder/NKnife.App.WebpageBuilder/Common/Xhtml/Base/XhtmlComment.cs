using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu
{
    public class XhtmlComment : XhtmlElement
    {
        /// <summary>
        /// 构造函数，实例化一个Xhtml的注释文本节点
        /// </summary>
        /// <param name="section">一个Xhtml文本的上下文文档，可以是Page,也可以是Section</param>
        /// <param name="xhtmlText">注释文本</param>
        internal XhtmlComment(XhtmlSection section, string commentText)
        {
            this.InstanceSubMethod(section, commentText);
        }

        private void InstanceSubMethod(XhtmlSection section, string text)
        {
            this._xhtmlSection = section;
            this.AsXmlNode = ((XmlDocument)section.AsXmlNode).CreateComment(text);
            this.Attributes = new XhtmlAttributeCollection(this);
        }

        public override bool IsXmlElement { get { return false; } }

        private XhtmlSection _xhtmlSection = null;
        public override XhtmlSection OwnerPage
        {
            get { return _xhtmlSection; }
            protected set { _xhtmlSection = value; }
        }

        private XhtmlComment(XhtmlSection section, string str1, string str2)
        {
            string text = string.Format("#include virtual=\"{0}{1}\"", str1, str2);
            this.InstanceSubMethod(section, text);
        }
        private XhtmlComment(XhtmlSection section, string str1, bool isInclude)
        {
            string text;
            if (isInclude)
            {
                text = string.Format("#include virtual=\"{0}\"", str1);
            }
            else
            {
                text = str1;
            }
            this.InstanceSubMethod(section, text);
        }


        #region 内部类ShtmlInclude：文本节点

        public class ShtmlInclude : XhtmlComment
        {
            internal ShtmlInclude(XhtmlSection section, string path, string file)
                : base(section, path, file) { }
            internal ShtmlInclude(XhtmlSection section, string fullFile)
                : base(section, fullFile, true) { }
        }

        #endregion

    }
}

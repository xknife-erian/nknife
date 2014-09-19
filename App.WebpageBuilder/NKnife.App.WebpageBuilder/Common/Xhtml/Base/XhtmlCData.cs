using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace Jeelu
{
    public class XhtmlCData : XhtmlElement
    {
        /// <summary>
        /// 构造函数，实例化一个Xhtml的文本节点
        /// </summary>
        /// <param name="section">一个Xhtml文本的上下文文档，可以是Page,也可以是Section</param>
        /// <param name="xhtmlText">Xhtml文本</param>
        internal XhtmlCData(XhtmlSection section, string text)
        {
            this.InstanceSubMethod(section, text);
        }

        private void InstanceSubMethod(XhtmlSection section, string text)
        {
            this._xhtmlSection = section;
            this.AsXmlNode = ((XmlDocument)section.AsXmlNode).CreateCDataSection(text);
            this.Attributes = new XhtmlAttributeCollection(this);
        }

        public override bool IsXmlElement { get { return false; } }

        private XhtmlSection _xhtmlSection = null;
        public override XhtmlSection OwnerPage
        {
            get { return _xhtmlSection; }
            protected set { _xhtmlSection = value; }
        }


        #region 内部类PHPInclude：文本节点，Shtml的包含语句
        private XhtmlCData(XhtmlSection section, int placeFlag)
        {
            string text = string.Empty;
            switch (placeFlag)
            {
                case 0://0表示在Html文件头部包含头部与正文相关信息
                    text = "<? include($_GET['content_id'].'_h.inc') ?>";
                    break;
                case 1://1表示包含Content正文的页面片文件
                    text = "<? include($_GET['content_id'].'_c.inc') ?>";
                    break;
                default:
                    Debug.Fail("仅支持数字0和1，\r\n0表示在Html文件头部包含头部与正文相关信息；\r\n1表示包含Content正文的页面片文件");
                    break;
            }
            this.InstanceSubMethod(section, text);
        }
        public class PHPInclude : XhtmlCData
        {
            internal PHPInclude(XhtmlSection section, int placeFlag)
                : base(section, placeFlag) { }
        }
        #endregion
    }
}

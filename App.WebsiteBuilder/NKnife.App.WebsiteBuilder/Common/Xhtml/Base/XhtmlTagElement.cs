using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu
{
    public class XhtmlTagElement : XhtmlElement
    {
        /// <summary>
        /// 将指定的文本添加到该节点的子节点列表的末尾。
        /// </summary>
        /// <param name="text">指定的文本</param>
        public void AppendText(string text)
        {
            XmlText textNode = this.AsXmlNode.OwnerDocument.CreateTextNode(text);
            this.AsXmlNode.AppendChild(textNode);
        }

        /// <summary>
        /// 节点的ID属性值
        /// </summary>
        public string Id 
        {
            get { return this.Attributes["id"].Value; }
            set { SetValue("id", value); }
        }

        /// <summary>
        /// 节点的Class属性值
        /// </summary>
        public string Class
        {
            get { return this.Attributes["class"].Value; }
            set { SetValue("class", value); }
        }

        /// <summary>
        /// 节点的Sytle属性值
        /// </summary>
        public string Sytle
        {
            get { return this.Attributes["sytle"].Value; }
            set { SetValue("sytle", value); }
        }

        /// <summary>
        /// 内部函数：几个属性的Set的提取方法。
        /// 功能：Set属性值。
        /// </summary>
        /// <param name="key">属性名</param>
        /// <param name="value">属性值</param>
        private void SetValue(string key, string value)
        {
            XhtmlAttribute newItem = new XhtmlAttribute(key, value);
            XhtmlAttribute oldItem;
            if (this.Attributes.Contains(key, out oldItem))
            {
                this.Attributes.Replace(newItem, oldItem);
            }
            else
            {
                this.Attributes.Add(newItem);
            }
        }

        private XhtmlSection _xhtmlSection = null;
        public override XhtmlSection OwnerPage
        {
            get { return _xhtmlSection; }
            protected set { _xhtmlSection = value; }
        }

    }
}

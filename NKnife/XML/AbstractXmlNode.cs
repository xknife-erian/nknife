using System.Xml;

namespace NKnife.XML
{
    /// <summary>
    /// 对.net的Xml常使用的XmlNode, XmlDocument, XmlElement类的基类的封装
    /// </summary>
    public abstract class AbstractBaseXmlNode
    {
        /// <summary>
        /// 内部的XmlNode（组合）
        /// </summary>
        internal protected XmlNode _BaseXmlNode;

        public virtual void AppendChild(AbstractBaseXmlNode baseNode)
        {
            this._BaseXmlNode.AppendChild(baseNode._BaseXmlNode);
        }

        public virtual void RemoveChild(AbstractBaseXmlNode baseNode)
        {
            this._BaseXmlNode.RemoveChild(baseNode._BaseXmlNode);
        }

        public virtual string GetAttribute(string name)
        {
            return (this._BaseXmlNode as XmlElement).GetAttribute(name);
        }

        public virtual void SetAttribute(string name, string value)
        {
            (this._BaseXmlNode as XmlElement).SetAttribute(name, value);
        }
    }
}

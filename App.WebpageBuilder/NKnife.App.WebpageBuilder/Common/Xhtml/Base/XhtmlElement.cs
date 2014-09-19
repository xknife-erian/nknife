using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Jeelu
{
    /// <summary>
    /// Jeelu.Xhtml组件的最基础类型。核心类。
    /// 类似于.net的Xml命名空间中的XmlNode
    /// </summary>
    public abstract class XhtmlElement
    {
        /// <summary>
        /// 移除了前缀的当前节点的名称。
        /// 例如，对于元素 [bk:book]，LocalName 是 book。
        /// 如果该节点没有前缀，则 LocalName 与 Name 相同。
        /// </summary>
        public string LocalName { get { return this.AsXmlNode.LocalName; } }

        /// <summary>
        /// 模仿XmlNode，在InitializeComponent方法中初始化,他也可能会是一个XmlDocument节点
        /// </summary>
        internal virtual XmlNode AsXmlNode { get; set; }

        /// <summary>
        /// 初始化本类型，对外不公开
        /// </summary>
        /// <param name="page">类似XmlDocument，对本组件是XhtmlPage</param>
        /// <param name="localName">当前节点的本地名</param>
        internal virtual void InitializeComponent(XhtmlSection section, string localName)
        {
            this.AsXmlNode = ((XmlDocument)section.AsXmlNode).CreateElement(localName);
            this.Attributes = new XhtmlAttributeCollection(this);
            this.OwnerPage = section;
        }
        /// <summary>
        /// 初始化本类型，对外不公开
        /// </summary>
        /// <param name="page">类似XmlDocument，对本组件是XhtmlPage</param>
        /// <param name="localName">当前节点的本地名</param>
        /// <param name="attributes">当前节点的属性集合</param>
        internal virtual void InitializeComponent(XhtmlSection section, string localName, params XhtmlAttribute[] attributes)
        {
            this.InitializeComponent(section, localName);
            foreach (XhtmlAttribute item in attributes)
            {
                this.SetAttribute(item);
            }
        }
       
        #region 一些类似XmlElement的常用属性与方法

        abstract public XhtmlSection OwnerPage { get; protected set; }

        /// <summary>
        /// 获取是否是一个XmlElement
        /// </summary>
        public virtual bool IsXmlElement { get { return true; } }
        /// <summary>
        /// 父节点
        /// </summary>
        public XhtmlElement ParentElement { get; protected set; }
        /// <summary>
        /// 子节点集合，类似XmlNode的ChildNodes
        /// </summary>
        public XhtmlTCollection<XhtmlElement> ChildElements 
        {
            get { return _ChildElements; }
            protected set { this._ChildElements = value; } 
        }
        private XhtmlTCollection<XhtmlElement> _ChildElements = new XhtmlTCollection<XhtmlElement>();
      
        /// <summary>
        /// 获取这个节点是否有属性
        /// </summary>
        public virtual bool HasAttributes 
        { 
            get 
            {
                if (this.AsXmlNode.Attributes == null || this.AsXmlNode.Attributes.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            } 
        }
        /// <summary>
        /// 获取这个节点是否有子节点集合
        /// </summary>
        public virtual bool HasChildElements { get { return this.AsXmlNode.HasChildNodes; } }
        /// <summary>
        /// 获取类似XmlDocument的属性集合
        /// </summary>
        public virtual XhtmlAttributeCollection Attributes { get; protected set; }
        /// <summary>
        /// 获取含本节点的Xml字符串
        /// </summary>
        public virtual string OuterXml { get { return this.ClearCDataTempMethod(this.AsXmlNode.OuterXml); } }
        /// <summary>
        /// 获取与设置节点的内的Xml字符串
        /// </summary>
        public virtual string InnerXml
        {
            get { return this.ClearCDataTempMethod(this.AsXmlNode.InnerXml); }
            set { this.AsXmlNode.InnerXml = value; }
        }
        /// <summary>
        /// 获取与设置节点的文本串值
        /// </summary>
        public virtual string InnerText
        {
            get { return this.ClearCDataTempMethod(this.AsXmlNode.InnerText); }
            set { this.AsXmlNode.InnerText = value; }
        }

        /// <summary>
        /// 指示本节点是否是以 "[name /]" 这种形式关闭，一般在解析节点使用.
        /// </summary>
        internal bool IsTerminated
        {
            get
            {
                if (this.HasChildElements)
                {
                    return false;
                }
                else
                {
                    return _IsTerminated | _IsExplicitlyTerminated;
                }
            }
            set { _IsTerminated = value; }
        }
        private bool _IsTerminated;

        /// <summary>
        /// 指示本节点是否是以 "[/name]" 这种形式关闭，一般在解析节点使用.
        /// </summary>
        internal bool IsExplicitlyTerminated
        {
            get { return _IsExplicitlyTerminated; }
            set { _IsExplicitlyTerminated = value; }
        }
        private bool _IsExplicitlyTerminated;

        /// <summary>
        /// 供XhtmlParser使用的内部调用属性
        /// </summary>
        internal bool NoEscaping
        {
            get { return "script".Equals(this.LocalName.ToLower()) || "style".Equals(this.LocalName.ToLower()); }
        }

        /// <summary>
        /// 将指定的节点添加到该节点的子节点列表的末尾。
        /// </summary>
        /// <param name="element">指定的节点</param>
        public virtual void AppendChild(XhtmlElement element)
        {
            if (element == null)
            {
                return;
            }
            if (this.AsXmlNode is XmlDocument)
            {
                XmlDocument doc = (XmlDocument)this.AsXmlNode;
                if (doc.DocumentElement == null)
                {
                    doc.AppendChild(element.AsXmlNode);
                }
                else
                {
                    doc.DocumentElement.AppendChild(element.AsXmlNode);
                }
            }
            else if (this.AsXmlNode is XmlElement)
            {
                this.AsXmlNode.AppendChild(element.AsXmlNode);
            }
            else
            {
                return;
            }
            this._ChildElements.Add(element);
        }
        /// <summary>
        /// 用 newElement 节点替换子节点 oldElement。
        /// </summary>
        /// <param name="newElement">新的节点</param>
        /// <param name="oldElement">指定的旧节点</param>
        public virtual void ReplaceChild(XhtmlElement newElement, XhtmlElement oldElement)
        {
            this.AsXmlNode.ReplaceChild(newElement.AsXmlNode, oldElement.AsXmlNode);
            this._ChildElements.Remove(oldElement);
            this._ChildElements.Add(newElement);
        }
        /// <summary>
        /// 将指定的节点紧接着插入指定的引用节点之后。
        /// </summary>
        /// <param name="newChild">指定的新节点</param>
        /// <param name="refChild">指定的引用节点</param>
        public virtual void InsertAfter(XhtmlElement newChild, XhtmlElement refChild)
        {
            this.AsXmlNode.InsertAfter(newChild.AsXmlNode, refChild.AsXmlNode);
            int i = this._ChildElements.IndexOf(refChild);
            this._ChildElements.Insert(i, newChild);
        }
        /// <summary>
        /// 将指定的节点紧接着插入指定的引用节点之前。
        /// </summary>
        /// <param name="newChild">指定的新节点</param>
        /// <param name="refChild">指定的引用节点</param>
        public virtual void InsertBefore(XhtmlElement newChild, XhtmlElement refChild)
        {
            this.AsXmlNode.InsertBefore(newChild.AsXmlNode, refChild.AsXmlNode);
            int i = this._ChildElements.IndexOf(refChild);
            if (i < 0)
            {
                Debug.Fail(" i < 0 ");
                this._ChildElements.Insert(0, newChild);
            }
            else
            {
                this._ChildElements.Insert(i - 1, newChild);
            }
        }
        /// <summary>
        /// 设置具有指定名称的属性的值。
        /// </summary>
        /// <param name="key">要创建或更改的属性的名称。这是限定名。如果该名称包含一个冒号，则将其解析为前缀和本地名称两个部分。</param>
        /// <param name="value">要为此属性设置的值。</param>
        public virtual void SetAttribute(string key, string value)
        {
            /// 如果是以下三种类型，不能写入属性
            if (this is XhtmlText || this is XhtmlComment || this is XhtmlCData)
            {
                Debug.Fail(this.GetType().Name + " cannot SetAttribute!");
                return;
            }
            XhtmlAttribute newXhtmlatt = new XhtmlAttribute(key, value);
            XmlAttribute attributeNode = this.GetAttributeNode(key);
            if (attributeNode == null)
            {
                XmlDocument doc = null;
                if (this.AsXmlNode is XmlDocument)
                {
                    doc = (XmlDocument)this.AsXmlNode;
                }
                else
                {
                    doc = this.AsXmlNode.OwnerDocument;
                }
                attributeNode = doc.CreateAttribute(key);
                attributeNode.Value = value;
                if (this.AsXmlNode is XmlDocument)
                {
                    doc.DocumentElement.Attributes.Append(attributeNode);
                }
                else
                {
                    this.AsXmlNode.Attributes.Append(attributeNode);
                }
                //this.Attributes.Add(newXhtmlatt);
            }
            else
            {
                attributeNode.Value = value;
                this.Attributes.Replace(newXhtmlatt, this.Attributes[key]);
            }
        }
        private XmlAttribute GetAttributeNode(string name)
        {
            if (this.HasAttributes)
            {
                return this.AsXmlNode.Attributes[name];
            }
            return null;
        }

        /// <summary>
        /// 添加指定的Jeelu.XhtmlAttribute。如果该项已存在，则替换其值；如果不存在，则增加该项。
        /// </summary>
        public virtual void SetAttribute(XhtmlAttribute attribute)
        {
            this.SetAttribute(attribute.Key, attribute.Value);
        }
        /// <summary>
        /// 返回具有指定名称的属性的值。
        /// </summary>
        /// <param name="key">要检索的属性的名称。这是限定名。它针对匹配节点的 Name 属性进行匹配。</param>
        /// <returns>指定属性的值。如果未找到匹配属性，或者如果此属性没有指定值或默认值，则返回空字符串。</returns>
        public virtual string GetAttribute(string key)
        {
            XmlAttribute attributeNode = this.GetAttributeNode(key);
            if (attributeNode != null)
            {
                return attributeNode.Value;
            }
            return string.Empty;
        }
        /// <summary>
        /// 根据一个本地名通过XPath检索子节点中匹配的子节点列表，对外不公开
        /// </summary>
        /// <param name="localname">一个本地名</param>
        /// <returns>XmlNodeList</returns>
        protected virtual XmlNodeList GetElementsByName(string localname)
        {
            return this.AsXmlNode.SelectNodes(localname);
        }
        /// <summary>
        /// 移除当前节点的所有指定属性和子级。不移除默认属性。
        /// </summary>
        public virtual void RemoveAll()
        {
            this.AsXmlNode.RemoveAll();
            this.Attributes.Clear();
            this._ChildElements.Clear();
        }
        /// <summary>
        /// 移除指定的子节点。
        /// </summary>
        /// <param name="element">正在被移除的节点。</param>
        public virtual void RemoveChild(XhtmlElement element)
        {
            this.AsXmlNode.RemoveChild(element.AsXmlNode);
            this._ChildElements.Remove(element);
        }
        /// <summary>
        /// 从元素移除所有指定的属性。不移除默认属性。
        /// </summary>
        public virtual void RemoveAllAttributes()
        {
            while (this.HasAttributes)
            {
                this.Attributes.Remove(this.Attributes[0]);
            }
            this.Attributes.Clear();
        }
        /// <summary>
        /// 从元素移除所有节点本地名集合指定的属性。
        /// </summary>
        /// <param name="names">节点本地名集合</param>
        public virtual void RemoveAttribute(params string[] names)
        {
            try
            {
                foreach (string name in names)
                {
                    ((XmlElement)this.AsXmlNode).RemoveAttribute(name);
                    this.Attributes.Remove(this.Attributes[name]);
                }
            }
            catch (XmlException e)
            {
                Debug.Fail(e.Message);
            }
        }

        #endregion

        /// <summary>
        /// 已重写。Jeelu.com.
        /// 能够返回本类型所对应的真实的Html Code.
        /// </summary>
        public override string ToString()
        {
            return this.ClearCDataTempMethod(this.OuterXml);
        }

        /// <summary>
        /// 返回一个上下文无关的XmlNode
        /// </summary>
        public XmlNode ToXmlNode()
        {
            return this.AsXmlNode;
        }

        /// <summary>
        /// 返回一个上下文相关的XmlNode
        /// </summary>
        /// <param name="doc">上下文XmlDocument</param>
        public XmlNode ToXmlNode(XmlDocument doc)
        {
            if (this.AsXmlNode is XmlDocument)
            {
                return doc.ImportNode(((XmlDocument)this.AsXmlNode).DocumentElement, true);
            }
            return doc.ImportNode(this.AsXmlNode, true);
        }

        /// <summary>
        /// 设置本节点的父节点
        /// </summary>
        /// <param name="parentElement">父节点</param>
        internal void SetParent(XhtmlElement parentElement)
        {
            this.ParentElement = parentElement;
        }

        #region 导入的HtmlCode的临时处理方法

        /// <summary>
        /// 导入的HtmlCode的临时处理方法
        /// </summary>
        private string ClearCDataTempMethod(string str)
        {
            string tempTag = @"\!\#\@\#\!";
            string begin = @"\<\!\[CDATA\[" + tempTag;
            string end = tempTag + @"\]\]\>";

            Regex regex = new Regex(begin + "(?<content>.*)" + end, RegexOptions.Singleline);
            string outstr = regex.Replace(str, ReplaceCallback);
            return outstr;
        }
        private string ReplaceCallback(Match m)
        {
            return m.Groups["content"].Value;
        }

        #endregion
    }
}

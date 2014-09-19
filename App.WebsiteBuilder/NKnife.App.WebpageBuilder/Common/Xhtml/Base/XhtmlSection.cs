using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml;

namespace Jeelu
{
    /// <summary>
    /// Jeelu.Xhtml组件的主要类型。
    /// XhtmlSection与XmlDoucment极为相似，在内部实现上也是运用XmlDoucment进行的转换。
    /// XhtmlPage与XhtmlSection的区别在于：
    /// Page一定关联一个文件，根据指定的Xhtml版本有相应的DocType节点，并且有对应Head，Body两个Xhtml文件重要的节点集合
    /// Section并不一定并联文件，且不推荐其关联文件，他将是部份Xml片断，当然也可能是整个Xml文件内容本身。
    /// </summary>
    public partial class XhtmlSection : XhtmlElement
    {
        /// <summary>
        /// 构造函数，生成一个XhtmlSection实例，根节点名未命名(以Unname为临时名)
        /// </summary>
        public XhtmlSection()
        {
            this.InitializeComponent("Unname");
        }
        /// <summary>
        /// 构造函数，生成一个XhtmlSection实例
        /// </summary>
        /// <param name="localname">Section节点的本地名</param>
        public XhtmlSection(string localname)
        {
            this.InitializeComponent(localname);
        }

        /// <summary>
        /// 初始化本类型，对外不公开
        /// </summary>
        /// <param name="localName">节点本地名</param>
        protected virtual void InitializeComponent(string localName)
        {
            XhtmlTagNameTabel tagNameTabel = XhtmlTagNameTabel.SingularCreator();
            this.AsXmlNode = new XmlDocument(tagNameTabel);

            string name = localName.ToLower(CultureInfo.CurrentCulture);
            this.DocumentXhtmlElement = this.OwnerPage.CreateXhtmlTag(name);
            this.AppendChild(this.DocumentXhtmlElement);
            this.Attributes = new XhtmlAttributeCollection(this);
            
            //XmlElement documentElement = ((XmlDocument)this.AsXmlNode).CreateElement(localName.ToLower(CultureInfo.CurrentCulture));
            //((XmlDocument)this.AsXmlNode).AppendChild(documentElement);
        }

        /// <summary>
        /// XHtml的相似XmlDocument，对外不公开
        /// </summary>
        protected virtual XmlDocument InnerXmlDocument
        {
            get { return (XmlDocument)this.AsXmlNode; }
        }
        /// <summary>
        /// 获取文档的根 System.Xml.XmlElement，对外不公开
        /// </summary>
        protected virtual XmlElement DocumentElement
        {
            get { return this.InnerXmlDocument.DocumentElement; }
        }
        /// <summary>
        /// 获取文档的根
        /// </summary>
        public virtual XhtmlElement DocumentXhtmlElement { get; protected set; }
        
        /// <summary>
        /// 将一个片断(Jeelu.XhtmlSection)导入到引用节点(深度克隆)。
        /// 功能实现重点：是将一个文档的片断导入到另一个文档
        /// </summary>
        /// <param name="section">即将被导入的片断</param>
        /// <param name="refElement">引用的节点，片断将被导入这个节点的子节点列表的末尾</param>
        /// <returns>导入成功与否。true成功，false失败</returns>
        public virtual bool ImportSection(XhtmlSection section, XhtmlElement refElement)
        {
            if (section == null || refElement == null)
            {
                return false;
            }
            XmlNode newNode = ((XmlDocument)this.AsXmlNode).ImportNode(((XmlDocument)section.AsXmlNode).DocumentElement, true);
            if (refElement.AsXmlNode is XmlDocument)
            {
                XmlDocument doc = (XmlDocument)refElement.AsXmlNode;
                doc.DocumentElement.AppendChild(newNode);
                return true;
            }
            else
            {
                refElement.AsXmlNode.AppendChild(newNode);
                return true;
            }
        }

        public override XhtmlSection OwnerPage
        {
            get
            {
                return this;
            }
            protected set
            {
                Debug.Fail("Don't Set!");
            }
        }
    }
}

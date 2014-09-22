using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace Jeelu
{
    /// <summary>
    /// Jeelu.Xhtml组件的主要类型。
    /// XhtmlPage与XmlDoucment极为相似，在内部实现上也是运用XmlDoucment进行的转换。
    /// XhtmlPage与XhtmlSection的区别在于：
    /// Page一定关联一个文件，根据指定的Xhtml版本有相应的DocType节点，并且有对应Head，Body两个Xhtml文件重要的节点集合
    /// Section并不一定并联文件，且不推荐其关联文件，他将是部份Xml片断，当然也可能是整个Xml文件本身。
    /// </summary>
    public partial class XhtmlPage : XhtmlSection, ICloneable, IXPathNavigable
    {
        /// <summary>
        /// 构造函数，生成一个XhtmlPage实例
        /// </summary>
        /// <param name="version">枚举：Xhtml的版本</param>
        public XhtmlPage(Xhtml.Version version)
            : base("html")
        {
            this.InitializeComponent(version);
        }
        /// <summary>
        /// 初始化本类型
        /// </summary>
        /// <param name="version">枚举：Xhtml的版本</param>
        protected void InitializeComponent(Xhtml.Version version)
        {
            this.XhtmlVersion = version;

            XhtmlTags.Head eleHead = this.CreateXhtmlHead();
            this.Head = eleHead;
            XhtmlTags.Title eleTitle = this.CreateXhtmlTitle();
            this.Title = eleTitle;
            //this.Head.AppendChild(eleTitle);
            XhtmlTags.Body eleBody = this.CreateXhtmlBody();
            this.Body = eleBody;

            this.AppendChild(eleHead);
            this.AppendChild(eleBody);
        }
        /// <summary>
        /// XHtml的文件信息
        /// </summary>
        public virtual FileInfo FileInfo { get; set; }
        /// <summary>
        /// XHtml的版本
        /// </summary>
        private Xhtml.Version _xhtmlVersion = Xhtml.Version.None;
        /// <summary>
        /// XHtml的版本(1.0严格与兼容两种版本,1.1版,共3种,在Jeelu项目推荐使用1.1版)
        /// </summary>
        public virtual Xhtml.Version XhtmlVersion
        {
            get { return this._xhtmlVersion; }
            set
            {
                this._xhtmlVersion = value;
                XmlDocumentType doctype = null;
                switch (value)
                {
                    #region case
                    case Xhtml.Version.Xhtml10Strict:
                        {
                            doctype = this.InnerXmlDocument.CreateDocumentType(
                                XhtmlDocType.Xhtml10StrictName,
                                XhtmlDocType.Xhtml10StrictPublicId,
                                null, //XhtmlDocType.xhtml10_strict_systemId,
                                null);
                            break;
                        }
                    case Xhtml.Version.Xhtml10Transitional:
                        {
                            doctype = this.InnerXmlDocument.CreateDocumentType(
                               XhtmlDocType.Xhtml10TransitionalName,
                               XhtmlDocType.Xhtml10TransitionalPublicId,
                               null, //XhtmlDocType.xhtml10_transitional_systemId,
                               null);
                            break;
                        }
                    case Xhtml.Version.Xhtml11:
                        {
                            doctype = this.InnerXmlDocument.CreateDocumentType(
                            XhtmlDocType.Xhtml11Name,
                            XhtmlDocType.Xhtml11PublicId,
                            null, //XhtmlDocType.xhtml11_systemId,
                            null);
                            break;
                        }
                    case Xhtml.Version.None:
                    default:
                        {
                            Debug.Fail("Please to input value for Xhtmlversion in first time!");
                            break;
                        }
                    #endregion
                }
                /// 将XmlDocumentType节点按照规则放在根的所有节点的第一位
                this.AsXmlNode.PrependChild(doctype);
            }//set
        }
        /// <summary>
        /// Xhtml文件头部信息
        /// </summary>
        public virtual XhtmlTags.Head Head { get; set; }
        /// <summary>
        /// Xhtml文件主体信息
        /// </summary>
        public virtual XhtmlTags.Body Body { get; set; }
        /// <summary>
        /// Xhtml文件标题
        /// </summary>
        public virtual XhtmlTags.Title Title
        { get; set; }

        #region Load and Save

        /// <summary>
        /// 从XHtml的源码载入XHtml页面。
        /// </summary>
        /// <param name="htmlCode">XHtml的源码</param>
        public virtual void Load(string htmlCode)
        {
            this.InnerXmlDocument.LoadXml(htmlCode);
        }

        /// <summary>
        /// 从文件载入XHtml页面。
        /// </summary>
        /// <param name="file">文件</param>
        public virtual void Load(FileInfo file)
        {
            this.FileInfo = file;
            this.InnerXmlDocument.Load(file.FullName);
        }

        /// <summary>
        /// 保存页面
        /// </summary>
        public virtual bool Save()
        {
            return this.Save(true);
        }
        /// <summary>
        /// 保存页面
        /// </summary>
        /// <param name="isJeelu">是否在文件尾插入面向搜索引擎友好的Jeelu的代码，true插入，false不插入</param>
        public virtual bool Save(bool isJeelu)
        {
            return this.Save(isJeelu, this.FileInfo.FullName);
        }
        /// <summary>
        /// 保存页面
        /// </summary>
        /// <param name="isJeelu">是否在文件尾插入面向搜索引擎友好的Jeelu的代码，true插入，false不插入</param>
        /// <param name="filePath">文件的路径</param>
        public virtual bool Save(bool isJeelu, string filePath)
        {
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                this.FileInfo = new FileInfo(filePath);
                FileAttributes fileAtts = FileAttributes.Normal;
                ///先获取此文件的属性
                fileAtts = File.GetAttributes(filePath);
                ///将文件属性设置为普通（即没有只读和隐藏等）
                File.SetAttributes(filePath, FileAttributes.Normal);
                ///在文件属性为普通的情况下保存。（不然有可能会“访问被拒绝”）

                StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8);
                sw.Write(this.ToString(isJeelu));
                sw.Close();
                sw.Dispose();

                ///恢复文件属性
                File.SetAttributes(filePath, fileAtts);
                return true;
            }
            Debug.Fail(filePath + "  >>> is Fail!");
            return false;
        }

        #endregion

        /// <summary>
        /// 根据W3C的标准校验本XHtml源码。
        /// TODO: 暂时未实现，对标准及实现方法没有思考清楚。
        /// </summary>
        protected virtual bool Verify()
        {
            if (this.InnerXmlDocument != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 返回XHtml页面的源代码。默认插入Jeelu的搜索引擎优化Code。
        /// </summary>
        public override string ToString()
        {
            return this.ToString(true);
        }
        /// <summary>
        /// 返回XHtml页面的源代码。
        /// </summary>
        /// <param name="isJeelu">Bool,是否在文件尾插入面向搜索引擎友好的Jeelu的代码</param>
        public string ToString(bool isJeelu)
        {
            if (!this.Verify())
            {
                return null;
            }
            string codeString = this.SpecialDisposalForCode(isJeelu);
            return codeString;
        }

        /// <summary>
        /// 在ToString()时调用的方法，该方法对该页面做一些无法使用XML特性的SimplusD特殊处理。
        /// 其实都是没有办法的办法。馊招。但效率也不会差，经测试考虑得也还完善。
        /// edit by lukan, 2008-6-29 02:40:13
        /// 1.在DocType中写入SystemId
        /// 2.在html节点插入xmlns
        /// 3.在文件尾插入面向搜索引擎友好的Jeelu的代码
        /// </summary>
        protected virtual string SpecialDisposalForCode(bool isJeelu)
        {

            {//3.在文件尾插入面向搜索引擎友好的Jeelu的代码
                string jeeluFooter = "http://ssi.jeelu.com/inc/jeelufooter.inc";
                XhtmlComment.ShtmlInclude jeeluInclude = this.CreateXhtmlCommentShtml(jeeluFooter);
                this.Body.AppendChild(jeeluInclude);
            }

            string xhtmlCode = this.AsXmlNode.OuterXml;

            if (isJeelu)
            {//1.在DocType中写入SystemId
                string v = "//EN\" \"\">";
                int i = xhtmlCode.IndexOf(v, StringComparison.CurrentCulture);
                string systemId = string.Empty;
                #region switch (this.XhtmlVersion)
                switch (this.XhtmlVersion)
                {
                    case Xhtml.Version.Xhtml10Strict:
                        systemId = XhtmlDocType.Xhtml10StrictSystemId;
                        break;
                    case Xhtml.Version.Xhtml11:
                        systemId = XhtmlDocType.Xhtml11SystemId;
                        break;
                    case Xhtml.Version.Xhtml10Transitional:
                        systemId = XhtmlDocType.Xhtml10TransitionalSystemId;
                        break;
                    case Xhtml.Version.None:
                    default:
                        Debug.Fail("Please to input value for Xhtmlversion in first time!");
                        break;
                }
                #endregion
                xhtmlCode = xhtmlCode.Insert(i + 7, systemId);
            }

            {//2.在html节点插入xmlns
                string tag = "<html>";
                int i = xhtmlCode.IndexOf(tag, StringComparison.CurrentCulture);
                string str = "<html xmlns=\"http://www.w3.org/1999/xhtml\">";
                xhtmlCode = xhtmlCode.Remove(i, tag.Length);
                xhtmlCode = xhtmlCode.Insert(i, str);
            }


            return xhtmlCode;
        }

        /// <summary>
        /// 将此 XmlNode 下子树完全深度中的所有 XmlText 节点都转换成“正常”形式，
        /// 在这种形式中只有标记（即标记、注释、处理指令、CDATA节和实体引用）
        /// 分隔 XmlText 节点，也就是说，没有相邻的 XmlText 节点。
        /// </summary>
        public void Normalize()
        {
            //this.InnerXmlDocument.Normalize();
        }

        #region ICloneable 成员

        /// <summary>
        /// 创建此节点的一个副本。实现ICloneable接口的方法。
        /// </summary>
        /// <returns>克隆的节点。PageAsXmlNode的副本。</returns>
        public object Clone()
        {
            return this.InnerXmlDocument.Clone();
        }

        #endregion

        #region IXPathNavigable 成员

        public XPathNavigator CreateNavigator()
        {
            return this.CreateNavigator(this.InnerXmlDocument);
        }

        public virtual XPathNavigator CreateNavigator(IXPathNavigable node)
        {
            return node.CreateNavigator();
        }

        #endregion

        /// <summary>
        /// Xhtml的文档类型声明
        /// </summary>
        public static class XhtmlDocType
        {
            public static readonly string Xhtml10TransitionalName = "html";
            public static readonly string Xhtml10StrictName = "html";
            public static readonly string Xhtml11Name = "html";

            public static readonly string Xhtml10TransitionalPublicId = "-//W3C//DTD Xhtml 1.0 Transitional//EN";
            public static readonly string Xhtml10StrictPublicId = "-//W3C//DTD Xhtml 1.0 Strict//EN";
            public static readonly string Xhtml11PublicId = "-//W3C//DTD Xhtml 1.1//EN";

            public static readonly string Xhtml10TransitionalSystemId = "http://www.w3.org/TR/Xhtml1/DTD/Xhtml1-transitional.dtd";
            public static readonly string Xhtml10StrictSystemId = "http://www.w3.org/TR/Xhtml1/DTD/Xhtml1-strict.dtd";
            public static readonly string Xhtml11SystemId = "http://www.w3.org/TR/Xhtml11/DTD/Xhtml11.dtd";
        }
    }
}

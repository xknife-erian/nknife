using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using System.IO;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 继承自IndexXmlElement的类，
    /// 这个类实现了IToHtml接口，也就是说继承该类型的XmlElement均能够根据节点的一些属性生成Xhtml代码
    /// 一般情况在继承类里重写TagCreator方法即可
    /// Design by lukan, 2008-6-23 00:28:04
    /// </summary>
    public abstract class ToHtmlXmlElement : IndexXmlElement
    {
        protected ToHtmlXmlElement(string localName, XmlDocument doc)
            : base(localName, doc)
        {
        }

        #region IToHtml 成员

        /// <summary>
        /// 是否已经生成过XHtml文件
        /// </summary>
        protected bool _isAlreadyToHtml = false;

        internal PageXmlDocument PageXmlDocument { get; set; }

        /// <summary>
        /// 页面片生成的文件，包含绝对路径，和文件名。
        /// 文件名的组成：主体是页面片的ID，后缀名“.inc”。
        /// 目录间隔采用的是“\”
        /// </summary>
        public string HtmlFile
        {
            get
            {
                TmpltXmlDocument doc = (TmpltXmlDocument)this.OwnerAnyDocument;
                if (string.IsNullOrEmpty(this._HtmlFile))
                {
                    _HtmlFile = Path.Combine(doc.TmpltToHtmlPath, this.Id + Utility.Const.IncludeFileExt);
                }
                return _HtmlFile;
            }
        }
        private string _HtmlFile;

        /// <summary>
        /// 获取当前节点生成的Xhtml代码字符串。
        /// </summary>
        public virtual string ToHtml()
        {
            if (!this._isAlreadyToHtml)
            {
                this.MarkXhtmlElement(this.PageXmlDocument);
            }
            return this._XhtmlElement.ToString();
        }

        long i = 0;
        /// <summary>
        /// 获取当前节点生成的供预览使用的Xhtml代码字符串
        /// </summary>
        public virtual string ToHtmlPreview()
        {
            i++;
            return "<h3><b>Call ToHtmlPreview(): " + i.ToString() + " !</b></h3>";// +this.ToHtml();
        }

        private ToHtmlHelper ToHtmlHelper { get; set; }

        /// <summary>
        /// 保存当前节点生成的Xhtml代码为一个文件。成功时返回一个Bool值为True。
        /// 调用时建议判断一下是否保存成功。
        /// </summary>
        public virtual bool SaveXhtml(ToHtmlHelper htmlHelper)
        {
            this.ToHtmlHelper = htmlHelper;
            TmpltXmlDocument doc = (TmpltXmlDocument)this.OwnerAnyDocument;
            string path = Path.Combine(doc.TmpltToHtmlPath, this.Id + Utility.Const.IncludeFileExt);
            return this.SaveXhtml(path);
        }

        /// <summary>
        /// 保存当前节点生成的Xhtml代码为一个文件。成功时返回一个Bool值为True。
        /// 调用时建议判断一下是否保存成功。
        /// </summary>
        /// <param name="fileFullName">正在保存的文件,應包含路徑</param>
        /// <returns>返回保存是否成功，不成功有两种情况，1文件系统导致，2该类型不支持存储成文件</returns>
        internal virtual bool SaveXhtml(string fileFullName)
        {
            return ToHtmlHelper.FileSave(fileFullName, this.ToHtml());
        }

        public virtual bool DeleteXhtml(ToHtmlHelper htmlHelper)
        {
            this.ToHtmlHelper = htmlHelper;
            File.Delete(this.HtmlFile);
            return true;
        }

        /// <summary>
        /// 该节点的Xhtml代码的父节点。
        /// </summary>
        public virtual XhtmlElement ParentXhtmlElement
        {
            get { return this._ParentXhtmlElement; }
            internal set { this._ParentXhtmlElement = value; }
        }
        protected XhtmlElement _ParentXhtmlElement;

        /// <summary>
        /// 该节点的Xhtml代码。
        /// </summary>
        public virtual XhtmlElement XhtmlElement
        {
            get { return this._XhtmlElement; }
            protected set { this._XhtmlElement = value; }
        }
        protected XhtmlElement _XhtmlElement;

        /// <summary>
        /// 设置本节点在生成Xhtml时的标签，默认是div
        /// </summary>
        protected virtual void TagCreator()
        {
            XhtmlSection ownerPage = (XhtmlSection)this._ParentXhtmlElement.OwnerPage;
            this._XhtmlElement = ownerPage.CreateXhtmlDiv();
            this._ParentXhtmlElement.AppendChild(this._XhtmlElement);
        }

        //internal virtual void MarkXhtmlElement()
        //{
        //    this.MarkXhtmlElement(null);
        //}

        /// <summary>
        /// 根据当前节点的特性来设置该节点
        /// </summary>
        internal virtual void MarkXhtmlElement(PageXmlDocument pageDoc)
        {
            this.PageXmlDocument = pageDoc;
            this.TagCreator();

            #region if DEBUG, 辩别生成效果
#if DEBUG       //为了辩别生成效果，在Debug时节点加入相关的节点名字
            //string eleName = this.LocalName;
            //PropertyInfo pinfo = this.GetType().GetProperty("SnipPartType");
            //if (pinfo != null)
            //{
            //    eleName = eleName + "_" + pinfo.GetValue(this, null);
            //}
            //if (this._XhtmlElement != null && !(this._XhtmlElement is XhtmlText) && !(this._XhtmlElement is XhtmlCData) && !(this._XhtmlElement is XhtmlComment))
            //{
            //    this._XhtmlElement.SetAttribute("name", eleName);
            //}
#endif
            #endregion

            foreach (XmlNode node in this.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                if (node is ToHtmlXmlElement)
                {
                    if (node is SnipXmlElement)
                    {
                        SnipXmlElement snip = (SnipXmlElement)node;
                        if (!snip.HasSnip)///如果页面片节点里面并不含有页面片的话，节约效率，就不用再往下遍历
                        {
                            continue;
                        }
                    }
                    ToHtmlXmlElement htmlNode = (ToHtmlXmlElement)node;
                    htmlNode.PageXmlDocument = this.PageXmlDocument;
                    htmlNode.ParentXhtmlElement = this.SetSubNode();
                    htmlNode.ToHtml();
                }
            }
            this._isAlreadyToHtml = true;
        }

        /// <summary>
        /// 设置下一级子节点的ParentXhtmlElement值
        /// </summary>
        protected virtual XhtmlElement SetSubNode()
        {
            return this._XhtmlElement;
        }

        #endregion
    }
}

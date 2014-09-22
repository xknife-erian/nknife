using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Reflection;
using System.Xml;
using System.Drawing;
using System.IO;
using Jeelu.Win;
using System.Text.RegularExpressions;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 页面
    /// </summary>
    abstract public partial class PageXmlDocument
    {

        #region ToHtml

        private ToHtmlHelper ToHtmlHelper { get; set; }


        /// <summary>
        /// 是否已经生成过MainPageXHtml文件
        /// </summary>
        protected bool _isAlreadyToMainPageHtml = false;
        /// <summary>
        /// 是否已经生成过HeadXHtml文件
        /// </summary>
        protected bool _isAlreadyToHeadHtml = false;
        /// <summary>
        /// 是否已经生成过ContentXHtml文件
        /// </summary>
        protected bool _isAlreadyToContentHtml = false;


        /// <summary>
        /// 获取当前节点生成的Xhtml代码字符串
        /// </summary>
        public virtual string ToHtml(PageSection pageSection)
        {
            switch (pageSection)
            {
                case PageSection.MainPage:
                    if (!_isAlreadyToContentHtml)
                    {
                        this.MarkXhtmlElement(pageSection);
                    }
                    return this._XhtmlElement.DocumentXhtmlElement.InnerXml;
                case PageSection.Head:
                    if (!_isAlreadyToHeadHtml)
                    {
                        this.MarkXhtmlElement(pageSection);
                    }
                    return this._HeadXhtmlElement.DocumentXhtmlElement.InnerXml;
                case PageSection.Content:
                    if (!_isAlreadyToContentHtml)
                    {
                        this.MarkXhtmlElement(pageSection);
                    }
                    return this._ContentXhtmlElement.DocumentXhtmlElement.InnerXml;
                case PageSection.None:
                default:
                    Debug.Fail(pageSection + " cannot used by \"switch\" !");
                    return "";
            }
        }

        /// <summary>
        /// 获取当前节点生成的供预览使用的Xhtml代码字符串
        /// </summary>
        public virtual string ToHtmlPreview(PageSection pageSection)
        {
            return "PageXmlDocument.ToHtmlPreview()";
        }

        /// <summary>
        /// 生成頁面的三个文件(MainPage,Head,Content)到默認目錄。成功时返回一个Bool值为True。
        /// 调用时建议判断一下是否保存成功。
        /// </summary>
        /// <returns>返回保存是否成功，不成功有两种情况，1文件系统导致，2该类型不支持存储成文件</returns>
        public virtual bool SaveXhtml(ToHtmlHelper htmlhelper)
        {
            this.ToHtmlHelper = htmlhelper;
            return
                this.SaveXhtml(htmlhelper, PageSection.MainPage) &
                this.SaveXhtml(htmlhelper, PageSection.Head) &
                this.SaveXhtml(htmlhelper, PageSection.Content);
        }

        /// <summary>
        /// 生成頁面的单个子文件到默認目錄。成功时返回一个Bool值为True。
        /// 调用时建议判断一下是否保存成功。
        /// </summary>
        /// <param name="section">正在生成的子文件类型，枚举</param>
        /// <returns>返回保存是否成功，不成功有两种情况，1文件系统导致，2该类型不支持存储成文件</returns>
        public virtual bool SaveXhtml(ToHtmlHelper htmlhelper, PageSection section)
        {
            switch (section)
            {
                case PageSection.MainPage:
                    return ToHtmlHelper.FileSave(this.HtmlFile, this.ToHtml(section));
                case PageSection.Head:
                    return ToHtmlHelper.FileSave(this.HtmlHeadFile, this.ToHtml(section));
                case PageSection.Content:
                    return ToHtmlHelper.FileSave(this.HtmlContentFile, this.ToHtml(section));
                case PageSection.None:
                default:
                    Debug.Fail("PageSection is Error!");
                    return ToHtmlHelper.FileSave(this.HtmlFile, this.ToHtml(section));
            }
        }

        /// <summary>
        /// 删除页面的所有生成的子文件(MainPage,Head,Content)
        /// </summary>
        /// <param name="pageId">将要生成的页面的Id</param>
        public virtual bool DeleteXhtml(ToHtmlHelper htmlhelper)
        {
            this.ToHtmlHelper = htmlhelper;
            try
            {
                File.Delete(this.HtmlFile);
                File.Delete(this.HtmlContentFile);
                File.Delete(this.HtmlHeadFile);
                return true;
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 删除移动前服务器端生成的Xhtml文件
        /// </summary>
        /// <param name="htmlhelper"></param>
        /// <returns></returns>
        public virtual bool DeleteOldXhtml(ToHtmlHelper htmlhelper)
        {
            return true;
        }


        /// <summary>
        /// 页面对应生成的主Html文件
        /// </summary>
        public string HtmlFile
        {
            get
            {
                string path = Path.Combine(this.ToHtmlHelper.BuildPath, this.GetChannelPath(PageSection.MainPage));
                return path;
            }
        }

        /// <summary>
        /// 页面对应生成的主Html文件
        /// </summary>
        public string HtmlHeadFile
        {
            get
            {
                string path = Path.Combine(this.ToHtmlHelper.BuildPath, this.GetChannelPath(PageSection.Head));
                return path;
            }
        }

        /// <summary>
        /// 页面对应生成的主Html文件
        /// </summary>
        public string HtmlContentFile
        {
            get
            {
                string path = Path.Combine(this.ToHtmlHelper.BuildPath, this.GetChannelPath(PageSection.Content));
                return path;
            }
        }



        /// <summary>
        /// 获取该文章的Channel包含路径(供页面存储用)
        /// </summary>
        /// <returns>Channel的路径，绝对路径</returns>
        private string GetChannelPath(PageSection pageSection)
        {
            SimpleExIndexXmlElement ele = (SimpleExIndexXmlElement)this.SdsiteElement;
            switch (pageSection)
            {
                case PageSection.MainPage:
                    {
                        return ele.RelativeUrl.Remove(0, 1).Replace('/', '\\');
                    }
                case PageSection.Head:
                    {
                        string str = ele.RelativeUrl.Remove(0, 1).Replace('/', '\\');
                        str = str.Insert(str.LastIndexOf('.'), "_head");
                        return str;
                    }
                case PageSection.Content:
                    {
                        string str = ele.RelativeUrl.Remove(0, 1).Replace('/', '\\');
                        str = str.Insert(str.LastIndexOf('.'), "_content");
                        return str;
                    }
                case PageSection.None:
                default:
                    {
                        Debug.Fail("PageSection is Error!");
                        return ele.RelativeUrl.Remove(0, 1).Replace('/', '\\');
                    }
            }
        }

        /// <summary>
        /// 该节点的主体Xhtml代码。
        /// </summary>
        public virtual XhtmlSection XhtmlElement
        {
            get { return this._XhtmlElement; }
            protected set { this._XhtmlElement = value; }
        }
        private XhtmlSection _XhtmlElement;

        /// <summary>
        /// 该节点的头部Xhtml代码。
        /// </summary>
        public virtual XhtmlSection HeadXhtmlElement
        {
            get { return this._HeadXhtmlElement; }
            protected set { this._HeadXhtmlElement = value; }
        }
        private XhtmlSection _HeadXhtmlElement;

        /// <summary>
        /// 该节点的正文Xhtml代码。
        /// </summary>
        public virtual XhtmlSection ContentXhtmlElement
        {
            get { return this._ContentXhtmlElement; }
            protected set { this._ContentXhtmlElement = value; }
        }
        private XhtmlSection _ContentXhtmlElement;

        /// <summary>
        /// 根据当前节点的特性来设置该节点
        /// </summary>
        protected virtual void MarkXhtmlElement(PageSection pageSection)
        {
            switch (pageSection)
            {
                case PageSection.MainPage:
                    {
                        _XhtmlElement = new XhtmlSection();
                        string tmpltFullFile = this.ToHtmlHelper.TmpltRelativeUrl;
                        XhtmlElement tag = _XhtmlElement.CreateXhtmlCommentShtml
                            (tmpltFullFile + this.SdsiteElement.TmpltId + Utility.Const.ShtmlFileExt);
                        _XhtmlElement.AppendChild(tag);
                        this._isAlreadyToMainPageHtml = true;
                        break;
                    }
                case PageSection.Head:
                    {
                        _HeadXhtmlElement = new XhtmlSection();

                        #region Title

                        XhtmlTagElement title = _HeadXhtmlElement.CreateXhtmlTitle();
                        title.InnerText = this.Title;
                        _HeadXhtmlElement.AppendChild(title);

                        #endregion

                        #region keywords

                        XhtmlAtts.Name name;
                        StringBuilder sb;
                        XhtmlAtts.Content content;

                        if (!(this.PageKeywords == null))
                        {
                            name = new XhtmlAtts.Name("keywords");
                            sb = new StringBuilder();
                            foreach (string str in this.PageKeywords)
                            {
                                sb.Append(str).Append(',');
                            }
                            content = new XhtmlAtts.Content(sb.ToString().TrimEnd(','));
                            XhtmlTagElement keyword = _HeadXhtmlElement.CreateXhtmlMeta(name, content);
                            _HeadXhtmlElement.AppendChild(keyword);
                        }

                        #endregion

                        #region description

                        name = new XhtmlAtts.Name("description");
                        content = new XhtmlAtts.Content(this.PageSummary);
                        XhtmlTagElement description = _HeadXhtmlElement.CreateXhtmlMeta(name, content);
                        _HeadXhtmlElement.AppendChild(description);

                        #endregion

                        this._isAlreadyToHeadHtml = true;
                        break;
                    }
                case PageSection.Content:
                    {
                        SdsiteXmlDocument doc = (SdsiteXmlDocument)this.SdsiteElement.OwnerAnyDocument;

                        string tmpltId = this.SdsiteElement.TmpltId;
                        TmpltXmlDocument tmplt = doc.GetTmpltDocumentById(this.SdsiteElement.TmpltId);
                        XhtmlSection ele = (XhtmlSection)tmplt.GetContentSnipEle().XhtmlElement;
                        if (ele == null)
                        {
                            SnipXmlElement snip = tmplt.GetContentSnipEle();
                            snip.MarkXhtmlElement(this);
                            ele = (XhtmlSection)tmplt.GetContentSnipEle().XhtmlElement;
                        }
                        _ContentXhtmlElement = ele;
                        
                        this._isAlreadyToContentHtml = true;
                        break;
                    }
                case PageSection.None:
                default:
                    break;

            }
        }

        #endregion

    }
}

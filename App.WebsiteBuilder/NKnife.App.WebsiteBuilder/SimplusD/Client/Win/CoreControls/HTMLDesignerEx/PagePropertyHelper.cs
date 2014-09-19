using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

namespace Jeelu.SimplusD.Client.Win
{
    class PagePropertyHelper
    {
        private string _fPath;
        public string FilePath
        {
            get { return _fPath; }
            set { _fPath = value; }
        }

        private XmlDocument  _uNameDoc;
        public XmlDocument  NameDoc
        {
            get { return _uNameDoc; }
            set { _uNameDoc = value; }
        }

        private PageXmlDocument _pageTextDoc;
        public PageXmlDocument PageTextDoc
        {
            get { return _pageTextDoc; }
            set { _pageTextDoc = value; }
        }
        /// <summary>
        /// 加载相应的内容文章文件
        /// </summary>
        public PagePropertyHelper(string pageID)
        {
            if (!string.IsNullOrEmpty(pageID))
            {
                try
                {
                    _pageTextDoc = Service.Sdsite.CurrentDocument.GetPageDocumentById(pageID) as PageXmlDocument;   
                }
                catch
                {
                    throw;
                }
            }
            else
            { 
                _pageTextDoc.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
                <content content_id="" content_title="" 
                content_title_alias="""" created_time="""" design_summary="""" content_source="""" is_always_pub=""True"" pub_time=""""
                stop_time=""0001-1-1 0:00:00"" is_pub=""false"" is_modified=""true"" is_delete=""false"" pub_by="""" created_by=""""
                pub_by_alias="""" modify_by="""" modify_time="""" created_by_alias="""">
                  <article_text />
                  <article_summary></article_summary>
                  <files>
                    <file />
                  </files>
                  <tags>
                  </tags>
                </content>");
            }
            
        }
        /// <summary>
        /// 读出文章正文
        /// </summary>
        /// <returns></returns>
        public string ReadPageText()
        {
            string _pageText = ((PageXmlDocument)PageTextDoc).PageText;
            return _pageText;
        }
        /// <summary>
        /// 读出相应的属性值
        /// </summary>
        public PageTextPropertyItem ReadPageTextProp()
        {
            PageTextPropertyItem propertyItem = new PageTextPropertyItem();

            propertyItem.Title = PageTextDoc.Title;
            propertyItem.TitleAlias = PageTextDoc.PageTitleAlias;
            propertyItem.DeliverTime = PageTextDoc.PageCreateTime.ToString();
            propertyItem.AuthorAlias = PageTextDoc.AuthorAlias;
            propertyItem.DeliverTime = PageTextDoc.PageCreateTime;
            propertyItem.Summary = PageTextDoc.PageSummary;
            propertyItem.ModifyBy = PageTextDoc.ModifyAlias;
            propertyItem.DesignSummary = PageTextDoc.DesignSummary;
            if (PageTextDoc.PageKeywords != null)
            {
                foreach (string keyWord in PageTextDoc.PageKeywords)
                {
                    propertyItem.tag.Add(keyWord);
                }
            }
            propertyItem.ContentSource = PageTextDoc.ContentSource;
            return propertyItem;
        }
        /// <summary>
        /// 读出文件中的所有关键字
        /// </summary>
        /// <param name="property"></param>
        /// <param name="tagsNode"></param>
        private void ReadXmlTag(PageTextPropertyItem property, XmlNode tagsNode)
        {
            XmlNodeList xnl = tagsNode.ChildNodes;
            foreach (XmlNode node in xnl)
            {
                property.tag.Add(node.InnerText);
            }
        }
        /// <summary>
        /// 将窗体值转入xml文件中
        /// </summary>
        public void WritePageTextProp(PageTextPropertyItem property,string PageId)
        {
            PageXmlDocument doc =Service.Sdsite.CurrentDocument.GetPageDocumentById(PageId) as PageXmlDocument;
            PageSimpleExXmlElement ele = Service.Sdsite.CurrentDocument.GetPageElementById(PageId) as PageSimpleExXmlElement;

            ele.IsModified = true;
            doc.Title = property.Title;
            doc.PageTitleAlias = property.TitleAlias;
            doc.PageCreateTime = property.DeliverTime;
            doc.PageSummary = property.Summary;
            doc.Author = property.AuthorAlias;
            doc.AuthorAlias = property.AuthorAlias;
            doc.ModifyAlias = property.ModifyBy;
            doc.DesignSummary = property.DesignSummary;
            
            doc.PageKeywords = property.tag.ToArray();
            doc.ContentSource = property.ContentSource;

            //                doc.DocumentElement.Attributes["content_source"].Value = property.ContentSource;
            //doc.DocumentElement.Attributes["is_always_pub"].Value = property.IsAlwaysPub.ToString();

            //doc.DocumentElement.Attributes["stop_time"].Value = property.EndPubTime;
            doc.Save();
            Service.Sdsite.CurrentDocument.Save();
            ///写入文章摘要
           /* 

            doc.DocumentElement.Attributes["content_source"].Value = property.ContentSource;
            doc.DocumentElement.Attributes["is_always_pub"].Value = property.IsAlwaysPub.ToString();

            doc.DocumentElement.Attributes["stop_time"].Value = property.EndPubTime;
           

            XmlNode tagNode = ContentDoc.SelectSingleNode("/content/tags");
            WriteXmlTag(property.tag, tagNode);

            if (text != "")
            {
                ///重新存储
                XmlNode textNode = ContentDoc.SelectSingleNode("/content/article_text");
                textNode.RemoveAll();
                XmlCDataSection xmlData = ContentDoc.CreateCDataSection(text);
                textNode.AppendChild(xmlData);
            }*/
        }
        /// <summary>
        /// 将关键字写入到文件中
        /// </summary>
        /// <param name="prop"></param>
        private void WriteXmlTag(List<string> tags, XmlNode tagsNode)
        {
            XmlNode tagNode = PageTextDoc.SelectSingleNode("/content/tags");
            tagNode.RemoveAll();
            foreach (string tag in tags)
            {
                XmlElement ele = PageTextDoc.CreateElement("tag");
                ele.InnerText = tag;
                tagNode.AppendChild(ele);
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        public void SaveXmlDocument()
        {
           // PageTextDoc.Save();
        }

    }

    /// <summary>
    /// 页面内容基本资料
    /// </summary>
    class PageTextPropertyItem
    {
        public List<string> tag = new List<string>();

        /// <summary>
        /// 标题
        /// </summary>
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 标题别名
        /// </summary>
        private string _titleAlias;
        public string TitleAlias
        {
            get { return _titleAlias; }
            set { _titleAlias = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        private string _deliverTime;
        public string DeliverTime
        {
            get { return _deliverTime; }
            set { _deliverTime = value; }
        }
        /// <summary>
        /// 作者别名
        /// </summary>
        private string _authorAlias;
        public string AuthorAlias
        {
            get { return _authorAlias; }
            set { _authorAlias = value; }
        }
        /// <summary>
        /// 修改人
        /// </summary>
        private string  _modifyBy;
        public string  ModifyBy
        {
            get { return _modifyBy; }
            set { _modifyBy = value; }
        }
        /// <summary>
        /// 设计摘要
        /// </summary>
        private string _designSummary;
        public string DesignSummary
        {
            get { return _designSummary; }
            set { _designSummary = value; }
        }
        /// <summary>
        /// 文章摘要
        /// </summary>
        private string _summary;
        public string Summary
        {
            get { return _summary; }
            set { _summary = value; }
        }
        /// <summary>
        /// 文章来源
        /// </summary>
        private string _contentSource;
        public string ContentSource
        {
            get { return _contentSource; }
            set { _contentSource = value; }
        }
        /// <summary>
        /// 是否一直发布
        /// </summary>
        private bool _isAlwaysPub;
        public bool IsAlwaysPub
        {
            get { return _isAlwaysPub; }
            set { _isAlwaysPub = value; }
        }
        /// <summary>
        /// 发布时间
        /// </summary>
        private string _startPubTime;
        public string StartPubTime
        {
            get { return _startPubTime; }
            set { _startPubTime = value; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        private string _endPubTime;
        public string EndPubTime
        {
            get { return _endPubTime; }
            set { _endPubTime = value; }
        }
    }

    public static class CleanHtmlSign
    {
        public static string CleanHtmlSignFunction(string Htmlstring)
        {
            //删除样式设置与脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<style[^>]*?>.*?</style>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace(">>", ",");
            Htmlstring.Replace("|", ",");
            Htmlstring.Replace("┊", ",");
            Htmlstring.Replace("  ", ",");
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring.Replace(",,", ",");
            return Htmlstring;
        }
    }
}

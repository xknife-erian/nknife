using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 页面在SdSite中的简单存放
    /// </summary>
    public class PageSimpleExXmlElement : SimpleExIndexXmlElement, IGetDocumentable
    {
        internal protected PageSimpleExXmlElement(XmlDocument doc)
            : base("page", doc)
        {
        }

        public PageXmlDocument GetIndexXmlDocument()
        {
            //return new PageXmlDocument(this.FilePath,this.Id, this.PageType);
            PageXmlDocument doc = PageXmlDocument.CreateInstance(this.RelativeFilePath, this.Id, this.PageType, this);
            doc.Load();

            return doc;
        }

        #region IGetDocumentable 成员

        IndexXmlDocument IGetDocumentable.GetIndexXmlDocument()
        {
            return GetIndexXmlDocument();
        }

        #endregion

        /// <summary>
        /// 页面类型
        /// </summary>
        public PageType PageType
        {
            get
            {
                string strType = this.GetAttribute("type");
                return (PageType)Enum.Parse(typeof(PageType), strType);
            }
        }

        public override string RelativeFilePath
        {
            get
            {
                if (OwnerFolderElement == null)
                {
                    return null;
                }
                return OwnerFolderElement.RelativeFilePath + FileName + Utility.Const.PageFileExt;
            }
        }

        /// <summary>
        /// 相对路径，windows方式的，如：“\abc\xyz\”。
        /// 
        /// </summary>
        public override string RelativeUrl
        {
            get
            {
                if (OwnerFolderElement == null)
                {
                    return null;
                }
                return OwnerFolderElement.RelativeUrl + this.FileName + Utility.Const.ShtmlFileExt; //todo:
            }
        }

        /// <summary>
        /// SimpleElement对应的数据类型
        /// </summary>
        public override DataType DataType
        {
            get { return DataType.Page; }
        }

        /// <summary>
        /// SD页面的扩展名
        /// </summary>
        public override string FileExtension
        {
            get
            {
                return Utility.Const.PageFileExt;
            }
        }

        /// <summary>
        /// 当前页面所关联的模板
        /// </summary>
        public string TmpltId
        {
            get { return GetAttribute("tmpltId"); }
            set { SetAttribute("tmpltId", value); }
        }

        /// <summary>
        /// 获取是否是索引页面
        /// </summary>
        public bool IsIndexPage
        {
            get
            {
                if (this.OwnerChannelElement.DefaultPageId == this.Id)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool IsOldIndexPage
        {
            get
            {
                try
                {
                    if (OwnerDocument.GetChannelElementById(this.OldParentFolderId).DefaultPageId == this.Id)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取正文相对模板的路径
        /// </summary>
        private string GetContentPath(string pagePath, string tmpltPath)
        {
            string contentPath = "";
            tmpltPath = tmpltPath.Substring(1);
            while (!string.IsNullOrEmpty(tmpltPath))
            {
                tmpltPath = DeleteFirstPath(tmpltPath);
                contentPath += "../";
            }

            return contentPath + pagePath.Substring(1);
        }
        private string DeleteFirstPath(string Path)
        {
            int i = Path.IndexOf("/");
            return Path.Substring(i + 1);
        }

        private string IsContainSameFile(string html)
        {
            return "";
        }

        public override string OldRelativeFilePath
        {
            get
            {
                ///获得原父节点
                FolderXmlElement oldOwnerFolder = OwnerFolderElement;
                if (!string.IsNullOrEmpty(OldParentFolderId))
                {
                    oldOwnerFolder = OwnerDocument.GetFolderElementById(OldParentFolderId);
                }

                if (oldOwnerFolder == null)
                {
                    return null;
                }

                ///获得原文件名
                string oldFileName = FileName;
                if (!string.IsNullOrEmpty(OldFileName))
                {
                    oldFileName = OldFileName;
                }

                ///通过原父节点和原文件名即可得到原路径
                return oldOwnerFolder.OldRelativeFilePath + oldFileName + Utility.Const.PageFileExt;
            }
        }

        public override string OldRelativeUrl
        {
            get
            {
                ///获得原父节点
                FolderXmlElement oldOwnerFolder = OwnerFolderElement;
                if (!string.IsNullOrEmpty(OldParentFolderId))
                {
                    oldOwnerFolder = OwnerDocument.GetFolderElementById(OldParentFolderId);
                }

                if (oldOwnerFolder == null)
                {
                    return null;
                }

                ///通过原父节点和原文件名即可得到原路径
                return oldOwnerFolder.OldRelativeUrl;
            }
        }

        #region 可能是王淼写的代码，重构页面生成，故注释掉。modify by lukan, 2008-6-25 12:22:59

        /*
        /// <summary>
        /// 主页的page
        /// </summary>
        public string HomePageToHtml()
        {
            string pageContent="";
            string tmpltId = this.TmpltId;
            string title=this.Title;
            string tmpltPath = OwnerDocument.GetTmpltElementById(tmpltId).RelativeUrl;
            PageXmlDocument pageXmlDoc = OwnerDocument.GetPageDocumentById(this.Id);
            //TmpltXmlDocument tmpltXmlDoc =
            string author = pageXmlDoc.Author;
            string description = pageXmlDoc.PageSummary;
            pageContent = Utility.Const.PublishDtd + "<!--#include virtual=\"" + tmpltPath + "?title=" + title + "&description=" + description + "&author=" + author + "\" -->";
            return pageContent;
        }

        /// <summary>
        /// 处理含正文型页面
        /// </summary>
        public string ContentPageToHtml(string pagePath)
        {
            //将page退到根节点
            string pageContent = "";
            string tmpltId = this.TmpltId;
            string title = OwnerDocument.GetPageElementById(this.Id).Title;
            string keyList = "./KeyList/";
            TmpltSimpleExXmlElement tmpltEle = OwnerDocument.GetTmpltElementById(tmpltId);
            string contentPath = GetContentPath(pagePath, tmpltEle.OwnerFolderElement.RelativeUrl);
            PageXmlDocument pageXmlDoc = OwnerDocument.GetPageDocumentById(this.Id);
            TmpltXmlDocument tmpltDoc = OwnerDocument.GetTmpltDocumentById(this.TmpltId);
            string author = pageXmlDoc.Author;
            string description = pageXmlDoc.PageSummary;
            //判断模版是否含有关键字列表tmpltDoc.HasAutoKeyWordsSequenceType
            if (pageXmlDoc.PageKeywords == null)
            {
                pageXmlDoc.PageKeywords = new string[1] { " " };
            }

            ///根据模板的层级，生成../
            const string OnePre = @"../";
            StringBuilder prePath = new StringBuilder(OnePre);   //初始化为第2级
            int level = tmpltEle.FileLevel;
            for (int i = 3; i <= level; i++)   //资源文件夹在第1级，所以从第2级开始加路径前缀，因为初始化了1级，所以从3开始循环)
            {
                prePath.Append(OnePre);
            }
            prePath.Remove(prePath.Length - 1, 1);   //删去最后一个反斜杠/，因为RelativeUrl已经带了

            string keyWordString = Utility.Convert.ConvertStringArrayToString(pageXmlDoc.PageKeywords, ",");
            if(tmpltDoc.HasAutoKeyWordsSequenceType)
            {
                SnipXmlElement snipEle = tmpltDoc.GetKeyListSnip();
                SnipPartXmlElement snipPartEle= snipEle.GetKeywordListBox();
                XmlNodeList xmlNodeList = snipPartEle.SelectSingleNode("//channels").ChildNodes;
                foreach (XmlElement xmlEle in xmlNodeList)
                {
                    string channelId=xmlEle.GetAttribute("id");
                    keyList += OwnerDocument.GetChannelElementById(channelId).FileName;
                }
                
                foreach(string keyWord in pageXmlDoc.PageKeywords)
                {
                    keyList += keyWord;
                }

                pageContent = Utility.Const.PublishDtd + "<!--#include virtual=\"" + tmpltEle.RelativeUrl + "?content=" + prePath + this.OwnerFolderElement.RelativeUrl + this.Id + "&title=" + title + "&description=" + description + "&author=" + author + "&keywordList=" + keyList + "&keyword=" + keyWordString + "\" -->";
            }
            else
            {
                pageContent = Utility.Const.PublishDtd + "<!--#include virtual=\"" + tmpltEle.RelativeUrl + "?content=" + prePath + this.OwnerFolderElement.RelativeUrl + this.Id + "&title=" + title + "&keywords=" + keyWordString + "&description=" + description + "&author=" + author + "\" -->";
            }
            return pageContent;
        }

        public string ToHtml()
        {
            if (this.PageType == PageType.Home)
            {
                return HomePageToHtml();
            }
            else
            {
                return ContentPageToHtml(this.OwnerFolderElement.RelativeUrl);
            }
        }

        private string ToFileName(string filePath)
        {
            string[] channelName= filePath.Split('/');
            string fileName="";
            foreach(string singleChanelName in channelName)
            {
                fileName+=singleChanelName;
            }
            return fileName;
        }

        */

        #endregion

    }
}

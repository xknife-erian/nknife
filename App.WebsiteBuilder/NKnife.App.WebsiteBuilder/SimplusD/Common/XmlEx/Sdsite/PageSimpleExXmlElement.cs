using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// ҳ����SdSite�еļ򵥴��
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

        #region IGetDocumentable ��Ա

        IndexXmlDocument IGetDocumentable.GetIndexXmlDocument()
        {
            return GetIndexXmlDocument();
        }

        #endregion

        /// <summary>
        /// ҳ������
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
        /// ���·����windows��ʽ�ģ��磺��\abc\xyz\����
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
        /// SimpleElement��Ӧ����������
        /// </summary>
        public override DataType DataType
        {
            get { return DataType.Page; }
        }

        /// <summary>
        /// SDҳ�����չ��
        /// </summary>
        public override string FileExtension
        {
            get
            {
                return Utility.Const.PageFileExt;
            }
        }

        /// <summary>
        /// ��ǰҳ����������ģ��
        /// </summary>
        public string TmpltId
        {
            get { return GetAttribute("tmpltId"); }
            set { SetAttribute("tmpltId", value); }
        }

        /// <summary>
        /// ��ȡ�Ƿ�������ҳ��
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
        /// ��ȡ�������ģ���·��
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
                ///���ԭ���ڵ�
                FolderXmlElement oldOwnerFolder = OwnerFolderElement;
                if (!string.IsNullOrEmpty(OldParentFolderId))
                {
                    oldOwnerFolder = OwnerDocument.GetFolderElementById(OldParentFolderId);
                }

                if (oldOwnerFolder == null)
                {
                    return null;
                }

                ///���ԭ�ļ���
                string oldFileName = FileName;
                if (!string.IsNullOrEmpty(OldFileName))
                {
                    oldFileName = OldFileName;
                }

                ///ͨ��ԭ���ڵ��ԭ�ļ������ɵõ�ԭ·��
                return oldOwnerFolder.OldRelativeFilePath + oldFileName + Utility.Const.PageFileExt;
            }
        }

        public override string OldRelativeUrl
        {
            get
            {
                ///���ԭ���ڵ�
                FolderXmlElement oldOwnerFolder = OwnerFolderElement;
                if (!string.IsNullOrEmpty(OldParentFolderId))
                {
                    oldOwnerFolder = OwnerDocument.GetFolderElementById(OldParentFolderId);
                }

                if (oldOwnerFolder == null)
                {
                    return null;
                }

                ///ͨ��ԭ���ڵ��ԭ�ļ������ɵõ�ԭ·��
                return oldOwnerFolder.OldRelativeUrl;
            }
        }

        #region ���������д�Ĵ��룬�ع�ҳ�����ɣ���ע�͵���modify by lukan, 2008-6-25 12:22:59

        /*
        /// <summary>
        /// ��ҳ��page
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
        /// ����������ҳ��
        /// </summary>
        public string ContentPageToHtml(string pagePath)
        {
            //��page�˵����ڵ�
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
            //�ж�ģ���Ƿ��йؼ����б�tmpltDoc.HasAutoKeyWordsSequenceType
            if (pageXmlDoc.PageKeywords == null)
            {
                pageXmlDoc.PageKeywords = new string[1] { " " };
            }

            ///����ģ��Ĳ㼶������../
            const string OnePre = @"../";
            StringBuilder prePath = new StringBuilder(OnePre);   //��ʼ��Ϊ��2��
            int level = tmpltEle.FileLevel;
            for (int i = 3; i <= level; i++)   //��Դ�ļ����ڵ�1�������Դӵ�2����ʼ��·��ǰ׺����Ϊ��ʼ����1�������Դ�3��ʼѭ��)
            {
                prePath.Append(OnePre);
            }
            prePath.Remove(prePath.Length - 1, 1);   //ɾȥ���һ����б��/����ΪRelativeUrl�Ѿ�����

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

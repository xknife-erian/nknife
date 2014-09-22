using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// [ 项目文件.sdsite ] 对应的XmlDocument。
    /// [ 项目文件.sdsite ] 是项目最主要的文件，
    /// 存储所有数据文件的索引、当前网站项目的选项、网站属性(siteData)，以及上传信息。
    /// 对开发人员来说，当前类是当前程序集的入口。
    /// </summary>
    public class SdsiteXmlDocument : AnyXmlDocument
    {
        public SdsiteXmlDocument(string absoluteFilePath)
            : base(absoluteFilePath)
        {
            this.PreserveWhitespace = false;
        }

        #region 一些供开发者使用的属性

        public ResourcesXmlElement Resources { get; private set; }

        public TmpltFolderXmlElement TmpltFolder { get; private set; }

        public SitePropertyXmlElement SiteProperty { get; private set; }

        public RootChannelXmlElement RootChannel { get; private set; }

        public HelperDataXmlElement HelperData { get; private set; }

        #endregion

        #region override重写基类的方法

        //private string _relativeFilePath;
        //public override string RelativeFilePath
        //{
        //    get
        //    {
        //        return _relativeFilePath;
        //    }
        //}

        /// <summary>
        /// 已重写。从指定的XmlReader加载XML文档。
        /// (XmlDocument所有的Load方法都最终调用此方法，若以后版本的.NET有改变，则此处需要改变)
        /// </summary>
        public override void Load(XmlReader reader)
        {
            base.Load(reader);

            ///处理channel节点
            ProcessChannelTemp(this.DocumentElement);

            ///给几个供开发者使用的主要属性赋值
            SiteProperty = (SitePropertyXmlElement)this.GetElementByName("siteProperty");
            RootChannel = (RootChannelXmlElement)this.GetElementByName("channel");
            Resources = (ResourcesXmlElement)this.GetElementByName("resources");
            TmpltFolder = (TmpltFolderXmlElement)this.GetElementByName("tmpltRootFolder");
            HelperData = (HelperDataXmlElement)this.GetElementByName("helperData");
        }

        /// <summary>
        /// (递归)处理channel节点，根据属性type将ChannelTempXmlElement替换成对应的类型
        /// </summary>
        private void ProcessChannelTemp(XmlElement eleInput)
        {
            XmlNodeList nodes = eleInput.SelectNodes(@"channel");
            foreach (XmlNode node in nodes)
            {
                ChannelTempXmlElement channelTemp = (ChannelTempXmlElement)node;
                XmlElement newChannelEle = null;

                if (channelTemp.IsRootChannel)
                {
                    RootChannelXmlElement ele = new RootChannelXmlElement(this);
                    XmlUtilService.CopyXmlElement(channelTemp, ele);
                    newChannelEle = ele;
                }
                else
                {
                    GeneralChannelXmlElement ele = new GeneralChannelXmlElement(this);
                    XmlUtilService.CopyXmlElement(channelTemp, ele);
                    newChannelEle = ele;
                }

                eleInput.InsertAfter(newChannelEle, channelTemp);

                eleInput.RemoveChild(channelTemp);

                ///递归调用
                ProcessChannelTemp(newChannelEle);
            }
        }

        public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
        {
            XmlElement returnEle = null;
            switch (localName)
            {
                #region 系统必须的

                case "folder":
                    returnEle = new FolderXmlElement(this);
                    break;

                case "channel":
                    returnEle = new ChannelTempXmlElement(this);
                    break;

                case "page":
                    returnEle = new PageSimpleExXmlElement(this);
                    break;

                case "resources":
                    returnEle = new ResourcesXmlElement(this);
                    break;

                case "tmpltRootFolder":
                    returnEle = new TmpltFolderXmlElement(this);
                    break;

                case "tmplt":
                    returnEle = new TmpltSimpleExXmlElement(this);
                    break;

                case "file":
                    returnEle = new FileSimpleExXmlElement(this);
                    break;

                case "helperData":
                    returnEle = new HelperDataXmlElement(this);
                    break;

                #endregion

                #region option节点下的节点
                case "hrSample":
                    returnEle = new HrSampleXmlElement(this);
                    break;

                case "productPropertyGroup":
                    returnEle = new ProductPropertyGroupXmlElement(this);
                    break;
                #endregion

                #region UGS节点下的节点

                //case "guestSearch":
                //    returnEle = new GuestSearchElement(this);
                //    break;
                //case "searchContent":
                //    returnEle = new SearchContentElement(this);
                //    break;

                #endregion

                #region 网站属性相关节点 zhangling 2008年2月26日19时13分
                case "siteProperty":
                    returnEle = new SitePropertyXmlElement(this);
                    break;
                case "siteBasicData":
                    returnEle = new SiteBasicDataXmlElement(this);
                    break;
                case "siteShowItem":
                    returnEle = new SiteShowItemsXmlElement(this);
                    break;
                case "department":
                    returnEle = new SiteDepartmentXmlElement(this);
                    break;
                #endregion

                default:
                    returnEle = base.CreateElement(prefix, localName, namespaceURI);
                    break;
            }

            return returnEle;
        }

        #endregion

        #region Create方法。用来创建频道、文件夹、页面、模板

        public ChannelSimpleExXmlElement CreateChannel(FolderXmlElement parentFolder, string title)
        {
            Debug.Assert(parentFolder != null);

            ChannelSimpleExXmlElement newEle = new ChannelSimpleExXmlElement(this);
            string newId = Utility.Guid.NewGuid();

            ///将文件名翻译成拼音，生成一个不重名的文件名
            string pinyinFileName = Utility.File.FormatFileName(title, false);
            string strFileName = Utility.File.BuildFileName(parentFolder.AbsoluteFilePath, pinyinFileName, true, false);

            newEle.Id = newId;
            newEle.Title = title;
            newEle.FileName = strFileName;
            newEle.IsDeleted = false;
            newEle.IsPublish = true;
            newEle.IsModified = true;

            parentFolder.IsModified = true;

            parentFolder.AppendChild(newEle);

            Directory.CreateDirectory(newEle.AbsoluteFilePath);

            Save();

            OnElementAdded(new EventArgs<SimpleExIndexXmlElement>(newEle));

            return newEle;
        }

        public ChannelSimpleExXmlElement CreateChannel(string parentFolderId, string title)
        {
            return CreateChannel(GetFolderElementById(parentFolderId), title);
        }

        public FolderXmlElement CreateFolder(FolderXmlElement parentFolder, string title)
        {
            Debug.Assert(parentFolder != null);

            FolderXmlElement newEle = new FolderXmlElement(this);
            string newId = Utility.Guid.NewGuid();

            newEle.Id = newId;

            ///将文件名翻译成拼音，生成一个不重名的文件名
            string pinyinFileName = Utility.File.FormatFileName(title, false);
            string strFileName = Utility.File.BuildFileName(parentFolder.AbsoluteFilePath, pinyinFileName, true, false);

            newEle.Title = title;
            newEle.FileName = strFileName;
            newEle.IsDeleted = false;
            newEle.IsPublish = true;
            newEle.IsModified = true;

            parentFolder.IsModified = true;

            if (parentFolder.FolderType != FolderType.None)
                newEle.FolderType = parentFolder.FolderType;
            else
            {
                if (parentFolder.DataType == DataType.TmpltFolder)
                    newEle.FolderType = FolderType.TmpltFolder;
                else if (parentFolder.DataType == DataType.Resources)
                    newEle.FolderType = FolderType.ResourcesFolder;
                else if (parentFolder.DataType == DataType.Channel)
                    newEle.FolderType = FolderType.ChannelFolder;
            }


            parentFolder.AppendChild(newEle);

            Directory.CreateDirectory(newEle.AbsoluteFilePath);

            Save();

            OnElementAdded(new EventArgs<SimpleExIndexXmlElement>(newEle));

            return newEle;
        }

        public FolderXmlElement CreateFolder(string parentFolderId, string title)
        {
            return CreateFolder(GetFolderElementById(parentFolderId), title);
        }

        /// <summary>
        /// 返回第一个为模板ID,第二个为页面ID
        /// </summary>
        /// <param name="parentFolder"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public KeyValuePair<string, string> CreateHome(FolderXmlElement parentFolder, string title)
        {
            Debug.Assert(parentFolder != null);

            TmpltSimpleExXmlElement tmpltEle = CreateTmplt("00000002", TmpltType.Home, title);
            PageSimpleExXmlElement pageEle = CreatePage(parentFolder, PageType.Home, title, tmpltEle.Id);

            if (string.IsNullOrEmpty(parentFolder.DefaultPageId))
            {
                parentFolder.DefaultPageId = pageEle.Id;
            }
            parentFolder.IsModified = true;

            return new KeyValuePair<string, string>(tmpltEle.Id, pageEle.Id);
        }

        public PageSimpleExXmlElement CreatePage(FolderXmlElement parentFolder, PageType type, string title, string tmlptId)
        {
            Debug.Assert(parentFolder != null);
            Debug.Assert(type != PageType.None);

            PageSimpleExXmlElement newEle = new PageSimpleExXmlElement(this);
            string newId = Utility.Guid.NewGuid();

            ///给newEle赋初值
            newEle.Id = newId;
            newEle.Title = title;

            ///将文件名翻译成拼音，生成一个不重名的文件名
            string pinyinFileName = Utility.File.FormatFileName(title, false);
            newEle.FileName = Utility.File.BuildFileName(parentFolder.AbsoluteFilePath, pinyinFileName + Utility.Const.PageFileExt, false, false);

            newEle.IsDeleted = false;
            newEle.IsPublish = true;
            newEle.IsModified = true;
            newEle.PublishedTime =DateTime.Now;
            newEle.AdTime =DateTime.MaxValue;
            //newEle.PublishedTime = Convert.ToDateTime(string.Empty);
            //newEle.AdTime = Convert.ToDateTime(string.Empty);
            //newEle.IsTitleModify = true;
            newEle.SetAttribute("type", type.ToString());
            newEle.CreatedTime = DateTime.Now;
            ///添加到父节点
            parentFolder.IsModified = true;
            parentFolder.AppendChild(newEle);

            ///todo:添加XmlDocument文件
            PageXmlDocument newDoc = PageXmlDocument.CreateInstance(newEle.RelativeFilePath, newEle.Id, type, newEle);
            string strXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<page sdVersion=""{3}"" id=""{0}"" title=""{1}"" type=""{2}"">
<pageText />
<pageSummary />
<resources />
</page>";
            strXml = string.Format(strXml, newId, title, type.ToString(),Utility.Version.SoftwareVersion);
            newDoc.LoadXml(strXml);
            newEle.TmpltId = tmlptId;
            newDoc.Save();

            Save();

            ///触发事件
            OnElementAdded(new EventArgs<SimpleExIndexXmlElement>(newEle));

            return newEle;
        }

        public PageSimpleExXmlElement CreatePage(string parentFolderId, PageType type, string title, string tmlptId)
        {
            return CreatePage(GetFolderElementById(parentFolderId), type, title, tmlptId);
        }

        /// <summary>
        /// 创建模板文件
        /// </summary>
        /// <param name="parentFolder">在什么地方创建</param>
        /// <param name="type">创建成什么类型</param>
        /// <returns>返回模板型的XML节点</returns>
        public TmpltSimpleExXmlElement CreateTmplt(string folderId, TmpltType type, string title)
        {
            return CreateTmplt(GetFolderElementById(folderId), type, title, false, 780, 600, null);
        }
        /// <summary>
        /// 创建模板文件
        /// </summary>
        /// <param name="parentFolder">在什么地方创建</param>
        /// <param name="type">创建成什么类型</param>
        /// <returns>返回模板型的XML节点</returns>
        public TmpltSimpleExXmlElement CreateTmplt(FolderXmlElement folderEle, TmpltType type, string title, bool hasContentSnip,
            int width, int height, Image backImg)
        {
            string newId = Utility.Guid.NewGuid();

            ///将文件名翻译成拼音，生成一个不重名的文件名
            string pinyinFileName = Utility.File.FormatFileName(title, false);
            string strFileName = Utility.File.BuildFileName(folderEle.AbsoluteFilePath, pinyinFileName + Utility.Const.TmpltFileExt, false, false);

            TmpltSimpleExXmlElement newEle = CreateNewTmpltElement(newId, strFileName, type);
            newEle.Title = title;

            ///添加到父节点
            folderEle.IsModified = true;
            folderEle.AppendChild(newEle);

            ///添加XmlDocument文件
            TmpltXmlDocument newDoc = TmpltXmlDocument.CreateInstance(newEle.RelativeFilePath, newEle.Id, newEle);
            string strXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?><tmplt sdVersion=""{2}"" id=""{0}"" title=""{1}""><lines></lines><rects></rects></tmplt>";
            strXml = string.Format(strXml, newId, title, Utility.Version.SoftwareVersion);
            newDoc.LoadXml(strXml);
            newDoc.HasAutoKeyWordsSequenceType = false;
            CreateNewTmpltXmlDocuMent(newDoc, hasContentSnip, width, height, backImg);

            newDoc.DocumentElement.SetAttribute("type", type.ToString());
            newDoc.Save();

            Save();

            ///触发事件
            OnElementAdded(new EventArgs<SimpleExIndexXmlElement>(newEle));

            return newEle;
        }

        private TmpltSimpleExXmlElement CreateNewTmpltElement(string id, string fileName, TmpltType type)
        {
            TmpltSimpleExXmlElement newEle = new TmpltSimpleExXmlElement(this);
            newEle.Id = id;
            newEle.FileName = fileName;
            newEle.TmpltType = type;
            newEle.IsPublish = true;
            return newEle;
        }

        /// <summary>
        /// ZhengHao : 填充新的模板文件（新建空文件）
        /// </summary>
        /// <param name="doc">要填充的文件对象</param>
        /// <param name="hasContentSnip">本模板是否拥有正文型页面片</param>
        /// <param name="width">模板宽度</param>
        /// <param name="height">模板高度</param>
        /// <param name="backImg">模板的背景图片（可以为null）</param>
        private void CreateNewTmpltXmlDocuMent(TmpltXmlDocument doc, bool hasContentSnip, int width, int height, Image backImg)
        {
            XmlElement ele = doc.DocumentElement;

            ele.SetAttribute("hasContent", hasContentSnip.ToString());
            ele.SetAttribute("width", width.ToString());
            ele.SetAttribute("height", height.ToString());
            //ele.SetAttribute("css", @"width:" + width.ToString() + "px;" + @"height:" + height.ToString() + "px;");
            //ele.SetAttribute("a", @"color:#0000ff; text-decoration:underline; ");
            //ele.SetAttribute("a_visited", @"color:#0000ff; text-decoration:underline; ");
            //ele.SetAttribute("webCss", "");
            //ele.SetAttribute("img", @"border:0; ");
            /////////////////////////////////////////////////////////////
            // 保存切割后的图片
            if (backImg != null)
            {
                ele.SetAttribute("hasBackImg", true.ToString());
                AnyXmlElement imgEle = new AnyXmlElement("backImage", doc);
                imgEle.CDataValue = Utility.Convert.ImageToBase64(backImg);
                ele.AppendChild(imgEle);
            }
            else
            {
                ele.SetAttribute("hasBackImg", false.ToString());
            }

            ///获取‘矩形’XML元素的集合
            SnipRectXmlElement rectEle = (SnipRectXmlElement)doc.CreateElement("rect");//原先这里创建的节点名是rectDiv，modify by lukan,2008年6月20日12时55分
            rectEle.Css = "";
            SnipXmlElement snipEle = new SnipXmlElement(doc);
            snipEle.Id = Utility.Guid.NewGuid();
            snipEle.SnipType = PageSnipType.None;
            snipEle.X = 0;
            snipEle.Y = 0;
            snipEle.HasSnip = false;
            snipEle.Css = @"width:" + width.ToString() + "px;" + @"height:" + height.ToString() + "px;";
            snipEle.Width = width.ToString();
            snipEle.Height = height.ToString();
            snipEle.IsLocked = false;
            snipEle.IsSelected = false;

            rectEle.AppendChild(snipEle);

            ///初始化模板XML的"rects"节点，lukan增加注释
            doc.RectElements = rectEle;

            ///获取‘线段’XML元素的集合
            int w = width;
            int h = height;
            List<BorderLineXmlElement> lines = new List<BorderLineXmlElement>();

            BorderLineXmlElement lineElement = new BorderLineXmlElement(doc);
            lineElement.Start = 0;
            lineElement.End = w;
            lineElement.Position = 0;
            lineElement.IsRow = true;
            lines.Add(lineElement);

            lineElement = new BorderLineXmlElement(doc);
            lineElement.Start = 0;
            lineElement.End = h;
            lineElement.Position = w;
            lineElement.IsRow = false;
            lines.Add(lineElement);

            lineElement = new BorderLineXmlElement(doc);
            lineElement.Start = 0;
            lineElement.End = h;
            lineElement.Position = 0;
            lineElement.IsRow = false; ;
            lines.Add(lineElement);

            lineElement = new BorderLineXmlElement(doc);
            lineElement.Start = 0;
            lineElement.End = w;
            lineElement.Position = h;
            lineElement.IsRow = true;
            lines.Add(lineElement);

            doc.BorderLineElements = lines;
        }

        private void Assert(bool p)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        //public TmpltSimpleExXmlElement CreateTmplt(string parentFolderId, TmpltType type, string title)
        //{
        //    return CreateTmplt(GetFolderElementById(parentFolderId), type, title);
        //}

        public FileSimpleExXmlElement CreateFileElement(FolderXmlElement parentFolder,string srcFileName)
        {
            ///将文件名翻译成拼音，生成一个不重名的文件名
            string fileName = Path.GetFileName(srcFileName);
            string pinyinFileName = Utility.File.FormatFileName(fileName, false);
            string formatFileName = Utility.File.BuildFileName(parentFolder.AbsoluteFilePath, pinyinFileName, false, true);

            ///拷贝文件
            File.Copy(srcFileName, Path.Combine(parentFolder.AbsoluteFilePath,formatFileName));

            ///创建Element
            FileSimpleExXmlElement newEle = new FileSimpleExXmlElement(this);
            newEle.Id = Utility.Guid.NewGuid();
            newEle.Title = fileName;
            newEle.FileName = formatFileName;
            newEle.IsPublish = true;
            newEle.IsModified = true;

            parentFolder.IsModified = true;
            parentFolder.AppendChild(newEle);
            Save();

            OnElementAdded(new EventArgs<SimpleExIndexXmlElement>(newEle));

            return newEle;
        }
        public FileSimpleExXmlElement CreateFileElement(string parentFolderId, string srcFileName)
        {
            return CreateFileElement(this.GetFolderElementById(parentFolderId), srcFileName);
        }

        public FileSimpleExXmlElement CreateFileElementNoCreateFile(FolderXmlElement parentFolder, string fileName, string title)
        {
            ///创建Element
            FileSimpleExXmlElement newEle = new FileSimpleExXmlElement(this);
            newEle.Id = Utility.Guid.NewGuid();
            newEle.Title = title;
            newEle.FileName = fileName;
            newEle.IsPublish = true;
            newEle.IsModified = true;

            parentFolder.IsModified = true;
            parentFolder.AppendChild(newEle);
            Save();

            OnElementAdded(new EventArgs<SimpleExIndexXmlElement>(newEle));

            return newEle;
        }
        public FileSimpleExXmlElement CreateFileElementNoCreateFile(string parentFolderId, string fileName, string title)
        {
            return CreateFileElementNoCreateFile(this.GetFolderElementById(parentFolderId), fileName, title);
        }

        //public SimpleExIndexXmlElement CreateOutSideEle(FolderXmlElement parentFolder,DataType type, string title)
        //{
        //    Debug.Assert(parentFolder != null);

        //    SimpleExIndexXmlElement newEle = new SimpleExIndexXmlElement(this);
        //    string newId = Utility.Guid.NewGuid();

        //    ///todo:给newEle赋初值
        //    newEle.Id = newId;
        //    newEle.Title = title;
        //    newEle.FileName = title;
        //    newEle.IsDeleted = false;
        //    newEle.IsPublish = true;
        //    newEle.IsModified = true;
        //    newEle.IsTitleModify = true;
        //    newEle.SetAttribute("type", type.ToString());
        //    newEle.CreatedTime = DateTime.Now;
        //    newEle.IsExclude = true;
        //    ///添加到父节点
        //    parentFolder.IsModified = true;
        //    parentFolder.AppendChild(newEle);

        //    Save();

        //    return newEle;
        //}

        #endregion

        #region GetDocumentById通过Id来获取相应的Document对象

        private IndexXmlDocument GetDocumentById(string id)
        {
            IndexXmlElement ele = (IndexXmlElement)this.GetElementById(id);

            ///检查返回的Element是否实现了IGetDocumentable
            if (ele is IGetDocumentable)
            {
                return ((IGetDocumentable)ele).GetIndexXmlDocument();
            }

            return null;
        }

        public PageXmlDocument GetPageDocumentById(string pageId)
        {
            return (PageXmlDocument)GetDocumentById(pageId);
        }

        public TmpltXmlDocument GetTmpltDocumentById(string tmpltId)
        {
            return (TmpltXmlDocument)GetDocumentById(tmpltId);
        }

        #endregion

        #region GetElementById：通过Id来获取相应的Element对象

        public PageSimpleExXmlElement GetPageElementById(string id)
        {
            return GetElementById(id, "page") as PageSimpleExXmlElement;
        }

        public TmpltSimpleExXmlElement GetTmpltElementById(string id)
        {
            return GetElementById(id, "tmplt") as TmpltSimpleExXmlElement;
        }

        public ChannelSimpleExXmlElement GetChannelElementById(string channelID)
        {
            return GetElementById(channelID, "channel") as ChannelSimpleExXmlElement;
        }

        /// <summary>
        /// 通过Id获取文件夹的XmlElement。(包括频道)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FolderXmlElement GetFolderElementById(string id)
        {
            return GetElementById(id) as FolderXmlElement;
        }

        #endregion

        #region 删除方法
        public bool DeleteItem(SimpleExIndexXmlElement ele)
        {
            if (ele.IsInDocument)
            {
                if (ele.IsAlreadyPublished)
                {
                    ele.IsDeleted = true;
                    ele.OwnerFolderElement.IsModified = true;
                }
                else
                {
                    bool result = RemoveNodeAndFile(ele);
                    if (!result)
                    {
                        return false;
                    }
                }

                Save();

                OnElementDeleted(new EventArgs<SimpleExIndexXmlElement>(ele));
            }
            return true;
        }
        public void DeleteItem(string id)
        {
            DeleteItem(GetElementById(id) as SimpleExIndexXmlElement);
        }

        //public void DeleteItemFinal(SimpleExIndexXmlElement ele)
        //{
        //    RemoveNodeAndFile(ele);
        //    Save();
        //}
        //public void DeleteItemFinal(string id)
        //{
        //    DeleteItemFinal(GetElementById(id) as SimpleExIndexXmlElement);
        //}

        /// <summary>
        /// 用来真实的删除节点和文件。
        /// (若是此节点未发布过，则QuiteDeleteItem应该调用此方法。否则由DeleteItemFinal方法调用。)
        /// </summary>
        private bool RemoveNodeAndFile(SimpleExIndexXmlElement ele)
        {
            ///删除文件
            try
            {
                Utility.File.DeleteFileOrDirectory(ele.AbsoluteFilePath);
            }
            catch (IOException)
            {
                string msg = string.Format("文件“{0}”删除失败！",ele.Title);
                MessageService.Show(msg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            ///删除Xml里的节点
            ele.ParentNode.RemoveChild(ele);

            this.Save();
            return true;
        }
        #endregion

        #region MyRegion

        /// <summary>
        /// 设置节点为修改后的状态
        /// </summary>
        public void MarkModified(SimpleExIndexXmlElement ele)
        {
            Debug.Assert(ele != null);

            ///设置为已修改，同时将修改时间设置为当前时间，并将版本递增
            ele.IsModified = true;
            ele.ModifiedTime = DateTime.Now;
            ele.Version++;
        }
        public void MarkModified(string id)
        {
            MarkModified(GetElementById(id) as SimpleExIndexXmlElement);
        }

        /// <summary>
        /// 设置节点为新建后的状态
        /// </summary>
        public void MarkCreated(SimpleExIndexXmlElement ele)
        {
            ele.Version = 0;
            ele.IsModified = true;
            ele.IsAlreadyPublished = false;
            ele.IsPublish = true;
            ele.CreatedTime = DateTime.Now;
        }
        public void MarkCreated(string id)
        {
            MarkCreated(GetElementById(id) as SimpleExIndexXmlElement);
        }

        /// <summary>
        /// 设置节点为已经发布后的状态
        /// </summary>
        public void MarkAlreadyPublished(SimpleExIndexXmlElement ele)
        {
            ele.IsAlreadyPublished = true;
            ele.PublishedTime = DateTime.Now;
            ele.IsModified = false;
            if (!string.IsNullOrEmpty(ele.OldParentFolderId))
            {
                ele.Attributes.Remove(ele.Attributes["oldParentFolderId"]);
            }
            //将移动设置为初始值
            if (ele.IsMoved == true)
            {
                ele.IsMoved = false;
                ele.Attributes.Remove(ele.Attributes["MoveSourcePlace"]);
            }
            this.Save();
        }
        public void MarkAlreadyPublished(string id)
        {
            MarkAlreadyPublished(GetElementById(id) as SimpleExIndexXmlElement);
        }

        /// <summary>
        /// 当发送移动命令后对节点的处理
        /// </summary>
        public void MarkAlreadyPublishedMove(SimpleExIndexXmlElement ele)
        {
            //是否被多次移动
            if (!string.IsNullOrEmpty(ele.OldParentFolderId))
            {
                ele.Attributes.Remove(ele.Attributes["oldParentFolderId"]);
            }
            //移出isMove,移出MoveSourcePlace
            ele.IsMoved = false;
        }
        public void MarkAlreadyPublishedMove(string id)
        {
            MarkAlreadyPublishedMove(GetElementById(id) as SimpleExIndexXmlElement);
        }

        /// <summary>
        /// 将此节点设置成停止发布
        /// </summary>
        public void StopPublish(SimpleExIndexXmlElement ele)
        {
            ele.IsPublish = false;
            ele.StopPublishTime = DateTime.Now;

            MarkModified(ele);
        }
        public void StopPublish(string id)
        {
            StopPublish(GetElementById(id) as SimpleExIndexXmlElement);
        }

        public void AddFavorite(SimpleExIndexXmlElement ele)
        {
            ele.IsFavorite = true;

            OnElementAddedFavorite(new EventArgs<SimpleExIndexXmlElement>(ele));
        }
        public void AddFavorite(string id)
        {
            AddFavorite(this.GetElementById(id) as SimpleExIndexXmlElement);
        }

        public void RemoveFavorite(SimpleExIndexXmlElement ele)
        {
            ele.IsFavorite = false;

            Save();

            OnElementRemovedFavorite(new EventArgs<SimpleExIndexXmlElement>(ele));
        }
        public void RemoveFavorite(string id)
        {
            SimpleExIndexXmlElement ele = this.GetElementById(id) as SimpleExIndexXmlElement;
            RemoveFavorite(ele);
        }

        public void ChangeTitle(SimpleExIndexXmlElement ele, string newTitle)
        {
            string oldTitle = ele.Title;
            if (oldTitle == newTitle)
            {
                return;
            }

            ///设置Element的title
            ele.Title = newTitle;

            Save();

            OnElementTitleChanged(new ChangeTitleEventArgs(ele, oldTitle, newTitle));
        }
        public void ChangeTitle(string id, string newTitle)
        {
            ChangeTitle(GetElementById(id) as SimpleExIndexXmlElement, newTitle);
        }

        #endregion

        #region 一些static静态成员

        //static public bool IsProjectOpened
        //{
        //    get { return CurrentDocument != null; }
        //}

        //public static SdsiteXmlDocument CurrentDocument { get; internal set; }

        public static SdsiteXmlDocument GetCurrentDocument()
        {
            return null;
        }

        #endregion

        #region 自定义事件

        /// <summary>
        /// .sdsite中的SimpleExIndexXmlElement节点的添加，而这个节点的添加往往也意味着一个
        /// 文件的添加(这不是一定的，channel就只是对应文件夹)
        /// </summary>
        public event EventHandler<EventArgs<SimpleExIndexXmlElement>> ElementAdded;
        protected virtual void OnElementAdded(EventArgs<SimpleExIndexXmlElement> e)
        {
            if (ElementAdded != null)
            {
                ElementAdded(this, e);
            }
        }

        public event EventHandler<EventArgs<SimpleExIndexXmlElement>> ElementMoved;
        protected virtual void OnElementMoved(EventArgs<SimpleExIndexXmlElement> e)
        {
            if (ElementMoved != null)
            {
                ElementMoved(this, e);
            }
        }

        public event EventHandler<EventArgs<SimpleExIndexXmlElement>> ElementDeleted;
        protected virtual void OnElementDeleted(EventArgs<SimpleExIndexXmlElement> e)
        {
            if (ElementDeleted != null)
            {
                ElementDeleted(this, e);
            }
        }

        public event EventHandler<ChangeTitleEventArgs> ElementTitleChanged;
        protected virtual void OnElementTitleChanged(ChangeTitleEventArgs e)
        {
            if (ElementTitleChanged != null)
            {
                ElementTitleChanged(this, e);
            }
        }

        public event EventHandler<EventArgs<SimpleExIndexXmlElement>> ElementAddedFavorite;
        protected virtual void OnElementAddedFavorite(EventArgs<SimpleExIndexXmlElement> e)
        {
            if (ElementAddedFavorite != null)
            {
                ElementAddedFavorite(this, e);
            }
        }

        public event EventHandler<EventArgs<SimpleExIndexXmlElement>> ElementRemovedFavorite;
        protected virtual void OnElementRemovedFavorite(EventArgs<SimpleExIndexXmlElement> e)
        {
            if (ElementRemovedFavorite != null)
            {
                ElementRemovedFavorite(this, e);
            }
        }

        public event EventHandler<EventArgs<SimpleExIndexXmlElement>> ElementExclude;
        protected virtual void OnElementExclude(EventArgs<SimpleExIndexXmlElement> e)
        {
            if (ElementExclude != null)
            {
                ElementExclude(this, e);
            }
        }

        public event EventHandler<EventArgs<SimpleExIndexXmlElement>> ElementInclude;
        protected virtual void OnElementInclude(EventArgs<SimpleExIndexXmlElement> e)
        {
            if (ElementInclude != null)
            {
                ElementInclude(this, e);
            }
        }

        #region 页面片和PART的定位
        /// <summary>
        /// 定位PART,即在模板视图中双击要定位到PART
        /// </summary>
        static public event EventHandler<EventArgs<string[]>> OrientationPart;
        static public void OnOrientationPart(EventArgs<string[]> e)
        {
            if (OrientationPart != null)
            {
                OrientationPart(null, e);
            }
        }

        /// <summary>
        /// 页面片重命名后,通知更新页面设计器的TEXT内容 add by fenggy 2008-06-17
        /// </summary>
        static public event EventHandler<EventArgs<string[]>> SnipDesignerFormTextChange;
        static public void OnSnipDesignerFormTextChange(EventArgs<string[]> e)
        {
            if (SnipDesignerFormTextChange != null)
            {
                SnipDesignerFormTextChange(null, e);
            }
        }
        #endregion

        #endregion

        #region 内部类：ChannelTempXmlElement

        private class ChannelTempXmlElement : AnyXmlElement
        {
            public ChannelTempXmlElement(XmlDocument doc)
                : base("channel", doc)
            { }

            public bool IsRootChannel
            {
                get
                {
                    return this.GetAttribute("id") == Utility.Const.ChannelRootId;
                }
            }
        }

        #endregion

        #region 设置当前产品属性组ID
        public string SettingCurrentPropGroup
        {
            get
            {
                XmlNode node = this.SelectSingleNode("//productPropGroupId");
                if (node != null)
                    return node.InnerText;
                return "";
            }
            set
            {
                XmlNode node = this.SelectSingleNode("//productPropGroupId");
                node.InnerText = value;
            }
        }

        #endregion

        public string SdsiteName
        {
            get { return Path.GetFileNameWithoutExtension(AbsoluteFilePath); }
        }

        /// <summary>
        /// 获得所有的模板文档
        /// </summary>
        /// <returns></returns>
        public string[] GetAllTmpltId()
        {
            XmlNodeList tmplts = this.SelectNodes("//tmplt");
            List<string> list = new List<string>();
            foreach (XmlNode node in tmplts)
            {
                list.Add(node.Attributes["id"].Value);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 获得所有的页面文档
        /// </summary>
        /// <returns></returns>
        public string[] GetAllPageId()
        {
            XmlNodeList pages = this.SelectNodes("//page");
            List<string> list = new List<string>();
            foreach (XmlNode node in pages)
            {
                list.Add(node.Attributes["id"].Value);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 此项目所属的用户
        /// </summary>
        public string OwnerUser
        {
            get
            {
                return this.DocumentElement.GetAttribute("ownerUser");
            }
            set
            {
                this.DocumentElement.SetAttribute("ownerUser",value);
            }
        }

        /// <summary>
        /// 清除所有上传信息，包括OwnerUser属性。
        /// </summary>
        public void ClearPublishInfo()
        {
            this.OwnerUser = "";

            ClearPublishInfoCore(this.RootChannel);

            this.Save();
        }

        private void ClearPublishInfoCore(SimpleExIndexXmlElement simpleEle)
        {
            simpleEle.IsAlreadyPublished = false;
            simpleEle.IsModified = true;

            ///若是模板，则将模板里的所有页面片重置IsModified为true
            if (simpleEle.DataType == DataType.Tmplt)
            {
                TmpltSimpleExXmlElement tmpltEle = (TmpltSimpleExXmlElement)simpleEle;
                TmpltXmlDocument tmpltDoc = tmpltEle.GetIndexXmlDocument();
                tmpltDoc.SetAllSnipsIsModified(true);
            }
            else
            {
                ///递归清除所有子节点的上传信息。
                foreach (XmlNode node in simpleEle.ChildNodes)
                {
                    if (node is SimpleExIndexXmlElement)
                    {
                        ClearPublishInfoCore((SimpleExIndexXmlElement)node);
                    }
                }
            }
        }

        /// <summary>
        /// 复制模板
        /// </summary>
        public TmpltSimpleExXmlElement CopyTmplt(string fromTmpltID, string toChannelID, TmpltType type)
        {
            TmpltSimpleExXmlElement oldTmpltEle = this.GetTmpltElementById(fromTmpltID);
            string newId = Utility.Guid.NewGuid();
            TmpltSimpleExXmlElement newEle = CreateNewTmpltElement(newId, oldTmpltEle.FileName, type);

            ///添加到父节点
            XmlNode parentFolder = this.GetElementById(toChannelID);
            parentFolder.AppendChild(newEle);

            ///添加XmlDocument文件
            TmpltXmlDocument newDoc = TmpltXmlDocument.CreateInstance(newEle.RelativeFilePath, newEle.Id, newEle);
            string strXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?><tmplt id=""{0}"" title=""{1}""><lines></lines><rects></rects></tmplt>";
            strXml = string.Format(strXml, newId, oldTmpltEle.Title);
            newDoc.LoadXml(strXml);
            newDoc.HasAutoKeyWordsSequenceType = false;
            newDoc.DocumentElement.SetAttribute("type", type.ToString());

            newDoc.Save();

            Save();

            ///触发事件
            OnElementAdded(new EventArgs<SimpleExIndexXmlElement>(newEle));

            return newEle;
        }

        /// <summary>
        /// 剪切节点
        /// </summary>
        public SimpleExIndexXmlElement CutNode(string cutNodeID, string toFolderID)
        {
            SimpleExIndexXmlElement cutEle = this.GetElementById(cutNodeID) as SimpleExIndexXmlElement;
            SimpleExIndexXmlElement toEle = this.GetElementById(toFolderID) as SimpleExIndexXmlElement;
            string newFileName = cutEle.FileName;

            ////5.21:caojun：构建新的不重复的名称
            if (string.IsNullOrEmpty(cutEle.FileExtension))
                newFileName = Utility.File.BuildFileName(toEle.AbsoluteFilePath, cutEle.FileName, true, false);//文件夹
            else
                newFileName = Utility.File.BuildFileName(toEle.AbsoluteFilePath, cutEle.FileName + cutEle.FileExtension, false, false);//文件
            if (cutEle is FileSimpleExXmlElement)
            {
                newFileName = Utility.File.BuildFileName(toEle.AbsoluteFilePath, cutEle.FileName, false, true);//资源文件
            }
            //if (XmlUtilService.ExistsSameFileName(toEle as FolderXmlElement, cutEle.DataType, cutEle.FileName))
            //{
            //    //todo:
            //    //BaseCutAndCopyForm CCForm = new BaseCutAndCopyForm();
            //    //CCForm.newNameTextBox.Text = cutEle.Title;
            //    //bool SameTitle = true;
            //    //while (SameTitle)
            //    //{
            //    //    if (CCForm.ShowDialog() == DialogResult.OK)
            //    //    {
            //    //        if (!XmlUtilService.ExistsSameTitle(toEle as FolderXmlElement, titleForType, CCForm.NewName))
            //    //        {
            //    //            SameTitle = false;
            //    //            newFileName = CCForm.NewName;
            //    //            cutEle.Title = newFileName;
            //    //        }
            //    //    }
            //    //    else
            //    //        return null;
            //    //}
            //    MessageService.Show("${res:Tree.msg.isExistsSameFile}");
            //    return null;
            //}
            string oldPath = cutEle.AbsoluteFilePath;

            ///如果已经发布,且OldParentFolder未被设置过,那么设置OldParentFolder
            if (cutEle.IsAlreadyPublished &&
                string.IsNullOrEmpty(cutEle.OldParentFolderId))
            {
                cutEle.OldParentFolderId = cutEle.OwnerFolderElement.Id;
                cutEle.OwnerFolderElement.IsModified = true;
            }

            ///删除原来位置的节点
            cutEle.FileName = newFileName;
            cutEle.ParentNode.RemoveChild(cutEle);

            ///添加到新位置

            toEle.AppendChild(cutEle);

            ///移动物理文件
            string newpath = ((SimpleExIndexXmlElement)toEle).AbsoluteFilePath + Utility.File.GetFileOrDirectoryName(((SimpleExIndexXmlElement)cutEle).AbsoluteFilePath).Substring(1);
            if (cutEle is FolderXmlElement)
            {
                Directory.Move(oldPath, newpath);
            }
            else
            {
                File.Move(oldPath, newpath);
                IndexXmlDocument dataDoc = this.GetDocumentById(cutEle.Id);
                dataDoc.DocumentElement.SetAttribute("fileName", newFileName);
                dataDoc.DocumentElement.SetAttribute("title", newFileName);
                dataDoc.Save();
            }

            ///保存Sdsite
            Save();

            OnElementMoved(new EventArgs<SimpleExIndexXmlElement>(cutEle));
            return cutEle;
        }

        /// <summary>
        /// 移动节点
        /// </summary>
        public bool MoveNode(SimpleExIndexXmlElement srcEle, SimpleExIndexXmlElement targetEle, DragDropResultType dropResultType)
        {
            string oldPath = srcEle.AbsoluteFilePath;
            string oldParentFolderId = srcEle.OwnerFolderElement.Id;

            ///找到父节点
            FolderXmlElement parentEle = null;
            if (dropResultType == DragDropResultType.Into)
            {
                parentEle = (FolderXmlElement)targetEle;
            }
            else
            {
                parentEle = targetEle.OwnerFolderElement;
            }

            string newFileName = "";
            if (string.IsNullOrEmpty(srcEle.FileExtension))
                newFileName = Utility.File.BuildFileName(parentEle.AbsoluteFilePath, srcEle.FileName, true, false);//文件夹
            else
                newFileName = Utility.File.BuildFileName(parentEle.AbsoluteFilePath, srcEle.FileName + srcEle.FileExtension, false, false);//文件
            if (srcEle is FileSimpleExXmlElement)
            {
                string parentPath = "";
                switch (dropResultType)
                {
                    case DragDropResultType.Before:
                    case DragDropResultType.After:
                        {
                            parentPath = ((FolderXmlElement)targetEle.ParentNode).AbsoluteFilePath;
                            break;
                        }
                    case DragDropResultType.Into:
                        {
                            parentPath = targetEle.AbsoluteFilePath;
                            break;
                        }
                }
                newFileName = Utility.File.BuildFileName(parentPath, srcEle.FileName, false, true);//文件
            }

            srcEle.FileName = newFileName;
            ////移动到了不同的文件夹，则判断有否同名
            //if (parentEle != targetEle.OwnerFolderElement)
            //{
            //    //判断是否同名
            //    if (XmlUtilService.ExistsSameFileName(parentEle, srcEle.DataType, srcEle.FileName))
            //    {
            //        //MessageService.Show("${res:Tree.msg.isExistsSameFile}");
            //        string newFileName="";
            //        if (string.IsNullOrEmpty(srcEle.FileExtension))
            //            newFileName = Utility.File.BuildFileName(parentEle.AbsoluteFilePath, srcEle.FileName, true, false);//文件夹
            //        else
            //            newFileName = Utility.File.BuildFileName(parentEle.AbsoluteFilePath, srcEle.FileName+srcEle.FileExtension, false, false);//文件
                   
            //        srcEle.FileName = newFileName;
            //       // return false;
            //    }
            //}

            ///删除原来位置的节点
            srcEle.ParentNode.RemoveChild(srcEle);

            ///添加到新位置
            switch (dropResultType)
            {
                case DragDropResultType.Before:
                    targetEle.ParentNode.InsertBefore(srcEle, targetEle);
                    break;
                case DragDropResultType.After:
                    targetEle.ParentNode.InsertAfter(srcEle, targetEle);
                    break;
                case DragDropResultType.Into:
                    targetEle.AppendChild(srcEle);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            ///移动物理文件
            string newPath = srcEle.AbsoluteFilePath;
            if (oldPath != newPath)
            {
                if (srcEle is FolderXmlElement)
                {
                    if (Directory.Exists(oldPath))
                    {
                        Directory.Move(oldPath, newPath);
                    }
                }
                else
                {
                    if (File.Exists(oldPath))
                    {
                        File.Move(oldPath, newPath);
                    }
                }
            }

            //如果移动后的父节点和原父节点不一样,则设置OldParentFolder
            if (oldParentFolderId != srcEle.OwnerFolderElement.Id)
            {
                ///如果已经发布,且OldParentFolder未被设置过,那么设置OldParentFolder
                if (srcEle.IsAlreadyPublished &&
                    string.IsNullOrEmpty(srcEle.OldParentFolderId))
                {
                    srcEle.OldParentFolderId = oldParentFolderId;
                    srcEle.OwnerFolderElement.IsModified = true;
                }
            }

            ///保存Sdsite
            Save();

            return true;
        }

        /// <summary>
        /// 复制节点
        /// </summary>
        public SimpleExIndexXmlElement CopyNode(string copyNodeID, string toFolderID)
        {
            SimpleExIndexXmlElement copyEle = this.GetElementById(copyNodeID) as SimpleExIndexXmlElement;
            SimpleExIndexXmlElement toEle = this.GetElementById(toFolderID) as SimpleExIndexXmlElement;
            SimpleExIndexXmlElement newEle = copyEle.Clone() as SimpleExIndexXmlElement;
            string oldPath = copyEle.AbsoluteFilePath;
            string newFileName = copyEle.FileName;
            //5.21:caojun：构建新的不重复的名称
            if (string.IsNullOrEmpty(copyEle.FileExtension))
                newFileName = Utility.File.BuildFileName(toEle.AbsoluteFilePath, copyEle.FileName, true, false);//文件夹
            else
                newFileName = Utility.File.BuildFileName(toEle.AbsoluteFilePath, copyEle.FileName + copyEle.FileExtension, false, false);//文件
            if (copyEle is FileSimpleExXmlElement)
            {
                newFileName = Utility.File.BuildFileName(toEle.AbsoluteFilePath, copyEle.FileName, false, true);//文件
            
            }
            string newID = Guid.NewGuid().ToString("N");
            newEle.Id = newID;
            newEle.FileName = newFileName;
            newEle.IsPublish = true;
            newEle.IsAlreadyPublished = false;
            toEle.AppendChild(newEle);

            string newpath = Path.Combine(toEle.AbsoluteFilePath, Utility.File.GetFileOrDirectoryName(newEle.AbsoluteFilePath).Substring(1));
            if (copyEle is FolderXmlElement)
                //CopyDirectory(oldPath, newpath);
                CopyDirectory(copyEle,newEle);
            else
            {
                File.Copy(oldPath, newpath);
                IndexXmlDocument dataDoc = this.GetDocumentById(newID);
                if (!(copyEle is FileSimpleExXmlElement))
                {
                    dataDoc.Id = newID;
                    dataDoc.DocumentElement.SetAttribute("fileName", newFileName);
                    dataDoc.DocumentElement.SetAttribute("title", newFileName);
                    dataDoc.Save();
                }
            }
            Save();

            OnElementAdded(new EventArgs<SimpleExIndexXmlElement>(newEle));
            return newEle;

        }

        /// <summary>
        /// 文件夹复制
        /// </summary>
        /// <param name="sourceDirName">原始路径</param>
        /// <param name="destDirName">目标路径</param>
        /// <returns></returns>
        private void CopyDirectory(string sour, string des)
        {
            Directory.CreateDirectory(des);

            if (!Directory.Exists(sour)) return;

            string[] directories = Directory.GetDirectories(sour);

            if (directories.Length > 0)
            {
                foreach (string d in directories)
                {
                    CopyDirectory(d, des + d.Substring(d.LastIndexOf("\\")));
                }
            }


            string[] files = Directory.GetFiles(sour);

            if (files.Length > 0)
            {
                foreach (string s in files)
                {
                    File.Copy(s, des + s.Substring(s.LastIndexOf("\\")));
                }
            }

        }

        private void CopyDirectory(SimpleExIndexXmlElement copyEle, SimpleExIndexXmlElement toEle)
        {
            Directory.CreateDirectory(toEle.AbsoluteFilePath);

            if (copyEle==null) return;

            XmlNodeList fromNodeList = copyEle.ChildNodes;
            XmlNodeList toNodeList = toEle.ChildNodes;

            if (fromNodeList.Count > 0)
            {
                for (int i = 0; i < fromNodeList.Count; i++)
                {
                    XmlElement fromEle = fromNodeList[i] as XmlElement;
                    if (fromEle is FolderXmlElement)
                    {
                        string id = ((FolderXmlElement)copyEle).Id;
                        FolderXmlElement toFloderEle = toNodeList[i] as FolderXmlElement;
                        if (toFloderEle == null) continue;
                        toFloderEle.Id = Guid.NewGuid().ToString("N");
                        CopyDirectory(fromEle as SimpleExIndexXmlElement, toFloderEle as SimpleExIndexXmlElement);
                    }
                    else
                    {
                        string id = ((FolderXmlElement)copyEle).Id;
                        SimpleExIndexXmlElement toFileEle = toNodeList[i] as SimpleExIndexXmlElement;
                        toFileEle.Id = Guid.NewGuid().ToString("N");
                        File.Copy(((SimpleExIndexXmlElement)fromEle).AbsoluteFilePath, toFileEle.AbsoluteFilePath);

                    }
                }
            }

        }

        public void CopyTmpltToOtherType(string copyNodeID, string toFolderID, TmpltType tmpltType, string tmpltFileName)
        {
            TmpltSimpleExXmlElement copyEle = this.GetElementById(copyNodeID) as TmpltSimpleExXmlElement;
            FolderXmlElement toEle = this.GetElementById(toFolderID) as FolderXmlElement;
            TmpltSimpleExXmlElement newEle = copyEle.Clone() as TmpltSimpleExXmlElement;

            if (XmlUtilService.ExistsSameFileName(toEle, DataType.Tmplt, tmpltFileName))
            {
                MessageService.Show("${res:Tree.msg.isExistsSameFile}");
                return;
            }

            string newID = Guid.NewGuid().ToString("N");
            newEle.Id = newID;
            newEle.TmpltType = tmpltType;
            newEle.FileName = tmpltFileName;
            newEle.IsPublish = true;
            newEle.IsAlreadyPublished = false;
            toEle.AppendChild(newEle);

            string oldPath = copyEle.AbsoluteFilePath;
            string newpath = Path.Combine(toEle.AbsoluteFilePath, Utility.File.GetFileOrDirectoryName(newEle.AbsoluteFilePath).Substring(1));
            File.Copy(oldPath, newpath);
            TmpltXmlDocument dataDoc = this.GetTmpltDocumentById(newID);
            dataDoc.Id = newID;
            dataDoc.Title = tmpltFileName;
            dataDoc.TmpltType = tmpltType;
            dataDoc.ResetAllProperty(copyEle.TmpltType != tmpltType , tmpltType == TmpltType.Home);
            dataDoc.Save();
            Save();

            OnElementAdded(new EventArgs<SimpleExIndexXmlElement>(newEle));
        }

        public XmlElement GetCssFontElement()
        {
            XmlNode optionNode = this.DocumentElement.SelectSingleNode("option");
            XmlNode cssFontNode = optionNode.SelectSingleNode("cssFont");
            if (cssFontNode == null)
            {
                cssFontNode = CreateElement("cssFont");
                optionNode.AppendChild(cssFontNode);
            }
            return (XmlElement)cssFontNode;
        }

        #region 排除,包含子项目
        /// <summary>
        /// 排除子项目
        /// </summary>
        /// <param name="id"></param>
        public void IncludeItem(string id)
        {
            SimpleExIndexXmlElement ele = this.GetElementById(id) as SimpleExIndexXmlElement;

            if (ele != null)
                ele.IsExclude = false;
            else //没有对应的索引时
            {
                //todo:没有对应的索引时,创建对应的索引
            }

            ele.OwnerFolderElement.IsModified = true;
            Save();
            OnElementInclude(new EventArgs<SimpleExIndexXmlElement>(ele));
        }

        /// <summary>
        /// 排除子项目
        /// </summary>
        /// <param name="id"></param>
        public void ExcludeItem(string id)
        {
            SimpleExIndexXmlElement ele = this.GetElementById(id) as SimpleExIndexXmlElement;
            ele.IsExclude = true;
            ele.OwnerFolderElement.IsModified = true;

            Save();
            OnElementExclude(new EventArgs<SimpleExIndexXmlElement>(ele));
        }
        #endregion

        /// <summary>
        /// 根据绝对路径取得ID
        /// </summary>
        /// <param name="p"></param>
        public void GetIdByAbsoluteFilePath(string absPath)
        {
           //string relativePath
           /* List<string> pathList = new List<string>();
            string path = Path.GetFileName(absPath);
            while (!string.IsNullOrEmpty(path) && path!="Resources")
            {
                pathList.Add(path);

                absPath = absPath.Substring(0, absPath.Length - path.Length-1);
                path = Path.GetFileName(absPath);
            }

            string[] pathArr = pathList.ToArray();
            for (int i = pathArr.Length - 1; i > -1; i--)
            {
 this.Resources
            }*/
        }

        #region CustomeId相关的

        /// <summary>
        /// 仅用key存储CustomId的集合
        /// </summary>
        Dictionary<string, SimpleExIndexXmlElement> _dicCustomIds = new Dictionary<string, SimpleExIndexXmlElement>();

        /// <summary>
        /// 创建不重复的CustomId
        /// </summary>
        public string CreateCustomId(IdType type)
        {
            string id = null;
            string profix = null;
            switch (type)
            {
                case IdType.File:
                    profix = "file";
                    break;
                case IdType.Page:
                    profix = "page";
                    break;
                case IdType.Tmplt:
                    profix = "tmplt";
                    break;
                case IdType.Channel:
                    profix = "channel";
                    break;
                case IdType.Folder:
                    profix = "folder";
                    break;
                default:
                    Debug.Fail("未处理的IdType:" + type);
                    break;
            }

            for (long i = 1; i < long.MaxValue; i++)
            {
                ///临时组成的CustomId
                string tempCustomId = profix + i;

                ///检查此CustomId是否已经存在
                if (!ExistsCustomId(tempCustomId))
                {
                    ///不存在此id，则使用此id
                    id = tempCustomId;
                    break;
                }
            }

            return id;
        }

        /// <summary>
        /// 检查指定CustomId是否已经存在
        /// </summary>
        public bool ExistsCustomId(string customId)
        {
            return _dicCustomIds.ContainsKey(customId);
        }

        #endregion

        //add by zhangling on 2008年7月2日
        #region 服务器生成时需要的返回值
        /// <summary>
        /// 此频道是否被作为tmplt子节点
        /// </summary>
        /// <param name="id">channelofsnip</param>
        /// <returns></returns>
        public bool isExistChannelofsnip(string channelId)
        {
            XmlNode node = this.DocumentElement.SelectSingleNode(string.Format("//channelofsnip[@id='{0}']", channelId));
            if (node != null )
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// //查找拥有此页面直属的频道节点(channelofsnip)的所有的模板节点,返回模板节点ID集合
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public string[] GetTmpltIdArray(string channelId)
        {
            List<string> list = new List<string>();
            list.Clear();
            XmlNodeList nodeList = this.DocumentElement.SelectNodes(string.Format("//tmplt/channelofsnip[@id='{0}']", channelId));
            foreach (XmlNode node in nodeList)
            {
                XmlNode tmpltNode = node.ParentNode;
                string id = ((XmlElement)tmpltNode).GetAttribute("id");
                list.Add(id);
            }

            return list.ToArray();
        }
        /// <summary>
        /// 返回当前频道下面的指定的页面类型的页面元素
        /// </summary>
        /// <param name="channelIdArray"></param>
        /// <returns></returns>
        public string[] GetPageIdArray(string[] channelIdArray,List<string> pageType)
        { 

            return new string [0];
        }
        #endregion
    }

    public class ChangeTitleEventArgs : EventArgs<SimpleExIndexXmlElement>
    {
        public string OldTitle { get; private set; }

        public string NewTitle { get; private set; }

        public ChangeTitleEventArgs(SimpleExIndexXmlElement ele, string oldTitle, string newTitle)
            : base(ele)
        {
            OldTitle = oldTitle;
            NewTitle = newTitle;
        }
    }
}

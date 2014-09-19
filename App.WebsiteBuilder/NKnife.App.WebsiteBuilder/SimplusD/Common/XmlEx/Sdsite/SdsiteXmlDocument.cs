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
    /// [ ��Ŀ�ļ�.sdsite ] ��Ӧ��XmlDocument��
    /// [ ��Ŀ�ļ�.sdsite ] ����Ŀ����Ҫ���ļ���
    /// �洢���������ļ�����������ǰ��վ��Ŀ��ѡ���վ����(siteData)���Լ��ϴ���Ϣ��
    /// �Կ�����Ա��˵����ǰ���ǵ�ǰ���򼯵���ڡ�
    /// </summary>
    public class SdsiteXmlDocument : AnyXmlDocument
    {
        public SdsiteXmlDocument(string absoluteFilePath)
            : base(absoluteFilePath)
        {
            this.PreserveWhitespace = false;
        }

        #region һЩ��������ʹ�õ�����

        public ResourcesXmlElement Resources { get; private set; }

        public TmpltFolderXmlElement TmpltFolder { get; private set; }

        public SitePropertyXmlElement SiteProperty { get; private set; }

        public RootChannelXmlElement RootChannel { get; private set; }

        public HelperDataXmlElement HelperData { get; private set; }

        #endregion

        #region override��д����ķ���

        //private string _relativeFilePath;
        //public override string RelativeFilePath
        //{
        //    get
        //    {
        //        return _relativeFilePath;
        //    }
        //}

        /// <summary>
        /// ����д����ָ����XmlReader����XML�ĵ���
        /// (XmlDocument���е�Load���������յ��ô˷��������Ժ�汾��.NET�иı䣬��˴���Ҫ�ı�)
        /// </summary>
        public override void Load(XmlReader reader)
        {
            base.Load(reader);

            ///����channel�ڵ�
            ProcessChannelTemp(this.DocumentElement);

            ///��������������ʹ�õ���Ҫ���Ը�ֵ
            SiteProperty = (SitePropertyXmlElement)this.GetElementByName("siteProperty");
            RootChannel = (RootChannelXmlElement)this.GetElementByName("channel");
            Resources = (ResourcesXmlElement)this.GetElementByName("resources");
            TmpltFolder = (TmpltFolderXmlElement)this.GetElementByName("tmpltRootFolder");
            HelperData = (HelperDataXmlElement)this.GetElementByName("helperData");
        }

        /// <summary>
        /// (�ݹ�)����channel�ڵ㣬��������type��ChannelTempXmlElement�滻�ɶ�Ӧ������
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

                ///�ݹ����
                ProcessChannelTemp(newChannelEle);
            }
        }

        public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
        {
            XmlElement returnEle = null;
            switch (localName)
            {
                #region ϵͳ�����

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

                #region option�ڵ��µĽڵ�
                case "hrSample":
                    returnEle = new HrSampleXmlElement(this);
                    break;

                case "productPropertyGroup":
                    returnEle = new ProductPropertyGroupXmlElement(this);
                    break;
                #endregion

                #region UGS�ڵ��µĽڵ�

                //case "guestSearch":
                //    returnEle = new GuestSearchElement(this);
                //    break;
                //case "searchContent":
                //    returnEle = new SearchContentElement(this);
                //    break;

                #endregion

                #region ��վ������ؽڵ� zhangling 2008��2��26��19ʱ13��
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

        #region Create��������������Ƶ�����ļ��С�ҳ�桢ģ��

        public ChannelSimpleExXmlElement CreateChannel(FolderXmlElement parentFolder, string title)
        {
            Debug.Assert(parentFolder != null);

            ChannelSimpleExXmlElement newEle = new ChannelSimpleExXmlElement(this);
            string newId = Utility.Guid.NewGuid();

            ///���ļ��������ƴ��������һ�����������ļ���
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

            ///���ļ��������ƴ��������һ�����������ļ���
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
        /// ���ص�һ��Ϊģ��ID,�ڶ���Ϊҳ��ID
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

            ///��newEle����ֵ
            newEle.Id = newId;
            newEle.Title = title;

            ///���ļ��������ƴ��������һ�����������ļ���
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
            ///��ӵ����ڵ�
            parentFolder.IsModified = true;
            parentFolder.AppendChild(newEle);

            ///todo:���XmlDocument�ļ�
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

            ///�����¼�
            OnElementAdded(new EventArgs<SimpleExIndexXmlElement>(newEle));

            return newEle;
        }

        public PageSimpleExXmlElement CreatePage(string parentFolderId, PageType type, string title, string tmlptId)
        {
            return CreatePage(GetFolderElementById(parentFolderId), type, title, tmlptId);
        }

        /// <summary>
        /// ����ģ���ļ�
        /// </summary>
        /// <param name="parentFolder">��ʲô�ط�����</param>
        /// <param name="type">������ʲô����</param>
        /// <returns>����ģ���͵�XML�ڵ�</returns>
        public TmpltSimpleExXmlElement CreateTmplt(string folderId, TmpltType type, string title)
        {
            return CreateTmplt(GetFolderElementById(folderId), type, title, false, 780, 600, null);
        }
        /// <summary>
        /// ����ģ���ļ�
        /// </summary>
        /// <param name="parentFolder">��ʲô�ط�����</param>
        /// <param name="type">������ʲô����</param>
        /// <returns>����ģ���͵�XML�ڵ�</returns>
        public TmpltSimpleExXmlElement CreateTmplt(FolderXmlElement folderEle, TmpltType type, string title, bool hasContentSnip,
            int width, int height, Image backImg)
        {
            string newId = Utility.Guid.NewGuid();

            ///���ļ��������ƴ��������һ�����������ļ���
            string pinyinFileName = Utility.File.FormatFileName(title, false);
            string strFileName = Utility.File.BuildFileName(folderEle.AbsoluteFilePath, pinyinFileName + Utility.Const.TmpltFileExt, false, false);

            TmpltSimpleExXmlElement newEle = CreateNewTmpltElement(newId, strFileName, type);
            newEle.Title = title;

            ///��ӵ����ڵ�
            folderEle.IsModified = true;
            folderEle.AppendChild(newEle);

            ///���XmlDocument�ļ�
            TmpltXmlDocument newDoc = TmpltXmlDocument.CreateInstance(newEle.RelativeFilePath, newEle.Id, newEle);
            string strXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?><tmplt sdVersion=""{2}"" id=""{0}"" title=""{1}""><lines></lines><rects></rects></tmplt>";
            strXml = string.Format(strXml, newId, title, Utility.Version.SoftwareVersion);
            newDoc.LoadXml(strXml);
            newDoc.HasAutoKeyWordsSequenceType = false;
            CreateNewTmpltXmlDocuMent(newDoc, hasContentSnip, width, height, backImg);

            newDoc.DocumentElement.SetAttribute("type", type.ToString());
            newDoc.Save();

            Save();

            ///�����¼�
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
        /// ZhengHao : ����µ�ģ���ļ����½����ļ���
        /// </summary>
        /// <param name="doc">Ҫ�����ļ�����</param>
        /// <param name="hasContentSnip">��ģ���Ƿ�ӵ��������ҳ��Ƭ</param>
        /// <param name="width">ģ����</param>
        /// <param name="height">ģ��߶�</param>
        /// <param name="backImg">ģ��ı���ͼƬ������Ϊnull��</param>
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
            // �����и���ͼƬ
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

            ///��ȡ�����Ρ�XMLԪ�صļ���
            SnipRectXmlElement rectEle = (SnipRectXmlElement)doc.CreateElement("rect");//ԭ�����ﴴ���Ľڵ�����rectDiv��modify by lukan,2008��6��20��12ʱ55��
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

            ///��ʼ��ģ��XML��"rects"�ڵ㣬lukan����ע��
            doc.RectElements = rectEle;

            ///��ȡ���߶Ρ�XMLԪ�صļ���
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
            ///���ļ��������ƴ��������һ�����������ļ���
            string fileName = Path.GetFileName(srcFileName);
            string pinyinFileName = Utility.File.FormatFileName(fileName, false);
            string formatFileName = Utility.File.BuildFileName(parentFolder.AbsoluteFilePath, pinyinFileName, false, true);

            ///�����ļ�
            File.Copy(srcFileName, Path.Combine(parentFolder.AbsoluteFilePath,formatFileName));

            ///����Element
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
            ///����Element
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

        //    ///todo:��newEle����ֵ
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
        //    ///��ӵ����ڵ�
        //    parentFolder.IsModified = true;
        //    parentFolder.AppendChild(newEle);

        //    Save();

        //    return newEle;
        //}

        #endregion

        #region GetDocumentByIdͨ��Id����ȡ��Ӧ��Document����

        private IndexXmlDocument GetDocumentById(string id)
        {
            IndexXmlElement ele = (IndexXmlElement)this.GetElementById(id);

            ///��鷵�ص�Element�Ƿ�ʵ����IGetDocumentable
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

        #region GetElementById��ͨ��Id����ȡ��Ӧ��Element����

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
        /// ͨ��Id��ȡ�ļ��е�XmlElement��(����Ƶ��)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FolderXmlElement GetFolderElementById(string id)
        {
            return GetElementById(id) as FolderXmlElement;
        }

        #endregion

        #region ɾ������
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
        /// ������ʵ��ɾ���ڵ���ļ���
        /// (���Ǵ˽ڵ�δ����������QuiteDeleteItemӦ�õ��ô˷�����������DeleteItemFinal�������á�)
        /// </summary>
        private bool RemoveNodeAndFile(SimpleExIndexXmlElement ele)
        {
            ///ɾ���ļ�
            try
            {
                Utility.File.DeleteFileOrDirectory(ele.AbsoluteFilePath);
            }
            catch (IOException)
            {
                string msg = string.Format("�ļ���{0}��ɾ��ʧ�ܣ�",ele.Title);
                MessageService.Show(msg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            ///ɾ��Xml��Ľڵ�
            ele.ParentNode.RemoveChild(ele);

            this.Save();
            return true;
        }
        #endregion

        #region MyRegion

        /// <summary>
        /// ���ýڵ�Ϊ�޸ĺ��״̬
        /// </summary>
        public void MarkModified(SimpleExIndexXmlElement ele)
        {
            Debug.Assert(ele != null);

            ///����Ϊ���޸ģ�ͬʱ���޸�ʱ������Ϊ��ǰʱ�䣬�����汾����
            ele.IsModified = true;
            ele.ModifiedTime = DateTime.Now;
            ele.Version++;
        }
        public void MarkModified(string id)
        {
            MarkModified(GetElementById(id) as SimpleExIndexXmlElement);
        }

        /// <summary>
        /// ���ýڵ�Ϊ�½����״̬
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
        /// ���ýڵ�Ϊ�Ѿ��������״̬
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
            //���ƶ�����Ϊ��ʼֵ
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
        /// �������ƶ������Խڵ�Ĵ���
        /// </summary>
        public void MarkAlreadyPublishedMove(SimpleExIndexXmlElement ele)
        {
            //�Ƿ񱻶���ƶ�
            if (!string.IsNullOrEmpty(ele.OldParentFolderId))
            {
                ele.Attributes.Remove(ele.Attributes["oldParentFolderId"]);
            }
            //�Ƴ�isMove,�Ƴ�MoveSourcePlace
            ele.IsMoved = false;
        }
        public void MarkAlreadyPublishedMove(string id)
        {
            MarkAlreadyPublishedMove(GetElementById(id) as SimpleExIndexXmlElement);
        }

        /// <summary>
        /// ���˽ڵ����ó�ֹͣ����
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

            ///����Element��title
            ele.Title = newTitle;

            Save();

            OnElementTitleChanged(new ChangeTitleEventArgs(ele, oldTitle, newTitle));
        }
        public void ChangeTitle(string id, string newTitle)
        {
            ChangeTitle(GetElementById(id) as SimpleExIndexXmlElement, newTitle);
        }

        #endregion

        #region һЩstatic��̬��Ա

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

        #region �Զ����¼�

        /// <summary>
        /// .sdsite�е�SimpleExIndexXmlElement�ڵ����ӣ�������ڵ���������Ҳ��ζ��һ��
        /// �ļ������(�ⲻ��һ���ģ�channel��ֻ�Ƕ�Ӧ�ļ���)
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

        #region ҳ��Ƭ��PART�Ķ�λ
        /// <summary>
        /// ��λPART,����ģ����ͼ��˫��Ҫ��λ��PART
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
        /// ҳ��Ƭ��������,֪ͨ����ҳ���������TEXT���� add by fenggy 2008-06-17
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

        #region �ڲ��ࣺChannelTempXmlElement

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

        #region ���õ�ǰ��Ʒ������ID
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
        /// ������е�ģ���ĵ�
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
        /// ������е�ҳ���ĵ�
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
        /// ����Ŀ�������û�
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
        /// ��������ϴ���Ϣ������OwnerUser���ԡ�
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

            ///����ģ�壬��ģ���������ҳ��Ƭ����IsModifiedΪtrue
            if (simpleEle.DataType == DataType.Tmplt)
            {
                TmpltSimpleExXmlElement tmpltEle = (TmpltSimpleExXmlElement)simpleEle;
                TmpltXmlDocument tmpltDoc = tmpltEle.GetIndexXmlDocument();
                tmpltDoc.SetAllSnipsIsModified(true);
            }
            else
            {
                ///�ݹ���������ӽڵ���ϴ���Ϣ��
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
        /// ����ģ��
        /// </summary>
        public TmpltSimpleExXmlElement CopyTmplt(string fromTmpltID, string toChannelID, TmpltType type)
        {
            TmpltSimpleExXmlElement oldTmpltEle = this.GetTmpltElementById(fromTmpltID);
            string newId = Utility.Guid.NewGuid();
            TmpltSimpleExXmlElement newEle = CreateNewTmpltElement(newId, oldTmpltEle.FileName, type);

            ///��ӵ����ڵ�
            XmlNode parentFolder = this.GetElementById(toChannelID);
            parentFolder.AppendChild(newEle);

            ///���XmlDocument�ļ�
            TmpltXmlDocument newDoc = TmpltXmlDocument.CreateInstance(newEle.RelativeFilePath, newEle.Id, newEle);
            string strXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?><tmplt id=""{0}"" title=""{1}""><lines></lines><rects></rects></tmplt>";
            strXml = string.Format(strXml, newId, oldTmpltEle.Title);
            newDoc.LoadXml(strXml);
            newDoc.HasAutoKeyWordsSequenceType = false;
            newDoc.DocumentElement.SetAttribute("type", type.ToString());

            newDoc.Save();

            Save();

            ///�����¼�
            OnElementAdded(new EventArgs<SimpleExIndexXmlElement>(newEle));

            return newEle;
        }

        /// <summary>
        /// ���нڵ�
        /// </summary>
        public SimpleExIndexXmlElement CutNode(string cutNodeID, string toFolderID)
        {
            SimpleExIndexXmlElement cutEle = this.GetElementById(cutNodeID) as SimpleExIndexXmlElement;
            SimpleExIndexXmlElement toEle = this.GetElementById(toFolderID) as SimpleExIndexXmlElement;
            string newFileName = cutEle.FileName;

            ////5.21:caojun�������µĲ��ظ�������
            if (string.IsNullOrEmpty(cutEle.FileExtension))
                newFileName = Utility.File.BuildFileName(toEle.AbsoluteFilePath, cutEle.FileName, true, false);//�ļ���
            else
                newFileName = Utility.File.BuildFileName(toEle.AbsoluteFilePath, cutEle.FileName + cutEle.FileExtension, false, false);//�ļ�
            if (cutEle is FileSimpleExXmlElement)
            {
                newFileName = Utility.File.BuildFileName(toEle.AbsoluteFilePath, cutEle.FileName, false, true);//��Դ�ļ�
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

            ///����Ѿ�����,��OldParentFolderδ�����ù�,��ô����OldParentFolder
            if (cutEle.IsAlreadyPublished &&
                string.IsNullOrEmpty(cutEle.OldParentFolderId))
            {
                cutEle.OldParentFolderId = cutEle.OwnerFolderElement.Id;
                cutEle.OwnerFolderElement.IsModified = true;
            }

            ///ɾ��ԭ��λ�õĽڵ�
            cutEle.FileName = newFileName;
            cutEle.ParentNode.RemoveChild(cutEle);

            ///��ӵ���λ��

            toEle.AppendChild(cutEle);

            ///�ƶ������ļ�
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

            ///����Sdsite
            Save();

            OnElementMoved(new EventArgs<SimpleExIndexXmlElement>(cutEle));
            return cutEle;
        }

        /// <summary>
        /// �ƶ��ڵ�
        /// </summary>
        public bool MoveNode(SimpleExIndexXmlElement srcEle, SimpleExIndexXmlElement targetEle, DragDropResultType dropResultType)
        {
            string oldPath = srcEle.AbsoluteFilePath;
            string oldParentFolderId = srcEle.OwnerFolderElement.Id;

            ///�ҵ����ڵ�
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
                newFileName = Utility.File.BuildFileName(parentEle.AbsoluteFilePath, srcEle.FileName, true, false);//�ļ���
            else
                newFileName = Utility.File.BuildFileName(parentEle.AbsoluteFilePath, srcEle.FileName + srcEle.FileExtension, false, false);//�ļ�
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
                newFileName = Utility.File.BuildFileName(parentPath, srcEle.FileName, false, true);//�ļ�
            }

            srcEle.FileName = newFileName;
            ////�ƶ����˲�ͬ���ļ��У����ж��з�ͬ��
            //if (parentEle != targetEle.OwnerFolderElement)
            //{
            //    //�ж��Ƿ�ͬ��
            //    if (XmlUtilService.ExistsSameFileName(parentEle, srcEle.DataType, srcEle.FileName))
            //    {
            //        //MessageService.Show("${res:Tree.msg.isExistsSameFile}");
            //        string newFileName="";
            //        if (string.IsNullOrEmpty(srcEle.FileExtension))
            //            newFileName = Utility.File.BuildFileName(parentEle.AbsoluteFilePath, srcEle.FileName, true, false);//�ļ���
            //        else
            //            newFileName = Utility.File.BuildFileName(parentEle.AbsoluteFilePath, srcEle.FileName+srcEle.FileExtension, false, false);//�ļ�
                   
            //        srcEle.FileName = newFileName;
            //       // return false;
            //    }
            //}

            ///ɾ��ԭ��λ�õĽڵ�
            srcEle.ParentNode.RemoveChild(srcEle);

            ///��ӵ���λ��
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

            ///�ƶ������ļ�
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

            //����ƶ���ĸ��ڵ��ԭ���ڵ㲻һ��,������OldParentFolder
            if (oldParentFolderId != srcEle.OwnerFolderElement.Id)
            {
                ///����Ѿ�����,��OldParentFolderδ�����ù�,��ô����OldParentFolder
                if (srcEle.IsAlreadyPublished &&
                    string.IsNullOrEmpty(srcEle.OldParentFolderId))
                {
                    srcEle.OldParentFolderId = oldParentFolderId;
                    srcEle.OwnerFolderElement.IsModified = true;
                }
            }

            ///����Sdsite
            Save();

            return true;
        }

        /// <summary>
        /// ���ƽڵ�
        /// </summary>
        public SimpleExIndexXmlElement CopyNode(string copyNodeID, string toFolderID)
        {
            SimpleExIndexXmlElement copyEle = this.GetElementById(copyNodeID) as SimpleExIndexXmlElement;
            SimpleExIndexXmlElement toEle = this.GetElementById(toFolderID) as SimpleExIndexXmlElement;
            SimpleExIndexXmlElement newEle = copyEle.Clone() as SimpleExIndexXmlElement;
            string oldPath = copyEle.AbsoluteFilePath;
            string newFileName = copyEle.FileName;
            //5.21:caojun�������µĲ��ظ�������
            if (string.IsNullOrEmpty(copyEle.FileExtension))
                newFileName = Utility.File.BuildFileName(toEle.AbsoluteFilePath, copyEle.FileName, true, false);//�ļ���
            else
                newFileName = Utility.File.BuildFileName(toEle.AbsoluteFilePath, copyEle.FileName + copyEle.FileExtension, false, false);//�ļ�
            if (copyEle is FileSimpleExXmlElement)
            {
                newFileName = Utility.File.BuildFileName(toEle.AbsoluteFilePath, copyEle.FileName, false, true);//�ļ�
            
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
        /// �ļ��и���
        /// </summary>
        /// <param name="sourceDirName">ԭʼ·��</param>
        /// <param name="destDirName">Ŀ��·��</param>
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

        #region �ų�,��������Ŀ
        /// <summary>
        /// �ų�����Ŀ
        /// </summary>
        /// <param name="id"></param>
        public void IncludeItem(string id)
        {
            SimpleExIndexXmlElement ele = this.GetElementById(id) as SimpleExIndexXmlElement;

            if (ele != null)
                ele.IsExclude = false;
            else //û�ж�Ӧ������ʱ
            {
                //todo:û�ж�Ӧ������ʱ,������Ӧ������
            }

            ele.OwnerFolderElement.IsModified = true;
            Save();
            OnElementInclude(new EventArgs<SimpleExIndexXmlElement>(ele));
        }

        /// <summary>
        /// �ų�����Ŀ
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
        /// ���ݾ���·��ȡ��ID
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

        #region CustomeId��ص�

        /// <summary>
        /// ����key�洢CustomId�ļ���
        /// </summary>
        Dictionary<string, SimpleExIndexXmlElement> _dicCustomIds = new Dictionary<string, SimpleExIndexXmlElement>();

        /// <summary>
        /// �������ظ���CustomId
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
                    Debug.Fail("δ�����IdType:" + type);
                    break;
            }

            for (long i = 1; i < long.MaxValue; i++)
            {
                ///��ʱ��ɵ�CustomId
                string tempCustomId = profix + i;

                ///����CustomId�Ƿ��Ѿ�����
                if (!ExistsCustomId(tempCustomId))
                {
                    ///�����ڴ�id����ʹ�ô�id
                    id = tempCustomId;
                    break;
                }
            }

            return id;
        }

        /// <summary>
        /// ���ָ��CustomId�Ƿ��Ѿ�����
        /// </summary>
        public bool ExistsCustomId(string customId)
        {
            return _dicCustomIds.ContainsKey(customId);
        }

        #endregion

        //add by zhangling on 2008��7��2��
        #region ����������ʱ��Ҫ�ķ���ֵ
        /// <summary>
        /// ��Ƶ���Ƿ���Ϊtmplt�ӽڵ�
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
        /// //����ӵ�д�ҳ��ֱ����Ƶ���ڵ�(channelofsnip)�����е�ģ��ڵ�,����ģ��ڵ�ID����
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
        /// ���ص�ǰƵ�������ָ����ҳ�����͵�ҳ��Ԫ��
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

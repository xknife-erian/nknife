using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using System.IO;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// һ��������������Ǳ�����SdSite�ļ��еľ���ID���Ե�Element��
    /// ��ЩElement����չ����һЩ�����ϴ���ص���Ϣ
    /// </summary>
    abstract public class SimpleExIndexXmlElement : IndexXmlElement
    {
        public SimpleExIndexXmlElement(string localName, XmlDocument doc)
            : base(localName, doc)
        {
        }

        public new SdsiteXmlDocument OwnerDocument
        {
            get
            {
                return (SdsiteXmlDocument)base.OwnerDocument;
            }
        }

        #region ������������ԣ�����ʱ��

        /// <summary>
        /// ��ȡ�����ô���ʱ��
        /// </summary>
        public DateTime CreatedTime
        {
            get { return Utility.Convert.StringToDateTime(this.GetAttribute("createdTime")); }
            set { this.SetAttribute("createdTime", value.ToString(Utility.Const.TimeFormat)); }
        }

        #endregion

        #region �޸ĵ�������ԣ��޸�ʱ�䣬�Ƿ��޸ģ�

        /// <summary>
        /// ��ȡ�������޸�ʱ��
        /// </summary>
        public DateTime ModifiedTime
        {
            get { return Utility.Convert.StringToDateTime(this.GetAttribute("modifiedTime")); }
            internal set { this.SetAttribute("modifiedTime", value.ToString(Utility.Const.TimeFormat)); }
        }

        /// <summary>
        /// ��ȡ�������Ƿ��޸�
        /// </summary>
        public bool IsModified
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isModified")); }
            set { this.SetAttribute("isModified", value.ToString()); }
        }

        #endregion

        #region ������������ԣ��Ƿ�ʼ�շ������Ƿ��Ѿ����������һ�εķ���ʱ�䣬ȡ��������ʱ��

        /// <summary>
        /// ��ȡ�������Ƿ񷢲�
        /// </summary>
        public bool IsPublish
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isPublish")); }
            set { this.SetAttribute("isPublish", value.ToString()); }
        }
        /// <summary>
        /// ��ȡ�������Ƿ��Ѿ�����
        /// </summary>
        public bool IsAlreadyPublished
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isAlreadyPublished")); }
            set { this.SetAttribute("isAlreadyPublished", value.ToString()); }
        }
        /// <summary>
        /// ��ȡ���������һ�εķ���ʱ��
        /// </summary>
        public DateTime PublishedTime
        {
            get { return Utility.Convert.StringToDateTime(this.GetAttribute("publishedTime")); }
            set { this.SetAttribute("publishedTime", value.ToString(Utility.Const.TimeFormat)); }
        }

        /// <summary>
        /// ��ȡ������ֹͣ������ʱ��
        /// </summary>
        public DateTime StopPublishTime
        {
            get { return Utility.Convert.StringToDateTime(this.GetAttribute("stopPublishTime")); }
            set { this.SetAttribute("stopPublishTime", value.ToString(Utility.Const.TimeFormat)); }
        }

        #endregion

        #region �ƹ��������ԣ��ƹ㣺������ǰPage����������棬�ø�����˿����������ݣ�

        /// <summary>
        /// ��ȡ���������Ƿ��ƹ�
        /// </summary>
        public bool IsAd
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isAd")); }
            set { this.SetAttribute("isAd", value.ToString()); }
        }
        /// <summary>
        /// ��ȡ���������Ƿ������ƹ����ʷ�����������Ƿ�ر��ƹ㣩
        /// </summary>
        public bool IsOnceAd
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("IsOnceAd")); }
            set { this.SetAttribute("IsOnceAd", value.ToString()); }
        }
        /// <summary>
        /// ��ȡ�����õ�ǰPage���һ������ΪIsAdΪtrue��ʱ��
        /// </summary>
        public DateTime AdTime
        {
            get { return Utility.Convert.StringToDateTime(this.GetAttribute("adTime")); }
            set { this.SetAttribute("adTime", value.ToString("yyyy-MM-dd HH:dd:ss")); }
        }

        #endregion

        #region ɾ����������ԣ�ɾ��ʱ�䣬�Ƿ�ɾ��

        /// <summary>
        /// ��ȡ���������Ƿ񳹵�ɾ��
        ///// </summary>
        //public bool IsDeleted
        //{
        //    get { return Utility.Convert.StringToBool(this.GetAttribute("IsDeleted")); }
        //    set { this.SetAttribute("IsDeleted", value.ToString()); }
        //}
        /// <summary>
        /// ��ȡ������ɾ��ʱ��
        /// </summary>
        public DateTime DeletedTime
        {
            get { return Utility.Convert.StringToDateTime(this.GetAttribute("deletedTime")); }
            set { this.SetAttribute("deletedTime", value.ToString("yyyy-MM-dd HH:dd:ss")); }
        }
        /// <summary>
        /// ��ȡ�������Ƿ�ɾ��(�������վ)
        /// </summary>
        public bool IsDeleted
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isDeleted")); }
            set { this.SetAttribute("isDeleted", value.ToString()); }
        }

        #endregion

        #region �ƶ���������ԣ��Ƿ��ƶ���Դλ�ã����һ���ƶ���ʱ��
        /// <summary>
        /// ��ȡ�������Ƿ��ƶ�
        /// </summary>
        public bool IsMoved
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isMoved")); }
            set { this.SetAttribute("isMoved", value.ToString()); }
        }

        /// <summary>
        /// ��ȡ�������ƶ�ʱ��
        /// </summary>
        public DateTime MovedTime
        {
            get { return Utility.Convert.StringToDateTime(this.GetAttribute("movedTime")); }
            set { this.SetAttribute("movedTime", value.ToString("yyyy-MM-dd HH:dd:ss")); }
        }

        /// <summary>
        /// ��ȡ�������ƶ�ǰ��Դλ��
        /// </summary>
        public string MoveSourcePlace
        {
            get { return this.GetAttribute("MoveSourcePlace"); }
            set { this.SetAttribute("MoveSourcePlace", value); }
        }
        #endregion

        #region �汾��������ԣ��汾��

        /// <summary>
        /// ��ȡ�������޸ĺ�İ汾(��������Эͬʱ�İ汾����ʹ��)
        /// ���û��޹أ��û������޸�
        /// </summary>
        public int Version
        {
            get { return Utility.Convert.StringToInt(this.GetAttribute("version")); }
            set { this.SetAttribute("version", value.ToString()); }
        }

        #endregion

        #region �ղؼе��������
        /// <summary>
        /// �Ƿ�����ղؼ�
        /// </summary>
        public bool IsFavorite
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isFavorite")); }
            set { this.SetAttribute("isFavorite", value.ToString()); }
        }
        #endregion

        public override string Title
        {
            get
            {
                return base.Title;
            }
            set
            {
                base.Title = value;

                ///����Element��title��ͬʱ��Ҳ����Document���title
                if (this is IGetDocumentable)
                {
                    if (this.IsInDocument)
                    {
                        IndexXmlDocument doc = ((IGetDocumentable)this).GetIndexXmlDocument();
                        if (doc.Title != value)
                        {
                            doc.Title = value;
                            doc.Save();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// �Ƿ����ʹ�ô��ļ���(��Ҫ�ж��Ƿ����ͬ��)
        /// </summary>
        public bool CanNewFileName(string newFileName)
        {
            if (newFileName == this.FileName)
            {
                return true;
            }

            string newFilePath = Path.Combine(OwnerFolderElement.AbsoluteFilePath, newFileName);

            if (IsFolder)
            {
                return !Directory.Exists(newFilePath);
            }
            else
            {
                ///.sdpage��������index����
                if (FileExtension.Equals(Utility.Const.PageFileExt, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (newFileName.Equals("index", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return false;
                    }
                }

                return !File.Exists(newFilePath + FileExtension);
            }
        }

        /// <summary>
        /// �Ƿ��������Ŀ��,ע���Ƿ��ų�
        /// </summary>
        public bool IsExclude
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isExclude")); }
            set { this.SetAttribute("isExclude", value.ToString()); }
        }

        public virtual string FileExtension { get { return ""; } }

        /// <summary>
        /// ��ȡ��Element����һ��FolderElmeent
        /// </summary>
        public FolderXmlElement OwnerFolderElement
        {
            get
            {
                return this.ParentNode as FolderXmlElement;
            }
        }

        /// <summary>
        /// ��ȡ��Element����һ��ChannelElement
        /// </summary>
        public FolderXmlElement OwnerChannelElement
        {
            get
            {
                FolderXmlElement parentFolder = this.ParentNode as FolderXmlElement;
                while (parentFolder != null && !(parentFolder is ChannelSimpleExXmlElement))
                {
                    parentFolder = parentFolder.ParentNode as FolderXmlElement;
                }
                return parentFolder as ChannelSimpleExXmlElement;
            }
        }

        /// <summary>
        /// ��ȡ����������
        /// </summary>
        abstract public DataType DataType { get; }

        /// <summary>
        /// �ļ��㼶����
        /// </summary>
        public int FileLevel
        {
            get
            {
                XmlNode tempEle = this;
                int level = 0;

                ///��rootChannel��Ϊ0��
                while (tempEle != this.OwnerDocument.DocumentElement)
                {
                    level++;
                    tempEle = tempEle.ParentNode;
                }

                return level - 1;
            }
        }

        /// <summary>
        /// ��ȡ�˽ڵ�����Ӧ�������ļ������·��
        /// </summary>
        abstract public string RelativeFilePath { get; }

        /// <summary>
        /// ��ȡ��Ե�Url
        /// </summary>
        abstract public string RelativeUrl { get; }

        /// <summary>
        /// �������˴��ļ������·��(���û�иı䣬�����RelativeFilePath)
        /// </summary>
        public abstract string OldRelativeFilePath { get; }

        public abstract string OldRelativeUrl { get; }

        /// <summary>
        /// �˽ڵ�����Ӧ�������ļ��ľ���·��
        /// </summary>
        public string AbsoluteFilePath
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(OwnerDocument.AbsoluteFilePath), RelativeFilePath);
            }
        }

        /// <summary>
        /// ͨ����������丸�ڵ㣬ȷ���Ƿ���Ҫ����
        /// (��ǰ�ڵ���κ��漶�Ľڵ��IsPublishΪfalse���ᵼ�·���false)
        /// </summary>
        public bool IsPublishRecursive
        {
            get
            {
                if (IsPublish)
                {
                    FolderXmlElement parentFolder = this.OwnerFolderElement;
                    while (parentFolder != null)
                    {
                        if (!parentFolder.IsPublish)
                        {
                            return false;
                        }
                        parentFolder = parentFolder.OwnerFolderElement;
                    }

                }

                return true;
            }
        }

        /// <summary>
        /// ͨ����������丸�ڵ㣬ȷ���Ƿ�ɾ��
        /// (��ǰ�ڵ���κ��漶�Ľڵ��IsDeletedΪtrue���ᵼ�·���true)
        /// </summary>
        public bool IsDeletedRecursive
        {
            get
            {
                if (IsDeleted)
                {
                    return true;
                }

                if (!IsDeleted)
                {
                    FolderXmlElement parentFolder = this.OwnerFolderElement;
                    while (parentFolder != null)
                    {
                        if (parentFolder.IsDeleted)
                        {
                            return true;
                        }
                        parentFolder = parentFolder.OwnerFolderElement;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// �ƶ����µ�λ�ú��¼ԭ�����ڵ��ļ��е�ID
        /// </summary>
        public string OldParentFolderId
        {
            get
            {
                return this.GetAttribute("oldParentFolderId");
            }
            set
            {
                this.SetAttribute("oldParentFolderId", value);
            }
        }

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        //public bool IsTitleModify
        //{
        //    get { return Utility.Convert.StringToBool(this.GetAttribute("isTitleModify")); }
        //    set { this.SetAttribute("isTitleModify", value.ToString()); }
        //}

        /// <summary>
        /// �������󣬱������ϴε�����
        /// </summary>
        public string OldFileName
        {
            get
            {
                return this.GetAttribute("oldFileName");
            }
            set
            {
                this.SetAttribute("oldFileName", value);
            }
        }

        /// <summary>
        /// ·���Ƿ�ı�(��Ҫ�ݹ��ж�OldFileName��OldParentFolderId)
        /// </summary>
        public bool IsChangedPosition
        {
            get
            {
                //��ͬ����Ϊ true������Ϊ false��
                return !string.Equals(RelativeFilePath, OldRelativeFilePath, StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// ��ȡ��ǰԪ�ض�Ӧ���Ƿ��ļ���
        /// </summary>
        public bool IsFolder
        {
            get
            {
                return DataType == DataType.Folder
                    || DataType == DataType.Channel
                    || DataType == DataType.Resources
                    || DataType == DataType.TmpltFolder;

            }
        }
      
    }
}

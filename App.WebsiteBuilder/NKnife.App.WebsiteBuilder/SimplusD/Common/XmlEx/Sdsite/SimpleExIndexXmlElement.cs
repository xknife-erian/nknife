using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using System.IO;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 一般来讲：大多数是保存在SdSite文件中的具有ID特性的Element，
    /// 这些Element将扩展保存一些关于上传相关的信息
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

        #region 创建的相关属性：创建时间

        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        public DateTime CreatedTime
        {
            get { return Utility.Convert.StringToDateTime(this.GetAttribute("createdTime")); }
            set { this.SetAttribute("createdTime", value.ToString(Utility.Const.TimeFormat)); }
        }

        #endregion

        #region 修改的相关属性：修改时间，是否修改，

        /// <summary>
        /// 获取或设置修改时间
        /// </summary>
        public DateTime ModifiedTime
        {
            get { return Utility.Convert.StringToDateTime(this.GetAttribute("modifiedTime")); }
            internal set { this.SetAttribute("modifiedTime", value.ToString(Utility.Const.TimeFormat)); }
        }

        /// <summary>
        /// 获取或设置是否被修改
        /// </summary>
        public bool IsModified
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isModified")); }
            set { this.SetAttribute("isModified", value.ToString()); }
        }

        #endregion

        #region 发布的相关属性：是否始终发布，是否已经发布，最后一次的发布时间，取消发布的时间

        /// <summary>
        /// 获取或设置是否发布
        /// </summary>
        public bool IsPublish
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isPublish")); }
            set { this.SetAttribute("isPublish", value.ToString()); }
        }
        /// <summary>
        /// 获取或设置是否已经发布
        /// </summary>
        public bool IsAlreadyPublished
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isAlreadyPublished")); }
            set { this.SetAttribute("isAlreadyPublished", value.ToString()); }
        }
        /// <summary>
        /// 获取或设置最后一次的发布时间
        /// </summary>
        public DateTime PublishedTime
        {
            get { return Utility.Convert.StringToDateTime(this.GetAttribute("publishedTime")); }
            set { this.SetAttribute("publishedTime", value.ToString(Utility.Const.TimeFormat)); }
        }

        /// <summary>
        /// 获取或设置停止发布的时间
        /// </summary>
        public DateTime StopPublishTime
        {
            get { return Utility.Convert.StringToDateTime(this.GetAttribute("stopPublishTime")); }
            set { this.SetAttribute("stopPublishTime", value.ToString(Utility.Const.TimeFormat)); }
        }

        #endregion

        #region 推广的相关属性（推广：即将当前Page在网络做广告，让更多的人看到本条内容）

        /// <summary>
        /// 获取或设置项是否推广
        /// </summary>
        public bool IsAd
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isAd")); }
            set { this.SetAttribute("isAd", value.ToString()); }
        }
        /// <summary>
        /// 获取或设置项是否曾经推广的历史（无论现在是否关闭推广）
        /// </summary>
        public bool IsOnceAd
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("IsOnceAd")); }
            set { this.SetAttribute("IsOnceAd", value.ToString()); }
        }
        /// <summary>
        /// 获取或设置当前Page最后一次设置为IsAd为true的时间
        /// </summary>
        public DateTime AdTime
        {
            get { return Utility.Convert.StringToDateTime(this.GetAttribute("adTime")); }
            set { this.SetAttribute("adTime", value.ToString("yyyy-MM-dd HH:dd:ss")); }
        }

        #endregion

        #region 删除的相关属性：删除时间，是否删除

        /// <summary>
        /// 获取或设置项是否彻底删除
        ///// </summary>
        //public bool IsDeleted
        //{
        //    get { return Utility.Convert.StringToBool(this.GetAttribute("IsDeleted")); }
        //    set { this.SetAttribute("IsDeleted", value.ToString()); }
        //}
        /// <summary>
        /// 获取或设置删除时间
        /// </summary>
        public DateTime DeletedTime
        {
            get { return Utility.Convert.StringToDateTime(this.GetAttribute("deletedTime")); }
            set { this.SetAttribute("deletedTime", value.ToString("yyyy-MM-dd HH:dd:ss")); }
        }
        /// <summary>
        /// 获取或设置是否删除(放入回收站)
        /// </summary>
        public bool IsDeleted
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isDeleted")); }
            set { this.SetAttribute("isDeleted", value.ToString()); }
        }

        #endregion

        #region 移动的相关属性：是否被移动，源位置，最后一次移动的时间
        /// <summary>
        /// 获取或设置是否被移动
        /// </summary>
        public bool IsMoved
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isMoved")); }
            set { this.SetAttribute("isMoved", value.ToString()); }
        }

        /// <summary>
        /// 获取或设置移动时间
        /// </summary>
        public DateTime MovedTime
        {
            get { return Utility.Convert.StringToDateTime(this.GetAttribute("movedTime")); }
            set { this.SetAttribute("movedTime", value.ToString("yyyy-MM-dd HH:dd:ss")); }
        }

        /// <summary>
        /// 获取或设置移动前的源位置
        /// </summary>
        public string MoveSourcePlace
        {
            get { return this.GetAttribute("MoveSourcePlace"); }
            set { this.SetAttribute("MoveSourcePlace", value); }
        }
        #endregion

        #region 版本的相关属性：版本号

        /// <summary>
        /// 获取或设置修改后的版本(供二期做协同时的版本控制使用)
        /// 与用户无关，用户无需修改
        /// </summary>
        public int Version
        {
            get { return Utility.Convert.StringToInt(this.GetAttribute("version")); }
            set { this.SetAttribute("version", value.ToString()); }
        }

        #endregion

        #region 收藏夹的相关属性
        /// <summary>
        /// 是否放入收藏夹
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

                ///设置Element的title的同时，也设置Document里的title
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
        /// 是否可以使用此文件名(主要判断是否会有同名)
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
                ///.sdpage不可以用index命名
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
        /// 是否包含在项目中,注：是否被排除
        /// </summary>
        public bool IsExclude
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isExclude")); }
            set { this.SetAttribute("isExclude", value.ToString()); }
        }

        public virtual string FileExtension { get { return ""; } }

        /// <summary>
        /// 获取此Element的上一级FolderElmeent
        /// </summary>
        public FolderXmlElement OwnerFolderElement
        {
            get
            {
                return this.ParentNode as FolderXmlElement;
            }
        }

        /// <summary>
        /// 获取此Element的上一级ChannelElement
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
        /// 获取其数据类型
        /// </summary>
        abstract public DataType DataType { get; }

        /// <summary>
        /// 文件层级。将
        /// </summary>
        public int FileLevel
        {
            get
            {
                XmlNode tempEle = this;
                int level = 0;

                ///将rootChannel视为0级
                while (tempEle != this.OwnerDocument.DocumentElement)
                {
                    level++;
                    tempEle = tempEle.ParentNode;
                }

                return level - 1;
            }
        }

        /// <summary>
        /// 获取此节点所对应的数据文件的相对路径
        /// </summary>
        abstract public string RelativeFilePath { get; }

        /// <summary>
        /// 获取相对的Url
        /// </summary>
        abstract public string RelativeUrl { get; }

        /// <summary>
        /// 服务器端此文件的相对路径(如果没有改变，则等于RelativeFilePath)
        /// </summary>
        public abstract string OldRelativeFilePath { get; }

        public abstract string OldRelativeUrl { get; }

        /// <summary>
        /// 此节点所对应的数据文件的绝对路径
        /// </summary>
        public string AbsoluteFilePath
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(OwnerDocument.AbsoluteFilePath), RelativeFilePath);
            }
        }

        /// <summary>
        /// 通过迭代检查其父节点，确定是否需要发布
        /// (当前节点或任何祖级的节点的IsPublish为false都会导致返回false)
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
        /// 通过迭代检查其父节点，确定是否被删除
        /// (当前节点或任何祖级的节点的IsDeleted为true都会导致返回true)
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
        /// 移动到新的位置后记录原来所在的文件夹的ID
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
        /// 是否被重命名
        /// </summary>
        //public bool IsTitleModify
        //{
        //    get { return Utility.Convert.StringToBool(this.GetAttribute("isTitleModify")); }
        //    set { this.SetAttribute("isTitleModify", value.ToString()); }
        //}

        /// <summary>
        /// 重命名后，保存其上次的名字
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
        /// 路径是否改变(主要递归判断OldFileName和OldParentFolderId)
        /// </summary>
        public bool IsChangedPosition
        {
            get
            {
                //相同，则为 true；否则为 false。
                return !string.Equals(RelativeFilePath, OldRelativeFilePath, StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// 获取当前元素对应的是否文件夹
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

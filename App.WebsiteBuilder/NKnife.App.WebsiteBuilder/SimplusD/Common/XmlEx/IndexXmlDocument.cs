using System;
using Jeelu.Win;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 有Id的XmlDocument。(以前的IndexXmlDocument的概念和这里不一样。
    /// 以前的所有IndexXmlDocument在现在项目里被SdsiteXmlDocument一个类给替代了。
    /// 这里的IndexXmlDocument一定意义上替代了以前的IndexXmlElement)
    /// </summary>
    abstract public class IndexXmlDocument : AnyXmlDocument
    {
        public IndexXmlDocument(SimpleExIndexXmlElement sdsiteElement)
            :base(sdsiteElement.AbsoluteFilePath)
        {
            this.SdsiteElement = sdsiteElement;
        }

        public string Id
        {
            get
            {
                return this.DocumentElement.GetAttribute("id");
            }
            set
            {
                this.DocumentElement.SetAttribute("id", value);
            }
        }
        //[PropertyPad(0, 0, "标题")]
        [Editor(0, 0, "Title", MainControlWidth = 180)]
        [Grid("0", true, 3, "Title", "Title", 50, IsAutoColumn = true)]
        [SnipPart("BaseTitle", "BaseTitle", "BaseTitle", "BaseTitle", 0, 100)]
        public string Title
        {
            get
            {
                return this.DocumentElement.GetAttribute("title");
            }
            set
            {
                this.DocumentElement.SetAttribute("title", value);
                ///设置Document的title的同时，也设置Element里的title
                if (SdsiteElement.Title != value)
                {
                    SdsiteElement.Title = value;
                }
            }
        }

        /// <summary>
        /// 当前XmlDocument在SdsiteXmlDocument里相对应的XmlElement
        /// </summary>
        public SimpleExIndexXmlElement SdsiteElement { get; private set; }

        /// <summary>
        /// 当前XmlDocument所在项目的SdsiteXmlDocument
        /// </summary>
        public SdsiteXmlDocument OwnerSdsiteDocument
        {
            get
            {
                return this.SdsiteElement.OwnerDocument;
            }
        }
        //public override string RelativeFilePath
        //{
        //    get
            //{
            //    return SdsiteElement.RelativeFilePath;
            //}
        //}

        #region 快捷属性

        #region 创建的相关属性：创建时间

        /// <summary>
        /// 获取创建时间
        /// </summary>
        [Grid("State,1", false, 8, "CreatedTime", "CreatedTime", 110)]
        public DateTime CreatedTime
        {
            get
            {
                return SdsiteElement.CreatedTime;
            }
        }

        #endregion

        #region 修改的相关属性：修改时间，是否修改，

        /// <summary>
        /// 获取修改时间
        /// </summary>
        [Grid("State,2", true, 5, "ModifiedTime", "ModifiedTime", 110)]
        public  DateTime ModifiedTime
        {
            get
            {
                return SdsiteElement.ModifiedTime;
            }
        }

        /// <summary>
        /// 获取是否被修改状态
        /// </summary>
        //[Grid("State,2", false, 4, "", "IsModified", 26)]
        public bool IsModified
        {
            get
            {
                return SdsiteElement.IsModified;
            }
        }

        #endregion

        #region 发布的相关属性：是否打算发布，是否已经发布，最后一次的发布时间，停止发布的时间

        /// <summary>
        /// 获取是否打算发布
        /// </summary>
        [Grid("State,2", false, 6, "", "IsPublish", 26)]
        public bool IsPublish
        {
            get
            {
                return SdsiteElement.IsPublish;
            }
        }
        /// <summary>
        /// 获取是否已经发布
        /// </summary>
        //[Grid("State,2", true, 1, "", "IsAlreadyPublished", 26)]
        public bool IsAlreadyPublished
        {
            get
            {
                return SdsiteElement.IsAlreadyPublished;
            }
        }

        [Grid("State,2", true, 1, "", "SimpleState", 26)]
        public PageSimpleState SimpleState
        {
            get
            {
                if (!IsAlreadyPublished)
                {
                    return PageSimpleState.New;
                }
                else if (IsModified)
                {
                    return PageSimpleState.Modified;
                }
                else
                {
                    return PageSimpleState.NotModified;
                }
            }
        }

        /// <summary>
        /// 获取最后一次的发布时间
        /// </summary>
        [Grid("State,2", false, 7, "PublishedTime", "PublishedTime", 110)]
        public DateTime PublishedTime
        {
            get
            {
                return SdsiteElement.PublishedTime;
            }
        }

        /// <summary>
        /// 获取停止发布的时间
        /// </summary>
        public DateTime StopPublishTime
        {
            get
            {
                return SdsiteElement.StopPublishTime;
            }
        }

        #endregion

        #region 删除的相关属性：彻底删除，删除时间，是否删除(放入回收站)

        /// <summary>
        /// 获取删除时间
        /// </summary>
        public DateTime DeletedTime
        {
            get
            {
                return SdsiteElement.DeletedTime;
            }
        }
        /// <summary>
        /// 获取是否删除(放入回收站)
        /// </summary>
       // [Grid("0", true, 999, "", "IsDeleted", 26, IsDisplayInGrid = false)]
        public bool IsDeleted
        {
            get
            {
                return SdsiteElement.IsDeleted;
            }
        }

        #endregion

        #region 移动的相关属性：是否被移动，源位置，最后一次移动的时间
        /// <summary>
        /// 获取是否被移动
        /// </summary>
        public bool IsMoved
        {
            get
            {
                return SdsiteElement.IsMoved;
            }
        }

        /// <summary>
        /// 最后一次移动的时间
        /// </summary>
        public DateTime MovedTime
        {
            get
            {
                return SdsiteElement.MovedTime;
            }
        }

        /// <summary>
        /// 获取该节点移动前的源位置
        /// </summary>
        public string MoveSourcePlace
        {
            get
            {
                return SdsiteElement.MoveSourcePlace;
            }
        }
        #endregion

        #region 推广的相关属性（推广：即将当前Page在网络做广告，让更多的人看到本条内容）

        /// <summary>
        /// 获取或设置项是否推广
        /// </summary>
        [Grid("State,2", true, 2, "", "IsAd", 28, IsFixColumn = true)]
        public bool IsAd
        {
            get {
                return SdsiteElement.IsAd;
            }
        }
        /// <summary>
        /// 获取或设置项是否曾经推广的历史（无论现在是否关闭推广）
        /// </summary>
        [Grid("State,2", false, 6, "", "IsOnceAd", 28, IsFixColumn = true)]
        public bool IsOnceAd
        {
            get {
                return SdsiteElement.IsOnceAd;
            }
        }
        /// <summary>
        /// 获取或设置当前Page最后一次设置为IsAd为true的时间
        /// </summary>
        [Grid("State,2", false, 7, "AdTime", "AdTime", 110)]
        public DateTime AdTime
        {
            get {
                return SdsiteElement.AdTime;
            }
        }

        #endregion

        #region 版本的相关属性：版本号

        /// <summary>
        /// 获取修改后的版本(供二期做协同时的版本控制使用)
        /// 与用户无关，用户无需修改
        /// </summary>
        public int Version
        {
            get
            {
                return SdsiteElement.Version;
            }
        }

        #endregion

        #region 收藏夹的相关属性
        /// <summary>
        /// 是否放入收藏夹
        /// </summary>
        [Grid("State,2", false, 998, "", "IsFavorite", 26, IsFixColumn = true)]
        public bool IsFavorite
        {
            get
            {
                return SdsiteElement.IsFavorite;
            }
        }
        #endregion

        #endregion

        public override void Save()
        {
            OwnerSdsiteDocument.MarkModified(SdsiteElement);
            OwnerSdsiteDocument.Save();
            base.Save();
        }

        public virtual void SaveWithoutMarkModified()
        {
            base.Save();
        }
    }
}

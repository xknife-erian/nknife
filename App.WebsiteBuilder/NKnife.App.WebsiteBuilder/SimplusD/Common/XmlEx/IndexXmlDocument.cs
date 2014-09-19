using System;
using Jeelu.Win;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// ��Id��XmlDocument��(��ǰ��IndexXmlDocument�ĸ�������ﲻһ����
    /// ��ǰ������IndexXmlDocument��������Ŀ�ﱻSdsiteXmlDocumentһ���������ˡ�
    /// �����IndexXmlDocumentһ���������������ǰ��IndexXmlElement)
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
        //[PropertyPad(0, 0, "����")]
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
                ///����Document��title��ͬʱ��Ҳ����Element���title
                if (SdsiteElement.Title != value)
                {
                    SdsiteElement.Title = value;
                }
            }
        }

        /// <summary>
        /// ��ǰXmlDocument��SdsiteXmlDocument�����Ӧ��XmlElement
        /// </summary>
        public SimpleExIndexXmlElement SdsiteElement { get; private set; }

        /// <summary>
        /// ��ǰXmlDocument������Ŀ��SdsiteXmlDocument
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

        #region �������

        #region ������������ԣ�����ʱ��

        /// <summary>
        /// ��ȡ����ʱ��
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

        #region �޸ĵ�������ԣ��޸�ʱ�䣬�Ƿ��޸ģ�

        /// <summary>
        /// ��ȡ�޸�ʱ��
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
        /// ��ȡ�Ƿ��޸�״̬
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

        #region ������������ԣ��Ƿ���㷢�����Ƿ��Ѿ����������һ�εķ���ʱ�䣬ֹͣ������ʱ��

        /// <summary>
        /// ��ȡ�Ƿ���㷢��
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
        /// ��ȡ�Ƿ��Ѿ�����
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
        /// ��ȡ���һ�εķ���ʱ��
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
        /// ��ȡֹͣ������ʱ��
        /// </summary>
        public DateTime StopPublishTime
        {
            get
            {
                return SdsiteElement.StopPublishTime;
            }
        }

        #endregion

        #region ɾ����������ԣ�����ɾ����ɾ��ʱ�䣬�Ƿ�ɾ��(�������վ)

        /// <summary>
        /// ��ȡɾ��ʱ��
        /// </summary>
        public DateTime DeletedTime
        {
            get
            {
                return SdsiteElement.DeletedTime;
            }
        }
        /// <summary>
        /// ��ȡ�Ƿ�ɾ��(�������վ)
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

        #region �ƶ���������ԣ��Ƿ��ƶ���Դλ�ã����һ���ƶ���ʱ��
        /// <summary>
        /// ��ȡ�Ƿ��ƶ�
        /// </summary>
        public bool IsMoved
        {
            get
            {
                return SdsiteElement.IsMoved;
            }
        }

        /// <summary>
        /// ���һ���ƶ���ʱ��
        /// </summary>
        public DateTime MovedTime
        {
            get
            {
                return SdsiteElement.MovedTime;
            }
        }

        /// <summary>
        /// ��ȡ�ýڵ��ƶ�ǰ��Դλ��
        /// </summary>
        public string MoveSourcePlace
        {
            get
            {
                return SdsiteElement.MoveSourcePlace;
            }
        }
        #endregion

        #region �ƹ��������ԣ��ƹ㣺������ǰPage����������棬�ø�����˿����������ݣ�

        /// <summary>
        /// ��ȡ���������Ƿ��ƹ�
        /// </summary>
        [Grid("State,2", true, 2, "", "IsAd", 28, IsFixColumn = true)]
        public bool IsAd
        {
            get {
                return SdsiteElement.IsAd;
            }
        }
        /// <summary>
        /// ��ȡ���������Ƿ������ƹ����ʷ�����������Ƿ�ر��ƹ㣩
        /// </summary>
        [Grid("State,2", false, 6, "", "IsOnceAd", 28, IsFixColumn = true)]
        public bool IsOnceAd
        {
            get {
                return SdsiteElement.IsOnceAd;
            }
        }
        /// <summary>
        /// ��ȡ�����õ�ǰPage���һ������ΪIsAdΪtrue��ʱ��
        /// </summary>
        [Grid("State,2", false, 7, "AdTime", "AdTime", 110)]
        public DateTime AdTime
        {
            get {
                return SdsiteElement.AdTime;
            }
        }

        #endregion

        #region �汾��������ԣ��汾��

        /// <summary>
        /// ��ȡ�޸ĺ�İ汾(��������Эͬʱ�İ汾����ʹ��)
        /// ���û��޹أ��û������޸�
        /// </summary>
        public int Version
        {
            get
            {
                return SdsiteElement.Version;
            }
        }

        #endregion

        #region �ղؼе��������
        /// <summary>
        /// �Ƿ�����ղؼ�
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

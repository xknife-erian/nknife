using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using Jeelu.Win;
using System.IO;

namespace Jeelu.SimplusD
{
    public partial class SnipXmlElement : ToHtmlXmlElement
    {
        internal SnipXmlElement(TmpltXmlDocument doc)
            : base("snip", doc)
        {
            TmpltDoc = doc;
        }

        #region 公共属性

        public TmpltXmlDocument TmpltDoc { get; set; }

        public int X
        {
            get { return Convert.ToInt32(GetAttribute("x")); }
            set { SetAttribute("x", value.ToString()); }
        }

        public int Y
        {
            get { return Convert.ToInt32(GetAttribute("y")); }
            set { SetAttribute("y", value.ToString()); }
        }

        /// <summary>
        /// 获取或设置宽度
        /// </summary>
        public string Width
        {
            get { return GetAttribute("width"); }
            set { SetAttribute("width", value); }
        }

        /// <summary>
        /// 获取或设置高度
        /// </summary>
        public string Height
        {
            get { return GetAttribute("height"); }
            set { SetAttribute("height", value); }
        }

        /// <summary>
        /// 获取或设置页面片的类型
        /// </summary>
        public PageSnipType SnipType
        {
            get { return (PageSnipType)Enum.Parse(typeof(PageSnipType), GetAttribute("type")); }
            set
            {
                if (value == PageSnipType.Content)
                {
                    ((TmpltXmlDocument)OwnerDocument).HasContentSnip = true;
                }
                //else
                //    ((TmpltXmlDocument)OwnerDocument).HasContentSnip = false;

                SetAttribute("type", value.ToString());
            }
        }

        /// <summary>
        /// 获取或设置是否被锁定
        /// </summary>
        public bool IsLocked
        {
            get { return Convert.ToBoolean(GetAttribute("isLocked")); }
            set { SetAttribute("isLocked", value.ToString()); }
        }

        /// <summary>
        /// 获取或设置是否被选定
        /// </summary>
        public bool IsSelected
        {
            get { return Convert.ToBoolean(GetAttribute("isSelected")); }
            set { SetAttribute("isSelected", value.ToString()); }
        }

        /// <summary>
        /// 获取或设置是否包含页面片
        /// </summary>
        public bool HasSnip
        {
            get { return Utility.Convert.StringToBool(GetAttribute("hasSnip")); }
            set { SetAttribute("hasSnip", value.ToString()); }
        }

        /// <summary>
        /// 获取或设置Css字符串
        /// </summary>
        public string Css
        {
            get { return GetAttribute("css"); }
            set { SetAttribute("css", value); }
        }

        /// <summary>
        /// 获取或设置是否被修改
        /// </summary>
        public bool IsModified
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isModified")); }
            set 
            {
                TmpltDoc.Reseted = value;
                this.SetAttribute("isModified", value.ToString()); 
            }
        }

        public string SnipName
        {
            get
            {
                return GetAttribute("snipName");
            }
            set { SetAttribute("snipName", value); }
        }

        private Dictionary<string, PropertyInfo> dicProperties = new Dictionary<string, PropertyInfo>();
        private Dictionary<string, PropertyInfo> dicIsReadResource = new Dictionary<string, PropertyInfo>();
        public bool IsContainKeyList
        {
            get
            {
                if (this.SelectNodes("descendant::part[@type='ListBox'and @sequenceType='AutoKeyWord']").Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public SnipPartXmlElement GetKeywordListBox()
        {
            return this.SelectSingleNode("//part[@type='ListBox'and @sequenceType='AutoKeyWord']") as SnipPartXmlElement;
        }

        /// <summary>
        /// 是否含有正文？
        /// </summary>
        public bool IsHaveContent
        {
            get
            {
                if (this.SelectNodes("//part[@type='Attribute' and @attributeName='Content']").Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region 公共方法

        public SnipPartsXmlElement GetPartsElement()
        {
            return (SnipPartsXmlElement)SelectSingleNode("parts");
        }
        /// <summary>
        /// 取得PartsElement元素
        /// </summary>
        /// 拥有channelId的channel节点的part节点
        /// <returns></returns>
        public SnipPartXmlElement[] GetPartElement(string channelId)
        {
            List<SnipPartXmlElement> list = new List<SnipPartXmlElement>();

            //todo:...
            return list.ToArray();
        }

        public SnipPartXmlElement CreatePart()
        {
            SnipPartXmlElement _partEle = new SnipPartXmlElement(OwnerDocument);
            return _partEle;
        }

        public XmlElement CreateChannelListElement()
        {
            XmlElement _channelIdListEle = OwnerDocument.CreateElement("channels");
            return _channelIdListEle;
        }

        public XmlElement CreateChannelElement(string channelId )
        {
            XmlElement ele = OwnerDocument.CreateElement("channel");
            ele.SetAttribute("id", channelId);
            return ele;
        }

        public XmlElement CreateAttributeListElement()
        {
            XmlElement ele = OwnerDocument.CreateElement("attributes");
            return ele;
        }

        public XmlElement CreateAttributeElement(string name)
        {
            XmlElement ele = OwnerDocument.CreateElement("attribute");
            ele.SetAttribute("name", name);
            return ele;
        }

        /// <summary>
        /// 判断是否含有Flash或Picture,用于发布时判断snip是否需要判断其生成的Html
        /// </summary>
        public bool ContainFlashOrPicture()
        {
            return true;
        }

        ///// <summary>
        ///// 判断是否含有列表或导航型part,用于发布时判断snip是否需要重新生成Html
        ///// </summary>
        //public bool ContainNavigationOrList()
        //{
        //    XmlNode xmlNavigationOrListNode = this.SelectSingleNode("//part[@type='Navigation']|//part[@type='List']");
        //    if (xmlNavigationOrListNode != null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        #endregion

        #region 重写Tohtml相关的方法

        /// <summary>
        /// 重写，考虑Snip节点下应该是一个SSI的包含语句，被包含的是一个“.inc”文件。
        /// </summary>
        protected override void TagCreator()
        {
            XhtmlElement tag;
            if (this._ParentXhtmlElement != null)
            {
                if (this.SnipType == PageSnipType.Content)//当是Content型的Snip时,不再生成,而是置入一条CGI语句
                {
                    SdCgiObject cgi = new SdCgiObject(CgiPlace.Content);
                    tag = this._ParentXhtmlElement.OwnerPage.CreateXhtmlCommentShtml(cgi);
                }
                else
                {
                    string path = ((TmpltXmlDocument)this.OwnerAnyDocument).TmpltToHtmlUrl;
                    string file = Path.GetFileName(this.HtmlFile);
                    tag = this._ParentXhtmlElement.OwnerPage.CreateXhtmlCommentShtml(path + file);
                }

                this._ParentXhtmlElement.AppendChild(tag);
            }
            this._XhtmlElement = new XhtmlSection("div");
        }

        #endregion
    }

    /*

    #region 列表排序类

    class ListTimeSort
    {
        private DateTime _time;
        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }

        private string Id;
        public string ID
        {
            get { return Id; }
            set { Id = value; }
        }
        public string[] GetContentId(List<string> ContentTimeList, int count)
        {
            return null;
        }
        private int ContentDataSortMethod(ListTimeSort cd1, ListTimeSort cd2)
        {
            return DateTime.Compare(cd1.Time, cd2.Time);
        }
    }

    class ListKeywordSort
    {
        private string[] _keyword;
        public string[] keyword
        {
            get { return _keyword; }
            set { _keyword = value; }
        }

        private string Id;
        public string ID
        {
            get { return Id; }
            set { Id = value; }
        }

        public string[] GetAutoKeyWordContentId(List<string> ContentKeyList, PageSimpleExXmlElement pageEle, int count)
        {
            return null;
        }
        public string[] GetCustomContentId(List<string> ContentKeyList, string[] keyWord, int count)
        {
            return GetContentId(ContentKeyList, keyWord, count);
        }

        private string[] GetContentId(List<string> ContentKeyList, string[] keyWord, int count)
        {
            return null;
        }

        /// <summary>
        /// 获取n个string集合的交集
        /// </summary>
        static List<string> IntersectionList(List<List<string>> AnyList)
        {
            List<string> interList = new List<string>();
            int countList = AnyList.Count;
            List<string> compareList = AnyList[0];
            for (int i = 0; i < compareList.Count; i++)
            {
                int SameCount = 0;
                for (int j = 1; j < countList; j++)
                {
                    if (AnyList[j].Contains(compareList[i]))
                    {
                        SameCount += 1;
                    }
                    else
                    {
                        break;
                    }
                }
                if (SameCount == countList - 1)
                {
                    interList.Add(compareList[i]);
                }

            }
            return interList;
        }
    }


    #endregion

    #region 创建Css类型
    enum CssType
    {
        Li = 0,

        UL = 1,

        Div = 2,

        Span = 4
    }
    #endregion

    */
}

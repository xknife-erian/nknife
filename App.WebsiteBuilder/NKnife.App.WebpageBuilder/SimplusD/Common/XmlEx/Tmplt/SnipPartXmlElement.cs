using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace Jeelu.SimplusD
{
    public partial class SnipPartXmlElement : ToHtmlXmlElement
    {
        internal SnipPartXmlElement(XmlDocument doc)
            : base("part", doc) { }

        /// <summary>
        /// edit by zhengaho at 2008-06-27 17:20
        /// 获取或设置是否使用本身的链接
        /// </summary>
        public bool UsedOwnerLink 
        {
            get { return Utility.Convert.StringToBool(GetAttribute("usedOwnerLink")); }
            set { SetAttribute("usedOwnerLink", value.ToString()); }
        }

        /// <summary>
        /// 获取或设置最大显示条数（列表专用）
        /// </summary>
        public int MaxListDisplayAmount 
        {
            get { return Convert.ToInt32(GetAttribute("displayAmount")); }
            set { SetAttribute("displayAmount", value.ToString()); }
        }

        /// <summary>
        /// edit by zhenghao at 2008-06-26 15:00
        /// 获取或设置样式类型
        /// </summary>
        public StyleType StyleType
        {
            get
            {
                try
                {
                    return (StyleType)Enum.Parse(typeof(StyleType), GetAttribute("styleType"));
                }
                catch (Exception)
                {
                    return StyleType.None;
                }
            }
            set { SetAttribute("styleType", value.ToString()); }
        }

        /// <summary>
        /// 设置或获取页面片组成部分的类型
        /// </summary>
        public SnipPartType SnipPartType
        {
            get { return (SnipPartType)Enum.Parse(typeof(SnipPartType), GetAttribute("type"), true); }
            set { SetAttribute("type", value.ToString()); }
        }

        /// <summary>
        /// 获取或设置该part的ID
        /// </summary>
        public string SnipPartId
        {
            get
            {
                return GetAttribute("partId");
            }
            set 
            {
                SetAttribute("partId", value);
            }
        }

        /// <summary>
        /// 获取或设置该part的CSS
        /// </summary>
        public string SnipPartCss
        {
            get
            {
                return GetAttribute("css");
            }
            set
            {
                SetAttribute("css", value);
            }
        }

        /// <summary>
        /// 获取或设置定制特性名
        /// </summary>
        public string AttributeName
        {
            get { return GetAttribute("attributeName"); }
            set { SetAttribute("attributeName", value); }
        }

        /// <summary>
        /// 获取是否有Part外层的链接。
        /// 根据2008-6-24全体策划会议决定加入的针对各种类型的Part都可以给其最外层加入链接。
        /// 链接相关属性将保存在Part节点的子节点中，子节点名以"a"命名。
        /// design by lukan, 2008-6-25 22:53:26
        /// </summary>
        public bool HasLinkForPart
        {
            get 
            {
                if (!this.HasChildNodes)
                {
                    return false;
                }
                foreach (XmlNode node in this.ChildNodes)
                {
                    if (node.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    if (node.LocalName.ToLower() == "a")
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 获取A链接节点
        /// </summary>
        public AnyXmlElement AElement 
        {
            get 
            {
                if (!HasChildNodes)
                {
                    return null;
                }
                foreach (XmlNode node in this.ChildNodes)
                {
                    if (node.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    if (node.LocalName.ToLower() == "a")
                    {
                        return (AnyXmlElement)node;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 获取或设置显示方式
        /// </summary>
        public DisplayType DisplayType 
        {
            get 
            { 
                return (DisplayType)Enum.Parse(typeof(DisplayType), GetAttribute("displayType"));
            }
            set 
            {
                SetAttribute("displayType", value.ToString());
            }
        }

        /// <summary>
        /// 获取或设置如果是List型Part时的排序方式
        /// design by lukan, 2008-6-26 09:14:25
        /// </summary>
        public SequenceType SequenceType
        {
            get
            {
                try
                {
                    return (SequenceType)Enum.Parse(typeof(SequenceType), this.GetAttribute("sequenceType"));
                }
                catch (Exception)
                {
#if !DEBUG
                    this.SetAttribute("sequenceType", SequenceType.None.ToString());
#endif
                    return SequenceType.None;
                }
            }
            set { this.SetAttribute("sequenceType", value.ToString()); }
        }

        private List<string> _listChannelIDs = new List<string>();
        /// <summary>
        /// 获取或设置为列表时覆盖的频道ID集合
        /// </summary>
        public List<string> ListChannelIDs 
        {
            get { return _listChannelIDs; }
            set 
            {
                _listChannelIDs = value;
                if (value.Count > 0)
                {
                    AnyXmlElement channelsEle = (AnyXmlElement)OwnerDocument.CreateElement("channels");
                    foreach (string channelId in value)
                    {
                        AnyXmlElement channelEle = (AnyXmlElement)OwnerDocument.CreateElement("channel");
                        channelEle.CDataValue = channelId;
                        channelsEle.AppendChild(channelEle);
                    }
                    AppendChild(channelsEle);
                }
            }
        }

        //add by zhangling on2008年7月4日
        /// <summary>
        /// 得到当前元素(type="List")下类型为listbox的part的所有页面类型
        /// </summary>
        public List<string> GetPageType()
        {
            List<string> list = new List<string>();
            list.Clear();
            XmlNodeList nodeList = this.SelectNodes(string.Format("part[@type='{0}']", "ListBox"));
            foreach (XmlNode node in nodeList )
            {
                XmlElement ele = (XmlElement)node;
                string styleType = ele.GetAttribute("styleType");
                list.Add(styleType);
            }
            return list;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    public class ProjectPartElement : IndexXmlElement
    {
        public ProjectPartElement(string localName, XmlDocument doc)
            : base(localName, doc)
        {
        }
        /// <summary>
        /// 项目的阶段
        /// </summary>
        public string Part
        {
            get { return this.GetAttribute("Part"); }
            set { this.SetAttribute("Part", value); }
        }
        /// <summary>
        /// 项目阶段完成时间
        /// </summary>
        public DateTime PartEndTime
        {
            get { return Utility.Convert.StringToDateTime(this.GetAttribute("partEndTime")); }
            set { this.SetAttribute("partEndTime", value.ToString(Utility.Const.TimeFormat)); }
        }

        /// <summary>
        /// 项目的阶段成本预算
        /// </summary>
        /// 
        public float PartCost
        {
            get { return Utility.Convert.StringToFloat(this.GetAttribute("partCost")); }
            set { this.SetAttribute("partCost", value.ToString()); }

        }
        /// <summary>
        /// 是否为当前进行的阶段
        /// </summary>
        /// 
        public bool IsNowDoing
        {
            get
            {
                return Utility.Convert.StringToBool(this.GetAttribute("isNowDoing"));
            }
            set
            {
                this.SetAttribute("isNowDoing", value.ToString());
            }
        }
    }
}

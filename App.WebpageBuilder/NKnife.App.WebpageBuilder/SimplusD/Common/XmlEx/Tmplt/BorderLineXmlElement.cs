using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD
{
    public  class BorderLineXmlElement : AnyXmlElement
    {
        /// <summary>
        /// .sdtmplt文档line元素(线段)
        /// </summary>
        /// <param name="doc"></param>
        public BorderLineXmlElement(TmpltXmlDocument doc)
            : base( "borderLine",  doc)
        { }

        /// <summary>
        /// 设置或获取线段的初始位置
        /// </summary>
        public int Start
        {
            get { return Convert.ToInt32(this.GetAttribute("start")); }
            set { this.SetAttribute("start", value.ToString()); }
        }

        /// <summary>
        /// 设置或获取线段的末尾位置
        /// </summary>
        public int End
        {
            get { return Convert.ToInt32(this.GetAttribute("end")); }
            set { this.SetAttribute("end", value.ToString()); }
        }

        /// <summary>
        /// 设置或获取线段的坐标（IsRow = true时为线段的横坐标 IsRow = false时为线段的纵坐标）
        /// </summary>
        public int Position
        {
            get { return Convert.ToInt32(this.GetAttribute("position")); }
            set { this.SetAttribute("position", value.ToString()); }
        }

        /// <summary>
        /// 设置或获取线段是否为水平线
        /// </summary>
        public bool IsRow
        {
            get { return bool.Parse(GetAttribute("isRow")); }
            set { SetAttribute("isRow",value.ToString()); }
        }
    }
}

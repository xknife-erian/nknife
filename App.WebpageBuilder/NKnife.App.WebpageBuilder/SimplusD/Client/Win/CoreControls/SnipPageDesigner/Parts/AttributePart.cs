using System;
using System.Collections.Generic;
using System.Text;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    [Serializable]
    public class AttributePart : SnipPagePart
    {
        /// <summary>
        /// 定制特性定义的Name。此Name标识正文型页面片的一种Part，如：title(标题)
        /// </summary>
        //[PropertyPad(0, 1, "attributePartName", MainControlType = MainControlType.TextBox,IsReadOnly=true )]
        public string AttributeName { get; set; }

        protected AttributePart(SnipPageDesigner designer)
            : base(designer)
        {
        }

        [PropertyPad(0, 1, "partText", MainControlType = MainControlType.TextBox, IsReadOnly = true)]
        public override string Text
        {
            get
            {
                return AutoLayoutPanel.GetLanguageText(AttributeName);
            }
            set
            {
                base.Text = value;
            }
        }

        /// <summary>
        /// edit by zhenghao at 2008-06-27 17:20
        /// 是否使用本身的链接
        /// </summary>
        public bool UsedOwnerLink { get; set; }

        static internal AttributePart Create(SnipPageDesigner designer)
        {
            return new AttributePart(designer);
        }
    }
}

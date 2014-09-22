using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    static public partial class SoftwareOption
    {
        [SoftOptionClass("Site")]
        static public class Site
        {
            /// <summary>
            /// 当前类中所有元素的父元素
            /// </summary>
            static XmlElement itemsEle
            {
                get
                {
                    return GetItemElement("Site");
                }
            }
            /// <summary>
            ///是否为小图标
            /// </summary>
            [PropertyPad(0, 0, "",
            GroupBoxDockTop = true ,
            GroupBoxUseWinStyle = true ,
            GroupBoxUseWinStyleText = "createSite",
            MainControlType = MainControlType.SimpleCheckBox,
            SimpleCheckBoxText = "showSmallIconTxt"
            )]
            public static bool ShowSmallIcon
            {
                get { return (bool)GetValue(itemsEle, "isSmallIcon", typeof(bool)); }
                set { SetValue(itemsEle, "isSmallIcon", value); }
            }

            /// <summary>
            /// 是否启动网站设置向导
            /// </summary>
            [PropertyPad(0, 1, "",
            MainControlType = MainControlType.SimpleCheckBox,
            SimpleCheckBoxText = "useSiteGuideTxt"
            )]
            public static bool UseSiteGuide
            {
                get { return (bool)GetValue(itemsEle, "isSiteGuide", typeof(bool)); }
                set { SetValue(itemsEle, "isSiteGuide", value); }
            }

            //预览


            //发布
            
        }
    }
}

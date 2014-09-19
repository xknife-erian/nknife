using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    static public partial class SoftwareOption
    {
        [SoftOptionClass("SnipDesigner")]
        static public class SnipDesigner
        {
            /// <summary>
            /// edit by zhenghao at 2008-06-25 11:45
            /// 获取显示间距，（错误的 ，需要改）
            /// </summary>
            static public int DisplaySpace
            {
                get { return 3; }
                set { }
            }
            /// <summary>
            /// edit by zhenghao at 2008-06-20 14:00
            /// 获取圆角半径，（错误的 ，需要改）
            /// </summary>
            static public int PieRadii
            {
                get { return 10; }
                set { }
            }

            /// <summary>
            /// edit by zhenghao at 2008-06-20 14:00
            /// 获取是否使用线性渐变效果
            /// </summary>
            static public bool UsingLinearGradient
            {
                get { return true; }
                set { }
            }

            /// <summary>
            /// edit by zhenghao at 2008-06-18 14:00
            /// 获取是否默认添加频道PART，（错误的 ，需要改）
            /// </summary>
            static public Color FormBackColor
            {
                get { return Color.White; }
                set { } 
            }

            /// <summary>
            /// edit by zhenghao at 2008-06-18 14:00
            /// 获取是否默认添加频道PART，（错误的 ，需要改）
            /// </summary>
            static public bool ListBoxHasChannelPart
            {
                get { return true; }
                private  set { }
            }
            /// <summary>
            /// 当前类中所有元素的父元素
            /// </summary>
            static XmlElement itemsEle
            {
                get
                {
                    return GetItemElement("SnipDesigner");
                }
            }
            //--------------------------------------------------
            
            /// <summary>
            /// 模块框的颜色
            /// </summary>
            [PropertyPad(0, 0, "",
                //GroupBoxDockTop = true,
                //GroupBoxUseWinStyle = true,
                //GroupBoxUseWinStyleText = "SnipPageOtherColorTxt",
                LabelRight = "tmpltBorderColor",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color ItemPenColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "itemPenColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "itemPenColor", value);
                }
            }
            /// <summary>
            /// 容器模块框的颜色
            /// </summary>
            [PropertyPad(0, 1, "",
                LabelRight = "containerTempltColor",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color BoxPenColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "boxPenColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "boxPenColor", value);
                }
            }
            /// <summary>
            /// 被选择模块框的颜色
            /// </summary>
            [PropertyPad(0, 2, "",
                LabelRight = "tmpltCheckedColor",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color SelectedPenColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "selectedPenColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "selectedPenColor", value);
                }
            }
            /// <summary>
            /// 容器模块的颜色
            /// </summary>
            [PropertyPad(0, 3, "",
                LabelRight = "boxColor",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color BoxFillColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "boxFillColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "boxFillColor", value);
                }
            }
            /// <summary>
            /// 页面片设计器底面颜色
            /// </summary>
            [PropertyPad(0, 4, "",
                LabelRight = "designerBackColorTxt",
                MainControlType = MainControlType.ColorGeneralButton,
                MainControlWidth = 25
                )]
            static public Color DesignerBackColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "designerBackColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "designerBackColor", value);
                }
            }

            /// <summary>
            /// 获取或设置页面片设计器静态模块的颜色
            /// </summary>
            [PropertyPad(1, 0, "",
                LabelRight = "staticPartColorTxt",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color StaticPartColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "staticPartColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "staticPartColor", value);
                }
            }

            /// <summary>
            /// 获取或设置页面片设计器定制特性模块的颜色
            /// </summary>
            [PropertyPad(1, 1, "",
                LabelRight = "attributePartColorTxt",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color AttributePartColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "attributePartColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "attributePartColor", value);
                }
            }

            /// <summary>
            /// 文本的颜色     //程序里已调用,只是在选项不让其设置
            /// </summary>
            //[PropertyPad(1, 7, "",
            //    LabelRight = "txtColor",
            //    MainControlType = MainControlType.ColorSelectorButton,
            //    MainControlWidth = 25
            //    )]
            static public Color TextColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "textColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "textColor", value);
                }
            }

            /// <summary>
            /// 面包屑的颜色
            /// </summary>
            [PropertyPad(1, 2, "",
                LabelRight = "pathPartColorTxt",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color PathPartColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "pathPartColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "pathPartColor", value);
                }
            }

            /// <summary>
            ///  获取或设置页面片设计器导航（频道/栏目）模块的颜色
            /// </summary>
            [PropertyPad(1, 3, "",
                LabelRight = "navagationPartColorTxt",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color NavagationPartColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "navagationPartColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "navagationPartColor", value);
                }
            }

            ///---------------------------前景色-----------------------------------

            /// <summary>
            /// 面包屑型前景色
            /// </summary>
            [PropertyPad(2, 0, "",
                GroupBoxDockTop = true,
                GroupBoxUseWinStyle = true,
                GroupBoxUseWinStyleText = "foreColorSettingTxt",
                LabelRight = "pathPartTxt",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color PathPartForeColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "pathPartForeColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "pathPartForeColor", value);
                }
            }
            /// <summary>
            /// 属性型前景色
            /// </summary>
            [PropertyPad(2, 1, "",
                LabelRight = "attributePartForeColorTxt",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color AttributePartForeColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "attributePartForeColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "attributePartForeColor", value);
                }
            }
            /// <summary>
            /// 导航型前景色
            /// </summary>
            [PropertyPad(2, 2, "",
                LabelRight = "navagationPartForeColorTxt",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color NavagationPartForeColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "navagationPartForeColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "navagationPartForeColor", value);
                }
            }
            /// <summary>
            /// 静态型前景色
            /// </summary>
            [PropertyPad(2, 3, "",
                LabelRight = "staticPartForeColorTxt",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color StaticPartForeColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "staticPartForeColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "staticPartForeColor", value);
                }
            }
            /// <summary>
            /// 容器型景色
            /// </summary>
            [PropertyPad(2, 4, "",
                LabelRight = "boxForeColorTxt",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color BoxForeColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "boxForeColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "boxForeColor", value);
                }
            }
      
        }
    }
}
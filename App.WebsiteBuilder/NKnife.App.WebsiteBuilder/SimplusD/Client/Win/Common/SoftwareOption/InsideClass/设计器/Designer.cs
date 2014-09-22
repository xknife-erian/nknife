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
    /*
         static public partial class SoftwareOption
    {
        [SoftOptionClass("Designer")]
        static public class Designer
        {
            /// <summary>
            /// 当前类中所有元素的父元素
            /// </summary>
            static XmlElement itemsEle
            {
                get
                {
                    return GetItemElement("Designer");
                }
            }

            /// <summary>
            /// 模板的宽度
            /// </summary>
            [PropertyPad(0, 0, "templtWidthTxt",
                MainControlType = MainControlType.ValidateTextBox,
                ValidateTextBoxRegexText = "^[0-9]*$",
                ValidateTextBoxRegexTextRuntime = "^[0-9]*$",
                //GroupBoxUseWinStyle = true,
                //GroupBoxDockTop = true, //此时本想将其置顶
                LabelHelpWidth = -1
                )]
            static public int TmpltWidth 
            {
                get
                {
                    return (int)GetValue(itemsEle, "templtWidth", typeof(int));
                }
                set 
                {
                    SetValue(itemsEle, "templtWidth", value);
                }
            }

            /// <summary>
            /// 模板的高度
            /// </summary>
            [PropertyPad(0, 1, "templtHeightTxt",
                MainControlType = MainControlType.ValidateTextBox,
                ValidateTextBoxRegexText = "^[0-9]*$",
                ValidateTextBoxRegexTextRuntime = "^[0-9]*$"
                )]
            static public int TmpltHeight
            {
                get
                {
                    return (int)GetValue(itemsEle, "templtHeight", typeof(int));
                }
                set
                {
                    SetValue(itemsEle, "templtHeight", value);
                }
            }
            /// <summary>
            /// 是否使用背景图
            /// </summary>
            [PropertyPad(0, 2, "",
             MainControlType = MainControlType.SimpleCheckBox,
             LabelHelpWidth = -1,
             SimpleCheckBoxText = "userBackgroundPic"
             )]
            static public bool UserBackgroundPic 
            {
                get
                { 
                    return (bool)GetValue (itemsEle,"userBackgroundPic",typeof (bool));
                }
                set
                {
                    SetValue(itemsEle, "userBackgroundPic", value);
                }
            }
        }
    }
     */

}
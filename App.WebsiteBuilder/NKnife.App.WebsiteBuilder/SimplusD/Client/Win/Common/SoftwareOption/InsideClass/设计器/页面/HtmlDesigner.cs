using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using Jeelu.Win;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    static public partial class SoftwareOption
    {
        [SoftOptionClass("HtmlDesigner")]
        static public class HtmlDesigner
        {
            /// <summary>
            /// 当前类中所有元素的父元素
            /// </summary>
            static XmlElement itemsEle
            {
                get
                {
                    return GetItemElement("HtmlDesigner");
                }
            }

            [PropertyPad(0, 0, "fontTxt",
                GroupBoxUseWinStyle = true,
                GroupBoxDockTop = true,
                GroupBoxUseWinStyleText = "codeEditTxt",
                MainControlType = MainControlType.FontComboBox
                )]
            static public string TextFont
            {
                get
                {
                    return (string)GetValue(itemsEle, "documentFont", typeof(string));
                }
                set
                {
                    SetValue(itemsEle, "documentFont", value);
                }
            }

            [PropertyPad(0, 1, "sizeTxt",
            MainControlType = MainControlType.ComboBox,
            MainControlBindingFile = "Size.xml",
            ComboBoxStyle = ComboBoxStyle.DropDownList
            )]
            static public string TextSize
            {
                get
                {
                    return (string)GetValue(itemsEle, "documentSize", typeof(string));
                }
                set
                {
                    SetValue(itemsEle, "documentSize", value);
                }
            }

            /// <summary>
            /// 设计器上默认显示视图
            /// </summary>
            [PropertyPad(0, 2, "showViewTxt",
            MainControlType = MainControlType.ComboBox,
             ComboBoxStyle = ComboBoxStyle.DropDownList
            )]
            static public DesignerOpenType ShowView
            {
                get
                {
                    return (DesignerOpenType)GetValue(itemsEle, "showView", typeof(DesignerOpenType));
                }
                set
                {
                    SetValue(itemsEle, "showView", value);
                }
            }


            /// <summary>
            /// 边框样式
            /// </summary>
            [PropertyPad(0, 3, "tecBorderStyleTxt",
                MainControlType = MainControlType.ComboBox,
                 ComboBoxStyle = ComboBoxStyle.DropDownList
                )]
            static public BorderStyle TecBorderStyle
            {
                get
                {
                    return (BorderStyle)GetValue(itemsEle, "tecBorderStyle", typeof(BorderStyle));
                }
                set
                {
                    SetValue(itemsEle, "tecBorderStyle", value);
                }
            }

            [PropertyPad(1, 0, "",
                MainControlType = MainControlType.SimpleCheckBox,
                SimpleCheckBoxText = "showLineNum"
                )]
            static public bool IsShowLineNum
            {
                get
                {
                    return (bool)GetValue(itemsEle, "isShowLineNum", typeof(bool));
                }
                set
                {
                    SetValue(itemsEle, "isShowLineNum", value);
                }
            }

            /// <summary>
            /// 是否显示行结尾标识
            /// </summary>
            [PropertyPad(1, 1, "",
                MainControlType = MainControlType.SimpleCheckBox,
                SimpleCheckBoxText = "showEOLMarkerTxt"
                )]
            static public bool TecShowEOLMarkers
            {
                get
                {
                    return (bool)GetValue(itemsEle, "tecShowEOLMarkers", typeof(bool));
                }
                set
                {
                    SetValue(itemsEle, "tecShowEOLMarkers", value);
                }
            }

            /// <summary>
            /// 是否显示横向标尺
            /// </summary>
            [PropertyPad(1, 2, "",
            MainControlType = MainControlType.SimpleCheckBox,
            SimpleCheckBoxText = "showHRulerTxt"
            )]
            static public bool TecShowHRuler
            {
                get
                {
                    return (bool)GetValue(itemsEle, "tecShowHRuler", typeof(bool));
                }
                set
                {
                    SetValue(itemsEle, "tecShowHRuler", value);
                }
            }

            /// <summary>
            /// 是否显示横向标尺
            /// </summary>
            [PropertyPad(1, 3, "",
            MainControlType = MainControlType.SimpleCheckBox,
            SimpleCheckBoxText = "showVRulerTxt"
            )]
            static public bool TecShowVRuler
            {
                get
                {
                    return (bool)GetValue(itemsEle, "tecShowVRuler", typeof(bool));
                }
                set
                {
                    SetValue(itemsEle, "tecShowVRuler", value);
                }
            }

            /// <summary>
            /// 表格的边框的颜色,当鼠标移到表格上方时触发
            /// </summary>
            [PropertyPad(1,4, "",
                LabelRight = "tableBorderColorTxt",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color TableBorderColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "tableBorderColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "tableBorderColor", value);
                }
            }
        }
    }
}
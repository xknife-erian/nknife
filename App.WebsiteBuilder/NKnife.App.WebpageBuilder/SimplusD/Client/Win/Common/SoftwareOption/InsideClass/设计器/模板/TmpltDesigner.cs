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
        [SoftOptionClass("TmpltDesigner")]
        static public class TmpltDesigner
        {
            /// <summary>
            /// 当前类中所有元素的父元素
            /// </summary>
            static XmlElement itemsEle
            {
                get
                {
                    return GetItemElement("TmpltDesigner");
                }
            }

            #region
            /// <summary>
            /// 矩形被选中时刷子颜色
            /// </summary>
            [PropertyPad(0, 0, "",
                LabelRight = "selBrushTxt",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color RectSelectedBrush
            {
                get
                {
                    return (Color)GetValue(itemsEle, "selectedBrush", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "selectedBrush", value);
                }
            }

            /// <summary>
            /// 矩形被锁定时刷子颜色
            /// </summary>
            [PropertyPad(0, 1, "",
                LabelRight = "lockBrushTxt",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color LockedBrush
            {
                get
                {
                    return (Color)GetValue(itemsEle, "lockedBrush", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "lockedBrush", value);
                }
            }

            /// <summary>
            /// 含有页面片时刷子颜色
            /// </summary>
            [PropertyPad(0, 2, "",
                LabelRight = "hasSnipBrushTxt",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color HasSnipBrush
            {
                get
                {
                    return (Color)GetValue(itemsEle, "hasSnipBrush", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "hasSnipBrush", value);
                }
            }

            /// <summary>
            /// 含有正文页面片时刷子颜色
            /// </summary>
            [PropertyPad(0, 3, "",
                LabelRight = "hasConSnipBrushTxt",
                    MainControlType = MainControlType.ColorSelectorButton,
                    MainControlWidth = 25
                    )]
            static public Color HasContentSnipBrush
            {
                get
                {
                    return (Color)GetValue(itemsEle, "hasContentSnipBrush", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "hasContentSnipBrush", value);
                }
            }
            /// <summary>
            /// 鼠标划过矩形时的填充色
            /// </summary>
            [PropertyPad(0, 4, "",
                LabelRight = "selectingRectColorTxt",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color SelectingRectColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "selectingRectColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "selectingRectColor", value);
                }
            }
            #endregion

            #region
            /// <summary>
            /// 默认画笔颜色
            /// </summary>
            [PropertyPad(1, 0, "",
                LabelRight = "penColorTxt",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color PenColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "penColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "penColor", value);
                }
            }
            /// <summary>
            /// 指示线的颜色
            /// </summary>
            [PropertyPad(1, 1, "",
                LabelRight = "directLineColorTxt",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color DirectLineColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "directLineColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "directLineColor", value);
                }
            }

            /// <summary>
            /// 选择状态的直线颜色
            /// </summary>
            [PropertyPad(1, 2, "",
                LabelRight = "selLinePenColorTxt",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color SelectedLinePenColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "selectedLinePenColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "selectedLinePenColor", value);
                }
            }

            /// <summary>
            /// 待选择状态的直线颜色
            /// </summary>
            [PropertyPad(1, 3, "",
                LabelRight = "selingLineColorTxt",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color SelectingLineColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "selectingLineColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "selectingLineColor", value);
                }
            }

            /// <summary>
            /// 锁定状态的直线颜色
            /// </summary>
            [PropertyPad(1, 4, "",
                LabelRight = "lockLinePenColorTxt",
                MainControlType = MainControlType.ColorSelectorButton,
                MainControlWidth = 25
                )]
            static public Color LockedLinePenColor
            {
                get
                {
                    return (Color)GetValue(itemsEle, "lockedLinePenColor", typeof(Color));
                }
                set
                {
                    SetValue(itemsEle, "lockedLinePenColor", value);
                }
            }

            #endregion

            #region

            /// <summary>
            /// 画笔粗度
            /// </summary>
            [PropertyPad(2, 0, "",
                LabelRight = "penSizeTxt",
                MainControlType = MainControlType.NumericUpDown,
                MainControlWidth = 75,
                NumericUpDownMax = 5,
                NumericUpDownStep = 1,
                LabelHelpWidth = -1
                )]
            static public int PenSize
            {
                get { return (int)GetValue(itemsEle, "penSize", typeof(int)); }
                set { SetValue(itemsEle, "penSize", value); }
            }

            #endregion

            /// <summary>
            /// 加粗画笔之加粗倍数
            /// </summary>
            [PropertyPad(2, 1, "",
                
                LabelRight = "boldPenTimesTxt",
                MainControlType = MainControlType.NumericUpDown,
                MainControlWidth = 75,
                NumericUpDownMax = 5,
                NumericUpDownStep = 1,
                LabelHelpWidth = -1
                )]
            static public int BoldPenTimes
            {
                get { return (int)GetValue(itemsEle, "boldPenTimes", typeof(int)); }
                set { SetValue(itemsEle, "boldPenTimes", value); }
            }
            /// <summary>
            /// 自动吸附的距离
            /// </summary>
            [PropertyPad(2, 2, "",
                LabelRight = "autoAdsorbTxt",
                MainControlType = MainControlType.NumericUpDown,
                MainControlWidth = 75,
                NumericUpDownMax = 5,
                NumericUpDownStep = 1,
                LabelHelpWidth = -1
                )]
            static public int AutoAdsorb
            {
                get { return (int)GetValue(itemsEle, "autoAdsorb", typeof(int)); }
                set { SetValue(itemsEle, "autoAdsorb", value); }
            }

            /// <summary>
            /// 选择对象偏移误差范围
            /// </summary>
            [PropertyPad(2, 3, "",
                LabelRight = "selectPrecisionTxt",
                MainControlType = MainControlType.NumericUpDown,
                MainControlWidth = 75,
                NumericUpDownMax = 5,
                NumericUpDownStep = 1,
                LabelHelpWidth = -1
                )]
            static public int SelectPrecision
            {
                get { return (int)GetValue(itemsEle, "selectPrecision", typeof(int)); }
                set { SetValue(itemsEle, "selectPrecision", value); }
            }
            /// <summary>
            /// 指示线的绘画的偏移距离
            /// </summary>
            [PropertyPad(2, 4, "",
                LabelRight = "offsetDirectionLineTxt",
                MainControlType = MainControlType.NumericUpDown,
                MainControlWidth = 75,
                NumericUpDownMax = 5,
                NumericUpDownStep = 1,
                LabelHelpWidth = -1
                )]
            static public int OffsetDirectionLine
            {
                get { return (int)GetValue(itemsEle, "offsetDirectionLine", typeof(int)); }
                set { SetValue(itemsEle, "offsetDirectionLine", value); }
            }
            /// <summary>
            /// 当前的偏移量
            /// </summary>
            [PropertyPad(2, 5, "",
                LabelRight = "currentPrecisionTxt",
                MainControlType = MainControlType.NumericUpDown,
                MainControlWidth = 75,
                NumericUpDownMax = 5,
                NumericUpDownStep = 1,
                LabelHelpWidth = -1
                )]
            static public int CurrentPrecision
            {
                get { return (int)GetValue(itemsEle, "currentPrecision", typeof(int)); }
                set { SetValue(itemsEle, "currentPrecision", value); }
            }

            /// <summary>
            /// 是否显示标尺
            /// </summary>
            [PropertyPad(2, 6, "",
             MainControlType = MainControlType.SimpleCheckBox,
             SimpleCheckBoxText = "showRulerTxt"
             )]
            static public bool ShowRuler
            {
                get { return (bool)GetValue(itemsEle, "showRuler", typeof(bool)); }
                set { SetValue(itemsEle, "showRuler", value); }
            }
            /// <summary>
            /// 是否显示工具栏
            /// </summary>
            //[PropertyPad(3, 0, "",
            // MainControlType = MainControlType.SimpleCheckBox,
            // SimpleCheckBoxText = "%显示工具栏"
            // )]
            static public bool ShowToolBar
            {
                get { return (bool)GetValue(itemsEle, "showToolBar", typeof(bool)); }
                set { SetValue(itemsEle, "showToolBar", value); }
            }


        }
    }
}
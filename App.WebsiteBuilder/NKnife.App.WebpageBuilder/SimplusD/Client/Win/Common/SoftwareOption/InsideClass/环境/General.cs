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
        [SoftOptionClass("General")]
        static public class General
        {
            /// <summary>
            /// 当前类中所有元素的父元素
            /// </summary>
            static XmlElement itemsEle 
            {
                get
                {
                    return GetItemElement("General");
                }
            }

            /// <summary>
            /// 当前应用程序的国际化语言
            /// </summary>
            [PropertyPad(0, 0, "",
                MainControlType = MainControlType.TextBox,
                MainControlWidth = 400,
                LabelTop = "languageList"
                )]
            static public string ApplicationLanguage
            {
                get
                {
                    return (string)GetValue(itemsEle, "applicationLanguage", typeof(string));
                }
                set
                {
                    SetValue(itemsEle, "applicationLanguage", value);
                }
            }

            /// <summary>
            /// 启动时的显示情况(下拉列表)
            /// </summary>
            [PropertyPad(0, 1, "",
                MainControlType = MainControlType.ComboBox,
                MainControlWidth = 400,
                MainControlBindingFile = "Starting.xml",
                ComboBoxStyle = ComboBoxStyle.DropDownList,
                LabelTop = "startUpTxt"
                )]
            static public string StartInterfaceState
            {
                get
                {
                    return (string)GetValue(itemsEle, "startInterfaceState", typeof(string));
                }
                set
                {
                    SetValue(itemsEle, "startInterfaceState", value);
                }
            }

            /// <summary>
            /// 最近使用的列表中显示的项
            /// </summary>
            [PropertyPad(0, 2, "",
                MainControlType = MainControlType.NumericUpDown,
                NumericUpDownMin = 0,
                NumericUpDownMax = 30,
                NumericUpDownStep = 1,
                MainControlWidth = 50,
                LabelRight = "recentList"
                )]
            static public int RecentUseItem
            {
                get
                {
                    return (int)GetValue(itemsEle, "recentUseItem", typeof(int));
                }
                set
                {
                    SetValue(itemsEle, "recentUseItem", value);
                }
            }

            /// <summary>
            /// 还原文件关联
            /// </summary>
            [PropertyPad(0, 3, "",
                MainControlType = MainControlType.Button,
                ButtonText = "revertFileSourceTxt",
                MainControlWidth = 120
            )]
            static public void ResetOptionProperty()
            {
                Service.FileBinding.Initialize();
                string tip = ResourceService.GetResourceText("option.buttonTip");
                MessageBox.Show(tip, "Jeelu.SimplusD", MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Jeelu.Win
{
    [ResReader(false)]
    public partial class BlockCssControl : CssMainControl
    {
        const string WORD_SPACING = "word-spacing";
        const string LETTER_SPACING = "letter-spacing";
        const string VERTICL_ALIGN = "vertical-align";
        const string TEXT_ALIGN = "text-align";
        const string TEXT_INDEN = "text-indent";
        const string WHITE_SPACE = "white-space";
        const string DISPLAY = "display";

        /// <summary>
        /// 内容设置有问题的控件集
        /// </summary>
        private List<KeyValuePair<Control, string>> _errors = new List<KeyValuePair<Control, string>>();

        public BlockCssControl()
        {
            InitializeComponent();
            if (!this.DesignMode)
            {
                Init();
            }

        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public override void EnterLoad()
        {
            InitControls();
            base.EnterLoad();
        }

        public override bool LeaveValidate()
        {
            foreach (Control control in Controls)
            {
                string curText = "";
                bool bErroe = false;
                Type type = control.GetType();
                if (type == typeof(ComboBox))
                {
                    ComboBox cmb = (ComboBox)control;
                    curText = cmb.Text;
                    switch (cmb.Name)
                    {
                        case "cb_TextSimilar":
                            bErroe = CssResources.CheckValue(TEXT_ALIGN, curText, false);
                            break;
                        case "cb_Space":
                            bErroe = CssResources.CheckValue(WHITE_SPACE, curText, false);
                            break;
                        case "cb_Show":
                            bErroe = CssResources.CheckValue(DISPLAY, curText, false);
                            break;
                        default:
                            break;
                    }
                    if (bErroe)
                    {
                        if (!CssUtility.ShowNotStandard(curText))
                        {
                            control.Focus();
                            return false;
                        }
                    }
                }
                else if (type == typeof(CssFieldUnit))
                {
                    CssFieldUnit cfu = (CssFieldUnit)control;
                    curText = cfu.Value;
                    if (!string.IsNullOrEmpty(curText))
                    {
                        if (!cfu.CheckValue())
                        {
                            control.Focus();
                            return false;
                        }
                    }

                }

            }
            SaveCssValue();
            return base.LeaveValidate();
        }
        /// <summary>
        /// 保存CSS的值
        /// </summary>
        private void  SaveCssValue()
        {
            //单词间距
            CssResourceList list = CssResources.Resources["wordSpaceUnit"];
            string str = css_WordSpace.Value;
            if (list.HasValue(str))
            {
                str = list.GetValue(str);
            }
            Value.Properties[WORD_SPACING] = str;

            //字母间距
            list = CssResources.Resources["letterSpaceUnit"];
            str = css_CharSpace.Value;
            if (list.HasValue(str))
            {
                str = list.GetValue(str);
            }
            Value.Properties[LETTER_SPACING] = str;

            //垂直对齐
            list = CssResources.Resources[VERTICL_ALIGN];
            str = css_VertiSimilar.Value;
            if (list.HasValue(str))
            {
                str = list.GetValue(str);
            }
            Value.Properties[VERTICL_ALIGN] = str;

            //文本对齐
            list = CssResources.Resources[TEXT_ALIGN];
            str = cb_TextSimilar.Text;
            if (list.HasValue(str))
            {
                str = list.GetValue(str);
            }
            Value.Properties[TEXT_ALIGN] = str;

            //文本缩进
           // list = CssResources.Resources[TEXT_INDEN];
            str = css_TextIndet.Value;
            //if (list.HasValue(str))
            //{
            //    str = list.GetValue(str);
            //}
            Value.Properties[TEXT_INDEN] = str;

            //空格
            list = CssResources.Resources[WHITE_SPACE];
            str = cb_Space.Text;
            if (list.HasValue(str))
            {
                str = list.GetValue(str);
            }
            Value.Properties[WHITE_SPACE] = str;

            //显示
            list = CssResources.Resources[DISPLAY];
            str = cb_Show.Text;
            if (list.HasValue(str))
            {
                str = list.GetValue(str);
            }
            Value.Properties[DISPLAY] = str;

        }
        /// <summary>
        /// 填充控件
        /// </summary>
        private void InitControls()
        {
            string strCssValue = "";
            string strtmp = "";
            //单词间距
            if (this.Value.Properties.TryGetValue(WORD_SPACING,out strCssValue))
            {
                css_WordSpace.Value = strCssValue;
            }
            //字母间距
            if (this.Value.Properties.TryGetValue(LETTER_SPACING, out strCssValue))
            {
                css_CharSpace.Value = strCssValue;
            }
            //垂直对齐
            if (this.Value.Properties.TryGetValue(VERTICL_ALIGN, out strCssValue))
            {
                css_VertiSimilar.Value = strCssValue;
            }
            //文本对齐
            if (this.Value.Properties.TryGetValue(TEXT_ALIGN, out strCssValue))
            {
                strtmp = CssResources.Resources[TEXT_ALIGN].ValueToText(strCssValue);
                cb_TextSimilar.Text = string.IsNullOrEmpty(strtmp) ? strCssValue : strtmp; 
               
            }
            //文本缩进
            if (this.Value.Properties.TryGetValue(TEXT_INDEN, out strCssValue))
            {
                //strtmp = CssResources.Resources[TEXT_INDEN].ValueToText(strCssValue);
                //css_TextIndet.Value = string.IsNullOrEmpty(strtmp) ? strCssValue : strtmp;
                css_TextIndet.Value = strCssValue;
                
            }
            //空格
            if (this.Value.Properties.TryGetValue(WHITE_SPACE, out strCssValue))
            {
                strtmp = CssResources.Resources[WHITE_SPACE].ValueToText(strCssValue);
                cb_Space.Text = string.IsNullOrEmpty(strtmp) ? strCssValue : strtmp;               
                
            }
            //显示
            if (this.Value.Properties.TryGetValue(DISPLAY, out strCssValue))
            {
                strtmp = CssResources.Resources[DISPLAY].ValueToText(strCssValue);
                cb_Show.Text = string.IsNullOrEmpty(strtmp) ? strCssValue : strtmp; 

            }

        }
         /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {

            //单词间距,字母间距,垂直对齐,文本缩进

            //文本对齐
            CssResourceList list = CssResources.Resources[TEXT_ALIGN];
            foreach (string text in list.Texts)
            {
                cb_TextSimilar.Items.Add(text);
            }

            //空格
            list = CssResources.Resources[WHITE_SPACE];
            foreach (string text in list.Texts)
            {
                cb_Space.Items.Add(text);
            }
            //显示
            list = CssResources.Resources[DISPLAY];
            foreach (string text in list.Texts)
            {
                cb_Show.Items.Add(text);
            }
        }

    }
}

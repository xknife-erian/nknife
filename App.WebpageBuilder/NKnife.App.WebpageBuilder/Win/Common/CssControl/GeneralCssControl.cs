using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
namespace Jeelu.Win
{
    [ResReader(false)]
    public partial class GeneralCssControl : CssMainControl
    {
        const string FONT_FAMILY = "font-family";
        const string FONT_SIZE = "font-size";
        const string FONT_WEIGHT = "font-weight";
        const string FONT_STYLE = "font-style";        
        const string FONT_VARIANT = "font-variant";
        
        //const string FONT_HEIGHT = "font-height";//modified by fenggy on 2008年5月28日:行高的CSS应该为 line-height
        const string FONT_HEIGHT = "line-height";
        const string TEXT_TRANSFORM = "text-transform";
        const string TEXT_DECORATION = "text-decoration";
        const string COLOR = "color";

        #region 公共属性

        private List<string> fontlists = new List<string>();

        #endregion

        public GeneralCssControl()
        {
            InitializeComponent();
            InitEvents();

            if (!this.DesignMode)
            {
                Init();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            //大小 行高

            //样式
            CssResourceList list = CssResources.Resources[FONT_STYLE];
            foreach (string text in list.Texts)
            {
                cb_FontMode.Items.Add(text);
            }
            //粗细
            list = CssResources.Resources[FONT_WEIGHT];
            foreach (string text in list.Texts)
            {
                cb_FontThick.Items.Add(text);
            }
            //变体
            list = CssResources.Resources[FONT_VARIANT];
            foreach (string text in list.Texts)
            {
                cb_Anamorphosis.Items.Add(text);
            }
            //大小写
            list = CssResources.Resources[TEXT_TRANSFORM];
            foreach (string text in list.Texts)
            {
                cb_CapsLock.Items.Add(text);
            }
            //修饰符,颜色  EnterLoad 设置

            //字体
            InitComboBoxFont(true);
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
                        case "cb_FontThick": //粗细
                            bErroe = CssResources.CheckValue(FONT_WEIGHT, curText, false);
                            break;
                        case "cb_Anamorphosis"://变体
                            bErroe = CssResources.CheckValue(FONT_VARIANT, curText, false);
                            break;
                        case "cb_FontMode"://样式
                            bErroe = CssResources.CheckValue(FONT_STYLE, curText, false);
                            break;
                        case "cb_CapsLock"://大小写
                            bErroe = CssResources.CheckValue(TEXT_TRANSFORM, curText, false);
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
                else if (type == typeof(ColorTextBoxButton))
                {
                    curText = colorText_FontClolor.ColorText;
                    if (!this.colorText_FontClolor.CheckColor)
                    {
                        CssUtility.ShowNotStandardColorMsg(curText);
                        control.Focus();
                        return false;
                    }
                }
            }
            SaveCssValue();
            return base.LeaveValidate();
        }

        /// <summary>
        /// 保存CSS的值
        /// </summary>
        private void SaveCssValue()
        {
            //字体                   
            
            Regex r = new Regex("[^\x00-\xff]"); //匹配汉字
            char c = '"';

            string strsign = c.ToString(); //双引号            
            string str = cb_Font.Text;   
            string []strFont = str.Split(new char[] { ',' });

            //对字体做一些处理
            str = "";
            foreach(string s in strFont)
            {
                string strtemp = s.Trim();
                if (strtemp.IndexOf(" ") >= 0 && !strtemp.StartsWith(strsign) && !strtemp.EndsWith(strsign))  //字体中有空格
                    str += '"' + strtemp + '"' + ", ";
                else if (r.Matches(s).Count > 0 && !strtemp.StartsWith(strsign) && !strtemp.EndsWith(strsign)) //字体中有汉字
                    str += '"' + strtemp + '"' + ", ";
                else str += strtemp + ", ";
            }
           //str = str.TrimEnd(new char[] { ' ' });//去掉最后一个空格
            str = str.Remove(str.Length - 2);//去掉最后的 逗号和空格
            Value.Properties[FONT_FAMILY] = str;

            //大小
            CssResourceList list = CssResources.Resources[FONT_SIZE];
            str = css_FontSize.Value;
            if (list.HasValue(str))
            {
                str = list.GetValue(str);
            }
            Value.Properties[FONT_SIZE] = str;

            //粗细
            list = CssResources.Resources[FONT_WEIGHT];
            str = cb_FontThick.Text;
            if (list.HasValue(str))
            {
                str = list.GetValue(str);
            }
            Value.Properties[FONT_WEIGHT] = str;

            //样式
            list = CssResources.Resources[FONT_STYLE];
            str =  cb_FontMode.Text;
            if (list.HasValue(str))
            {
                str = list.GetValue(str);
            }
            Value.Properties[FONT_STYLE] = str;
            //变体
            list = CssResources.Resources[FONT_VARIANT];
            str = cb_Anamorphosis.Text;
            if (list.HasValue(str))
            {
                str = list.GetValue(str);
            }
            Value.Properties[FONT_VARIANT] = str;

            //行高
            list = CssResources.Resources["normal"];
            str = css_FontRowHeight.Value;
            if (list.HasValue(str))
            {
                str = list.GetValue(str);
            }
            Value.Properties[FONT_HEIGHT] = str;

            //大小写
            list = CssResources.Resources[TEXT_TRANSFORM];
            str = cb_CapsLock.Text;
            if (list.HasValue(str))
            {
                str = list.GetValue(str);
            }
            Value.Properties[TEXT_TRANSFORM] = str;

            //修饰
            if (chk_None.Checked) str = "none";
            else
            {
                str ="";
                if (chk_Underline.Checked) str += "underline ";
                if (chk_Upline.Checked) str += "overline ";
                if (chk_Delline.Checked) str += "line-through ";
                if (chk_Ray.Checked) str += "blink ";
                if(str.Length > 0)
                str = str.Remove(str.Length - 1);//去掉最后一个空格
            }            
            Value.Properties[TEXT_DECORATION] = str;

            //颜色
            str = colorText_FontClolor.ColorText;
            Value.Properties[COLOR] = str;

        }

        #region 内部函数 事件消息

         /// <summary>
        /// 填充控件
        /// </summary>
        private void InitControls()
        {
            string strCssValue = "";
            string strtmp = "";

            //字体  另处理
            SetComboBoxFont();
            //大小
            if (this.Value.Properties.TryGetValue(FONT_SIZE, out strCssValue))
            {
                css_FontSize.Value = strCssValue;
            }
            //粗细
            if (this.Value.Properties.TryGetValue(FONT_WEIGHT, out strCssValue))
            {
                strtmp = CssResources.Resources[FONT_WEIGHT].ValueToText(strCssValue);
                cb_FontThick.Text = string.IsNullOrEmpty(strtmp) ? strCssValue : strtmp; 
            }

            //样式
            if (this.Value.Properties.TryGetValue(FONT_STYLE, out strCssValue))
            {
                strtmp = CssResources.Resources[FONT_STYLE].ValueToText(strCssValue);
                cb_FontMode.Text = string.IsNullOrEmpty(strtmp) ? strCssValue : strtmp;               
            }
            //变体
            if (this.Value.Properties.TryGetValue(FONT_VARIANT, out strCssValue))
            {
                strtmp = CssResources.Resources[FONT_VARIANT].ValueToText(strCssValue);
                cb_Anamorphosis.Text  = string.IsNullOrEmpty(strtmp) ? strCssValue : strtmp; 
            }
            //行高
            if (this.Value.Properties.TryGetValue(FONT_HEIGHT, out strCssValue))
            {
                css_FontRowHeight.Value = strCssValue;
            }
            //大小写
            if (this.Value.Properties.TryGetValue(TEXT_TRANSFORM, out strCssValue))
            {
                strtmp = CssResources.Resources[TEXT_TRANSFORM].ValueToText(strCssValue);
                cb_CapsLock.Text  = string.IsNullOrEmpty(strtmp) ? strCssValue : strtmp; 
            }
            //修饰
            if (this.Value.Properties.TryGetValue(TEXT_DECORATION, out strCssValue))
            {
                if (strCssValue.IndexOf("none") >= 0)
                {
                    chk_None.Checked = true;
                    chk_Underline.Checked = false;
                    chk_Upline.Checked = false;
                    chk_Delline.Checked = false;
                    chk_Ray.Checked = false;
                }
                else
                {
                    if (strCssValue.IndexOf("underline") >= 0) chk_Underline.Checked = true;
                    if (strCssValue.IndexOf("overline") >= 0) chk_Upline.Checked = true;
                    if (strCssValue.IndexOf("line-through") >= 0) chk_Delline.Checked = true;
                    if (strCssValue.IndexOf("blink") >= 0) chk_Ray.Checked = true;
                }
            }
            //颜色
            if (this.Value.Properties.TryGetValue(COLOR, out strCssValue))
            {
                colorText_FontClolor.ColorText = strCssValue;
            }

        }
        /// <summary>
        /// 填充字体COMBOX
        /// </summary>
        private void SetComboBoxFont()
        {
            string strCssValue = "";
            if (this.Value.Properties.TryGetValue(FONT_FAMILY, out strCssValue)) //没有值要清空
            {
                //如果就一个字体 并且如果带双引号的情况下,显示的时候要去掉 ,其他原样输出
                char c = '"';
                string str = c.ToString();
                if (strCssValue.IndexOf(",") < 0 && strCssValue.StartsWith(str) && strCssValue.EndsWith(str))
                    strCssValue = strCssValue.Substring(1, strCssValue.Length - 2);//去掉双引号
                cb_Font.Text = strCssValue;
            }
            else
            {
                cb_Font.Text = "";
            }
        }
        /// <summary>
        /// 初始化事件
        /// </summary>
        private void InitEvents()
        {
            cb_Font.SelectedIndexChanged += new EventHandler(cb_Font_SelectedIndexChanged);
        }


        private void cb_Font_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Font.SelectedIndex == cb_Font.Items.Count - 1)
            {
                FontListEditorForm form = new FontListEditorForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.listStr.Count <= 0) return;

                    fontlists = form.listStr;
                    InitComboBoxFont(false);
                    return;
                }
                else
                {
                    cb_Font.Text = "";
                }
            }
        }

        #endregion

        /// <summary>
        /// 初始化字体选择框
        /// </summary>
        /// <param name="isInit"></param>
        private void InitComboBoxFont(bool isInit)
        {
            if (isInit)
            {
                string path = Path.Combine(Application.StartupPath, "Config/fontlist.xml");
                XmlDocument fontListDoc = new XmlDocument();
                if (File.Exists(path))
                {
                    try
                    {
                        fontListDoc.Load(path);
                        XmlNodeList xnl = fontListDoc.DocumentElement.ChildNodes;
                        if (xnl != null)
                        {
                            foreach (XmlNode node in xnl)
                            {
                                fontlists.Add(node.InnerText);
                            }
                        }
                    }
                    catch
                    {
                        File.Delete(path);
                        throw;
                    }
                }
            }
            cb_Font.Items.Clear();
            foreach (string str in fontlists)
            {
                cb_Font.Items.Add(str);
            }
            cb_Font.Items.Add(StringParserService.Parse("${res:TextpropertyPanel.fontname.editfontlist}"));
        }
        private void chk_Ray_Click(object sender, EventArgs e)
        {
            if ( ((CheckBox)sender).Checked == true)
            {
                if(chk_None.Checked == true) // 加个判断
                chk_None.Checked = false;
            }
        }

        private void chk_None_Click(object sender, EventArgs e)
        {
            if(chk_None.Checked)
            {
                chk_Underline.Checked = false;
                chk_Upline.Checked = false;
                chk_Delline.Checked = false;
                chk_Ray.Checked = false;
            }
        }
               
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 当前的类型
    /// </summary>
    public enum CurType 
    {
        None = 0,
        Tmplt = 1,
        Snip = 2,
        Part = 3,
        WebPage = 4
    }

    public partial class CssSetupForm : BaseForm
    {
        #region 构造函数

        /// <summary>
        /// 矩形或页面片内部组成部分的Css设置
        /// </summary>
        /// <param name="partCss">当前的Css字符串</param>
        public CssSetupForm( string partCss,CurType curType)
        {
            InitializeComponent();
            if (!Service.Util.DesignMode)
            {
                this.tabControlGeneral.TabPages.Clear();
            }

            CSSText = partCss;
            _curType = curType;
            switch (_curType)
            {
                case CurType.None:
                    break;
                case CurType.Tmplt:
                    break;
                case CurType.Snip:
                   // cssDesignerDisplayControl.NumericUpDown.Enabled = false;
                    groupBoxLinkOption.Visible = false;
                    checkBoxUsingCurImg.Visible = true;
                    Text = StringParserService.Parse("${res:tmpltDesign.tmpltDrawPanel.cssSetupFormText.snip}");
                    break;
                case CurType.Part:
                    groupBoxLinkOption.Visible = false;
                   // cssDesignerDisplayControl.NumericUpDown.Enabled = true;
                    checkBoxUsingCurImg.Visible = false;
                    Text = StringParserService.Parse("${res:tmpltDesign.tmpltDrawPanel.cssSetupFormText.part}");
                    break;
                case CurType.WebPage:
                    break;
                default:
                    break;
            }
            checkBoxIsAutoSize.Enabled = false;
            tabControlGeneral.TabPages.Add(this.tabPageLayout);
            tabControlGeneral.TabPages.Add(this.tabPageBackGroud);
            tabControlGeneral.TabPages.Add(this.tabPageBorder);
            tabControlGeneral.TabPages.Add(this.tabPageFont);
            //InitEvent();
        }

        /// <summary>
        /// 页面的Css设置
        /// </summary>
        /// <param name="doc">对应的模板文档</param>
        public CssSetupForm(TmpltXmlDocument doc)
        {
            InitializeComponent();
            if (!Service.Util.DesignMode)
            {
                this.tabControlGeneral.TabPages.Clear();
            }
            Doc = doc;
            XmlElement ele = doc.DocumentElement;
            CSSText = ele.GetAttribute("webCss");
            _webPageDicA = CssSection.Parse(ele.GetAttribute("a"));
            _webPageDicA_visited = CssSection.Parse(ele.GetAttribute("a_visited"));
            this.checkBoxUsingCurImg.Visible = false;
            checkBoxIsAutoSize.Enabled = true;
            tabControlGeneral.TabPages.Add(this.tabPageBackGroud);
            tabControlGeneral.TabPages.Add(this.tabPageBorder);
            tabControlGeneral.TabPages.Add(this.tabPageFont);
            this.groupBoxLinkOption.Visible = true;
            //this.cssDesignerDisplayControl.NumericUpDown.Enabled = true;
            _curType = CurType.WebPage;
            this.textBoxCurType.Text = StringParserService.Parse("${res:tmpltDesign.tmpltDrawPanel.cssSetupFormType.tmplt}");
            this.Text = StringParserService.Parse("${res:tmpltDesign.tmpltDrawPanel.cssSetupFormText.tmplt}");
            _tmpltID = doc.Id;
            this.textBoxTitle.Enabled = false;
            this.textBoxTitle.Text = ele.GetAttribute("title");
            //InitEvent();            
        }

        /// <summary>
        ///  初始化控件的事件
        /// </summary>
        void InitEvent()
        {
            //背景颜色变化
            btnSelectBackColor.MyColorChanged += new EventHandler(btnSelectBackColor_MyColorChanged);

            //添加内容选项变化事件响应
            cssGeneralContectControl.CssChanged += new EventHandler(cssGeneralContectControl_CssChanged);

            //添加布局选项变化事件响应
            cssGeneralLayoutControl.CssChanged += new EventHandler(cssGeneralLayoutControl_CssChanged);

            colorBtnBefore.MyColorChanged +=new EventHandler(colorBtnBefore_MyColorChanged);

            colorBtnLast.MyColorChanged += new EventHandler(colorBtnLast_MyColorChanged);

            myBoxModelPanel.CssChanged += new EventHandler(myBoxModelPanel_CssChanged);

            myFont.CssChanged += new EventHandler(myFont_CssChanged);

            myFont.CssSection = cssSection;

        }
        
        #endregion

        #region 内部变量

        CssSection cssSection = new CssSection();

        CssSection  _webPageDicA = new CssSection();

        CssSection _webPageDicImg = new CssSection();

        CssSection _webPageDicA_visited = new CssSection();

        private CurType _curType = CurType.None;

        private TmpltXmlDocument Doc;

        private string _tmpltID;

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置模板背景图片的URL
        /// </summary>
        public string TmpltBackImgUrl { get; set; }

        /// <summary>
        /// 获取标题
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// 获取或设置CSS字符串
        /// </summary>
        public string CSSText
        {
            get { return cssSection.ToString(); }
            set 
            {
                cssSection = CssSection.Parse(value);
                
            }
        }

        /// <summary>
        /// 获取在页面片设计器中Part的显示高度（20的倍数）
        /// </summary>
        public int SnipDesignerPartHeightMultiple { get; private set; }

        /// <summary>
        /// 获取或设置是否使用当前背景
        /// </summary>
        public bool UseCurBackImg { get; set; }

        #endregion

        #region 控件事件 Css变化

        /// <summary>
        /// 窗体运行时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CssSetupForm_Load(object sender, EventArgs e)
        {
           // cssDesignerDisplayControl.NumericUpDown.Value = 2;
            cssGeneralContectControl.InitValue(cssSection);
            cssGeneralLayoutControl.InitValue(cssSection);
            myFont.InitValue(cssSection);
            myBoxModelPanel.InitValue(cssSection);

            string strValue;

            if (_curType == CurType.WebPage)
            {
                if (_webPageDicA.Properties.TryGetValue("color", out strValue))
                {
                    colorBtnBefore.MyColor = ColorTranslator.FromHtml(strValue);
                    textBoxBeforeColor.Text = strValue;
                }
                if (_webPageDicA.Properties.TryGetValue("text-decoration", out strValue))
                {
                    switch (strValue)
                    {
                        case "none":
                            checkBoxUnderLine.Checked = false;
                            break;
                        default:
                            checkBoxUnderLine.Checked = true;
                            break;
                    }
                }
                if (_webPageDicA_visited.Properties.TryGetValue("color", out strValue))
                {
                    colorBtnLast.MyColor = ColorTranslator.FromHtml(strValue);
                    textBoxLastColor.Text = strValue;
                }
                if (_webPageDicImg.Properties.TryGetValue("border", out strValue))
                {
                    checkBoxImgBorder.Checked = strValue == "0" ? false : true;
                }
            }
            if (cssSection.Properties.TryGetValue("background-color", out strValue))
            {
                textBoxBackColor.Text = strValue;
                btnSelectBackColor.MyColor = ColorTranslator.FromHtml(strValue);
                checkBoxUsingBackColor.Checked = true;
            }
            if (cssSection.Properties.TryGetValue("background-repeat", out strValue))
            {
                switch (strValue)
                {
                    case "no-repeat":
                        comboBoxBackgroudLayout.SelectedIndex = 0;
                        break;
                    case "repeat":
                        comboBoxBackgroudLayout.SelectedIndex = 1;
                        break;
                    case "repeat-x":
                        comboBoxBackgroudLayout.SelectedIndex = 2;
                        break;
                    case "repeat-y":
                        comboBoxBackgroudLayout.SelectedIndex = 3;
                        break;
                    default:
                        break;
                }
            }
            if (cssSection.Properties.TryGetValue("background-image", out strValue) && cssSection.Properties["background-image"] != @"url()")
            {
                checkBoxUsingBackImg.Checked = true;
            }

            else
            {
                checkBoxUsingBackImg.Checked = false;
                comboBoxBackgroudLayout.SelectedIndex = 0;
            }
            
            treeCSSType.Nodes[0].Checked = true;
            InitEvent();
        }

        /// <summary>
        /// 点击“确定”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void buttonOK_Click(object sender, EventArgs e)
        {
            SaveCSSSetup();
        }

        /// <summary>
        /// 点击“应用”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void buttonUsed_Click(object sender, EventArgs e)
        {
            SaveCSSSetup();
        }

        /// <summary>
        /// 左侧树型选项的消息响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void treeCSSType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (treeCSSType.SelectedNode.Name)
            {
                case "_generalNode":
                    labelCurrentContent.Text = treeCSSType.SelectedNode.Text;
                    tabControlGeneral.Visible = true;
                    break;
                case "_advanceNode":
                    labelCurrentContent.Text = treeCSSType.SelectedNode.Text;
                    tabControlGeneral.Visible = false;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 是否显示css字符串的文本框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void checkBoxDisplayCSSText_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDisplayCSSText.Checked)
            {
                textBoxCSSText.Visible = true;
                buttonOK.Location = new Point(510, 349);
                buttonExit.Location = new Point(574, 349);
                Size = new Size(653, 403);
            }
            else
            {
                textBoxCSSText.Visible = false;
                buttonOK.Location = new Point(510, 313);
                buttonExit.Location = new Point(574, 313);
                Size = new Size(653, 369);
            }
        }

        /// <summary>
        /// 页面片设计器中Part的显示高度（20的倍数）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
           // SnipDesignerPartHeightMultiple = (int)cssDesignerDisplayControl.NumericUpDown.Value;
        }        

        /// <summary>
        /// 内容选项部分响应Css变化消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cssGeneralContectControl_CssChanged(object sender, EventArgs e)
        {
            textBoxCSSText.Text = cssSection.ToString() + myBoxModelPanel.ToString(); ;
        }

        /// <summary>
        /// 布局选项部分响应Css变化消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cssGeneralLayoutControl_CssChanged(object sender, EventArgs e)
        {
            textBoxCSSText.Text = cssSection.ToString() + myBoxModelPanel.ToString(); ;
        }

        /// <summary>
        /// 边框响应Css变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void myBoxModelPanel_CssChanged(object sender, EventArgs e)
        {
            textBoxCSSText.Text = cssSection.ToString() + myBoxModelPanel.ToString();
        }

        /// <summary>
        /// 字体响应Css变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void myFont_CssChanged(object sender, EventArgs e)
        {
            textBoxCSSText.Text = cssSection.ToString() + myBoxModelPanel.ToString();
        }

        /// <summary>
        /// 背景颜色响应Css变化 按钮选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnSelectBackColor_MyColorChanged(object sender, EventArgs e)
        {
            textBoxBackColor.Text = btnSelectBackColor.ColorArgbString;
            cssSection.Properties["background-color"] = btnSelectBackColor.ColorArgbString;
            textBoxCSSText.Text = cssSection.ToString() + myBoxModelPanel.ToString();;
        }

        /// <summary>
        /// 背景颜色响应Css变化 文本输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void textBoxBackColor_TextChanged(object sender, EventArgs e)
        {
            if (checkBoxUsingBackColor.Checked)
            {
                cssSection.Properties["background-color"] = textBoxBackColor.Text;
            }
            else
            {
                cssSection.Properties["background-color"] = string.Empty;
            }
            textBoxCSSText.Text = cssSection.ToString() + myBoxModelPanel.ToString(); ;
        }

        /// <summary>
        /// 背景图片布局方式响应Css变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void comboBoxBackgroudLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetBackgroudLayout();
            textBoxCSSText.Text = cssSection.ToString() + myBoxModelPanel.ToString(); ;
        }

        /// <summary>
        /// 选择了“使用背景图片”响应Css变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void checkBoxUsingBackImg_CheckedChanged(object sender, EventArgs e)
        {
            btnBrowsePic.Enabled = checkBoxUsingBackImg.Checked;
            if (checkBoxUsingBackImg.Checked)
            {
                checkBoxUsingCurImg.Enabled = true;
                checkBoxUsingCurImg.Checked = false;
            }
            else
            {
                checkBoxUsingCurImg.Enabled = false;
                checkBoxUsingCurImg.Checked = false;
            }
            comboBoxBackgroudLayout.Enabled = checkBoxUsingBackImg.Checked;
            if (checkBoxUsingBackImg.Checked)
            {
                checkBoxUsingCurImg.Checked = false;
                comboBoxBackgroudLayout.SelectedIndex = 1;
            }
            else if (!checkBoxUsingCurImg.Checked)
            {
                cssSection.Properties["background-image"] = string.Empty;
                cssSection.Properties["background-position"] = string.Empty;
                cssSection.Properties["background-repeat"] = string.Empty;
            }
            textBoxCSSText.Text = cssSection.ToString() + myBoxModelPanel.ToString();;
        }

        /// <summary>
        /// 浏览背景图片响应Css变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnBrowsePic_Click(object sender, EventArgs e)
        {
            cssSection.Properties["background-image"] = @"url(" + SiteResourceService.SelectResourceFormat(MediaFileType.Pic, Service.Workbench.MainForm) + ")";
            GetBackgroudLayout();
            textBoxCSSText.Text = cssSection.ToString() + myBoxModelPanel.ToString();;
        }

        /// <summary>
        /// 使用当前背景响应Css变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void checkBoxUsingCurImg_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUsingCurImg.Checked)
            {
                btnBrowsePic.Enabled = false;
                labelBackLayout.Enabled = true;
                comboBoxBackgroudLayout.Enabled = true;
                UseCurBackImg = true;
            }
            else
            {
                if (checkBoxUsingBackImg.Checked)
                {
                    btnBrowsePic.Enabled = true;
                }
                else
                {
                    btnBrowsePic.Enabled = false;
                }

                UseCurBackImg = false;
                if (!checkBoxUsingBackImg.Checked)
                {
                    labelBackLayout.Enabled = false;
                    comboBoxBackgroudLayout.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 链接点击后的颜色响应Css变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void colorBtnLast_MyColorChanged(object sender, EventArgs e)
        {
            textBoxLastColor.Text = colorBtnLast.ColorArgbString;
            _webPageDicA_visited.Properties["color"] = colorBtnLast.ColorArgbString; 
            textBoxCSSText.Text = cssSection.ToString() + myBoxModelPanel.ToString(); ;
        }

        /// <summary>
        /// 链接点击前颜色响应Css变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void colorBtnBefore_MyColorChanged(object sender, EventArgs e)
        {
            textBoxBeforeColor.Text = colorBtnBefore.ColorArgbString;
            _webPageDicA.Properties["color"] = colorBtnBefore.ColorArgbString; 
        }

        /// <summary>
        /// 链接是否显示下划线响应Css变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void checkBoxUnderLine_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUnderLine.Checked)
            {
                _webPageDicA.Properties["text-decoration"] = "underline";
                _webPageDicA_visited.Properties["text-decoration"] = "underline";
            }
            else
            {
                _webPageDicA.Properties["text-decoration"] = "none";
                _webPageDicA_visited.Properties["text-decoration"] = "none";
            }
            textBoxCSSText.Text = cssSection.ToString() + myBoxModelPanel.ToString(); ;
        }

        /// <summary>
        /// 图片链接是否显示边框响应Css变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void checkBoxImgBorder_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxImgBorder.Checked)
            {
                _webPageDicImg.Properties["border"] = "1";
            }
            else
                _webPageDicImg.Properties["border"] = "0";
            textBoxCSSText.Text = cssSection.ToString() + myBoxModelPanel.ToString(); ;
        }

        private void checkBoxIsAutoSize_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIsAutoSize.Checked)
                cssSection.Properties["width"] = "100%";
            else
            {
                cssSection.Properties["width"] = Doc.Width;
            }
        }

        private void checkBoxUsingBackColor_CheckedChanged(object sender, EventArgs e)
        {
            btnSelectBackColor.Enabled = checkBoxUsingBackColor.Checked;
            if (!checkBoxUsingBackColor.Checked)
            {
                cssSection.Properties["background-color"] = string.Empty;
            }
            else 
            {
                cssSection.Properties["background-color"] = textBoxBackColor.Text;
            }
            textBoxCSSText.Text = cssSection.ToString() + myBoxModelPanel.ToString();
        }

        #endregion

        #region 内部函数

        /// <summary>
        /// 保存当前的css设置
        /// </summary>
        private void SaveCSSSetup()
        {

            switch (_curType)
            {
                case CurType.None:
                    break;
                case CurType.WebPage:
                    #region 保存页面的设置
                    {
                        XmlElement docEle = Doc.DocumentElement;
                        docEle.SetAttribute("a", _webPageDicA.ToString());
                        docEle.SetAttribute("a_visited", _webPageDicA_visited.ToString());
                        docEle.SetAttribute("img", _webPageDicImg.ToString());
                        docEle.SetAttribute("webCss", cssSection.ToString() + myBoxModelPanel.ToString());
                        break;
                    }
                    #endregion
                case CurType.Tmplt:
                    #region 保存模板的设置
                    {
                        XmlElement docEle = Doc.DocumentElement;
                        docEle.SetAttribute("a", _webPageDicA.ToString());
                        docEle.SetAttribute("a_visited", _webPageDicA_visited.ToString());
                        docEle.SetAttribute("img", _webPageDicImg.ToString());
                        docEle.SetAttribute("webCss", cssSection.ToString() + myBoxModelPanel.ToString());
                        break;
                    }
                    #endregion
                case CurType.Snip:
                    #region 保存矩形的设置
                    {
                        break;
                    }
                    #endregion
                case CurType.Part:
                    #region 保存模块的设置
                    {
                        break;
                    }
                    #endregion
                default:
                    break;
            }
            CSSText = cssSection.ToString() + myBoxModelPanel.ToString(); ;
        }
        
        /// <summary>
        /// 设置背景图片的布局方式
        /// </summary>
        /// <param name="combox"></param>
        private void GetBackgroudLayout()
        {
            switch (comboBoxBackgroudLayout.SelectedIndex)
            {
                case 0://居中
                    cssSection.Properties["background-position"] = "center";
                    cssSection.Properties["background-repeat"] = "no-repeat";
                    break;
                case 1://平铺
                    cssSection.Properties["background-position"] = "";
                    cssSection.Properties["background-repeat"] = "repeat";
                    break;
                case 2://垂直平铺
                    cssSection.Properties["background-position"] = "top";
                    cssSection.Properties["background-repeat"] = "repeat-y";
                    break;
                case 3://水平平铺
                    cssSection.Properties["background-position"] = "top";
                    cssSection.Properties["background-repeat"] = "repeat-x";
                    break;
                default:
                    cssSection.Properties["background-position"] = "center";
                    cssSection.Properties["background-repeat"] = "no-repeat";
                    break;
            }
        }

        private string GetSizeUnit(ComboBox sizeUnitCombox)
        {
            string sizeUint = "px";
            switch (sizeUnitCombox.SelectedIndex)
            {
                case 0:
                    sizeUint = "px";
                    break;
                case 1:
                    sizeUint = "pt";
                    break;
                case 2:
                    sizeUint = "in";
                    break;
                case 3:
                    sizeUint = "cm";
                    break;
                case 4:
                    sizeUint = "mm";
                    break;
                case 5:
                    sizeUint = "pc";
                    break;
                case 6:
                    sizeUint = "em";
                    break;
                case 7:
                    sizeUint = "ex";
                    break;
                case 8:
                    sizeUint = @"%";
                    break;
                default:
                    break;
            }
            return sizeUint;
        }

        private KeyValuePair<string, int> GetSizeAndUnit(string size)
        {
            string _key = "0";
            int _value = 0;
            string _unit = size.Substring(size.Length - 2, 2);
            if (size[size.Length - 1] == '%')
            {
                _key = size.Remove(size.Length - 1);
                _value = 8;
            }
            else
            {
                switch (_unit)
                {
                    case "px":
                        _key = size.Remove(size.Length - 2);
                        _value = 0;
                        break;
                    case "pt":
                        _key = size.Remove(size.Length - 2);
                        _value = 1;
                        break;
                    case "in":
                        _key = size.Remove(size.Length - 2);
                        _value = 2;
                        break;
                    case "cm":
                        _key = size.Remove(size.Length - 2);
                        _value = 3;
                        break;
                    case "mm":
                        _key = size.Remove(size.Length - 2);
                        _value = 4;
                        break;
                    case "pc":
                        _key = size.Remove(size.Length - 2);
                        _value = 5;
                        break;
                    case "em":
                        _key = size.Remove(size.Length - 2);
                        _value = 6;
                        break;
                    case "ex":
                        _key = size.Remove(size.Length - 2);
                        _value = 7;
                        break;
                    default:
                        _key = size;
                        break;
                }
            }
            return new KeyValuePair<string, int>(_key, _value);
        }

        private KeyValuePair<string, int> GetModelSize(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return new KeyValuePair<string,int>("",0);
            }
            string[] ss = str.Split(new char[] { ' ' });
            if (ss.Length > 0)
            {
                if (!Utility.Regex.HasAnyNumber(ss[0]))
                {
                    return new KeyValuePair<string, int>("", 0);
                }
                else
                {
                    KeyValuePair<string, int> uintValue = Service.Util.GetSizeAndUnit(ss[0]);
                    return uintValue;
                }
            }
            return new KeyValuePair<string, int>("", 0);
        }

        #endregion        
    }
}
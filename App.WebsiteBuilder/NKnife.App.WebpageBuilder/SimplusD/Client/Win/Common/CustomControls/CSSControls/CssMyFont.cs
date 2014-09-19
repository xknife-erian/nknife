using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 字体控件
    /// </summary>
    public partial class CssMyFont : UserControl
    {
        #region 内部变量
        
        private string _fontList = "";

        private List<string> fontlists = new List<string>();

        #endregion

        #region 公共属性

        public CssSection CssSection { get; set; }

        /// <summary>
        /// 获取或设置是否粗体
        /// </summary>
        public bool IsBold { get; set; }

        /// <summary>
        /// 获取或设置是否斜体
        /// </summary>
        public bool IsItalic { get; set; }

        /// <summary>
        /// 获取或设置当前的字体
        /// </summary>
        public string CurentFont { get; set; }

        #endregion

        #region 构造函数

        public CssMyFont()
        {

            InitializeComponent();
            CssSection = new CssSection();
            InitEvents();
            if (!Service.Util.DesignMode)
            {
                InitComboBoxFont(true);
                InitOther();
            }
        }

        #endregion

        #region 内部函数 事件消息

        /// <summary>
        /// 初始化事件
        /// </summary>
        private void InitEvents()
        {
            comboBoxFont.SelectedIndexChanged += new EventHandler(comboBoxFont_SelectedIndexChanged);
            comboBoxSize.SelectedIndexChanged += new EventHandler(comboBoxSize_SelectedIndexChanged);
            comboBoxSizeUint.SelectedIndexChanged += new EventHandler(comboBoxSizeUint_SelectedIndexChanged);
            btnBold.Click += new EventHandler(btnBold_Click);
            btnI.Click += new EventHandler(btnI_Click);
            btnColor.MyColorChanged += new EventHandler(btnColor_MyColorChanged);
            textBoxColor.TextChanged += new EventHandler(textBoxColor_TextChanged);
        }

        /// <summary>
        /// 初始化字体选择框
        /// </summary>
        /// <param name="isInit"></param>
        private void InitComboBoxFont(bool isInit)
        {
            List<string> lt = new List<string>();
            if (isInit)
            {
                string path = PathService.Config_FontList;
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
                            lt = GetFontList(fontlists[0]);
                        }
                    }
                    catch
                    {
                        File.Delete(path);
                        throw;
                    }
                }
            }
            else
            {
                lt = GetFontList(_fontList);
            }
            comboBoxFont.Items.Clear();
            comboBoxFont.Items.Add(StringParserService.Parse("${res:TextpropertyPanel.label.tip}"));
            comboBoxFont.Items.AddRange(lt.ToArray());
            comboBoxFont.Items.Add("---------------------");
            comboBoxFont.Items.Add(StringParserService.Parse("${res:TextpropertyPanel.fontname.editfontlist}"));
        }

        /// <summary>
        /// 获取字体列表
        /// </summary>
        private List<string> GetFontList(string p)
        {
            if (string.IsNullOrEmpty(p))
            {
                return new List<string>();
            }
            List<string> listStr = new List<string>();
            string[] ss = p.Split(new char[] { ','});
            listStr.AddRange(ss);
            return listStr;
        }

        /// <summary>
        /// 初始化其它控件
        /// </summary>
        private void InitOther()
        {
            #region comboBoxSize

            comboBoxSize.Items.Clear();
            comboBoxSize.Items.AddRange(SizeItems);

            #endregion

            #region comboBoxUint

            comboBoxSizeUint.Items.Clear();
            comboBoxSizeUint.Items.AddRange(SizeUnitItems);

            #endregion
        }

        void textBoxColor_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxColor.Text))
            {
                CssSection.Properties["color"] = "";
            }
            if (textBoxColor.Text.Length == 7)
            {
                CssSection.Properties["color"] = textBoxColor.Text;
            }
            OnCssChanged(EventArgs.Empty);
        }

        void btnColor_MyColorChanged(object sender, EventArgs e)
        {
            textBoxColor.Text = ColorTranslator.ToHtml(btnColor.MyColor);
            CssSection.Properties["color"] = textBoxColor.Text;
            OnCssChanged(EventArgs.Empty); 
        }

        void btnI_Click(object sender, EventArgs e)
        {
            IsItalic = !IsItalic;
            if (IsItalic)
            {
                btnI.FlatStyle = FlatStyle.Flat;
                CssSection.Properties["font-style"] = "italic";
            }
            else
            {
                btnI.FlatStyle = FlatStyle.Popup;
                CssSection.Properties["font-style"] = "";
            }

            OnCssChanged(EventArgs.Empty);
        }

        void btnBold_Click(object sender, EventArgs e)
        {
            IsBold = !IsBold;
            if (IsBold)
            {
                btnBold.FlatStyle = FlatStyle.Flat;
                CssSection.Properties["font-weight"] = "bold";
            }
            else
            {
                btnBold.FlatStyle = FlatStyle.Popup;
                CssSection.Properties["font-weight"] = "";
            }
            OnCssChanged(EventArgs.Empty);
        }

        void comboBoxSizeUint_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnCssChanged(EventArgs.Empty);
        }

        void comboBoxSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSize.SelectedIndex > 0 && comboBoxSize.SelectedIndex < 9)
            {
                comboBoxSizeUint.Enabled = true;
                if (comboBoxSizeUint.Text == "")
                    comboBoxSizeUint.SelectedIndex = 0;
                CssSection.Properties["font-size"] = comboBoxSize.Text + comboBoxSizeUint.Text;
            }
            else
            {
                comboBoxSizeUint.Enabled = false;
                CssSection.Properties["font-size"] = comboBoxSize.Text;
            }

            OnCssChanged(EventArgs.Empty);
        }

        void comboBoxFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFont.SelectedIndex == 0)
            {
                SelectFontListForm form = new SelectFontListForm(fontlists);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    comboBoxFont.Items.Clear();
                    comboBoxFont.Items.Add(StringParserService.Parse("${res:TextpropertyPanel.label.tip}"));
                    comboBoxFont.Items.AddRange(GetFontList(form.FontList).ToArray());
                    comboBoxFont.Items.Add("---------------------");
                    comboBoxFont.Items.Add(StringParserService.Parse("${res:TextpropertyPanel.fontname.editfontlist}"));
                }
                comboBoxFont.Text = CurentFont;
                CssSection.Properties["font-family"] = CurentFont;
            }
            else if (comboBoxFont.SelectedIndex == comboBoxFont.Items.Count - 2)
            {
                CssSection.Properties["font-family"] = CurentFont;
            }
            else if (comboBoxFont.SelectedIndex == comboBoxFont.Items.Count - 1)
            {
                FontListEditor form = new FontListEditor();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _fontList = form.FontListString;
                    InitComboBoxFont(false);
                    CssSection.Properties["font-family"] = CurentFont;
                    OnCssChanged(EventArgs.Empty);
                    return;
                }
                return;
            }
            else
            {
                CssSection.Properties["font-family"] = comboBoxFont.Text;
            }
            OnCssChanged(EventArgs.Empty);
        }

        /// <summary>
        /// 大小的值集合
        /// </summary>
        private string[] SizeItems
        {
            get
            {
                string[] size = new string[18];
                size[0] = StringParserService.Parse("${res:TextpropertyPanel.size.none}");
                size[1] = StringParserService.Parse("${res:TextpropertyPanel.size.t9}");
                size[2] = StringParserService.Parse("${res:TextpropertyPanel.size.t10}");
                size[3] = StringParserService.Parse("${res:TextpropertyPanel.size.t12}");
                size[4] = StringParserService.Parse("${res:TextpropertyPanel.size.t14}");
                size[5] = StringParserService.Parse("${res:TextpropertyPanel.size.t16}");
                size[6] = StringParserService.Parse("${res:TextpropertyPanel.size.t18}");
                size[7] = StringParserService.Parse("${res:TextpropertyPanel.size.t24}");
                size[8] = StringParserService.Parse("${res:TextpropertyPanel.size.t36}");
                size[9] = StringParserService.Parse("${res:TextpropertyPanel.size.xxsmall}");
                size[10] = StringParserService.Parse("${res:TextpropertyPanel.size.xsmall}");
                size[11] = StringParserService.Parse("${res:TextpropertyPanel.size.small}");
                size[12] = StringParserService.Parse("${res:TextpropertyPanel.size.medium}");
                size[13] = StringParserService.Parse("${res:TextpropertyPanel.size.large}");
                size[14] = StringParserService.Parse("${res:TextpropertyPanel.size.xlarge}");
                size[15] = StringParserService.Parse("${res:TextpropertyPanel.size.xxlarge}");
                size[16] = StringParserService.Parse("${res:TextpropertyPanel.size.smaller}");
                size[17] = StringParserService.Parse("${res:TextpropertyPanel.size.larger}");
                return size;
            }
        }

        /// <summary>
        /// 单位的值集合
        /// </summary>
        private string[] SizeUnitItems
        {
            get
            {
                string[] sizeUnit = new string[9];
                sizeUnit[0] = StringParserService.Parse("${res:TextpropertyPanel.sizeunit.px}");
                sizeUnit[1] = StringParserService.Parse("${res:TextpropertyPanel.sizeunit.pt}");
                sizeUnit[2] = StringParserService.Parse("${res:TextpropertyPanel.sizeunit.in}");
                sizeUnit[3] = StringParserService.Parse("${res:TextpropertyPanel.sizeunit.cm}");
                sizeUnit[4] = StringParserService.Parse("${res:TextpropertyPanel.sizeunit.mm}");
                sizeUnit[5] = StringParserService.Parse("${res:TextpropertyPanel.sizeunit.pc}");
                sizeUnit[6] = StringParserService.Parse("${res:TextpropertyPanel.sizeunit.em}");
                sizeUnit[7] = StringParserService.Parse("${res:TextpropertyPanel.sizeunit.ex}");
                sizeUnit[8] = StringParserService.Parse("${res:TextpropertyPanel.sizeunit.per}");
                return sizeUnit;
            }
        }

        #endregion

        #region 自定义事件

        /// <summary>
        /// 当Css属性变化时
        /// </summary>
        public event EventHandler CssChanged;

        /// <summary>
        /// 当Css属性变化时
        /// </summary>
        protected virtual void OnCssChanged(EventArgs e)
        {
            if (CssChanged != null)
            {
                CssChanged(this, e);
            }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 初始化现有的值
        /// </summary>
        public void InitValue(CssSection cssSection)
        {
            CurentFont = "";
            CssSection = cssSection;
            string cssVlaue = "";
            if (cssSection.Properties.TryGetValue("font-family", out cssVlaue))
            {
                comboBoxFont.Text = cssVlaue;
                CurentFont = cssVlaue;
            }
            if (cssSection.Properties.TryGetValue("font-weight", out cssVlaue))
            {
                btnBold.FlatStyle = FlatStyle.Flat;
                IsBold = true;
            }
            if (cssSection.Properties.TryGetValue("font-style", out cssVlaue))
            {
                btnI.FlatStyle = FlatStyle.Flat;
                IsItalic = true;
            }
            if (cssSection.Properties.TryGetValue("font-size", out cssVlaue))
            {
                KeyValuePair<string, int> _kv = Service.Util.GetSizeAndUnit(cssVlaue);
                comboBoxSize.Text = _kv.Key;
                comboBoxSizeUint.SelectedIndex = _kv.Value;
            }

            if (cssSection.Properties.TryGetValue("color", out cssVlaue))
            {
                textBoxColor.Text = cssVlaue;
                btnColor.MyColor = ColorTranslator.FromHtml(cssVlaue);
            }
        }

        #endregion
    }
}

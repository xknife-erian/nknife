using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;


namespace Jeelu.SimplusD.Client.Win
{
    public partial class BoxModelPanel : Control
    {
        #region 控件变量的声明

        CssSection _dic = new CssSection() ;

        CssSection _marginCssSection = new CssSection();
        CssSection _borderCssSection = new CssSection();
        CssSection _paddingCssSection = new CssSection();

        ImageList _imageList1 = new ImageList();
        ImageList _imageList2 = new ImageList();
        ImageList _imageList3 = new ImageList();

        TabControl _tabControl;
        TabPage _tabPage1;
        TabPage _tabPage2;
        TabPage _tabPage3;

        SingerBoxItemControl _page1_1;
        SingerBoxItemControl _page1_2;
        SingerBoxItemControl _page1_3;
        SingerBoxItemControl _page1_4;
        SingerBoxItemControl _page1_5;
        CheckBox _checkBox1;

        SingerBoxItemControl _page2_1;
        SingerBoxItemControl _page2_2;
        SingerBoxItemControl _page2_3;
        SingerBoxItemControl _page2_4;
        SingerBoxItemControl _page2_5;
        CheckBox _checkBox2;

        SingerBoxItemControl _page3_1;
        SingerBoxItemControl _page3_2;
        SingerBoxItemControl _page3_3;
        SingerBoxItemControl _page3_4;
        SingerBoxItemControl _page3_5;
        CheckBox _checkBox3;
        #endregion

        public BoxModelPanel()
        {
            Image[] _images1 = new Image[5] ;
            Image[] _images2 = new Image[5] ;
            Image[] _images3 = new Image[5] ;

            if (!Service.Util.DesignMode)
            {
                _images1[0] = Image.FromFile(Path.Combine(PathService.SoftwarePath, @"icon\all1.png"));
                _images1[1] = Image.FromFile(Path.Combine(PathService.SoftwarePath, @"icon\left1.png"));
                _images1[2] = Image.FromFile(Path.Combine(PathService.SoftwarePath, @"icon\right1.png"));
                _images1[3] = Image.FromFile(Path.Combine(PathService.SoftwarePath, @"icon\top1.png"));
                _images1[4] = Image.FromFile(Path.Combine(PathService.SoftwarePath, @"icon\bottom1.png"));

                _images2[0] = Image.FromFile(Path.Combine(PathService.SoftwarePath, @"icon\all1.jpg"));
                _images2[1] = Image.FromFile(Path.Combine(PathService.SoftwarePath, @"icon\left1.jpg"));
                _images2[2] = Image.FromFile(Path.Combine(PathService.SoftwarePath, @"icon\right1.jpg"));
                _images2[3] = Image.FromFile(Path.Combine(PathService.SoftwarePath, @"icon\top1.jpg"));
                _images2[4] = Image.FromFile(Path.Combine(PathService.SoftwarePath, @"icon\bottom1.jpg"));

                _images3[0] = Image.FromFile(Path.Combine(PathService.SoftwarePath, @"icon\all1.jpg"));
                _images3[1] = Image.FromFile(Path.Combine(PathService.SoftwarePath, @"icon\left1.jpg"));
                _images3[2] = Image.FromFile(Path.Combine(PathService.SoftwarePath, @"icon\right1.jpg"));
                _images3[3] = Image.FromFile(Path.Combine(PathService.SoftwarePath, @"icon\top1.jpg"));
                _images3[4] = Image.FromFile(Path.Combine(PathService.SoftwarePath, @"icon\bottom1.jpg"));

                XmlDocument doc = new XmlDocument();
                doc.Load(PathService.CL_DS_BorderPart);
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("item");
                foreach (XmlNode node in nodes)
                {
                    _dic.Properties[node.Attributes["name"].Value] = node.InnerText;
                }
                _imageList1.Images.Clear();
                _imageList1.Images.AddRange(_images1);

                _imageList2.Images.Clear();
                _imageList2.Images.AddRange(_images2);

                _imageList3.Images.Clear();
                _imageList3.Images.AddRange(_images3);
            }
            InitControls();
        }

        private void InitControls()
        {
            _tabControl = new TabControl();
            _tabControl.Dock = DockStyle.Fill;

            string outStr = "";

            #region  边界
            _tabPage1 = new TabPage();
            _tabPage1.Name = "MarginTabPage";
            _tabPage1.Text = "边界";

            _checkBox1 = new CheckBox();
            _checkBox1.Name = "_checkBox1";
            _checkBox1.Text = StringParserService.Parse("${res:cssDesign.cssForm.modelPanelAllSame}");
            _checkBox1.AutoSize = true;
            _checkBox1.Dock = DockStyle.Top;
            
            _page1_1 = new SingerBoxItemControl(false);
            if (_dic.Properties.TryGetValue("bottom", out outStr))
                _page1_1.LabelName = outStr;  

            _page1_2 = new SingerBoxItemControl(false);
            if (_dic.Properties.TryGetValue("top", out outStr))
                _page1_2.LabelName = outStr;

            _page1_3 = new SingerBoxItemControl(false);
            if (_dic.Properties.TryGetValue("right", out outStr))
                _page1_3.LabelName = outStr;

            _page1_4 = new SingerBoxItemControl(false);
            if (_dic.Properties.TryGetValue("left", out outStr))
                _page1_4.LabelName = outStr;            

            _page1_5 = new SingerBoxItemControl(false);
            if (_dic.Properties.TryGetValue("all", out outStr))
                _page1_5.LabelName = outStr;

            


            if (_imageList1.Images.Count >= 5)
            {
                _page1_1.Image = _imageList1.Images[4];
                _page1_2.Image = _imageList1.Images[3];
                _page1_3.Image = _imageList1.Images[2];
                _page1_4.Image = _imageList1.Images[1];
                _page1_5.Image = _imageList1.Images[0];
            }
            
            _tabPage1.Controls.Add(_page1_1);
            _tabPage1.Controls.Add(_page1_2);
            _tabPage1.Controls.Add(_page1_3);
            _tabPage1.Controls.Add(_page1_4);
            _tabPage1.Controls.Add(_page1_5);
            _tabPage1.Controls.Add(_checkBox1);

            #endregion

            #region 边框
            _tabPage2 = new TabPage();
            _tabPage2.Name = "BorderTabPage";
            _tabPage2.Text = "边框";

            _checkBox2 = new CheckBox();
            _checkBox2.Name = "_checkBox2";
            _checkBox2.Text = StringParserService.Parse("${res:cssDesign.cssForm.modelPanelAllSame}");
            _checkBox2.AutoSize = true;
            _checkBox2.Dock = DockStyle.Top;

            _page2_1 = new SingerBoxItemControl(true);
            if (_dic.Properties.TryGetValue("bottom", out outStr))
                _page2_1.LabelName = outStr;

            _page2_2 = new SingerBoxItemControl(true);
            if (_dic.Properties.TryGetValue("top", out outStr))
                _page2_2.LabelName = outStr;

            _page2_3 = new SingerBoxItemControl(true);
            if (_dic.Properties.TryGetValue("right", out outStr))
                _page2_3.LabelName = outStr;

            _page2_4 = new SingerBoxItemControl(true);
            if (_dic.Properties.TryGetValue("left", out outStr))
                _page2_4.LabelName = outStr;

            _page2_5 = new SingerBoxItemControl(true);
            if (_dic.Properties.TryGetValue("all", out outStr))
                _page2_5.LabelName = outStr;

            if (_imageList2.Images.Count >= 5)
            {
                _page2_1.Image = _imageList2.Images[4];
                _page2_2.Image = _imageList2.Images[3];
                _page2_3.Image = _imageList2.Images[2];
                _page2_4.Image = _imageList2.Images[1];
                _page2_5.Image = _imageList2.Images[0];
            }

            _tabPage2.Controls.Add(_page2_1);
            _tabPage2.Controls.Add(_page2_2);
            _tabPage2.Controls.Add(_page2_3);
            _tabPage2.Controls.Add(_page2_4);
            _tabPage2.Controls.Add(_page2_5);
            _tabPage2.Controls.Add(_checkBox2);

            #endregion

            #region 填充 
            _tabPage3 = new TabPage();
            _tabPage3.Name = "PaddingTabPage";
            _tabPage3.Text = "填充";

            _checkBox3 = new CheckBox();
            _checkBox3.Name = "_checkBox3";
            _checkBox3.Text = StringParserService.Parse("${res:cssDesign.cssForm.modelPanelAllSame}");
            _checkBox3.AutoSize = true;
            _checkBox3.Dock = DockStyle.Top;
           
            _page3_1 = new SingerBoxItemControl(false);
            if (_dic.Properties.TryGetValue("bottom", out outStr))
                _page3_1.LabelName = outStr;

            _page3_2 = new SingerBoxItemControl(false);
            if (_dic.Properties.TryGetValue("top", out outStr))
                _page3_2.LabelName = outStr;

            _page3_3 = new SingerBoxItemControl(false);
            if (_dic.Properties.TryGetValue("right", out outStr))
                _page3_3.LabelName = outStr;

            _page3_4 = new SingerBoxItemControl(false);
            if (_dic.Properties.TryGetValue("left", out outStr))
                _page3_4.LabelName = outStr;

            _page3_5 = new SingerBoxItemControl(false);
            if (_dic.Properties.TryGetValue("all", out outStr))
                _page3_5.LabelName = outStr;

            if (_imageList3.Images.Count >= 5)
            {
                _page3_1.Image = _imageList3.Images[4];
                _page3_2.Image = _imageList3.Images[3];
                _page3_3.Image = _imageList3.Images[2];
                _page3_4.Image = _imageList3.Images[1];
                _page3_5.Image = _imageList3.Images[0];
            }

            _tabPage3.Controls.Add(_page3_1);
            _tabPage3.Controls.Add(_page3_2);
            _tabPage3.Controls.Add(_page3_3);
            _tabPage3.Controls.Add(_page3_4);
            _tabPage3.Controls.Add(_page3_5);
            _tabPage3.Controls.Add(_checkBox3);


            _tabControl.TabPages.Add(_tabPage1);
            _tabControl.TabPages.Add(_tabPage2);
            _tabControl.TabPages.Add(_tabPage3);
            _tabControl.Appearance = TabAppearance.Buttons;
            #endregion

            _page1_1.CssChanged += new EventHandler(_page1_CssChanged);
            _page1_2.CssChanged += new EventHandler(_page1_2_CssChanged);
            _page1_3.CssChanged += new EventHandler(_page1_3_CssChanged);
            _page1_4.CssChanged += new EventHandler(_page1_4_CssChanged);
            _page1_5.CssChanged += new EventHandler(_page1_5_CssChanged);
            _checkBox1.CheckedChanged += new EventHandler(_checkBox1_CheckedChanged);
            _checkBox1.Checked = true;

            _page2_1.CssChanged += new EventHandler(_page2_CssChanged);
            _page2_2.CssChanged += new EventHandler(_page2_2_CssChanged);
            _page2_3.CssChanged += new EventHandler(_page2_3_CssChanged);
            _page2_4.CssChanged += new EventHandler(_page2_4_CssChanged);
            _page2_5.CssChanged += new EventHandler(_page2_5_CssChanged);
            _checkBox2.CheckedChanged += new EventHandler(_checkBox2_CheckedChanged);
            _checkBox2.Checked = true;

            _page3_1.CssChanged += new EventHandler(_page3_CssChanged);
            _page3_2.CssChanged += new EventHandler(_page3_2_CssChanged);
            _page3_3.CssChanged += new EventHandler(_page3_3_CssChanged);
            _page3_4.CssChanged += new EventHandler(_page3_4_CssChanged);
            _page3_5.CssChanged += new EventHandler(_page3_5_CssChanged);
            _checkBox3.CheckedChanged += new EventHandler(_checkBox3_CheckedChanged);
            _checkBox3.Checked = true;

            this.Controls.Add(_tabControl);
        }

        void _page3_5_CssChanged(object sender, EventArgs e)
        {
            int[] oldSize = GetOldSize(_paddingCssSection, "padding");
            _paddingCssSection.Properties["padding"] = _page3_5.Css;
            _paddingCssSection.Properties["padding-left"] = "";
            _paddingCssSection.Properties["padding-right"] = "";
            _paddingCssSection.Properties["padding-top"] = "";
            _paddingCssSection.Properties["padding-bottom"] = "";
            ChangedCss(_paddingCssSection, "padding", oldSize, 0);
            OnCssChanged(EventArgs.Empty);
        }

        void _page3_4_CssChanged(object sender, EventArgs e)
        {
            int[] oldSize = GetOldSize(_paddingCssSection, "padding");
            _paddingCssSection.Properties["padding"] = "";
            _paddingCssSection.Properties["padding-left"] = _page3_4.Css;
            ChangedCss(_paddingCssSection, "padding-left", oldSize, 4);
            OnCssChanged(EventArgs.Empty);
        }

        void _page3_3_CssChanged(object sender, EventArgs e)
        {
            int[] oldSize = GetOldSize(_paddingCssSection, "padding");
            _paddingCssSection.Properties["padding"] = "";
            _paddingCssSection.Properties["padding-right"] = _page3_3.Css;
            ChangedCss(_paddingCssSection, "padding-right", oldSize, 3);
            OnCssChanged(EventArgs.Empty);
        }

        void _page3_2_CssChanged(object sender, EventArgs e)
        {
            int[] oldSize = GetOldSize(_paddingCssSection, "padding");
            _paddingCssSection.Properties["padding"] = "";
            _paddingCssSection.Properties["padding-top"] = _page3_2.Css;
            ChangedCss(_paddingCssSection, "padding-top", oldSize, 2);
            OnCssChanged(EventArgs.Empty);
        }

        void _page3_CssChanged(object sender, EventArgs e)
        {
            int[] oldSize = GetOldSize(_paddingCssSection, "padding");
            _paddingCssSection.Properties["padding"] = "";
            _paddingCssSection.Properties["padding-bottom"] = _page3_1.Css;
            ChangedCss(_paddingCssSection, "padding-bottom", oldSize, 1);
            OnCssChanged(EventArgs.Empty);
        }

        void _page2_5_CssChanged(object sender, EventArgs e)
        {
            int[] oldSize = GetOldSize(_borderCssSection, "border");
            _borderCssSection.Properties["border"] = _page2_5.Css;
            _borderCssSection.Properties["border-left"] = "";
            _borderCssSection.Properties["border-right"] = "";
            _borderCssSection.Properties["border-top"] = "";
            _borderCssSection.Properties["border-bottom"] = "";
            ChangedCss(_borderCssSection, "border", oldSize, 0);
            OnCssChanged(EventArgs.Empty);
        }

        void _page2_4_CssChanged(object sender, EventArgs e)
        {
            int[] oldSize = GetOldSize(_borderCssSection, "border");
            _borderCssSection.Properties["border"] = "";
            _borderCssSection.Properties["border-left"] = _page2_4.Css;
            ChangedCss(_borderCssSection, "border-left", oldSize, 4);
            OnCssChanged(EventArgs.Empty);
        }

        void _page2_3_CssChanged(object sender, EventArgs e)
        {
            int[] oldSize = GetOldSize(_borderCssSection, "border");
            _borderCssSection.Properties["border"] = "";
            _borderCssSection.Properties["border-right"] = _page2_3.Css;
            ChangedCss(_borderCssSection, "border-right", oldSize, 3);
            OnCssChanged(EventArgs.Empty);
        }

        void _page2_2_CssChanged(object sender, EventArgs e)
        {
            int[] oldSize = GetOldSize(_borderCssSection, "border");
            _borderCssSection.Properties["border"] = "";
            _borderCssSection.Properties["border-top"] = _page2_2.Css;
            ChangedCss(_borderCssSection, "border-top", oldSize, 2);
            OnCssChanged(EventArgs.Empty);
        }

        void _page2_CssChanged(object sender, EventArgs e)
        {
            int[] oldSize = GetOldSize(_borderCssSection, "border");
            _borderCssSection.Properties["border"] = "";
            _borderCssSection.Properties["border-bottom"] = _page2_1.Css;
            ChangedCss(_borderCssSection, "border-bottom", oldSize, 1);
            OnCssChanged(EventArgs.Empty);
        }

        void _page1_5_CssChanged(object sender, EventArgs e)
        {
            int[] oldSize = GetOldSize(_marginCssSection, "margin");
            _marginCssSection.Properties["margin"] = _page1_5.Css;
            _marginCssSection.Properties["margin-left"] = "";
            _marginCssSection.Properties["margin-right"] = "";
            _marginCssSection.Properties["margin-top"] = "";
            _marginCssSection.Properties["margin-bottom"] = "";
            ChangedCss(_marginCssSection, "margin", oldSize, 0);
            OnCssChanged(EventArgs.Empty);
        }

        void _page1_4_CssChanged(object sender, EventArgs e)
        {
            int[] oldSize = GetOldSize(_marginCssSection, "margin");
            _marginCssSection.Properties["margin"] = "";
            _marginCssSection.Properties["margin-left"] = _page1_4.Css;
            ChangedCss(_marginCssSection, "margin-left", oldSize, 4);
            OnCssChanged(EventArgs.Empty);
        }

        void _page1_3_CssChanged(object sender, EventArgs e)
        {
            int[] oldSize = GetOldSize(_marginCssSection, "margin");
            _marginCssSection.Properties["margin"] = "";
            _marginCssSection.Properties["margin-right"] = _page1_3.Css;
            ChangedCss(_marginCssSection, "margin-right", oldSize, 3);
            OnCssChanged(EventArgs.Empty);
        }

        void _page1_2_CssChanged(object sender, EventArgs e)
        {
            int[] oldSize = GetOldSize(_marginCssSection, "margin");
            _marginCssSection.Properties["margin"] = "";
            _marginCssSection.Properties["margin-top"] = _page1_2.Css;
            ChangedCss(_marginCssSection, "margin-top", oldSize, 2);
            OnCssChanged(EventArgs.Empty);
        }

        void _page1_CssChanged(object sender, EventArgs e)
        {
            int[] oldSize = GetOldSize(_marginCssSection, "margin");
            _marginCssSection.Properties["margin"] = "";
            _marginCssSection.Properties["margin-bottom"] = _page1_1.Css;
            ChangedCss(_marginCssSection, "margin-bottom", oldSize, 1);
            OnCssChanged(EventArgs.Empty);
        }

        void _checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (_checkBox3.Checked)
            {
                _page3_5.Enabled = true;
                _paddingCssSection.Properties["padding"] = _page3_5.Css;
                _page3_4.Enabled = false;
                _page3_3.Enabled = false;
                _page3_2.Enabled = false;
                _page3_1.Enabled = false;
                _paddingCssSection.Properties["padding-left"] = "";
                _paddingCssSection.Properties["padding-right"] = "";
                _paddingCssSection.Properties["padding-top"] = "";
                _paddingCssSection.Properties["padding-bottom"] = "";
            }
            else
            {
                _page3_5.Enabled = false;
                _paddingCssSection.Properties["padding"] = "";
                _page3_4.Enabled = true;
                _page3_3.Enabled = true;
                _page3_2.Enabled = true;
                _page3_1.Enabled = true;
                _paddingCssSection.Properties["padding-left"] = _page3_4.Css;
                _paddingCssSection.Properties["padding-right"] = _page3_3.Css;
                _paddingCssSection.Properties["padding-top"] = _page3_2.Css;
                _paddingCssSection.Properties["padding-bottom"] = _page3_1.Css;
            }
            OnCssChanged(EventArgs.Empty);
        }

        void _checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (_checkBox2.Checked)
            {
                _page2_5.Enabled = true;
                _page2_4.Enabled = false;
                _page2_3.Enabled = false;
                _page2_2.Enabled = false;
                _page2_1.Enabled = false;
                _borderCssSection.Properties["border"] = _page2_5.Css;
                _borderCssSection.Properties["border-left"] = "";
                _borderCssSection.Properties["border-right"] = "";
                _borderCssSection.Properties["border-top"] = "";
                _borderCssSection.Properties["border-bottom"] = "";
            }
            else
            {
                _page2_5.Enabled = false;
                _page2_4.Enabled = true;
                _page2_3.Enabled = true;
                _page2_2.Enabled = true;
                _page2_1.Enabled = true;
                _borderCssSection.Properties["border"] = "";
                _borderCssSection.Properties["border-left"] = _page2_4.Css;
                _borderCssSection.Properties["border-right"] = _page2_3.Css;
                _borderCssSection.Properties["border-top"] = _page2_2.Css;
                _borderCssSection.Properties["border-bottom"] = _page2_1.Css;
            }
            OnCssChanged(EventArgs.Empty);
        }

        void _checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (_checkBox1.Checked)
            {
                _page1_5.Enabled = true;
                _page1_4.Enabled = false;
                _page1_3.Enabled = false;
                _page1_2.Enabled = false;
                _page1_1.Enabled = false;
                _marginCssSection.Properties["margin"] = _page1_5.Css;
                _marginCssSection.Properties["margin-left"] = "";
                _marginCssSection.Properties["margin-right"] = "";
                _marginCssSection.Properties["margin-top"] = "";
                _marginCssSection.Properties["margin-bottom"] = "";
            }
            else
            {
                _page1_5.Enabled = false;
                _page1_4.Enabled = true;
                _page1_3.Enabled = true;
                _page1_2.Enabled = true;
                _page1_1.Enabled = true;
                _marginCssSection.Properties["margin"] = _page1_5.Css;
                _marginCssSection.Properties["margin-left"] = _page1_4.Css;
                _marginCssSection.Properties["margin-right"] = _page1_3.Css;
                _marginCssSection.Properties["margin-top"] = _page1_2.Css;
                _marginCssSection.Properties["margin-bottom"] = _page1_1.Css;
            }
            OnCssChanged(EventArgs.Empty);
        }

        //StringBuilder _cssStringBuilder;
        /// <summary>
        /// 返回给用户的一组css样式
        /// 
        public override string ToString()
        {
            return _marginCssSection.ToString() + _borderCssSection.ToString() + _paddingCssSection.ToString();
        }

        public event EventHandler CssChanged;

        protected virtual void OnCssChanged(EventArgs e)
        {
            if (CssChanged != null)
            {
                CssChanged(this, e);
            }
        }

        #region 公共方法

        /// <summary>
        /// 初始化现有的值
        /// </summary>
        public void InitValue(CssSection _dic)
        {
            string strValue = "";
            //margin 边界
            if (_dic.Properties.TryGetValue("margin-bottom", out strValue))
            {
                _dic.Properties.Remove("margin-bottom");
                _page1_1.InitValue(strValue);
            }
            if (_dic.Properties.TryGetValue("margin-top", out strValue))
            {
                _dic.Properties.Remove("margin-top");
                _page1_2.InitValue(strValue);
            }
            if (_dic.Properties.TryGetValue("margin-right", out strValue))
            {
                _dic.Properties.Remove("margin-right");
                _page1_3.InitValue(strValue);
            }
            if (_dic.Properties.TryGetValue("margin-left", out strValue))
            {
                _dic.Properties.Remove("margin-left");
                _page1_4.InitValue(strValue);
            }
            if (_dic.Properties.TryGetValue("margin", out strValue))
            {
                _checkBox1.Checked = true;
                _dic.Properties.Remove("margin");
                _page1_5.InitValue(strValue);
            }
            else
            {
                _checkBox1.Checked = false;
            }
            //border 边框
            if (_dic.Properties.TryGetValue("border-bottom", out strValue))
            {
                _dic.Properties.Remove("border-bottom");
                _page2_1.InitValue(strValue);
            }
            if (_dic.Properties.TryGetValue("border-top", out strValue))
            {
                _dic.Properties.Remove("border-top");
                _page2_2.InitValue(strValue);
            }
            if (_dic.Properties.TryGetValue("border-right", out strValue))
            {
                _dic.Properties.Remove("border-right");
                _page2_3.InitValue(strValue);
            }
            if (_dic.Properties.TryGetValue("border-left", out strValue))
            {
                _dic.Properties.Remove("border-left");
                _page2_4.InitValue(strValue);
            }
            if (_dic.Properties.TryGetValue("border", out strValue))
            {
                _checkBox2.Checked = true;
                _dic.Properties.Remove("border");
                _page2_5.InitValue(strValue);
            }
            else
            {
                _checkBox2.Checked = false;
            }
            //padding 填充
            if (_dic.Properties.TryGetValue("padding-bottom", out strValue))
            {
                _dic.Properties.Remove("padding-bottom");
                _page3_1.InitValue(strValue);
            }
            if (_dic.Properties.TryGetValue("padding-top", out strValue))
            {
                _dic.Properties.Remove("padding-top");
                _page3_2.InitValue(strValue);
            }
            if (_dic.Properties.TryGetValue("padding-right", out strValue))
            {
                _dic.Properties.Remove("padding-right");
                _page3_3.InitValue(strValue);
            }
            if (_dic.Properties.TryGetValue("padding-left", out strValue))
            {
                _dic.Properties.Remove("padding-left");
                _page3_4.InitValue(strValue);
            }
            if (_dic.Properties.TryGetValue("padding", out strValue))
            {
                _checkBox3.Checked = true;
                _dic.Properties.Remove("padding");
                _page3_5.InitValue(strValue);
            }
            else
            {
                _checkBox3.Checked = false;
            }
            this._dic = _dic;
        }

        private KeyValuePair<string, int> GetModelSize(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return new KeyValuePair<string, int>("", 0);
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

        void ChangedCss(CssSection cssSection,string name,int[] oldSize,int index)
        {
            string str = "";
            int wLeft = 0;
            int wRight = 0;
            int hTop = 0;
            int hBottom = 0;

            int newSize = 0;

            KeyValuePair<string, int> kkvv = new KeyValuePair<string, int>();

            if (cssSection.Properties.TryGetValue(name, out str))
            {
                if (!string.IsNullOrEmpty(str))
                {
                    kkvv = GetModelSize(str);
                    if (!string.IsNullOrEmpty(kkvv.Key))
                    {
                        newSize = Convert.ToInt32(kkvv.Key);
                    }
                }
            }
            else
                return;

            switch (index)
            {
                case 0:
                    #region
                    {
                        wLeft -= (oldSize[4] + oldSize[0] - newSize);
                        wRight -= (oldSize[3] + oldSize[0] - newSize);
                        hTop -= (oldSize[2] + oldSize[0] - newSize);
                        hBottom -= (oldSize[1] + oldSize[0] - newSize);
                    }
                    #endregion
                    break;
                case 1:
                    #region
                    {
                        hBottom -= (oldSize[1] + oldSize[0] - newSize);
                    }
                    #endregion
                    break;
                case 2:
                    #region
                    {
                        hTop -= (oldSize[2] + oldSize[0] - newSize);
                    }
                    #endregion
                    break;
                case 3:
                    #region
                    {
                        wRight -= (oldSize[3] + oldSize[0] - newSize); 
                    }
                    #endregion
                    break;
                case 4:
                    #region
                    {
                        wLeft -= (oldSize[4] + oldSize[0] - newSize);
                    }
                    #endregion
                    break;
                default:
                    break;
            }

            KeyValuePair<string, int> kv = new KeyValuePair<string, int>();
            if (_dic.Properties.TryGetValue("width", out str))
            {
                kv = Service.Util.GetSizeAndUnit(str);
                if (kv.Value == kkvv.Value)
                {
                    string unit = Service.Util.GetUnitByInt(kv.Value);
                    int w = Convert.ToInt32(kv.Key);
                    w = w - wLeft - wRight;
                    _dic.Properties["width"] = w.ToString() + unit;
                }
            }

            if (_dic.Properties.TryGetValue("height", out str))
            {
                kv = Service.Util.GetSizeAndUnit(str);
                if (kv.Value == kkvv.Value)
                {
                    string unit = Service.Util.GetUnitByInt(kv.Value);
                    int h = Convert.ToInt32(kv.Key);
                    h = h - hTop - hBottom;
                    _dic.Properties["height"] = h.ToString() + unit;
                }
            }
        }

        private int[] GetOldSize(CssSection cssSection, string name)
        {
            int[] oldSize = new int[5];
            string oldStr = "";
            KeyValuePair<string, int> kv = new KeyValuePair<string, int>("", 0);
            if (cssSection.Properties.TryGetValue(name, out oldStr))
            {
                if (!string.IsNullOrEmpty(oldStr))
                {
                    kv = GetModelSize(oldStr);
                    if (!string.IsNullOrEmpty(kv.Key))
                        oldSize[0] = Convert.ToInt32(kv.Key);
                    else
                        oldSize[0] = 0;
                }
            }
            else
                oldSize[0] = 0;

            if (cssSection.Properties.TryGetValue(name+"-bottom", out oldStr))
            {
                if (!string.IsNullOrEmpty(oldStr))
                {
                    kv = GetModelSize(oldStr);
                    if (!string.IsNullOrEmpty(kv.Key))
                        oldSize[1] = Convert.ToInt32(kv.Key);
                    else
                        oldSize[1] = 0;
                }
            }
            else
                oldSize[1] = 0;

            if (cssSection.Properties.TryGetValue(name + "-top", out oldStr))
            {
                if (!string.IsNullOrEmpty(oldStr))
                {
                    kv = GetModelSize(oldStr);
                    if (!string.IsNullOrEmpty(kv.Key))
                        oldSize[2] = Convert.ToInt32(kv.Key);
                    else
                        oldSize[2] = 0;
                }
            }
            else
                oldSize[2] = 0;

            if (cssSection.Properties.TryGetValue(name + "-right", out oldStr))
            {
                if (!string.IsNullOrEmpty(oldStr))
                {
                    kv = GetModelSize(oldStr);
                    if (!string.IsNullOrEmpty(kv.Key))
                        oldSize[3] = Convert.ToInt32(kv.Key);
                    else
                        oldSize[3] = 0;
                }
            }
            else
                oldSize[3] = 0;

            if (cssSection.Properties.TryGetValue(name + "-left", out oldStr))
            {
                if (!string.IsNullOrEmpty(oldStr))
                {
                    kv = GetModelSize(oldStr);
                    if (!string.IsNullOrEmpty(kv.Key))
                        oldSize[4] = Convert.ToInt32(kv.Key);
                    else
                        oldSize[4] = 0;
                }
            }
            else
                oldSize[4] = 0;
            return oldSize;
        }

        #endregion
    }
}
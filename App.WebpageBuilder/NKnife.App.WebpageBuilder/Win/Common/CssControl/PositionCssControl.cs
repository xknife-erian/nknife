using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    [ResReader(false)]
    public partial class PositionCssControl : CssMainControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PositionCssControl()
        {
            InitializeComponent();
            if (!_isUsingAutoSize)
            {
                checkBoxHeightAuto.Visible = false;
                checkBoxHeightAuto.Enabled = false;
                checkBoxWidthAuto.Visible = false;
                checkBoxWidthAuto.Enabled = false;
                cssHeight.Width = 195;
                cssWidth.Width = 195;
            }
            else
            {
                checkBoxHeightAuto.Visible = true;
                checkBoxHeightAuto.Enabled = true;
                checkBoxWidthAuto.Visible = true;
                checkBoxWidthAuto.Enabled = true;
                cssHeight.Width = 141;
                cssWidth.Width = 141;
            }
            Init();
        }

        #region 内部变量

        bool _isUsingAutoSize = false;

        #endregion

        #region 公共属性

        /// <summary>
        /// 获取宽度是否自动设置
        /// </summary>
        public bool WidthAuto { get; private set; }

        /// <summary>
        /// 获取高度是否自动设置
        /// </summary>
        public bool HeightAuto { get; private set; }

        #endregion

        #region 重写函数

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public override bool LeaveValidate()
        {
            #region 写入当前值
            //类型
            CssResourceList list = CssResources.Resources["position"];
            string str = cmbPosition.Text;
            if (list.HasValue(str))
            {
                str = list.GetValue(str);
            }
            Value.Properties["position"] = str;
            //浮动
            list = CssResources.Resources["float"];
            str = cmbFloat.Text;
            if (list.HasValue(str))
            {
                str = list.GetValue(str);
            }
            Value.Properties["float"] = str;
            //显示
            list = CssResources.Resources["visibility"];
            str = cmbVisibility.Text;
            if (list.HasValue(str))
            {
                str = list.GetValue(str);
            }
            Value.Properties["visibility"] = str;
            //Z轴
            list = CssResources.Resources["z-index"];
            str = cmbZindex.Text;
            if (list.HasValue(str))
            {
                str = list.GetValue(str);
            }
            Value.Properties["z-index"] = str;
            //溢出
            list = CssResources.Resources["overflow"];
            str = cmbOverflow.Text;
            if (list.HasValue(str))
            {
                str = list.GetValue(str);
            }
            Value.Properties["overflow"] = str;
            //清除
            list = CssResources.Resources["clear"];
            str = cmbClear.Text;
            if (list.HasValue(str))
            {
                str = list.GetValue(str);
            }
            Value.Properties["clear"] = str;

            //宽度
            if (!WidthAuto)
            {
                Value.Properties["width"] = cssWidth.Value;
            }
            //高度
            if (!HeightAuto)
            {
                Value.Properties["height"] = cssHeight.Value;
            }
            //上
            Value.Properties["top"] = cssTop.Value;
            //右
            Value.Properties["right"] = cssRight.Value;
            //下
            Value.Properties["bottom"] = cssBottom.Value;
            //左
            Value.Properties["left"] = cssLeft.Value;
            //剪辑rect(auto,123px,123px,123px)
            string clip1 = "auto";
            string clip2 = "auto";
            string clip3 = "auto";
            string clip4 = "auto";
            if (string.IsNullOrEmpty(cssClipTop.Value) && string.IsNullOrEmpty(cssClipRight.Value) && string.IsNullOrEmpty(cssClipBottom.Value) && string.IsNullOrEmpty(cssClipLeft.Value))
            {
                Value.Properties["clip"] = "";
            }
            else
            {
                if (!string.IsNullOrEmpty(cssClipTop.Value))
                {
                    clip1 = cssClipTop.Value;
                }
                if (!string.IsNullOrEmpty(cssClipRight.Value))
                {
                    clip2 = cssClipRight.Value;
                }
                if (!string.IsNullOrEmpty(cssClipBottom.Value))
                {
                    clip3 = cssClipBottom.Value;
                }
                if (!string.IsNullOrEmpty(cssClipLeft.Value))
                {
                    clip4 = cssClipLeft.Value;
                }
                Value.Properties["clip"] = "rect(" + clip1 + "," + clip2 + "," + clip3 + "," + clip4 + ")";
            }
            
            #endregion

            foreach (Control control in Controls)
            {
                string curText = "";
                string cssName = "";
                Type type = control.GetType();
                if (type == typeof(ComboBox))
                {
                    #region 下拉框
                    ComboBox cmb = (ComboBox)control;
                    curText = cmb.Text;
                    bool _isError = false;
                    switch (cmb.Name)
                    {
                        case "cmbPosition":
                            cssName = "position";
                            _isError = CssResources.CheckValue(cssName, curText, false);
                            break;
                        case "cmbFloat":
                            cssName = "float";
                            _isError = CssResources.CheckValue(cssName, curText, false);
                            break;
                        case "cmbVisibility":
                            cssName = "visibility";
                            _isError = CssResources.CheckValue(cssName, curText, false);
                            break;
                        case "cmbZindex":
                            cssName = "z-index";
                            double d = 0;
                            if (curText == "(值)")
                            {
                                CssUtility.ShowReplaceMsg(curText);
                                cmb.Focus();
                                return false;
                            }
                            if (curText != "自动" && curText != "auto" && !string.IsNullOrEmpty(curText) && !Double.TryParse(curText, out d))
                            {
                                _isError = true;
                            }
                            else
                                _isError = false;
                            break;
                        case "cmbOverflow":
                            cssName = "overflow";
                            _isError = CssResources.CheckValue(cssName, curText, false);
                            break;
                        case "cmbClear":
                            cssName = "clear";
                            _isError = CssResources.CheckValue(cssName, curText, false);
                            break;
                        default:
                            break;
                    }
                    if (_isError)
                    {
                        if (!CssUtility.ShowNotStandard(curText))
                        {
                            cmb.Focus();
                            return false;
                        }
                    }
                    #endregion
                }
                if (type == typeof(GroupBox))
                {
                    foreach (Control item in control.Controls)
                    {
                        if (item.GetType() == typeof(CssFieldUnit))
                        {
                            #region 数值单位型
                            CssFieldUnit cfu = (CssFieldUnit)item;
                            curText = cfu.Value;
                            cssName = cfu.Name;
                            if (!cfu.CheckValue())
                            {
                                cfu.SelectFirst();
                                return false;
                            }
                            #endregion
                        }
                    }
                }
            }
            
            return base.LeaveValidate();
        }

        public override void EnterLoad()
        {
            InitControls();
            base.EnterLoad();
        }

        #endregion

        #region 控件事件
        
        #region checkBox控件事件

        private void checkBoxWidthAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxWidthAuto.Checked)
            {
                if (MessageService.Show("使用自动可以智能排版，确认不使用自动吗？", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    cssWidth.Enabled = false;
                    checkBoxWidthAuto.Checked = true;
                    WidthAuto = true;
                }
                else
                {
                    cssWidth.Enabled = true;
                    WidthAuto = false;
                }
            }
            else
            {
                cssWidth.Enabled = false;
                WidthAuto = true;
            }
        }

        private void checkBoxHeightAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxHeightAuto.Checked)
            {
                if (MessageService.Show("使用自动可以智能排版，确认不使用自动吗？", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    cssHeight.Enabled = false;
                    checkBoxHeightAuto.Checked = true;
                    HeightAuto = true;
                }
                else
                {
                    cssHeight.Enabled = true;
                    HeightAuto = false;
                }
            }
            else
            {
                cssHeight.Enabled = false;
                HeightAuto = true;
            }
        }

        #endregion

        #endregion

        #region 内部方法

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            CssResourceList list = new CssResourceList();

            #region 初始化comBox控件

            //类型
            cmbPosition.Items.Clear();
            list = CssResources.Resources["position"];
            foreach (string text in list.Texts)
            {
                cmbPosition.Items.Add(text);
            }
            //浮动
            cmbFloat.Items.Clear();
            list = CssResources.Resources["float"];
            foreach (string text in list.Texts)
            {
                cmbFloat.Items.Add(text);
            }
            //显示
            cmbVisibility.Items.Clear();
            list = CssResources.Resources["visibility"];
            foreach (string text in list.Texts)
            {
                cmbVisibility.Items.Add(text);
            }
            //Z轴
            cmbZindex.Items.Clear();
            list = CssResources.Resources["z-index"];
            foreach (string text in list.Texts)
            {
                cmbZindex.Items.Add(text);
            }
            //溢出
            cmbOverflow.Items.Clear();
            list = CssResources.Resources["overflow"];
            foreach (string text in list.Texts)
            {
                cmbOverflow.Items.Add(text);
            }
            //清除
            cmbClear.Items.Clear();
            list = CssResources.Resources["clear"];
            foreach (string text in list.Texts)
            {
                cmbClear.Items.Add(text);
            }

            #endregion
        }

        /// <summary>
        /// 初始化状态
        /// </summary>
        private void InitControls()
        {
            if (Value == null)
            {
                return;
            }

            string cssValue = "";

            checkBoxHeightAuto.Checked = true;
            checkBoxWidthAuto.Checked = true;

            #region 类型、宽、高、浮动

            if (Value.Properties.TryGetValue("position",out cssValue))
            {
                string str = CssResources.Resources["position"].ValueToText(cssValue);
                cmbPosition.Text = string.IsNullOrEmpty(str) ? cssValue : str;
            }
            if (Value.Properties.TryGetValue("width", out cssValue))
            {
                cssWidth.Value = cssValue;
            }
            if (Value.Properties.TryGetValue("height", out cssValue))
            {
                cssHeight.Value = cssValue;
            }
            if (Value.Properties.TryGetValue("float", out cssValue))
            {
                string str = CssResources.Resources["float"].ValueToText(cssValue);
                cmbFloat.Text = string.IsNullOrEmpty(str) ? cssValue : str; 
            }

            #endregion

            #region 显示、Z轴、溢出、清除

            if (Value.Properties.TryGetValue("visibility", out cssValue))
            {
                string str = CssResources.Resources["visibility"].ValueToText(cssValue);
                cmbVisibility.Text = string.IsNullOrEmpty(str) ? cssValue : str; 
            }
            if (Value.Properties.TryGetValue("z-index", out cssValue))
            {
                double d = 0;
                if (Double.TryParse(cssValue, out d))
                {
                    cmbZindex.Text = d.ToString();
                }
                else
                {
                    string str = CssResources.Resources["z-index"].ValueToText(cssValue);
                    cmbZindex.Text = string.IsNullOrEmpty(str) ? cssValue : str;
                }
            }
            if (Value.Properties.TryGetValue("overflow", out cssValue))
            {
                string str = CssResources.Resources["overflow"].ValueToText(cssValue);
                cmbOverflow.Text = string.IsNullOrEmpty(str) ? cssValue : str; 
            }
            if (Value.Properties.TryGetValue("clear", out cssValue))
            {
                string str = CssResources.Resources["clear"].ValueToText(cssValue);
                cmbClear.Text = string.IsNullOrEmpty(str) ? cssValue : str; 
            }           

            #endregion

            #region 定位
            
            if (Value.Properties.TryGetValue("top", out cssValue))
            {
                cssTop.Value = cssValue;
            }
            if (Value.Properties.TryGetValue("right", out cssValue))
            {
                cssRight.Value = cssValue;
            }
            if (Value.Properties.TryGetValue("bottom", out cssValue))
            {
                cssBottom.Value = cssValue;
            }
            if (Value.Properties.TryGetValue("left", out cssValue))
            {
                cssLeft.Value = cssValue;
            }

            #endregion

            #region 剪辑
                        
            if (Value.Properties.TryGetValue("clip", out cssValue))
            {
                //rect(auto,123px,123px,123px)
                string ss = cssValue.Substring(5, cssValue.Length - 6);
                //cssValue.Remove(cssValue.Length - 1, 1);
                //cssValue.Remove(0, 5);
                char[] c = new char[]{','};
                string[] s = ss.Split(c);
                cssClipTop.Value = s[0];
                cssClipRight.Value = s[1];
                cssClipBottom.Value = s[2];
                cssClipLeft.Value = s[3];
            }

            #endregion

        }

        #endregion
    }
}

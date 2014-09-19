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
    public partial class BoxCssControl : CssMainControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BoxCssControl()
        {
            InitializeComponent();
            Init();
        }

        #region 内部变量

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
            #region 写入Css值

            CssResourceList list = CssResources.Resources["borderStyle"];
            if (WidthAuto)
            {
                Value.Properties["padding-right"] = cssPaddingRight.Value;
                Value.Properties["padding-left"] = cssPaddingLeft.Value;

                Value.Properties["margin-right"] = cssMarginRight.Value;
                Value.Properties["margin-left"] = cssMarginLeft.Value;

                Value.Properties["border-right-style"] = list.GetValueAny(cmbStyleRight.Text);
                Value.Properties["border-left-style"] = list.GetValueAny(cmbStyleLeft.Text);

                Value.Properties["border-right-width"] = cssBorderWidthRight.Value;
                Value.Properties["border-left-width"] = cssBorderWidthLeft.Value;

                Value.Properties["border-right-color"] = cssBorderColorRight.ColorText;
                Value.Properties["border-left-color"] = cssBorderColorLeft.ColorText;
            }
            else
            {
                Value.Properties["padding-right"] = cssPaddingRight.Value;
                Value.Properties["padding-left"] = cssPaddingLeft.Value;

                Value.Properties["margin-right"] = cssMarginRight.Value;
                Value.Properties["margin-left"] = cssMarginLeft.Value;

                Value.Properties["border-right-style"] = list.GetValueAny(cmbStyleRight.Text);
                Value.Properties["border-left-style"] = list.GetValueAny(cmbStyleLeft.Text);

                Value.Properties["border-right-width"] = cssBorderWidthRight.Value;
                Value.Properties["border-left-width"] = cssBorderWidthLeft.Value;

                Value.Properties["border-right-color"] = cssBorderColorRight.ColorText;
                Value.Properties["border-left-color"] = cssBorderColorLeft.ColorText;
            }
            if (HeightAuto)
            {
                //填充
                Value.Properties["padding-top"] = cssPaddingUp.Value;
                Value.Properties["padding-bottom"] = cssPaddingDown.Value;
                //边界
                Value.Properties["margin-top"] = cssMarginUp.Value;
                Value.Properties["margin-bottom"] = cssMarginDown.Value;
                //边框
                //***样式

                Value.Properties["border-top-style"] = list.GetValueAny(cmbStyleUp.Text);
                Value.Properties["border-bottom-style"] = list.GetValueAny(cmbStyleDown.Text);
                //***宽度
                Value.Properties["border-top-width"] = cssBorderWidthUp.Value;
                Value.Properties["border-bottom-width"] = cssBorderWidthDown.Value;
                //***颜色
                Value.Properties["border-top-color"] = cssBorderColorUp.ColorText;
                Value.Properties["border-bottom-color"] = cssBorderColorDown.ColorText;
            }
            else
            {
                //填充
                Value.Properties["padding-top"] = cssPaddingUp.Value;
                Value.Properties["padding-bottom"] = cssPaddingDown.Value;
                //边界
                Value.Properties["margin-top"] = cssMarginUp.Value;
                Value.Properties["margin-bottom"] = cssMarginDown.Value;
                //边框
                //***样式

                Value.Properties["border-top-style"] = list.GetValueAny(cmbStyleUp.Text);
                Value.Properties["border-bottom-style"] = list.GetValueAny(cmbStyleDown.Text);
                //***宽度
                Value.Properties["border-top-width"] = cssBorderWidthUp.Value;
                Value.Properties["border-bottom-width"] = cssBorderWidthDown.Value;
                //***颜色
                Value.Properties["border-top-color"] = cssBorderColorUp.ColorText;
                Value.Properties["border-bottom-color"] = cssBorderColorDown.ColorText;
            }

            #endregion

            #region check

            if (checkBoxPadding.Checked)
            {
                if (!cssPaddingUp.CheckValue())
                {
                    cssPaddingUp.SelectFirst();
                    return false;
                }
            }
            else
            {
                foreach (Control control in tabPagePadding.Controls)
                {
                    if (control.GetType() == typeof(CssFieldUnit))
                    {
                        CssFieldUnit cfu = (CssFieldUnit)control;
                        if (!cfu.CheckValue())
                        {
                            cfu.SelectFirst();
                            return false;
                        }
                    }
                }
            }
            if (checkBoxMargin.Checked)
            {
                if (!cssMarginUp.CheckValue())
                {
                    cssMarginUp.SelectFirst();
                    return false;
                }
            }
            else
            {
                foreach (Control control in tabPageMargin.Controls)
                {
                    if (control.GetType() == typeof(CssFieldUnit))
                    {
                        CssFieldUnit cfu = (CssFieldUnit)control;
                        if (!cfu.CheckValue())
                        {
                            cfu.SelectFirst();
                            return false;
                        }
                    }
                }
            }
            if (checkBoxBoderStyle.Checked)
            {
                list = CssResources.Resources["borderStyle"];
                if (!list.HasValue(cmbStyleUp.Text) && !string.IsNullOrEmpty(cmbStyleUp.Text))
                {
                    if (!CssUtility.ShowNotStandard(cmbStyleUp.Text))
                    {
                        cmbStyleUp.Focus();
                        return false;
                    }
                }
            }
            else
            {
                foreach (Control control in groupStyle.Controls)
                {
                    if (control.GetType() == typeof(ComboBox))
                    {
                        ComboBox cfu = (ComboBox)control;
                        if (!list.HasValue(cfu.Text) && !string.IsNullOrEmpty(cfu.Text))
                        {
                            if (!CssUtility.ShowNotStandard(cfu.Text))
                            {
                                cfu.Focus();
                                return false;
                            }
                        }
                    }
                }
            }
            
            if (checkBoxBorderWidth.Checked)
            {
                if (!cssBorderWidthUp.CheckValue())
                {
                    return false;
                }
            }
            else
            {
                foreach (Control control in groupBoxWidth.Controls)
                {
                    if (control.GetType() == typeof(CssFieldUnit))
                    {
                        CssFieldUnit cfu = (CssFieldUnit)control;
                        if (!cfu.CheckValue())
                        {
                            cfu.SelectFirst();
                            return false;
                        }
                    }
                }
            }

            return base.LeaveValidate();

            #endregion
        }

        public override void EnterLoad()
        {
            InitControls();
            base.EnterLoad();
        }

        #endregion

        #region 控件事件

        #region 复选框

        /// <summary>
        /// 填充的“是否全部”选择框变化
        /// </summary>
        private void checkBoxPadding_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPadding.Checked)
            {
                cssPaddingRight.Enabled = false;
                cssPaddingDown.Enabled = false;
                cssPaddingLeft.Enabled = false;
            }
            else
            {
                cssPaddingRight.Enabled = true;
                cssPaddingDown.Enabled = true;
                cssPaddingLeft.Enabled = true;
            }
        }

        /// <summary>
        /// 边界的“是否全部”选择框变化
        /// </summary>
        private void checkBoxMargin_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMargin.Checked)
            {
                cssMarginRight.Enabled = false;
                cssMarginDown.Enabled = false;
                cssMarginLeft.Enabled = false;
            }
            else
            {
                cssMarginRight.Enabled = true;
                cssMarginDown.Enabled = true;
                cssMarginLeft.Enabled = true;
            }
        }

        /// <summary>
        /// 边框样式的“是否全部”选择框变化
        /// </summary>
        private void checkBoxBoderStyle_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBoderStyle.Checked)
            {
                cmbStyleRight.Enabled = false;
                cmbStyleDown.Enabled = false;
                cmbStyleLeft.Enabled = false;
            }
            else
            {
                cmbStyleRight.Enabled = true;
                cmbStyleDown.Enabled = true;
                cmbStyleLeft.Enabled = true;
            }
        }

        /// <summary>
        /// 边框宽度的“是否全部”选择框变化
        /// </summary>
        private void checkBoxBorderWidth_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBorderWidth.Checked)
            {
                cssBorderWidthRight.Enabled = false;
                cssBorderWidthDown.Enabled = false;
                cssBorderWidthLeft.Enabled = false;
            }
            else
            {
                cssBorderWidthRight.Enabled = true;
                cssBorderWidthDown.Enabled = true;
                cssBorderWidthLeft.Enabled = true;
            }
        }

        /// <summary>
        /// 边框颜色的“是否全部”选择框变化
        /// </summary>
        private void checkBoxBorderColor_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBorderColor.Checked)
            {
                cssBorderColorRight.Enabled = false;
                cssBorderColorDown.Enabled = false;
                cssBorderColorLeft.Enabled = false;
            }
            else
            {
                cssBorderColorRight.Enabled = true;
                cssBorderColorDown.Enabled = true;
                cssBorderColorLeft.Enabled = true;
            }
        }

        #endregion

        private void cssPaddingUp_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxPadding.Checked)
            {
                cssPaddingRight.Value = cssPaddingUp.Value;
                cssPaddingDown.Value = cssPaddingUp.Value;
                cssPaddingLeft.Value = cssPaddingUp.Value;
            }
        }

        private void cssMarginUp_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxMargin.Checked)
            {
                cssMarginRight.Value = cssMarginUp.Value;
                cssMarginDown.Value = cssMarginUp.Value;
                cssMarginLeft.Value = cssMarginUp.Value;
            }
        }

        private void cssBorderColorUp_ColorTextChanged(object sender, EventArgs e)
        {
            if (checkBoxBorderColor.Checked)
            {
                cssBorderColorRight.ColorText = cssBorderColorUp.ColorText;
                cssBorderColorDown.ColorText = cssBorderColorUp.ColorText;
                cssBorderColorLeft.ColorText = cssBorderColorUp.ColorText;
            }
        }

        private void cssBorderWidthUp_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxBorderWidth.Checked)
            {
                cssBorderWidthRight.Value = cssBorderWidthUp.Value;
                cssBorderWidthDown.Value = cssBorderWidthUp.Value;
                cssBorderWidthLeft.Value = cssBorderWidthUp.Value;
            }
        }

        private void cmbStyleUp_TextChanged(object sender, EventArgs e)
        {
            if (checkBoxBoderStyle.Checked)
            {
                cmbStyleRight.Text = cmbStyleUp.Text;
                cmbStyleDown.Text = cmbStyleUp.Text;
                cmbStyleLeft.Text = cmbStyleUp.Text;
            }
        }


        #endregion

        #region 内部方法

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            cmbStyleDown.Items.Clear();
            cmbStyleUp.Items.Clear();
            cmbStyleRight.Items.Clear();
            cmbStyleLeft.Items.Clear();

            CssResourceList list = new CssResourceList();

            list = CssResources.Resources["borderStyle"];
            foreach (string text in list.Texts)
            {
                cmbStyleDown.Items.Add(text);
                cmbStyleLeft.Items.Add(text);
                cmbStyleRight.Items.Add(text);
                cmbStyleUp.Items.Add(text);
            }

        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitControls()
        {
            if (Value == null)
            {
                return;
            }
            string cssValue = "";

            #region 填充控件
            string _top, _right, _bottom, _left;
            if (Value.Properties.TryGetValue("padding-top", out _top))
            {
                cssPaddingUp.Value = _top;
            }
            if (Value.Properties.TryGetValue("padding-right", out _right))
            {
                cssPaddingRight.Value = _right;
            }
            if (Value.Properties.TryGetValue("padding-bottom", out _bottom))
            {
                cssPaddingDown.Value = _bottom;
            }
            if (Value.Properties.TryGetValue("padding-left", out _left))
            {
                cssPaddingLeft.Value = _left;
            }
            if (_top == _right && _top == _bottom && _top == _left)
            {
                checkBoxPadding.Checked = true;
            }
            else
            {
                checkBoxPadding.Checked = false;
            }

            #endregion

            #region 边界控件

            if (Value.Properties.TryGetValue("margin-top", out _top))
            {
                cssMarginUp.Value = _top;
            }
            if (Value.Properties.TryGetValue("margin-right", out _right))
            {
                cssMarginRight.Value = _right;
            }
            if (Value.Properties.TryGetValue("margin-bottom", out _bottom))
            {
                cssMarginDown.Value = _bottom;
            }
            if (Value.Properties.TryGetValue("margin-left", out _left))
            {
                cssMarginLeft.Value = _left;
            }
            if (_top == _right && _top == _bottom && _top == _left)
            {
                checkBoxMargin.Checked = true;
            }
            else
            {
                checkBoxMargin.Checked = false;
            }            

            #endregion

            #region 边框控件

            if (Value.Properties.TryGetValue("border-top-style", out _top))
            {
                cmbStyleUp.Text = _top;
            }
            if (Value.Properties.TryGetValue("border-right-style", out _right))
            {
                cmbStyleRight.Text = _right;
            }
            if (Value.Properties.TryGetValue("border-bottom-style", out _bottom))
            {
                cmbStyleDown.Text = _bottom;
            }
            if (Value.Properties.TryGetValue("border-left-style", out _left))
            {
                cmbStyleLeft.Text = _left;
            }
            if (_top == _right && _top == _bottom && _top == _left)
            {
                checkBoxBoderStyle.Checked = true;
            }
            else
            {
                checkBoxBoderStyle.Checked = false;
            }      

            if (Value.Properties.TryGetValue("border-top-width", out _top))
            {
                cssBorderWidthUp.Value = _top;
            }
            if (Value.Properties.TryGetValue("border-right-width", out _right))
            {
                cssBorderWidthRight.Value = _right;
            }
            if (Value.Properties.TryGetValue("border-bottom-width", out _bottom))
            {
                cssBorderWidthDown.Value = _bottom;
            }
            if (Value.Properties.TryGetValue("border-left-width", out _left))
            {
                cssBorderWidthLeft.Value = _left;
            }
              if (_top == _right && _top == _bottom && _top == _left)
            {
                checkBoxBorderWidth.Checked = true;
            }
            else
            {
                checkBoxBorderWidth.Checked = false;
            }  

            if (Value.Properties.TryGetValue("border-top-color", out _top))
            {
                cssBorderColorUp.ColorText = _top;
            }
            if (Value.Properties.TryGetValue("border-right-color", out _right))
            {
                cssBorderColorRight.ColorText = _right;
            }
            if (Value.Properties.TryGetValue("border-bottom-color", out _bottom))
            {
                cssBorderColorDown.ColorText = _bottom;
            }
            if (Value.Properties.TryGetValue("border-left-color", out _left))
            {
                cssBorderColorLeft.ColorText = _left;
            }
              if (_top == _right && _top == _bottom && _top == _left)
            {
                checkBoxBorderColor.Checked = true;
            }
            else
            {
                checkBoxBorderColor.Checked = false;
            }  
            

            #endregion
        }

        #endregion

    }
}

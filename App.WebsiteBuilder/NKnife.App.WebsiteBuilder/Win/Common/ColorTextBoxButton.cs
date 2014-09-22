using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    public partial class ColorTextBoxButton : UserControl
    {
        public ColorTextBoxButton()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        #region 公共属性

        /// <summary>
        /// 获取或设置颜色代码字符串
        /// </summary>
        public string ColorText
        {
            get
            {
                return textBoxColor.Text;
            }
            set
            {
                if (!CssUtility.IsEffectiveColor(value))
                {
                    textBoxColor.Text = value;
                }
                else
                {
                    textBoxColor.Text = value; int x = 0;
                    if (int.TryParse(value, out x))
                    {
                        btnColor.MyColor = ColorTranslator.FromWin32(x);
                    }
                    else
                    {
                        btnColor.MyColor = ColorTranslator.FromHtml(value.Substring(0, 7));
                    }
                }
            }
        }

        public Color Color
        { 
            get { return btnColor.MyColor; }
            set { btnColor.MyColor = value; }
        }

        /// <summary>
        /// 检查颜色
        /// </summary>
        /// <returns></returns>
        public bool CheckColor{get;set;}

        #endregion


        private void btnColor_MyColorChanged(object sender, EventArgs e)
        {
            ColorText = ColorTranslator.ToHtml(btnColor.MyColor);
            CheckColor = true;
            OnColorTextChanged(EventArgs.Empty);
        }

        #region 自定义事件

        /// <summary>
        /// 当color属性变化时
        /// </summary>
        public event EventHandler ColorTextChanged;

        /// <summary>
        /// 当Css属性变化时
        /// </summary>
        protected virtual void OnColorTextChanged(EventArgs e)
        {
            if (ColorTextChanged != null)
            {
                ColorTextChanged(this, e);
            }
        }

        #endregion

        private void ColorTextBoxButton_Leave(object sender, EventArgs e)
        {
            string colorText = textBoxColor.Text;
            Color color = btnColor.MyColor;
            if (string.IsNullOrEmpty(colorText))
            {
                //btnColor.MyColor = SystemColors.Control;
                CheckColor = true;
            }
            else
            {
                CheckColor = CssUtility.IsEffectiveColor(colorText);
                if (CheckColor)
                {
                    CheckColor = true;
                    int x = 0;
                    if (int.TryParse(colorText, out x))
                    {
                        string xx = x.ToString();
                        int l = xx.Length;
                        if (xx.Length < 6)
                        {
                            for (int i = 0; i < 6-l; i++)
                            {
                                xx += "0";
                            }
                        }
                        else
                        {
                            xx = xx.Substring(0, 6);
                        }
                        xx = "#" + xx;
                        btnColor.MyColor = ColorTranslator.FromHtml(xx);
                    }
                    else
                    {
                        btnColor.MyColor = ColorTranslator.FromHtml(colorText.Substring(0, 7));
                    }

                }
                else
                {
                    CheckColor = false;
                    CssUtility.ShowNotStandardColorMsg("");
                    textBoxColor.Text = "";
                    SelectFirst();
                }
            }
        }

        /// <summary>
        /// 选中控件
        /// </summary>
        internal void SelectFirst()
        {
            textBoxColor.Focus();
        }
    }
}

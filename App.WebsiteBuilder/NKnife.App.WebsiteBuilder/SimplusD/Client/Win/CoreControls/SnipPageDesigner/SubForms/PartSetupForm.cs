using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class PartSetupForm : BaseForm
    {
        public PartSetupForm(NavigationPart part)
        {
            InitializeComponent();
            Part = part;
            cssFieldUnitWidth.Value = part.Width_Css;
            _oldWidth = part.Width_Css;

            switch (part.DisplayType)
            {
                case DisplayType.Default:
                    radioButtonDefault.Checked = true;
                    break;
                case DisplayType.Title:
                    radioButtonTitle.Checked = true;
                    break;
                case DisplayType.Pictrue:
                    radioButtonPic.Checked = true;
                    break;
                case DisplayType.AppointedText:
                    radioButtonText.Checked = true;
                    break;
                default:
                    break;
            }
        }

        #region 内部变量

        string _oldWidth = "";

        string _newWidth = "";

        DisplayType _displayType = DisplayType.Default;

        #endregion

        #region 公共属性

        /// <summary>
        /// 获取要设置的"块"-part
        /// </summary>
        public NavigationPart Part { get; private set; }

        /// <summary>
        /// 获取宽度是否改变
        /// </summary>
        public bool WidthChanged { get; private set; }

        #endregion        

        #region 公共方法

        #endregion

        #region 内部方法

        #endregion
        
        #region 事件响应

        private void radioButtonDefault_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDefault.Checked)
            {
                buttonBrowsePic.Enabled = false;
                textBoxText.Enabled = false;
                textBoxTitle.Enabled = false;
                _displayType = DisplayType.Default;
            }
        }

        private void radioButtonTitle_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTitle.Checked)
            {
                buttonBrowsePic.Enabled = false;
                textBoxText.Enabled = false;
                _displayType = DisplayType.Title;
                textBoxTitle.Text = Part.Text;
            }
        }

        private void radioButtonPic_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonPic.Checked)
            {
                buttonBrowsePic.Enabled = true;
                textBoxText.Enabled = false;
                _displayType = DisplayType.Pictrue;
            }
        }

        private void radioButtonText_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonText.Checked)
            {
                buttonBrowsePic.Enabled = false;
                textBoxText.Enabled = true;
                _displayType = DisplayType.AppointedText;
            }
        }

        private void textBoxText_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxText.Text))
            {
                textBoxText.Text = Part.Text;
                Part.AppointedText = textBoxText.Text;
            }
            else
            {
                Part.AppointedText = textBoxText.Text;
            }
        }

        private void cssFieldUnitWidth_ValueChanged(object sender, EventArgs e)
        {
            _newWidth = cssFieldUnitWidth.Value;
            if (cssFieldUnitWidth.Value != _oldWidth)
            {
                WidthChanged = true;                
            }
            else
            {
                WidthChanged = false;
            }
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (WidthChanged)
            {
                Part.Width_Css = _newWidth;
            }
            else
            {
                Part.Width_Css = _oldWidth;
            }
            
            Part.DisplayType = _displayType;
        }

        #endregion

        #region 自定义事件

        #endregion

    }
}

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
    public partial class EditPathPartForm : BaseForm
    {
        public EditPathPartForm(PathPart part)
        {
            InitializeComponent();
            _part = part;
            _displayType = part.LinkDisplayType;
            _separtorCode = part.SeparatorCode;
            textBoxSeparatorCode.Text = _separtorCode;
        }

        #region 内部变量

        PathPart _part;

        DisplayType _displayType = DisplayType.Default;

        string _separtorCode = ">>";

        #endregion

        #region 公共属性

        #endregion

        #region 事件响应

        private void radioButtonDefault_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDefault.Checked)
            {
                _displayType = DisplayType.Default;
            }
        }

        private void radioButtonTitle_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTitle.Checked)
            {
                _displayType = DisplayType.Title;
            }
        }

        private void radioButtonPic_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonPic.Checked)
            {
                _displayType = DisplayType.Pictrue;
            }
        }

        private void radioButtonText_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonText.Checked)
            {
                _displayType = DisplayType.AppointedText;
            }
        }

        private void textBoxSeparatorCode_Leave(object sender, EventArgs e)
        {
            _separtorCode = textBoxSeparatorCode.Text;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            _part.LinkDisplayType = _displayType;
            _part.SeparatorCode = _separtorCode;
        }

        #endregion

    }
}

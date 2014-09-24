using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.App.PictureTextPicker.Common.Base;
using NKnife.Ioc;
using NLog;

namespace NKnife.App.PictureTextPicker
{
    public partial class AppOptionSetting : Form
    {
        private IAppOption _AppOption = DI.Get<IAppOption>();
        private Logger _Logger = LogManager.GetCurrentClassLogger();
        public AppOptionSetting()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButtonClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ThumbWidthTextBox.Text) && !string.IsNullOrWhiteSpace(ThumbWidthTextBox.Text))
            {
                int thumbWidth;
                if (Int32.TryParse(ThumbWidthTextBox.Text, out thumbWidth))
                {
                    if (thumbWidth > 0)
                    {
                        _AppOption.SetOption("ThumbWidth",thumbWidth);
                    }
                    else
                    {
                        _Logger.Warn("缩略图宽度参数不正确");
                    }
                }
                else
                {
                    _Logger.Info("缩略图宽度参数不正确");
                }
            }

            if (!string.IsNullOrEmpty(ThumbHeightTextBox.Text) && !string.IsNullOrWhiteSpace(ThumbHeightTextBox.Text))
            {
                int thumbHeight;
                if (Int32.TryParse(ThumbHeightTextBox.Text, out thumbHeight))
                {
                    if (thumbHeight > 0)
                    {
                        _AppOption.SetOption("ThumbHeight", thumbHeight);
                    }
                    else
                    {
                        _Logger.Warn("缩略图高度参数不正确");
                    }
                }
                else
                {
                    _Logger.Info("缩略图高度参数不正确");
                }
            }
            if (!string.IsNullOrEmpty(PictureTypeComboBox.Text) && !string.IsNullOrWhiteSpace(PictureTypeComboBox.Text))
            {
                _AppOption.SetOption("PictureFileType", PictureTypeComboBox.Text);
            }

            _AppOption.SetOption("FixThumbSize", FixThumbSizeTureRadioBox.Checked);
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButtonClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}

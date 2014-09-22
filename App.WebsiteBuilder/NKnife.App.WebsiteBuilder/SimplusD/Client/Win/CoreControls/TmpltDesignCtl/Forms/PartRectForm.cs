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
    public partial class PartRectForm :  BaseForm
    {
        /// <summary>
        /// 要分割的矩形
        /// </summary>
        private Rect _rect;

        /// <summary>
        /// 分割矩形的份数
        /// </summary>
        public int PartNum { get; set; }

        #region 构造函数

        public PartRectForm(Rect rect)
        {
            _rect = rect;
            this.Text += ((SnipRect)rect).SnipName;
            InitializeComponent();
        }

        #endregion

        #region 消息处理

        private void OKBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(partNumText.Text))
            {
                DialogResult = DialogResult.Cancel;
                return;
            }
            PartNum = Convert.ToInt32(partNumText.Text);
            if (isRowRadioBtn.Checked)
            {
                if (PartNum < 2 || PartNum > _rect.Height)
                {
                    MessageBox.Show(StringParserService.Parse("${res:tmpltDesign.partRect.error.partNum}"));
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                if (PartNum < 2 || PartNum > _rect.Width)
                {
                    MessageBox.Show(StringParserService.Parse("${res:tmpltDesign.partRect.error.partNum}"));
                }
                else
                {
                    this.DialogResult = DialogResult.OK; 
                }
            }
        }

        #endregion

    }
}
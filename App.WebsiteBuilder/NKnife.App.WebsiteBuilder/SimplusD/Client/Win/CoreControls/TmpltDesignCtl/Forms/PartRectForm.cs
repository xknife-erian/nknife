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
        /// Ҫ�ָ�ľ���
        /// </summary>
        private Rect _rect;

        /// <summary>
        /// �ָ���εķ���
        /// </summary>
        public int PartNum { get; set; }

        #region ���캯��

        public PartRectForm(Rect rect)
        {
            _rect = rect;
            this.Text += ((SnipRect)rect).SnipName;
            InitializeComponent();
        }

        #endregion

        #region ��Ϣ����

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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class InsertFlashCodeForm : BaseMediaCodeForm
    {
        public InsertFlashCodeForm()
            :base()
        {
            this.Text = "插入Flash";
            this.mediaInfoGroupBox.Text = "Flash信息";
        }

        public override MediaFileType MediaType
        {
            get
            {
                return MediaFileType.Flash ;
            }
            set
            {
                base.MediaType = value;
            }
        }

        public override void medioPathBtn_Click(object sender, EventArgs e)
        {
            SetFormedioPathChange(MediaFileType.Flash);
        }

        public override void widthNumUpDown_Validated(object sender, EventArgs e)
        {
            if (limitScaleCheckBox.Checked)
            {
                heightUintComboBox.SelectedIndex =widthUintComboBox.SelectedIndex;
                heightNumUpDown.Value = Convert.ToInt32(this.widthNumUpDown.Value /Ratio);
            }
        }


        public override void heightNumUpDown_Validated(object sender, EventArgs e)
        {
            if (limitScaleCheckBox.Checked)
            {
               widthUintComboBox .SelectedIndex =heightUintComboBox .SelectedIndex;
               widthNumUpDown.Value = Convert.ToInt32(this.heightNumUpDown.Value * Ratio);
            }
        }
        #region 对外属性抛出
        public Flash.FlashQuality FlashQuality
        {
            get
            {
                if (qualityComboBox.SelectedIndex == 0) return Flash.FlashQuality.Hight;
                else if (qualityComboBox.SelectedIndex == 1) return Flash.FlashQuality.Low;
                else if (qualityComboBox.SelectedIndex == 2) return Flash.FlashQuality.AutoHight;
                else return Flash.FlashQuality.AutoLow;
            }
            // set { flashQualityComboBox.Text = value; }
        }
        public Flash.FlashAlign FlashAlign
        {
            get
            {
                if (alignComboBox.SelectedIndex == 0) return Flash.FlashAlign.Default;
                else if (alignComboBox.SelectedIndex == 1) return Flash.FlashAlign.baseline;
                else if (alignComboBox.SelectedIndex == 2) return Flash.FlashAlign.top;
                else if (alignComboBox.SelectedIndex == 3) return Flash.FlashAlign.middle;
                else if (alignComboBox.SelectedIndex == 4) return Flash.FlashAlign.bottom;
                else if (alignComboBox.SelectedIndex == 5) return Flash.FlashAlign.texttop;
                else if (alignComboBox.SelectedIndex == 6) return Flash.FlashAlign.absolutemiddle;
                else if (alignComboBox.SelectedIndex == 7) return Flash.FlashAlign.absolutebottom;
                else if (alignComboBox.SelectedIndex == 8) return Flash.FlashAlign.left;
                else return Flash.FlashAlign.right;
            }
            // set { flashAlignComboBox.Text = value; }
        }
        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Jeelu.Win
{
    public partial class InsertFlashCodeForm : BaseMediaCodeForm
    {
        public InsertFlashCodeForm(XhtmlTagElement objEle, string path)
            : base(objEle, path)
        {
            this.Text = "插入Flash";
            this.mediaInfoGroupBox.Text = "Flash信息";         
        }

        public override void okBtn_Click(object sender, EventArgs e)
        {
            
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
        public Quality FlashQuality
        {
            get
            {
                if (qualityComboBox.SelectedIndex == 0) return Quality.Hight;
                else if (qualityComboBox.SelectedIndex == 1) return Quality.Low;
                else if (qualityComboBox.SelectedIndex == 2) return Quality.AutoHight;
                else return Quality.AutoLow;
            }
            // set { flashQualityComboBox.Text = value; }
        }
        public Align FlashAlign
        {
            get
            {
                if (alignComboBox.SelectedIndex == 0) return Align.Default;
                else if (alignComboBox.SelectedIndex == 1) return Align.baseline;
                else if (alignComboBox.SelectedIndex == 2) return Align.top;
                else if (alignComboBox.SelectedIndex == 3) return Align.middle;
                else if (alignComboBox.SelectedIndex == 4) return Align.bottom;
                else if (alignComboBox.SelectedIndex == 5) return Align.texttop;
                else if (alignComboBox.SelectedIndex == 6) return Align.absolutemiddle;
                else if (alignComboBox.SelectedIndex == 7) return Align.absolutebottom;
                else if (alignComboBox.SelectedIndex == 8) return Align.left;
                else return Align.right;
            }
            // set { flashAlignComboBox.Text = value; }
        }

        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.Win
{
    public class InsertAudioCodeForm : BaseMediaCodeForm
    {
        public InsertAudioCodeForm(XhtmlTagElement imgEle, string path)
            : base(imgEle, path)
        {
            this.Text = "插入音频";
            this.mediaInfoGroupBox.Text = "音频信息";
        }

        public override MediaFileType MediaType
        {
            get
            {
                return MediaFileType.Audio ;
            }
            set
            {
                base.MediaType = value;
            }
        }
        public override void medioPathBtn_Click(object sender, EventArgs e)
        {
            SetFormedioPathChange(MediaFileType.Audio);
        }

        #region 对外抛出属性
        public Quality AudioQuality
        {
            get
            {
                if (qualityComboBox.SelectedIndex == 0) return Quality.Hight;
                else if (qualityComboBox.SelectedIndex == 1) return Quality.Low;
                else if (qualityComboBox.SelectedIndex == 2) return Quality.AutoHight;
                else return Quality.AutoLow;
            }
        }
        public Align AudioAlign
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
        }
        #endregion

    }
}

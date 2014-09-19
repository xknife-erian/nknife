using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public class InsertAudioCodeForm : BaseMediaCodeForm
    {
        public InsertAudioCodeForm()
            :base()
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
        public Audio.Quality AudioQuality
        {
            get
            {
                if (qualityComboBox.SelectedIndex == 0) return Audio.Quality.Hight;
                else if (qualityComboBox.SelectedIndex == 1) return Audio.Quality.Low;
                else if (qualityComboBox.SelectedIndex == 2) return Audio.Quality.AutoHight;
                else return Audio.Quality.AutoLow;
            }
        }
        public Audio.Align AudioAlign
        {
            get
            {
                if (alignComboBox.SelectedIndex == 0) return Audio.Align.Default;
                else if (alignComboBox.SelectedIndex == 1) return Audio.Align.baseline;
                else if (alignComboBox.SelectedIndex == 2) return Audio.Align.top;
                else if (alignComboBox.SelectedIndex == 3) return Audio.Align.middle;
                else if (alignComboBox.SelectedIndex == 4) return Audio.Align.bottom;
                else if (alignComboBox.SelectedIndex == 5) return Audio.Align.texttop;
                else if (alignComboBox.SelectedIndex == 6) return Audio.Align.absolutemiddle;
                else if (alignComboBox.SelectedIndex == 7) return Audio.Align.absolutebottom;
                else if (alignComboBox.SelectedIndex == 8) return Audio.Align.left;
                else return Audio.Align.right;
            }
        }
        #endregion

    }
}

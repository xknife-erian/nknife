using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public class InsertVideoCodeForm : BaseMediaCodeForm
    {
        public InsertVideoCodeForm()
            : base()
        {
            this.Text = "插入视频";
            this.mediaInfoGroupBox.Text = "视频信息";
        }

        public override MediaFileType MediaType
        {
            get
            {
                return MediaFileType.Video;
            }
            set
            {
                base.MediaType = value;
            }
        }

        public override void medioPathBtn_Click(object sender, EventArgs e)
        {
            SetFormedioPathChange(MediaFileType.Video);
        }

        public Video.Quality MediaQuality
        {
            get
            {
                if (qualityComboBox.SelectedIndex == 0) return Video.Quality.Hight;
                else if (qualityComboBox.SelectedIndex == 1) return Video.Quality.Low;
                else if (qualityComboBox.SelectedIndex == 2) return Video.Quality.AutoHight;
                else return Video.Quality.AutoLow;
            }
        }
        public Video.Align MediaAlign
        {
            get
            {
                if (alignComboBox.SelectedIndex == 0) return Video.Align.Default;
                else if (alignComboBox.SelectedIndex == 1) return Video.Align.baseline;
                else if (alignComboBox.SelectedIndex == 2) return Video.Align.top;
                else if (alignComboBox.SelectedIndex == 3) return Video.Align.middle;
                else if (alignComboBox.SelectedIndex == 4) return Video.Align.bottom;
                else if (alignComboBox.SelectedIndex == 5) return Video.Align.texttop;
                else if (alignComboBox.SelectedIndex == 6) return Video.Align.absolutemiddle;
                else if (alignComboBox.SelectedIndex == 7) return Video.Align.absolutebottom;
                else if (alignComboBox.SelectedIndex == 8) return Video.Align.left;
                else return Video.Align.right;
            }
        }
    }
}

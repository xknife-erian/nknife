using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusSoftwareUpdate
{
    public partial class ProgressForm : Form
    {
        private DownloadFiles _downloadFiles;
        public ProgressForm(DownloadFiles downloadFiles)
        {
            this._downloadFiles = downloadFiles;

            InitializeComponent();
        }

        public void CallbackException(Exception ex)
        {
            if (ex != null)
            {
                MessageBox.Show("出现未处理异常：" + ex.Message);
            }
            this.Close();
        }

        public void CallbackEndRun()
        {
            this.Hide();

            ///显示最终Form
            SetupFinish setupFinish = new SetupFinish();
            setupFinish.ShowDialog();

            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_downloadFiles.IsEnd)
            {
                if (MessageBox.Show("正在下载中，确定退出吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }

            base.OnFormClosing(e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private long _prevDownloadBytesCount = -1;
        private DateTime _prevDateTime;
        private int _speed;   //每秒下载的字节数

        private void timerUpdateState_Tick(object sender, EventArgs e)
        {
            if (_prevDownloadBytesCount != _downloadFiles.DownloadBytesCount)
            {
                if (_prevDownloadBytesCount >= 0)
                {
                    long now = DateTime.Now.ToFileTimeUtc() / 10000;
                    long prevTime = _prevDateTime.ToFileTimeUtc() / 10000;
                    int tick = (int)(now - prevTime);

                    _speed = (int)((_downloadFiles.DownloadBytesCount - _prevDownloadBytesCount) / tick * 1000);
                }

                _prevDownloadBytesCount = _downloadFiles.DownloadBytesCount;
                _prevDateTime = DateTime.Now;

                //显示已下载的文件列表
                int listBoxItemCount = listBoxFiles.Items.Count;
                int downloadedFileCount = _downloadFiles.DownloadedFilesCount;
                if (listBoxItemCount < downloadedFileCount)
                {
                    for (int i = listBoxItemCount; i < downloadedFileCount; i++)
                    {
                        listBoxFiles.Items.Add(_downloadFiles.UpdateFiles[i].Path);
                    }
                }
            }

            ///显示下载进度和相关信息
            string showMsg = "总大小:{0} 已下载:{1} 速度:{2}";
            showMsg = string.Format(showMsg, FormatBytes(_downloadFiles.AllBytesCount, 2),
                FormatBytes(_downloadFiles.DownloadBytesCount, 0),
                FormatBytes(_speed, 2)+"/秒");
            lblShowMsg.Text = showMsg;
            if (_downloadFiles.DownloadBytesCount > 0)
            {
                float progress = _downloadFiles.DownloadBytesCount * 100.0f / _downloadFiles.AllBytesCount;
                mainProgressBar.Value = (int)progress;
                this.Text = string.Format("{0:0.0}% - 进度窗口", progress);
            }
        }

        static private string FormatBytes(long count,long kAfterPointCount)
        {
            float k = ((float)count) / 1024;
            if (k < 1)
            {
                return count + "字节";
            }

            float m = ((float)k) / 1024;
            if (m < 1)
            {
                string format = "0";
                if (kAfterPointCount > 0)
                {
                    format = "0.";
                    for (int i = 0; i < kAfterPointCount; i++)
                    {
                        format += "0";
                    }
                }
                return k.ToString(format) + "K字节";
            }

            return m.ToString("0.00") + "M字节";
        }
    }
}

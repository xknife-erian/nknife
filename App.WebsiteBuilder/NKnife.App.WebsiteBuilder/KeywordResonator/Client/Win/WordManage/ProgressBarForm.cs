using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.KeywordResonator.Client
{
    public partial class ProgressBarForm : Form
    {

        private long iSecond = 0;

        public ProgressBarForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 改变进度条
        /// </summary>
        public void SucceedCrawle()
        {         
            if (progressFecthWord.Value < ((WorkbenchForm)this.Owner).WordsManager.TotalPages)
            {
                progressFecthWord.Value++;
                LableGetedPage.Text = ((WorkbenchForm)this.Owner).WordsManager.GetedPage.ToString();
                //TimeSpan timeSpan = DateTime.Now - ((WorkbenchForm)this.Owner).WordsManager.StartCrawlerTime;
                //lableElapseTime.Text = timeSpan.TotalSeconds.ToString();
                lstBoxUrls.Items.Add(((WorkbenchForm)this.Owner).WordsManager.GetedUrls[((WorkbenchForm)this.Owner).WordsManager.GetedUrls.Count - 1]+" :成功");
            }
        }

        /// <summary>
        /// 设置进度条最大值
        /// </summary>
        /// <param name="max"></param>
        public void SetMaxProgress(int max)
        {
            progressFecthWord.Maximum = max;
        }

        /// <summary>
        /// 重置进度条
        /// </summary>
        public void ResetProgress()
        {
            progressFecthWord.Value = 0;
            LableGetedPage.Text = "0";
            lableElapseTime.Text = "0";
            labelFailedPage.Text = "0";
            lstBoxUrls.Items.Clear();
        }

        /// <summary>
        /// 抓取失败时处理
        /// </summary>
        internal void FailCrawle()
        {
            if (progressFecthWord.Value < ((WorkbenchForm)this.Owner).WordsManager.TotalPages)
            {
                progressFecthWord.Value++;
                labelFailedPage.Text = ((WorkbenchForm)this.Owner).WordsManager.FailedPage.ToString();
                //TimeSpan timeSpan = DateTime.Now - ((WorkbenchForm)this.Owner).WordsManager.StartCrawlerTime;
                //lableElapseTime.Text = timeSpan.TotalSeconds.ToString();
                lstBoxUrls.Items.Add(((WorkbenchForm)this.Owner).WordsManager.FailedUrls[((WorkbenchForm)this.Owner).WordsManager.FailedUrls.Count-1]+" :失败");
            }
        }

        private void timer_ElapseTime_Tick(object sender, EventArgs e)
        {
            iSecond++;
            lableElapseTime.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", iSecond / 3600, iSecond % 3600 / 60, iSecond % 60);
        }
    }
}

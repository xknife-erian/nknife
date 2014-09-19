using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Jeelu.Billboard;

namespace SegmentQuickDemo
{
    public partial class Demo : Form
    {
        public Demo()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 状态描述字符串
        /// </summary>
        public string StatusString
        {
            get { return this._StatusLabel.Text; }
            set
            {
                this._StatusLabel.Text = value;
                this.Update();
                Thread.Sleep(20);
            }
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Update();
            Thread.Sleep(10);
            this.StatusString = "初始化词库...";
            this.Init();
            this.StatusString = "WordSegmentor is Complex!";
        }

        private void 快速测试F5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.messageListBox.Items.Clear();
            this.JWordSegmentor.FilterStopWords = this.checkBox1.Checked;
            this.JWordSegmentor.AutoInsertUnknownWords = true;
            this.JWordSegmentor.AutoStudy = true;
            this.JWordSegmentor.FreqFirst = true;
            string[] keywords = this.JWordSegmentor.Segment(this.TextFile()).ToArray();
            Dictionary<string, ulong> dic = KwHelper.KeywordSortor(keywords, false);
            foreach (var item in dic)
            {
                this.AddText(item.Key + " | " + item.Value.ToString());
            }
        }

        void AddText(string str)
        {
            this.messageListBox.Items.Add(str);
        }

        /// <summary>
        /// 词库初始化
        /// </summary>
        private void Init()
        {
            this.JWordSegmentor = JWordSegmentorService.Creator("", @"segdata\");
        }
        public JWordSegmentor JWordSegmentor { get; set; }

        private string TextFile()
        {
            string file = "TextFile1.txt";
            StreamReader sr = File.OpenText(file);
            return sr.ReadToEnd();
        }

        #region MyRegion

        private void 新建NToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 退出XToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

    }
}

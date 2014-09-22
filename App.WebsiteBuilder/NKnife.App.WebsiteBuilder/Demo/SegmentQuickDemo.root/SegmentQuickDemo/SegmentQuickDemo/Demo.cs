using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.WordSeg;
using System.IO;
using System.Threading;

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
            WordSegmentor seg = new WordSegmentor();
            seg.IsFilterStopWords = true;
            seg.IsMatchName = true;
            List<string> strList = seg.Segmentor(this.TextFile());
            StringBuilder sb = new StringBuilder();
            foreach (var item in strList)
            {
                sb.Append(item).Append("/");
            }
            this.codeTextBox.Text = sb.ToString();
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

        /// <summary>
        /// 词库初始化
        /// </summary>
        private void Init()
        {
            string path = Path.Combine(Application.StartupPath, "segdata" + @"\");
            WordSegService.Initialize(
                Path.Combine(path, "worddict.wseg"),
                Path.Combine(path, "chsstopwords.wseg"),
                Path.Combine(path, "chssymbol.wseg"),
                Path.Combine(path, "engstopwords.wseg"),
                Path.Combine(path, "engsymbol.wseg"));
        }

        private string TextFile()
        {
            string file = "TextFile1.txt";
            StreamReader sr = File.OpenText(file);
            return sr.ReadToEnd();
        }

    }
}

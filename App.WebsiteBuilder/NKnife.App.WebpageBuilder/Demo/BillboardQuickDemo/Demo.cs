using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Jeelu.Billboard;
using System.Diagnostics;
using HtmlAgilityPack;
using Jeelu.WordSegmentor;

namespace BillboardQuickDemo
{
    public partial class Demo : Form
    {
        JWordSegmentor _JWordSegmentor = null;
        public Demo()
        {
            InitializeComponent();
            _JWordSegmentor = JWordSegmentorService.Creator("", @"D:\Jeelu\Demo\BillboardQuickDemo\bin\Debug\segdata\");
        }

        List<WebRuleCollection> wrcList = new List<WebRuleCollection>();

        private void 快速测试F5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string site = "http://news.ifeng.com/mil/taiwan/200807/0718_1569_661217.shtml";

            //HtmlContent hc = new HtmlContent(site);

            MessageBox.Show(JWordSegmentorService.IsExist("互联网").ToString());

            //MessageBox.Show(JWordSegmentorService.GetFrequency("芦侃").ToString());
















            //Uri uri = new Uri("http://www.spiffycorners.com/sc.php? sc=my & bg=000000 & fg=ffffff ");

            //string key = @"www.spiffycorners.com/sc.php?scbgfg";

            //WebRule wr = new WebRule(uri);

            //this.propertyGrid.SelectedObject = wr;

            //WebRuleCollection wrc = new WebRuleCollection(uri);
            //for (int i = 0; i < wrc.Count; i++)
            //{
            //    WebRule r = wrc[i];
            //    string md5 = r.ToString();
            //}


            //JDictionaryFileManager filemgr = new JDictionaryFileManager();
            //filemgr.Load(JDictionaryTypeEnum.ChannelDictionary, @"testtest\index.xml");
            //filemgr.AddDictFile(i.ToString(), (i-1).ToString());
            //filemgr.Save();
            //i++;
            //this.propertyGrid.SelectedObject = filemgr;
        }

        void Add(object obj)
        {
            this.messageListBox.Items.Add(obj);
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

        private void urlTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Stopwatch watch = new Stopwatch();

                string Url = this.urlTextBox.Text;
                watch.Start();

                Dictionary<string, ulong> content = HtmlContent.SetupSingleUrl(Url, false, _JWordSegmentor, wrcList);

                watch.Stop();

                this.messageListBox.Items.Add("ExtractContent: " + watch.ElapsedMilliseconds.ToString());

                this.propertyGrid.SelectedObject = content;
            }
        }
    }
}

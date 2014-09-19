using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Jeelu;
using System.Diagnostics;

namespace GetWebQuickDemo
{
    public partial class Demo : Form
    {
        public Demo()
        {
            InitializeComponent();
            this.SetMessageListbox();
        }

        private void SetMessageListbox()
        {
            StreamReader reader = File.OpenText("url.txt");
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                this.AddText(line);
            }
        }

        public void AddText(string text)
        {
            this.messageListBox.Items.Add(text);
        }

        private void messageListBox_DoubleClick(object sender, EventArgs e)
        {
            this.codeTextBox.Clear();
            object obj = this.messageListBox.SelectedItem;
            string url = obj.ToString();
            this.Text = url;
            WebPage wp = null;
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            Stopwatch swch = new Stopwatch();
            swch.Start();
            /// 核心测试
            wp = Utility.Web.GetHtmlCodeFromUrl(url, 2, 10);
            this.codeTextBox.Text = wp.HtmlCode;
            swch.Stop();
            if (wp.LastBadStatusCode != -1)
            {
                this.Text = url + " | " + wp.LastBadStatusCode.ToString();
            }
            else
            {
                this.Text = url + " | " + swch.ElapsedMilliseconds.ToString() + " | " + wp.Encoding.EncodingName;
            }
            this.Cursor = Cursors.Default;
        }
    }
}

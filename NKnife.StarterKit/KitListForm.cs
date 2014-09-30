using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.StarterKit.Forms;

namespace NKnife.StarterKit
{
    public partial class KitListForm : Form
    {
        readonly LoggingStarterForm _LoggingForm = new LoggingStarterForm();

        public KitListForm()
        {
            InitializeComponent();
            _MultiLanguageLoPanleMenuItem.CheckedChanged += (s, e) =>
            {
                _MultiLanguageLoPanleMenuItem.Text = _MultiLanguageLoPanleMenuItem.Checked ? "英文LogPanel" : "中文LogPanel";
            };
        }

        private void _LogPanelTestMenuItem_Click(object sender, EventArgs e)
        {
            NKnife.Global.Culture = _MultiLanguageLoPanleMenuItem.Checked ? "en-US" : "zh-CN";
            _LoggingForm.MdiParent = this;
            _LoggingForm.FormClosed += LoggingFormClosed;
            _LoggingForm.Show();
        }

        void LoggingFormClosed(object sender, FormClosedEventArgs e)
        {
            var form = (Form) sender;
            form.Hide();
        }

        private void 汉字使用频率ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ChineseCharUseFrequency();
            form.MdiParent = this;
            form.Show();
        }

        private void 图片浏览容器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ImagesPanelDemo();
            form.MdiParent = this;
            form.Show();
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.DockForm.Views;
using NKnife.StarterKit.Forms;

namespace NKnife.StarterKit
{
    public partial class KitListForm : Form
    {
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
            var form = new LoggingStarterForm();
            form.MdiParent = this;
            form.FormClosed += LoggingFormClosed;
            form.Show();
        }

        void LoggingFormClosed(object sender, FormClosedEventArgs e)
        {
            var form = (Form) sender;
            form.Close();
        }

        private void 简易拼音输入法ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new PinyinImeForm();
            form.MdiParent = this;
            form.Show();
        }

        private void 汉字使用频率ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ChineseCharUseFrequency();
            form.MdiParent = this;
            form.Show();
        }

        private void 启动DockFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new Workbench();
            form.Show();
        }

    }
}

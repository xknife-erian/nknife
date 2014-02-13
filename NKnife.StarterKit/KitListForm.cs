using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
            form.Show();
        }
    }
}

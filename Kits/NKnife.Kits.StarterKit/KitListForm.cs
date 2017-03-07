using System;
using System.Windows.Forms;
using Common.Logging;
using NKnife.Kits.StarterKit.Forms;

namespace NKnife.Kits.StarterKit
{
    public partial class KitListForm : Form
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        public KitListForm()
        {
            InitializeComponent();
            _MultiLanguageLoPanleMenuItem.CheckedChanged += (s, e) =>
            {
                _MultiLanguageLoPanleMenuItem.Text = _MultiLanguageLoPanleMenuItem.Checked ? "英文LogPanel" : "中文LogPanel";
            };
            _logger.InfoFormat("{1}-{0}", this.Name, this.GetType().Name);
        }

        private void _LogPanelTestMenuItem_Click(object sender, EventArgs e)
        {
            var loggingForm = new LoggingStarterForm();
            Global.Culture = _MultiLanguageLoPanleMenuItem.Checked ? "en-US" : "zh-CN";
            loggingForm.MdiParent = this;
            loggingForm.FormClosed += LoggingFormClosed;
            loggingForm.Show();
        }

        private void LoggingFormClosed(object sender, FormClosedEventArgs e)
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

        private void 自定义工具栏控件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new UserToolStripForm();
            form.MdiParent = this;
            form.Show();
        }
    }
}
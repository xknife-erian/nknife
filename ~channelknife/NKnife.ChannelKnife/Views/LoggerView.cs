using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NKnife.NLog.WinForm;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.ChannelKnife.Views
{
    public partial class LoggerView : DockContent
    {
        public LoggerView()
        {
            InitializeComponent();
            var logPanel = LoggerListView.Instance;
            logPanel.Dock = DockStyle.Fill;
            logPanel.Location = new Point(0, 0);
            logPanel.TabIndex = 0;
            logPanel.ToolStripVisible = true;
            logPanel.SetDebugMode(true);
            Controls.Add(logPanel);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Show(DockPanel, DockState.DockBottomAutoHide);
            e.Cancel = true;
        }
    }
}

using System;
using System.ComponentModel;
using System.Windows.Forms;
using NKnife.NLog.Controls;
using NKnife.NLog.Properties;

namespace NKnife.NLog
{
    public partial class NLogForm : Form
    {
        public NLogForm()
        {
            InitializeComponent();
            Icon = OwnResources.NLogForm;
            Padding = new Padding(3);
            var logPanel = LogPanel.AppendLogPanelToContainer(this);
            logPanel.HeaderStyle = ColumnHeaderStyle.Clickable;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            int h = Screen.PrimaryScreen.Bounds.Height;
            Top = h - Height - 40;

            int w = Screen.PrimaryScreen.Bounds.Width;
            Left = w - Width - 2;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
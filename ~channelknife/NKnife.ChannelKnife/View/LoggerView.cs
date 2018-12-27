﻿using System.Drawing;
using System.Windows.Forms;
using NKnife.NLog.WinForm;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.ChannelKnife.View
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
    }
}

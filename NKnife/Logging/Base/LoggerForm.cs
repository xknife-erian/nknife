﻿using System;
using System.Drawing;
using System.Windows.Forms;
using NKnife.Ioc;

namespace NKnife.Logging.Base
{
    public partial class LoggerForm : Form
    {
        public LoggerForm()
        {
            InitializeComponent();

            var logPanel = DI.Get<LogPanel.LogPanel>();
            logPanel.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right;
            logPanel.Font = new Font("Tahoma", 8.25F);
            logPanel.HeaderStyle = ColumnHeaderStyle.Clickable;
            logPanel.Location = new Point(12, 12);
            logPanel.Name = "logPanel1";
            logPanel.Size = new Size(652, 485);
            logPanel.TabIndex = 0;
            logPanel.ToolStripVisible = true;
            Controls.Add(logPanel);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            int h = Screen.PrimaryScreen.Bounds.Height;
            Top = h - Height - 40;

            int w = Screen.PrimaryScreen.Bounds.Width;
            Left = w - Width - 5;
        }
    }
}
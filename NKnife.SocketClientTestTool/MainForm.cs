using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.Ioc;
using NKnife.Logging.LogPanel;
using NKnife.Utility;

namespace NKnife.SocketClientTestTool
{
    public partial class MainForm : Form
    {
        private LogPanel _LogViewPanel;

        public MainForm()
        {
            InitializeComponent();
            Icon = Properties.Resources.MainIcon;
            SetupLogControl();
            _SendButton.Enabled = false;
            _CLoseButton.Enabled = false;
        }

        private void SetupLogControl()
        {
            _LogViewPanel = DI.Get<LogPanel>();
            _LogViewPanel.BringToFront();
            _LogViewPanel.Font = new Font("Tahoma", 8.25F);
            _LogViewPanel.Dock = DockStyle.Fill;
            _LogTabPage.Controls.Add(_LogViewPanel);
        }

        private void _ConnButton_Click(object sender, EventArgs e)
        {
        }

        private void _CLoseButton_Click(object sender, EventArgs e)
        {
        }

        private void _SendButton_Click(object sender, EventArgs e)
        {
        }

    }
}

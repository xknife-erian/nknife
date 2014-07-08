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
using NKnife.Net;
using NKnife.Utility;

namespace NKnife.SocketClientTestTool
{
    public partial class MainForm : Form
    {
        private LogPanel _LogViewPanel;

        public MainForm()
        {
            InitializeComponent();
            Icon = NKnife.SocketClientTestTool.Properties.Resources.MainIcon;
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

        QuickSocket _QuickSocket;

        private void _ConnButton_Click(object sender, EventArgs e)
        {
            if (_QuickSocket == null)
            {
                _QuickSocket = new QuickSocket(_IpAddressControl.Text, (int)_PortNumberBox.Value, 15000);
                _SendButton.Enabled = true;
                _ConnButton.Enabled = false;
                _CLoseButton.Enabled = true;
            }
        }

        private void _CLoseButton_Click(object sender, EventArgs e)
        {
            if (_QuickSocket != null)
            {
                _QuickSocket.Close();
                _ConnButton.Enabled = true;
                _SendButton.Enabled = false;
                _CLoseButton.Enabled = false;
                _QuickSocket = null;
            }
        }

        private void _SendButton_Click(object sender, EventArgs e)
        {
            try
            {
                var rec = _QuickSocket.SendTo(__ContentTextbox.Text, true, true);
                var r = new List<byte>();
                foreach (var bytese in rec)
                {
                    r.AddRange(bytese);
                }
                _ReceviedTextBox.Text = Encoding.Default.GetString(r.ToArray());
            }
            catch (Exception)
            {
                
            }
        }


    }
}

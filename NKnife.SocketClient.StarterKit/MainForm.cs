using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using NKnife.Ioc;
using NKnife.Logging.LogPanel;
using NKnife.SocketClient.StarterKit.Base;
using SocketKnife.Interfaces;

namespace NKnife.SocketClient.StarterKit
{
    public partial class MainForm : Form
    {
        private ISocketClient _Socket;

        public MainForm()
        {
            InitializeComponent();
            Icon = Properties.Resources.MainIcon;
            SetupLogControl();
            OnSocketClosed();
        }

        private void SetupLogControl()
        {
            var logPanel = DI.Get<LogPanel>();
            logPanel.BringToFront();
            logPanel.Font = new Font("Tahoma", 8.25F);
            logPanel.Dock = DockStyle.Fill;
            _LogTabPage.Controls.Add(logPanel);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (_Socket != null)
            {
                _Socket.Close();
                Thread.Sleep(200);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception)
            {
                Application.Exit();
            }
            base.OnClosed(e);
        }

        private void OnSocketOpened()
        {
            _RequestToolStrip.Enabled = true;
            _CLoseButton.Enabled = true;
        }

        private void OnSocketClosed()
        {
            _RequestToolStrip.Enabled = false;
            _CLoseButton.Enabled = false;
        }

        private void _ConnButton_Click(object sender, EventArgs e)
        {
            _Socket = new ClientKit();
            _Socket.ConnectTo(_IpAddressControl.Text, (int)_PortNumberBox.Value);
            OnSocketOpened();
        }

        private void _CLoseButton_Click(object sender, EventArgs e)
        {
            _Socket.Close();
            _Socket = null;
            OnSocketClosed();
        }

        private void _SendButton_Click(object sender, EventArgs e)
        {
            _Socket.SendTo(_RequestTextbox.Text);
        }



    }
}

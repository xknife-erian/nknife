using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using NKnife.Ioc;
using NKnife.Logging.LogPanel;
using NKnife.Socket.StarterKit.Base;
using NKnife.Socket.StarterKit.Properties;
using NLog;
using SocketKnife.Interfaces;
using SocketKnife.Interfaces.Sockets;

namespace NKnife.Socket.StarterKit
{
    public partial class MainForm : Form
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        private ISocketClient _Socket;

        public MainForm()
        {
            InitializeComponent();
            Icon = Resources.MainIcon;
            _ProtocolButton.Image = Resources.Protocol;
            _SendButton.Image = Resources.Sender;
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
            _Logger.Info("日志面板安装完成");
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (_Socket != null)
            {
                _Socket.Close();
                Thread.Sleep(100);
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
            _Socket.ReceiveDataParsedEvent += (args, point) =>
            {
                var p = args.Protocol.ToString();
                var handler = new ControlExtension.InvokeHandler(delegate { _ReceviedTextBox.Text = p; });
                _ReceviedTextBox.ThreadSafeInvoke(handler);
            };
            OnSocketOpened();
            _Logger.Info("Socket连接完成:{0}:{1}", _IpAddressControl.Text, _PortNumberBox.Value);
        }

        private void _CLoseButton_Click(object sender, EventArgs e)
        {
            _Socket.Close();
            _Socket = null;
            OnSocketClosed();
            _Logger.Info("Socket关闭");
        }

        private void _SendButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(_RequestTextbox.Text))
                _Socket.SendTo(_RequestTextbox.Text);
        }



    }
}

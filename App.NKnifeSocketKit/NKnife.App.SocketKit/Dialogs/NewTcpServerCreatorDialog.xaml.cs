using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NKnife.IoC;
using NKnife.Utility;
using SocketKnife;
using SocketKnife.Generic;

namespace NKnife.App.SocketKit.Dialogs
{
    /// <summary>
    /// NewTcpServerCreatorDialog.xaml 的交互逻辑
    /// </summary>
    public partial class NewTcpServerCreatorDialog : Window
    {
        public NewTcpServerCreatorDialog()
        {
            InitializeComponent();
            var ips = UtilityNet.GetLocalIpv4();
            foreach (var ipAddress in ips)
            {
                _LocalIpBox.Items.Add(ipAddress);
            }
            if (ips.Any())
            {
                _LocalIpBox.SelectedIndex = 0;
            }
            ServerConfig = DI.Get<KnifeSocketServerConfig>();
        }

        public IPAddress IpAddress { get; private set; }
        public int Port { get; private set; }
        public KnifeSocketServerConfig ServerConfig { get; set; }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            IpAddress = (IPAddress) _LocalIpBox.SelectedItem;
            Port = int.Parse(_PortTextBox.Text);

            ServerConfig.ReceiveBufferSize = int.Parse(_ReceiveBufferSizeTextBox.Text);
            ServerConfig.SendBufferSize = int.Parse(_SendBufferSizeSizeTextBox.Text);
            ServerConfig.MaxBufferSize = int.Parse(_MaxBufferSizeTextBox.Text);
            ServerConfig.MaxConnectCount = int.Parse(_MaxConnectCountTextBox.Text);
            ServerConfig.ReceiveTimeout = int.Parse(_ReceiveTimeoutTextBox.Text);
            ServerConfig.SendTimeout = int.Parse(_SendTimeoutTextBox.Text);

            DialogResult = true;
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

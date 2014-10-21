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
using NKnife.Utility;
using SocketKnife;

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
        }

        public IPAddress IpAddress { get; private set; }
        public int Port { get; private set; }
        public int ReceiveBufferSize { get; set; }
        public int MaxBufferSize { get; set; }
        public int MaxConnectCount { get; set; }
        public int ReceiveTimeout { get; set; }
        public int SendTimeout { get; set; }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            IpAddress = (IPAddress) _LocalIpBox.SelectedItem;
            Port = int.Parse(_PortTextBox.Text);

            ReceiveBufferSize = int.Parse(_ReceiveBufferSizeTextBox.Text);
            MaxBufferSize = int.Parse(_MaxBufferSizeTextBox.Text);
            MaxConnectCount = int.Parse(_MaxConnectCountTextBox.Text);
            ReceiveTimeout = int.Parse(_ReceiveTimeoutTextBox.Text);
            SendTimeout = int.Parse(_SendTimeoutTextBox.Text);

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

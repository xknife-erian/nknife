using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NKnife.Socket;
using SocketKnife;

namespace NKnife.App.SocketKit.Dialogs
{
    /// <summary>
    /// NewTcpServerCreatorDialog.xaml 的交互逻辑
    /// </summary>
    public partial class NewTcpServerCreatorDialog : Window
    {
        private TcpServerKnife _Server;

        public NewTcpServerCreatorDialog()
        {
            InitializeComponent();
            _LocalIpBox.Items.Add("192.168.2.123");
            _LocalIpBox.Items.Add("192.168.2.255");

            _Server = new TcpServerKnife();
            //_Server.SetProtocolFactory(null);//ProtocolFactory);
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

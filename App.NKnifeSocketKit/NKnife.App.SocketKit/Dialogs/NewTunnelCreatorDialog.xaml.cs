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
using NKnife.Adapters;
using NKnife.App.SocketKit.Common;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Protocol;
using NKnife.Protocol.Generic.CommandParsers;
using NKnife.Protocol.Generic.Packers;
using NKnife.Tunnel;
using NKnife.Utility;
using SocketKnife;
using SocketKnife.Generic;
using SocketKnife.Generic.Families;

namespace NKnife.App.SocketKit.Dialogs
{
    /// <summary>
    /// NewTcpServerCreatorDialog.xaml 的交互逻辑
    /// </summary>
    public partial class NewTunnelCreatorDialog : Window
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        public NewTunnelCreatorDialog()
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
            SocketTools = new SocketTools();

            var decoders = UtilityType.FindTypesByDirectory(AppDomain.CurrentDomain.BaseDirectory, typeof(IDatagramDecoder<>), true);
            foreach (var decoder in decoders)
            {
                _DecoderComboBox.Items.Add(decoder);
            }
            _DecoderComboBox.SelectedItem = typeof(FixedTailDecoder);

            var encoders = UtilityType.FindTypesByDirectory(AppDomain.CurrentDomain.BaseDirectory, typeof(IDatagramEncoder<>), true);
            foreach (var encoder in encoders)
            {
                _EncoderComboBox.Items.Add(encoder);
            }
            _EncoderComboBox.SelectedItem = typeof(FixedTailEncoder);

            var packers = UtilityType.FindTypesByDirectory(AppDomain.CurrentDomain.BaseDirectory, typeof(IProtocolPacker<>), true);
            foreach (var packer in packers)
            {
                _PackerComboBox.Items.Add(packer);
            }
            _PackerComboBox.SelectedItem = typeof(TextPlainPacker);

            var unpackers = UtilityType.FindTypesByDirectory(AppDomain.CurrentDomain.BaseDirectory, typeof(IProtocolUnPacker<>), true);
            foreach (var unpacker in unpackers)
            {
                _UnPackerComboBox.Items.Add(unpacker);
            }
            _UnPackerComboBox.SelectedItem = typeof(TextPlainUnPacker);

            var commandParsers = UtilityType.FindTypesByDirectory(AppDomain.CurrentDomain.BaseDirectory, typeof(IProtocolCommandParser<>), true);
            foreach (var commandParser in commandParsers)
            {
                _CommandParserComboBox.Items.Add(commandParser);
            }
            _CommandParserComboBox.SelectedItem = typeof(FirstFieldCommandParser);
        }

        public KnifeSocketServerConfig ServerConfig { get; set; }
        public SocketTools SocketTools { get; set; }
        public string IpAddressLabel
        {
            get { return _IpAddressLabel.Text; }
            set { _IpAddressLabel.Text = value; }
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            ServerConfig.ReceiveBufferSize = int.Parse(_ReceiveBufferSizeTextBox.Text);
            ServerConfig.SendBufferSize = int.Parse(_SendBufferSizeSizeTextBox.Text);
            ServerConfig.MaxBufferSize = int.Parse(_MaxBufferSizeTextBox.Text);
            ServerConfig.MaxConnectCount = int.Parse(_MaxConnectCountTextBox.Text);
            ServerConfig.ReceiveTimeout = int.Parse(_ReceiveTimeoutTextBox.Text);
            ServerConfig.SendTimeout = int.Parse(_SendTimeoutTextBox.Text);

            try
            {
                SocketTools.IpAddress = (IPAddress)_LocalIpBox.SelectedItem;
                SocketTools.Port = int.Parse(_PortTextBox.Text);
                _logger.Info(string.Format("用户设置:{0},{1} <<< {2}",SocketTools.IpAddress, SocketTools.Port, _PortTextBox.Text));

                SocketTools.CommandParser = (Type)_CommandParserComboBox.SelectedItem;
                SocketTools.Decoder = (Type)_DecoderComboBox.SelectedItem;
                SocketTools.Encoder = (Type)_EncoderComboBox.SelectedItem;
                SocketTools.Packer = (Type)_PackerComboBox.SelectedItem;
                SocketTools.UnPacker = (Type)_UnPackerComboBox.SelectedItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (_IsHeartBeat.IsChecked != null) 
                SocketTools.NeedHeartBeat = (bool) _IsHeartBeat.IsChecked;
            else
                SocketTools.NeedHeartBeat = true;
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

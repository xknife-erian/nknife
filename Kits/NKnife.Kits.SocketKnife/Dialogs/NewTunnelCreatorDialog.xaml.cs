using System;
using System.Linq;
using System.Net;
using System.Windows;
using Common.Logging;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Protocol;
using NKnife.Protocol.Generic.TextPlain;
using NKnife.Tunnel;
using NKnife.Utility;
using SocketKnife.Generic;
using SocketKnife.Generic.Families;

namespace NKnife.Kits.SocketKnife.Dialogs
{
    /// <summary>
    /// NewTcpServerCreatorDialog.xaml 的交互逻辑
    /// </summary>
    public partial class NewTunnelCreatorDialog : Window
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

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
            _CommandParserComboBox.SelectedItem = typeof(TextPlainFirstFieldCommandParser);
        }

        public KnifeSocketConfig Config { get; set; }
        public SocketTools SocketTools { get; set; }
        public string IpAddressLabel
        {
            get { return _IpAddressLabel.Text; }
            set { _IpAddressLabel.Text = value; }
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            Config.ReceiveBufferSize = int.Parse(_ReceiveBufferSizeTextBox.Text);
            Config.SendBufferSize = int.Parse(_SendBufferSizeSizeTextBox.Text);
            Config.MaxBufferSize = int.Parse(_MaxBufferSizeTextBox.Text);
            Config.MaxConnectCount = int.Parse(_MaxConnectCountTextBox.Text);
            Config.ReceiveTimeout = int.Parse(_ReceiveTimeoutTextBox.Text);
            Config.SendTimeout = int.Parse(_SendTimeoutTextBox.Text);

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

using System;
using System.Linq;
using System.Net;
using System.Windows;
using Common.Logging;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Protocol.Generic.TextPlain;
using NKnife.Tunnel;
using NKnife.Tunnel.Generic;
using NKnife.Utility;
using SocketKnife.Generic;
using SocketKnife.Generic.Families;
using MessageBox = System.Windows.Forms.MessageBox;

namespace NKnife.Kits.SocketKnife.Dialogs
{
    /// <summary>
    /// NewTcpServerCreatorDialog.xaml 的交互逻辑
    /// </summary>
    public partial class NewTunnelCreatorDialog : Window
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private bool _IsServer;

        public NewTunnelCreatorDialog()
        {
            InitializeComponent();
            var ips = UtilityNet.GetLocalIpv4();
            foreach (var ipAddress in ips)
            {
                _LocalIpBox.Items.Add(ipAddress);
            }
            if (ips.Length > 0)
                _RemoteIpBox.Text = ips[0].ToString();
            if (ips.Any())
            {
                _LocalIpBox.SelectedIndex = 0;
            }
            CustomSetting = new SocketCustomSetting();

            var decoders = UtilityType.FindTypesByDirectory(AppDomain.CurrentDomain.BaseDirectory, typeof(IDatagramDecoder<string>), false);
            foreach (var decoder in decoders)
            {
                _DecoderComboBox.Items.Add(decoder);
            }
            _DecoderComboBox.SelectedItem = typeof (FixedTailDecoder);

            var encoders = UtilityType.FindTypesByDirectory(AppDomain.CurrentDomain.BaseDirectory, typeof(IDatagramEncoder<string>), false);
            foreach (var encoder in encoders)
            {
                _EncoderComboBox.Items.Add(encoder);
            }
            _EncoderComboBox.SelectedItem = typeof (FixedTailEncoder);

            var packers = UtilityType.FindTypesByDirectory(AppDomain.CurrentDomain.BaseDirectory, typeof (IProtocolPacker<>), true);
            foreach (var packer in packers)
            {
                _PackerComboBox.Items.Add(packer);
            }
            _PackerComboBox.SelectedItem = typeof (TextPlainPacker);

            var unpackers = UtilityType.FindTypesByDirectory(AppDomain.CurrentDomain.BaseDirectory, typeof (IProtocolUnPacker<>), true);
            foreach (var unpacker in unpackers)
            {
                _UnPackerComboBox.Items.Add(unpacker);
            }
            _UnPackerComboBox.SelectedItem = typeof (TextPlainUnPacker);

            var commandParsers = UtilityType.FindTypesByDirectory(AppDomain.CurrentDomain.BaseDirectory, typeof (IProtocolCommandParser<>), true);
            foreach (var commandParser in commandParsers)
            {
                _CommandParserComboBox.Items.Add(commandParser);
            }
            _CommandParserComboBox.SelectedItem = typeof (TextPlainFirstFieldCommandParser);
        }

        public KnifeSocketConfig Config { get; set; }
        internal SocketCustomSetting CustomSetting { get; set; }

        public bool IsServer
        {
            get { return _IsServer; }
            set
            {
                _IsServer = value;
                if (!value)
                {
                    _RemoteIpBox.Visibility = Visibility.Visible;
                    _LocalIpBox.Visibility = Visibility.Hidden;
                }
                else
                {
                    _RemoteIpBox.Visibility = Visibility.Hidden;
                    _LocalIpBox.Visibility = Visibility.Visible;
                }
            }
        }

        public string IpAddressLabel
        {
            get { return _IpAddressLabel.Text; }
            set { _IpAddressLabel.Text = value; }
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            var localIpAddress = (IPAddress)_LocalIpBox.SelectedItem;
            int port;
            if (!int.TryParse(_PortTextBox.Text, out port))
            {
                MessageBox.Show("请输入正确的端口号。");
                return;
            }

            //仅服务端需要判断
            if (IsServer)
            {
                if (DI.Get<ServerMap>().ContainsKey(new IPEndPoint(localIpAddress, port)))
                {
                    MessageBox.Show("已启动相同端口号的服务端，请重新选择。");
                    return;
                }
            }

            Config.ReceiveBufferSize = int.Parse(_ReceiveBufferSizeTextBox.Text);
            Config.SendBufferSize = int.Parse(_SendBufferSizeSizeTextBox.Text);
            Config.MaxBufferSize = int.Parse(_MaxBufferSizeTextBox.Text);
            Config.MaxConnectCount = int.Parse(_MaxConnectCountTextBox.Text);
            Config.ReceiveTimeout = int.Parse(_ReceiveTimeoutTextBox.Text);
            Config.SendTimeout = int.Parse(_SendTimeoutTextBox.Text);

            try
            {
                if (IsServer)
                {
                    CustomSetting.IpAddress = localIpAddress;
                }
                else
                {
                    IPAddress clientIpAddress;
                    if (!IPAddress.TryParse(_RemoteIpBox.Text, out clientIpAddress))
                    {
                        MessageBox.Show("请输入正确的Server端IP地址。");
                    }
                    CustomSetting.IpAddress = IPAddress.Parse(_RemoteIpBox.Text);
                }
                CustomSetting.Port = port;
                _logger.Info(string.Format("用户设置:{0},{1} <<< {2}", CustomSetting.IpAddress, CustomSetting.Port, _PortTextBox.Text));

                CustomSetting.CommandParser = (Type) _CommandParserComboBox.SelectedItem;
                CustomSetting.Decoder = (Type) _DecoderComboBox.SelectedItem;
                CustomSetting.Encoder = (Type) _EncoderComboBox.SelectedItem;
                CustomSetting.Packer = (Type) _PackerComboBox.SelectedItem;
                CustomSetting.UnPacker = (Type) _UnPackerComboBox.SelectedItem;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("创建Socket异常", ex.Message);
            }

            if (_IsHeartBeat.IsChecked != null) 
                CustomSetting.NeedHeartBeat = (bool) _IsHeartBeat.IsChecked;
            else
                CustomSetting.NeedHeartBeat = true;
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

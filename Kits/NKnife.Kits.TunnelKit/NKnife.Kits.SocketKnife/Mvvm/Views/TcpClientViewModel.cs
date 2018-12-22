using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using NKnife.Collections;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Kits.SocketKnife.Demo;
using NKnife.Protocol.Generic;
using SocketKnife;
using SocketKnife.Generic;

namespace NKnife.Kits.SocketKnife.Mvvm.Views
{
    public class TcpClientViewModel : ViewModelBase
    {
        private readonly DemoClient _client = new DemoClient();
        private StringProtocol _protocol;
        private DemoClientHandler _handler;
        private readonly ProtocolViewModel _protocolViewModel = Di.Get<ProtocolViewModel>();

        public TcpClientViewModel()
        {
            SocketMessages = new AsyncObservableCollection<SocketMessage>();
        }

        public StringProtocol CurrentProtocol
        {
            get { return _protocol; }
            set
            {
                _protocol = value;
                RaisePropertyChanged(() => CurrentProtocol);
            }
        }

        public AsyncObservableCollection<SocketMessage> SocketMessages { get; set; }

        internal void StartClient(SocketConfig config, SocketCustomSetting customSetting)
        {
            _handler = new DemoClientHandler(_client.GetFamily(),SocketMessages);
            _client.Initialize(config, customSetting, _handler);
            _client.Start();
            //_ProtocolViewModel.AddFamily(_Client.GetFamily());
        }

        public void StopClient()
        {
            _client.Stop();
        }

    }
}

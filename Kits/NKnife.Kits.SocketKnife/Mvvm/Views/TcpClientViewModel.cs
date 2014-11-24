﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Windows.Threading;
using NKnife.Collections;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Kits.SocketKnife.Demo;
using NKnife.Mvvm;
using NKnife.Protocol.Generic;
using SocketKnife;
using SocketKnife.Generic;

namespace NKnife.Kits.SocketKnife.Mvvm.Views
{
    public class TcpClientViewModel : NotificationObject
    {
        private readonly DemoClient _Client = new DemoClient();
        private StringProtocol _Protocol;
        private DemoClientHandler _Handler;
        private readonly ProtocolViewModel _ProtocolViewModel = DI.Get<ProtocolViewModel>();

        public TcpClientViewModel()
        {
            SocketMessages = new AsyncObservableCollection<SocketMessage>();
        }

        public StringProtocol CurrentProtocol
        {
            get { return _Protocol; }
            set
            {
                _Protocol = value;
                RaisePropertyChanged(() => CurrentProtocol);
            }
        }

        public AsyncObservableCollection<SocketMessage> SocketMessages { get; set; }

        internal void StartClient(KnifeSocketConfig config, SocketCustomSetting customSetting)
        {
            _Handler = new DemoClientHandler(SocketMessages);
            _Client.Initialize(config, customSetting, _Handler);
            _Client.Start();
            _ProtocolViewModel.AddFamily(_Client.GetFamily());
        }

        public void StopClient()
        {
            _Client.Stop();
        }

    }
}
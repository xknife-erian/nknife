using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Mvvm;
using NKnife.Protocol.Generic;
using SocketKnife;
using SocketKnife.Generic;

namespace NKnife.Kits.SocketKnife.Mvvm.Views
{
    public class TcpClientViewModel : NotificationObject
    {
        private KnifeSocketClient _Client = DI.Get<KnifeSocketClient>();
        private StringProtocol _Protocol;

        public StringProtocol CurrentProtocol
        {
            get { return _Protocol; }
            set
            {
                _Protocol = value;
                RaisePropertyChanged(() => CurrentProtocol);
            }
        }

        internal void StartClient(KnifeSocketConfig config, SocketCustomSetting customSetting)
        {
        }

        public void StopClient()
        {
        }

    }
}

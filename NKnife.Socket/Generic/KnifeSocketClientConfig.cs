using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeSocketClientConfig : KnifeSocketConfig, ISocketClientConfig
    {
        public KnifeSocketClientConfig()
        {
            _Map.Add("ReconnectTime", 1000*6);
        }

        public int ReconnectTime
        {
            get { return int.Parse(_Map["ReconnectTime"].ToString()); }
            set
            {
                _Map["ReconnectTime"] = value;
                RaisePropertyChanged(() => ReceiveBufferSize);
            }
        }
    }
}

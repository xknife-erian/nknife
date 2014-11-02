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
            _Map.Add("ReconnectTime", 1000 * 6);
            _Map.Add("EnableReconnect", true);
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

        public bool EnableReconnect
        {
            get { return bool.Parse(_Map["EnableReconnect"].ToString()); }
            set
            {
                _Map["EnableReconnect"] = value;
                RaisePropertyChanged(() => ReceiveBufferSize);
            }
        }
    }
}

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
            _Map.Add("ReconnectInterval", 1000 * 6); //默认自动重连间隔6秒
        }

        public int ReconnectInterval
        {
            get { return int.Parse(_Map["ReconnectInterval"].ToString()); }
            set
            {
                _Map["ReconnectInterval"] = value;
            }
        }
    }
}

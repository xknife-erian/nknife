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
            _Map.Add("EnableReconnect", true); //默认自动重连

            //通过EnableDisconnectAfterReceive和EnableDisconnectAfterSend这两个属性，
            //可以控制长连接或短连接，如果两个属性均为false，则为长连接
            _Map.Add("EnableDisconnectAfterReceive",false); //默认接收后不主动断开连接
        }

        public int ReconnectInterval
        {
            get { return int.Parse(_Map["ReconnectInterval"].ToString()); }
            set
            {
                _Map["ReconnectInterval"] = value;
                RaisePropertyChanged(() => ReconnectInterval);
            }
        }

        /// <summary>
        /// 是否连接断开（服务端断开）后自动重连
        /// </summary>
        public bool EnableReconnect
        {
            get { return bool.Parse(_Map["EnableReconnect"].ToString()); }
            set
            {
                _Map["EnableReconnect"] = value;
                RaisePropertyChanged(() => EnableReconnect);
            }
        }
        /// <summary>
        /// 是否接收后自动断开连接（客户端断开）
        /// </summary>
        public bool EnableDisconnectAfterReceive
        {
            get { return bool.Parse(_Map["EnableDisconnectAfterReceive"].ToString()); }
            set
            {
                _Map["EnableDisconnectAfterReceive"] = value;
                RaisePropertyChanged(() => EnableDisconnectAfterReceive);
            }
        }
    }
}

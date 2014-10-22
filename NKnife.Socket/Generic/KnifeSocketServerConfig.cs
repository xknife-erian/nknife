using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeSocketServerConfig : KnifeSocketConfig, ISocketServerConfig
    {
        public KnifeSocketServerConfig()
        {
            //默认值
            _Map.Add("ReceiveBufferSize", 10240);
            _Map.Add("SendBufferSize", 10240);
            _Map.Add("MaxBufferSize", 20480);
            _Map.Add("MaxConnectCount", 64);
            _Map.Add("ReceiveTimeout", 1200);
            _Map.Add("SendTimeout", 1200);
        }

        public void Initialize(int receiveTimeout, int sendTimeout, int maxBufferSize, int maxConnectCount, int receiveBufferSize, int sendBufferSize)
        {
            ReceiveBufferSize = receiveBufferSize;
            SendBufferSize = sendBufferSize;
            MaxBufferSize = maxBufferSize;
            MaxConnectCount = maxConnectCount;
            ReceiveTimeout = receiveTimeout;
            SendTimeout = sendTimeout;
        }

        public virtual int ReceiveBufferSize
        {
            get { return int.Parse(_Map["ReceiveBufferSize"].ToString()); }
            set
            {
                _Map["ReceiveBufferSize"] = value;
                RaisePropertyChanged(() => ReceiveBufferSize);
            }
        }

        public int SendBufferSize
        {
            get { return int.Parse(_Map["SendBufferSize"].ToString()); }
            set
            {
                _Map["SendBufferSize"] = value;
                RaisePropertyChanged(() => SendBufferSize);
            }
        }

        public virtual int MaxBufferSize
        {
            get { return int.Parse(_Map["MaxBufferSize"].ToString()); }
            set
            {
                _Map["MaxBufferSize"] = value;
                RaisePropertyChanged(() => MaxBufferSize);
            }
        }

        public virtual int MaxConnectCount
        {
            get { return int.Parse(_Map["MaxConnectCount"].ToString()); }
            set
            {
                _Map["MaxConnectCount"] = value;
                RaisePropertyChanged(() => MaxConnectCount);
            }
        }

        public virtual int ReceiveTimeout
        {
            get { return int.Parse(_Map["ReceiveTimeout"].ToString()); }
            set
            {
                _Map["ReceiveTimeout"] = value;
                RaisePropertyChanged(() => ReceiveTimeout);
            }
        }

        public virtual int SendTimeout
        {
            get { return int.Parse(_Map["SendTimeout"].ToString()); }
            set
            {
                _Map["SendTimeout"] = value;
                RaisePropertyChanged(() => SendTimeout);
            }
        }
    }
}
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeSocketServerConfig : KnifeSocketConfig, ISocketServerConfig
    {
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
            get { return int.Parse(_Map["ReadBufferSize"].ToString()); }
            set
            {
                _Map["ReadBufferSize"] = value;
                RaisePropertyChanged(() => ReceiveBufferSize);
            }
        }

        public int SendBufferSize
        {
            get { return int.Parse(_Map["SendBufferSize"].ToString()); }
            set
            {
                _Map["SendBufferSize"] = value;
                RaisePropertyChanged(() => ReceiveBufferSize);
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
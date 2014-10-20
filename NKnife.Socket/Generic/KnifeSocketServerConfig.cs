using System.Collections.Generic;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeSocketServerConfig : Dictionary<string, object>, ISocketConfig
    {
        public void Initialize(int receiveTimeout, int sendTimeout, int maxBufferSize, int maxConnectCount, int readBufferSize)
        {
            ReadBufferSize = readBufferSize;
            MaxBufferSize = maxBufferSize;
            MaxConnectCount = maxConnectCount;
            ReceiveTimeout = receiveTimeout;
            SendTimeout = sendTimeout;
        }

        public virtual int ReadBufferSize { get; set; }
        public virtual int MaxBufferSize { get; set; }
        public virtual int MaxConnectCount { get; set; }
        public virtual int ReceiveTimeout { get; set; }
        public virtual int SendTimeout { get; set; }
    }
}

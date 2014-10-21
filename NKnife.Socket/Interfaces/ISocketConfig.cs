using NKnife.Tunnel;

namespace SocketKnife.Interfaces
{
    public interface ISocketServerConfig : ITunnelConfig
    {
        void Initialize(int receiveTimeout, int sendTimeout, int maxBufferSize, int maxConnectCount, int receiveBufferSize, int sendBufferSize);

        int ReceiveBufferSize { get; set; }
        int SendBufferSize { get; set; }

        /// <summary>
        ///     ���հ���С
        /// </summary>
        int MaxBufferSize { get; set; }

        /// <summary>
        ///     ����û�������
        /// </summary>
        int MaxConnectCount { get; set; }

        /// <summary>
        ///     SOCKET �� ReceiveTimeout����
        /// </summary>
        int ReceiveTimeout { get; set; }

        /// <summary>
        ///     SOCKET �� SendTimeout
        /// </summary>
        int SendTimeout { get; set; }
    }
}
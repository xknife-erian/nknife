using NKnife.Tunnel;

namespace SocketKnife.Interfaces
{
    public interface ISocketServerConfig : ITunnelConfig
    {
        void Initialize(int receiveTimeout, int sendTimeout, int maxBufferSize, int maxConnectCount, int readBufferSize);

        int ReadBufferSize { get; set; }

        /// <summary>
        ///     接收包大小
        /// </summary>
        int MaxBufferSize { get; set; }

        /// <summary>
        ///     最大用户连接数
        /// </summary>
        int MaxConnectCount { get; set; }

        /// <summary>
        ///     SOCKET 的 ReceiveTimeout属性
        /// </summary>
        int ReceiveTimeout { get; set; }

        /// <summary>
        ///     SOCKET 的 SendTimeout
        /// </summary>
        int SendTimeout { get; set; }
    }
}
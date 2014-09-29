namespace SocketKnife.Interfaces
{
    public interface ISocketConfig
    {
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
        ///     是否关闭SOCKET Delay算法
        /// </summary>
        bool NoDelay { get; set; }

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
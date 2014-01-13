namespace Gean.Net.Interfaces
{
    /// <summary>
    /// Socket的工作模式
    /// </summary>
    public enum SocketMode
    {
        /// <summary>
        /// 同步。对话模式，互相交易次数不定，则客户端负责断开连接
        /// </summary>
        Talk,
        /// <summary>
        /// 异步。长连接。
        /// </summary>
        AsyncKeepAlive,
    }

    /// <summary>
    /// 网络连接状态
    /// </summary>
    public enum ConnectionStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal,
        /// <summary>
        /// 断开
        /// </summary>
        Break
    }
}

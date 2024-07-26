using System.Net;

namespace NKnife.NLog.Target.Socket.WebSocket
{
    /// <summary>
    /// 用户信息
    /// </summary>
    internal class UserToken
    {
        /// <summary>
        /// 连接的Socket对象
        /// </summary>
        public System.Net.Sockets.Socket? ConnectSocket { get; set; }

        /// <summary>
        /// websocket对象
        /// </summary>
        public System.Net.WebSockets.WebSocket? WebSocket { get; set; }

        /// <summary>
        /// 连接的时间
        /// </summary>
        public DateTime ConnectTime { get; set; }

        /// <summary>
        /// 远程地址
        /// </summary>
        public EndPoint? RemoteAddress { get; set; }

        /// <summary>
        /// 客户端IP地址
        /// </summary>
        public IPAddress? IpAddress { get; set; }

        /// <summary>
        /// 是否是 websocket（握手成功）
        /// </summary>
        public bool IsWebSocket { get; set; }
    }
}

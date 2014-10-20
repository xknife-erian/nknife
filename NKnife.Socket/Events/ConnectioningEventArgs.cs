using System;
using System.Net;

namespace SocketKnife.Events
{
    /// <summary>
    ///     当Socket即将启动连接包含事件数据的类
    /// </summary>
    public class ConnectioningEventArgs : EventArgs
    {
        public ConnectioningEventArgs(IPEndPoint serverInfo)
        {
            ServerInfo = serverInfo;
        }

        public IPEndPoint ServerInfo { get; set; }
    }
}
using System;
using System.Net;

namespace SocketKnife.Events
{
    /// <summary>
    ///     ��Socket�����������Ӱ����¼����ݵ���
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
using System;
using System.Net;

namespace NKnife.Tunnel.Events
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
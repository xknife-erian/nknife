using System;
using System.Net;

namespace SocketKnife.Events
{
    /// <summary>
    ///     ��Socket�����������Ӱ����¼����ݵ���
    /// </summary>
    public class ConnectioningEventArgs : EventArgs
    {
        public ConnectioningEventArgs(EndPoint serverInfo)
        {
            ServerInfo = serverInfo;
        }

        public EndPoint ServerInfo { get; set; }
    }
}
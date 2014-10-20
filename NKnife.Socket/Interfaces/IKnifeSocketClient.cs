using System.Net;
using System.Net.Sockets;
using NKnife.Tunnel;
using SocketKnife.Common;

namespace SocketKnife.Interfaces
{
    public interface IKnifeSocketClient : ITunnel<EndPoint, Socket, string>
    {
        /// <summary>
        ///     是否连接成功
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is connection suceess; otherwise, <c>false</c>.
        /// </value>
        bool IsConnectionSuceess { get; }

        /// <summary>
        ///     远端服务器状态
        /// </summary>
        /// <value><c>true</c> if [server status]; otherwise, <c>false</c>.</value>
        bool ServerStatus { get; }
    }
}
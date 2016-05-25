using System;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel
{
    /// <summary>
    /// 这是一个描述数据链路的接口，他可以是一个串口，一个Socket端口，一个并口等等
    /// </summary>
    public interface IDataConnector
    {
        /// <summary>
        /// 关闭数据链路
        /// </summary>
        /// <returns></returns>
        bool Stop();

        /// <summary>
        /// 打开数据链路
        /// </summary>
        /// <returns></returns>
        bool Start();

        /// <summary>
        /// 当数据链路中建立一个链接时
        /// </summary>
        event EventHandler<SessionEventArgs> SessionBuilt;
        /// <summary>
        /// 当数据链路中一个链接被断开时
        /// </summary>
        event EventHandler<SessionEventArgs> SessionBroken;
        /// <summary>
        /// 当数据链路中有数据到达时
        /// </summary>
        event EventHandler<SessionEventArgs> DataReceived;
        /// <summary>
        /// 当数据发送完成时
        /// </summary>
        event EventHandler<SessionEventArgs> DataSent;

        void Send(long id, byte[] data);

        void SendAll(byte[] data);

        void KillSession(long id);
    }
}
using System;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel
{
    public interface ITunnelFilter
    {
        #region 针对DataConnector的事件处理

        /// <summary>
        ///     Tunnel接收到来自dataconnector数据时，遍历调用Filter链表中所有Filter的该方法，
        ///     Filter在此方法中处理来自dataconnector的数据
        /// </summary>
        /// <param name="session">dataconnector会话数据包</param>
        /// <returns>
        ///     返回true，允许Filter链表中下一个Filter继续执行接收数据处理，
        ///     返回false则Tunnel中断数据接收处理动作
        /// </returns>
        bool PrcoessReceiveData(ITunnelSession session);

        /// <summary>
        ///     Tunnel接收到来自dataconnector的会话中断消息时，通过该方法通知Filter链表中所有Filter会话中断，
        ///     Filter根据自身逻辑执行中断处理
        /// </summary>
        /// <param name="id">dataconnector会话id</param>
        void ProcessSessionBroken(long id);

        /// <summary>
        ///     Tunnel接收到来自dataconnector的会话建立消息时，通过该方法通知Filter链表中所有Filter会话建立，
        ///     Filter根据自身逻辑执行会话建立处理
        /// </summary>
        /// <param name="id"></param>
        void ProcessSessionBuilt(long id);

        #endregion

        #region 针对Filter的事件处理和方法

        /// <summary>
        ///     Filter的使用者（包括Tunnel）
        ///     可以通过该方法由Filter对要发送的数据进行处理（注意,Filter是不能直接调用dataconnector发送数据的）,
        ///     Filter处理完数据后，可以通过OnSendToSession事件将处理后的数据抛给Tunnel,Tunnel会从Filter链表中
        ///     找到当前Filter前一个Filter继续执行本方法，如果当前Filter已经是链表中的第一个Filter，则Tunnel会调用connctor的数据发送方法，
        ///     将数据发出，从而实现任何一个Filter要发出在数据也可以经过其Filter链表之前的Filter过滤或拦截（Filter处理后觉得不需要继续发送，
        ///     OnSendToSession事件即可）
        /// </summary>
        /// <param name="session"></param>
        void ProcessSendToSession(ITunnelSession session);

        /// <summary>
        ///     Filter的使用者（包括Tunnel）
        ///     可以通过该方法由Filter对要发送的数据进行处理（注意,Filter是不能直接调用dataconnector发送数据的）,
        ///     Filter处理完数据后，可以通过OnSendToAll事件将处理后的数据抛给Tunnel,Tunnel会从Filter链表中
        ///     找到当前Filter前一个Filter继续执行本方法，如果当前Filter已经是链表中的第一个Filter，则Tunnel会调用connctor的数据发送方法，
        ///     将数据发出，从而实现任何一个Filter要发出在数据也可以经过其Filter链表之前的Filter过滤或拦截（Filter处理后觉得不需要继续发送，
        ///     OnSendToAll事件即可）
        /// </summary>
        /// <param name="data"></param>
        void ProcessSendToAll(byte[] data);

        /// <summary>
        ///     Filter通过该消息，通知Tunnel，有数据想通过connctor发送给指定会话，Tunnel会从Filter链表中
        ///     找到当前Filter前一个Filter继续执行ProcessSendToSession方法，如果当前Filter已经是链表中的第一个Filter，
        ///     则Tunnel会调用connctor的数据发送方法，
        /// </summary>
        event EventHandler<SessionEventArgs> SendToSession;

        /// <summary>
        ///     Filter通过该消息，通知Tunnel，有数据想通过connctor发送给所有会话，Tunnel会从Filter链表中
        ///     找到当前Filter前一个Filter继续执行ProcessSendToAll方法，如果当前Filter已经是链表中的第一个Filter，
        ///     则Tunnel会调用connctor的数据发送方法，
        /// </summary>
        event EventHandler<SessionEventArgs> SendToAll;

        /// <summary>
        ///     Filter自身想主动中断会话，则通过该事件通知Tunnel,Tunnel收到该事件后会主动断开会话
        /// </summary>
        event EventHandler<SessionEventArgs> KillSession;

        #endregion
    }
}
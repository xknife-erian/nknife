using System;
using System.Net;
using System.Net.Sockets;
using NKnife.Tunnel.Events;
using SocketKnife.Events;

namespace SocketKnife
{

    /// <summary>
    ///     侦听到新客户端连接事件
    /// </summary>
    public delegate bool ListenToClientEventHandler(SocketAsyncEventArgs e);

    /// <summary>
    ///     数据异步接收到后事件
    /// </summary>
    public delegate void SocketAsyncDataComeInEventHandler(byte[] data, EndPoint endPoint);

    /// <summary>
    ///     Socket连接状态发生改变
    /// </summary>
    public delegate void SocketStatusChangedEventHandler(Object sender, SocketStatusChangedEventArgs e);

    /// <summary>
    ///     Socket即将连接事件
    /// </summary>
    public delegate void ConnectioningEventHandler(ConnectioningEventArgs e);

    /// <summary>
    ///     当断线后Socket重新连接后的事件(根据IsConnSucceed判断是否连接成功)
    /// </summary>
    public delegate void ConnectionedWhileBreakEventHandler(ConnectionedEventArgs e);

    /// <summary>
    ///     Socket连接后事件(根据IsConnSucceed判断是否连接成功)
    /// </summary>
    public delegate void ConnectionedEventHandler(ConnectionedEventArgs e);

    /// <summary>
    ///     接收到的数据解析完成后事件
    /// </summary>
    public delegate void ReceiveDataParsedEventHandler(ReceiveDataParsedEventArgs<string> e, EndPoint endPoint);

}
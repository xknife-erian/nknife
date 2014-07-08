using System;
using System.Net;
using System.Net.Sockets;
using SocketKnife.Interfaces;

namespace SocketKnife.Common
{

    #region 服务器侦听到新客户端连接事件

    /// <summary>
    ///     侦听到新客户端连接事件
    /// </summary>
    public delegate bool ListenToClientEventHandler(SocketAsyncEventArgs e);

    #endregion

    #region 数据异步接收到后事件

    /// <summary>
    ///     数据异步接收到后事件
    /// </summary>
    public delegate void SocketAsyncDataComeInEventHandler(byte[] data, EndPoint endPoint);

    /// <summary>
    ///     当有数据异步接收到后事件发生时包含事件数据的类。
    /// </summary>
    public class SocketAsyncDataComeInEventArgs : EventArgs
    {
        public SocketAsyncDataComeInEventArgs(byte[] data)
        {
            AsyncData = data;
        }

        public byte[] AsyncData { get; private set; }
    }

    #endregion

    #region 连接出错或断开触发事件

    /// <summary>
    ///     连接出错或断开触发事件
    /// </summary>
    public delegate void ConnectionBreakEventHandler(ConnectionBreakEventArgs e);

    /// <summary>
    ///     当出错或断开触发事件发生时包含事件数据的类，继承自 SocketAsyncEventArgs
    /// </summary>
    public class ConnectionBreakEventArgs : SocketAsyncEventArgs
    {
        public ConnectionBreakEventArgs(string message)
        {
            EventMessage = message;
        }

        public string EventMessage { get; private set; }
    }

    #endregion

    #region Socket连接状态发生改变

    /// <summary>
    ///     Socket连接状态发生改变
    /// </summary>
    public delegate void SocketStatusChangedEventHandler(Object sender, SocketStatusChangedEventArgs e);

    /// <summary>
    ///     Socket连接状态发生改变时包含数据的类
    /// </summary>
    public class SocketStatusChangedEventArgs : EventArgs
    {
        public SocketStatusChangedEventArgs(ConnectionStatus status)
        {
            Status = status;
        }

        public ConnectionStatus Status { get; private set; }
    }

    #endregion

    #region 连接相关事件：ConnectioningEventHandler,ConnectionedEventHandler,ConnectionedWhileBreakEventHandler

    /// <summary>
    ///     Socket即将连接事件
    /// </summary>
    public delegate void ConnectioningEventHandler(ConnectioningEventArgs e);

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

    /// <summary>
    ///     当断线后Socket重新连接后的事件(根据IsConnSucceed判断是否连接成功)
    /// </summary>
    public delegate void ConnectionedWhileBreakEventHandler(ConnectionedEventArgs e);

    /// <summary>
    ///     Socket连接后事件(根据IsConnSucceed判断是否连接成功)
    /// </summary>
    public delegate void ConnectionedEventHandler(ConnectionedEventArgs e);

    /// <summary>
    ///     当Socket连接后事件发生时包含事件数据的类(根据IsConnSucceed判断是否连接成功)
    /// </summary>
    public class ConnectionedEventArgs : EventArgs
    {
        public ConnectionedEventArgs(bool isConnSucceed, string message)
        {
            IsConnSucceed = isConnSucceed;
            Message = message;
        }

        public string Message { get; private set; }
        public bool IsConnSucceed { get; set; }
    }

    #endregion

    #region 接收到的数据解析完成后事件

    /// <summary>
    ///     接收到的数据解析完成后事件
    /// </summary>
    public delegate void ReceiveDataParsedEventHandler(ReceiveDataParsedEventArgs e, EndPoint endPoint);

    /// <summary>
    ///     当接收到的数据解析完成后事件发生时包含事件数据的类
    /// </summary>
    public class ReceiveDataParsedEventArgs : EventArgs
    {
        public ReceiveDataParsedEventArgs(IProtocol protocol)
        {
            Protocol = protocol;
        }

        public IProtocol Protocol { get; private set; }
    }

    #endregion
}
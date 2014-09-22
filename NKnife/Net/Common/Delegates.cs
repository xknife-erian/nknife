using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Gean.Net.Common;
using NKnife.Net.Protocol;

namespace Gean.Net.Interfaces
{

    #region 服务器侦听到新客户端连接事件

    /// <summary>
    /// 侦听到新客户端连接事件
    /// </summary>
    public delegate bool ListenToClientEventHandler(SocketAsyncEventArgs e);

    #endregion

    #region 数据异步接收到后事件

    /// <summary>
    /// 数据异步接收到后事件
    /// </summary>
    public delegate void SocketAsyncDataComeInEventHandler(byte[] data, EndPoint endPoint);

    /// <summary>
    /// 当有数据异步接收到后事件发生时包含事件数据的类。
    /// </summary>
    public class SocketAsyncDataComeInEventArgs : EventArgs
    {
        public byte[] AsyncData { get; private set; }
        public SocketAsyncDataComeInEventArgs(byte[] data)
        {
            this.AsyncData = data;
        }
    }

    #endregion

    #region 连接出错或断开触发事件

    /// <summary>
    /// 连接出错或断开触发事件
    /// </summary>
    public delegate void ConnectionBreakEventHandler(ConnectionBreakEventArgs e);

    /// <summary>
    /// 当出错或断开触发事件发生时包含事件数据的类，继承自 SocketAsyncEventArgs
    /// </summary>
    public class ConnectionBreakEventArgs : SocketAsyncEventArgs
    {
        public string EventMessage { get; private set; }
        public ConnectionBreakEventArgs(string message)
        {
            this.EventMessage = message;
        }
    }

    #endregion

    #region Socket连接状态发生改变

    /// <summary>
    /// Socket连接状态发生改变
    /// </summary>
    public delegate void SocketStatusChangedEventHandler(Object sender, SocketStatusChangedEventArgs e);

    /// <summary>
    /// Socket连接状态发生改变时包含数据的类
    /// </summary>
    public class SocketStatusChangedEventArgs : EventArgs
    {
        public ConnectionStatus Status { get; private set; }
        public SocketStatusChangedEventArgs(ConnectionStatus status)
        {
            this.Status = status;
        }
    }
    
    #endregion

    #region 连接相关事件：ConnectioningEventHandler,ConnectionedEventHandler,ConnectionedWhileBreakEventHandler

    /// <summary>
    /// Socket即将连接事件
    /// </summary>
    public delegate void ConnectioningEventHandler(ConnectioningEventArgs e);

    /// <summary>
    /// 当Socket即将启动连接包含事件数据的类
    /// </summary>
    public class ConnectioningEventArgs : EventArgs
    {
        public IPEndPoint ServerInfo { get; set; }
        public ConnectioningEventArgs(IPEndPoint serverInfo)
        {
            this.ServerInfo = serverInfo;
        }
    }

    /// <summary>当断线后Socket重新连接后的事件(根据IsConnSucceed判断是否连接成功)
    /// </summary>
    public delegate void ConnectionedWhileBreakEventHandler(ConnectionedEventArgs e);

    /// <summary>Socket连接后事件(根据IsConnSucceed判断是否连接成功)
    /// </summary>
    public delegate void ConnectionedEventHandler(ConnectionedEventArgs e);

    /// <summary>当Socket连接后事件发生时包含事件数据的类(根据IsConnSucceed判断是否连接成功)
    /// </summary>
    public class ConnectionedEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public bool IsConnSucceed { get; set; }
        public ConnectionedEventArgs(bool isConnSucceed, string message)
        {
            this.IsConnSucceed = isConnSucceed;
            this.Message = message;
        }
    }

    #endregion

    #region 接收到的数据解析完成后事件

    /// <summary>
    /// 接收到的数据解析完成后事件
    /// </summary>
    public delegate void ReceiveDataParsedEventHandler(ReceiveDataParsedEventArgs e, EndPoint endPoint);

    /// <summary>
    /// 当接收到的数据解析完成后事件发生时包含事件数据的类
    /// </summary>
    public class ReceiveDataParsedEventArgs : EventArgs
    {
        public IProtocol Protocol { get; private set; }
        public ReceiveDataParsedEventArgs(IProtocol protocol)
        {
            this.Protocol = protocol;
        }
    }
        
    #endregion
}

﻿using System.Net;
using System.Net.Sockets;
using SocketKnife.Common;
using SocketKnife.Protocol.Interfaces;

namespace SocketKnife.Interfaces
{
    public interface IFilter
    {
        IDatagramCommandParser CommandParser { get; }
        IDatagramDecoder Decoder { get; }
        IDatagramEncoder Encoder { get; }

        #region 事件

        /// <summary>
        ///     数据异步接收到后事件,得到的数据是Byte数组,一般情况下没有必要使用该事件,使用 ReceiveDataParsedEvent 会比较方便。
        /// </summary>
        event SocketAsyncDataComeInEventHandler DataComeInEvent;

        /// <summary>
        ///     服务器侦听到新客户端连接事件
        /// </summary>
        event ListenToClientEventHandler ListenToClient;

        /// <summary>
        ///     连接出错或断开触发事件
        /// </summary>
        event ConnectionBreakEventHandler ConnectionBreak;

        /// <summary>
        ///     接收到的数据解析完成后发生的事件
        /// </summary>
        event ReceiveDataParsedEventHandler ReceiveDataParsedEvent;

        #endregion

    }
}
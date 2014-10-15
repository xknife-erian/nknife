﻿using System;
using System.Net;
using System.Net.Sockets;
using SocketKnife.Common;

namespace SocketKnife.Interfaces
{
    public interface ISocketServerFilter
    {
        bool ContinueNextFilter { get; }

        void PrcoessReceiveData(ISocketSession socket, byte[] data);

        void Bind(Func<IProtocolFamily> familyGetter, Func<IProtocolHandler> handlerGetter, Func<ISocketSessionMap> mapGetter);

        #region 事件

        /// <summary>
        ///     数据异步接收到后事件,得到的数据是Byte数组
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

        #endregion

    }
}
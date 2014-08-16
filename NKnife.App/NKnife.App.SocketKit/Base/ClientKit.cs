﻿using NKnife.Ioc;
using SocketKnife;
using SocketKnife.Common;
using SocketKnife.Interfaces;
using SocketKnife.Interfaces.Sockets;
using SocketKnife.Protocol;

namespace NKnife.Socket.StarterKit.Base
{
    public class ClientKit : LightClient
    {
        public ClientKit()
            : base(SocketMode.Talk, "Socket-Client-StarterKit")
        {
        }

        /// <summary>是否采用抽象基类中的重连机制(较简单:在有动作触发时，发现连接断开时，按频率尝试重连服务器)
        /// </summary>
        public override bool IsCustomTryConnectionMode
        {
            get { return false; }
        }

        public override ISocketClientSetting Option
        {
            get { return DI.Get<SocketOption>(); }
        }

    }
}
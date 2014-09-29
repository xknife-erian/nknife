﻿using System;
using NKnife.Socket.Interfaces;

namespace SocketKnife.Interfaces
{
    public interface ISessionHandler
    {
        void Created(ISocketSession ioSession);

        void Opened(ISocketSession ioSession);

        void Closed(ISocketSession ioSession);

        void Idle(ISocketSession ioSession);//, IdleStatus idleStatus);

        void ExceptionCaught(ISocketSession ioSession, Exception throwable);

        void MessageReceived(ISocketSession ioSession, Object o);

        void MessageSent(ISocketSession ioSession, Object o);
    }
}
﻿using System;
using System.Net.Sockets;
using NKnife.Tunnel.Common;

namespace SocketKnife.Generic
{
    /// <summary>
    ///     仅用于Socket协议
    /// </summary>
    public class KnifeSocketSession : TunnelSession
    {
        public const int RECEIVE_BUFFER_SIZE = 16*1024; // 16 K

        public KnifeSocketSession()
        {
            ResetBuffer();
        }

        public Socket AcceptSocket { get; set; }
        public SessionState State { get; set; }
        public DisconnectType DisconnectType { get; set; }
        public byte[] ReceiveBuffer { get; set; }
        public DateTime LastSessionTime { get; set; }

        public void ResetBuffer()
        {
            ReceiveBuffer = new byte[RECEIVE_BUFFER_SIZE];
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode*397) ^ (Id.GetHashCode());
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((KnifeSocketSession) obj);
        }
    }
}
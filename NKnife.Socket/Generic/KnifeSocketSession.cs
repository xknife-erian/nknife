using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NKnife.Tunnel;
using NKnife.Tunnel.Common;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    /// <summary>
    /// 除了EndPoint, byte[]之外，多了Socket，只能用于Socket协议
    /// </summary>
    public class KnifeSocketSession : KnifeTunnelSession<EndPoint>
    {
        public int ReceiveBufferSize = 16 * 1024;  // 16 K
        public Socket AcceptSocket { get; set; }
        public SessionState State { get; set; }
        public DisconnectType DisconnectType { get; set; }
        public byte[] ReceiveBuffer { get; set; }
        public DateTime LastSessionTime { get; set; }

        public KnifeSocketSession()
        {
            ResetBuffer();
        }

        public void ResetBuffer()
        {
            ReceiveBuffer = new byte[ReceiveBufferSize];
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ (Id != null ? Id.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((KnifeSocketSession) obj);
        }





    }
}

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
    public class KnifeSocketSession : KnifeTunnelSession
    {
        public Socket AcceptSocket { get; set; }

        protected bool Equals(KnifeSocketSession other)
        {
            return Id == other.Id && Equals(Id, other.Id);
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

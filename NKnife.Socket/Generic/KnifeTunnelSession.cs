using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using NKnife.Tunnel;

namespace SocketKnife.Generic
{
    /// <summary>
    /// 有EndPoint,有byte[]，可用于socket, http等网络协议
    /// </summary>
    public class KnifeTunnelSession : ITunnelSession<byte[], EndPoint>
    {
        public EndPoint Id { get; set; }
        public byte[] Data { get; set; }

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
            return Equals((KnifeSocketSession)obj);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NKnife.Tunnel;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeSocketSession : ITunnelSession<EndPoint, Socket>
    {
        public KnifeSocketSession()
        {
            Id = DateTime.Now.Ticks;
        }

        public long Id { get; private set; }
        public EndPoint Source { get; set; }
        public Socket Connector { get; set; }

        public bool WaitingForReply { get; set; }

        protected bool Equals(KnifeSocketSession other)
        {
            return Id == other.Id && Equals(Source, other.Source) && WaitingForReply.Equals(other.WaitingForReply);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ (Source != null ? Source.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Connector != null ? Connector.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ WaitingForReply.GetHashCode();
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

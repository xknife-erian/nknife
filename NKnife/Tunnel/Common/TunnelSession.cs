﻿namespace NKnife.Tunnel.Common
{
    /// <summary>
    ///     数据类型为byte[]，
    ///     如果SessionId的类型可以为EndPoint，可用于socket, http等网络协议，
    ///     如果SessionId的类型为int,可用于serialport等串口协议,用int串口号做标记
    /// </summary>
    public class TunnelSession : ITunnelSession
    {
        public long Id { get; set; }
        public byte[] Data { get; set; }
        public object Tag { get; set; }

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
            return Equals(obj);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using NKnife.Converts;
using NKnife.IoC;
using NKnife.Protocol;
using NKnife.Protocol.Generic;

namespace MonitorKnife.Tunnels.Common
{
    /// <summary>
    ///     面向Care制定的协议的封装
    /// </summary>
    public class CareSaying : IProtocol<byte[]>
    {
        private string _Scpi = string.Empty;
        private byte[] _ScpiBytes = null;

        public CareSaying()
        {
            ((IProtocol<byte[]>) this).Command = new byte[2];
            ((IProtocol<byte[]>) this).CommandParam = new byte[1];
        }

        #region

        /// <summary>
        ///     主命令字
        /// </summary>
        public byte MainCommand
        {
            get { return ((IProtocol<byte[]>) this).Command[0]; }
            set { ((IProtocol<byte[]>) this).Command[0] = value; }
        }

        /// <summary>
        ///     子命令
        /// </summary>
        public byte SubCommand
        {
            get { return ((IProtocol<byte[]>)this).Command[1]; }
            set { ((IProtocol<byte[]>)this).Command[1] = value; }
        }

        /// <summary>
        ///     GPIB地址
        /// </summary>
        public short GpibAddress
        {
            get { return UtilityConvert.ConvertTo<short>(((IProtocol<byte[]>)this).CommandParam[0]); }
            set { ((IProtocol<byte[]>)this).CommandParam[0] = UtilityConvert.ConvertTo<byte>(value); }
        }

        /// <summary>
        ///     内容的长度
        /// </summary>
        public short Length
        {
            get { return (short) _ScpiBytes.Length; }
        }

        /// <summary>
        ///     内容
        /// </summary>
        public string Content
        {
            get
            {
                return _Scpi;
            }
            set
            {
                _ScpiBytes = Encoding.ASCII.GetBytes(value);
                _Scpi = value;
            }
        }

        #endregion

        #region IProtocol Members

        private readonly Dictionary<string, byte[]> _Infomations = null;

        /// <summary>协议族名称
        /// </summary>
        /// <value>The family.</value>
        public string Family { get; set; }

        byte[] IProtocol<byte[]>.Command { get; set; }

        byte[] IProtocol<byte[]>.CommandParam { get; set; }

        Dictionary<string, byte[]> IProtocol<byte[]>.Infomations
        {
            get { return _Infomations; }
        }

        List<object> IProtocol<byte[]>.Tags { get; set; }

        public CareSaying NewInstance()
        {
            var protocol = (IProtocol<byte[]>)(new CareSaying());
            protocol.Family = Family;
            protocol.Command = ((IProtocol<byte[]>) this).Command;
            return (CareSaying) protocol;
        }

        IProtocol<byte[]> IProtocol<byte[]>.NewInstance()
        {
            return NewInstance();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BytesProtocol)obj);
        }

        protected bool Equals(BytesProtocol other)
        {
            return string.Equals(Family, other.Family) &&
                   Equals(((IProtocol<byte[]>) this).Command, other.Command) &&
                   Equals(((IProtocol<byte[]>) this).CommandParam, other.CommandParam) &&
                   Equals(((IProtocol<byte[]>) this).Tags, other.Tags) &&
                   Equals(((IProtocol<byte[]>) this).Infomations, other.Infomations);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (((IProtocol<byte[]>)this).Command != null ? ((IProtocol<byte[]>)this).Command.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (((IProtocol<byte[]>)this).CommandParam != null ? ((IProtocol<byte[]>)this).CommandParam.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (((IProtocol<byte[]>)this).Tags != null ? ((IProtocol<byte[]>)this).Tags.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (((IProtocol<byte[]>)this).Infomations != null ? ((IProtocol<byte[]>)this).Infomations.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Family != null ? Family.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}
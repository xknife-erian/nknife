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
    public class CareSaying : BytesProtocol
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
                if (string.IsNullOrWhiteSpace(_Scpi) && _ScpiBytes != null && _ScpiBytes.Length > 0)
                    _Scpi = Encoding.ASCII.GetString(_ScpiBytes);
                return _Scpi;
            }
            set
            {
                _ScpiBytes = Encoding.ASCII.GetBytes(value);
                _Scpi = value;
            }
        }

        #endregion
    }
}
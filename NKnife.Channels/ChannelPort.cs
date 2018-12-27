using System;
using System.Net;
using System.Text;
using NKnife.Channels.Enums;

namespace NKnife.Channels
{
    /// <summary>
    /// 描述一个数据端口，一般是只能打开一次的独占数据端口。比如串口，TCPIP端口等。
    /// </summary>
    public class ChannelPort
    {
        private readonly int[] _serialPort = {-1, 115200};
        private string _id;
        private IPEndPoint _ipEndPoint;
        private string[] _serialPortInfo;

        public ChannelType ChannelType { get; private set; }

        /// <summary>
        /// 获取串口信息
        /// </summary>
        /// <returns>一般由2个值构成，第1个值是串口，第2个值是该串口的波特率</returns>
        public int[] GetSerialPortInfo()
        {
            if (_serialPort[0] == -1)
            {
                string port = _serialPortInfo[0].ToUpper().TrimStart('C', 'O', 'M');
                if (!int.TryParse(port, out _serialPort[0]))
                    _serialPort[0] = 0;
                if (!int.TryParse(_serialPortInfo[1], out _serialPort[1]))
                    _serialPort[1] = 115200;
            }
            return _serialPort;
        }

        public IPEndPoint GetIpEndPoint()
        {
            if (_ipEndPoint == null)
            {
                if (!IPAddress.TryParse(_serialPortInfo[0], out var ip))
                    ip = new IPAddress(new byte[] {0x00, 0x00, 0x00, 0x00});
                if (!int.TryParse(_serialPortInfo[1], out var port))
                    port = 5025;
                _ipEndPoint = new IPEndPoint(ip, port);
            }
            return _ipEndPoint;
        }

        public static ChannelPort Build(ChannelType channelType, params string[] ports)
        {
            var port = new ChannelPort
            {
                ChannelType = channelType,
                _serialPortInfo = ports
            };
            switch (channelType)
            {
                case ChannelType.Serial:
                {
                    if (!int.TryParse(ports[0], out port._serialPort[0]))
                        port._serialPort[0] = 0;
                    if (ports.Length == 1 || !int.TryParse(ports[1], out port._serialPort[1]))
                        port._serialPort[1] = 115200;
                    port._id = $"{port._serialPort[0]}:{port._serialPort[1]}";
                    break;
                }
                case ChannelType.Tcpip:
                {
                    IPAddress ip = IPAddress.Parse(ports[0]);
                    int p = int.Parse(ports[1]);
                    var ipe = new IPEndPoint(ip, p);
                    port._ipEndPoint = ipe;
                    port._id = ipe.ToString();
                    break;
                }
            }
            return port;
        }

        public override string ToString()
        {
            switch (ChannelType)
            {
                case ChannelType.Tcpip:
                    return GetIpEndPoint().ToString();
                case ChannelType.Serial:
                default:
                {
                    int[] ports = GetSerialPortInfo();
                    var sb = new StringBuilder();
                    foreach (int port in ports)
                        sb.Append(port).Append(':');
                    return sb.ToString().TrimEnd(':');
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ChannelPort)) return false;
            return (Equals(((ChannelPort) obj)));
        }

        protected bool Equals(ChannelPort other)
        {
            return _id.Equals(other._id) && ChannelType == other.ChannelType;
        }

        private readonly int _hash = (Guid.NewGuid().GetHashCode() >> 28) * 31;

        /// <summary>用作特定类型的哈希函数。</summary>
        /// <returns>当前 <see cref="T:System.Object" /> 的哈希代码。</returns>
        public override int GetHashCode()
        {
            return _hash;
        }
    }
}
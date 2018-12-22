using System.IO.Ports;
using NKnife.Channels.Interfaces;

namespace NKnife.Channels.Serials
{
    public class SerialConfig : IChannelConfig
    {
        public SerialConfig(ushort port)
        {
            Port = port;
        }

        public ushort Port { get; }
        public int BaudRate { get; set; } = 9600;
        public int DataBit { get; set; } = 8;

        /// <summary>
        ///     读串口数据的固定超时。
        /// </summary>
        public int ReadTotalTimeoutConstant { get; set; } = 1*1000;

        /// <summary>
        ///     写串口数据的固定超时。
        /// </summary>
        public int WriteTotalTimeoutConstant { get; set; } = 1*1000;

        /// <summary>
        /// 事件触发前内部输入缓冲区中的字节数。默认值为 1。
        /// 只要内部缓冲区有大于 ReceivedBytesThreshold 个字节的时候，就会引发 DataReceived 事件。
        /// 一般情况下无需更改。
        /// </summary>
        public int ReceivedBytesThreshold { get; set; } = 1;
        public int ReadBufferSize { get; set; } = 64;
        public bool DtrEnable { get; set; } = false;
        public Parity Parity { get; set; } = Parity.None;
        public bool RtsEnable { get; set; } = true;
        public StopBits StopBit { get; set; } = StopBits.One;

        /// <summary>
        /// 当 ReceivedBytesThreshold 引发 DataReceived 事件后，等待 ReadWait 的时间，待串口数据接收到阶段性时再进行读取。默认值0。
        /// </summary>
        public int ReadWait { get; set; } = 0;

        protected bool Equals(SerialConfig other)
        {
            return Port == other.Port;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SerialConfig) obj);
        }

        public override int GetHashCode()
        {
            return Port.GetHashCode();
        }
    }
}
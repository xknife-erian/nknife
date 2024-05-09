using System.IO.Ports;

namespace NKnife.Serials
{
    /// <summary>
    ///     串口配置参数
    /// </summary>
    public class SerialConfig
    {
        /// <summary>
        ///     波特率
        /// </summary>
        public uint BaudRate { get; set; } = 9600;

        /// <summary>
        ///     停止位
        /// </summary>
        public StopBits StopBit { get; set; } = StopBits.One;

        /// <summary>
        ///     数据位
        /// </summary>
        public DataBits DataBit { get; set; } = DataBits.Eight;

        public Parity Parity { get; set; } = Parity.None;

        public bool RtsEnable { get; set; } = false;

        public bool DtrEnable { get; set; } = false;

        /// <summary>
        ///     读串口数据的固定超时。
        /// </summary>
        public int ReadTotalTimeoutConstant { get; set; } = 1 * 1000;

        /// <summary>
        ///     写串口数据的固定超时。
        /// </summary>
        public int WriteTotalTimeoutConstant { get; set; } = 1 * 1000;

        /// <summary>
        ///     事件触发前内部输入缓冲区中的字节数。默认值为 1。
        ///     只要内部缓冲区有大于 ReceivedBytesThreshold 个字节的时候，就会引发 DataReceived 事件。
        ///     一般情况下无需更改。
        /// </summary>
        public int ReceivedBytesThreshold { get; set; } = 1;

        /// <summary>
        ///     读数缓冲区大小
        /// </summary>
        public int ReadBufferSize { get; set; } = 64;

        /// <summary>
        ///     当 ReceivedBytesThreshold 引发 DataReceived 事件后，等待 ReadWait 的时间，待串口数据接收到阶段性时再进行读取。默认值0。
        /// </summary>
        public int ReadWait { get; set; } = 0;

        public static SerialConfig Default => new SerialConfig();
    }
}
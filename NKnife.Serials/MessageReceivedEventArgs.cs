using System;

namespace NKnife.Serials
{
    /// <summary>
    /// 串口接收到的一包数据（未做任何业务逻辑处理）。
    /// </summary>
    public class MessageReceivedEventArgs
    {
        /// <summary>
        /// 接收到的数据（未做任何业务逻辑处理）
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="data">接收到的数据（未做任何业务逻辑处理）</param>
        public MessageReceivedEventArgs(byte[] data)
        {
            Data = data;
        }
    }

    /// <summary>
    /// 串口接收到的一包数据（未做任何业务逻辑处理）。
    /// </summary>
    public class SerialMessageReceivedEventArgs
    {
        /// <summary>
        /// 接收到的数据（未做任何业务逻辑处理）
        /// </summary>
        public ArraySegment<byte> Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="data">接收到的数据（未做任何业务逻辑处理）</param>
        public SerialMessageReceivedEventArgs(ArraySegment<byte> data)
        {
            Data = data;
        }
    }
}


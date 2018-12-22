using System;

namespace SerialKnife.Pan.Common
{
    /// <summary>串口接收到的数据
    /// </summary>
    public class DataRecvEventArgs : EventArgs
    {
        private readonly string _commId;
        private readonly byte[] _recvData;

        public DataRecvEventArgs(byte[] recv, int count, string commId)
        {
            _commId = commId;
            _recvData = new byte[count];
            Array.Copy(recv, _recvData, count);
        }

        /// <summary>数据
        /// </summary>
        public byte[] RecvData
        {
            get { return _recvData; }
        }

        /// <summary>消息Id（这个参数原本设计作为数据包唯一标识符，
        /// 但目前少数使用者通过这个参数来传递数据收发的串口号）
        /// </summary>
        public string CommId
        {
            get { return _commId; }
        }
    }
}
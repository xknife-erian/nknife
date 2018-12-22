using System;

namespace SerialKnife.Pan.Common
{
    /// <summary>双向数据包发送后，收到回复的事件参数
    /// </summary>
    public class SendRecvEventArgs : EventArgs
    {
        private readonly byte[] _recvData;
        private readonly int _senderId;

        public SendRecvEventArgs(int senderId, byte[] recv, int count)
        {
            _senderId = senderId;
            _recvData = new byte[count];
            Array.Copy(recv, _recvData, count);
        }

        public int SenderId
        {
            get { return _senderId; }
        }

        /// <summary>接收到的数据
        /// </summary>
        public byte[] RecvData
        {
            get { return _recvData; }
        }
    }
}
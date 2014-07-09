using System;
using System.Text;
using SocketKnife.Interfaces;

namespace NKnife.SocketClient.StarterKit.Base.ProtocolTools
{
    /// <summary>
    /// 一个最常用的回复消息的字节数组生成器
    /// </summary>
    public class Encoder : IDatagramEncoder
    {
        #region IDatagramEncoder Members

        public byte[] Execute(string replay, bool isCompress)
        {
            if (string.IsNullOrWhiteSpace(replay))
            {
                return null;
            }
            byte[] contentBytes = Encoding.UTF8.GetBytes(replay);
            byte[] head = BitConverter.GetBytes(contentBytes.Length);
            Array.Reverse(head);
            var bs = new byte[head.Length + contentBytes.Length];
            Array.Copy(head, bs, head.Length);
            Array.Copy(contentBytes, 0, bs, head.Length, contentBytes.Length);
            return bs;
        }

        #endregion
    }
}
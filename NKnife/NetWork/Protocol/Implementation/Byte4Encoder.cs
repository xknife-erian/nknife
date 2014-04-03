using System;
using System.Text;
using NKnife.NetWork.Interfaces;
using NKnife.Utility;
using NLog;

namespace NKnife.NetWork.Protocol.Implementation
{
    /// <summary>
    /// 一个最常用的回复消息的字节数组生成器
    /// </summary>
    public class Byte4Encoder : IDatagramEncoder
    {
        private static readonly Logger _Logger = NLog.LogManager.GetCurrentClassLogger();

        #region IDatagramEncoder Members

        public byte[] Execute(string replay, bool isCompress = false)
        {
            if (string.IsNullOrWhiteSpace(replay))
            {
                return null;
            }

            //处理是否压缩字符串的字节数组进行发送
            byte[] reBytes;
            if (isCompress)
            {
                var src = Encoding.UTF8.GetBytes(replay);
                reBytes = UtilityCompression.Compress(src);
                _Logger.Trace(string.Format("发送字符串压缩:原始:{0},压缩后:{1}", src.Length, reBytes.Length));
            }
            else
            {
                reBytes = Encoding.UTF8.GetBytes(replay);
            }
            //得到将要发送的数据的长度
            var head = BitConverter.GetBytes(reBytes.Length);

            var bs = new byte[head.Length + reBytes.Length];
            Array.Copy(head, bs, head.Length);
            Array.Copy(reBytes, 0, bs, head.Length, reBytes.Length);
            return bs;
        }

        #endregion
    }
}
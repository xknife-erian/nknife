using System;
using System.Text;
using NKnife.NetWork.Interfaces;

namespace Gean.Network.Protocol.Implementation
{
    /// <summary>
    /// 一个最常用的回复消息的字节数组生成器,以“ö”结尾，防止粘包
    /// </summary>
    public class EndMarkEncode : IDatagramEncoder
    {
        private static readonly byte[] _Tail = Encoding.UTF8.GetBytes("ö");

        public byte[] Execute(string replay, bool isCompress = false)
        {
            byte[] content = Encoding.UTF8.GetBytes(replay);

            var result = new byte[content.Length + _Tail.Length];

            Array.Copy(content, result, content.Length);
            Array.Copy(_Tail, 0, result, content.Length, _Tail.Length);

            return result;
        }
    }
}

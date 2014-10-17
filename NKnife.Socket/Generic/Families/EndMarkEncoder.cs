using System;
using System.Text;
using NKnife.Tunnel;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Families
{
    /// <summary>
    /// 一个最常用的回复消息的字节数组生成器,以“ö”结尾，防止粘包
    /// </summary>
    public class EndMarkEncode : IDatagramEncoder
    {
        private static readonly byte[] _tail = Encoding.UTF8.GetBytes("ö");

        public byte[] Execute(string replay)
        {
            byte[] content = Encoding.UTF8.GetBytes(replay);

            var result = new byte[content.Length + _tail.Length];

            Array.Copy(content, result, content.Length);
            Array.Copy(_tail, 0, result, content.Length, _tail.Length);

            return result;
        }

        public bool EnabelCompress { get; set; }
    }
}

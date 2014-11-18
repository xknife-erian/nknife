using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using NKnife.Interface;
using NKnife.Tunnel;
using NKnife.Utility;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Families
{
    public class FixedTailDecoder : KnifeSocketDatagramDecoder
    {
        private static readonly byte[] _tail = Encoding.Default.GetBytes("\r\n");

        public virtual byte[] GetTail()
        {
            return _tail;
        }

        protected virtual string GetString(byte[] data, int index, int count)
        {
            return Encoding.Default.GetString(data, index, count);
        }

        /// <summary>
        /// 解码。将字节数组解析成字符串。
        /// </summary>
        /// <param name="data">需解码的字节数组.</param>
        /// <param name="finishedIndex">已完成解码的数组的长度.</param>
        /// <returns>字符串结果数组</returns>
        public override string[] Execute(byte[] data, out int finishedIndex)
        {
            var start = 0;
            var end = data.Find(_tail);
            if (end < 0)
            {
                finishedIndex = 0;
                return new string[0];
            }
            var result = new List<string>();
            while (end > 0)
            {
                string src = GetString(data, start, end);
                result.Add(src);
                start = end;
                end = data.Find(_tail, end);
            }
            finishedIndex = start;
            return result.ToArray();
        }
    }
}

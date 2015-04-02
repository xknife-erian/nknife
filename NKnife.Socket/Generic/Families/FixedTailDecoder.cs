using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using NKnife.Interface;
using NKnife.Tunnel;
using NKnife.Tunnel.Generic;
using NKnife.Utility;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Families
{
    public class FixedTailDecoder : StringDatagramDecoder
    {
        private byte[] _Tail = Encoding.Default.GetBytes("\r\n");

        public virtual byte[] Tail
        {
            get { return _Tail; }
            set { _Tail = value; }
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
            var end = data.Find(_Tail);
            if (end < 0)
            {
                finishedIndex = 0;
                return new string[0];
            }
            var result = new List<string>();
            while (end > 0)
            {
                int location = start;
                if (start != 0)
                    location = start + _Tail.Length;
                string src = GetString(data, location, end - location);
                result.Add(src);
                start = end;
                if (end + _Tail.Length <= data.Length)
                    end = data.Find(_Tail, end + _Tail.Length);
            }
            finishedIndex = start + _Tail.Length;
            return result.ToArray();
        }
    }
}

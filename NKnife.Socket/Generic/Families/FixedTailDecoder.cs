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

        protected virtual byte[] GetTail()
        {
            return _tail;
        }

        protected virtual string GetString(byte[] data, int index, int count)
        {
            return Encoding.Default.GetString(data, index, count);
        }

        public override string[] Execute(byte[] data, out int done)
        {
            done = data.Length;
            var index = data.IndexOf(_tail);
            if (index < 0)
            {
                done = 0;
                return new string[0];
            }
            string src = GetString(data, 0, index);
            var result = new[] {src};
            return result;
        }
    }
}

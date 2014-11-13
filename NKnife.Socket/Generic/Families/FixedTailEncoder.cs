using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Families
{
    public class FixedTailEncoder : KnifeSocketDatagramEncoder
    {
        private static readonly byte[] _tail = Encoding.Default.GetBytes("\r\n");

        protected virtual byte[] GetTail()
        {
            return _tail;
        }

        public override byte[] Execute(string replay)
        {
            var r = Encoding.Default.GetBytes(replay);
            return r.Concat(GetTail()).ToArray();
        }
    }
}

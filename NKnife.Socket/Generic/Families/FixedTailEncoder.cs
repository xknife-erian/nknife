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
        private byte[] _Tail = Encoding.Default.GetBytes("\r\n");

        public virtual byte[] Tail
        {
            get { return _Tail; }
            set { _Tail = value; }
        }

        protected virtual byte[] GetBytes(string replay)
        {
            return Encoding.Default.GetBytes(replay);
        }

        public override byte[] Execute(string replay)
        {
            if (string.IsNullOrEmpty(replay))
                return _Tail;
            var r = GetBytes(replay);
            return r.Concat(_Tail).ToArray();
        }
    }
}

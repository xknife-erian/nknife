using System.Linq;
using System.Text;

namespace NKnife.Tunnel.Generic
{
    public class FixedTailEncoder : StringDatagramEncoder
    {
        private byte[] _tail = Encoding.Default.GetBytes("\r\n");

        public virtual byte[] Tail
        {
            get { return _tail; }
            set { _tail = value; }
        }

        protected virtual byte[] GetBytes(string replay)
        {
            return Encoding.Default.GetBytes(replay);
        }

        public override byte[] Execute(string replay)
        {
            if (string.IsNullOrEmpty(replay))
                return _tail;
            var r = GetBytes(replay);
            return r.Concat(_tail).ToArray();
        }
    }
}

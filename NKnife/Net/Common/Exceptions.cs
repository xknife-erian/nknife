using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gean.Net.Common
{
    /// <summary>
    /// Socket客户端无法连接时的异常
    /// </summary>
    [Serializable]
    public class SocketClientDisOpenedException : Exception
    {
        public SocketClientDisOpenedException() { }
        public SocketClientDisOpenedException(string message) : base(message) { }
        public SocketClientDisOpenedException(string message, Exception inner) : base(message, inner) { }
        protected SocketClientDisOpenedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

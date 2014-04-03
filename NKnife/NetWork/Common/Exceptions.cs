using System;

namespace NKnife.NetWork.Common
{
    /// <summary>
    /// Socket客户端无法连接时的异常
    /// </summary>
    [Serializable]
    public class SocketClientDisOpenedException : ApplicationException
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

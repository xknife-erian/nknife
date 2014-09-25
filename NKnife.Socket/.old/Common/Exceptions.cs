using System;
using System.Runtime.Serialization;

namespace SocketKnife.Common
{
    /// <summary>
    ///     Socket客户端无法连接时的异常
    /// </summary>
    [Serializable]
    public class SocketClientDisOpenedException : ApplicationException
    {
        public SocketClientDisOpenedException()
        {
        }

        public SocketClientDisOpenedException(string message) : base(message)
        {
        }

        public SocketClientDisOpenedException(string message, Exception inner) : base(message, inner)
        {
        }

        protected SocketClientDisOpenedException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
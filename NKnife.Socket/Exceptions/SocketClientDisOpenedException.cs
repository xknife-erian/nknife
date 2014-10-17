using System;
using NKnife.Exceptions;

namespace SocketKnife.Exceptions
{
    /// <summary>
    ///     Socket客户端无法连接时的异常
    /// </summary>
    [Serializable]
    public class SocketClientDisOpenedException : NKnifeException
    {
        public SocketClientDisOpenedException(string message) 
            : base(message)
        {

        }
    }
}
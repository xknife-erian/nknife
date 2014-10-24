using System;

namespace NKnife.Chesses.Common.Exceptions
{
    [Serializable]
    public class GameException : ChessException
    {
        public GameException() { }
        public GameException(string message) : base(message) { }
        public GameException(string message, System.Exception inner) : base(message, inner) { }
        protected GameException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

using System;

namespace NKnife.Chesses.Common.Exceptions
{
    [Serializable]
    public class PieceException : ChessException
    {
        public PieceException() { }
        public PieceException(string message) : base(message) { }
        public PieceException(string message, System.Exception inner) : base(message, inner) { }
        protected PieceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

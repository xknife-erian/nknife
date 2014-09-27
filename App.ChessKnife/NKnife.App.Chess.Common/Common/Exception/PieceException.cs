using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    [Serializable]
    public class PieceException : ChessException
    {
        public PieceException() { }
        public PieceException(string message) : base(message) { }
        public PieceException(string message, Exception inner) : base(message, inner) { }
        protected PieceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

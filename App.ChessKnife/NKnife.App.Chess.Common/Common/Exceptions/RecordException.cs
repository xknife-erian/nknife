using System;

namespace NKnife.Chesses.Common.Exceptions
{
    [Serializable]
    public class RecordException : ChessException
    {
        public RecordException() { }
        public RecordException(string message) : base(message) { }
        public RecordException(string message, System.Exception inner) : base(message, inner) { }
        protected RecordException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

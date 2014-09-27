using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    [Serializable]
    public class RecordException : ChessException
    {
        public RecordException() { }
        public RecordException(string message) : base(message) { }
        public RecordException(string message, Exception inner) : base(message, inner) { }
        protected RecordException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

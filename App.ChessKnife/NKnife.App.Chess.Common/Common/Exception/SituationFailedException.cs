using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    [global::System.Serializable]
    public class SituationFailedException : ChessException
    {
        public SituationFailedException() { }
        public SituationFailedException(string message) : base(message) { }
        public SituationFailedException(string message, Exception inner) : base(message, inner) { }
        protected SituationFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

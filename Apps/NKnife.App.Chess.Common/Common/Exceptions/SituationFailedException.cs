namespace NKnife.Chesses.Common.Exceptions
{
    [global::System.Serializable]
    public class SituationFailedException : ChessException
    {
        public SituationFailedException() { }
        public SituationFailedException(string message) : base(message) { }
        public SituationFailedException(string message, System.Exception inner) : base(message, inner) { }
        protected SituationFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

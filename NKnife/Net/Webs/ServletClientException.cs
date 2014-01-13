using System;
using System.Runtime.Serialization;

namespace Gean.Net.Webs
{
    [Serializable]
    public class ServletClientException : Exception
    {
        public ServletClientException()
        {
        }

        public ServletClientException(string message)
            : base(message)
        {
        }

        public ServletClientException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ServletClientException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
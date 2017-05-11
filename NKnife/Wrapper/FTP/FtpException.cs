using System;
using System.Runtime.Serialization;

namespace NKnife.Wrapper.FTP
{
    [Serializable]
    public class FtpException : ApplicationException
    {
        public FtpException()
        {
        }

        public FtpException(string message) : base(message)
        {
        }

        public FtpException(string message, Exception inner) : base(message, inner)
        {
        }

        protected FtpException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
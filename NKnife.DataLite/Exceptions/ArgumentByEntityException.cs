using System;

namespace NKnife.DataLite.Exceptions
{
    /// <summary>
    /// 在数据中对实体进行处理时的异常
    /// </summary>
    public class ArgumentByEntityException : ArgumentNullException
    {
        public ArgumentByEntityException(string message, string paramName)
            : base(message, paramName)
        {
        }

    }
}
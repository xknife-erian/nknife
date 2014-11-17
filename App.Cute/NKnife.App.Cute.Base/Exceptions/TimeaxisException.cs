using System;
using NKnife.Exceptions;

namespace NKnife.App.Cute.Base.Exceptions
{
    /// <summary>Timeaxis项目的异常基类
    /// </summary>
    public class TimeaxisException : NKnifeException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        protected TimeaxisException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常消息</param>
        protected TimeaxisException(string message)
            : base(message)
        {
        }

    }
}

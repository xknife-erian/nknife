using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKnife.Exceptions;

namespace Didaku.Engine.Timeaxis.Base.Exceptions
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

using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Exceptions
{
    /// <summary>
    /// 类型转换异常
    /// </summary>
    [Serializable]
    public class TypeConvertingException : GeanException
    {
        public TypeConvertingException(string exceptionMsg, Exception e)
            : base(exceptionMsg, e)
        {

        }
    }
}

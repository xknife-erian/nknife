using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.DataLite.Exceptions
{
    /// <summary>
    /// 操作LiteDB数据库文件时的异常
    /// </summary>
    public class DatabaseFileInvalidOperationException : ArgumentException
    {
        public DatabaseFileInvalidOperationException(string message, string paramName)
            :base(message, paramName)
        {
        }
    }
}

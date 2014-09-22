using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Jeelu.WordSegmentor
{
    /// <summary>
    /// 文件操作异常
    /// </summary>
    public class CFileException : CException
    {
        /// <summary>
        /// 设置异常类型
        /// </summary>
        protected override void SetExceptionType()
        {
            _Type = ExceptionType.FileException;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Code">异常代码</param>
        /// <param name="strMessage">异常消息</param>
        /// <param name="strReason">异常原因</param>
        public CFileException(ExceptionCode Code, String strMessage, String strReason)
            : base(Code, strMessage, strReason)
        {
            Debug.Assert((int)Code >= 1100 && (int)Code < 1200);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Code">异常代码</param>
        /// <param name="strMessage">异常消息</param>
        /// <param name="e">从这个异常引入的异常</param>
        /// <example>
        /// try
        /// {
        /// ......
        /// 
        /// }
        /// catch (Exception e)
        /// {
        ///     CDbException E = new CDbException ("Example Exception", e);
        ///     E.Raise();
        /// }
        /// </example>
        public CFileException(ExceptionCode Code, String strMessage, Exception e)
            : base(Code, strMessage,e)
        {
            Debug.Assert((int)Code >= 1100 && (int)Code < 1200);
        }
 
    }
}

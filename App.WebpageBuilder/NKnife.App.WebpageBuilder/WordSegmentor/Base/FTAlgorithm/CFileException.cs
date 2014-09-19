using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Jeelu.WordSegmentor
{
    /// <summary>
    /// �ļ������쳣
    /// </summary>
    public class CFileException : CException
    {
        /// <summary>
        /// �����쳣����
        /// </summary>
        protected override void SetExceptionType()
        {
            _Type = ExceptionType.FileException;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Code">�쳣����</param>
        /// <param name="strMessage">�쳣��Ϣ</param>
        /// <param name="strReason">�쳣ԭ��</param>
        public CFileException(ExceptionCode Code, String strMessage, String strReason)
            : base(Code, strMessage, strReason)
        {
            Debug.Assert((int)Code >= 1100 && (int)Code < 1200);
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Code">�쳣����</param>
        /// <param name="strMessage">�쳣��Ϣ</param>
        /// <param name="e">������쳣������쳣</param>
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

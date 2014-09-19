using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
[assembly: CLSCompliant(true)]


namespace Jeelu.WordSeg
{
    /// <summary>
    /// 异常类型
    /// 每个派生类都必须在这里定义异常类型
    /// </summary>
    public enum ExceptionType 
    {
        /// <summary>
        /// 未知异常类型
        /// </summary>
        UnKownType  = 0,

        /// <summary>
        /// 数据库异常
        /// </summary>
        DbException = 1,

        /// <summary>
        /// 文件异常
        /// </summary>
        FileException = 2,

        /// <summary>
        /// 用户子系统异常
        /// </summary>
        UserSubSystemException = 1000,

    }

    /// <summary>
    /// 异常码，用于唯一定位异常
    /// </summary>
    public enum ExceptionCode
    {
        /// <summary>
        /// 未知异常号
        /// </summary>
        UnKownCode = 0,

        //DbException Code 1000 - 1099

        /// <summary>
        /// 连接数据库失败
        /// </summary>
        DbConnectFail = 1000,

        /// <summary>
        /// 关闭数据库连接失败
        /// </summary>
        DbCloseFail   = 1002,

        /// <summary>
        /// 执行SQL查询失败
        /// </summary>
        DbQueryFail   = 1003,

        /// <summary>
        /// 执行更改，插入，删除等非查询操作失败
        /// </summary>
        DbExcuteFail  = 1004,

        /// <summary>
        /// 生成插入语句时出错
        /// </summary>
        DbMakeInsertTextFail = 1005,

        //FileException Code 1100 - 1199
        /// <summary>
        /// 生成插入语句时出错
        /// </summary>
        FileReadFail = 1100,

        //FileException Code 1100 - 1199
        /// <summary>
        /// 生成插入语句时出错
        /// </summary>
        FileWriteFail = 1101,


        //User SubSystem Exception Code 10000 - 10099
        /// <summary>
        /// 没有证件号数据
        /// </summary>
        NoCertificateType = 10000,

        /// <summary>
        /// 没有职业类型数据
        /// </summary>
        NoEmployment = 10001,

        /// <summary>
        /// 没有爱好数据
        /// </summary>
        NoFan = 10002,

        /// <summary>
        /// 没有订阅类型数据
        /// </summary>
        NoSubscribeType = 10003,

        /// <summary>
        /// 修改订阅条件出错
        /// </summary>
        UpdateSubscribeFail = 10004,

        /// <summary>
        /// 无效的校验码
        /// </summary>
        InvalidUserCheckNum = 10005,

        /// <summary>
        /// 没有企业类型数据
        /// </summary>
        NoEnterPriseType = 10006

    }

    /// <summary>
    /// 异常的统一基类
    /// 该类是一个抽象类，必须要从这个类派生才可以使用
    /// </summary>
    abstract public class CException : Exception
    {
        /// <summary>
        /// 异常类型
        /// </summary>
        protected ExceptionType _Type;

        /// <summary>
        /// 异常原因
        /// </summary>
        protected String _Reason;

        /// <summary>
        /// 异常的内部堆栈回溯
        /// </summary>
        protected String _InnerStackTrace;

        /// <summary>
        /// 异常代码
        /// </summary>
        protected ExceptionCode _ExceptionCode;

        /// <summary>
        /// 被引用的Exception
        /// </summary>
        protected Exception _Exception;

        private void InitVar ()
        {
            SetExceptionType();
            _Reason = "";
            _InnerStackTrace = "";
            _Exception = null;
            _ExceptionCode = ExceptionCode.UnKownCode;
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CException()
        {
            InitVar();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strMessage">异常消息</param>
        public CException(String strMessage)
            :base(strMessage)
        {
            InitVar();
        }

        static public int GetSqlErrorNumber(Exception e)
        {
            if (e == null)
            {
                return -1;
            }

            try
            {
                if (e is CException)
                {
                    e = ((CException)e)._Exception;

                    if (e == null)
                    {
                        return -1;
                    }
                }

                if (e is SqlException)
                {
                    return ((SqlException)e).Number;
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return -1;
            }
        }

        static public int GetErrorCode(Exception e)
        {
            if (e == null)
            {
                return -1;
            }

            try
            {
                if (e is CException)
                {
                    e = ((CException)e)._Exception;

                    if (e == null)
                    {
                        return -1;
                    }
                }

                if (e is ExternalException)
                {
                    return ((ExternalException)e).ErrorCode;
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Code">异常代码</param>
        /// <param name="strMessage">异常消息</param>
        /// <param name="strReason">异常原因</param>
        public CException(ExceptionCode Code, String strMessage, String strReason)
            : base(strMessage)
        {
            InitVar();
            _Reason = strReason;
            _ExceptionCode = Code;
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
        ///     CException E = new CException ("Example Exception", e);
        ///     E.Raise();
        /// }
        /// </example>
        public CException(ExceptionCode Code, String strMessage, Exception e)
            : base(strMessage)
        {
            InitVar();
            _Reason = e.Message;
            _InnerStackTrace = e.StackTrace;
            _ExceptionCode = Code;
            _Exception = e;
            
        }

        /// <summary>
        /// 获取SQL错误NUMBER。 
        /// </summary>
        public int SqlErrorNumber
        {
            get
            {
                return GetSqlErrorNumber(_Exception);
            }
        }


        /// <summary>
        /// 获取错误的 HRESULT。 
        /// </summary>
        public int ErrorCode
        {
            get
            {
                return GetErrorCode(_Exception);
            }
        }

        /// <summary>
        /// 异常类型
        /// 上层应用处理异常时判断该类型，并做出相应处理
        /// </summary>
        public ExceptionType Type 
        {
            get
            {
                return _Type;
            }
        }

        /// <summary>
        /// 异常信息
        /// </summary>
        public String MessageInfo
        {
            get
            {
                return Message;
            }
        }

        /// <summary>
        /// 发生异常的原因，
        /// 如果该异常是对其它异常的封装，
        /// 则该属性填入其它异常的Message信息
        /// </summary>
        public String Reason
        {
            get
            {
                return _Reason;
            }
        }

        /// <summary>
        /// 内部栈回溯信息，如果该异常是对其它异常的封装，
        /// 则其它异常发生时的栈回溯被填入该属性。
        /// 否则该属性为空字符串
        /// </summary>
        public String InnerStackTrace 
        {
            get
            {
                return _InnerStackTrace;
            }
        }

        /// <summary>
        /// 内部异常，如果该异常是对其它异常的封装，
        /// 则返回该其它异常。
        /// 否则返回null
        /// </summary>
        public Exception InnerExp
        {
            get
            {
                return _Exception;
            }
        }

        /// <summary>
        /// 异常代码
        /// </summary>
        public ExceptionCode Code 
        {
            get
            {
                return _ExceptionCode;
            }
        }

        /// <summary>
        /// 触发异常
        /// </summary>
        public void Raise ()
        {
            throw (this);
        }

        /// <summary>
        /// 设置异常类型，该函数派生类必须要重载!
        /// </summary>
        abstract protected void SetExceptionType();
    }
}

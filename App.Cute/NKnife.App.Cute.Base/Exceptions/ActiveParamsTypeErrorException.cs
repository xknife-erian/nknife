using System;

namespace Didaku.Engine.Timeaxis.Base.Exceptions
{
    /// <summary>描述一个异常：错误的Activity请求参数类型
    /// </summary>
    public class ActiveParamsTypeErrorException : TimeaxisException
    {
        /// <summary>异常：错误的Activity请求参数类型
        /// </summary>
        /// <param name="expectType">期望的类型</param>
        /// <param name="actualType">实际的类型</param>
        public ActiveParamsTypeErrorException(Type expectType, Type actualType)
            : base(string.Format("错误的Activity请求参数类型，期望值是:{0},实际是{1}", expectType.Name, actualType.Name))
        {
        }
    }
}
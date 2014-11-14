using System;

namespace Didaku.Engine.Timeaxis.Base.Exceptions
{
    /// <summary>构建请求参数类型时数据转换为参数类型内部数据时异常
    /// </summary>
    public class ActiveParamsDataConvertErrorException : TimeaxisException
    {
        public ActiveParamsDataConvertErrorException(Exception e)
            : base("构建请求参数类型时数据转换为参数类型内部数据时异常", e)
        {
        }
    }
}
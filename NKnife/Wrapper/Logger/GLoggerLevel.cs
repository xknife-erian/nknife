namespace Gean.Wrapper.Logger
{
    /// <summary>
    /// 日志信息级别
    /// </summary>
    public enum GLogLevel
    {
        /// <summary>
        /// 1.最常见的记录信息，一般用于普通输出
        /// </summary>
        Trace,
        /// <summary>
        /// 2.同样是记录信息，不过出现的频率要比Trace少一些，一般用来调试程序
        /// </summary>
        Debug,
        /// <summary>
        /// 3.信息类型的消息
        /// </summary>
        Info,
        /// <summary>
        /// 4.警告信息，一般用于比较重要的场合
        /// </summary>
        Warn,
        /// <summary>
        /// 5.错误信息
        /// </summary>
        Error,
        /// <summary>
        /// 6.致命异常信息。一般来讲，发生致命异常之后程序将无法继续执行
        /// </summary>
        Fatal
    }
}

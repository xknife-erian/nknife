namespace Gean.Wrapper.Logger
{
    public interface IGLoggerWriter
    {
        /// <summary>
        /// 写入Log信息
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="message">The message.</param>
        void Write(GLogLevel logLevel, params object[] message);

        void Trace(params object[] message);
        void Debug(params object[] message);
        void Info(params object[] message);
        void Warn(params object[] message);
        void Error(params object[] message);
        void Fatal(params object[] message);
    }
}

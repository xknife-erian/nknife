using System;

namespace NLog
{
    public static class LoggerEx
    {
        public static void TraceE(this Logger logger, string message, Exception e)
        {
            logger.Trace(string.Format("{0} -> Ex: {1}\r\n{2}", message, e.Message, e.StackTrace), e);
        }
        public static void DebugE(this Logger logger, string message, Exception e)
        {
            logger.Debug(string.Format("{0} -> Ex: {1}\r\n{2}", message, e.Message, e.StackTrace), e);
        }
        public static void InfoE(this Logger logger, string message, Exception e)
        {
            logger.Info(string.Format("{0} -> Ex: {1}\r\n{2}", message, e.Message, e.StackTrace), e);
        }
        public static void WarnE(this Logger logger, string message, Exception e)
        {
            logger.Warn(string.Format("{0} -> Ex: {1}\r\n{2}", message, e.Message, e.StackTrace), e);
        }
        public static void ErrorE(this Logger logger, string message, Exception e)
        {
            logger.Error(string.Format("{0} -> Ex: {1}\r\n{2}", message, e.Message, e.StackTrace), e);
        }
        public static void FatalE(this Logger logger, string message, Exception e)
        {
            logger.Fatal(string.Format("{0} -> Ex: {1}\r\n{2}", message, e.Message, e.StackTrace), e);
        }
    }
}
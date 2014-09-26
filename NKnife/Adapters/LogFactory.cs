using System;
using System.Diagnostics;
using NKnife.Interface;
using NKnife.Ioc;

namespace NKnife.Adapters
{
    public class LogFactory
    {
        public static ILogger GetLogger(String name)
        {
            var iLoggerFactory = DI.Get<ILoggerFactory>();
            return iLoggerFactory == null ? NopLogger.Instance : iLoggerFactory.GetLogger(name);
        }

        public static ILogger GetLogger(Type clazz)
        {
            return GetLogger(clazz.Name);
        }

        public static ILogger GetLogger<T>()
        {
            return GetLogger(typeof (T).Name);
        }

        public static ILogger GetCurrentClassLogger()
        {
            string loggerName;
            Type declaringType;
            int framesToSkip = 1;
            do
            {

                StackFrame frame = new StackFrame(framesToSkip, false);
                var method = frame.GetMethod();
                declaringType = method.DeclaringType;
                if (declaringType == null)
                {
                    loggerName = method.Name;
                    break;
                }

                framesToSkip++;
                loggerName = declaringType.FullName;
            } while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));

            return GetLogger(loggerName);
        }

        /// <summary>
        /// 空实现
        /// </summary>
        class NopLogger:ILogger
        {
            private static readonly Lazy<NopLogger> _Lazy = new Lazy<NopLogger>(() => new NopLogger());
            public static NopLogger Instance { get { return _Lazy.Value; } }
            private NopLogger(){ }

            public void Trace(string message){ }

            public void Trace(string message, Exception exception)
            {
            }

            public void Trace(LogMessageGenerator messageFunc)
            {
            }

            public void Debug(string message)
            {
            }

            public void Debug(string message, Exception exception)
            {
            }

            public void Debug(LogMessageGenerator messageFunc)
            {
            }

            public void Info(string message)
            {
            }

            public void Info(string message, Exception exception)
            {
            }

            public void Info(LogMessageGenerator messageFunc)
            {
            }

            public void Warn(string message)
            {
            }

            public void Warn(string message, Exception exception)
            {
            }

            public void Warn(LogMessageGenerator messageFunc)
            {
            }

            public void Error(string message)
            {
            }

            public void Error(string message, Exception exception)
            {
            }

            public void Error(LogMessageGenerator messageFunc)
            {
            }

            public void Fatal(string message)
            {
            }

            public void Fatal(string message, Exception exception)
            {
            }

            public void Fatal(LogMessageGenerator messageFunc)
            {
            }
        }
    }
}

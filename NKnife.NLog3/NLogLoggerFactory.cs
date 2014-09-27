using System;
using System.Collections.Concurrent;
using NKnife.Adapters;
using NKnife.Interface;
using NLog;
using LogMessageGenerator = NKnife.Interface.LogMessageGenerator;

namespace NKnife.NLog3
{
    public class NLogLoggerFactory:ILoggerFactory
    {
        private readonly ConcurrentDictionary<string, ILogger> _LoggerMap = new ConcurrentDictionary<string, ILogger>(); 
        /// <summary>
        /// 返回Logger
        /// </summary>
        /// <param name="name">使用Logger的所在类的classname</param>
        /// <returns></returns>
        public ILogger GetLogger(string name)
        {
            return _LoggerMap.GetOrAdd(name, new NLogLogger(name));
        }

        class NLogLogger:ILogger
        {
            private readonly Logger _Logger;

            private NLogLogger() { }

            public NLogLogger(string name)
            {
                _Logger = LogManager.GetCurrentClassLogger();
            }

            public void Trace(string message)
            {
                _Logger.Trace(message);
            }

            public void Trace(string message, Exception exception)
            {
                _Logger.Trace(message, exception);
            }

            public void Trace(LogMessageGenerator messageFunc)
            {
                _Logger.Trace(messageFunc);
            }

            public void Debug(string message)
            {
                _Logger.Debug(message);
            }

            public void Debug(string message, Exception exception)
            {
                _Logger.Debug(message,exception);
            }

            public void Debug(LogMessageGenerator messageFunc)
            {
                _Logger.Debug(messageFunc);
            }

            public void Info(string message)
            {
                _Logger.Info(message);
            }

            public void Info(string message, Exception exception)
            {
                _Logger.Info(message,exception);
            }

            public void Info(LogMessageGenerator messageFunc)
            {
                _Logger.Info(messageFunc);
            }

            public void Warn(string message)
            {
                _Logger.Warn(message);
            }

            public void Warn(string message, Exception exception)
            {
                _Logger.Warn(message,exception);
            }

            public void Warn(LogMessageGenerator messageFunc)
            {
                _Logger.Warn(messageFunc);
            }

            public void Error(string message)
            {
                _Logger.Error(message);
            }

            public void Error(string message, Exception exception)
            {
                _Logger.Error(message,exception);
            }

            public void Error(LogMessageGenerator messageFunc)
            {
                _Logger.Error(messageFunc);
            }

            public void Fatal(string message)
            {
                _Logger.Fatal(message);
            }

            public void Fatal(string message, Exception exception)
            {
                _Logger.Fatal(message,exception);
            }

            public void Fatal(LogMessageGenerator messageFunc)
            {
                _Logger.Fatal(messageFunc);
            }
        }
    }
}

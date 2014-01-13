using System;
using System.Threading;

namespace Gean.Logging
{
    /// <summary>
    /// Provides basic methods for implementation classes,
    /// including the Wrapper class around Log4Net.
    /// </summary>
    public abstract class LogBase : ILog
    {
        #region Protected Data

        protected int _lockMilliSecondsForRead = 1000;
        protected int _lockMilliSecondsForWrite = 1000;
        protected ILog _parent;
        protected ReaderWriterLock _readwriteLock = new ReaderWriterLock();

        #endregion

        #region Constructors

        /// <summary>
        /// Default logger.
        /// </summary>
        public LogBase()
        {
        }


        /// <summary>
        /// Initialize logger with default settings.
        /// </summary>
        public LogBase(Type type)
        {
            Name = type.FullName;
            Settings = new LogSettings();
            Settings.Level = LogLevel.Info;
        }


        /// <summary>
        /// Initialize logger with default settings.
        /// </summary>
        public LogBase(string name)
        {
            Name = name;
            Settings = new LogSettings();
            Settings.Level = LogLevel.Info;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Get / set the parent of this logger.
        /// </summary>
        public ILog Parent { get; set; }


        /// <summary>
        /// Log settings.
        /// </summary>
        public LogSettings Settings { get; set; }

        /// <summary>
        /// Name of this logger.
        /// </summary>
        public virtual string Name { get; set; }


        /// <summary>
        /// Log level.
        /// </summary>
        public virtual LogLevel Level
        {
            get { return Settings.Level; }
            set { ExecuteWrite(() => Settings.Level = value); }
        }


        /// <summary>
        /// Gets a value indicating whether this instance is debug enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is debug enabled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsDebugEnabled
        {
            get { return IsEnabled(LogLevel.Debug); }
        }


        /// <summary>
        /// Gets a value indicating whether this instance is info enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is error enabled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsInfoEnabled
        {
            get { return IsEnabled(LogLevel.Info); }
        }


        /// <summary>
        /// Gets a value indicating whether this instance is warn enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is error enabled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsWarnEnabled
        {
            get { return IsEnabled(LogLevel.Warn); }
        }


        /// <summary>
        /// Gets a value indicating whether this instance is error enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is error enabled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsErrorEnabled
        {
            get { return IsEnabled(LogLevel.Error); }
        }


        /// <summary>
        /// Gets a value indicating whether this instance is fatal enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is fatal enabled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsFatalEnabled
        {
            get { return IsEnabled(LogLevel.Fatal); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get a logger by it's name.
        /// </summary>
        /// <param name="logger"></param>
        public virtual ILog this[string loggerName]
        {
            get { return this; }
        }


        /// <summary>
        /// Get logger at the specified index.
        /// This is a single logger and this call will always return 
        /// referece to self.
        /// </summary>
        /// <param name="logIndex"></param>
        /// <returns></returns>
        public virtual ILog this[int logIndex]
        {
            get { return this; }
        }


        /// <summary>
        /// Whether or not the level specified is enabled.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public virtual bool IsEnabled(LogLevel level)
        {
            return level >= Settings.Level;
        }


        /// <summary>
        /// Logs the event.
        /// </summary>
        /// <remarks>This is the method to override in any logger that extends this class.</remarks>
        public abstract void Log(LogEvent logEvent);


        /// <summary>
        /// Flush the log entries to output.
        /// </summary>
        public virtual void Flush()
        {
            // Nothing here.
        }


        /// <summary>
        /// Logs as Warn.
        /// </summary>
        /// <param name="message">The message.</param>
        public virtual void Warn(object message)
        {
            if (IsEnabled(LogLevel.Warn)) InternalLog(LogLevel.Warn, message, null, null);
        }


        /// <summary>
        /// Logs as Warn.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public virtual void Warn(object message, Exception exception)
        {
            if (IsEnabled(LogLevel.Warn)) InternalLog(LogLevel.Warn, message, exception, null);
        }


        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The args.</param>
        public virtual void Warn(object message, Exception ex, object[] args)
        {
            if (IsEnabled(LogLevel.Warn)) InternalLog(LogLevel.Warn, message, ex, args);
        }


        /// <summary>
        /// Logs as Error.
        /// </summary>
        /// <param name="message">The message.</param>
        public virtual void Error(object message)
        {
            if (IsEnabled(LogLevel.Error)) InternalLog(LogLevel.Error, message, null, null);
        }


        /// <summary>
        /// Logs as Error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public virtual void Error(object message, Exception exception)
        {
            if (IsEnabled(LogLevel.Error)) InternalLog(LogLevel.Error, message, exception, null);
        }


        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The args.</param>
        public virtual void Error(object message, Exception ex, object[] args)
        {
            if (IsEnabled(LogLevel.Error)) InternalLog(LogLevel.Error, message, ex, args);
        }


        /// <summary>
        /// Logs as Debug.
        /// </summary>
        /// <param name="message">The message.</param>
        public virtual void Debug(object message)
        {
            if (IsEnabled(LogLevel.Debug)) InternalLog(LogLevel.Debug, message, null, null);
        }


        /// <summary>
        /// Logs as Debug.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public virtual void Debug(object message, Exception exception)
        {
            if (IsEnabled(LogLevel.Debug)) InternalLog(LogLevel.Debug, message, exception, null);
        }


        /// <summary>
        /// Logs the message as debug.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The args.</param>
        public virtual void Debug(object message, Exception ex, object[] args)
        {
            if (IsEnabled(LogLevel.Debug)) InternalLog(LogLevel.Debug, message, ex, args);
        }


        /// <summary>
        /// Logs as Fatal.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public virtual void Fatal(object message)
        {
            if (IsEnabled(LogLevel.Fatal)) InternalLog(LogLevel.Fatal, message, null, null);
        }


        /// <summary>
        /// Logs as Fatal.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public virtual void Fatal(object message, Exception exception)
        {
            if (IsEnabled(LogLevel.Fatal)) InternalLog(LogLevel.Fatal, message, exception, null);
        }


        /// <summary>
        /// Logs the message as fatal.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The args.</param>
        public virtual void Fatal(object message, Exception ex, object[] args)
        {
            if (IsEnabled(LogLevel.Fatal)) InternalLog(LogLevel.Fatal, message, ex, args);
        }


        /// <summary>
        /// Logs as info.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public virtual void Info(object message)
        {
            if (IsEnabled(LogLevel.Info)) InternalLog(LogLevel.Info, message, null, null);
        }


        /// <summary>
        /// Logs as info.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public virtual void Info(object message, Exception exception)
        {
            if (IsEnabled(LogLevel.Info)) InternalLog(LogLevel.Info, message, exception, null);
        }


        /// <summary>
        /// Infoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The args.</param>
        public virtual void Info(object message, Exception ex, object[] args)
        {
            if (IsEnabled(LogLevel.Info)) InternalLog(LogLevel.Info, message, ex, args);
        }


        /// <summary>
        /// Logs as Message.
        /// </summary>
        /// <param name="message">The message.</param>
        public virtual void Message(object message)
        {
            InternalLog(LogLevel.Message, message, null, null);
        }


        /// <summary>
        /// Logs as Message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public virtual void Message(object message, Exception exception)
        {
            InternalLog(LogLevel.Message, message, exception, null);
        }


        /// <summary>
        /// Infoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The args.</param>
        public virtual void Message(object message, Exception ex, object[] args)
        {
            InternalLog(LogLevel.Message, message, ex, args);
        }


        /// <summary>
        /// Shutdown logger.
        /// </summary>
        public virtual void ShutDown()
        {
            Console.WriteLine("Shutting down logger " + Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="args"></param>
        public virtual void InternalLog(LogLevel level, object message, Exception ex, object[] args)
        {
            LogEvent logevent = BuildLogEvent(level, message, ex, args);
            Log(logevent);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Construct the logevent using the values supplied.
        /// Fills in other data values in the log event.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="args"></param>
        public virtual LogEvent BuildLogEvent(LogLevel level, object message, Exception ex, object[] args)
        {
            return LogHelper.BuildLogEvent(GetType(), level, message, ex, args);
        }


        /// <summary>
        /// Builds the log message using message and arguments.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        protected virtual string BuildMessage(LogEvent logEvent)
        {
            return LogFormatter.Format("", logEvent);
        }

        #endregion

        #region Synchronization Helper Methods

        /// <summary>
        /// Exectutes the action under a read operation after
        /// aquiring the reader lock.
        /// </summary>
        /// <param name="executor"></param>
        protected void ExecuteRead(ActionVoid executor)
        {
            AcquireReaderLock();
            try
            {
                executor();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                ReleaseReaderLock();
            }
        }


        /// <summary>
        /// Exectutes the action under a write operation after
        /// aquiring the writer lock.
        /// </summary>
        /// <param name="executor"></param>
        protected void ExecuteWrite(ActionVoid executor)
        {
            AcquireWriterLock();
            try
            {
                executor();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                ReleaseWriterLock();
            }
        }


        /// <summary>
        /// Gets the reader lock.
        /// </summary>
        protected void AcquireReaderLock()
        {
            _readwriteLock.AcquireReaderLock(_lockMilliSecondsForRead);
        }


        /// <summary>
        /// Release the reader lock.
        /// </summary>
        protected void ReleaseReaderLock()
        {
            _readwriteLock.ReleaseReaderLock();
        }


        /// <summary>
        /// Acquire the writer lock.
        /// </summary>
        protected void AcquireWriterLock()
        {
            _readwriteLock.AcquireWriterLock(_lockMilliSecondsForWrite);
        }


        /// <summary>
        /// Release the writer lock.
        /// </summary>
        protected void ReleaseWriterLock()
        {
            _readwriteLock.ReleaseWriterLock();
        }

        #endregion
    }
}
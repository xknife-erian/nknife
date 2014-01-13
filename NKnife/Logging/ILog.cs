﻿using System;

namespace Gean.Logging
{
    /// <summary>
    /// Interface for a logger that represents a chain(multiple) loggers.
    /// </summary>
    public interface ILogMulti : ILog
    {
        /// <summary>
        /// Get a logger by it's name.
        /// </summary>
        ILog this[string loggerName] { get; }


        /// <summary>
        /// Get a logger by it's index position.
        /// </summary>
        ILog this[int index] { get; }

        /// <summary>
        /// Get the number of loggers that are in here.
        /// </summary>
        int Count { get; }


        /// <summary>
        /// Append another logger to the chain of loggers.
        /// </summary>
        /// <param name="logger"></param>
        void Append(ILog logger);


        /// <summary>
        /// Clear all the chained loggers.
        /// </summary>
        void Clear();
    }


    /// <summary>
    /// Simple interface for logging information.
    /// This extends the common Log4net interface by 
    /// 
    /// 1. Taking additional argument as an object array
    /// 2. Exposing a simple Log method that takes in the loglevel.
    /// 
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Get the name of the logger.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Get / set the loglevel.
        /// </summary>
        LogLevel Level { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is debug enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is debug enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is error enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is error enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is fatal enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is fatal enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsFatalEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is info enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is info enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsInfoEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is warn enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is warn enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsWarnEnabled { get; }

        /// <summary>
        /// Logs the specified level.
        /// </summary>
        void Log(LogEvent logEvent);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warn(object message);

        /// <summary>
        /// Logs a warning message with exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Warn(object message, Exception exception);

        /// <summary>
        /// Logs a warning message with exception and additional arguments.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="args">Additional arguments.</param>
        void Warn(object message, Exception exception, object[] args);

        /// <summary>
        /// Logs a Error message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Error(object message);

        /// <summary>
        /// Logs a Error message with exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Error(object message, Exception exception);

        /// <summary>
        /// Logs an error message with the exception additional arguments.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="args">The args.</param>
        void Error(object message, Exception exception, object[] args);

        /// <summary>
        /// Logs a Debug message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Debug(object message);

        /// <summary>
        /// Logs a Debug message with exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Debug(object message, Exception exception);

        /// <summary>
        /// Logs a debug message with the exception and arguments.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="args">The args.</param>
        void Debug(object message, Exception exception, object[] args);

        /// <summary>
        /// Logs a Fatal message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Fatal(object message);

        /// <summary>
        /// Logs a Fatal message with exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Fatal(object message, Exception exception);

        /// <summary>
        /// Logs a fatal message with exception and arguments.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="args">The args.</param>
        void Fatal(object message, Exception exception, object[] args);

        /// <summary>
        /// Logs a Info message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(object message);

        /// <summary>
        /// Logs a Info message with exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Info(object message, Exception exception);

        /// <summary>
        /// Logs a info message with the arguments.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="args">The args.</param>
        void Info(object message, Exception exception, object[] args);

        /// <summary>
        /// Logs a Message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Message(object message);

        /// <summary>
        /// Logs a Message with exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Message(object message, Exception exception);

        /// <summary>
        /// Messages should always get logged.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="args">The args.</param>
        void Message(object message, Exception exception, object[] args);

        /// <summary>
        /// Is the level enabled.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        bool IsEnabled(LogLevel level);

        /// <summary>
        /// Builds a log event from the parameters supplied.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        LogEvent BuildLogEvent(LogLevel level, object message, Exception ex, object[] args);

        /// <summary>
        /// Flushes the buffers.
        /// </summary>
        void Flush();

        /// <summary>
        /// Shutdown the logger.
        /// </summary>
        void ShutDown();
    }
}
using System;

namespace NKnife.Interface
{
    public delegate string LogMessageGenerator();

    /// <summary>
    /// 日志记录器的接口
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        ///     Writes the diagnostic message at the <c>Trace</c> level.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Trace(string message);

        /// <summary>
        ///     Writes the diagnostic message and exception at the <c>Trace</c> level.
        /// </summary>
        /// <param name="message">string to be written</param>
        /// <param name="exception">An exception to be logged.</param>
        void Trace(string message, Exception exception);

        /// <summary>
        ///     Writes the diagnostic message at the <c>Trace</c> level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        void Trace(LogMessageGenerator messageFunc);

        /// <summary>
        ///     Writes the diagnostic message at the <c>Debug</c> level.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Debug(string message);

        /// <summary>
        ///     Writes the diagnostic message and exception at the <c>Debug</c> level.
        /// </summary>
        /// <param name="message">string to be written</param>
        /// <param name="exception">An exception to be logged.</param>
        void Debug(string message, Exception exception);

        /// <summary>
        ///     Writes the diagnostic message at the <c>Debug</c> level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        void Debug(LogMessageGenerator messageFunc);

        /// <summary>
        ///     Writes the diagnostic message at the <c>Info</c> level.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Info(string message);

        /// <summary>
        ///     Writes the diagnostic message and exception at the <c>Info</c> level.
        /// </summary>
        /// <param name="message">string to be written</param>
        /// <param name="exception">An exception to be logged.</param>
        void Info(string message, Exception exception);

        /// <summary>
        ///     Writes the diagnostic message at the <c>Info</c> level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        void Info(LogMessageGenerator messageFunc);

        /// <summary>
        ///     Writes the diagnostic message at the <c>Warn</c> level.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Warn(string message);

        /// <summary>
        ///     Writes the diagnostic message and exception at the <c>Warn</c> level.
        /// </summary>
        /// <param name="message">string to be written</param>
        /// <param name="exception">An exception to be logged.</param>
        void Warn(string message, Exception exception);

        /// <summary>
        ///     Writes the diagnostic message at the <c>Warn</c> level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        void Warn(LogMessageGenerator messageFunc);

        /// <summary>
        ///     Writes the diagnostic message at the <c>Error</c> level.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Error(string message);

        /// <summary>
        ///     Writes the diagnostic message and exception at the <c>Error</c> level.
        /// </summary>
        /// <param name="message">string to be written</param>
        /// <param name="exception">An exception to be logged.</param>
        void Error(string message, Exception exception);

        /// <summary>
        ///     Writes the diagnostic message at the <c>Error</c> level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        void Error(LogMessageGenerator messageFunc);

        /// <summary>
        ///     Writes the diagnostic message at the <c>Fatal</c> level.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Fatal(string message);

        /// <summary>
        ///     Writes the diagnostic message and exception at the <c>Fatal</c> level.
        /// </summary>
        /// <param name="message">string to be written</param>
        /// <param name="exception">An exception to be logged.</param>
        void Fatal(string message, Exception exception);

        /// <summary>
        ///     Writes the diagnostic message at the <c>Fatal</c> level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        void Fatal(LogMessageGenerator messageFunc);
    }
}
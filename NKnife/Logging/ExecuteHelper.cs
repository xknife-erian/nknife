using System;
using System.Diagnostics;

namespace Gean.Logging
{
    class ExecuteHelper
    {
        /// <summary>
        /// Executes an action inside a try catch and does not do anything when
        /// an exception occurrs.
        /// </summary>
        /// <param name="action"></param>
        public static void TryCatch(ActionVoid action)
        {
            TryCatchLog(string.Empty, null, action);
        }

        /// <summary>
        /// Executes an action inside a try catch and logs any exceptions.
        /// </summary>
        /// <param name="action"></param>
        public static void TryCatchLog(ActionVoid action)
        {
            ILog log = Logger.GetNew<ExecuteHelper>();
            TryCatchLog(string.Empty, log, action);
        }

        /// <summary>
        /// Executes an action inside a try catch and logs any exceptions.
        /// </summary>
        public static void TryCatchLog(string errorMessage, ActionVoid action)
        {
            ILog log = Logger.GetNew<ExecuteHelper>();
            TryCatchLog(errorMessage, log, action);
        }

        /// <summary>
        /// Executes an action inside a try catch and logs any exceptions.
        /// </summary>
        public static void TryCatchLog(string errorMessage, ILog log, ActionVoid action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (log != null) log.Error(errorMessage, ex, null);
            }
        }

        /// <summary>
        /// Executes an action inside a try catch and does not do anything when
        /// an exception occurrs.
        /// </summary>
        public static void TryCatchHandle(ActionVoid actionCode)
        {
            TryCatchFinallySafe(string.Empty, actionCode, HandleException, null);
        }

        /// <summary>
        /// Executes an action inside a try catch and does not do anything when
        /// an exception occurrs.
        /// </summary>
        public static void TryCatchHandle(ActionVoid actionCode, ActionVoid finallyCode)
        {
            TryCatchFinallySafe(string.Empty, actionCode, HandleException, finallyCode);
        }

        /// <summary>
        /// Executes an action inside a try catch and logs any exceptions.
        /// </summary>
        public static void TryCatchFinallySafe(string errorMessage, ActionVoid action, Action<Exception> exceptionHandler, ActionVoid finallyHandler)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (exceptionHandler != null)
                    exceptionHandler(ex);
            }
            finally
            {
                if (finallyHandler != null)
                {
                    TryCatchHandle(finallyHandler);
                }
            }
        }

        /// <summary>
        /// Executes an action inside a try catch and logs any exceptions.
        /// </summary>
        public static void TryCatch(ActionVoid action, Action<Exception> exceptionHandler)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (exceptionHandler != null)
                    exceptionHandler(ex);
            }
        }

        /// <summary>
        /// Executes an action inside a try catch and logs any exceptions.
        /// </summary>
        public static T TryCatchLog<T>(string errorMessage, Func<T> action)
        {
            ILog log = Logger.GetNew<ExecuteHelper>();
            return TryCatchLogRethrow(errorMessage, log, false, action);
        }

        /// <summary>
        /// Executes an action inside a try catch and logs any exceptions.
        /// </summary>
        public static T TryCatchLog<T>(string errorMessage, ILog log, Func<T> action)
        {
            return TryCatchLogRethrow(errorMessage, log, false, action);
        }

        /// <summary>
        /// Executes an action inside a try catch and logs any exceptions.
        /// </summary>
        public static T TryCatchLogRethrow<T>(string errorMessage, ILog log, bool rethrow, Func<T> action)
        {
            T result = default(T);
            try
            {
                result = action();
            }
            catch (Exception ex)
            {
                if (log != null)
                    log.Error(errorMessage, ex, null);
                if (rethrow)
                    throw;
            }
            return result;
        }

        /// <summary>
        /// Handle the highest level application exception.
        /// </summary>
        /// <param name="ex"></param>
        public static void HandleException(Exception ex)
        {
            string message = string.Format("{0}, {1} \r\n {2}", ex.Message, ex.Source, ex.StackTrace);
            Console.WriteLine(message);
            try
            {
                ILog log = Logger.GetNew<ExecuteHelper>();
                log.Error(null, ex, null);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        /// <summary>
        /// Handle the highest level application exception.
        /// </summary>
        /// <param name="ex"></param>
        public static void HandleExceptionLight(Exception ex)
        {
            string message = string.Format("{0}, {1}", ex.Message, ex.Source);
            Console.WriteLine(message);
            try
            {
                ILog log = Logger.GetNew<ExecuteHelper>();
                log.Error(message, null, null);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}
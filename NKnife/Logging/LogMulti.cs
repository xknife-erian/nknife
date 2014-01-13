using System.Collections;
using System.Collections.Generic;
using Gean.Collections;

namespace Gean.Logging
{
    /// <summary>
    /// Logging class that will log to multiple loggers.
    /// </summary>
    public class LogMulti : LogBase, ILogMulti
    {
        private DictionaryOrdered<string, ILog> _Loggers;
        private LogLevel _LowestLevel = LogLevel.Debug;

        /// <summary>
        /// Initalize multiple loggers.
        /// </summary>
        /// <param name="name"> </param>
        /// <param name="logger"></param>
        public LogMulti(string name, ILog logger)
            : base(typeof (LogMulti).FullName)
        {
            Init(name, new List<ILog> {logger});
        }


        /// <summary>
        /// Initalize multiple loggers.
        /// </summary>
        public LogMulti(string name, IList<ILog> loggers) : base(typeof (LogMulti).FullName)
        {
            Init(name, loggers);
        }

        #region ILogMulti Members

        /// <summary>
        /// Log the event to each of the loggers.
        /// </summary>
        /// <param name="logEvent"></param>
        public override void Log(LogEvent logEvent)
        {
            // Log using the readerlock.
            ExecuteRead(() => _Loggers.ForEach(logger => logger.Value.Log(logEvent)));
        }


        /// <summary>
        /// Append to the chain of loggers.
        /// </summary>
        /// <param name="logger"></param>
        public void Append(ILog logger)
        {
            // Add to loggers.
            ExecuteWrite(() => _Loggers.Add(logger.Name, logger));
        }


        /// <summary>
        /// Get the number of loggers that are part of this loggerMulti.
        /// </summary>
        public int Count
        {
            get
            {
                int count = 0;
                ExecuteRead(() => count = _Loggers.Count);
                return count;
            }
        }


        /// <summary>
        /// Clear all the exiting loggers and only add the console logger.
        /// </summary>
        public void Clear()
        {
            ExecuteWrite(() =>
                             {
                                 _Loggers.Clear();
                                 _Loggers.Add("console", new LogConsole());
                             });
        }


        /// <summary>
        /// Get a logger by it's name.
        /// </summary>
        public override ILog this[string loggerName]
        {
            get
            {
                ILog logger = null;
                ExecuteRead(() =>
                                {
                                    if (!_Loggers.ContainsKey(loggerName))
                                        return;

                                    logger = _Loggers[loggerName];
                                });
                return logger;
            }
        }


        /// <summary>
        /// Get a logger by it's name.
        /// </summary>
        public override ILog this[int logIndex]
        {
            get
            {
                ILog logger = null;
                if (logIndex < 0) return null;

                ExecuteRead(() =>
                                {
                                    if (logIndex >= _Loggers.Count)
                                        return;

                                    logger = _Loggers[logIndex];
                                });
                return logger;
            }
        }


        /// <summary>
        /// Get the level. ( This is the lowest level of all the loggers. ).
        /// </summary>
        public override LogLevel Level
        {
            get { return _LowestLevel; }
            set
            {
                ExecuteWrite(() =>
                                 {
                                     _Loggers.ForEach(logger => logger.Value.Level = value);
                                     _LowestLevel = value;
                                 });
            }
        }


        /// <summary>
        /// Whether or not the level specified is enabled.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public override bool IsEnabled(LogLevel level)
        {
            return _LowestLevel <= level;
        }


        /// <summary>
        /// Flushes the buffers.
        /// </summary>
        public override void Flush()
        {
            ExecuteRead(() => _Loggers.ForEach(logger => logger.Value.Flush()));
        }


        /// <summary>
        /// Shutdown all loggers.
        /// </summary>
        public override void ShutDown()
        {
            ExecuteRead(() => _Loggers.ForEach(logger => logger.Value.ShutDown()));
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Determine the lowest level by getting the lowest level
        /// of all the loggers.
        /// </summary>
        public void ActivateOptions()
        {
            // Get the lowest level from all the loggers.
            ExecuteRead(() =>
                            {
                                LogLevel level = LogLevel.Fatal;
                                for (int ndx = 0; ndx < _Loggers.Count; ndx++)
                                {
                                    ILog logger = _Loggers[ndx];
                                    if (logger.Level <= level) level = logger.Level;
                                }
                                _LowestLevel = level;
                            });
        }

        #endregion

        /// <summary>
        /// Initialize with loggers.
        /// </summary>
        public void Init(string name, IList<ILog> loggers)
        {
            Name = name;
            _Loggers = new DictionaryOrdered<string, ILog>();
            loggers.ForEach(logger => _Loggers.Add(logger.Name, logger));
            ActivateOptions();
        }
    }
}
using System;

namespace Gean.Logging
{
    /// <summary>
    /// This is the default extremely simple ( Console ) logger for the static class Logger.
    /// This means that the Logger does NOT have to be initialized with a provider.
    /// You can just use it immediately.
    /// </summary>
    public class LogConsole : LogBase
    {
        private static object _ColorSync = new object();
        private bool _UseColorCoding;

        /// <summary>
        /// Default constructor. Not associated with any class/type.
        /// </summary>
        public LogConsole() 
            : base(typeof (LogConsole).FullName)
        {
        }

        /// <summary>
        /// Constructor with name.
        /// </summary>
        public LogConsole(string name) 
            : this(name, false)
        {
        }


        /// <summary>
        /// Constructor with name.
        /// </summary>
        public LogConsole(string name, bool useColorCoding) 
            : base(name)
        {
            _UseColorCoding = useColorCoding;
        }

        /// <summary>
        /// This is the only method REQUIRED to be implemented.
        /// </summary>
        /// <param name="logEvent"></param>
        public override void Log(LogEvent logEvent)
        {
            if (!string.IsNullOrEmpty(logEvent.FinalMessage))
                Console.WriteLine(logEvent.FinalMessage);
            else
            {
                string message = BuildMessage(logEvent);
                Console.WriteLine(message);
            }
        }
    }
}
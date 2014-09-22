using System.Collections.ObjectModel;

namespace NKnife.Logging.LoggerWPFControl
{
    public sealed class LogMessageCollection : ObservableCollection<LogMessage>
    {
        #region 单件实例

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static LogMessageCollection ME
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            static Singleton() { Instance = new LogMessageCollection(); }
            internal static readonly LogMessageCollection Instance = null;
        }

        private LogMessageCollection()
        {
        }

        #endregion
    }
}

using System;
using System.Collections.ObjectModel;

namespace NKnife.NLog.WPF
{
    internal sealed class LogMessageObservableCollection : ObservableCollection<LogMessage>
    {
        #region 单件实例

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static LogMessageObservableCollection Instance
        {
            get { return _MyInstance.Value; }
        }

        private static readonly Lazy<LogMessageObservableCollection> _MyInstance = new Lazy<LogMessageObservableCollection>(() => new LogMessageObservableCollection());

        #endregion

    }
}

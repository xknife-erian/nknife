using System.Collections;
using System.Collections.Generic;
using NLog;

namespace NKnife.NLog.WPF
{
    public class LogMessageFilter : IList<LogLevel>
    {
        private readonly List<LogLevel> _levels = new List<LogLevel>(6);

        public LogMessageFilter()
        {
            _levels.Add(LogLevel.Trace);
            _levels.Add(LogLevel.Debug);
            _levels.Add(LogLevel.Info);
            _levels.Add(LogLevel.Warn);
            _levels.Add(LogLevel.Error);
            _levels.Add(LogLevel.Fatal);
        }

        public IEnumerator<LogLevel> GetEnumerator()
        {
            return _levels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(LogLevel item)
        {
            if (!_levels.Contains(item))
                _levels.Add(item);
        }

        public void Clear()
        {
            _levels.Clear();
        }

        public bool Contains(LogLevel item)
        {
            return _levels.Contains(item);
        }

        public void CopyTo(LogLevel[] array, int arrayIndex)
        {
            _levels.CopyTo(array, arrayIndex);
        }

        public bool Remove(LogLevel item)
        {
            return _levels.Remove(item);
        }

        public int Count
        {
            get { return _levels.Count; }
        }

        bool ICollection<LogLevel>.IsReadOnly
        {
            get { return ((ICollection<LogLevel>) _levels).IsReadOnly; }
        }

        public int IndexOf(LogLevel item)
        {
            return _levels.IndexOf(item);
        }

        public void Insert(int index, LogLevel item)
        {
            _levels.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _levels.RemoveAt(index);
        }

        public LogLevel this[int index]
        {
            get { return _levels[index]; }
            set { _levels[index] = value; }
        }
    }
}
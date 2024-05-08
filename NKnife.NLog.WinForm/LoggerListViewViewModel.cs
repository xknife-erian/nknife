using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using NKnife.NLog.WinForm.Util;
using NLog;
using Timer = System.Windows.Forms.Timer;

namespace NKnife.NLog.WinForm
{
    internal class LoggerListViewViewModel
    {
        private readonly ConcurrentQueue<CustomLogInfo> _queue = new ConcurrentQueue<CustomLogInfo>();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private uint _maxViewCount = 250;
#if DEBUG
        private Level _currentLevel = Level.Trace | Level.Debug | Level.Info | Level.Warn | Level.Error | Level.Fatal;
#else
        private Level _currentLevel = Level.Info | Level.Warn | Level.Error | Level.Fatal;
#endif

        public static LoggerListViewViewModel Instance => s_myInstance.Value;
        private static readonly Lazy<LoggerListViewViewModel> s_myInstance = new Lazy<LoggerListViewViewModel>(() => new LoggerListViewViewModel());

        private LoggerListViewViewModel()
        {
            var cancellationToken = _cancellationTokenSource.Token;
            var thread = new Thread(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (_queue.TryDequeue(out CustomLogInfo logInfo))
                    {
                        LogInfos.Add(logInfo);
                        SizeCollection(LogInfos, (int)MaxViewCount);
                        Thread.Sleep(1);
                    }
                }
            })
            {
                Name = $"{nameof(LoggerListView)}_ReadLog_Thread",
                IsBackground = true
            };

            thread.Start();
            Application.ApplicationExit += (s, e) =>
            {
                _cancellationTokenSource.Cancel();
            };
        }

        public string ContentFilterRegular { get; set; }
        public string SourceFilterRegular { get; set; }

        public Level CurrentLevel
        {
            get => _currentLevel;
            set
            {
                _currentLevel = value;
                OnCurrentLevelChanged();
            }
        }

        public event EventHandler CurrentLevelChanged;

        public uint MaxViewCount
        {
            get => _maxViewCount;
            set
            {
                _maxViewCount = value;
                OnMaxViewCountChanged();
            }
        }

        public event EventHandler MaxViewCountChanged;

        public ObservableRangeCollection<CustomLogInfo> LogInfos { get; } = new ObservableRangeCollection<CustomLogInfo>();

        public void AddLogInfo(LogEventInfo logEvent)
        {
            if (CurrentLevel.HasFlag(GetTopLevel(logEvent.Level)))
            {
                _queue.Enqueue(new CustomLogInfo(logEvent));
            }
        }

        public static void SizeCollection<T>(ObservableRangeCollection<T> collection, int size)
        {
            if (collection.Count >= size)
            {
                collection.RemoveRange(size - 1, collection.Count - size);
            }
        }

        private static Level GetTopLevel(LogLevel logLevel)
        {
            if (!Enum.TryParse(logLevel.Name, out Level result))
                result = Level.None;
            return result;
        }

        protected virtual void OnMaxViewCountChanged()
        {
            MaxViewCountChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnCurrentLevelChanged()
        {
            CurrentLevelChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
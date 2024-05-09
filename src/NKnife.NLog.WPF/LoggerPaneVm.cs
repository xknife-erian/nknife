using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using NLog;

namespace NKnife.NLog.WPF
{
    public class LoggerPaneVm : BasicNotifyPropertyObject
    {
        private double _callerWidth = 220;

        private LogEventInfo _currentLogger = null!;
        private bool _isFirst;
        private double _levelWidth = 60;
        private double _messageWidth = 550;
        private double _timeWidth = 96;

        private bool _isDisplayTrace = true;
        private bool _isDisplayDebug = true;
        private bool _isDisplayInfo = true;
        private bool _isDisplayWarn = true;
        private bool _isDisplayError = true;
        private bool _isDisplayFatal = true;

        public LoggerPaneVm() : base()
        {
            Logs = LogStack.Instance.Logs;
        }

        public ObservableCollection<LogEventInfo> Logs { get; }

        public double TimeWidth
        {
            get => _timeWidth;
            set => SetProperty(ref _timeWidth, value);
        }

        public double LevelWidth
        {
            get => _levelWidth;
            set => SetProperty(ref _levelWidth, value);
        }

        public double MessageWidth
        {
            get => _messageWidth;
            set => SetProperty(ref _messageWidth, value);
        }

        public double CallerWidth
        {
            get => _callerWidth;
            set => SetProperty(ref _callerWidth, value);
        }

        public int MaxRowCount { get; set; } = 50;
        public string TimeHeader { get; set; } = "Time";
        public string CallerHeader { get; set; } = "Caller";
        public string LevelHeader { get; set; } = "Level";
        public string MessageHeader { get; set; } = "Message";

        public LogEventInfo CurrentLogger
        {
            get => _currentLogger;
            set => SetProperty(ref _currentLogger, value);
        }

        public ICommand ClearAllLogCommand => new RelayCommand((_) => { Logs.Clear(); });

        // public ICommand LoggerItemClickCommand => new RelayCommand<LogEventInfo>(log =>
        // {
        //     if(log != null)
        //         _Bench.ToolsManagerCoordinator.LoggerSelected(log);
        // });
        //
        //
        //
        // public ICommand ViewSizeChangedCommand => new RelayCommand<double>(AdjustColumnWidth);
        //
        // public ICommand ViewLoadedCommand => new RelayCommand<double>(w =>
        // {
        //     if(!_isFirst)
        //     {
        //         AdjustColumnWidth(w);
        //         _isFirst = true;
        //     }
        // });

        public bool IsDisplayTrace
        {
            get => _isDisplayTrace;
            set => SetProperty(ref _isDisplayTrace, value);
        }

        public bool IsDisplayDebug
        {
            get => _isDisplayDebug;
            set => SetProperty(ref _isDisplayDebug, value);
        }

        public bool IsDisplayInfo
        {
            get => _isDisplayInfo;
            set => SetProperty(ref _isDisplayInfo, value);
        }

        public bool IsDisplayWarn
        {
            get => _isDisplayWarn;
            set => SetProperty(ref _isDisplayWarn, value);
        }

        public bool IsDisplayError
        {
            get => _isDisplayError;
            set => SetProperty(ref _isDisplayError, value);
        }

        public bool IsDisplayFatal
        {
            get => _isDisplayFatal;
            set => SetProperty(ref _isDisplayFatal, value);
        }

        #region AutoWidth

        private double _viewWidth = -1;

        private void AdjustColumnWidth(double w)
        {
            if (Math.Abs(_viewWidth - w) <= 0) return;
            if (_viewWidth < 0)
                _viewWidth = w;
            var yw = (w - TimeWidth - LevelWidth) / 10;
            MessageWidth = yw * 7;
            CallerWidth = yw * 2.75;
        }

        #endregion
    }
}
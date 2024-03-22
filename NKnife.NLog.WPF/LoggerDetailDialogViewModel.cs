using System;
using System.Windows.Input;
using NLog;

namespace NKnife.NLog.WPF
{
    public class LoggerDetailDialogViewModel : BasicNotifyPropertyObject
    {
        private LogEventInfo _currentLogger = null!;
        private bool? _dialogResult;

        public LoggerDetailDialogViewModel(LogEventInfo log)
        {
            CurrentLogger = log;
        }

        public LogEventInfo CurrentLogger
        {
            get => _currentLogger;
            set => SetProperty(ref _currentLogger, value);
        }

        public ICommand AcceptCommand => new BasicRelayCommand((_) => {
            DialogResult = true;
        });

        public bool? DialogResult
        {
            get => _dialogResult;
            set => SetProperty(ref _dialogResult, value);
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}

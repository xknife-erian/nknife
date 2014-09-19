using System;
using System.Windows.Input;

namespace NKnife.Mvvm
{
    /// <summary>
    ///     Delegatecommand，这种WPF.SL都可以用，VIEW里面直接使用INTERACTION的trigger激发。适合不同的UIElement控件。
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Func<object, bool> _CanExecute;
        private readonly Action<object> _ExecuteAction;
        private bool _CanExecuteCache;

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecute)
        {
            _ExecuteAction = executeAction;
            _CanExecute = canExecute;
        }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            bool temp = _CanExecute(parameter);

            if (_CanExecuteCache != temp)
            {
                _CanExecuteCache = temp;
                OnCanExecuteChanged();
            }

            return _CanExecuteCache;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _ExecuteAction(parameter);
        }

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion
    }
}
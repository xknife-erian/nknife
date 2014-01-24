using System;
using NKnife.MvvmBase.Interfaces;

namespace NKnife.MvvmBase
{
    /// <summary>一些较为简单的命令可以使用这个比较简单的委托命令类型
    /// </summary>
    public class FormCommand : IFormCommand
    {
        private readonly Predicate<object> _CanExecute;
        private readonly Action<object, EventArgs> _Execute;

        public FormCommand(Action<object, EventArgs> execute)
            : this(execute, null)
        {
        }

        public FormCommand(Action<object, EventArgs> execute, Predicate<object> canExecute)
        {
            _Execute = execute;
            _CanExecute = canExecute;
        }

        #region IFormCommand Members

        public event EventHandler CanExecuteChanged;
        private Action<object, EventArgs> action;

        protected virtual void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }

        public virtual bool CanExecute(object parameter)
        {
            return _CanExecute == null || _CanExecute(parameter);
        }

        public virtual void Execute(object sender, EventArgs e)
        {
            _Execute(sender, e);
        }

        #endregion
    }
}
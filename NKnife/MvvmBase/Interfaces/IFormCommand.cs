using System;

namespace NKnife.MvvmBase.Interfaces
{
    /// <summary>一个基于WinForm的MVVM模式的命令的接口
    /// </summary>
    public interface IFormCommand
    {
        /// <summary>当调用者“是否可执行命令”的属性发生改变时
        /// </summary>
        event EventHandler CanExecuteChanged;

        /// <summary>判断调用者此时是否可执行命令
        /// </summary>
        /// <param name="param">判断的依据参数</param>
        /// <returns>false不可执行，反之，可执行</returns>
        bool CanExecute(object param);

        /// <summary>执行命令，一般为控件的事件激发
        /// </summary>
        void Execute(object sender, EventArgs e);
    }

    /// <summary>一个基于WinForm的MVVM模式的命令的接口
    /// </summary>
    public interface IFormQuickCommand
    {
        /// <summary>当调用者“是否可执行命令”的属性发生改变时
        /// </summary>
        event EventHandler<CanExecuteEventArgs> CanExecuteChanged;

        /// <summary>判断调用者此时是否可执行命令
        /// </summary>
        /// <param name="param">判断的依据参数</param>
        /// <returns>false不可执行，反之，可执行</returns>
        bool CanExecute(object param);

        /// <summary>执行命令，一般为控件的事件激发
        /// </summary>
        void Execute(object sender, EventArgs e);
    }

    public class CanExecuteEventArgs : EventArgs
    {
        public bool CanExecute { get; protected set; }

        public CanExecuteEventArgs(bool canExecute)
        {
            CanExecute = canExecute;
        }
    }

    
}

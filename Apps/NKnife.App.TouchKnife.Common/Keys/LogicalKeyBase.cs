using System;
using System.ComponentModel;
using System.Windows.Controls;
using WindowsInput;
using NKnife.IoC;

namespace NKnife.App.TouchInputKnife.Commons.Keys
{
    /// <summary>
    /// </summary>
    public abstract class LogicalKeyBase : Button, ILogicalKey
    {
        protected readonly InputSimulator _Simulator = DI.Get<InputSimulator>();
        private string _DisplayName;

        /// <summary>
        /// </summary>
        public event LogicalKeyPressedEventHandler LogicalKeyPressed;

        /// <summary>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual string DisplayName
        {
            get { return _DisplayName; }
            set
            {
                if (value != _DisplayName)
                {
                    _DisplayName = value;
                    OnPropertyChanged("DisplayName");
                }
            }
        }

        public virtual void Press()
        {
            OnKeyPressed();
        }

        protected void OnKeyPressed()
        {
            if (LogicalKeyPressed != null)
                LogicalKeyPressed(this, new LogicalKeyEventArgs(this));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var args = new PropertyChangedEventArgs(propertyName);
                handler(this, args);
            }
        }
    }

    public class LogicalKeyEventArgs : EventArgs
    {
        public LogicalKeyEventArgs(ILogicalKey key)
        {
            Key = key;
        }

        public ILogicalKey Key { get; private set; }
    }

    public delegate void LogicalKeyPressedEventHandler(object sender, LogicalKeyEventArgs e);
}
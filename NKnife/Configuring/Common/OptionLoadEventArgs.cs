using System;

namespace Gean.Configuring.Common
{
    public class OptionLoadEventArgs : EventArgs
    {
        public object Tag { get; private set; }
        public OptionLoadEventArgs(object obj)
        {
            this.Tag = obj;
        }
    }
}
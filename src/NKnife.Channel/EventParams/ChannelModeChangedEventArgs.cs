using System;

namespace NKnife.Channel.EventParams
{
    public class ChannelModeChangedEventArgs : EventArgs
    {
        public bool IsSynchronous { get; }

        public ChannelModeChangedEventArgs(bool isSynchronous)
        {
            IsSynchronous = isSynchronous;
        }
    }
}
﻿using System;

namespace NKnife.Channels.Channels.EventParams
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
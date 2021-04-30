using System;
using NKnife.Upgrade4Github.Base;

namespace NKnife.Upgrade4Github.App
{
    class UpdateStatusChangedEventArgs : EventArgs
    {
        public UpdateStatus Status { get; private set; }
        public string Message { get; private set; }

        public UpdateStatusChangedEventArgs(UpdateStatus status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
using System;
using NKnife.Interface;

namespace NKnife.Events
{
    public class JobRunEventArgs : EventArgs
    {
        public JobRunEventArgs(IJob job)
        {
            Job = job;
        }
        public IJob Job { get; }
    }
}
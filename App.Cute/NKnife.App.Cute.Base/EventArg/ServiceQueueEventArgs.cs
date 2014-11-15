using System;
using NKnife.App.Cute.Base.Interfaces;

namespace NKnife.App.Cute.Base.EventArg
{
    public delegate void ServiceQueueEventHandler(IServiceQueue serviceQueue, ServiceQueueEventArgs e);

    public class ServiceQueueEventArgs : EventArgs
    {
        public ITransaction Transaction { get; private set; }

        public ServiceQueueEventArgs(ITransaction tran)
        {
            Transaction = tran;
        }
    }
}
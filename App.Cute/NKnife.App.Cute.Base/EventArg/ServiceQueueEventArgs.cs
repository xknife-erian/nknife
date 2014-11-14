using System;
using Didaku.Engine.Timeaxis.Base.Interfaces;

namespace Didaku.Engine.Timeaxis.Base.EventArg
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Common.Logging;
using NKnife.Tunnel;
using NKnife.Tunnel.Events;

namespace NKnife.Kits.SocketKnife.StressTest.TestCase
{
    public class TestServerMonitorFilter:ITunnelFilter
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        private static object _lockObj = new object();
        private int _SessionCount = 0;

        public EventHandler<ServerStateEventArgs> StateChanged;

        public bool PrcoessReceiveData(ITunnelSession session)
        {
            return true;
        }

        public void ProcessSessionBroken(long id)
        {
            lock (_lockObj)
            {
                _SessionCount -= 1;
                var handler = StateChanged;
                if(handler !=null)
                    handler.Invoke(this,new ServerStateEventArgs
                    {
                        SessionCount = _SessionCount,
                    });
            }
        }

        public void ProcessSessionBuilt(long id)
        {
            lock (_lockObj)
            {
                _SessionCount += 1;
                var handler = StateChanged;
                if(handler !=null)
                    handler.Invoke(this,new ServerStateEventArgs
                    {
                        SessionCount = _SessionCount,
                    });
            }
        }

        public void ProcessSendToSession(ITunnelSession session)
        {
            
        }

        public void ProcessSendToAll(byte[] data)
        {
           
        }

        public event EventHandler<SessionEventArgs> SendToSession;
        public event EventHandler<SessionEventArgs> SendToAll;
        public event EventHandler<SessionEventArgs> KillSession;
    }
}

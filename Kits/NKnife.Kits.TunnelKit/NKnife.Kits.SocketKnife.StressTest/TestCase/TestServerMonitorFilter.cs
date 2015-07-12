using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using Common.Logging;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Tunnel;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Events;

namespace NKnife.Kits.SocketKnife.StressTest.TestCase
{
    public class TestServerMonitorFilter:ITunnelFilter
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        private static object _lockObj = new object();
        private int _SessionCount = 0;
        public int TalkCount { get; set; }

        public EventHandler<ServerStateEventArgs> StateChanged;

        public List<SessionWrapper> SessionList { get; set; }

        public TestServerMonitorFilter()
        {
            SessionList = new List<SessionWrapper>();
        }

        public bool PrcoessReceiveData(ITunnelSession session)
        {
            return true;
        }

        public void ProcessSessionBroken(long id)
        {
            lock (_lockObj)
            {
                RemoveSession(id);
                _SessionCount -= 1;
                InvokeServerStateChanged();
            }
        }

        public void ProcessSessionBuilt(long id)
        {
            lock (_lockObj)
            {
                AddSession(id);
                _SessionCount += 1;
                InvokeServerStateChanged();
            }
        }

        private void AddSession(long id)
        {
            SessionList.Add(new SessionWrapper()
            {
                Id = id,
            });
        }

        private void RemoveSession(long id)
        {
            var index = SessionList.FindIndex(item => item.Id == id);
            if (index > -1)
            {
                SessionList.RemoveAt(index);
            }
            

        }

        public void InvokeServerStateChanged()
        {
            var handler = StateChanged;
            if (handler != null)
                handler.Invoke(this, new ServerStateEventArgs
                {
                    SessionCount = _SessionCount,
                    TalkCount = TalkCount,
                });
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

        public void WriteToSession(long id,byte[] data)
        {
            var handler = SendToSession;
            if (handler != null)
            {
                handler.Invoke(this,new SessionEventArgs(new TunnelSession()
                {
                    Id = id,
                    Data = data
                }));
            }
        }
    }
}

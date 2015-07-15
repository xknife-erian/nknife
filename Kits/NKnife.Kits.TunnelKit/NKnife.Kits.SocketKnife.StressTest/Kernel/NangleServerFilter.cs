using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Base;
using SocketKnife.Generic.Filters;

namespace NKnife.Kits.SocketKnife.StressTest.Kernel
{
    public class NangleServerFilter : SocketBytesProtocolFilter
    {
        private static object _lockObj = new object();
        private int _SessionCount = 0;
        public int TalkCount { get; set; }

        public EventHandler<ServerStateEventArgs> StateChanged;

        public List<SessionWrapper> SessionList { get; set; }

        public NangleServerFilter()
        {
            SessionList = new List<SessionWrapper>();
        }

        public override void ProcessSessionBuilt(long id)
        {
            base.ProcessSessionBuilt(id);

            lock (_lockObj)
            {
                AddSession(id);
                _SessionCount += 1;
                InvokeServerStateChanged();
            }

        }

        public override void ProcessSessionBroken(long id)
        {
            base.ProcessSessionBroken(id);

            lock (_lockObj)
            {
                RemoveSession(id);
                _SessionCount -= 1;
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
    }
}

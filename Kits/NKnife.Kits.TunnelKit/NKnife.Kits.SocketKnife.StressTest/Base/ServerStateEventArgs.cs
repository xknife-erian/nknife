using System;

namespace NKnife.Kits.SocketKnife.StressTest.Base
{
    public class ServerStateEventArgs : EventArgs
    {
        public int SessionCount { get; set; }
        public int TalkCount { get; set; }
    }
}
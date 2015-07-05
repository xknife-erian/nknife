using System;

namespace NKnife.Kits.SocketKnife.StressTest.TestCase
{
    public class ServerStateEventArgs : EventArgs
    {
        public int SessionCount { get; set; }
        public int TalkCount { get; set; }
    }
}
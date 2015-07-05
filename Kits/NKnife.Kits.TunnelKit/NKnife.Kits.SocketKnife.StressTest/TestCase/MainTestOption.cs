using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Kits.SocketKnife.StressTest.TestCase
{
    public class MainTestOption
    {
        public int ClientCount { get; set; }
        public int SendInterval { get; set; }

        public MainTestOption(int clientCount = 3, int sendInterval = 1000)
        {
            ClientCount = clientCount;
            SendInterval = sendInterval;
        }
    }
}

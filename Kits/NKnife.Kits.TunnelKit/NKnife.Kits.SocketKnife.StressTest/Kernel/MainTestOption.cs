using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Kits.SocketKnife.StressTest.TestCase
{
    public class MainTestOption
    {
        public int ClientCount { get; set; }

        public MainTestOption(int clientCount = 3)
        {
            ClientCount = clientCount;
        }
    }
}

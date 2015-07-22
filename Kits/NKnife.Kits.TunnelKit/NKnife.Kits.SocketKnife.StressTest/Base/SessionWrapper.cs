using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Kits.SocketKnife.StressTest.Base
{
    public class SessionWrapper
    {
        public long Id { get; set; }
        public long Address { get; set; }

        public override string ToString()
        {
            return string.Format("Session {0}", Id);
        }
    }
}

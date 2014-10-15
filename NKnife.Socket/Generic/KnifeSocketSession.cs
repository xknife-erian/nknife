using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeSocketSession : ISocketSession
    {
        public KnifeSocketSession()
        {
            Id = DateTime.Now.Ticks;
            WaitHeartBeatingReplay = false;
        }

        public virtual long Id { get; private set; }
        public virtual EndPoint EndPoint { get; set; }
        public virtual Socket Socket { get; set; }
        public bool WaitHeartBeatingReplay { get; set; }
    }
}

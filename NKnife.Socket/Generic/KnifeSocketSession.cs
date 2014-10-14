﻿using System;
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
        }

        public virtual long Id { get; private set; }
        public virtual EndPoint Point { get; set; }
        public virtual Socket Socket { get; set; }

    }
}
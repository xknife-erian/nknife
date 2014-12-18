﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NKnife.Tunnel;
using NKnife.Tunnel.Events;
using SocketKnife.Generic;
using SocketKnife.Interfaces;

namespace SocketKnife.Events
{
    public class SocketSessionEventArgs : SessionEventArgs<byte[], EndPoint>
    {
        public SocketSessionEventArgs(KnifeSocketSession item) 
            : base(item)
        {
        }
    }
}

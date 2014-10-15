using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using SocketKnife.Common;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Filters
{
    public class HeartbeatServerFilter : KnifeSocketServerFilter
    {
        public override void PrcoessReceiveData(Socket socket, byte[] data)
        {
            
        }
    }
}

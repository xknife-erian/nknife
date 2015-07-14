using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Kernel;
using SocketKnife;

namespace NKnife.Kits.SocketKnife.StressTest.Base
{
    public interface IKernel
    {
        KnifeSocketServer Server { get; }
        ServerHandler ProtocolHandler { get;}

        List<KnifeLongSocketClient> Clients { get; }
        List<MockClientHandler> ClientHandlers { get; }
    }
}

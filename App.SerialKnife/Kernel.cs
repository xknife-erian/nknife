using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Management;
using System.Threading;
using NKnife.Channels.Channels.Serials;

namespace SocketKnife
{
    internal class Kernel
    {
        public static void Initialize()
        {
            new Thread(() =>
            {
                SerialUtils.RefreshSerialPorts();
            }).Start();
        }
    }
}
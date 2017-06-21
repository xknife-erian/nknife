using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKnife.Channels.Channels.Serials;
using NKnife.Channels.Interfaces.Channels;

namespace NKnife.Kits.ChannelKit
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialUtils.RefreshSerialPorts();

            Console.WriteLine();
            Console.WriteLine("=== Serial Channel Demo. =========================");
            var serial = new SerialChannel(new SerialConfig(10));

            Console.WriteLine("=== Press any key exit. =========================");
            Console.ReadKey();
        }
    }
}

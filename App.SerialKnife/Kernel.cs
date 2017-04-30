using System.Threading;
using NKnife.Channels.Channels.Serials;

namespace NKnife.Channels.SerialKnife
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
using System.Collections.Generic;
using NKnife.Channels.Serials;

namespace NKnife.ChannelKnife.Controller
{
    public class SerialInfoController
    {
        public Dictionary<string, string> LocalSerial
        {
            get
            {
                SerialUtils.RefreshSerialPorts();
                return SerialUtils.LocalSerialPorts;
            }
        }
    }
}
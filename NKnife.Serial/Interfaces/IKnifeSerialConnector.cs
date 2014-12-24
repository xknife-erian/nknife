using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel;

namespace SerialKnife.Interfaces
{
    public interface IKnifeSerialConnector : IDataConnector<byte[], int>
    {
        int PortNumber { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel;

namespace SerialKnife.Interfaces
{
    public interface IKnifeSerialConnector : IDataConnector
    {
        int PortNumber { get; set; }
    }
}

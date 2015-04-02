using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel;

namespace SerialKnife.Interfaces
{
    public interface ISerialConnector : IDataConnector
    {
        int PortNumber { get; set; }
    }
}

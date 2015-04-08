using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel;
using SerialKnife.Common;

namespace SerialKnife.Interfaces
{
    public interface ISerialConnector : IDataConnector
    {
        int PortNumber { get; set; }

        SerialConfig SerialConfig
        {
            get;
            set;
        }
    }
}

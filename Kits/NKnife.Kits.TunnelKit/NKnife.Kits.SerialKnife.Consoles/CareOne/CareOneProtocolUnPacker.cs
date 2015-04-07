using System;
using System.Diagnostics;
using System.Text;
using NKnife.Converts;
using NKnife.Protocol;
using NKnife.Protocol.Generic;

namespace MonitorKnife.Tunnels.Common
{
    public class CareOneProtocolUnPacker : IProtocolUnPacker<byte[]>
    {
        void IProtocolUnPacker<byte[]>.Execute(IProtocol<byte[]> protocol, byte[] data, byte[] command)
        {
            var careSaying = protocol as CareSaying;
            if (careSaying == null)
            {
                Debug.Assert(careSaying == null, "协议不应为Null");
            }
            Execute(careSaying, data, command);
        }
        public virtual void Execute(CareSaying careSaying, byte[] data, byte[] command)
        {
            ((IProtocol<byte[]>) careSaying).Command = command;
            careSaying.GpibAddress = UtilityConvert.ConvertTo<short>(data[1]);
            careSaying.Content = Encoding.ASCII.GetString(data);
        }

    }
}
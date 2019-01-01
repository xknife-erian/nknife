using System.Collections.Generic;
using NKnife.Channels.Serials;

namespace NKnife.ChannelKnife.Model
{
    public class SerialInfoService
    {
        public Dictionary<string, string> LocalSerial
        {
            get
            {
                SerialUtils.RefreshSerialPorts();
                return SerialUtils.LocalSerialPorts;
            }
        }

        public static ushort DefaultBaudRate { get; } = SerialUtils.DefaultBaudRate;

        public static ushort DefaultStopBit { get; } = SerialUtils.DefaultStopBit;

        public static ushort DefaultDataBit { get; } = SerialUtils.DefaultDataBit;

        public static ushort DefaultParity { get; } = SerialUtils.DefaultParity;

        public static object[] BaudRates { get; } = SerialUtils.BaudRates;

        public static object[] StopBits { get; } = SerialUtils.StopBits;

        public static object[] DataBits { get; } = SerialUtils.DataBits;

        public static object[] Parities { get; } = SerialUtils.Parities;

    }
}
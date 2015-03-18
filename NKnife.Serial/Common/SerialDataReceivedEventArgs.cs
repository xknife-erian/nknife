using System;

namespace SerialKnife.Common
{
    public class SerialDataReceivedEventArgs : EventArgs
    {
        public SerialDataReceivedEventArgs(byte[] data)
        {
            Data = data;
        }

        public byte[] Data { get; set; }
    }
}
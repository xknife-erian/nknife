using System;

namespace NKnife.NSerial.Common
{
    public class SerialProDataReceivedEventArgs : EventArgs
    {
        public SerialProDataReceivedEventArgs(byte[] data)
        {
            Data = data;
        }

        public byte[] Data { get; set; }
    }
}
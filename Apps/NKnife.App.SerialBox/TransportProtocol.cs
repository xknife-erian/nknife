namespace NKnife.SerialBox
{
    public class TransportProtocol
    {
        public const int MAX_LEN = 0x105;
        public const int MIN_LEN = 5;
        public const int DATA_MAX_LENGTH = 0xff;

        public string DeviceAddr { get; set; }

        public string FunctionType { get; set; }

        public string SbData { get; set; }

        public string Sequence { get; set; }

        public string DataLength { get; set; }

        public string TransportProtocolLength { get; set; }

        public string Checksum { get; set; }

        public string Result { get; set; } = "";

        public int ChecksumMethod { get; set; } = 3;

        public bool IsValid { get; set; }
    }
}
using System;

namespace NKnife.Channels
{
    public struct Bytes
    {
        private readonly byte _byte1;
        private readonly byte _byte2;
        private readonly byte _byte3;
        private readonly byte _byte4;

        public Bytes(int kind, int room, int bed, bool isBge = false)
        {
            Build(isBge, kind, room, bed, out _byte1, out _byte2, out _byte3, out _byte4);
            ByteArray = new[] {_byte1, _byte2, _byte3, _byte4};
        }

        public byte Byte1 => _byte1;

        public byte Byte2 => _byte2;

        public byte Byte3 => _byte3;

        public byte Byte4 => _byte4;

        public byte[] ByteArray { get; }

        public static void Build(bool isBge, int kind, int room, int bed, out byte byte1, out byte byte2, out byte byte3, out byte byte4)
        {
            var k = BitConverter.GetBytes(kind);
            var r = BitConverter.GetBytes(room);
            var b = BitConverter.GetBytes(bed);
            if (!isBge)
            {
                Array.Reverse(k);
                Array.Reverse(r);
                Array.Reverse(b);
            }
            byte1 = (byte) (k[3] << 4);
            byte1 = (byte) (byte1 ^ (r[2] >> 2));

            byte2 = (byte) (r[2] << 6);
            byte2 = (byte) (byte2 ^ ((r[3] & 192) >> 2)); //r[3]|192取值后右移两位，再填充到byte2;
            byte2 = (byte) (byte2 ^ ((r[3] & 60) >> 2));

            byte3 = (byte) (r[3] << 6);
            byte3 = (byte) (byte3 ^ b[2] & 63);

            byte4 = b[3];
        }
    }
}
using System;

namespace NKnife.Channels
{
    public struct Bytes
    {
        private readonly byte _Byte1;
        private readonly byte _Byte2;
        private readonly byte _Byte3;
        private readonly byte _Byte4;

        public Bytes(int kind, int room, int bed, bool isBge = false)
        {
            Build(isBge, kind, room, bed, out _Byte1, out _Byte2, out _Byte3, out _Byte4);
            ByteArray = new[] {_Byte1, _Byte2, _Byte3, _Byte4};
        }

        public byte Byte1 => _Byte1;

        public byte Byte2 => _Byte2;

        public byte Byte3 => _Byte3;

        public byte Byte4 => _Byte4;

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
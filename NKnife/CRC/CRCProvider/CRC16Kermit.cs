using System;

namespace NKnife.CRC.CRCProvider
{
    public class CRC16Kermit : BaseCRCProvider
    {
        private readonly uint[] _crcTable = new uint[256];

        public CRC16Kermit(uint polynomial = 0x8408)
        {
            for (uint i = 0; i < _crcTable.Length; ++i)
            {
                uint value = 0;
                var temp = i;
                for (uint j = 0; j < 8; ++j)
                {
                    if (((value ^ temp) & 0x0001) != 0)
                        value = (value >> 1) ^ polynomial;
                    else
                        value >>= 1;
                    temp >>= 1;
                }

                _crcTable[i] = value;
            }
        }

        public override byte[] CRCheck(byte[] source)
        {
            if (source.IsNullOrEmpty())
                throw new ArgumentNullException();
            ushort crc = 0;

            for (uint i = 0; i < source.Length; ++i)
            {
                var index = (byte) (crc ^ source[i]);
                crc = (ushort) ((crc >> 8) ^ _crcTable[index]);
            }

            var crcArray = BitConverter.GetBytes(crc);

            switch (Endianness)
            {
                case Endianness.LE:
                    break;
                case Endianness.BE:
                    Array.Reverse(crcArray);
                    break;
            }
            return crcArray;
        }
    }
}
using System;

namespace NKnife.CRC.CRCProvider
{
    public class CRC32 : BaseCRCProvider
    {
        private readonly uint[] _crcTable = new uint[256];

        public CRC32(uint polynomial = 0xedb88320)
        {
            for (uint i = 0; i < _crcTable.Length; ++i)
            {
                var temp = i;
                for (uint j = 8; j > 0; --j)
                    if ((temp & 1) == 1)
                        temp = (temp >> 1) ^ polynomial;
                    else
                        temp >>= 1;
                _crcTable[i] = temp;
            }
        }

        public override byte[] CRCheck(byte[] source)
        {
            if (source.IsNullOrEmpty())
                throw new ArgumentNullException();
            var crc = 0xffffffff;
            for (var i = 0; i < source.Length; ++i)
            {
                var index = (byte) ((crc & 0xff) ^ source[i]);
                crc = (crc >> 8) ^ _crcTable[index];
            }

            var crcTemp = ~crc;
            var crcArray = BitConverter.GetBytes(crcTemp);
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
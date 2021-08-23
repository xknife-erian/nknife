using System;

namespace NKnife.CRC.CRCProvider
{
    public class CRC8 : BaseCRCProvider
    {
        private readonly uint[] _crcTable = new uint[256];

        public CRC8(uint polynomial = 0xD5)
        {
            for (var i = 0; i < 256; ++i)
            {
                var curr = i;

                for (var j = 0; j < 8; ++j)
                    if ((curr & 0x80) != 0)
                        curr = (curr << 1) ^ (byte) polynomial;
                    else
                        curr <<= 1;

                _crcTable[i] = (byte) curr;
            }
        }

        public override byte[] CRCheck(byte[] source)
        {
            if(source.IsNullOrEmpty())
                throw new ArgumentNullException();
            byte crc = 0;

            foreach (var b in source) 
                crc = (byte) _crcTable[crc ^ b];

            var crcArray = new[] {crc};
            return crcArray;
        }
    }
}
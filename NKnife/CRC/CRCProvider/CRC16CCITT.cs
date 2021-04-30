using System;
using System.Linq;
// ReSharper disable InconsistentNaming

namespace NKnife.CRC.CRCProvider
{
    /// <summary>
    /// CRC16CCITT算法
    /// </summary>
    public class CRC16CCITT : BaseCRCProvider
    {
        private const uint Initail = 4129;
        private readonly uint[] _crcTable = new uint[256];
        private readonly uint _polynomial;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polynomial"></param>
        public CRC16CCITT(uint polynomial = 0)
        {
            _polynomial = polynomial;

            for (uint i = 0; i < _crcTable.Length; ++i)
            {
                uint temp = 0;
                var value = i << 8;
                for (uint j = 0; j < 8; ++j)
                {
                    if (((temp ^ value) & 0x8000) != 0)
                        temp = (temp << 1) ^ Initail;
                    else
                        temp <<= 1;
                    value <<= 1;
                }

                _crcTable[i] = temp;
            }
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="source">待校验的数据</param>
        /// <returns>CRC校验码</returns>
        public override byte[] CRCheck(byte[] source)
        {
            if (source.IsNullOrEmpty())
                throw new ArgumentNullException();
            var crc = (ushort) _polynomial;
            crc = source.Aggregate(crc, (current, t) => (ushort) ((current << 8) ^ _crcTable[(current >> 8) ^ (0xFF & t)]));

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
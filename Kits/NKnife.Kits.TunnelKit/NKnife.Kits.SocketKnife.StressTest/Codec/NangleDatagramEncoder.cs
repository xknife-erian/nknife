using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel.Generic;

namespace NKnife.Kits.SocketKnife.StressTest.Codec
{
    public class NangleDatagramEncoder : BytesDatagramEncoder
    {
        public byte FirstHeadByte { get; set; }
        public byte SecondHeadByte { get; set; }

        private const int LENGTH_BYTE_COUNT = 2;
        private const int TARGET_BYTE_COUNT = 4;
        private const int COMMAND_BYTE_COUNT = 2;
        private const int CHK_BYTE_COUNT = 1;

        public NangleDatagramEncoder()
        {
            FirstHeadByte = 0xAA;
            SecondHeadByte = 0x55;
        }

        /// <summary>
        /// 固定头，尾，长度，校验
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override byte[] Execute(byte[] data)
        {
            var lenBytes = NangleCodecUtility.ConvertFromIntToTwoBytes(data.Length + CHK_BYTE_COUNT); //当前数据长度要增加上最后一个字节的校验位
            var chk = NangleCodecUtility.GetOneByteChk(data);
            if (data.Length >= COMMAND_BYTE_COUNT)
            {
                var result = new byte[data.Length + 5];
                result[0] = FirstHeadByte;
                result[1] = SecondHeadByte;
                result[2] = lenBytes[0];
                result[3] = lenBytes[1];
                result[data.Length + 4] = chk; //最后一位
                Array.Copy(data, 0, result, 4, data.Length);
                return result;
            }
            return new byte[] {FirstHeadByte,SecondHeadByte, 0x00,0x03,0x00,0x00, 0x00};
        }
    }
}

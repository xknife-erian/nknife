using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Kits.SocketKnife.StressTest.Codec
{
    public class NangleCodecUtility
    {
        /// <summary>
        /// 整数长度转换成2字节
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] ConvertFromIntToTwoBytes(int length)
        {
            return new[] { (byte)((length / 256) % 256), (byte)(length % 256) };
        }

        /// <summary>
        /// 2字节转换成整数，如果长度不是2，则返回0
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static int ConvertFromTwoBytesToInt(byte[] bytes)
        {
            if (bytes.Length == 2)
                return bytes[0]*256 + bytes[1];
            return 0;
        }

        /// <summary>
        /// 整数长度转换成4字节
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] ConvertFromIntToFourBytes(int length)
        {
            return new[] { (byte)((length / (256*256*256)) % 256), (byte)((length / (256*256)) % 256), (byte)((length / 256) % 256), (byte)(length % 256) };
        }

        /// <summary>
        /// 计算1字节校验和
        /// </summary>
        /// <param name="tempData"></param>
        /// <returns></returns>
        public static byte GetOneByteChk(byte[] tempData)
        {
            var sum = tempData.Aggregate(0, (current, t) => current + t);
            return (byte)(sum % 256);
        }
    }
}

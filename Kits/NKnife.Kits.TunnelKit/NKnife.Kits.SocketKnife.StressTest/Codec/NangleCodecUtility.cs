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
        public static byte[] GetLengthFromIntToTwoBytes(int length)
        {
            return new[] { (byte)((length / 255) % 255), (byte)(length % 255) };
        }

        /// <summary>
        /// 计算1字节校验和
        /// </summary>
        /// <param name="tempData"></param>
        /// <returns></returns>
        public static byte GetOneByteChk(byte[] tempData)
        {
            var sum = tempData.Aggregate(0, (current, t) => current + t);
            return (byte)(sum % 255);
        }
    }
}

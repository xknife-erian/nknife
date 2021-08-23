using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NKnife.Bytes
{
    /// <summary>
    /// 面向字节的一些帮助方法
    /// </summary>
    public static class UtilByte
    {
        /// <summary>
        ///     根据Int类型的值，返回用1或0(对应true或false)填充的数组
        ///     <remarks>从右侧开始向左索引(0~31)</remarks>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<bool> GetBitList(int value)
        {
            var list = new List<bool>(32);
            for (var i = 0; i <= 31; i++)
            {
                var val = 1 << i;
                list.Add((value & val) == val);
            }
            return list;
        }

        /// <summary>
        ///     返回Int数据中某一位是否为1
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index">32位数据的从右向左的偏移位索引(0~31)</param>
        /// <returns>true表示该位为1，false表示该位为0</returns>
        public static bool GetBitValue(int value, short index)
        {
            if (index > 31) throw new ArgumentOutOfRangeException(nameof(index)); //索引出错
            if (index <= 0) throw new ArgumentOutOfRangeException(nameof(index));
            var val = 1 << index;
            return (value & val) == val;
        }

        /// <summary>
        ///     设定Int数据中某一位的值
        /// </summary>
        /// <param name="value">位设定前的值</param>
        /// <param name="index">32位数据的从右向左的偏移位索引(0~31)</param>
        /// <param name="bitValue">true设该位为1,false设为0</param>
        /// <returns>返回位设定后的值</returns>
        public static int SetBitValue(int value, short index, bool bitValue)
        {
            if (index > 31) throw new ArgumentOutOfRangeException(nameof(index)); //索引出错
            if (index <= 0) throw new ArgumentOutOfRangeException(nameof(index));
            var val = 1 << index;
            return bitValue ? value | val : value & ~val;
        }

        /// <summary>
        ///     将int转换为大端模式的字节数组，即高位在前（与<seealso cref="ToIntByBigEndian"/>相对应）。BigEndian是指低地址存放最高有效字节，高位在前（MSB）；而LittleEndian则是低地址存放最低有效字节，高位在后（LSB）。
        /// </summary>
        public static byte[] ToBigEndianByteArray(int value)
        {
            var src = new byte[4];
            src[0] = (byte)((value >> 24) & 0xFF);
            src[1] = (byte)((value >> 16) & 0xFF);
            src[2] = (byte)((value >> 8) & 0xFF);
            src[3] = (byte)(value & 0xFF);
            return src;
        }

        /// <summary>
        ///     将大端模式（高位在前）的byte数组转为int(与<seealso cref="ToBigEndianByteArray"/>相对应)。BigEndian是指低地址存放最高有效字节，高位在前（MSB）；而LittleEndian则是低地址存放最低有效字节，高位在后（LSB）。
        /// </summary>
        public static int ToIntByBigEndian(byte[] array)
        {
            if (array.Length != 4)
                return -1;
            return ((array[0] & 0xff) << 24) | ((array[1] & 0xff) << 16) | ((array[2] & 0xff) << 8) | ((array[3] & 0xff) << 0);
        }

        /// <summary>
        /// 16进制格式string转byte[]。
        /// </summary>
        /// <param name="hexString">一个描述16进制数据的字符串，每个16进制数据可能是全写，也可能是简写，如0xFF,或者FF。</param>
        /// <param name="separator">每个字节之间的间隔符，默认是空格。可以没有空格符。</param>
        /// <exception cref="ArgumentException">输入16进制数据字符串长度不符合要求</exception>
        /// <exception cref="FormatException">输入16进制数据字符串不符合16进制格式</exception>
        public static byte[] ConvertToBytes(string hexString, string separator = " ")
        {
            if (string.IsNullOrEmpty(separator) && hexString.Length % 2 != 0)
                throw new ArgumentException("输入16进制数据字符串长度不符合要求", nameof(hexString));
            bool hasHexFlag = hexString.Contains("0x");//是否有“0x”标志
            string[] hexArray;
            if (!string.IsNullOrEmpty(separator))
            {
                hexArray = hexString.Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries);
                if (hasHexFlag)
                {
                    for (int i = 0; i < hexArray.Length; i++)
                    {
                        hexArray[i] = hexArray[i].Substring(2);
                    }
                }
            }
            else
            {
                int jump = hasHexFlag ? 4 : 2;
                var list = new List<string>();
                for (int i = 0; i < hexString.Length; i = i + jump)
                {
                    if (hexString.Length >= i + jump)
                    {
                        var sub = hexString.Substring(i, jump);
                        if (hasHexFlag)
                            sub = sub.Substring(2);
                        list.Add(sub);
                    }
                }

                hexArray = list.ToArray();
            }

            byte[] bs = new byte[hexArray.Length];

            for (int i = 0; i < hexArray.Length; i++)
            {
                var b = byte.Parse(hexArray[i], NumberStyles.HexNumber);
                bs[i] = b;
            }

            return bs;
        }

        private static readonly Regex _ByteCharRegex = new Regex("^[A-Fa-f0-9]+$");

        private static bool IsHexDigit(string str)
        {
            return _ByteCharRegex.IsMatch(str);
        }
    }
}

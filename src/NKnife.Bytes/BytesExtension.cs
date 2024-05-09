using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class BytesExtension
    {
        /// <summary>
        ///     判断是否为null,empty,或由指定的数据组成
        /// </summary>
        public static bool IsNullOrEmptyOrConsistBy(this byte[] bytes, byte element = 0x00)
        {
            if (bytes == null || bytes.Length <= 0)
                return true;
            if (bytes.Any(b => b != element))
                return false;
            return true;
        }

        /// <summary>
        /// 指定的字节数组是否全部都是ASCII编码
        /// </summary>
        /// <param name="bytes">指定的字节数组</param>
        public static bool IsASCII(this IEnumerable<byte> bytes)
        {
            return bytes.All(b => b > 0x01 && b <= 127);
        }

        /// <summary>
        ///     字节数组转换成字符串。在针对java程序socket发给C#客户端的处理时，有时候字节流头三个字节bytes[0] == 239 && bytes[1] == 187 && bytes[2] == 191无意义，将此三个字节抛弃不做转换。
        /// </summary>
        public static string ToString(this byte[] bytes, Encoding encoding, bool isJavaSpecial = false)
        {
            if (!Equals(encoding, Encoding.UTF8)) return encoding.GetString(bytes);
            if (isJavaSpecial && bytes[0] == 239 && bytes[1] == 187 && bytes[2] == 191)
                return encoding.GetString(bytes, 3, bytes.Length - 3);
            return encoding.GetString(bytes);
        }

        /// <summary>
        ///     转换为十六进制字符串
        /// </summary>
        public static string ToHexString(this byte b)
        {
            return b.ToString("X2");
        }

        /// <summary>
        ///     转换为十六进制字符串
        /// </summary>
        public static string ToHexString(this IEnumerable<byte> bytes, char[] separatingCharacter)
        {
            if (bytes == null)
                return string.Empty;
            var sb = new StringBuilder();
            foreach (var b in bytes)
                sb.Append(b.ToString("X2")).Append(separatingCharacter);
            return sb.ToString().TrimEnd(separatingCharacter);
        }

        /// <summary>
        ///     转换为十六进制字符串
        /// </summary>
        public static string ToHexString(this IEnumerable<byte> bytes, string separatingCharacter = "")
        {
            return ToHexString(bytes, separatingCharacter.ToCharArray());
        }

        /// <summary>
        ///     转换为Base64字符串
        /// </summary>
        public static string ToBase64String(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        ///     转换为基础数据类型
        /// </summary>
        public static int ToInt(this byte[] value, int startIndex)
        {
            return BitConverter.ToInt32(value, startIndex);
        }

        /// <summary>
        ///     转换为基础数据类型
        /// </summary>
        public static long ToInt64(this byte[] value, int startIndex)
        {
            return BitConverter.ToInt64(value, startIndex);
        }

        /// <summary>
        ///     转换为基础数据类型
        /// </summary>
        public static bool ToBoolean(this byte[] value, int startIndex)
        {
            return BitConverter.ToBoolean(value, startIndex);
        }

        /// <summary>
        ///     转换为基础数据类型
        /// </summary>
        public static char ToChar(this byte[] value, int startIndex)
        {
            return BitConverter.ToChar(value, startIndex);
        }

        /// <summary>
        ///     转换为基础数据类型
        /// </summary>
        public static double ToDouble(this byte[] value, int startIndex)
        {
            return BitConverter.ToDouble(value, startIndex);
        }

        /// <summary>
        ///     转换为基础数据类型
        /// </summary>
        public static float ToSingle(this byte[] value, int startIndex)
        {
            return BitConverter.ToSingle(value, startIndex);
        }

        /// <summary>
        ///     转换为基础数据类型
        /// </summary>
        public static ushort ToUInt16(this byte[] value, int startIndex)
        {
            return BitConverter.ToUInt16(value, startIndex);
        }

        /// <summary>
        ///     转换为基础数据类型
        /// </summary>
        public static uint ToUInt32(this byte[] value, int startIndex)
        {
            return BitConverter.ToUInt32(value, startIndex);
        }

        /// <summary>
        ///     转换为基础数据类型
        /// </summary>
        public static ulong ToUInt64(this byte[] value, int startIndex)
        {
            return BitConverter.ToUInt64(value, startIndex);
        }

        /// <summary>
        ///     使用指定算法Hash
        /// </summary>
        public static byte[] Hash(this byte[] data, string hashName = "")
        {
            var algorithm = string.IsNullOrEmpty(hashName) ? HashAlgorithm.Create() : HashAlgorithm.Create(hashName);
            return algorithm?.ComputeHash(data);
        }

        /// <summary>
        ///     位运算:获取第index位是否为1
        /// </summary>
        public static bool GetBit(this byte b, int index)
        {
            return (b & (1 << index)) > 0;
        }

        /// <summary>
        ///     位运算:将第index位设为1
        /// </summary>
        public static byte SetBit(this byte b, int index)
        {
            b |= (byte) (1 << index);
            return b;
        }

        /// <summary>
        ///     位运算:将第index位设为0
        /// </summary>
        public static byte ClearBit(this byte b, int index)
        {
            b &= (byte) ((1 << 8) - 1 - (1 << index));
            return b;
        }

        /// <summary>
        ///     位运算:将第index位取反
        /// </summary>
        public static byte ReverseBit(this byte b, int index)
        {
            b ^= (byte) (1 << index);
            return b;
        }

        /// <summary>
        ///     保存为文件
        /// </summary>
        public static void Save(this byte[] data, string path)
        {
            File.WriteAllBytes(path, data);
        }

        /// <summary>
        ///     报告指定的字节数组在源数组中的第一个匹配项的索引。
        /// </summary>
        /// <param name="data">源数组</param>
        /// <param name="target">指定的字节数组</param>
        /// <param name="position">开始匹配的位置</param>
        /// <returns>索引值。为-1时，指无匹配项。</returns>
        public static int Find(this byte[] data, byte[] target, int position = 0)
        {
            if (target==null || target.Length <=0)
                return -1;
            int i;

            for (i = position; i < data.Length; i++)
                if (i + target.Length <= data.Length)
                {
                    int j;
                    for (j = 0; j < target.Length; j++)
                        if (data[i + j] != target[j])
                            break;

                    if (j == target.Length)
                        return i;
                }
                else
                {
                    break;
                }

            return -1;
        }

        /// <summary>
        ///     转换为内存流
        /// </summary>
        public static MemoryStream ToMemoryStream(this byte[] data)
        {
            return new MemoryStream(data);
        }

        /// <summary>
        /// 比较字节数组
        /// </summary>
        /// <param name="b1">字节数组1</param>
        /// <param name="b2">字节数组2</param>
        public static bool Compare(this byte[] b1, byte[] b2)
        {
            if (b1.Length != b2.Length)
                return false;
            return b1.Where((t, i) => t.Equals(b2[i])).Any();
        }

        /// <summary>
        /// 用memcmp比较字节数组
        /// </summary>
        /// <param name="b1">字节数组1</param>
        /// <param name="b2">字节数组2</param>
        /// <returns>如果两个数组相同，返回0；如果数组1小于数组2，返回小于0的值；如果数组1大于数组2，返回大于0的值。</returns>
        public static int MemoryCompare(this byte[] b1, byte[] b2)
        {
            IntPtr retval = memcmp(b1, b2, new IntPtr(b1.Length));
            return retval.ToInt32();
        }

        /// <summary>
        /// memcmp API
        /// </summary>
        /// <param name="b1">字节数组1</param>
        /// <param name="b2">字节数组2</param>
        /// <param name="count"></param>
        /// <returns>如果两个数组相同，返回0；如果数组1小于数组2，返回小于0的值；如果数组1大于数组2，返回大于0的值。</returns>
        [DllImport("msvcrt.dll")]
        private static extern IntPtr memcmp(byte[] b1, byte[] b2, IntPtr count);

    }
}
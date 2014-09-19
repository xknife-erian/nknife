using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu
{
    static public partial class Utility
    {
        static public class Stream
        {
            /// <summary>
            /// 读取各种流的公共方法
            /// </summary>
            static public byte[] ReadStream(System.IO.Stream stream, int length)
            {
                if (length <= 0)
                {
                    length = 1024 * 1024;
                }
                byte[] buffer = new byte[length];

                int iStart = 0;
                int iOffset = length;
                while (iOffset > 0)
                {
                    try
                    {
                        int m = stream.Read(buffer, iStart, iOffset);
                        if (m <= 0)
                        {
                            break;
                        }
                        iStart += m;
                        iOffset -= m;
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }

                if (iStart == 0)
                {
                    return new byte[0];
                }
                return buffer;
            }

            /// <summary>
            /// 判断是否两个字节数组相等
            /// </summary>
            public static bool BytesEquals(byte[] bytes1, byte[] bytes2)
            {
                ///若是同一个引用，则返回true
                if (object.ReferenceEquals(bytes1, bytes2))
                {
                    return true;
                }

                if (bytes1 == null || bytes2 == null) return false;

                if (bytes1.Length != bytes2.Length) return false;

                for (int i = 0; i < bytes1.Length; i++)
                    if (bytes1[i] != bytes2[i])
                        return false;

                return true;
            }
        }
    }
}

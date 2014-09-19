using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Jeelu.WordSeg
{
    /// <summary>
    /// 对流操作
    /// </summary>
    public class CStream
    {
        /// <summary>
        /// 将流数据读取到字符数组中
        /// </summary>
        /// <param name="input">输入的流数据</param>
        /// <param name="buf">输出字符数组</param>
        /// <param name="Encode">
        /// 编码方式 
        /// 如 "gb2312" "utf-8"
        /// </param>
        public static void ReadStreamToChars(Stream input, out Char[] buf, String Encode)
        {
            input.Position = 0;

            int read = 0;
            int offset = 0;
            int streamLength = (int)input.Length ;
            Char[] temp = new Char[streamLength];

            StreamReader readStream = new StreamReader(input, Encoding.GetEncoding(Encode));

            while ((read = readStream.Read(temp, offset, streamLength - offset)) > 0)
            {
                offset += read;
            }
            
            buf = new Char[offset];
            Array.Copy(temp, buf, offset);
        }

        /// <summary>
        /// 将流数据读取到字符数组中
        /// </summary>
        /// <param name="input">输入的流数据</param>
        /// <param name="str">输出字符串</param>
        /// <param name="Encode">
        /// 编码方式 
        /// 如 "gb2312" "utf-8"
        /// </param>
        public static void ReadStreamToString(Stream input, out String str, String Encode)
        {
            ReadStreamToString(input, out str, Encoding.GetEncoding(Encode));
        }

        /// <summary>
        /// 将流数据读取到字符数组中
        /// </summary>
        /// <param name="input">输入的流数据</param>
        /// <param name="str">输出字符串</param>
        /// <param name="Encode">
        /// 编码方式 
        /// 如 "gb2312" "utf-8"
        /// </param>
        public static void ReadStreamToString(Stream input, out String str, Encoding Encode)
        {
            input.Position = 0;
            StreamReader readStream = new StreamReader(input, Encode);
            str = readStream.ReadToEnd();
        }


        /// <summary>
        /// 把字符串写入一个流中
        /// </summary>
        /// <param name="str">要写入的字符串</param>
        /// <returns>输出的流</returns>
        /// <param name="Encode">编码方式如 "gb2312" "utf-8"</param>
        public static Stream WriteStringToStream(String str, String Encode)
        {
            return WriteStringToStream(str, Encoding.GetEncoding(Encode));
        }

        /// <summary>
        /// 把字符串写入一个流中
        /// </summary>
        /// <param name="str">要写入的字符串</param>
        /// <returns>输出的流</returns>
        /// <param name="Encode">编码方式如 "gb2312" "utf-8"</param>
        public static Stream WriteStringToStream(String str, Encoding Encode)
        {
            MemoryStream output = new MemoryStream();
            StreamWriter writeStream = new StreamWriter(output, Encode);
            writeStream.Write(str);
            writeStream.Flush();
            output.Position = 0;
            return output;
        }

    }
}

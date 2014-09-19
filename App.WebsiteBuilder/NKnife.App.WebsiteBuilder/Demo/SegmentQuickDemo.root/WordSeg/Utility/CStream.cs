using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Jeelu.WordSeg
{
    /// <summary>
    /// ��������
    /// </summary>
    public class CStream
    {
        /// <summary>
        /// �������ݶ�ȡ���ַ�������
        /// </summary>
        /// <param name="input">�����������</param>
        /// <param name="buf">����ַ�����</param>
        /// <param name="Encode">
        /// ���뷽ʽ 
        /// �� "gb2312" "utf-8"
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
        /// �������ݶ�ȡ���ַ�������
        /// </summary>
        /// <param name="input">�����������</param>
        /// <param name="str">����ַ���</param>
        /// <param name="Encode">
        /// ���뷽ʽ 
        /// �� "gb2312" "utf-8"
        /// </param>
        public static void ReadStreamToString(Stream input, out String str, String Encode)
        {
            ReadStreamToString(input, out str, Encoding.GetEncoding(Encode));
        }

        /// <summary>
        /// �������ݶ�ȡ���ַ�������
        /// </summary>
        /// <param name="input">�����������</param>
        /// <param name="str">����ַ���</param>
        /// <param name="Encode">
        /// ���뷽ʽ 
        /// �� "gb2312" "utf-8"
        /// </param>
        public static void ReadStreamToString(Stream input, out String str, Encoding Encode)
        {
            input.Position = 0;
            StreamReader readStream = new StreamReader(input, Encode);
            str = readStream.ReadToEnd();
        }


        /// <summary>
        /// ���ַ���д��һ������
        /// </summary>
        /// <param name="str">Ҫд����ַ���</param>
        /// <returns>�������</returns>
        /// <param name="Encode">���뷽ʽ�� "gb2312" "utf-8"</param>
        public static Stream WriteStringToStream(String str, String Encode)
        {
            return WriteStringToStream(str, Encoding.GetEncoding(Encode));
        }

        /// <summary>
        /// ���ַ���д��һ������
        /// </summary>
        /// <param name="str">Ҫд����ַ���</param>
        /// <returns>�������</returns>
        /// <param name="Encode">���뷽ʽ�� "gb2312" "utf-8"</param>
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

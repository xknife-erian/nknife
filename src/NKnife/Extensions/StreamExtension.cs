using System;
using System.Collections.Generic;
using System.Text;

// ReSharper disable once CheckNamespace
namespace System.IO
{
    public static class StreamExtension
    {
        /// <summary>
        ///     将流转换成字符串,同时关闭该流
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="encoding">字符编码</param>
        public static string StreamToString(this Stream stream, Encoding encoding)
        {
            //获取的文本
            string streamText;

            //读取流
            try
            {
                using (var reader = new StreamReader(stream, encoding))
                {
                    streamText = reader.ReadToEnd();
                }
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                stream.Close();
            }

            //返回文本
            return streamText;
        }

        /// <summary>
        ///     将流转换成字符串,同时关闭该流
        /// </summary>
        /// <param name="stream">流</param>
        public static string StreamToString(this Stream stream)
        {
            return StreamToString(stream, Encoding.Default);
        }
    }
}

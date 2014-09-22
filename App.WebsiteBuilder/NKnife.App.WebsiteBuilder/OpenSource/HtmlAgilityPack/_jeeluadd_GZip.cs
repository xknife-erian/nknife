using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.SharpZipLib.GZip;
using System.IO;
using System.IO.Compression;

namespace HtmlAgilityPack
{
    class GZip
    {
        //目前有错，暂且注销
        static public string DeCompress(Stream stream)
        {

            GZipStream gzip = new GZipStream(stream, CompressionMode.Decompress);
            using (StreamReader reader = new StreamReader(gzip))
            {
                return reader.ReadToEnd();
            }
        }
        ///   <summary>  
        ///   压缩文件流  
        ///   </summary>  
        ///   <param   name="uncompressedString"></param>  
        ///   <returns></returns>  
        static public string Compress(string uncompressedString)
        {
            byte[] byteData = System.Text.Encoding.UTF8.GetBytes(uncompressedString);
            MemoryStream ms = new MemoryStream();
            Stream s = new GZipOutputStream(ms);
            s.Write(byteData, 0, byteData.Length);
            s.Close();
            byte[] compressData = (byte[])ms.ToArray();
            ms.Flush();
            ms.Close();
            return System.Convert.ToBase64String(compressData, 0, compressData.Length);
        }

        /////   <summary>  
        /////   减压文件流  
        /////   </summary>  
        /////   <param   name="comppressedString"></param>  
        /////   <returns></returns>  
        //static public string DeCompress(string comppressedString)
        //{
        //    int size = 0;
        //    string uncompressString = string.Empty;
        //    StringBuilder sb = new StringBuilder(40960);
        //    int totalLength = 0;
        //    byte[] byteInput = System.Convert.FromBase64String(comppressedString);
        //    byte[] writeData = new byte[4096];
        //    Stream s = new GZipInputStream(new MemoryStream(byteInput));
        //    while (true)
        //    {
        //        size = s.Read(writeData, 0, writeData.Length);
        //        if (size > 0)
        //        {
        //            totalLength += size;
        //            sb.Append(System.Text.Encoding.UTF8.GetString(writeData, 0, size));
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
        //    s.Flush();
        //    s.Close();
        //    return sb.ToString();
        //}
    }
}

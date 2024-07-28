using System.IO.Compression;

namespace NKnife.NLog.Target.Socket.Common
{
    public class CompressionHelper
    {
        /// <summary>
        /// 压缩 byte[] 数据
        /// </summary>
        public static byte[] Compress(byte[] data)
        {
            using var compressedStream = new MemoryStream();
            using var gzipStream = new GZipStream(compressedStream, CompressionMode.Compress);
            gzipStream.Write(data, 0, data.Length);
            return compressedStream.ToArray();
        }

        /// <summary>
        /// 解压缩 byte[] 数据
        /// </summary>
        public static byte[] Decompress(byte[] compressedData)
        {
            using var compressedStream = new MemoryStream(compressedData);
            using var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress);
            using var resultStream = new MemoryStream();
            gzipStream.CopyTo(resultStream);
            return resultStream.ToArray();
        }
    }
}
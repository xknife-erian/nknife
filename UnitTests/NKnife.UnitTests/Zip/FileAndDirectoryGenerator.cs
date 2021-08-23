using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NKnife.Util;

namespace NKnife.UnitTests.Zip
{
    public class FileAndDirectoryGenerator
    {
        /// <summary>
        /// 生成一个目录系统，这个目录将包含指定数量的目录和文件
        /// </summary>
        /// <param name="basePath">根目录</param>
        /// <param name="txtFileCount">文本文件个数</param>
        /// <param name="binaryFileCount">二进制文件个数</param>
        /// <param name="dirLevel">目录级别，每级别目录中包含前两个参数个数的文件总个数</param>
        public static Dir Run(string basePath, ushort txtFileCount, ushort binaryFileCount, ushort dirLevel = 0)
        {
            var dir = new Dir(basePath);
            if (!Directory.Exists(basePath))
                UtilFile.CreateDirectory(basePath);
            if (txtFileCount == 0 && binaryFileCount == 0)
                throw new ArgumentException();
            var path = basePath;
            var currDir = dir;
            for (int level = 0; level < dirLevel + 1; level++) //至少一级，即当前根目录
            {
                if (level > 0)
                {
                    path = Path.Combine(path, $"{level}=={level}\\");
                    Directory.CreateDirectory(path);
                    var d = new Dir(path);
                    currDir.ChildDirectory.Add(d);
                    currDir = d;
                }

                for (int i = 0; i < txtFileCount; i++)
                {
                    var txtFile = CreateTextFile($"{level}-{i}-b", path);
                    currDir.Files.Add(txtFile);
                }

                for (int i = 0; i < txtFileCount; i++)
                {
                    var binFile = CreateBinaryFile($"{level}-{i}-t", path);
                    currDir.Files.Add(binFile);
                }
            }

            return dir;
        }

        private static SimpleFile CreateBinaryFile(string pre, string path)
        {
            var guid = Guid.NewGuid();
            var bs = guid.ToByteArray();
            var fileName = $"{GetFileName(pre, guid.ToString())}.bin";
            var dir = Path.Combine(path, fileName);
            File.WriteAllBytes(dir, bs);
            var sf = new SimpleFile(dir, Mode.Text, new FileInfo(dir).Length, fileName) {BinaryContent = bs};
            return sf;
        }

        private static  SimpleFile CreateTextFile(string pre, string path)
        {
            var guid = Guid.NewGuid().ToString();
            var fileName = $"{GetFileName(pre, guid)}.txt";
            var dir = Path.Combine(path, fileName);
            File.AppendAllText(dir, guid);
            var sf = new SimpleFile(dir, Mode.Text, new FileInfo(dir).Length, fileName) {TextContent = guid};
            return sf;
        }

        private static  string GetFileName(string pre, string content)
        {
            return $"{pre}---{content.Substring(0, 8)}";
        }
    }

    public class Dir
    {
        public Dir(string path)
        {
            Path = path;
        }
        /// <summary>
        /// 本目录的绝对路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 本级目录下的文件
        /// </summary>
        public List<SimpleFile> Files { get; set; } = new List<SimpleFile>();
        /// <summary>
        /// 子目录
        /// </summary>
        public List<Dir> ChildDirectory { get; set; } = new List<Dir>();

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Path;
        }

        #endregion
    }

    public class SimpleFile
    {
        public SimpleFile(string path, Mode mode, long size, string fileName)
        {
            Path = path;
            Mode = mode;
            Size = size;
            File = fileName;
        }

        /// <summary>
        /// 本目录的绝对路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string File { get; set; }

        public Mode Mode { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get; set; }

        public string TextContent { get; set; }
        public byte[] BinaryContent { get; set; }
    }

    /// <summary>
    /// 测试时创建的文件的两种类型
    /// </summary>
    public enum Mode
    {
        Text, Binary
    }
}

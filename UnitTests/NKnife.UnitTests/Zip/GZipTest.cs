using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using NKnife.Zip;
using Xunit;

namespace NKnife.UnitTests.Zip
{
    public class GZipTest
    {
        /// <summary>
        /// 模拟器的测试。（测试的测试）
        /// </summary>
        [Fact]
        public void FileAndDirectoryGeneratorTest()
        {
            var basePath = $"d:\\--TEST-{Guid.NewGuid().ToString()}\\";
            ushort count = 5;
            ushort dirLevel = 5;

            var dir = FileAndDirectoryGenerator.Run(basePath, count, count, dirLevel);
            dir.Path.Should().Be(basePath);
            CheckDir(dir, count);
            Directory.Delete(basePath, true);
        }

        /// <summary>
        /// 递归检查
        /// </summary>
        private static void CheckDir(Dir dir, ushort count)
        {
            Directory.Exists(dir.Path).Should().BeTrue();
            dir.Should().NotBeNull();
            dir.Files.Count.Should().Be(count * 2);
            if (dir.ChildDirectory.Count == 1)
            {
                dir.ChildDirectory.Count.Should().BeLessOrEqualTo(1);
                CheckDir(dir.ChildDirectory[0], count);
            }
        }

        [Fact]
        public void ZipTest()
        {
            var basePath = $"d:\\--TEST-{Guid.NewGuid().ToString()}\\";
            var zipPath = $"d:\\--ZIP-{Guid.NewGuid().ToString()}\\";
            var zipFile = "ZIP-File.zip";
            var deZipPath = $"d:\\--DE_ZIP-{Guid.NewGuid().ToString()}\\";
            var zipFullName = Path.Combine(zipPath, zipFile);
            if (!Directory.Exists(zipPath))
                Directory.CreateDirectory(zipPath);
            ushort count = 10;
            ushort dirLevel = 10;

            var dir = FileAndDirectoryGenerator.Run(basePath, count, count, dirLevel);
            GZip.Compress(basePath, zipPath, "ZIP-File.zip");

            File.Exists(zipFullName).Should().BeTrue($"压缩文件不存在");

            GZip.Decompress(zipPath, deZipPath, zipFile);
            var files = Directory.GetFiles(deZipPath);
            files.Length.Should().Be(count * 2);
            foreach (var file in files)
            {
                var has = false;
                foreach (var simpleFile in dir.Files)
                {
                    var f = new FileInfo(file);
                    if (simpleFile.File.Equals(f.Name))
                    {
                        has = true;
                        f.Length.Should().Be(simpleFile.Size, "文件大小不对");
                        if (f.Extension.Contains("txt"))
                        {
                            var content = File.ReadAllText(file);
                            content.Should().Be(simpleFile.TextContent, "文本文件内容不对");
                        }
                        else if (f.Extension.Contains("bin"))
                        {
                            var content = File.ReadAllBytes(file);
                            for (int j = 0; j < content.Length; j++)
                            {
                                content[j].Should().Be(simpleFile.BinaryContent[j], "二进制文件内容不符");
                            }
                        }
                        else
                        {
                            Assert.False(true, "有不符合要求的文件扩展名");
                        }

                        break;
                    }
                }

                has.Should().BeTrue("压缩包中文件数量不对");
            }

            Directory.Delete(basePath, true);
            Directory.Delete(zipPath, true);
            Directory.Delete(deZipPath, true);
        }

        /// <summary>
        /// 递归检查
        /// </summary>
        private static void CheckZipDir(Dir dir, ushort count)
        {
            Directory.Exists(dir.Path).Should().BeTrue();
            dir.Should().NotBeNull();
            dir.Files.Count.Should().Be(count * 2);
            if (dir.ChildDirectory.Count == 1)
            {
                dir.ChildDirectory.Count.Should().BeLessOrEqualTo(1);
                CheckDir(dir.ChildDirectory[0], count);
            }
        }

    }
}

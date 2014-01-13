using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Gean.Wrapper
{
    /// <summary>
    /// 对SharpZipLib的使用进行了一些易用性封装
    /// </summary>
    public class AblyZip : IDisposable
    {
        private readonly bool _Disposed;
        private ZipFile _SharpZipFile;

        public AblyZip(ZipFile zipFile)
        {
            _Disposed = false;
            _SharpZipFile = zipFile;
        }

        public AblyZip(string fileName)
        {
            _Disposed = false;
            _SharpZipFile = File.Exists(fileName) ? new ZipFile(fileName) : ZipFile.Create(fileName);
        }

        public string Comment
        {
            get
            {
                if (!_Disposed)
                    return _SharpZipFile.ZipFileComment;
                return null;
            }
            set
            {
                if (!_Disposed)
                    _SharpZipFile.SetComment(value);
            }
        }

        public int Count
        {
            get
            {
                if (!_Disposed)
                    return (int) _SharpZipFile.Count;
                return 0;
            }
        }

        public byte[] this[string filename]
        {
            get
            {
                if (_Disposed || !_SharpZipFile.GetEntry(filename).IsFile)
                    return null;
                var buffer = new byte[((int) _SharpZipFile.GetEntry(filename).Size) + 1];
                ZipEntry zipEntry = _SharpZipFile.GetEntry(filename);
                var size = (int) _SharpZipFile.GetEntry(filename).Size;
                Stream stream = _SharpZipFile.GetInputStream(zipEntry);
                stream.Read(buffer, 0, size);
                return buffer;
            }
        }

        public byte[] this[string name, CompressionMethod compressionMethod]
        {
            set
            {
                if (!_Disposed)
                {
                    _SharpZipFile.BeginUpdate();
                    var ms = new MemoryStream(value);
                    var sds = new StaticDataSource(ms);
                    _SharpZipFile.Add(sds, name, compressionMethod);
                    _SharpZipFile.CommitUpdate();
                }
            }
        }

        public ZipFile ZipFile
        {
            get
            {
                if (!_Disposed)
                    return _SharpZipFile;
                return null;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            _SharpZipFile.Close();
            _SharpZipFile = null;
        }

        #endregion

        public void Add(string directoryName)
        {
            if (!_Disposed && Directory.Exists(directoryName))
            {
                _SharpZipFile.BeginUpdate();
                _SharpZipFile.AddDirectory(directoryName);
                _SharpZipFile.CommitUpdate();
            }
        }

        public void Add(string fileName, string name)
        {
            if (!_Disposed && File.Exists(fileName))
            {
                _SharpZipFile.BeginUpdate();
                _SharpZipFile.Add(fileName);
                _SharpZipFile.CommitUpdate();
            }
        }

        public void Delete(string fileName)
        {
            if (!_Disposed)
            {
                _SharpZipFile.BeginUpdate();
                _SharpZipFile.Delete(_SharpZipFile.GetEntry(fileName));
                _SharpZipFile.CommitUpdate();
            }
        }

        public void Extract(string directory, string filename)
        {
            if (!_Disposed & _SharpZipFile.GetEntry(filename).IsFile)
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                using (var stream = new FileStream(directory + @"\" + filename, FileMode.CreateNew))
                {
                    byte[] array = this[filename];
                    stream.Write(array, 0, array.Length);
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        public void ExtractAll(string directory)
        {
            if (!_Disposed)
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                var zip = new FastZip();
                zip.ExtractZip(_SharpZipFile.Name, directory, "");
            }
        }

        public bool Contain(string fileName)
        {
            return ((!_Disposed & (_SharpZipFile.GetEntry(fileName) != null)) &&
                    _SharpZipFile.GetEntry(fileName).IsFile);
        }

        /// <summary>将指定的文件集合压缩为指定的路径下的文件
        /// </summary>
        /// <param name="fileList">指定的文件集合.</param>
        /// <param name="outputPath">指定的路径下的文件，含完整的路径与文件名</param>
        /// <param name="inpath">
        /// 压缩包中的文件的相对路径。
        /// 可以为Null和空值，当为Null或空值时，将删除文件的所有路径信息，解压时文件将全部在一个目录中。
        /// </param>
        /// <param name="level">The level.</param>
        /// <param name="password">The password.</param>
        public static void ZipFiles(string[] fileList, string outputPath, string inpath, int level = 6, string password = "")
        {
            var outZipStream = new ZipOutputStream(File.Create(outputPath)); //创建Zip流
            if (!string.IsNullOrEmpty(password))
                outZipStream.Password = password;
            if (level < 0 || level > 9)
                level = 5;
            outZipStream.SetLevel(level); //设置压缩等级
            foreach (string file in fileList)
            {
                if (!File.Exists(file) || file.EndsWith(@"/")) //如果文件不存在，或者表达的是目录
                    continue;
                string newfilename = null;
                if (inpath == null)
                {
                    newfilename = file.Contains("\\") ? file.Substring(file.LastIndexOf('\\')) : file;
                }
                else
                {
                    if (file.Contains(inpath))
                        newfilename = file.Replace(inpath, "");
                }
                var oZipEntry = new ZipEntry(newfilename)
                                    {
                                        Size = new FileInfo(file).Length,
                                        IsUnicodeText = true
                                    };
                outZipStream.PutNextEntry(oZipEntry);

                using (FileStream ostream = File.OpenRead(file))
                {
                    var obuffer = new byte[ostream.Length];
                    ostream.Read(obuffer, 0, obuffer.Length);
                    outZipStream.Write(obuffer, 0, obuffer.Length);
                    ostream.Close();
                }
            }
            outZipStream.Finish();
            outZipStream.Close();
        }

        public static void UnZipFiles(FileInfo zipPathAndFile, string outputFolder, string password = "", bool needDeleteZipFile = false)
        {
            UnZipFiles(zipPathAndFile.FullName, outputFolder, password, needDeleteZipFile);
        }

        public static void UnZipFiles(string zipPathAndFile, string outputFolder, string password = "", bool needDeleteZipFile = false)
        {
            var zipStream = new ZipInputStream(File.OpenRead(zipPathAndFile));
            if (!string.IsNullOrWhiteSpace(password))
                zipStream.Password = password;
            ZipEntry zipEntry;
            while ((zipEntry = zipStream.GetNextEntry()) != null)
            {
                string fileName = Path.GetFileName(zipEntry.Name);
                if (string.IsNullOrWhiteSpace(fileName))
                    continue;
                if (!string.IsNullOrWhiteSpace(outputFolder))
                    UtilityFile.CreateDirectory(outputFolder);
                string fullPath = outputFolder + "\\" + zipEntry.Name;
                fullPath = fullPath.Replace("\\ ", "\\");
                string fullDirPath = Path.GetDirectoryName(fullPath);
                if (fullDirPath != null && !Directory.Exists(fullDirPath))
                    UtilityFile.CreateDirectory(fullDirPath);
                using (FileStream fileStream = new FileStream(fullPath,FileMode.OpenOrCreate))
                {
                    int size = 2048;
                    var data = new byte[size];
                    while (true)
                    {
                        size = zipStream.Read(data, 0, data.Length);
                        if (size > 0)
                            fileStream.Write(data, 0, size);
                        else
                            break;
                    }
                    fileStream.Flush();
                    fileStream.Close();
                }
            }
            zipStream.Close();
            if (needDeleteZipFile)
                File.Delete(zipPathAndFile);
        }

        #region Nested type: StaticDataSource

        private class StaticDataSource : IStaticDataSource
        {
            private readonly Stream _Stream;

            public StaticDataSource(Stream stream)
            {
                _Stream = stream;
            }

            #region IStaticDataSource Members

            public Stream GetSource()
            {
                return _Stream;
            }

            #endregion
        }

        #endregion
    }
}
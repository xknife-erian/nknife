using System;
using System.Collections;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace NKnife.Compress
{
    /// <summary>
    ///     对SharpZipLib的使用进行了一些易用性封装
    /// </summary>
    public class AblyZip : IDisposable, IEnumerable
    {
        private readonly bool _disposed;
        private ZipFile _sharpZipFile;

        public AblyZip(ZipFile zipFile)
        {
            _disposed = false;
            _sharpZipFile = zipFile;
        }

        public AblyZip(string fileName)
        {
            _disposed = false;
            _sharpZipFile = File.Exists(fileName) ? new ZipFile(fileName) : ZipFile.Create(fileName);
        }

        public string Comment
        {
            get
            {
                if (!_disposed)
                    return _sharpZipFile.ZipFileComment;
                return null;
            }
            set
            {
                if (!_disposed)
                    _sharpZipFile.SetComment(value);
            }
        }

        public int Count
        {
            get
            {
                if (!_disposed)
                    return (int) _sharpZipFile.Count;
                return 0;
            }
        }

        public byte[] this[string filename]
        {
            get
            {
                if (_disposed || !_sharpZipFile.GetEntry(filename).IsFile)
                    return null;
                var buffer = new byte[((int) _sharpZipFile.GetEntry(filename).Size) + 1];
                var zipEntry = _sharpZipFile.GetEntry(filename);
                var size = (int) _sharpZipFile.GetEntry(filename).Size;
                var stream = _sharpZipFile.GetInputStream(zipEntry);
                stream.Read(buffer, 0, size);
                return buffer;
            }
        }

        public byte[] this[string name, CompressionMethod compressionMethod]
        {
            set
            {
                if (!_disposed)
                {
                    _sharpZipFile.BeginUpdate();
                    var ms = new MemoryStream(value);
                    var sds = new StaticDataSource(ms);
                    _sharpZipFile.Add(sds, name, compressionMethod);
                    _sharpZipFile.CommitUpdate();
                }
            }
        }

        internal ZipFile ZipFile
        {
            get
            {
                if (!_disposed)
                    return _sharpZipFile;
                return null;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            _sharpZipFile.Close();
            _sharpZipFile = null;
        }

        #endregion

        public IEnumerator GetEnumerator()
        {
            return ZipFile.GetEnumerator();
        }

        public void Add(string directoryName)
        {
            if (!_disposed && Directory.Exists(directoryName))
            {
                _sharpZipFile.BeginUpdate();
                _sharpZipFile.AddDirectory(directoryName);
                _sharpZipFile.CommitUpdate();
            }
        }

        public void Add(string fileName, string name)
        {
            if (!_disposed && File.Exists(fileName))
            {
                _sharpZipFile.BeginUpdate();
                _sharpZipFile.Add(fileName);
                _sharpZipFile.CommitUpdate();
            }
        }

        public void Delete(string fileName)
        {
            if (!_disposed)
            {
                _sharpZipFile.BeginUpdate();
                _sharpZipFile.Delete(_sharpZipFile.GetEntry(fileName));
                _sharpZipFile.CommitUpdate();
            }
        }

        public void Extract(string directory, string filename)
        {
            if (!_disposed & _sharpZipFile.GetEntry(filename).IsFile)
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                using (var stream = new FileStream(directory + @"\" + filename, FileMode.CreateNew))
                {
                    var array = this[filename];
                    stream.Write(array, 0, array.Length);
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        public void ExtractAll(string directory)
        {
            if (!_disposed)
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                var zip = new FastZip();
                zip.ExtractZip(_sharpZipFile.Name, directory, "");
            }
        }

        public bool Contain(string fileName)
        {
            return ((!_disposed & (_sharpZipFile.GetEntry(fileName) != null)) &&
                    _sharpZipFile.GetEntry(fileName).IsFile);
        }

        #region Nested type: StaticDataSource

        private class StaticDataSource : IStaticDataSource
        {
            private readonly Stream _stream;

            public StaticDataSource(Stream stream)
            {
                _stream = stream;
            }

            #region IStaticDataSource Members

            public Stream GetSource()
            {
                return _stream;
            }

            #endregion
        }

        #endregion
    }
}
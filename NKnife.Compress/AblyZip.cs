using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace NKnife.Compress
{
    /// <summary>
    ///     对SharpZipLib的使用进行了一些易用性封装
    /// </summary>
    public class AblyZip : IDisposable, IEnumerable
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

        internal ZipFile ZipFile
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

        public IEnumerator GetEnumerator()
        {
            return ZipFile.GetEnumerator();
        }

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
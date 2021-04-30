using System;
using System.IO;
using System.Text;

namespace NKnife.Upgrade4Github.Util.Zip
{
    /// <summary>
    ///     File entry class.
    /// </summary>
    public class GZipFileEntry
    {
        private DateTime _creationTime;


        private DateTime _lastAccessTime;
        private DateTime _lastWriteTime;


        public GZipFileEntry()
        {
        }

        public GZipFileEntry(string fileName)
        {
            var fileInfo = new FileInfo(fileName);


            OriginalLength = (int) fileInfo.Length;
            FileFullName = fileInfo.FullName;
            _creationTime = fileInfo.CreationTime;
            _lastAccessTime = fileInfo.LastAccessTime;
            _lastWriteTime = fileInfo.LastWriteTime;
        }


        public int OriginalLength { get; set; }


        public int GZipFileLength { get; set; }


        public int FileEntryLength { get; set; }

        public string FormattedStr
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append(Path.GetFileName(FileFullName));
                sb.Append($"|{OriginalLength}");
                sb.Append($"|{GZipFileLength}");
                sb.Append($"|{_lastWriteTime:yyyy-MM-dd hh:mm:ss}");
                sb.Append($"|{_lastAccessTime:yyyy-MM-dd hh:mm:ss}");
                sb.Append($"|{_creationTime:yyyy-MM-dd hh:mm:ss}");
                sb.Append($"|{FileFullName}");
                return sb.ToString();
            }
        }

        public string FileName => Path.GetFileName(FileFullName);

        public string FileFullName { get; private set; }

        public void WriteLengthInfo(Stream outStream)
        {
            var bytes1 = BitConverter.GetBytes(OriginalLength);
            var bytes2 = BitConverter.GetBytes(GZipFileLength);
            var bytes3 = BitConverter.GetBytes(FileEntryLength);


            SimpleCipher.EncryptBytes(bytes1);
            SimpleCipher.EncryptBytes(bytes2);
            SimpleCipher.EncryptBytes(bytes3);


            outStream.Write(bytes1, 0, bytes1.Length);
            outStream.Write(bytes2, 0, bytes2.Length);
            outStream.Write(bytes3, 0, bytes3.Length);
        }

        public void ReadLengthInfo(Stream srcStream)
        {
            var bytes1 = new byte[4];
            var bytes2 = new byte[4];
            var bytes3 = new byte[4];

            srcStream.Read(bytes1, 0, bytes1.Length);
            srcStream.Read(bytes2, 0, bytes2.Length);
            srcStream.Read(bytes3, 0, bytes3.Length);

            SimpleCipher.EncryptBytes(bytes1);
            SimpleCipher.EncryptBytes(bytes2);
            SimpleCipher.EncryptBytes(bytes3);

            OriginalLength = BitConverter.ToInt32(bytes1, 0);
            GZipFileLength = BitConverter.ToInt32(bytes2, 0);
            FileEntryLength = BitConverter.ToInt32(bytes3, 0);
        }

        public void WriteEntryInfo(Stream outStream)
        {
            var entryBytes = GetFileEntryByes();
            SimpleCipher.EncryptBytes(entryBytes);
            outStream.Write(entryBytes, 0, entryBytes.Length); // 文件项内容

            FileEntryLength = entryBytes.Length;
        }

        public void ReadEntryInfo(Stream srcStream)
        {
            var entryBytes = new byte[FileEntryLength];
            srcStream.Read(entryBytes, 0, entryBytes.Length); // FileEntry 字节
            SimpleCipher.EncryptBytes(entryBytes);

            var entryStr = Encoding.Default.GetString(entryBytes); // 不能用 ASCII, 要处理汉字
            var strArray = entryStr.Split('|');

            var lastWriteTimeticks = long.Parse(strArray[3]);
            var lastAccessTimeticks = long.Parse(strArray[4]);
            var lastCreateTimeticks = long.Parse(strArray[5]);

            _lastWriteTime = new DateTime(lastWriteTimeticks);
            _lastAccessTime = new DateTime(lastAccessTimeticks);
            _creationTime = new DateTime(lastCreateTimeticks);

            FileFullName = strArray[6];
        }

        public void ResetFileDateTime(string folderCompressTo)
        {
            var fileName = folderCompressTo + Path.GetFileName(FileFullName);

            File.SetLastAccessTime(fileName, _lastAccessTime);
            File.SetCreationTime(fileName, _creationTime);
            File.SetLastWriteTime(fileName, _lastWriteTime);
        }

        private byte[] GetFileEntryByes()
        {
            var sb = new StringBuilder();
            sb.Append(Path.GetFileName(FileFullName));
            sb.Append($"|{OriginalLength}");
            sb.Append($"|{GZipFileLength}");
            sb.Append($"|{_lastWriteTime.Ticks}");
            sb.Append($"|{_lastAccessTime.Ticks}");
            sb.Append($"|{_creationTime.Ticks}");
            sb.Append($"|{FileFullName}");
            var str = sb.ToString();
            return Encoding.Default.GetBytes(str); // 不能用 ASCII, 要处理汉字
        }
    }
}
using System;
using System.IO;
using System.Text;
using NKnife.Encrypt;

namespace NKnife.Zip
{
    /// <summary>
    ///     File entry class.
    /// </summary>
    public class GZipFileEntry
    {
        private DateTime _creationTime;
        private int _fileEntryLength;


        private string _fileFullName;
        private int _gZipFileLength;


        private DateTime _lastAccessTime;
        private DateTime _lastWriteTime;
        private int _originalLength;


        public GZipFileEntry()
        {
        }


        public GZipFileEntry(string fileName)
        {
            var fileInfo = new FileInfo(fileName);


            _originalLength = (int) fileInfo.Length;
            _fileFullName = fileInfo.FullName;
            _creationTime = fileInfo.CreationTime;
            _lastAccessTime = fileInfo.LastAccessTime;
            _lastWriteTime = fileInfo.LastWriteTime;
        }


        public int OriginalLength
        {
            get { return _originalLength; }
            set { _originalLength = value; }
        }


        public int GZipFileLength
        {
            get { return _gZipFileLength; }
            set { _gZipFileLength = value; }
        }


        public int FileEntryLength
        {
            get { return _fileEntryLength; }
            set { _fileEntryLength = value; }
        }

        public string FormattedStr
        {
            get
            {
                var sb = new StringBuilder();


                sb.Append(Path.GetFileName(_fileFullName));
                sb.Append("|" + _originalLength);
                sb.Append("|" + _gZipFileLength);
                sb.Append("|" + _lastWriteTime.ToString("yyyy-MM-dd hh:mm:ss"));
                sb.Append("|" + _lastAccessTime.ToString("yyyy-MM-dd hh:mm:ss"));
                sb.Append("|" + _creationTime.ToString("yyyy-MM-dd hh:mm:ss"));
                sb.Append("|" + _fileFullName);


                return sb.ToString();
            }
        }


        public string FileName
        {
            get { return Path.GetFileName(_fileFullName); }
        }


        public string FileFullName
        {
            get { return _fileFullName; }
        }


        public void WriteLengthInfo(Stream outStream)
        {
            byte[] bytes1 = BitConverter.GetBytes(_originalLength);
            byte[] bytes2 = BitConverter.GetBytes(_gZipFileLength);
            byte[] bytes3 = BitConverter.GetBytes(_fileEntryLength);


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


            _originalLength = BitConverter.ToInt32(bytes1, 0);
            _gZipFileLength = BitConverter.ToInt32(bytes2, 0);
            _fileEntryLength = BitConverter.ToInt32(bytes3, 0);
        }


        public void WriteEntryInfo(Stream outStream)
        {
            byte[] entryBytes = GetFileEntryByes();
            SimpleCipher.EncryptBytes(entryBytes);
            outStream.Write(entryBytes, 0, entryBytes.Length); // 文件项内容


            _fileEntryLength = entryBytes.Length;
        }


        public void ReadEntryInfo(Stream srcStream)
        {
            var entryBytes = new byte[_fileEntryLength];
            srcStream.Read(entryBytes, 0, entryBytes.Length); // FileEntry 字节
            SimpleCipher.EncryptBytes(entryBytes);


            string entryStr = Encoding.Default.GetString(entryBytes); // 不能用 ASCII, 要处理汉字
            string[] strArray = entryStr.Split('|');


            long lastWriteTimeticks = long.Parse(strArray[3]);
            long lastAccessTimeticks = long.Parse(strArray[4]);
            long lastCreateTimeticks = long.Parse(strArray[5]);


            _lastWriteTime = new DateTime(lastWriteTimeticks);
            _lastAccessTime = new DateTime(lastAccessTimeticks);
            _creationTime = new DateTime(lastCreateTimeticks);


            _fileFullName = strArray[6];
        }


        public void ResetFileDateTime(string folderCompressTo)
        {
            string fileName = folderCompressTo + Path.GetFileName(_fileFullName);


            File.SetLastAccessTime(fileName, _lastAccessTime);
            File.SetCreationTime(fileName, _creationTime);
            File.SetLastWriteTime(fileName, _lastWriteTime);
        }


        private byte[] GetFileEntryByes()
        {
            var sb = new StringBuilder();


            sb.Append(Path.GetFileName(_fileFullName));
            sb.Append("|" + _originalLength);
            sb.Append("|" + _gZipFileLength);
            sb.Append("|" + _lastWriteTime.Ticks);
            sb.Append("|" + _lastAccessTime.Ticks);
            sb.Append("|" + _creationTime.Ticks);
            sb.Append("|" + _fileFullName);


            string str = sb.ToString();
            return Encoding.Default.GetBytes(str); // 不能用 ASCII, 要处理汉字
        }
    }
}
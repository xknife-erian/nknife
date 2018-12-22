using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using NKnife.Encrypt;

namespace NKnife.Zip
{
    public class GZipCompressBar : ProgressBar
    {
        private const string FileExtensionName = ".gcf"; // gzip compress file
        private const int MaxFileTotalLength = 1024*1024*1024; // 1G
        private const int ReadBufferSize = 8*1024; // 8K

        private List<GZipFileEntry> _fileEntryList = new List<GZipFileEntry>();
        private string _folderDecompressTo = string.Empty;
        private string _gZipFileName = string.Empty;
        private int _nowMaxBarValue;
        private List<GZipFileEntry> _packetEntryList = new List<GZipFileEntry>();

        private byte[] _readBuffer = new byte[ReadBufferSize];

        public GZipCompressBar()
        {
        }

        public GZipCompressBar(string gzipFileName)
        {
            Visible = false;
            GZipFileName = gzipFileName;
        }

        public GZipCompressBar(string gzipFileName, string decompressFolder)
        {
            Visible = false;
            GZipFileName = gzipFileName;
            FolderDecompressTo = decompressFolder;
        }

        [Description("Set/Get GZip filename with extension .gcf")]
        public string GZipFileName
        {
            get { return _gZipFileName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _gZipFileName = value;
                    _packetEntryList.Clear();
                }
                else
                {
                    if (!IsValidFileName(value))
                    {
                        MessageBox.Show("GZip filename or it's path contains invalid char.");
                    }
                    else if (Path.GetExtension(value).ToUpper() != FileExtensionName.ToUpper())
                    {
                        MessageBox.Show("GZip filename must has extension " + FileExtensionName + ".");
                    }
                    else
                    {
                        _packetEntryList.Clear();
                        _gZipFileName = value;
                    }
                }
            }
        }

        [Description("Set/Get folder to decompress files")]
        public string FolderDecompressTo
        {
            get { return _folderDecompressTo; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _folderDecompressTo = value;
                }
                else
                {
                    if (!Directory.Exists(value))
                    {
                        MessageBox.Show("Decompress folder: " + value + " does not exists.");
                    }
                    else
                    {
                        if (value.EndsWith(@"\"))
                        {
                            _folderDecompressTo = value;
                        }
                        else
                        {
                            _folderDecompressTo = value + @"\";
                        }
                    }
                }
            }
        }

        [Description("Get the default gzip file extension name.")]
        public string DefaultFileExtentionName
        {
            get { return FileExtensionName; }
        }

        /// <summary>
        ///     ��MS��ģ��, ֻ��Ҫ���� Dispose ����
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    _readBuffer = null;


                    _fileEntryList.Clear();
                    _fileEntryList = null;
                    _packetEntryList.Clear();
                    _packetEntryList = null;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        /// <summary>
        ///     Add a file to compress list.
        /// </summary>
        public bool AppendFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
            {
                MessageBox.Show("GZipFileName is empty or does not exist.");
                return false;
            }


            var addFileInfo = new GZipFileEntry(fileName);
            long totalLength = addFileInfo.OriginalLength;


            foreach (GZipFileEntry fileEntry in _fileEntryList)
            {
                if (fileEntry.FileName.ToUpper() == addFileInfo.FileName.ToUpper())
                {
                    MessageBox.Show("File: " + fileEntry.FileName + " has exists.");
                    return false;
                }
                totalLength += fileEntry.OriginalLength;
            }


            if (totalLength > MaxFileTotalLength)
            {
                MessageBox.Show("Total files length is over " + (MaxFileTotalLength/(1024*1024)) + "M.");
                return false;
            }


            _fileEntryList.Add(addFileInfo);
            return true;
        }

        /// <summary>
        ///     Clear all add files for compress.
        /// </summary>
        public void ClearFiles()
        {
            _fileEntryList.Clear();
            _packetEntryList.Clear();
            SetStartPosition();
        }

        /// <summary>
        ///     Compress files with gzip algorithm.
        /// </summary>
        public bool Compress()
        {
            return Compressing();
        }

        /// <summary>
        ///     Decompress files from gzip file.
        /// </summary>
        public bool Decompress()
        {
            return Decompressing();
        }

        /// <summary>
        ///     If the gzipfile contains an assigned filename."
        /// </summary>
        public bool ContainsFile(string fileName)
        {
            if (!GetPacketEntryList())
            {
                return false;
            }


            string realFileName = Path.GetFileName(fileName.Trim());
            foreach (GZipFileEntry fileEntry in _packetEntryList)
            {
                if (fileEntry.FileName.ToUpper() == realFileName.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     string format: filename|fileLength|gzippedLength|lastModifiedDate|lastAccessDate|creationgDate|fullFileName
        /// </summary>
        public string GetFileEntryStringByFileName(string fileName)
        {
            if (!GetPacketEntryList())
            {
                return null;
            }


            string realFileName = Path.GetFileName(fileName.Trim());
            foreach (GZipFileEntry fileEntry in _packetEntryList)
            {
                if (fileEntry.FileName.ToUpper() == fileName.ToUpper())
                {
                    return fileEntry.FormattedStr;
                }
            }
            return null;
        }

        /// <summary>
        ///     string format: filename|fileLength|gzippedLength|lastModifiedDate|lastAccessDate|creationgDate|fullFileName
        /// </summary>
        public List<string> GetFileEntryStringList()
        {
            if (!GetPacketEntryList())
            {
                return null;
            }


            var fileInfoList = new List<string>();


            foreach (GZipFileEntry fileEntry in _packetEntryList)
            {
                fileInfoList.Add(fileEntry.FormattedStr);
            }
            return fileInfoList;
        }

        private bool Compressing()
        {
            bool opSuccess = false;


            if (_fileEntryList.Count == 0)
            {
                MessageBox.Show("There has no compress file.");
                return opSuccess;
            }


            if (string.IsNullOrEmpty(_gZipFileName))
            {
                MessageBox.Show("GZipFileName is empty or not set.");
                return opSuccess;
            }


            SetApplicationCursor(Cursors.WaitCursor);


            try
            {
                using (var outStream = new FileStream(_gZipFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    _packetEntryList.Clear();
                    WriteHeaderEmptyInfo(outStream); // д�ļ������ֽ�, ѹ������������ʵ������
                    SetProgressBarMaxValue(false);
                    ShowBeginStep();


                    foreach (GZipFileEntry fileEntry in _fileEntryList)
                    {
                        SetProgressBarNowMaxValue(fileEntry, false);


                        fileEntry.WriteEntryInfo(outStream);
                        ShowProgressStep();
                        CompressFile(fileEntry, outStream);
                        _packetEntryList.Add(fileEntry);
                    }


                    WriteHeaderLengthInfo(outStream); // �����ļ�ͷ, ��ʱ�и���ĳ�����Ϣ
                }
                opSuccess = true;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                ShowFinalStep();
                SetApplicationCursor(Cursors.Default);
            }
            return opSuccess;
        }

        private bool Decompressing()
        {
            bool opSuccess = false;


            if (string.IsNullOrEmpty(_folderDecompressTo))
            {
                MessageBox.Show("Decompress folder is empty.");
                return opSuccess;
            }


            if (string.IsNullOrEmpty(_gZipFileName))
            {
                MessageBox.Show("GZipFileName is empty or does not exist.");
                return opSuccess;
            }


            SetApplicationCursor(Cursors.WaitCursor);


            try
            {
                using (var srcStream = new FileStream(_gZipFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    _packetEntryList.Clear();
                    ReadHeaderLengthInfo(srcStream); // ��ø���ĳ�����Ϣ, ȱ���ļ����е��ļ��������ڵ���Ϣ
                    SetProgressBarMaxValue(true);
                    ShowBeginStep();


                    foreach (GZipFileEntry fileEntry in _packetEntryList)
                    {
                        SetProgressBarNowMaxValue(fileEntry, true);


                        fileEntry.ReadEntryInfo(srcStream); // ����ǰ������ڡ��ļ�����Ϣ
                        ShowProgressStep();
                        DecompressFile(srcStream, fileEntry);
                        fileEntry.ResetFileDateTime(_folderDecompressTo);
                    }
                }
                opSuccess = true;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                ShowFinalStep();
                SetApplicationCursor(Cursors.Default);
            }


            return opSuccess;
        }

        /// <summary>
        ///     Compress one file.
        /// </summary>
        private void CompressFile(GZipFileEntry fileEntry, Stream outStream)
        {
            long preStreamPosition = outStream.Position;


            using (var srcStream = new FileStream(fileEntry.FileFullName, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var zipStream = new GZipStream(outStream, CompressionMode.Compress, true))
            {
                ShowProgressStep();


                int readCount = ReadBufferSize;
                while (readCount == ReadBufferSize)
                {
                    readCount = srcStream.Read(_readBuffer, 0, ReadBufferSize);
                    zipStream.Write(_readBuffer, 0, readCount);


                    ShowProgressStep();
                }
            }


            fileEntry.GZipFileLength = (int) (outStream.Position - preStreamPosition); // д��ĳ���
        }

        /// <summary>
        ///     Deompress one file.
        /// </summary>
        private void DecompressFile(Stream srcStream, GZipFileEntry fileEntry)
        {
            using (var outStream = new FileStream(_folderDecompressTo + fileEntry.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var memStream = new MemoryStream())
            using (var zipStream = new GZipStream(memStream, CompressionMode.Decompress, true))
            {
                int gzipFileLength = fileEntry.GZipFileLength;
                int readCount;
                while (gzipFileLength > 0)
                {
                    int maxCount = Math.Min(gzipFileLength, ReadBufferSize);

                    readCount = srcStream.Read(_readBuffer, 0, maxCount);
                    memStream.Write(_readBuffer, 0, readCount);


                    gzipFileLength -= readCount;


                    ShowProgressStep();
                }


                memStream.Position = 0;
                readCount = ReadBufferSize;
                while (readCount == ReadBufferSize)
                {
                    readCount = zipStream.Read(_readBuffer, 0, ReadBufferSize);
                    outStream.Write(_readBuffer, 0, readCount);


                    ShowProgressStep();
                }
            }
            ShowProgressStep();
        }

        /// <summary>
        ///     д��ͷ�ֽ�, ����ռλ��
        /// </summary>
        private void WriteHeaderEmptyInfo(Stream outStream)
        {
            int headerSize = 1 + _fileEntryList.Count*3; // ǰ4���ֽ����ļ���, ÿ���ļ�3����, �ֱ���: ԭ�ļ�����ѹ���󳤡��ļ��
            var headerBytes = new byte[4*headerSize];
            outStream.Write(headerBytes, 0, headerBytes.Length);
        }

        /// <summary>
        ///     дʵ�ʵ��ļ������ļ����ȡ�����ֽ�
        /// </summary>
        private void WriteHeaderLengthInfo(Stream outStream)
        {
            byte[] fileCountBytes = BitConverter.GetBytes(_packetEntryList.Count);
            SimpleCipher.EncryptBytes(fileCountBytes);


            outStream.Position = 0;
            outStream.Write(fileCountBytes, 0, fileCountBytes.Length);


            foreach (GZipFileEntry entry in _packetEntryList)
            {
                entry.WriteLengthInfo(outStream);
            }
        }

        private void ReadHeaderLengthInfo(Stream srcStream)
        {
            var fileCountBytes = new byte[4];
            srcStream.Read(fileCountBytes, 0, fileCountBytes.Length);
            SimpleCipher.EncryptBytes(fileCountBytes);


            int fileCount = BitConverter.ToInt32(fileCountBytes, 0);


            for (int k = 1; k <= fileCount; k++)
            {
                var entry = new GZipFileEntry();
                entry.ReadLengthInfo(srcStream);
                _packetEntryList.Add(entry);
            }
        }

        private bool GetPacketEntryList()
        {
            if (_packetEntryList.Count > 0)
            {
                return true;
            }


            if (string.IsNullOrEmpty(_gZipFileName) || !File.Exists(_gZipFileName))
            {
                MessageBox.Show("GZipFileName is empty or does not exist.");
                return false;
            }


            bool opSuccess = false;
            SetApplicationCursor(Cursors.WaitCursor);


            try
            {
                using (var srcStream = new FileStream(_gZipFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    ReadHeaderLengthInfo(srcStream);
                    foreach (GZipFileEntry fileEntry in _packetEntryList)
                    {
                        fileEntry.ReadEntryInfo(srcStream);
                        srcStream.Position += fileEntry.GZipFileLength;
                    }
                }
                opSuccess = true;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                SetApplicationCursor(Cursors.Default);
            }
            return opSuccess;
        }

        private int GetFileMaxStepLength(GZipFileEntry fileEntry, bool decompress)
        {
            int maxLength = Math.Max(fileEntry.OriginalLength, fileEntry.GZipFileLength);
            int stepValue = 0;


            if (decompress)
            {
                stepValue++; // ȡ�ļ���
                stepValue += 2*(maxLength/ReadBufferSize); // ����ѹ����
                stepValue++; // �ر��ļ�
            }
            else
            {
                stepValue++; // ��Դ�ļ�
                stepValue++; // д�ļ���Ϣ��
                stepValue += maxLength/ReadBufferSize; // ѹ��
            }


            return stepValue;
        }

        private void SetProgressBarMaxValue(bool decompress)
        {
            SetStartPosition();
            _nowMaxBarValue = 0;


            int maxBarValue = 1; // ��/�����ļ�
            if (decompress)
            {
                foreach (GZipFileEntry fileEntry in _packetEntryList)
                {
                    maxBarValue += GetFileMaxStepLength(fileEntry, decompress); // ��ÿ���ļ��Ĳ���
                }
            }
            else
            {
                foreach (GZipFileEntry fileEntry in _fileEntryList)
                {
                    maxBarValue += GetFileMaxStepLength(fileEntry, decompress); // ��ÿ���ļ��Ĳ���
                }
            }
            maxBarValue += 1; // �����β
            Maximum = maxBarValue;
        }

        private void SetProgressBarNowMaxValue(GZipFileEntry fileEntry, bool decompress)
        {
            _nowMaxBarValue += GetFileMaxStepLength(fileEntry, decompress);
        }

        /// <summary>
        ///     ���õ�ǰ�ؼ�����ȫ�����ؼ��Ĺ��
        /// </summary>
        private void SetApplicationCursor(Cursor cursor)
        {
            Cursor = cursor;
            Control parent = Parent;
            while (parent != null)
            {
                parent.Cursor = cursor;
                parent = parent.Parent;
            }
        }

        private void SetStartPosition()
        {
            Value = 0;
            Refresh();
        }

        private void ShowBeginStep()
        {
            Value += 1;
            Refresh();
        }

        private void ShowProgressStep()
        {
            if (Value + 1 < _nowMaxBarValue)
            {
                Value += 1;
            }
        }

        private void ShowFinalStep()
        {
            while (Value + 1 < Maximum)
            {
                Value += 1;
            }
            Value = Maximum;
            Refresh();
        }

        private bool IsValidFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            string realName = Path.GetFileName(fileName);
            string pathName = Path.GetDirectoryName(fileName);


            char[] errChars = Path.GetInvalidPathChars();
            if (realName.IndexOfAny(errChars) >= 0)
            {
                return false;
            }


            errChars = Path.GetInvalidPathChars();
            if (pathName.IndexOfAny(errChars) >= 0)
            {
                return false;
            }
            return true;
        }
    }
}
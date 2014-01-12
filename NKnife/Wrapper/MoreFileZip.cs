//using System;
//using System.IO;
//using System.Text;
//using System.IO.Compression;
//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Windows.Forms;
//using System.ComponentModel;
//using System.Drawing;


//namespace CSUST.Data
//{
//    public class TGZipCompressBar : TSmartProgressBar
//    {
//        private const string m_FileExtensionName = ".gcf";  // gzip compress file
//        private const int m_MaxFileTotalLength = 1024 * 1024 * 1024;  // 1G
//        private const int m_ReadBufferSize = 8 * 1024;  // 8K


//        private string m_GZipFileName = string.Empty;
//        private string m_FolderDecompressTo = string.Empty;


//        private List<TGZipFileEntry> m_FileEntryList = new List<TGZipFileEntry>();
//        private List<TGZipFileEntry> m_PacketEntryList = new List<TGZipFileEntry>();


//        private byte[] m_ReadBuffer = new byte[m_ReadBufferSize];


//        private int m_NowMaxBarValue = 0;


//        public TGZipCompressBar()
//        {
//        }


//        public TGZipCompressBar(string gzipFileName)
//        {
//            this.Visible = false;
//            this.GZipFileName = gzipFileName;
//        }


//        public TGZipCompressBar(string gzipFileName, string decompressFolder)
//        {
//            this.Visible = false;
//            this.GZipFileName = gzipFileName;
//            this.FolderDecompressTo = decompressFolder;
//        }


//        /// <summary>
//        /// 按MS的模板, 只需要重载 Dispose 方法
//        /// </summary>
//        protected override void Dispose(bool disposing)
//        {
//            try
//            {
//                if (disposing)
//                {
//                    m_ReadBuffer = null;


//                    m_FileEntryList.Clear();
//                    m_FileEntryList = null;
//                    m_PacketEntryList.Clear();
//                    m_PacketEntryList = null;
//                }
//            }
//            finally
//            {
//                base.Dispose(disposing);
//            }
//        }


//        [Description("Set/Get GZip filename with extension .gcf")]
//        public string GZipFileName
//        {
//            get
//            {
//                return m_GZipFileName;
//            }
//            set
//            {
//                if (string.IsNullOrEmpty(value))
//                {
//                    this.m_GZipFileName = value;
//                    this.m_PacketEntryList.Clear();
//                }
//                else
//                {
//                    if (!this.IsValidFileName(value))
//                    {
//                        MessageBox.Show("GZip filename or it's path contains invalid char.");
//                    }
//                    else if (Path.GetExtension(value).ToUpper() != m_FileExtensionName.ToUpper())
//                    {
//                        MessageBox.Show("GZip filename must has extension " + m_FileExtensionName + ".");
//                    }
//                    else
//                    {
//                        this.m_PacketEntryList.Clear();
//                        this.m_GZipFileName = value;
//                    }
//                }
//            }
//        }


//        [Description("Set/Get folder to decompress files")]
//        public string FolderDecompressTo
//        {
//            get
//            {
//                return m_FolderDecompressTo;
//            }
//            set
//            {
//                if (string.IsNullOrEmpty(value))
//                {
//                    m_FolderDecompressTo = value;
//                }
//                else
//                {
//                    if (!Directory.Exists(value))
//                    {
//                        MessageBox.Show("Decompress folder: " + value + " does not exists.");
//                    }
//                    else
//                    {
//                        if (value.EndsWith(@"\"))
//                        {
//                            m_FolderDecompressTo = value;
//                        }
//                        else
//                        {
//                            m_FolderDecompressTo = value + @"\";
//                        }
//                    }
//                }
//            }
//        }


//        [Description("Get the default gzip file extension name.")]
//        public string DefaultFileExtentionName
//        {
//            get { return m_FileExtensionName; }
//        }


//        /// <summary>
//        /// Add a file to compress list.
//        /// </summary>
//        public bool AppendFile(string fileName)
//        {
//            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
//            {
//                MessageBox.Show("GZipFileName is empty or does not exist.");
//                return false;
//            }


//            TGZipFileEntry addFileInfo = new TGZipFileEntry(fileName);
//            long totalLength = addFileInfo.OriginalLength;


//            foreach (TGZipFileEntry fileEntry in m_FileEntryList)
//            {
//                if (fileEntry.FileName.ToUpper() == addFileInfo.FileName.ToUpper())
//                {
//                    MessageBox.Show("File: " + fileEntry.FileName + " has exists.");
//                    return false;
//                }
//                totalLength += fileEntry.OriginalLength;
//            }


//            if (totalLength > m_MaxFileTotalLength)
//            {
//                MessageBox.Show("Total files length is over " + (m_MaxFileTotalLength / (1024 * 1024)).ToString() + "M.");
//                return false;
//            }


//            m_FileEntryList.Add(addFileInfo);
//            return true;
//        }


//        /// <summary>
//        /// Clear all add files for compress.
//        /// </summary>
//        public void ClearFiles()
//        {
//            this.m_FileEntryList.Clear();
//            this.m_PacketEntryList.Clear();
//            this.SetStartPosition();
//        }


//        /// <summary>
//        /// Compress files with gzip algorithm.
//        /// </summary>
//        public bool Compress()
//        {
//            return this.Compressing();
//        }


//        /// <summary>
//        /// Decompress files from gzip file.
//        /// </summary>


//        public bool Decompress()
//        {
//            return this.Decompressing();
//        }


//        /// <summary>
//        /// If the gzipfile contains an assigned filename."
//        /// </summary>
//        public bool ContainsFile(string fileName)
//        {
//            if (!this.GetPacketEntryList())
//            {
//                return false;
//            }


//            string realFileName = Path.GetFileName(fileName.Trim());
//            foreach (TGZipFileEntry fileEntry in m_PacketEntryList)
//            {
//                if (fileEntry.FileName.ToUpper() == realFileName.ToUpper())
//                {
//                    return true;
//                }
//            }
//            return false;
//        }

//        /// <summary>
//        /// string format: filename|fileLength|gzippedLength|lastModifiedDate|lastAccessDate|creationgDate|fullFileName
//        /// </summary>
//        public string GetFileEntryStringByFileName(string fileName)
//        {
//            if (!this.GetPacketEntryList())
//            {
//                return null;
//            }


//            string realFileName = Path.GetFileName(fileName.Trim());
//            foreach (TGZipFileEntry fileEntry in m_PacketEntryList)
//            {
//                if (fileEntry.FileName.ToUpper() == fileName.ToUpper())
//                {
//                    return fileEntry.FormattedStr;
//                }
//            }
//            return null;
//        }


//        /// <summary>
//        /// string format: filename|fileLength|gzippedLength|lastModifiedDate|lastAccessDate|creationgDate|fullFileName
//        /// </summary>
//        public List<string> GetFileEntryStringList()
//        {
//            if (!this.GetPacketEntryList())
//            {
//                return null;
//            }


//            List<string> fileInfoList = new List<string>();


//            foreach (TGZipFileEntry fileEntry in m_PacketEntryList)
//            {
//                fileInfoList.Add(fileEntry.FormattedStr);
//            }
//            return fileInfoList;
//        }


//        private bool Compressing()
//        {
//            bool opSuccess = false;


//            if (m_FileEntryList.Count == 0)
//            {
//                MessageBox.Show("There has no compress file.");
//                return opSuccess;
//            }


//            if (string.IsNullOrEmpty(m_GZipFileName))
//            {
//                MessageBox.Show("GZipFileName is empty or not set.");
//                return opSuccess;
//            }


//            this.SetApplicationCursor(Cursors.WaitCursor);


//            try
//            {
//                using (FileStream outStream = new FileStream(m_GZipFileName, FileMode.Create, FileAccess.Write, FileShare.None))
//                {
//                    this.m_PacketEntryList.Clear();
//                    this.WriteHeaderEmptyInfo(outStream);  // 写文件长度字节, 压缩结束后再填实际数据
//                    this.SetProgressBarMaxValue(false);
//                    this.ShowBeginStep();


//                    foreach (TGZipFileEntry fileEntry in m_FileEntryList)
//                    {
//                        this.SetProgressBarNowMaxValue(fileEntry, false);


//                        fileEntry.WriteEntryInfo(outStream);
//                        this.ShowProgressStep();
//                        this.CompressFile(fileEntry, outStream);
//                        this.m_PacketEntryList.Add(fileEntry);
//                    }


//                    this.WriteHeaderLengthInfo(outStream);  // 再填文件头, 此时有各块的长度信息
//                }
//                opSuccess = true;
//            }
//            catch (Exception err)
//            {
//                throw new Exception(err.Message);
//            }
//            finally
//            {
//                this.ShowFinalStep();
//                this.SetApplicationCursor(Cursors.Default);
//            }
//            return opSuccess;
//        }


//        private bool Decompressing()
//        {
//            bool opSuccess = false;


//            if (string.IsNullOrEmpty(m_FolderDecompressTo))
//            {
//                MessageBox.Show("Decompress folder is empty.");
//                return opSuccess;
//            }


//            if (string.IsNullOrEmpty(m_GZipFileName))
//            {
//                MessageBox.Show("GZipFileName is empty or does not exist.");
//                return opSuccess;
//            }


//            this.SetApplicationCursor(Cursors.WaitCursor);


//            try
//            {
//                using (FileStream srcStream = new FileStream(m_GZipFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
//                {
//                    this.m_PacketEntryList.Clear();
//                    this.ReadHeaderLengthInfo(srcStream);  // 获得各块的长度信息, 缺少文件项中的文件名、日期等信息
//                    this.SetProgressBarMaxValue(true);
//                    this.ShowBeginStep();


//                    foreach (TGZipFileEntry fileEntry in m_PacketEntryList)
//                    {
//                        this.SetProgressBarNowMaxValue(fileEntry, true);


//                        fileEntry.ReadEntryInfo(srcStream);  // 读当前项的日期、文件名信息
//                        this.ShowProgressStep();
//                        this.DecompressFile(srcStream, fileEntry);
//                        fileEntry.ResetFileDateTime(m_FolderDecompressTo);
//                    }
//                }
//                opSuccess = true;
//            }
//            catch (Exception err)
//            {
//                throw new Exception(err.Message);
//            }
//            finally
//            {
//                this.ShowFinalStep();
//                this.SetApplicationCursor(Cursors.Default);
//            }


//            return opSuccess;
//        }


//        /// <summary>
//        /// Compress one file.
//        /// </summary>
//        private void CompressFile(TGZipFileEntry fileEntry, Stream outStream)
//        {
//            long preStreamPosition = outStream.Position;


//            using (FileStream srcStream = new FileStream(fileEntry.FileFullName, FileMode.Open, FileAccess.Read, FileShare.Read))
//            using (GZipStream zipStream = new GZipStream(outStream, CompressionMode.Compress, true))
//            {
//                this.ShowProgressStep();


//                int readCount = m_ReadBufferSize;
//                while (readCount == m_ReadBufferSize)
//                {
//                    readCount = srcStream.Read(m_ReadBuffer, 0, m_ReadBufferSize);
//                    zipStream.Write(m_ReadBuffer, 0, readCount);


//                    this.ShowProgressStep();
//                }
//            }


//            fileEntry.GZipFileLength = (int)(outStream.Position - preStreamPosition);  // 写入的长度
//        }


//        /// <summary>
//        /// Deompress one file.
//        /// </summary>
//        private void DecompressFile(Stream srcStream, TGZipFileEntry fileEntry)
//        {
//            using (FileStream outStream = new FileStream(this.m_FolderDecompressTo + fileEntry.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
//            using (MemoryStream memStream = new MemoryStream())
//            using (GZipStream zipStream = new GZipStream(memStream, CompressionMode.Decompress, true))
//            {
//                int gzipFileLength = fileEntry.GZipFileLength;
//                int readCount;
//                while (gzipFileLength > 0)
//                {
//                    int maxCount = Math.Min(gzipFileLength, m_ReadBufferSize);

//                    readCount = srcStream.Read(m_ReadBuffer, 0, maxCount);
//                    memStream.Write(m_ReadBuffer, 0, readCount);


//                    gzipFileLength -= readCount;


//                    this.ShowProgressStep();
//                }


//                memStream.Position = 0;
//                readCount = m_ReadBufferSize;
//                while (readCount == m_ReadBufferSize)
//                {
//                    readCount = zipStream.Read(m_ReadBuffer, 0, m_ReadBufferSize);
//                    outStream.Write(m_ReadBuffer, 0, readCount);


//                    this.ShowProgressStep();
//                }
//            }
//            this.ShowProgressStep();
//        }


//        /// <summary>
//        /// 写空头字节, 用于占位置
//        /// </summary>
//        private void WriteHeaderEmptyInfo(Stream outStream)
//        {
//            int headerSize = 1 + m_FileEntryList.Count * 3;  // 前4个字节是文件数, 每个文件3部分, 分别是: 原文件长、压缩后长、文件项长
//            byte[] headerBytes = new byte[4 * headerSize];
//            outStream.Write(headerBytes, 0, headerBytes.Length);
//        }


//        /// <summary>
//        /// 写实际的文件数、文件长度、项长度字节
//        /// </summary>
//        private void WriteHeaderLengthInfo(Stream outStream)
//        {
//            byte[] fileCountBytes = BitConverter.GetBytes((int)m_PacketEntryList.Count);
//            TCipher.EncryptBytes(fileCountBytes);


//            outStream.Position = 0;
//            outStream.Write(fileCountBytes, 0, fileCountBytes.Length);


//            foreach (TGZipFileEntry entry in m_PacketEntryList)
//            {
//                entry.WriteLengthInfo(outStream);
//            }
//        }


//        private void ReadHeaderLengthInfo(Stream srcStream)
//        {
//            byte[] fileCountBytes = new byte[4];
//            srcStream.Read(fileCountBytes, 0, fileCountBytes.Length);
//            TCipher.EncryptBytes(fileCountBytes);


//            int fileCount = BitConverter.ToInt32(fileCountBytes, 0);


//            for (int k = 1; k <= fileCount; k++)
//            {
//                TGZipFileEntry entry = new TGZipFileEntry();
//                entry.ReadLengthInfo(srcStream);
//                m_PacketEntryList.Add(entry);
//            }
//        }


//        private bool GetPacketEntryList()
//        {
//            if (m_PacketEntryList.Count > 0)
//            {
//                return true;
//            }


//            if (string.IsNullOrEmpty(m_GZipFileName) || !File.Exists(m_GZipFileName))
//            {
//                MessageBox.Show("GZipFileName is empty or does not exist.");
//                return false;
//            }


//            bool opSuccess = false;
//            this.SetApplicationCursor(Cursors.WaitCursor);


//            try
//            {
//                using (FileStream srcStream = new FileStream(m_GZipFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
//                {
//                    this.ReadHeaderLengthInfo(srcStream);
//                    foreach (TGZipFileEntry fileEntry in m_PacketEntryList)
//                    {
//                        fileEntry.ReadEntryInfo(srcStream);
//                        srcStream.Position += fileEntry.GZipFileLength;
//                    }
//                }
//                opSuccess = true;
//            }
//            catch (Exception err)
//            {
//                throw new Exception(err.Message);
//            }
//            finally
//            {
//                this.SetApplicationCursor(Cursors.Default);
//            }
//            return opSuccess;
//        }


//        private int GetFileMaxStepLength(TGZipFileEntry fileEntry, bool decompress)
//        {
//            int maxLength = Math.Max(fileEntry.OriginalLength, fileEntry.GZipFileLength);
//            int stepValue = 0;


//            if (decompress)
//            {
//                stepValue++;  // 取文件项
//                stepValue += 2 * (maxLength / m_ReadBufferSize);  // 产生压缩流
//                stepValue++;  // 关闭文件
//            }
//            else
//            {
//                stepValue++;  // 打开源文件
//                stepValue++;  // 写文件信息项
//                stepValue += maxLength / m_ReadBufferSize;  // 压缩
//            }


//            return stepValue;
//        }


//        private void SetProgressBarMaxValue(bool decompress)
//        {
//            this.SetStartPosition();
//            m_NowMaxBarValue = 0;


//            int maxBarValue = 1;  // 打开/建立文件
//            if (decompress)
//            {
//                foreach (TGZipFileEntry fileEntry in m_PacketEntryList)
//                {
//                    maxBarValue += this.GetFileMaxStepLength(fileEntry, decompress);  // 加每个文件的步长
//                }
//            }
//            else
//            {
//                foreach (TGZipFileEntry fileEntry in m_FileEntryList)
//                {
//                    maxBarValue += this.GetFileMaxStepLength(fileEntry, decompress);  // 加每个文件的步长
//                }
//            }
//            maxBarValue += 1;  // 最后收尾
//            this.Maximum = maxBarValue;
//        }


//        private void SetProgressBarNowMaxValue(TGZipFileEntry fileEntry, bool decompress)
//        {
//            m_NowMaxBarValue += this.GetFileMaxStepLength(fileEntry, decompress);
//        }


//        /// <summary>
//        /// 设置当前控件及其全部父控件的光标
//        /// </summary>
//        private void SetApplicationCursor(Cursor cursor)
//        {
//            this.Cursor = cursor;
//            Control parent = this.Parent;
//            while (parent != null)
//            {
//                parent.Cursor = cursor;
//                parent = parent.Parent;
//            }
//        }


//        private void SetStartPosition()
//        {
//            this.Value = 0;
//            this.Refresh();
//        }


//        private void ShowBeginStep()
//        {
//            this.Value += 1;
//            this.Refresh();
//        }


//        private void ShowProgressStep()
//        {
//            if (this.Value + 1 < m_NowMaxBarValue)
//            {
//                this.Value += 1;
//            }
//        }


//        private void ShowFinalStep()
//        {
//            while (this.Value + 1 < this.Maximum)
//            {
//                this.Value += 1;
//            }
//            this.Value = this.Maximum;
//            this.Refresh();
//        }


//        private bool IsValidFileName(string fileName)
//        {
//            if (string.IsNullOrEmpty(fileName))
//            {
//                return false;
//            }
//            else
//            {
//                string realName = Path.GetFileName(fileName);
//                string pathName = Path.GetDirectoryName(fileName);


//                char[] errChars = Path.GetInvalidPathChars();
//                if (realName.IndexOfAny(errChars) >= 0)
//                {
//                    return false;
//                }


//                errChars = Path.GetInvalidPathChars();
//                if (pathName.IndexOfAny(errChars) >= 0)
//                {
//                    return false;
//                }
//            }
//            return true;
//        }


//    }


//    /// <summary>
//    /// File entry class.
//    /// </summary>
//    public class TGZipFileEntry
//    {
//        private int m_OriginalLength;
//        private int m_GZipFileLength;
//        private int m_FileEntryLength;


//        private string m_FileFullName;


//        private DateTime m_CreationTime;
//        private DateTime m_LastAccessTime;
//        private DateTime m_LastWriteTime;


//        public TGZipFileEntry() { }


//        public TGZipFileEntry(string fileName)
//        {
//            FileInfo fileInfo = new FileInfo(fileName);


//            m_OriginalLength = (int)fileInfo.Length;
//            m_FileFullName = fileInfo.FullName;
//            m_CreationTime = fileInfo.CreationTime;
//            m_LastAccessTime = fileInfo.LastAccessTime;
//            m_LastWriteTime = fileInfo.LastWriteTime;
//        }


//        public int OriginalLength
//        {
//            get { return m_OriginalLength; }
//            set { m_OriginalLength = value; }
//        }


//        public int GZipFileLength
//        {
//            get { return m_GZipFileLength; }
//            set { m_GZipFileLength = value; }
//        }


//        public int FileEntryLength
//        {
//            get { return m_FileEntryLength; }
//            set { m_FileEntryLength = value; }
//        }


//        public void WriteLengthInfo(Stream outStream)
//        {
//            byte[] bytes1 = BitConverter.GetBytes(m_OriginalLength);
//            byte[] bytes2 = BitConverter.GetBytes(m_GZipFileLength);
//            byte[] bytes3 = BitConverter.GetBytes(m_FileEntryLength);


//            TCipher.EncryptBytes(bytes1);
//            TCipher.EncryptBytes(bytes2);
//            TCipher.EncryptBytes(bytes3);


//            outStream.Write(bytes1, 0, bytes1.Length);
//            outStream.Write(bytes2, 0, bytes2.Length);
//            outStream.Write(bytes3, 0, bytes3.Length);
//        }


//        public void ReadLengthInfo(Stream srcStream)
//        {
//            byte[] bytes1 = new byte[4];
//            byte[] bytes2 = new byte[4];
//            byte[] bytes3 = new byte[4];


//            srcStream.Read(bytes1, 0, bytes1.Length);
//            srcStream.Read(bytes2, 0, bytes2.Length);
//            srcStream.Read(bytes3, 0, bytes3.Length);


//            TCipher.EncryptBytes(bytes1);
//            TCipher.EncryptBytes(bytes2);
//            TCipher.EncryptBytes(bytes3);


//            m_OriginalLength = BitConverter.ToInt32(bytes1, 0);
//            m_GZipFileLength = BitConverter.ToInt32(bytes2, 0);
//            m_FileEntryLength = BitConverter.ToInt32(bytes3, 0);
//        }


//        public void WriteEntryInfo(Stream outStream)
//        {
//            byte[] entryBytes = this.GetFileEntryByes();
//            TCipher.EncryptBytes(entryBytes);
//            outStream.Write(entryBytes, 0, entryBytes.Length);  // 文件项内容


//            m_FileEntryLength = entryBytes.Length;
//        }


//        public void ReadEntryInfo(Stream srcStream)
//        {
//            byte[] entryBytes = new byte[m_FileEntryLength];
//            srcStream.Read(entryBytes, 0, entryBytes.Length);  // FileEntry 字节
//            TCipher.EncryptBytes(entryBytes);


//            string entryStr = Encoding.Default.GetString(entryBytes);  // 不能用 ASCII, 要处理汉字
//            string[] strArray = entryStr.Split('|');


//            long lastWriteTimeticks = long.Parse(strArray[3]);
//            long lastAccessTimeticks = long.Parse(strArray[4]);
//            long lastCreateTimeticks = long.Parse(strArray[5]);


//            m_LastWriteTime = new DateTime(lastWriteTimeticks);
//            m_LastAccessTime = new DateTime(lastAccessTimeticks);
//            m_CreationTime = new DateTime(lastCreateTimeticks);


//            m_FileFullName = strArray[6];
//        }


//        public void ResetFileDateTime(string folderCompressTo)
//        {
//            string fileName = folderCompressTo + Path.GetFileName(m_FileFullName);


//            File.SetLastAccessTime(fileName, m_LastAccessTime);
//            File.SetCreationTime(fileName, m_CreationTime);
//            File.SetLastWriteTime(fileName, m_LastWriteTime);
//        }


//        public string FormattedStr
//        {
//            get
//            {
//                StringBuilder sb = new StringBuilder();


//                sb.Append(Path.GetFileName(m_FileFullName));
//                sb.Append("|" + m_OriginalLength.ToString());
//                sb.Append("|" + m_GZipFileLength.ToString());
//                sb.Append("|" + m_LastWriteTime.ToString("yyyy-MM-dd hh:mm:ss"));
//                sb.Append("|" + m_LastAccessTime.ToString("yyyy-MM-dd hh:mm:ss"));
//                sb.Append("|" + m_CreationTime.ToString("yyyy-MM-dd hh:mm:ss"));
//                sb.Append("|" + m_FileFullName);


//                return sb.ToString();
//            }
//        }


//        public string FileName
//        {
//            get { return Path.GetFileName(m_FileFullName); }
//        }


//        public string FileFullName
//        {
//            get { return m_FileFullName; }
//        }


//        private byte[] GetFileEntryByes()
//        {
//            StringBuilder sb = new StringBuilder();


//            sb.Append(Path.GetFileName(m_FileFullName));
//            sb.Append("|" + m_OriginalLength.ToString());
//            sb.Append("|" + m_GZipFileLength.ToString());
//            sb.Append("|" + m_LastWriteTime.Ticks.ToString());
//            sb.Append("|" + m_LastAccessTime.Ticks.ToString());
//            sb.Append("|" + m_CreationTime.Ticks.ToString());
//            sb.Append("|" + m_FileFullName);


//            string str = sb.ToString();
//            return Encoding.Default.GetBytes(str);  // 不能用 ASCII, 要处理汉字
//        }
//    }


//    /// <summary>
//    /// 简单加密类
//    /// </summary>
//    public class TCipher
//    {
//        private static readonly byte[] m_XORVector = new byte[] { 8, 3, 6, 1, 0, 9 };


//        public static void EncryptBytes(byte[] byteArray)
//        {
//            int k = 0;
//            for (int i = 0; i < byteArray.Length; i++)
//            {
//                byteArray[i] ^= m_XORVector[k];
//                k++;
//                k = k % m_XORVector.Length;
//            }
//        }
//    }
//}
